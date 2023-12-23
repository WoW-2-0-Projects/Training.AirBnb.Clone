<template>

  <!-- Header-->
  <main-header/>


  <button class="mt-80" @click="appThemeService.toggleDarkMode()">Dark mode</button>
</template>

<script setup lang="ts">

import MainHeader from "@/common/components/MainHeader.vue";
import { AppThemeService } from "@/infrastructure/services/AppThemeService";

// import LocationsContainer from "@/modules/locations/components/ListingsContainer.vue";

const appThemeService = new AppThemeService();

</script>