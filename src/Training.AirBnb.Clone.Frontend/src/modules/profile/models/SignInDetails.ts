import type {SignInByEmailDetails} from "@/modules/profile/models/SignInByEmailDetails";
import type {SignInByPhoneDetails} from "@/modules/profile/models/SignInByPhoneDetails";

/*
 * Represents sign in request
 */
export class SignInDetails {
    public signInByEmail?: SignInByEmailDetails;

    public signInByPhone?: SignInByPhoneDetails
}