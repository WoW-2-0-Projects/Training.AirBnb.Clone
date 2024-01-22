<template>

<div class="h-screen w-screen flex flex-col theme-bg-primary">

  <!-- Header-->
  <main-header/>
  
  <!--Listings-->
  <listings-grid class="mt-24"/>

</div>
=======
>>>>>>> Stashed changes

</template>

<script setup lang="ts">

import ListingsGrid from "@/Modules/locations/components/ListingsGrid.vue";
import MainHeader from "@/common/components/MainHeader.vue";
import { AppThemeService } from "@/infrastructure/services/AppThemeService";

const appThemeService = new AppThemeService();
</script>