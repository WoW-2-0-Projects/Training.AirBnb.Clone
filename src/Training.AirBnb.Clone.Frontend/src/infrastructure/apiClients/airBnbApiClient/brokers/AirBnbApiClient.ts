import ApiClientBase from "../../apiClientBase/services/ApiClientBase";
import { ListingCategoryEndpointsClient } from "./ListingCategoryEndpointsClient";
import { ListingEndpointsClient } from "./ListingEndpointsClient";
import {AuthEndpointsClient} from "@/infrastructure/apiClients/airBnbApiClient/brokers/AuthEndpointsClient";

export class AirBnbApiClient {
    private readonly client: ApiClientBase;
    public readonly baseUrl: string;

    constructor() {
        this.baseUrl = "https://localhost:7266";

        this.client = new ApiClientBase({
            baseURL: this.baseUrl,
            withCredentials: true
        })

        this.listingCategories = new ListingCategoryEndpointsClient(this.client);
        this.listings = new ListingEndpointsClient(this.client);
        this.auth = new AuthEndpointsClient(this.client);
    }

    public readonly listingCategories: ListingCategoryEndpointsClient;
    public readonly listings: ListingEndpointsClient;
    public readonly auth: AuthEndpointsClient;
}