<template>

    <button
        @click="onclick"
        class="theme-text-primary text-sm theme-hover-button font-sans text-left w-full px-5 py-3 theme-hover-shadow">
        <strong v-if="menuItem.special">{{menuItem.title}}</strong>
        <span v-if="!menuItem.special">{{menuItem.title}}</span>
    </button>

</template>

<script setup lang="ts">

import {defineProps, onUnmounted} from "vue";
import type {MenuAction} from "@/modules/profile/models/MenuAction";
import router from "@/infrastructure/router";

const props = defineProps({
    menuItem: {
        type: Object as () => MenuAction,
        required: true
    }
});

const onclick = () => {
    if (props.menuItem.callback)
        props.menuItem.callback();
    else if (props.menuItem.routeName)
        router.push({name: props.menuItem.routeName});
}

</script>