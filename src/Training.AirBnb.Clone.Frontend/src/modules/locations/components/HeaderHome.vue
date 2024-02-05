<template>
    <div class="hidden md:flex items-center justify-center theme-bg p-[12px] gap-3">
        <button class="text-sm font-medium theme-text-primary line-clamp-1">
            <span class="line-clamp-1">Airbnb your home</span>
        </button>

        <button>
            <svg xmlns="http://www.w3.org/2000/svg"
                 class="w-4 h-4 stroke-0 theme-icon-primary"
                 viewBox="0 0 16 16"
                 aria-hidden="true"
                 role="presentation"
                 focusable="false">
                <path
                    d="M8 .25a7.77 7.77 0 0 1 7.75 7.78 7.75 7.75 0 0 1-7.52 7.72h-.25A7.75 7.75 0 0 1 .25 8.24v-.25A7.75 7.75 0 0 1 8 .25zm1.95 8.5h-3.9c.15 2.9 1.17 5.34 1.88 5.5H8c.68 0 1.72-2.37 1.93-5.23zm4.26 0h-2.76c-.09 1.96-.53 3.78-1.18 5.08A6.26 6.26 0 0 0 14.17 9zm-9.67 0H1.8a6.26 6.26 0 0 0 3.94 5.08 12.59 12.59 0 0 1-1.16-4.7l-.03-.38zm1.2-6.58-.12.05a6.26 6.26 0 0 0-3.83 5.03h2.75c.09-1.83.48-3.54 1.06-4.81zm2.25-.42c-.7 0-1.78 2.51-1.94 5.5h3.9c-.15-2.9-1.18-5.34-1.89-5.5h-.07zm2.28.43.03.05a12.95 12.95 0 0 1 1.15 5.02h2.75a6.28 6.28 0 0 0-3.93-5.07z">
                </path>
            </svg>
        </button>

        <!--Profile-->
        <button ref="profileButton" :disabled="profileButtonDisable"
                :class="showUserProfile ? 'shadow-md' : 'shadow-none' " @click="toggleUserProfile"
                class="hover-shadow-zero relative h-12 flex items-center justify-center gap-3 pb-[4px] pt-[4px] pl-[10px] pr-[7px] theme-border rounded-full">
      <span class="h-6 w-6 flex items-center justify-center">
        <svg class="h-5 w-5 theme-icon-primary"
             xmlns="http://www.w3.org/2000/svg"
             viewBox="0 0 32 32"
             aria-hidden="true"
             role="presentation"
             focusable="false"
             style="display: block; fill: none; height: 16px; width: 16px; stroke: currentcolor; stroke-width: 3; overflow: visible;">
          <g fill="none">
            <path d="M2 16h28M2 24h28M2 8h28"></path>
          </g>
        </svg>
         </span>
            <span class="h-8 w-8 ">
        <svg class="h-8 w-8 theme-icon-secondary"
             xmlns="http://www.w3.org/2000/svg"
             viewBox="0 0 32 32"
             aria-hidden="true"
             role="presentation"
             focusable="false"
             style="display: block; height: 100%; width: 100%; fill: currentcolor;"
        >
          <path
              d="M16 .7C7.56.7.7 7.56.7 16S7.56 31.3 16 31.3 31.3 24.44 31.3 16 24.44.7 16 .7zm0 28c-4.02 0-7.6-1.88-9.93-4.81a12.43 12.43 0 0 1 6.45-4.4A6.5 6.5 0 0 1 9.5 14a6.5 6.5 0 0 1 13 0 6.51 6.51 0 0 1-3.02 5.5 12.42 12.42 0 0 1 6.45 4.4A12.67 12.67 0 0 1 16 28.7z"></path>
        </svg>
      </span>
        </button>

        <profile-menu-component v-show="showUserProfile" @onClose="closeProfileMenu"
                                :primary-actions="primaryActions" :secondary-actions="secondaryActions"/>

    </div>

    <button class="md:hidden justify-end p-4 theme-border border-[1px] theme-icon-primary rounded-full ml-3">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" aria-hidden="true" role="presentation"
             focusable="false"
             style="display: block; fill: none; height: 16px; width: 16px; stroke: currentcolor; stroke-width: 4; overflow: visible;">
            <path fill="none"
                  d="M7 16H3m26 0H15M29 6h-4m-8 0H3m26 20h-4M7 16a4 4 0 1 0 8 0 4 4 0 0 0-8 0zM17 6a4 4 0 1 0 8 0 4 4 0 0 0-8 0zm0 20a4 4 0 1 0 8 0 4 4 0 0 0-8 0zm0 0H3"></path>
        </svg>
    </button>

</template>

<script setup lang="ts">

import ProfileMenuComponent from "@/modules/profile/components/ProfileMenuComponent.vue";
import {ref} from 'vue';
import type {MenuItem} from "@/modules/profile/models/MenuItem";

const showUserProfile = ref<boolean>(false);
const profileButtonDisable = ref<boolean>(false);

const emit = defineEmits(['signInUpRequest']);

const toggleUserProfile = () => {
    if(showUserProfile.value)
        closeProfileMenu();
    else
        openProfileMenu();
}

const openProfileMenu = () => {
    if(!profileButtonDisable.value)
        showUserProfile.value = true;
}

const closeProfileMenu = () => {
    profileButtonDisable.value = true;

    setTimeout(() => {
        profileButtonDisable.value = false;
    }, 200);

    showUserProfile.value = false;
}

// TODO : Add authentication check
const primaryActions: Array<MenuItem> = [
    {
        title: 'Log In',
        callback: () => {
            closeProfileMenu();
            emit('signInUpRequest')
        },
        special: true
    },
    {
        title: 'Sign up',
        callback: () => {
            closeProfileMenu();
            emit('signInUpRequest')
        },
    }
]

const secondaryActions: Array<MenuItem> = [
    {
        title: 'Gift cards',
        routeName: 'GiftCards'
    },
    {
        title: 'Airbnb your home',
        routeName: 'HostHomes'
    },
    {
        title: 'Help Center',
        routeName: 'Help'
    }
]


</script>