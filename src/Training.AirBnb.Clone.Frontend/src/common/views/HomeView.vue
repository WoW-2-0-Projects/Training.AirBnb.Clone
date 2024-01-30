<template>

<div class="flex flex-col w-screen h-screen theme-bg-primary">

  <!-- Header-->
  <main-header/>
  
  <!-- Listings Container -->
  <listings-container/>
  
  <!--Listings-->
  <listings-grid/>

</div>

</template>

<script setup lang="ts">

import ListingsGrid from "@/Modules/locations/components/ListingsGrid.vue";
import MainHeader from "@/common/components/MainHeader.vue";
import { AppThemeService } from "@/infrastructure/services/AppThemeService";
import ListingsContainer from "@/modules/locations/components/ListingsContainer.vue";

const appThemeService = new AppThemeService();

</script>