import ApiClientBase from "../../apiClientBase/services/ApiClientBase";
import { ListingCategoryEndpointsClient } from "./ListingCategoryEndpointsClient";
import { ListingEndpointsClient } from "./ListingEndpointsClient";

export class AirBnbApiClient {
    private readonly client: ApiClientBase;
    public readonly baseUrl: string;

    constructor() {
        // this.configurationService = new ConfigurationService();
        // const test = this.configurationService.configuration().then((result) => {
        //     console.log(result);
        // })

        this.baseUrl = "https://localhost:7266";

        this.client = new ApiClientBase({
            baseURL: this.baseUrl,
            withCredentials: true
        });


        this.listingCategories = new ListingCategoryEndpointsClient(this.client);
        this.listings = new ListingEndpointsClient(this.client);
    }

    public readonly listingCategories: ListingCategoryEndpointsClient;
    public readonly listings: ListingEndpointsClient;
}