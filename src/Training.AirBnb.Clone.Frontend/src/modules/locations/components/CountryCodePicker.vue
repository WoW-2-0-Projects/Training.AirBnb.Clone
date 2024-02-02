<template>

<div class="h-[56px] w-auto px-3 py-1 flex justify-between"
    :class="[isOpen ? 'border-2 rounded-md border-black dark:border-bgColorPrimary' : 'border rounded-t-md border-inputBorderColor']"
    tabindex="0"
    @click="toggleDown"
    @blur="onBlur">
    <div>
        <h3 class="text-modalTextSecondary text-[12px]">Country/Region</h3>
        <h1 class="text-base theme-text-primary">{{ `${selectedCountry.country} (${selectedCountry.code})` }}</h1>
    </div>
    
    <button>
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
            stroke-width: 4; 
            overflow: visible;">
            <path fill="none" d="M28 12 16.7 23.3a1 1 0 0 1-1.4 0L4 12"></path>
        </svg>
    </button>
</div>

<div v-show="isOpen" class="relative">


<div class="absolute z-20 w-full overflow-y-auto border rounded-md shadow-xl theme-bg-secondary theme-text-primary border-inputBorderColor max-h-96">
    <h5 v-for="(country, index) in countriesWithPhoneCodes" :key="index" class="px-1 hover:bg-dropDownHoverColor hover:text-white" @mousedown="selectCountry(country)">{{ `${country.country} (${country.code})` }}</h5>
</div>

</div>
    
</template>

<script setup lang="ts">

import { CountryPhoneCode } from '../models/CountryPhoneCode';
import { onMounted, ref } from 'vue';

const emit = defineEmits(['countryCodeChanged']);

const isOpen = ref(false);

const toggleDown = () => {
    isOpen.value = !isOpen.value;
}

const onBlur = () => {
    isOpen.value = false;
}

const countriesWithPhoneCodes: CountryPhoneCode[] = [
  { country: 'Afghanistan', code: '+93' },
  { country: 'Albania', code: '+355' },
  { country: 'Algeria', code: '+213' },
  { country: 'Andorra', code: '+376' },
  { country: 'Angola', code: '+244' },
  { country: 'Antigua and Barbuda', code: '+1-268' },
  { country: 'Argentina', code: '+54' },
  { country: 'Armenia', code: '+374' },
  { country: 'Australia', code: '+61' },
  { country: 'Austria', code: '+43' },
  { country: 'Azerbaijan', code: '+994' },
  { country: 'Bahrain', code: '+973' },
  { country: 'Bangladesh', code: '+880' },
  { country: 'Belarus', code: '+375' },
  { country: 'Belgium', code: '+32' },
  { country: 'Belize', code: '+501' },
  { country: 'Benin', code: '+229' },
  { country: 'Bhutan', code: '+975' },
  { country: 'Bolivia', code: '+591' },
  { country: 'Bosnia and Herzegovina', code: '+387' },
  { country: 'Botswana', code: '+267' },
  { country: 'Brazil', code: '+55' },
  { country: 'Brunei', code: '+673' },
  { country: 'Bulgaria', code: '+359' },
  { country: 'Burkina Faso', code: '+226' },
  { country: 'Burundi', code: '+257' },
  { country: 'Cabo Verde', code: '+238' },
  { country: 'Cambodia', code: '+855' },
  { country: 'Cameroon', code: '+237' },
  { country: 'Canada', code: '+1' },
  { country: 'Central African Republic', code: '+236' },
  { country: 'Chad', code: '+235' },
  { country: 'Chile', code: '+56' },
  { country: 'China', code: '+86' },
  { country: 'Colombia', code: '+57' },
  { country: 'Comoros', code: '+269' },
  { country: 'Fiji', code: '+679' },
  { country: 'Finland', code: '+358' },
  { country: 'France', code: '+33' },
  { country: 'Gabon', code: '+241' },
  { country: 'Gambia', code: '+220' },
  { country: 'Iran', code: '+98' },
  { country: 'Iraq', code: '+964' },
  { country: 'Ireland', code: '+353' },
  { country: 'Italy', code: '+39' },
  { country: 'Japan', code: '+81' },
  { country: 'Jordan', code: '+962' },
  { country: 'Kazakhstan', code: '+7' },
  { country: 'Kenya', code: '+254' },
  { country: 'Kiribati', code: '+686' },
  { country: 'Korea, North', code: '+850' },
  { country: 'Korea, South', code: '+82' },
  { country: 'Kosovo', code: '+383' },
  { country: 'Kuwait', code: '+965' },
  { country: 'Kyrgyzstan', code: '+996' },
  { country: 'Laos', code: '+856' },
  { country: 'Latvia', code: '+371' },
  { country: 'Lebanon', code: '+961' },
  { country: 'Lesotho', code: '+266' },
  { country: 'Liberia', code: '+231' },
  { country: 'Libya', code: '+218' },
  { country: 'Liechtenstein', code: '+423' },
  { country: 'Lithuania', code: '+370' },
  { country: 'Luxembourg', code: '+352' },
  { country: 'Madagascar', code: '+261' },
  { country: 'Malawi', code: '+265' },
  { country: 'Malaysia', code: '+60' },
  { country: 'Maldives', code: '+960' },
  { country: 'Mali', code: '+223' },
  { country: 'Malta', code: '+356' },
  { country: 'Marshall Islands', code: '+692' },
  { country: 'Mauritania', code: '+222' },
  { country: 'Mauritius', code: '+230' },
  { country: 'Mexico', code: '+52' },
  { country: 'Micronesia', code: '+691' },
  { country: 'Moldova', code: '+373' },
  { country: 'Monaco', code: '+377' },
  { country: 'Mongolia', code: '+976' },
  { country: 'Montenegro', code: '+382' },
  { country: 'Morocco', code: '+212' },
  { country: 'Mozambique', code: '+258' },
  { country: 'Myanmar', code: '+95' },
  { country: 'Namibia', code: '+264' },
  { country: 'Nauru', code: '+674' },
  { country: 'Nepal', code: '+977' },
  { country: 'Netherlands', code: '+31' },
  { country: 'New Zealand', code: '+64' },
  { country: 'Nicaragua', code: '+505' },
  { country: 'Niger', code: '+227' },
  { country: 'Nigeria', code: '+234' },
  { country: 'North Macedonia', code: '+389' },
  { country: 'Norway', code: '+47' },
  { country: 'Oman', code: '+968' },
  { country: 'Pakistan', code: '+92' },
  { country: 'Palau', code: '+680' },
  { country: 'Panama', code: '+507' },
  { country: 'Papua New Guinea', code: '+675' },
  { country: 'Paraguay', code: '+595' },
  { country: 'Peru', code: '+51' },
  { country: 'Philippines', code: '+63' },
  { country: 'Poland', code: '+48' },
  { country: 'Portugal', code: '+351' },
  { country: 'Qatar', code: '+974' },
  { country: 'Romania', code: '+40' },
  { country: 'Russia', code: '+7' },
  { country: 'Saudi Arabia', code: '+966' },
  { country: 'Senegal', code: '+221' },
  { country: 'Serbia', code: '+381' },
  { country: 'Seychelles', code: '+248' },
  { country: 'Sierra Leone', code: '+232' },
  { country: 'Singapore', code: '+65' },
  { country: 'Slovakia', code: '+421' },
  { country: 'Slovenia', code: '+386' },
  { country: 'Solomon Islands', code: '+677' },
  { country: 'Somalia', code: '+252' },
  { country: 'South Africa', code: '+27' },
  { country: 'South Sudan', code: '+211' },
  { country: 'Spain', code: '+34' },
  { country: 'Sri Lanka', code: '+94' },
  { country: 'Sudan', code: '+249' },
  { country: 'Suriname', code: '+597' },
  { country: 'Sweden', code: '+46' },
  { country: 'Switzerland', code: '+41' },
  { country: 'Syria', code: '+963' },
  { country: 'Taiwan', code: '+886' },
  { country: 'Tajikistan', code: '+992' },
  { country: 'Tanzania', code: '+255' },
  { country: 'Thailand', code: '+66' },
  { country: 'Togo', code: '+228' },
  { country: 'Tonga', code: '+676' },
  { country: 'Tunisia', code: '+216' },
  { country: 'Turkey', code: '+90' },
  { country: 'Turkmenistan', code: '+993' },
  { country: 'Tuvalu', code: '+688' },
  { country: 'Uganda', code: '+256' },
  { country: 'Ukraine', code: '+380' },
  { country: 'United Arab Emirates', code: '+971' },
  { country: 'United Kingdom', code: '+44' },
  { country: 'United States', code: '+1' },
  { country: 'Uruguay', code: '+598' },
  { country: 'Uzbekistan', code: '+998' },
  { country: 'Vanuatu', code: '+678' },
  { country: 'Vatican City', code: '+379' },
  { country: 'Venezuela', code: '+58' },
  { country: 'Vietnam', code: '+84' },
  { country: 'Yemen', code: '+967' },
  { country: 'Zambia', code: '+260' },
  { country: 'Zimbabwe', code: '+263' },
];

const selectedCountry = ref<CountryPhoneCode>(countriesWithPhoneCodes[0]);

const selectCountry = (country: CountryPhoneCode) => {
    selectedCountry.value = country;
    emit('countryCodeChanged', country);
}

onMounted(() => {
   selectCountry(selectedCountry.value); 
});

</script>