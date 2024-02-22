<template>

    <button class="text-sm font-medium theme-text-primary line-clamp-1 px-4 py-3 theme-hover-button rounded-full">
        <span class="line-clamp-1">{{
                accountStore.account.isLoggedIn() && accountStore.account.hasHostRole ?
                    LayoutConstants.SwitchToHostAccount :
                    LayoutConstants.AirBnbYourHome
            }}</span>
    </button>

</template>

<script setup lang="ts">

import { useAccountStore } from "@/infrastructure/stores/AccountStore";
import { LayoutConstants } from "@/infrastructure/constants/LayoutConstants";

const accountStore = useAccountStore();

</script>