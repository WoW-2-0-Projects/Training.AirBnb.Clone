import type { FilterPagination } from "@/modules/locations/models/FilterPagination";
import type ApiClientBase from "../../apiClientBase/services/ApiClientBase";

export class AccountsEndpointsClient {
    public client: ApiClientBase

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async checkByEmailAsync(emailAddress: string) {
        const endpointUrl =  `api/Accounts/by-email/${emailAddress}`;

        return await this.client.getAsync<string>(endpointUrl);
    }

    public async checkByPhoneNumberAsync(phoneNumber: string) {
        const endpointUrl =  `api/Accounts/by-phone/${phoneNumber}`;

        return await this.client.getAsync<string>(endpointUrl);
    }
}