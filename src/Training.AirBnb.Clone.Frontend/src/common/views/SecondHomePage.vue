<template>
  <div class="flex flex-col w-screen h-screen theme-bg-primary">
  <main-header/>
  <listing-details-card/>
  </div>
</template>

<script>

import {defineComponent} from "vue";
import MainHeader from "@/common/components/MainHeader.vue";
import ListingDetailsCard from "@/Modules/locations/components/ListingDetailsCard.vue";

export default defineComponent({
  components: {ListingDetailsCard, MainHeader}
})
</script>