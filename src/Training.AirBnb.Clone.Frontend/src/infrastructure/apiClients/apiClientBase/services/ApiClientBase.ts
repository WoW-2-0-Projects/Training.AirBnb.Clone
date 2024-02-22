import type { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";
import { ApiResponse } from "../models/ApiResponse";
import axios, {HttpStatusCode} from "axios";
import type { ProblemDetails } from "../models/ProblemDetails";
import { LocalStorageService } from "@/infrastructure/services/storage/LocalStorageService";

export default class ApiClientBase {
    public readonly client: AxiosInstance;
    private readonly localStorageService: LocalStorageService;
    public mapResponse!: <T>(response: ApiResponse<T>) => ApiResponse<T>

    constructor(config: AxiosRequestConfig) {
        this.client = axios.create(config);
        this.localStorageService = new LocalStorageService();


        // Access token interceptor
        this.client.interceptors.request.use(async (config) => {
            const accessToken = this.localStorageService.get<string>("accessToken");
            if (accessToken) {
                config.headers.Authorization = `Bearer ${accessToken}`;
            }

            return config;
        });

        // Refresh token interceptor
        this.client.interceptors.response.use((response) => {
            return response;
        }, async (error) => {

            if (error.response.status == HttpStatusCode.Unauthorized && !error.retry) {

                error._retry = true;

                const refreshToken = this.localStorageService.get<string>("refreshToken");

                if (refreshToken) {
                    try {
                        // Make a request to the refresh token endpoint
                        const response = await axios.post('/auth/refresh-token', { refreshToken });
                        const newAccessToken = response.data.accessToken;

                        // Store the new access token in local storage
                        this.localStorageService.set("accessToken", newAccessToken);

                        // Update the Authorization header
                        error.config.headers['Authorization'] = `Bearer ${newAccessToken}`;

                        // Retry the original request
                        return this.client.request(error.config);
                    } catch (refreshError) {
                    }
                }
            }

            return Promise.reject(error);

        });

        // Add success and error response interceptors
        this.client.interceptors.response.use(<TResponse>(response: AxiosResponse<TResponse>) => {
              let data = new ApiResponse(response.data as TResponse, null, response.status);

              if (this.mapResponse != null)
                  data = this.mapResponse(data);

              return {
                  ...response,
                  data: data
              };
          },
          (error: AxiosError) => {
              return {
                  ...error,
                  data: new ApiResponse(null, error.response?.data as ProblemDetails, error.response?.status ?? 500)
              };
          }
        );
    }

    public async getAsync<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return (await this.client.get<ApiResponse<T>>(url, config)).data;
    }

    public async postAsync<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return (await this.client.post<ApiResponse<T>>(url, data, config)).data;
    }

    public async putAsync<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return (await this.client.put<ApiResponse<T>>(url, data, config)).data;
    }

    public async deleteAsync<T>(url: string, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
        return (await this.client.delete<ApiResponse<T>>(url, config)).data;
    }
}