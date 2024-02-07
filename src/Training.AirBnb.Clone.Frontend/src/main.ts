import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from "@/infrastructure/router";
import { AppThemeService } from "@/infrastructure/services/AppThemeService";

const appThemeService = new AppThemeService();

const app = createApp(App);

app.use(router);

appThemeService.setAppTheme();

app.mount('#app')