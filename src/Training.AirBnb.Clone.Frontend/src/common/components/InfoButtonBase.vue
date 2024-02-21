<template>

<Teleport to="body">    
    <Transition name="slide-fade">
        <div v-show="modalActive" class="fixed inset-0 z-30 overflow-auto no-scrollbar" @click="$emit('closeModal')">
            <div class="relative h-auto mx-auto my-8 border-2 sm:w-full md:w-[745px] theme-bg-secondary rounded-xl" @click.stop>
                <slot></slot>
            </div>
        </div>
    </Transition>
</Teleport>
    
</template>

<script setup lang="ts">

const emit = defineEmits(['closeModal'])

const props = defineProps({
    modalActive: {
        type: Boolean,
        default: false
    }
});
    
</script>

<style scoped>

.slide-fade-enter-active {
  transition: all 0.3s ease-out;
}

.slide-fade-leave-active {
  transition: all 0.3s cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateY(500px);
}

</style>