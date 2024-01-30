<template>
  <div tabindex="-1" ref="container" @focusout="onClose" class="absolute top-[65px] h-auto w-[220px] z-auto rounded-xl theme-bg-primary -mr-[20px] theme-shadow drop-shadow-lg flex flex-col justify-between items-start theme-shadow border-b border-2">
    <button class="theme-text-primary font-sans text-left w-full p-3 theme-hover-shadow">
      <strong> <a href="http://localhost:5173/">Sign up</a></strong>
    </button>

    <button class="text-left theme-text-primary font-sans font-light pb-4 ml-3 mt-2 w-full theme-hover-shadow">
      <a href="http://localhost:5173/">Log in</a>
    </button>

    <div class="h-[1px] flex top-[150px] w-full border-b"/>

    <button class="text-left theme-text-primary font-sans font-light p-3 w-full theme-hover-shadow">
      <a href="http://localhost:5173/">Gift cards</a>
    </button>

    <button class="text-left theme-text-primary font-sans font-light p-3 w-full theme-hover-shadow">
      <a href="http://localhost:5173/">Airbnb your home</a>
    </button>

    <button class="text-left theme-text-primary font-sans font-light p-3 w-full theme-hover-shadow">
      <a href="http://localhost:5173/">Help Center</a>
    </button>

    <div class="h-[1px] flex top-[150px] w-full border-b"/>

    <div class="h-10 w-full flex items-center max-w-full max-h-full mb-2 mt-1">
      <span class="mr-[75px] text-left theme-text-primary font-sans font-light pl-3">Dark mode</span>
      <darkmode v-model="toggleState"/>
    </div>
  </div>
</template>
<script setup lang="ts">
import Darkmode from "@/Modules/profile/components/Darkmode.vue";
import {nextTick, onMounted, ref} from "vue";
import {AppThemeService} from "@/infrastructure/services/AppThemeService";

const appThemeService = new AppThemeService();

const toggleState = ref(appThemeService.isDarkMode() ? true : false!);

const props = defineProps({
  value:{
    type: Boolean
  }
})
const emit = defineEmits(['update:value', 'onClose']);

//profile menu - visible or invisible
const container =ref<HTMLElement>();

onMounted(async () => {
  await nextTick(() => {
    container.value?.focus();
  });
});


const onClose = () => {
  setTimeout(() => {
    const innerElementFocused = container.value!.contains(document.activeElement!);
    if (!innerElementFocused)
      emit('onClose');
  }, 10);
}
</script>