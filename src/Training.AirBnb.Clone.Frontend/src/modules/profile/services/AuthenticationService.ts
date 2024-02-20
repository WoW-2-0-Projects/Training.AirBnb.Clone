import type {SignInByPhoneDetails} from "@/modules/profile/models/SignInByPhoneDetails";
import type {SignInByEmailDetails} from "@/modules/profile/models/SignInByEmailDetails";
import {AirBnbApiClient} from "@/infrastructure/apiClients/airBnbApiClient/brokers/AirBnbApiClient";
import {LocalStorageService} from "@/infrastructure/services/storage/LocalStorageService";
import type {SignInDetails} from "@/modules/profile/models/SignInDetails";
import {useAccountStore} from "@/infrastructure/stores/AccountStore";
import {Account} from "@/modules/profile/models/Account";

export class AuthenticationService {
    private airBnbApiClient: AirBnbApiClient;
    private localStorageService: LocalStorageService;
    private accountStore = useAccountStore();

    constructor() {
        this.airBnbApiClient = new AirBnbApiClient();
        this.localStorageService = new LocalStorageService();
    }

    public async signInAsync(signInDetails: SignInDetails) {
        const signInResponse = await this.airBnbApiClient.auth.signInAsync(signInDetails);

        if(!signInResponse.isSuccess)
            return false;

        // Store security tokens
        this.localStorageService.set('accessToken', signInResponse.response?.accessToken);
        this.localStorageService.set('refreshToken', signInResponse.response?.refreshToken);

        // Set user account
        const currentUser = await this.airBnbApiClient.auth.getCurrentUser();
        if(currentUser.isSuccess)
        {
            const account = new Account();
            account.user  = currentUser.response!;
            this.accountStore.set(account);
        }

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
}