<template>

<Teleport to="body">
    <div v-show="modalActive" class="absolute top-0 z-10 flex justify-center w-screen bg-black bg-opacity-40">
    
        <div class="border-2 w-[570px] bg-white rounded-xl h-auto">
            <!-- Header (Login) -->
            <div class="h-[64px] border-b gray-300 flex justify-between items-center px-4">

                <button class="hover:bg-gray-100 rounded-full h-8 px-[8px]" @click="closeModal">
                    <svg xmlns="http://www.w3.org/2000/svg" 
                    viewBox="0 0 32 32" 
                    aria-hidden="true" 
                    role="presentation" 
                    focusable="false" 
                    style="display: block; 
                    fill: none; 
                    height: 16px; 
                    width: 16px; 
                    stroke: currentcolor; 
                    stroke-width: 3; 
                    overflow: visible;">
                    <path d="m6 6 20 20M26 6 6 26"></path>
                    </svg>
                </button>

                <h2 class="font-semibold basis-[64%] text-textPrimary">Log in or sign up</h2>
            </div>

            <!-- Body -->
            <div class="mx-5 mb-5">
                <!-- Welcome Message -->
                <h1 class="mt-8 text-2xl font-semibold text-textPrimary">Welcome to Airbnb</h1>

                <!-- Sign Up/In using phone number or email -->
                <EmailPhoneInput :by-email="signInByEmail"/>
                
                <!-- Privacy Policy section -->
                <h6 class="text-[12px]">We'll call or text you to confirm your number. Standard message and data rates apply. 
                    <a class="font-semibold underline text-textPrimary" href="https://google.com" target="_blank">Privacy Policy</a>
                </h6>
                
                <!-- Continue button -->
                <button class="rounded-lg bg-red-500 text-white font-bold mt-5 px-[228px] py-3">Continue</button>

                <!-- Divider -->
                <div class="flex mt-5 place-items-center">
                    <div class="h-[1px] w-full  bg-gray-300"></div>
                    <h6 class="mx-6 text-[12.5px] text-textPrimary">or</h6>
                    <div class="h-[1px] w-full  bg-gray-300"></div>
                </div>

                <AuthenticationServices :by-email="signInByEmail" @change-auth-type="changeAuthType"/>
            </div>
        </div>
    </div>
</Teleport>



</template>

<script setup lang="ts">

import EmailPhoneInput from './EmailPhoneInput.vue';
import AuthenticationServices from './AuthenticationServices.vue'
import { ref } from 'vue';

const emit = defineEmits(['closeModal'])

const closeModal = () => {
    emit('closeModal')
}

const props = defineProps({
    modalActive: {
        type: Boolean,
        default: false
    }
});

const signInByEmail = ref<boolean>(false);

const changeAuthType = () => {
    signInByEmail.value = !signInByEmail.value;
}

</script>