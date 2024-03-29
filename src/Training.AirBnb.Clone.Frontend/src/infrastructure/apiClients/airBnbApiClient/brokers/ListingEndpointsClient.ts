import type { FilterPagination } from "@/modules/locations/models/FilterPagination";
import type ApiClientBase from "../../apiClientBase/services/ApiClientBase";
import type {Listing} from "@/modules/locations/models/Listing";

export class ListingEndpointsClient {
    public client: ApiClientBase

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async getAsync(categoryId: string, filterPagination: FilterPagination) {
        const endpointUrl = `api/listings/category/${categoryId}?pageToken=${filterPagination.pageToken}&pageSize=${filterPagination.pageSize}`;

        return await this.client.getAsync<Array<Listing>>(endpointUrl);
    }
}