import ApiClientBase from "../../apiClientBase/services/ApiClientBase";
import { ListingCategoryEndpointsClient } from "./ListingCategoryEndpointsClient";

export class AirBnbApiClient {
    private readonly client: ApiClientBase;
    public readonly baseUrl: string;

    constructor() {
        this.baseUrl = "https://localhost:7104";

        this.client = new ApiClientBase({
            baseURL: this.baseUrl,
            withCredentials: true
        })

        this.listingCategories = new ListingCategoryEndpointsClient(this.client);
    }

    public readonly listingCategories: ListingCategoryEndpointsClient;
}