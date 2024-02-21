<template>
  <InfoButtonBase class="items-center justify-center flex" :modalActive="props.modalActive" @close-modal="closeModal">
    <div class="w-[750px] rounded-xl px-5" v-show="modalActive">
        <div class="m-5 flex cursor-pointer" @click="$emit('close-modal')">
      <span>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" aria-hidden="true" role="presentation" focusable="false" style="display: block; fill: none; height: 16px; width: 16px; stroke: currentcolor; stroke-width: 3; overflow: hidden;">
          <path
              d="m6 6 20 20M26 6 6 26">
          </path>
        </svg>
      </span>
        </div>
      <h1 class="flex items-center justify-start text-xl font-bold ml-5">
        About this space
      </h1>
      <div class="flex ml-5 mt-5 max-h-[550px] overflow-y-scroll">
        <p>
          A cosy nature inspired cabin, on the way and close to the Northcape.
          Take it easy at this unique and relaxing getaway.
          Currently, under renovation will be done by 01.06. and there are more pictures coming as soon as the bedrooms and bathroom are going to be done and remodelled. We are also adding jacuzzi and sauna.
          Surroundings are astonishing, and feeling in the cabiA cosy nature inspired cabin, on the way and close to the Northcape.
          Take it easy at this unique and relaxing getaway.
          Currently, under renovation will be done by 01.06. and there are more pictures coming as soon as the bedrooms and bathroom are going to be done and remodelled. We are also adding jacuzzi and sauna.
          Surroundings are astonishing, and feeling in the cabin is so calming.Sections 1.10.32 and 1.10.33 from "de Finibus Bonorum et Malorum" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.
        </p>
      </div>
    </div>
  </InfoButtonBase>
</template>

<script setup lang="ts">
import ModalBase from "@/common/components/InfoButtonBase.vue";
import InfoButtonBase from "@/common/components/InfoButtonBase.vue";

const emit = defineEmits(["closeModal"])
const props = defineProps({
  modalActive:{
    type: Boolean,
    default: false
  }
})
const closeModal = () => {
  emit('closeModal')
}
</script>
