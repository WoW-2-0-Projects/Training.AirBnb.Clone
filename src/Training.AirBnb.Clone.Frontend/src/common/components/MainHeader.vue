<template>

  <header
      class="fixed top-0 z-10 flex items-center justify-between w-full h-20 border-b theme-bg-primary content-padding theme-border">

    <!-- Logo -->
    <main-logo/>

    <!-- Header Search -->
    <header-menu/>

    <!-- Header Home -->
    <header-home @sign-in-up-request="toggleModal"/>

    <sign-in-up-modal :modal-active="modalActive" @close-modal="toggleModal"/>


    <!-- Bottom border -->
    <div class="absolute border-b-2 border-borderSecondary"></div>

  </header>

</template>

<script setup lang="ts">

import MainLogo from "@/common/components/icons/MainLogo.vue";
import HeaderMenu from "@/modules/locations/components/HeaderMenu.vue";
import HeaderHome from "@/modules/locations/components/HeaderHome.vue";
import { ref, watchEffect } from 'vue';
import SignInUpModal from "@/Modules/locations/components/SignInUpModal.vue";

const modalActive = ref(false);

const toggleModal = () => {
  modalActive.value = !modalActive.value;
}

watchEffect(() => {
  if (modalActive.value) {
    document.body.classList.add('overflow-hidden');
  } else {
    document.body.classList.remove('overflow-hidden');
  }
});

</script>