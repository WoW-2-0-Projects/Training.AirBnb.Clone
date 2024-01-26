<template>

<Teleport to="body">
    <div v-show="modalActive" class="fixed inset-0 z-10 overflow-auto bg-black bg-opacity-50 no-scrollbar" @click="closeModal">
    
        <div class="relative h-auto mx-auto my-8 border-2 sm:w-full md:w-[570px] lg:w-[570px] xl:w-[570px] theme-bg-secondary theme-border rounded-xl" @click.stop>
            <!-- Header (Login) -->
            <div class="h-[64px] border-b theme-border flex justify-between items-center px-4">

                <button class="rounded-full h-8 px-[8px] theme-button" @click="closeModal">
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

                <h2 class="font-semibold basis-[64%] theme-text-primary">Log in or sign up</h2>
            </div>

            <!-- Body -->
            <div class="mx-5 mb-5 theme-text-primary">
                <!-- Welcome Message -->
                <h1 class="mt-8 text-2xl font-semibold">Welcome to Airbnb</h1>

                <!-- Sign Up/In using phone number or email -->
                <EmailPhoneInput :by-email="signInByEmail" @input-submit="onSubmit"/>
                
                <!-- Continue button -->
                <button class="w-full py-3 mt-5 font-bold text-white bg-red-500 rounded-lg">Continue</button>

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

const onSubmit = (value: string) => {
    // need to send a request to api (when backend is ready)
}

</script>