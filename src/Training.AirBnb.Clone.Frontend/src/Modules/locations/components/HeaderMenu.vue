<template>
  <div class="text-textPrimary bg-defaultBackground pt-[8px] pb-[10px] pl-[20px] pr-[10px] border border-gray-300 shadow-md rounded-full">
    <div class="h-8 hidden md:flex items-center justify-center gap-5">
      <button class="text-sm font-medium line-clamp-1">
        <span class="line-clamp-1">Anywhere</span>
      </button>

      <div class="border-r border-gray-300 h-3/5"></div>

      <button class="text-sm font-medium">
        <span class="line-clamp-1">Any week</span>
      </button>

      <div class="border-r border-gray-300 h-3/5"></div>

      <button class="flex items-center justify-center text-textSecondary text-sm gap-3">
        <span class="line-clamp-1">
          Add guests
        </span>
        <span class="bg-logoPrimary h-[32px] w-[32px] flex items-center justify-center rounded-full text-white">
                    <svg xmlns="http://www.w3.org/2000/svg"
                         viewBox="0 0 32 32"
                         aria-hidden="true"
                         role="presentation"
                         focusable="false"
                         style="display: block; fill: none; height: 12px; width: 12px; stroke: currentcolor; stroke-width: 5.33333; overflow: visible;">
                        <path fill="none"
                              d="M13 24a11 11 0 1 0 0-22 11 11 0 0 0 0 22zm8-3 9 9"></path>
                    </svg>
                </span>
      </button>

    </div>
    <!-- Location menu responsive -->
    <button class="md:hidden flex items-center justify-center gap-3 h-12 pr-[10px]">

      <span class="h-[40px] w-[40px] flex items-center justify-center text-textPrimary">
          <svg class="w-[20px] h-[20px] stroke-textPrimary stroke-[5px]"
               width="20px" height="20px"
               xmlns="http://www.w3.org/2000/svg"
               viewBox="0 0 32 32"
               aria-hidden="true"
               role="presentation"
               focusable="false">
              <path fill="none"
                    d="M13 24a11 11 0 1 0 0-22 11 11 0 0 0 0 22zm8-3 9 9"></path>
          </svg>
      </span>

      <span class="flex flex-col items-start">
                <span class="text-sm font-medium">
                    <span>Anywhere</span>
                </span>
                <span class="text-xs text-textSecondary">
                    <span>Any week</span>
                    <span>•</span>
                    <span>Add guests</span>
                </span>
            </span>

    </button>


  </div>

</template>
<script setup lang="ts">
</script>