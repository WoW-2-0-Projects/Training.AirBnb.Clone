<template>

    <Transition name="slide-fade">

        <div tabindex="0" ref="container" @focusout="onClose"
             class="absolute top-[70px] h-auto w-[220px] z-auto rounded-xl theme-bg-primary -mr-[40px] theme-shadow drop-shadow-lg flex flex-col justify-between items-start theme-border theme-shadow over">

            <!-- Menu actions -->
            <div class="w-full" v-for="(actions, index) in menuActions.actions" :key="index">
                <MenuButton v-for="(menuItem, index) in actions" :key="index" :menu-item="menuItem"/>
                <div class="h-[1px] flex top-[150px] w-full theme-border"/>
            </div>

            <div class="h-[50px] w-full flex items-center max-w-full max-h-full rounded-b-xl theme-hover-button">
                <span class="mr-[75px] text-left theme-text-primary font-sans font-light pl-3">Dark mode</span>
                <dark-mode-toggler v-model="toggleState"/>
            </div>
        </div>
    </Transition>

</template>

<script setup lang="ts">

import {nextTick, onMounted, ref, defineProps, defineEmits} from "vue";
import {AppThemeService} from "@/infrastructure/services/AppThemeService";
import DarkModeToggler from "@/modules/profile/components/DarkModeToggler.vue";
import MenuButton from "@/common/components/MenuButton.vue";
import type {MenuActions} from "@/modules/profile/models/MenuActions";

const appThemeService = new AppThemeService();

const props = defineProps({
    menuActions: {
        type: Object as () => MenuActions,
        required: true
    },
});

const emit = defineEmits(['update:value', 'onClose']);

const toggleState = ref(appThemeService.isDarkMode() ? true : false!);

const container = ref<HTMLElement>();

onMounted(async () => {
    await nextTick(() => {
        container.value?.focus();
    });
});

const onClose = () => {
    if (!container.value) return;

    const innerElementFocused = container.value!.contains(document.activeElement);
    if (!innerElementFocused)
        emit('onClose');
}

</script>

<style scoped>

.slide-fade-enter-active,
.slide-fade-leave-active {
    transition: opacity 0.1s ease-out;
    opacity: 1;
}

.slide-fade-enter-from,
.slide-fade-leave-to {
    opacity: 0;
}

</style>
