<template>

    <div tabindex="0" ref="container" @focusout="onClose"
         class="absolute top-[70px] h-auto w-[220px] z-auto rounded-xl theme-bg-primary -mr-[40px] theme-shadow drop-shadow-lg flex flex-col justify-between items-start theme-border theme-shadow over">

        <!-- TODO : Add authentication check -->

        <button
            class="text-left theme-text-primary theme-hover-button font-sans font-light pb-4 pl-3 pt-3 w-full theme-hover-shadow">
            <a href="http://localhost:5173/">Log in</a>
        </button>

        <button
            class="theme-text-primary rounded-t-xl theme-hover-button font-sans text-left w-full p-3 theme-hover-shadow">
            <strong> <a href="http://localhost:5173/">Sign up</a></strong>
        </button>

        <div class="h-[1px] flex top-[150px] w-full theme-border"/>

        <button
            class="text-left theme-text-primary theme-hover-button font-sans font-light p-3 w-full theme-hover-shadow">
            <a href="http://localhost:5173/">Gift cards</a>
        </button>

        <button
            class="text-left theme-text-primary theme-hover-button font-sans font-light p-3 w-full theme-hover-shadow">
            <a href="http://localhost:5173/">Airbnb your home</a>
        </button>

        <button
            class="text-left theme-text-primary theme-hover-button font-sans font-light p-3 w-full theme-hover-shadow">
            <a href="http://localhost:5173/">Help Center</a>
        </button>

        <div class="h-[1px] flex top-[150px] w-full theme-border"/>

        <div class="h-[50px] w-full flex items-center max-w-full max-h-full rounded-b-xl theme-hover-button">
            <span class="mr-[75px] text-left theme-text-primary font-sans font-light pl-3">Dark mode</span>
            <dark-mode-toggler v-model="toggleState"/>
        </div>
    </div>
</template>
<script setup lang="ts">
import {nextTick, onMounted, ref} from "vue";
import {AppThemeService} from "@/infrastructure/services/AppThemeService";
import DarkModeToggler from "@/Modules/profile/components/DarkModeToggler.vue";

const appThemeService = new AppThemeService();

const toggleState = ref(appThemeService.isDarkMode() ? true : false!);

const emit = defineEmits(['update:value', 'onClose']);

//profile menu - visible or invisible
const container = ref<HTMLElement>();

onMounted(async () => {
    await nextTick(() => {
        container.value?.focus();
    });
});

const onClose = () => {
    const innerElementFocused = container.value!.contains(document.activeElement);
    if (!innerElementFocused)
        emit('onClose');
}
</script>