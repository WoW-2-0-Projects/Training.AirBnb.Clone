<template>

  <div class="flex flex-col w-screen h-screen theme-bg-primary">

    <!-- Header-->
    <main-header/>
  </div>

  <div class="w-full h-full flex flex-col theme-bg-primary">

    <!--Listing header card-->
    <ListingHeaderCard/>
  </div>

  <div class="w-full h-full flex flex-col theme-bg-primary">

    <!--Header Image Gallery-->
    <HeaderImageGallery/>
  </div>


</template>

<script setup lang="ts">

import MainHeader from "@/common/components/MainHeader.vue";
import ListingHeaderCard from "@/modules/locations/components/ListingHeaderCard.vue";
import HeaderImageGallery from "@/modules/locations/components/HeaderImageGallery.vue";
</script>
