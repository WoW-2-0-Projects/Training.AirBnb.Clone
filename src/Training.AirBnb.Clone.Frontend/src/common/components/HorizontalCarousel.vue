<template>

<div class="relative" @mouseover="() => hovering = true" @mouseleave="() => hovering = false">

    <!-- Carousel content -->
    <div ref="carouselContainer" class="flex overflow-x-scroll no-scrollbar">
        <slot v-bind="$attrs"></slot>
    </div>

   <!-- Navigation actions -->
   <div class="flex items-center justify-between w-full px-2 absolute-center">
        <previous-button :class="{'collapse': !hovering || !canMovePrev}" @click="onMovePrev"/>
        <next-button ref="nextButton" :class="{'collapse': !hovering || !canMoveNext}" @click="onMoveNext"/>
    </div>
    
</div>

</template>

<script setup lang="ts">

import NextButton from './NextButton.vue';
import PreviousButton from './PreviousButton.vue';
import { DocumentService } from '@/infrastructure/services/DocumentService';
import { ref, onMounted, onUnmounted, computed, triggerRef } from 'vue';
import { Action } from '@/infrastructure/models/Action';

const props = defineProps({
    onSourceChanged: {
        type: Object as () => Action,
        required: false
    },
    loopToStart: {
        type: Boolean,
        required: false,
        default: false
    }
});

const documentService = new DocumentService();
const carouselContainer = ref<HTMLDivElement>();
const nextButton = ref(null);

const visibleImageIndex = ref(0);
const imagesCount = computed(() => carouselContainer.value?.children.length! - 1);
const moveDistance = computed(() => documentService.getChildWidth(carouselContainer.value!));

const canMovePrev = computed(() => props.loopToStart || visibleImageIndex.value > 0);
const canMoveNext = computed(() => props.loopToStart || visibleImageIndex.value < imagesCount.value!);
const hovering = ref<boolean>(false);

onMounted(() => {
    window.addEventListener("resize", onWindowResized);
    
    props.onSourceChanged!.callBack = () => {
        triggerRef(visibleImageIndex);
    };
});

onUnmounted(() => {
    window.removeEventListener("resize", onWindowResized);
});

/*
 * Manually triggers the recalculation of distance to scroll when window is resized
 */
const onWindowResized = () => {
    if (!carouselContainer.value) return;

    triggerRef(carouselContainer);
}

/*
 * Moves carousel to previous item
 */
 const onMovePrev = () => {
    if (!carouselContainer.value) return;

    if (visibleImageIndex.value === 0 && props.loopToStart) {
        documentService.scrollToEnd(carouselContainer.value);
        visibleImageIndex.value = imagesCount.value;
    } else {
        visibleImageIndex.value--;
        documentService.scrollTo(carouselContainer.value, moveDistance.value! * visibleImageIndex.value);
    }
};

/*
 * Moves carousel to next item
 */
 const onMoveNext = async () => {
    if (!carouselContainer.value) return;

    if (visibleImageIndex.value === imagesCount.value! && props.loopToStart) {
        documentService.scrollToBeginning(carouselContainer.value);
        visibleImageIndex.value = 0;
    } else {
        visibleImageIndex.value++;
        documentService.scrollTo(carouselContainer.value, moveDistance.value! * visibleImageIndex.value);
    }
}

</script>