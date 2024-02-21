<template>

    <div class=" aspect-auto flex w-full gap-1.5 lg:gap-2 rounded-lg lg:rounded-xl overflow-hidden">

       <div class="w-1/2">

           <img class="h-full object-cover "
                :src="listing.imagesUrls[0]" alt="Listing main image">
       </div>

        <div class="w-1/2 relative grid gap-1.5 lg:gap-2 grid-cols-2">

            <img class="w-full aspect-square object-cover"
                 v-for="(image, index) in listing.imagesUrls.slice(1)" :key="index" :src="image" alt="Listing image">

           <button class="flex absolute bottom-6 right-6 gap-2 bg-bgColorPrimary text-black p-1   rounded hover:bg-gray-300">
                    <span class="mt-1">

                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" aria-hidden="true"
                             role="presentation"
                             focusable="false" style="display: block; height: 16px; width: 16px; fill: currentcolor;">
                            <path fill-rule="evenodd"
                                  d="M3 11.5a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3zm5 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3zm5 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3zm-10-5a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3zm5 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3zm5 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3zm-10-5a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3zm5 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3zm5 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3z"></path>
                        </svg>
                    </span>

                <span>
                    Show all photos
                </span>

            </button>
        </div>
    </div>

</template>

<script setup lang="ts">

import {Listing} from "@/modules/locations/models/Listing";
import { defineProps } from "vue";
const props = defineProps({
    listing: {
        type: Object as () => Listing,
        required: true
    }
});
</script>