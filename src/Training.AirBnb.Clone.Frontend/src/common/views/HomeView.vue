<template>

<div class="flex flex-col w-screen h-screen theme-bg-primary">

  <!-- Header-->
  <main-header/>
  
  <!-- Listings Container -->
  <listings-container/>

</div>

</template>

<script setup lang="ts">

import MainHeader from "@/common/components/MainHeader.vue";
import ListingsContainer from "@/modules/locations/components/ListingsContainer.vue";

</script>