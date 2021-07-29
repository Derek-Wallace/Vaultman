import 'bootstrap'
import 'jquery'
import 'popper.js'
import { createApp } from 'vue'
// @ts-ignore
import App from './App.vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import { router } from './router'
import mitt from 'mitt'
import { VueMasonryPlugin } from 'vue-masonry/src/masonry-vue3.plugin'

const emitter = mitt()
const root = createApp(App)
root.config.globalProperties.emitter = emitter
root.use(VueMasonryPlugin)
registerGlobalComponents(root)

root
  .use(router)
  .mount('#app')
