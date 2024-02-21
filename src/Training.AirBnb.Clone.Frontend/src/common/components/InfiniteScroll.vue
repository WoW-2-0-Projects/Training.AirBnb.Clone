<template>

    <!-- Scroll container -->
    <div ref="scrollContainer">
        <slot ref="contentItems" v-bind="$attrs"></slot>
    </div>

</template>

<script setup lang="ts">

import {
    defineProps,
    defineEmits,
    onMounted,
    onUnmounted,
    ref,
    type PropType,
    onUpdated, triggerRef, watch
} from 'vue';
import {DocumentService} from "@/infrastructure/services/DocumentService";
import {Action, NotificationSource} from "@/infrastructure/models/Action";

const documentService = new DocumentService();
const scrollContainer = ref<HTMLDivElement>();

const emit = defineEmits(['onScroll']);

const minimumScrollThresholdDistance = ref<number>(0);
const isLoading = ref<boolean>(false);

const props = defineProps({
    contentChangeSource: {
        type: NotificationSource,
        required: false
    },
    scrollThresholdDistance: {
        type: Number as PropType<number>,
        required: false,
        default: 0
    }
});

onMounted(() => {
    if (!scrollContainer.value) return;

    // Add listener for content load change
    props.contentChangeSource?.addListener(() => {
        calculateScrollThreshold();
    });

    window.addEventListener("scroll", onScroll);
    documentService.addWindowEventListener("resize", onWindowResized);
});

onUnmounted(() => {
    if (!scrollContainer.value) return;

    window.removeEventListener("scroll", onScroll);
    documentService.removeWindowEventListener("resize", onWindowResized);
});

const onWindowResized = () => {
    if (!scrollContainer.value) return;

    triggerRef(scrollContainer);
    calculateScrollThreshold();
}

const onScroll = () => {
    if (minimumScrollThresholdDistance.value === 0) return;

    if (documentService.isDocumentScrolledToBottom(minimumScrollThresholdDistance.value)) {
        emit("onScroll");
    }
};

const calculateScrollThreshold = () => {
    if(!scrollContainer.value) return;

    if (minimumScrollThresholdDistance.value == 0 && scrollContainer.value?.children.length > 0) {
        minimumScrollThresholdDistance.value = 1.5 * documentService.getHeight(scrollContainer.value.children[0] as
            HTMLElement);
    }
}

</script>