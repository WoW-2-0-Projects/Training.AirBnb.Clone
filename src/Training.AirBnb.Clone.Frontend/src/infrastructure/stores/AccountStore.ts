import { defineStore } from 'pinia'
import {Account} from "@/modules/profile/models/Account";

export const useAccountStore = defineStore('account', {
    state: () => ({
        account: new Account()
    }),
    actions: {
        set(newAccount: Account) {
            this.account = newAccount;
        },
        remove() {
            this.isLoggedIn = false;
        }
    },
    getters: {
        doubleCount: (state) => state.count * 2
    }
})