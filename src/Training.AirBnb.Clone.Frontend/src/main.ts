import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import { AppThemeService } from "@/infrastructure/services/AppThemeService";

// createApp(App).mount('#app')
const appThemeService = new AppThemeService();

const app = createApp(App);

appThemeService.setAppTheme();
console.log("main.ts file: ", appThemeService.isDarkMode())

app.mount('#app')