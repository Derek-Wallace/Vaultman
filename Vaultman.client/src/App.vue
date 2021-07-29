<template>
  <header>
    <Navbar />
  </header>
  <main class="home-bg">
    <router-view />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
.home-bg{
  background-color: rgb(159, 162, 175);
}
</style>
