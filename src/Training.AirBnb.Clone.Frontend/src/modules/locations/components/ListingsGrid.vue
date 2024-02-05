<template>

    <div class="grid w-full grid-cols-1 px-20 theme-bg-primary sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-x-5 gap-y-10">

        <ListingCard v-for="listing in listings" :listing="listing"/>

    </div>

</template>

<script setup lang="ts">

import ListingCard from './ListingCard.vue';
import { Listing } from '../models/Listing';

const props = defineProps({
    listings: {
        type: Array<Listing>,
        required: true
    }
})

</script>