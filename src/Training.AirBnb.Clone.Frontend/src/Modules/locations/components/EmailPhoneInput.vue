<template>

<div class="mt-5">
    <CountryCodePicker v-if="!byEmail"/>

    <div class="h-[56px] w-[520px] flex flex-col justify-center px-3 py-1"
        :class="[isFocused ? 'border-2 rounded-md border-black dark:border-bgColorPrimary' : byEmail ? 'border rounded-md border-[#b0b0b0]' : 'border-x border-b rounded-b-md border-[#b0b0b0]']"
        tabindex="0">
        
        <h3 class="text-[#797979] text-[12px]">{{ byEmail ? 'Email' : 'Phone number' }}</h3>
        <input v-if="!byEmail" type="text" class="text-base bg-transparent theme-text-primary focus:outline-none" @keydown="handleKeyDown" @click="toggleIsFocused" @blur="onBlur">
        <input v-if="byEmail" type="text" class="text-base bg-transparent theme-text-primary focus:outline-none" @click="toggleIsFocused" @blur="onBlur">

    </div>
</div>


</template>

<script setup lang="ts">

import CountryCodePicker from './CountryCodePicker.vue';
import { ref } from 'vue';

const props = defineProps({
    byEmail: {
        type: Boolean,
        default: false
    }
})

const isFocused = ref<boolean>(false);

const toggleIsFocused = () => {
    isFocused.value = true;
}

const onBlur = () => {
    isFocused.value = false;
    console.log("blur")
}

const handleKeyDown = (event: KeyboardEvent) => {
    const allowedKeys = ['Backspace', 'ArrowRight', 'ArrowUp', 'ArrowDown']

    if (!allowedKeys.includes(event.key) && (event.key.length === 1 && isNaN(Number(event.key)))) {
        event.preventDefault();
    }
}

</script>