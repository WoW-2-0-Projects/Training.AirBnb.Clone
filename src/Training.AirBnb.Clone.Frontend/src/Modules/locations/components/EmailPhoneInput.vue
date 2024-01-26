<template>

<div class="mt-5">
    <CountryCodePicker v-if="!byEmail" @country-code-changed="onCountryChanged"/>

    <div class="h-[56px] w-auto flex flex-col justify-center px-3 py-1"
        :class="[isFocused ? 'border-2 rounded-md border-black dark:border-bgColorPrimary' : byEmail ? 'border rounded-md border-[#b0b0b0]' : 'border-x border-b rounded-b-md border-[#b0b0b0]']"
        tabindex="0">
        
        <h3 class="text-[#797979] text-[12px]">{{ byEmail ? 'Email' : 'Phone number' }}</h3>
        
        <div class="flex items-center">
            <form @submit.prevent="onSubmit">
                <span v-if="!byEmail" class="whitespace-pre-wrap">{{ selectedCountryCode }}</span>
                <input v-if="!byEmail" v-model="phoneNumber" type="text" class="text-base bg-transparent theme-text-primary focus:outline-none" @keydown="handleKeyDown" @click="toggleIsFocused" @blur="onBlur">
                <input v-if="byEmail" v-model="email" type="text" class="text-base bg-transparent theme-text-primary focus:outline-none" @click="toggleIsFocused" @blur="onBlur">
            </form>
        </div>
    </div>

     <!-- Privacy Policy section -->
    <h6 class="text-[12px]">We'll call or text you to confirm your number. Standard message and data rates apply. 
        <a class="font-semibold underline" href="https://google.com" target="_blank">Privacy Policy</a>
    </h6>
</div>


</template>

<script setup lang="ts">

import type { CountryPhoneCode } from '../models/CountryPhoneCode';
import CountryCodePicker from './CountryCodePicker.vue';
import { ref, watch } from 'vue';

const props = defineProps({
    byEmail: {
        type: Boolean,
        default: false
    }
})

const phoneNumber = ref('');
const email = ref('');

const emit = defineEmits(['inputSubmit']);

const isFocused = ref<boolean>(false);

const toggleIsFocused = () => {
    isFocused.value = true;
}

const onBlur = () => {
    isFocused.value = false;
}

const handleKeyDown = (event: KeyboardEvent) => {
    const allowedKeys = ['Backspace', 'ArrowRight', 'ArrowUp', 'ArrowDown']

    if (!allowedKeys.includes(event.key) && (event.key.length === 1 && isNaN(Number(event.key)))) {
        event.preventDefault();
    }
}

const selectedCountryCode = ref<string>();

const onCountryChanged = (country: CountryPhoneCode) => {
    selectedCountryCode.value = `${country.code} `
}

const onSubmit = () => {
    if (props.byEmail) 
        emit('inputSubmit', email.value);    
    else 
        emit('inputSubmit', phoneNumber.value);
}

watch(() => props.byEmail, (oldValue, newValue) => {
    if (newValue != oldValue) {
        email.value = '';
        phoneNumber.value = '';
    }
})

</script>