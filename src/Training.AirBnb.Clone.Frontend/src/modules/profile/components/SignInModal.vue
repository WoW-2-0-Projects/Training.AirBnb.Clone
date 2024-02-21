<template>

    <ModalBase :modalActive="props.modalActive" @close-modal="closeModal">

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
            <h1 class="mt-8 text-2xl font-semibold">{{ modalTitle }}</h1>

            <!-- Sign by email form -->
            <form v-if="signInByEmail" @submit.prevent="onSubmit">

                <form-input v-model="signInByEmailValue.emailAddress"
                            :type="FormInputType.Email" class="mt-5"
                            label="Email address" placeholder="Enter your email address here"/>

                <form-input v-if="userExistenceCheckResult" v-model="signInByEmailValue.password"
                            :type="FormInputType.Password" class="mt-5"
                            label="Password" placeholder="Enter your password here"/>

            </form>

            <!-- Sign in by phone number form -->
            <form v-if="!signInByEmail" @submit.prevent="onSubmit">

                <form-drop-down :values="countriesWithPhoneCodes" v-model="selectedCountryCode" class="mt-5"
                                label="Country / Region" placeholder="Search country"/>

                <form-input v-model="signInByPhoneValue.phoneNumber" :prefix="selectedCountryCode?.value"
                            :type="FormInputType.Number" class="mt-5"
                            label="Phone number" placeholder="Enter your phone number here"/>

                <form-input v-if="userExistenceCheckResult" v-model="signInByPhoneValue.password"
                            :type="FormInputType.Password" class="mt-5"
                            label="Password" placeholder="Enter your password here"/>

            </form>

            <!-- Continue button -->
            <button class="w-full py-3 mt-5 font-bold text-white bg-red-500 rounded-lg" @click="onSubmit">
                Continue
            </button>

            <!-- Divider -->
            <div class="flex mt-5 place-items-center">
                <div class="h-[1px] w-full  bg-gray-300"></div>
                <h6 class="mx-6 text-[12.5px] theme-text-primary">or</h6>
                <div class="h-[1px] w-full  bg-gray-300"></div>
            </div>

            <AuthenticationServices :by-email="signInByEmail" @change-auth-type="changeAuthType"/>

        </div>

    </ModalBase>

</template>

<script setup lang="ts">

import {ref} from 'vue';
import AuthenticationServices from '../../locations/components/AuthenticationServices.vue'
import ModalBase from '@/common/components/ModalBase.vue';
import FormInput from "@/common/components/forms/FormInput.vue";
import FormDropDown from "@/common/components/forms/dropDown/FormDropDown.vue";
import {FormInputType} from "@/common/components/forms/FormInputType";
import {SignInByEmailDetails} from "@/modules/profile/models/SignInByEmailDetails";
import {SignInByPhoneDetails} from "@/modules/profile/models/SignInByPhoneDetails";
import {AirBnbApiClient} from "@/infrastructure/apiClients/airBnbApiClient/brokers/AirBnbApiClient";
import {LayoutConstants} from "@/infrastructure/constants/LayoutConstants";
import {SignInDetails} from "@/modules/profile/models/SignInDetails";
import {AuthenticationService} from "@/modules/profile/services/AuthenticationService";
import type {DropDownValue} from "@/common/components/forms/dropDown/DropDownValue";

const emit = defineEmits(['closeModal']);

const props = defineProps({
    modalActive: {
        type: Boolean,
        default: false
    }
});

// Component states
const signInByEmailValue = ref<SignInByEmailDetails>(new SignInByEmailDetails());
const signInByPhoneValue = ref<SignInByPhoneDetails>(new SignInByPhoneDetails());
const userExistenceCheckResult = ref<boolean | null>(null);
const modalTitle = ref<string>(LayoutConstants.WelcomeToMessage);
const signInByEmail = ref<boolean>(false);

// Services
const airBnbApiClient = new AirBnbApiClient();
const authService = new AuthenticationService();

const changeAuthType = () => {
    signInByEmail.value = !signInByEmail.value;
    userExistenceCheckResult.value = null;
}

const closeModal = () => {
    // Reset values
    signInByEmailValue.value = new SignInByEmailDetails();
    signInByPhoneValue.value = new SignInByPhoneDetails();
    signInByEmail.value = false;

    emit('closeModal')
}

const onSubmit = async (value: string) => {
    // TODO : Add validation
    if (signInByEmail.value) {
       await onSubmitByEmailAsync();
    }

    if(!signInByEmail.value)
      await onSubmitByPhoneAsync();
}

const onSubmitByPhoneAsync = async () => {
    // TODO : inform user about the error ( if user check or sign in fails )

    // Check user existence
    if (!userExistenceCheckResult.value) {
        const userExistsResponse = await airBnbApiClient.accounts.checkByPhoneNumberAsync(signInByPhoneValue.value.phoneNumber);
        if (userExistsResponse.status !== 200 && userExistsResponse.status !== 404)
            closeModal();

        // if user doesn't exist, open sign up modal
        if (!userExistsResponse)
            ;

        userExistenceCheckResult.value = userExistsResponse.status === 200;
        modalTitle.value = LayoutConstants.WelcomeBackMessage + userExistsResponse.response;

        return;
    }

    // Sign in
    // Validation

    const signInDetails = new SignInDetails();
    signInDetails.signInByPhone = signInByPhoneValue.value;
    const result = await authService.signInAsync(signInDetails);

    // If result is false display error

    if (result)
        closeModal();
}

/* region Sign in by phone number





 */

/* endregion */

/* region Sign in by email */

const onSubmitByEmailAsync = async () => {
    // TODO : inform user about the error ( if user check or sign in fails )

    // Check user existence
    if (!userExistenceCheckResult.value) {
        const userExistsResponse = await airBnbApiClient.accounts.checkByEmailAsync(signInByEmailValue.value.emailAddress);
        if (userExistsResponse.status !== 200 && userExistsResponse.status !== 404)
            closeModal();

        // if user doesn't exist, open sign up modal
        if (!userExistsResponse)
            ;

        userExistenceCheckResult.value = userExistsResponse.status === 200;
        modalTitle.value = LayoutConstants.WelcomeBackMessage + userExistsResponse.response;

        return;
    }

    // Sign in
    // Validation

    const signInDetails = new SignInDetails();
    signInDetails.signInByEmail = signInByEmailValue.value;
    const result = await authService.signInAsync(signInDetails);

    // If result is false display error

    if (result)
        closeModal();
}


const countriesWithPhoneCodes: DropDownValue[] = [
    {
        "key": "Afghanistan (+93)",
        "value": "+93"
    }, {"key": "Albania (+355)", "value": "+355"}, {"key": "Algeria (+213)", "value": "+213"}, {
        "key": "Andorra (+376)",
        "value": "+376"
    }, {"key": "Angola (+244)", "value": "+244"}, {
        "key": "Antigua and Barbuda (+1-268)",
        "value": "+1-268"
    }, {"key": "Argentina (+54)", "value": "+54"}, {"key": "Armenia (+374)", "value": "+374"}, {
        "key": "Australia (+61)",
        "value": "+61"
    }, {"key": "Austria (+43)", "value": "+43"}, {"key": "Azerbaijan (+994)", "value": "+994"}, {
        "key": "Bahrain (+973)",
        "value": "+973"
    }, {"key": "Bangladesh (+880)", "value": "+880"}, {"key": "Belarus (+375)", "value": "+375"}, {
        "key": "Belgium (+32)",
        "value": "+32"
    }, {"key": "Belize (+501)", "value": "+501"}, {"key": "Benin (+229)", "value": "+229"}, {
        "key": "Bhutan (+975)",
        "value": "+975"
    }, {"key": "Bolivia (+591)", "value": "+591"}, {
        "key": "Bosnia and Herzegovina (+387)",
        "value": "+387"
    }, {"key": "Botswana (+267)", "value": "+267"}, {"key": "Brazil (+55)", "value": "+55"}, {
        "key": "Brunei (+673)",
        "value": "+673"
    }, {"key": "Bulgaria (+359)", "value": "+359"}, {
        "key": "Burkina Faso (+226)",
        "value": "+226"
    }, {"key": "Burundi (+257)", "value": "+257"}, {"key": "Cabo Verde (+238)", "value": "+238"}, {
        "key": "Cambodia (+855)",
        "value": "+855"
    }, {"key": "Cameroon (+237)", "value": "+237"}, {
        "key": "Canada (+1)",
        "value": "+1"
    }, {"key": "Central African Republic (+236)", "value": "+236"}, {
        "key": "Chad (+235)",
        "value": "+235"
    }, {"key": "Chile (+56)", "value": "+56"}, {"key": "China (+86)", "value": "+86"}, {
        "key": "Colombia (+57)",
        "value": "+57"
    }, {"key": "Comoros (+269)", "value": "+269"}, {"key": "Fiji (+679)", "value": "+679"}, {
        "key": "Finland (+358)",
        "value": "+358"
    }, {"key": "France (+33)", "value": "+33"}, {"key": "Gabon (+241)", "value": "+241"}, {
        "key": "Gambia (+220)",
        "value": "+220"
    }, {"key": "Iran (+98)", "value": "+98"}, {"key": "Iraq (+964)", "value": "+964"}, {
        "key": "Ireland (+353)",
        "value": "+353"
    }, {"key": "Italy (+39)", "value": "+39"}, {"key": "Japan (+81)", "value": "+81"}, {
        "key": "Jordan (+962)",
        "value": "+962"
    }, {"key": "Kazakhstan (+7)", "value": "+7"}, {"key": "Kenya (+254)", "value": "+254"}, {
        "key": "Kiribati (+686)",
        "value": "+686"
    }, {"key": "Korea, North (+850)", "value": "+850"}, {
        "key": "Korea, South (+82)",
        "value": "+82"
    }, {"key": "Kosovo (+383)", "value": "+383"}, {"key": "Kuwait (+965)", "value": "+965"}, {
        "key": "Kyrgyzstan (+996)",
        "value": "+996"
    }, {"key": "Laos (+856)", "value": "+856"}, {"key": "Latvia (+371)", "value": "+371"}, {
        "key": "Lebanon (+961)",
        "value": "+961"
    }, {"key": "Lesotho (+266)", "value": "+266"}, {"key": "Liberia (+231)", "value": "+231"}, {
        "key": "Libya (+218)",
        "value": "+218"
    }, {"key": "Liechtenstein (+423)", "value": "+423"}, {
        "key": "Lithuania (+370)",
        "value": "+370"
    }, {"key": "Luxembourg (+352)", "value": "+352"}, {
        "key": "Madagascar (+261)",
        "value": "+261"
    }, {"key": "Malawi (+265)", "value": "+265"}, {"key": "Malaysia (+60)", "value": "+60"}, {
        "key": "Maldives (+960)",
        "value": "+960"
    }, {"key": "Mali (+223)", "value": "+223"}, {"key": "Malta (+356)", "value": "+356"}, {
        "key": "Marshall Islands (+692)",
        "value": "+692"
    }, {"key": "Mauritania (+222)", "value": "+222"}, {"key": "Mauritius (+230)", "value": "+230"}, {
        "key": "Mexico (+52)",
        "value": "+52"
    }, {"key": "Micronesia (+691)", "value": "+691"}, {"key": "Moldova (+373)", "value": "+373"}, {
        "key": "Monaco (+377)",
        "value": "+377"
    }, {"key": "Mongolia (+976)", "value": "+976"}, {"key": "Montenegro (+382)", "value": "+382"}, {
        "key": "Morocco (+212)",
        "value": "+212"
    }, {"key": "Mozambique (+258)", "value": "+258"}, {"key": "Myanmar (+95)", "value": "+95"}, {
        "key": "Namibia (+264)",
        "value": "+264"
    }, {"key": "Nauru (+674)", "value": "+674"}, {"key": "Nepal (+977)", "value": "+977"}, {
        "key": "Netherlands (+31)",
        "value": "+31"
    }, {"key": "New Zealand (+64)", "value": "+64"}, {"key": "Nicaragua (+505)", "value": "+505"}, {
        "key": "Niger (+227)",
        "value": "+227"
    }, {"key": "Nigeria (+234)", "value": "+234"}, {
        "key": "North Macedonia (+389)",
        "value": "+389"
    }, {"key": "Norway (+47)", "value": "+47"}, {"key": "Oman (+968)", "value": "+968"}, {
        "key": "Pakistan (+92)",
        "value": "+92"
    }, {"key": "Palau (+680)", "value": "+680"}, {
        "key": "Panama (+507)",
        "value": "+507"
    }, {"key": "Papua New Guinea (+675)", "value": "+675"}, {
        "key": "Paraguay (+595)",
        "value": "+595"
    }, {"key": "Peru (+51)", "value": "+51"}, {"key": "Philippines (+63)", "value": "+63"}, {
        "key": "Poland (+48)",
        "value": "+48"
    }, {"key": "Portugal (+351)", "value": "+351"}, {"key": "Qatar (+974)", "value": "+974"}, {
        "key": "Romania (+40)",
        "value": "+40"
    }, {"key": "Russia (+7)", "value": "+7"}, {"key": "Saudi Arabia (+966)", "value": "+966"}, {
        "key": "Senegal (+221)",
        "value": "+221"
    }, {"key": "Serbia (+381)", "value": "+381"}, {
        "key": "Seychelles (+248)",
        "value": "+248"
    }, {"key": "Sierra Leone (+232)", "value": "+232"}, {
        "key": "Singapore (+65)",
        "value": "+65"
    }, {"key": "Slovakia (+421)", "value": "+421"}, {
        "key": "Slovenia (+386)",
        "value": "+386"
    }, {"key": "Solomon Islands (+677)", "value": "+677"}, {
        "key": "Somalia (+252)",
        "value": "+252"
    }, {"key": "South Africa (+27)", "value": "+27"}, {"key": "South Sudan (+211)", "value": "+211"}, {
        "key": "Spain (+34)",
        "value": "+34"
    }, {"key": "Sri Lanka (+94)", "value": "+94"}, {"key": "Sudan (+249)", "value": "+249"}, {
        "key": "Suriname (+597)",
        "value": "+597"
    }, {"key": "Sweden (+46)", "value": "+46"}, {"key": "Switzerland (+41)", "value": "+41"}, {
        "key": "Syria (+963)",
        "value": "+963"
    }, {"key": "Taiwan (+886)", "value": "+886"}, {"key": "Tajikistan (+992)", "value": "+992"}, {
        "key": "Tanzania (+255)",
        "value": "+255"
    }, {"key": "Thailand (+66)", "value": "+66"}, {"key": "Togo (+228)", "value": "+228"}, {
        "key": "Tonga (+676)",
        "value": "+676"
    }, {"key": "Tunisia (+216)", "value": "+216"}, {"key": "Turkey (+90)", "value": "+90"}, {
        "key": "Turkmenistan (+993)",
        "value": "+993"
    }, {"key": "Tuvalu (+688)", "value": "+688"}, {"key": "Uganda (+256)", "value": "+256"}, {
        "key": "Ukraine (+380)",
        "value": "+380"
    }, {"key": "United Arab Emirates (+971)", "value": "+971"}, {
        "key": "United Kingdom (+44)",
        "value": "+44"
    }, {"key": "United States (+1)", "value": "+1"}, {
        "key": "Uruguay (+598)",
        "value": "+598"
    }, {"key": "Uzbekistan (+998)", "value": "+998"}, {
        "key": "Vanuatu (+678)",
        "value": "+678"
    }, {"key": "Vatican City (+379)", "value": "+379"}, {"key": "Venezuela (+58)", "value": "+58"}, {
        "key": "Vietnam (+84)",
        "value": "+84"
    }, {"key": "Yemen (+967)", "value": "+967"}, {"key": "Zambia (+260)", "value": "+260"}, {
        "key": "Zimbabwe (+263)",
        "value": "+263"
    }];

const selectedCountryCode = ref<DropDownValue | null>(countriesWithPhoneCodes[0]);

/* endregion */

</script>