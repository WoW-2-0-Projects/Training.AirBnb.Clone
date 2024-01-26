<template>

  <!--    <main-logo/>-->
  <home-view @sign-in-up-request="toggleModal"/>

  <sign-in-up-modal :modal-active="modalActive" @close-modal="toggleModal"/>

  

</template>

<script lang="ts" setup>

import HomeView from "@/common/views/HomeView.vue";
import { ref, watchEffect } from 'vue';
import SignInUpModal from "./Modules/locations/components/SignInUpModal.vue";

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