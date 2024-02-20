import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from "@/infrastructure/router";
import { AppThemeService } from "@/infrastructure/services/AppThemeService";
import {createPinia} from "pinia";

const appThemeService = new AppThemeService();

const app = createApp(App);
const pinia = createPinia();

app.use(pinia);
app.use(router);

appThemeService.setAppTheme();

app.mount('#app');