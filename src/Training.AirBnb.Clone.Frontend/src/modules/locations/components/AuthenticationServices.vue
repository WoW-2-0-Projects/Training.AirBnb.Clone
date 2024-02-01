<template>
     <!-- Sign up with other services-->
     <div class="flex flex-col gap-4 mt-5">
        <button class="flex items-center h-12 border rounded-lg theme-button border-textPrimary dark:border-bgColorPrimary">
            <svg class="basis-[12%]" 
            xmlns="http://www.w3.org/2000/svg" 
            viewBox="0 0 32 32" 
            aria-hidden="true" 
            role="presentation"  
            focusable="false" 
            style="display: block; 
            height: 20px; 
            width: 20px;">
                <path fill="#1877F2" d="M32 0v32H0V0z"></path>
                <path fill="#FFF" d="M22.94 16H18.5v-3c0-1.27.62-2.5 2.6-2.5h2.02V6.56s-1.83-.31-3.58-.31c-3.65 0-6.04 2.21-6.04 6.22V16H9.44v4.63h4.06V32h5V20.62h3.73l.7-4.62z"></path>
            </svg>
            <h1 class="font-semibold text-sm basis-[75%]">Continue with Facebook</h1>
        </button>

        <button class="flex items-center h-12 border rounded-lg theme-button border-textPrimary dark:border-bgColorPrimary">
            <svg class="basis-[12%]" 
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 32 32" 
            aria-hidden="true" 
            role="presentation" 
            focusable="false" 
            style="display: block; 
            height: 20px; 
            width: 20px;">
                <path fill="#4285f4" d="M24.12 25c2.82-2.63 4.07-7 3.32-11.19H16.25v4.63h6.37A5.26 5.26 0 0 1 20.25 22z"></path>
                <path fill="#34a853" d="M5.62 21.31A12 12 0 0 0 24.12 25l-3.87-3a7.16 7.16 0 0 1-10.69-3.75z"></path>
                <path fill="#fbbc02" d="M9.56 18.25c-.5-1.56-.5-3 0-4.56l-3.94-3.07a12.08 12.08 0 0 0 0 10.7z"></path>
                <path fill="#ea4335" d="M9.56 13.69c1.38-4.32 7.25-6.82 11.19-3.13l3.44-3.37a11.8 11.8 0 0 0-18.57 3.43l3.94 3.07z">
                </path>
            </svg>
            <h1 class="font-semibold text-sm basis-[75%]">Continue with Google</h1>
        </button>

        <button class="flex items-center h-12 border rounded-lg theme-button border-textPrimary dark:border-bgColorPrimary">
            <svg class="basis-[12%]"
            viewBox="0 0 24 24" 
            xmlns="http://www.w3.org/2000/svg"
            role="presentation" 
            aria-hidden="true" 
            focusable="false" 
            style="height: 20px; 
            width: 20px; 
            display: block; 
            fill: currentcolor;">
                <path d="m13.3 2.1a5.1 5.1 0 0 1 3.8-2.1 5.1 5.1 0 0 1 -1.2 3.8 4.1 4.1 0 0 1 -3.6 1.7 4.5 4.5 0 0 1 1-3.4zm-5 3.7c-2.8 0-5.8 2.5-5.8 7.3 0 4.9 3.5 10.9 6.3 10.9 1 0 2.5-1 4-1s2.6.9 4 .9c3.1 0 5.3-6.4 5.3-6.4a5.3 5.3 0 0 1 -3.2-4.9 5.2 5.2 0 0 1 2.6-4.5 5.4 5.4 0 0 0 -4.7-2.4c-2 0-3.5 1.1-4.3 1.1-.9 0-2.4-1-4.2-1z"></path>
            </svg>
            <h1 class="font-semibold text-sm basis-[75%]">Continue with Apple</h1>
        </button>

        <button class="flex items-center h-12 border rounded-lg border-textPrimary theme-button dark:border-bgColorPrimary"
            @click="changeAuthType">
            
            <!-- Email Icon -->
            <svg v-if="!byEmail"
            class="basis-[12%]" 
            xmlns="http://www.w3.org/2000/svg" 
            viewBox="0 0 32 32" 
            aria-hidden="true" 
            role="presentation" 
            focusable="false" 
            style="display: block; 
            height: 20px; 
            width: 20px; 
            fill: currentcolor;">
                <path d="M30.51 5.88A5.06 5.06 0 0 0 26 3H6a5.06 5.06 0 0 0-4.51 2.88A4.94 4.94 0 0 0 1 8v16a5 5 0 0 0 5 5h20a5 5 0 0 0 5-5V8a4.94 4.94 0 0 0-.49-2.12ZM6 5h20a2.97 2.97 0 0 1 1.77.6L17.95 14a2.98 2.98 0 0 1-3.9 0L4.23 5.6A2.97 2.97 0 0 1 6 5Zm23 19a3 3 0 0 1-3 3H6a3 3 0 0 1-3-3V8a2.97 2.97 0 0 1 .1-.74l9.65 8.27a4.97 4.97 0 0 0 6.5 0l9.65-8.27A2.97 2.97 0 0 1 29 8Z"></path>
            </svg>

            <!-- Phone Icon -->
            <svg v-if="byEmail"
                class="basis-[12%]" 
                xmlns="http://www.w3.org/2000/svg" 
                viewBox="0 0 32 32" 
                aria-hidden="true" 
                role="presentation" 
                focusable="false" 
                style="display: block; 
                height: 20px; 
                width: 20px; 
                fill: currentcolor;">
                    <path d="M22 1a5 5 0 0 1 5 4.78V26a5 5 0 0 1-4.78 5H10a5 5 0 0 1-5-4.78V6a5 5 0 0 1 4.78-5H10zm0 2H10a3 3 0 0 0-3 2.82V26a3 3 0 0 0 2.82 3H22a3 3 0 0 0 3-2.82V6a3 3 0 0 0-2.82-3zm-6 22a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm4-4a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm-4 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm-4 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm8-4a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm-4 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm-4 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm8-4a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm-4 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm-4 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2z"></path>
            </svg>
            
            <h1 class="font-semibold text-sm basis-[75%]">{{ byEmail ? 'Continue with phone' : 'Continue with email' }}</h1>
        </button>

    </div>
</template>

<script setup lang="ts">

const emit = defineEmits(['changeAuthType'])

const changeAuthType = () => {
    emit('changeAuthType');
}

const props = defineProps({
    byEmail: {
        type: Boolean,
        required: true
    }
})

</script>