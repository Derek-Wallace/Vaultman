<template>
  <div v-if="profile != null" class="container-fluid">
    <div class="row my-5">
      <div class="col-lg-2 col-6 d-flex justify-content-end mt-5">
        <img :src="profile.picture" alt="profile">
      </div>
      <div class="col-lg-10 col-6 pt-3 mt-5 text-light">
        <div class="titles">
          {{ profile.name }}
        </div>
        <div>Vaults: {{ vaults.length }}</div>
        <div>Keeps: {{ keeps.length }}</div>
      </div>
    </div>
    <div class="row d-flex justify-content-center" v-if="loading">
      <div class="lds-grid" v-show="loading">
        <div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div>
      </div>
    </div>
    <div class="row" v-show="loaded">
      <div class="col-lg-10 m-auto">
        <div class="ml-5 mb-3 text-light titles">
          Vaults: <i v-if="profile.id == account.id"
                     data-toggle="modal"
                     data-target="#CreateVault"
                     class="mdi mdi-plus color-green"
                     title="Create Vault"
                     role="button"
          ></i>
        </div>
      </div>
    </div>
    <div class="row ml-5" v-show="loaded">
      <Vault v-for="vault in vaults" :key="vault.id" :vault="vault" />
    </div>
    <div class="row mt-5" v-show="loaded">
      <div class="col-10 m-auto">
        <div class="ml-5 mb-3 text-light titles">
          Keeps: <i v-if="profile.id == account.id"
                    data-toggle="modal"
                    data-target="#CreateKeep"
                    class="mdi mdi-plus color-green"
                    title="Create Keep"
                    role="button"
          ></i>
        </div>
      </div>
    </div>
    <div class="row" v-masonry v-show="loaded">
      <Keep v-for="keep in keeps" :key="keep.id" :keep="keep" v-masonry-tile />
    </div>
  </div>
  <KeepModal />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed, onMounted, onUnmounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    const route = useRoute()
    onMounted(async() => {
      if (route.params.id) {
        keepsService.loading()
        await profilesService.GetProfileById(route.params.id)
      }
    })
    onUnmounted(() => {
      AppState.activeProfile = null
      AppState.profileKeeps = []
      AppState.profileVaults = []
      AppState.loading = true
      AppState.loaded = false
    })
    return {
      loading: computed(() => AppState.loading),
      loaded: computed(() => AppState.loaded),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults)
    }
  }
}
</script>

<style>
.color-green {
  color: #03ff03;
}
.titles {
  font-size: 30px;
  font-weight: 500;
}
.lds-grid {
  display: inline-block;
  position: relative;
  width: 80px;
  height: 80px;
}
.lds-grid div {
  position: absolute;
  width: 16px;
  height: 16px;
  border-radius: 50%;
  background: rgb(41, 41, 41);
  animation: lds-grid 1.2s linear infinite;
}
.lds-grid div:nth-child(1) {
  top: 8px;
  left: 8px;
  animation-delay: 0s;
}
.lds-grid div:nth-child(2) {
  top: 8px;
  left: 32px;
  animation-delay: -0.4s;
}
.lds-grid div:nth-child(3) {
  top: 8px;
  left: 56px;
  animation-delay: -0.8s;
}
.lds-grid div:nth-child(4) {
  top: 32px;
  left: 8px;
  animation-delay: -0.4s;
}
.lds-grid div:nth-child(5) {
  top: 32px;
  left: 32px;
  animation-delay: -0.8s;
}
.lds-grid div:nth-child(6) {
  top: 32px;
  left: 56px;
  animation-delay: -1.2s;
}
.lds-grid div:nth-child(7) {
  top: 56px;
  left: 8px;
  animation-delay: -0.8s;
}
.lds-grid div:nth-child(8) {
  top: 56px;
  left: 32px;
  animation-delay: -1.2s;
}
.lds-grid div:nth-child(9) {
  top: 56px;
  left: 56px;
  animation-delay: -1.6s;
}
@keyframes lds-grid {
  0%, 100% {
    opacity: 1;
  }
  50% {
    opacity: 0.5;
  }
}
</style>
