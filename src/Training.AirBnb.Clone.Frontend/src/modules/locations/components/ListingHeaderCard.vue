<template>
      <div class="flex w-full gap-4 theme-bg-primary">

        <div class="mt-5 w-full flex flex-grow justify-between">

            <p class="text-2xl theme-text-primary" >{{listing.name}} </p>

          <div class="flex">

              <div class="rounded hover:bg-gray-200">

                <button class="flex flex-grow text-sm font-medium theme-text-primary p-2 underline underline-offset-1 ">
                    <span>
                      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" aria-hidden="true" role="presentation" focusable="false" style="display: block; fill: none; height: 20px; width: 16px; stroke: currentcolor; stroke-width: 2; overflow: visible;"><g fill="none"><path d="M27 18v9a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2v-9M16 3v23V3zM6 13l9.3-9.3a1 1 0 0 1 1.4 0L26 13"></path></g></svg>
                    </span>
                      <p class="ml-1">Shere</p>
                </button>
              </div>

                <div class="rounded hover:bg-gray-200">

                  <button class="flex flex-grow text-sm font-medium theme-text-primary p-2 underline underline-offset-1">
                      <span class="">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" aria-hidden="true" role="presentation" focusable="false" style="display: block; fill: none; height: 20px; width: 16px; stroke: currentcolor; stroke-width: 2; overflow: visible;"><path d="M16 28c7-4.73 14-10 14-17a6.98 6.98 0 0 0-7-7c-1.8 0-3.58.68-4.95 2.05L16 8.1l-2.05-2.05a6.98 6.98 0 0 0-9.9 0A6.98 6.98 0 0 0 2 11c0 7 7 12.27 14 17z"></path></svg>
                      </span>
                       <p class="ml-1">Save</p>
                  </button>
              </div>

          </div>
        </div>

      </div>

</template>

<script setup lang="ts">

import {defineProps} from "vue";
import {Listing} from "@/modules/locations/models/Listing";

const props = defineProps({
    listing: {
        type: Object as () => Listing,
        required: true
    }
});
</script>
