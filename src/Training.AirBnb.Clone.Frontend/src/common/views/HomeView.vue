<template>

<div class="flex flex-col w-screen h-screen theme-bg-primary">

  <!-- Header-->
  <main-header @sign-in-up-request="$emit('signInUpRequest')"/>
  
  <!-- Listings Container -->
  <listings-container/>
  
  <!--Listings-->
  <listings-grid class="mt-24"/>
  
  
  <button class="mt-80" @click="appThemeService.toggleDarkMode()">Dark mode</button>
</div>

</template>

<script setup lang="ts">

import ListingsGrid from "@/Modules/locations/components/ListingsGrid.vue";
import MainHeader from "@/common/components/MainHeader.vue";
import { AppThemeService } from "@/infrastructure/services/AppThemeService";
import ListingsContainer from "@/modules/locations/components/ListingsContainer.vue";
import type { emit } from "process";

const appThemeService = new AppThemeService();

</script>