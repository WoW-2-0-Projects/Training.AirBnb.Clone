<template>

  <div class="flex flex-col w-screen h-screen theme-bg-primary">

    <!-- Header-->
    <main-header/>

    <div class="mt-20 content-padding">
      <!--Listing header card-->
      <ListingHeaderCard/>

      <!--Header Image Gallery-->
      <HeaderImageGallery class="mt-5"/>
    </div>

  </div>
</template>

<script setup lang="ts">

import MainHeader from "@/common/components/MainHeader.vue";
import ListingHeaderCard from "@/modules/locations/components/ListingHeaderCard.vue";
import HeaderImageGallery from "@/modules/locations/components/HeaderImageGallery.vue";
</script>
