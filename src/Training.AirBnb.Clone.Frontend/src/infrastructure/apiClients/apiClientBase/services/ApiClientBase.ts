import type { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";
import { ApiResponse } from "../models/ApiResponse";
import axios from "axios";
import type { ProblemDetails } from "../models/ProblemDetails";

export default class ApiClientBase {
    public readonly client: AxiosInstance;
    public mapResponse!: <T>(response: ApiResponse<T>) => ApiResponse<T>

    constructor(config: AxiosRequestConfig) {
        this.client = axios.create(config);

        // register interceptors
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