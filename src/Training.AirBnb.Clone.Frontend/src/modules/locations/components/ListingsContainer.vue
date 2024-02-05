<template>

    <div class="flex flex-col items-center justify-center theme-bg-primary">

        <!-- Listings category tab -->
        <listings-category-tab :listingCategories="listingCategories"
                               :selectedCategoryId="selectedCategoryId"
                               @onCategorySelected="onCategorySelected"/>
        
        <listings-grid class="mt-44" :listings="listings"/>
        
    </div>

</template>

<script setup lang="ts">

import ListingsCategoryTab from "@/modules/locations/components/ListingsCategoryTab.vue";
import ListingsGrid from "./ListingsGrid.vue";
import { AirBnbApiClient } from "@/infrastructure/apiClients/airBnbApiClient/brokers/AirBnbApiClient";
import { ListingCategory } from "@/modules/locations/models/ListingCategory";
import { ref, onBeforeMount } from "vue";
import { FilterPagination } from "../models/FilterPagination";
import type { Listing } from "../models/Listing";

const airBnbApiClient = new AirBnbApiClient();

const selectedCategoryId = ref<string>("");
const listingCategories = ref<Array<ListingCategory>>([]);
const listings = ref<Array<Listing>>([]);

onBeforeMount(async () => {
    await loadListingCategoriesAsync();
    await loadListingsAsync();
});

const onCategorySelected = async (categoryId: string) => {
    selectedCategoryId.value = categoryId;
    await loadListingsAsync();
};
    
const loadListingCategoriesAsync = async () => {
    const response = await airBnbApiClient.listingCategories.getAsync();
    
    if (response.response) {
        listingCategories.value = response.response;
        
        if (response.response.length > 0) selectedCategoryId.value = response.response[0].id;
    }
};

const loadListingsAsync = async () => {
    const response = await airBnbApiClient.listings.getAsync(selectedCategoryId.value, new FilterPagination(30, 1));
    
    if (response.response) {
        listings.value = response.response;
    }
}
    
</script>