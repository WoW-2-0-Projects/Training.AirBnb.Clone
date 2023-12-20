import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import index from './infrastructure/router/index'

const app = createApp(App)

app.use(createPinia())
app.use(index)

app.mount('#app')
