import type { ListingCategory } from "@/modules/locations/models/ListingCategory";
import type ApiClientBase from "../../apiClientBase/services/ApiClientBase";

export class ListingCategoryEndpointsClient {
    private client: ApiClientBase;

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async getAsync() {
        return await this.client.getAsync<Array<ListingCategory>>("api/categories");
    }
}