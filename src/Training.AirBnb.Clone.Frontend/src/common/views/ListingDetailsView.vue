<template>

  <div class="flex flex-col w-screen h-screen theme-bg-primary">

    <!-- Header-->
    <main-header/>

    <div class="mt-20 content-padding">
      <!--Listing header card-->
      <listing-header-card :listing ="listing"/>

      <!--Header Image Gallery-->
      <header-image-gallery class="mt-5" :listing="listing"/>
    </div>

  </div>
</template>

<script setup lang="ts">


import MainHeader from "@/common/components/MainHeader.vue";
import ListingHeaderCard from "@/modules/locations/components/ListingHeaderCard.vue";
import HeaderImageGallery from "@/modules/locations/components/HeaderImageGallery.vue";
import {computed, onBeforeMount, ref} from "vue";

import { useRouter } from 'vue-router';
import {AirBnbApiClient} from "@/infrastructure/apiClients/airBnbApiClient/brokers/AirBnbApiClient";
import type {Listing} from "@/modules/locations/models/Listing";

    const router = useRouter()
    const listingId = router.currentRoute.value.params.listingId.toString();



const airBnBApiClient = new AirBnbApiClient();
const listing = ref<Listing>();

onBeforeMount(async () => {
    await loadListingsAsync();
});
const loadListingsAsync = async () => {

    const response = await airBnBApiClient.listings.getByIdAsync(listingId)

    if(response.response){
        listing.value = response.response
    }
}

</script>
