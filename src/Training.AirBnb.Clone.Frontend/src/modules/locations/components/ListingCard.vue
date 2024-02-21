<template>

    <!--Listing card-->
    <div @click="onClick($event)" ref="listingCardContainer" class="flex flex-col items-start justify-start gap-2 rounded-lg theme-bg-primary theme-border hover-shadow-zero">

        <!--Listing header-->
        <div class="relative w-full overflow-hidden rounded-t-lg">

            <horizontal-carousel
                ref="imagesCarousel"
                :contentChangeSource="contentChangeSource"
                :loop-to-start="true">
                <img v-for="(image, index) in listing.imagesUrls" :key="index"
                     class="object-cover aspect-square"
                     loading="eager"
                     alt="Listing image"
                     @load="() => {contentChangeSource.updateListeners()}"
                     :src="image">
            </horizontal-carousel>

            <!--Like button-->
            <button class="absolute top-4 right-4">
                <svg class="stroke-bgColorPrimary" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32"
                     aria-hidden="true"
                     role="presentation" focusable="false"
                     style="display: block; fill: rgba(0, 0, 0, 0.5); height: 22px; width: 22px;  stroke-width: 2; overflow: visible;">
                    <path
                        d="M16 28c7-4.73 14-10 14-17a6.98 6.98 0 0 0-7-7c-1.8 0-3.58.68-4.95 2.05L16 8.1l-2.05-2.05a6.98 6.98 0 0 0-9.9 0A6.98 6.98 0 0 0 2 11c0 7 7 12.27 14 17z"></path>
                </svg>
            </button>
        </div>

        <!--Listing details-->
        <div class="flex justify-between w-full p-4 pt-1">

            <div class="relative flex flex-col w-full">
                <h5 class="font-medium theme-text-primary">{{ `${listing.name}, ${listing.address.city}` }}</h5>
                <p class="text-sm text-textSecondary theme-text-secondary">{{ `Built-in ${listing.builtDate}` }}</p>
                <p class="text-sm text-textSecondary theme-text-secondary">Feb 5-7</p>
                <h5 class="mt-3 theme-text-primary">
                    <strong class="font-medium">{{ `$${listing.pricePerNight.amount}` }}</strong> night
                </h5>
            </div>

            <!--Rating-->
            <div>
                <div class="flex items-center justify-center gap-1 theme-text-primary">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" aria-hidden="true" role="presentation"
                         focusable="false"
                         style="display: block; height: 12px; width: 12px; fill: currentcolor;">
                        <path fill-rule="evenodd"
                              d="m15.1 1.58-4.13 8.88-9.86 1.27a1 1 0 0 0-.54 1.74l7.3 6.57-1.97 9.85a1 1 0 0 0 1.48 1.06l8.62-5 8.63 5a1 1 0 0 0 1.48-1.06l-1.97-9.85 7.3-6.57a1 1 0 0 0-.55-1.73l-9.86-1.28-4.12-8.88a1 1 0 0 0-1.82 0z"></path>
                    </svg>
                    <p> {{ listing.rating.overallRating === 0 ? 'New' : listing.rating.overallRating.toFixed(2) }} </p>
                </div>
            </div>
        </div>
    </div>

</template>

<script setup lang="ts">

import {Listing} from '../models/Listing';
import HorizontalCarousel from '@/common/components/HorizontalCarousel.vue';
import {onMounted, ref} from 'vue';
import {NotificationSource} from '@/infrastructure/models/Action';
import {useRouter} from 'vue-router'
import {WindowService} from "@/infrastructure/services/WindowService";
import {UrlNavigationOptions} from "@/infrastructure/services/UrlNavigationOptions";

const router = useRouter();
const listingCardContainer = ref<HTMLDivElement>();
const imagesCarousel = ref<HTMLDivElement>();

const windowService = new WindowService();

const props = defineProps({
    listing: {
        type: Object as () => Listing,
        required: true
    }
});

const emit = defineEmits(['onMounted']);

const contentChangeSource = ref<NotificationSource>(new NotificationSource());

onMounted(() => {
    emit('onMounted', contentChangeSource);
});


const onClick = (event:any) => {
  //router.push({name: 'listingDetails', params: {'listingId': props.listing?.id}})

  const carousel = imagesCarousel.value as HTMLDivElement;

  console.log(event.target);
  console.log('carousel', carousel);
 // console.log(carousel.contains(event.target));

  const routeData = router.resolve({name: 'listingDetails', params:{'listingId': props.listing?.id}});
  windowService.navigateToUrl(routeData.href, UrlNavigationOptions.Blank);
}

</script>