<template> 

<div class="relative flex items-center overflow-x-scroll no-scrollbar">
    <div v-if="canScrollPrev" class="absolute left-0 z-10 hidden h-full mb-2 theme-bg-primary sm:inline-block">
        <!-- Previous button -->
        <previous-button class="mx-1 my-4 theme-bg-primary hover-shadow-zero" @click="onScrollPrev"/>
    </div>

    
    <div ref="scrollContainer" class="flex gap-10 overflow-x-scroll md:gap-12 no-scrollbar">
       <slot v-bind="$attrs"></slot>
   </div>
   
   <div v-show="canScrollNext" ref="nextButton" class="absolute right-0 z-10 hidden h-full mb-2 theme-bg-primary sm:inline-block">
        <!-- Next button -->
        <next-button class="mx-1 my-4 theme-bg-primary hover-shadow-zero primary-transition" @click="onScrollNext"/>
    </div>
</div>

</template>

<script setup lang="ts">

import NextButton from './NextButton.vue';
import PreviousButton from './PreviousButton.vue';
import { ref, type PropType, watch, onMounted, onUnmounted, nextTick } from 'vue';
import { DocumentService } from "@/infrastructure/services/DocumentService";

const scrollContainer = ref<HTMLDivElement>();
const documentService = new DocumentService();

const props = defineProps({
   scrollChangeSource: {
        type: Array as PropType<Array<any>>,
        required: true
   }
});

const scrollPosition = ref<number>(0);
const canScrollPrev = ref<boolean>(true);
const canScrollNext = ref<boolean>(true);
const isMounted = ref<boolean>(false);
const firstComputed = ref<boolean>(false);

watch(() => [props.scrollChangeSource], () => {
    if (firstComputed.value) return;
    
    nextTick(() => {
        computeCanScroll();
        firstComputed.value = true; 
    });
});

const computeCanScroll = () => {
  canScrollPrev.value = documentService.canScrollLeft(scrollContainer.value!, 20);
  canScrollNext.value = documentService.canScrollRight(scrollContainer.value!, 20);
};

onMounted(() => {
   if (!scrollContainer.value) return; 

   documentService.addEventListener(scrollContainer.value, "scroll", onScroll);

   isMounted.value = true;
});


onUnmounted(() => {
   if (!scrollContainer.value) return;
   
   documentService.removeEventListener(scrollContainer.value, "scroll", onScroll);
});

const onScrollPrev = () => {
  if (!scrollContainer.value) return;  

  documentService.scrollLeft(scrollContainer.value);
};

const onScrollNext = () => {
    if (!scrollContainer.value) return;

    documentService.scrollRight(scrollContainer.value);
}

const onScroll = (target: HTMLElement) => {
    scrollPosition.value = target.scrollLeft;
    computeCanScroll();
}

</script>