<template>

  <div
      class="fixed h-20 top-0 z-10 content-padding w-full pr-[40px] pl-[40px] bg-defaultBackground flex justify-between items-center border-b border-borderSecondary text-textPrimary">

    <!-- Logo -->
    <div class="hidden md:flex">
      <main-logo/>
    </div>

    <!-- Header Search -->
    <header-menu/>

    <!-- Header Home -->
    <header-home/>

  </div>

</template>

<script setup lang="ts">

import MainLogo from "@/common/components/icons/MainLogo.vue";

import HeaderMenu from "@/Modules/locations/components/HeaderMenu.vue";

import HeaderHome from "@/Modules/locations/components/HeaderHome.vue";

</script>

