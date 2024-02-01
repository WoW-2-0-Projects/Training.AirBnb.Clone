<template>
  <div :class="modeValue ? 'bg-logoPrimary' : 'bg-gray-300'" @click="toggle" class="group w-10 h-6 flex primary-transition-toggle theme-bg-accent rounded-full p-1 theme-text-primary">
    <div  :class="modeValue ? 'translate-x-4' : 'translate-x-0'" class="w-4 h-4 primary-transition  theme-bg-toggle rounded-full theme-shadow"></div>
  </div>

</template>
<script setup lang="ts">
import {defineProps, ref} from "vue";
import {AppThemeService} from "@/infrastructure/services/AppThemeService";

const appThemeService = new AppThemeService();
const modeValue = ref<boolean>(appThemeService.isDarkMode())
function toggle(){
  modeValue.value = !modeValue.value;
  appThemeService.toggleDarkMode();
}
</script>