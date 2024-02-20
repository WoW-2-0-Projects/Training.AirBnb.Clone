import type ApiClientBase from "../../apiClientBase/services/ApiClientBase";
import type {SignInDetails} from "@/modules/profile/models/SignInDetails";
import type {IdentityToken} from "@/modules/profile/models/IdentityToken";
import type {User} from "@/modules/profile/models/User";

export class AuthEndpointsClient {
    public client: ApiClientBase

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async signInAsync(signInDetails: SignInDetails) {
        const endpointUrl =  'api/auth/sign-in';
        return await this.client.postAsync<IdentityToken>(endpointUrl, signInDetails);
    }

    public async getCurrentUser() {
        const endpointUrl =  'api/auth/me';
        return await this.client.getAsync<User>(endpointUrl);
    }
}