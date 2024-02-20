import type {SignInByPhoneDetails} from "@/modules/profile/models/SignInByPhoneDetails";
import type {SignInByEmailDetails} from "@/modules/profile/models/SignInByEmailDetails";
import {AirBnbApiClient} from "@/infrastructure/apiClients/airBnbApiClient/brokers/AirBnbApiClient";
import {LocalStorageService} from "@/infrastructure/services/storage/LocalStorageService";
import type {SignInDetails} from "@/modules/profile/models/SignInDetails";
import {useAccountStore} from "@/infrastructure/stores/AccountStore";
import {Account} from "@/modules/profile/models/Account";
import {RoleType} from "@/modules/profile/models/RoleType";

export class AuthenticationService {
    private airBnbApiClient: AirBnbApiClient;
    private localStorageService: LocalStorageService;
    private accountStore = useAccountStore();

    constructor() {
        this.airBnbApiClient = new AirBnbApiClient();
        this.localStorageService = new LocalStorageService();
    }

    public hasAccessToken() {
        return this.localStorageService.get('accessToken') !== null;
    }

    public isLoggedIn() {
        return this.accountStore.account.isLoggedIn();
    }

    public async signInAsync(signInDetails: SignInDetails) {
        const signInResponse = await this.airBnbApiClient.auth.signInAsync(signInDetails);

        if(!signInResponse.isSuccess)
            return false;

        // Store security tokens
        this.localStorageService.set('accessToken', signInResponse.response?.accessToken);
        this.localStorageService.set('refreshToken', signInResponse.response?.refreshToken);

        await this.setCurrentUser();

        return true;
    }

    public async signOutAsync() {
        const refreshToken = this.localStorageService.get('refreshToken');

        const signOutResponse = await this.airBnbApiClient.auth.signOutAsync(refreshToken);

        if(signOutResponse.isSuccess)
        {
            this.localStorageService.remove('accessToken');
            this.localStorageService.remove('refreshToken');
            this.accountStore.remove();
        }
    }

    public async setCurrentUser() {
        if(this.isLoggedIn() && !this.hasAccessToken()) return;

        // Set user account
        const currentUser = await this.airBnbApiClient.auth.getCurrentUser();
        if(currentUser.isSuccess)
        {
            const account = new Account();
            account.user  = currentUser.response!;
            this.accountStore.set(account);
        }

        // Check if user has host role
        const userRoles = await this.airBnbApiClient.auth.getCurrentUserRoles();
        if(userRoles.isSuccess)
        {
            const account = this.accountStore.account;
            account.hasHostRole = userRoles.response?.includes(RoleType.Host);
            this.accountStore.set(account);
        }
    }
}