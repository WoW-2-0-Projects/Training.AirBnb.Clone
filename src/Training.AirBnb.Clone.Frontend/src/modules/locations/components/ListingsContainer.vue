<template>

    <!-- listings container -->
    <div class="flex flex-col items-center justify-center theme-bg-primary">

        <!-- Listings category tab -->
        <listings-category-tab :listingCategories="listingCategories"
                               :selectedCategoryId="selectedCategoryId"
                               @onCategorySelected="onCategorySelected"/>

        <!-- Listing grid infinite scroll -->
        <infinite-scroll @onScroll="onScroll"
                         :contentChangeSource="listingChangeSource"
                         class="grid w-full px-20 gap-x-5 gap-y-10 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 theme-bg-primary">

            <!-- Listing card -->
            <listing-card v-for="(listing, index) in listings" :key="listing.id" :listing="listing"
                          @onMounted="(changeSource: Ref<NotificationSource>) =>
                          {
                              if(index == listings.length - 1)
                                changeSource.value.addListener(() => listingChangeSource.updateListeners());
                          }"/>

        </infinite-scroll>
    </div>

</template>

<script setup lang="ts">

import ListingsCategoryTab from "@/modules/locations/components/ListingsCategoryTab.vue";
import {AirBnbApiClient} from "@/infrastructure/apiClients/airBnbApiClient/brokers/AirBnbApiClient";
import {ListingCategory} from "@/modules/locations/models/ListingCategory";
import {ref, onBeforeMount, onUpdated, watch, type Ref} from "vue";
import {FilterPagination} from "../models/FilterPagination";
import type {Listing} from "../models/Listing";
import ListingCard from './ListingCard.vue';
import InfiniteScroll from "@/common/components/InfiniteScroll.vue";
import {NotificationSource} from "@/infrastructure/models/Action";

const airBnbApiClient = new AirBnbApiClient();
const selectedCategoryId = ref<string>("");
const listingCategories = ref<Array<ListingCategory>>([]);
const listings = ref<Array<Listing>>([]);

const listingsPagination = ref<FilterPagination>({
    pageToken: 1,
    pageSize: 30
});

// Listing loading
const isListingsLoading = ref<boolean>(false);
const noListingsForCategory = ref<boolean>(false);
const loadNextListings = ref<boolean>(false);
const listingChangeSource = ref<NotificationSource>(new NotificationSource());

onBeforeMount(async () => {
    await loadListingCategoriesAsync();
    await loadListingsAsync();
});

const onCategorySelected = async (categoryId: string) => {
    selectedCategoryId.value = categoryId;
    listings.value = [];
    noListingsForCategory.value = false;
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
    if (selectedCategoryId.value === "" || noListingsForCategory.value) return;

    isListingsLoading.value = true;
    const response = await airBnbApiClient.listings.getAsync(selectedCategoryId.value, listingsPagination.value);

    if (response.response) {
        listings.value.push(...response.response)
    } else if (response.status == 404 || response.status == 204) {
        noListingsForCategory.value = true;
    }

    // Enable loading listings after 1 second
    setTimeout(() => isListingsLoading.value = false, 1000);
}

const onScroll = async () => {
    loadNextListings.value = true;
};

watch(() => [isListingsLoading.value, loadNextListings.value], async () => {
    if (isListingsLoading.value) return;

    if (!isListingsLoading.value && loadNextListings.value) {
        listingsPagination.value.pageToken++;
        await loadListingsAsync();
        loadNextListings.value = false;
    }
});

</script>