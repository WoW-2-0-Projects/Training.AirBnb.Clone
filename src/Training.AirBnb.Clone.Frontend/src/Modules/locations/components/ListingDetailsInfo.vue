<template>
  <div class="w-[750px]">
    <!-- Translater Info-->
    <div class="flex items-center justify-start bg-gray-200 rounded-xl mt-10 px-3 py-5">
      <h1>
        Some info is shown in its original language.
      </h1>
      <a href="https://google.com" target="_blank">
        <h1 class="font-medium underline ml-2">
          Show original
        </h1>
      </a>
    </div>
    <!--Full Info-->
    <div class="mt-10">
      <p class="truncate text-wrap">
        A cosy nature inspired cabin, on the way and close to the Northcape.
        Take it easy at this unique and relaxing getaway.
        Currently, under renovation will be done by 01.06. and there are more pictures coming as soon as the bedrooms and bathroom are going to be done and remodelled. We are also adding jacuzzi and sauna.
        Surroundings are astonishing, and feeling in the cabin is so calming.
        The idea came together with local artist who have inspired the owners and have participated in renovation.
      </p>
        <button @click="togalModal" class="flex items-center justify-start font-medium underline mt-3">
          <span  class="text-lg"> Show more </span>
          <span class="mt-1 ml-2">
            <svg viewBox="0 0 18 18" role="presentation" aria-hidden="true" focusable="false" style="height: 12px; width: 12px; display: block;">
              <path
                  d="m4.29 1.71a1 1 0 1 1 1.42-1.41l8 8a1 1 0 0 1 0 1.41l-8 8a1 1 0 1 1 -1.42-1.41l7.29-7.29z" fill-rule="evenodd">
              </path>
            </svg>
          </span>
        </button>

      <listing-full-info :modalActive="modalActive" @close-modal="togalModal"/>
    </div>
  </div>
</template>
<script setup lang="ts">
  import ListingFullInfo from "@/Modules/locations/components/ListingFullInfo.vue";
  import {ref} from "vue";
  import {DocumentService} from "@/infrastructure/services/DocumentService";
  const modalActive = ref(false);
  const documentService = new DocumentService();
  const togalModal = () => {
    modalActive.value = !modalActive.value
    documentService.handleBodyOverflow(modalActive.value)
  }
</script>