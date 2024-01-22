<template>
  <div :class="modelValue ? 'theme-bg-primary-toggle' : 'bg-gray-300'" @click="toggle" class="w-10 h-6 flex primary-transition-toggle theme-bg-accent rounded-full p-1 theme-text-primary">
    <div  :class="modelValue ? 'translate-x-4' : 'translate-x-0'" class="w-4 h-4 primary-transition-toggle theme-bg-primary-toggle rounded-full theme-shadow"></div>
  </div>

</template>
<script setup lang="ts">
import {defineProps} from "vue";
import {AppThemeService} from "@/infrastructure/services/AppThemeService";

const appThemeService = new AppThemeService();
const props = defineProps({
  modelValue:{
    type:Boolean
  }
})
const emit = defineEmits(['update:modelValue'])
function toggle(){
  emit('update:modelValue', !props.modelValue)
    appThemeService.toggleDarkMode();
  // else
  //   appThemeService.toggleDarkMode();
}
</script>