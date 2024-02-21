import './assets/main.css'

import {createApp} from 'vue'
import App from './App.vue'
import router from "@/infrastructure/router";
import {AppThemeService} from "@/infrastructure/services/AppThemeService";
import {createPinia} from "pinia";
import {AuthenticationService} from "@/modules/profile/services/AuthenticationService";

const app = createApp(App);
const pinia = createPinia();


app.use(pinia);
app.use(router);

// Set app theme
const appThemeService = new AppThemeService();
appThemeService.setAppTheme();

// Set current user
const authService = new AuthenticationService();
await authService.setCurrentUser();

app.mount('#app');