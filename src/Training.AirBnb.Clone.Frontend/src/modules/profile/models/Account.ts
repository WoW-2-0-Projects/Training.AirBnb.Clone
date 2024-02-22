import type {User} from "@/modules/profile/models/User";

export class Account {
    public user!: User;
    public hasHostRole: boolean;

    public isLoggedIn = () => this.user !== null && this.user !== undefined;
}