<template>
  <div class="container-fluid" v-if="vault != null && vault.isPrivate != true">
    <div class="row mt-4">
      <div class="col-10 mx-auto mb-4">
        <h1>{{ vault.name }} <i v-if="vault.creatorId == account.id" role="button" @click="DeleteVault(vault.id)" class="mdi mdi-trash-can-outline text-danger"></i></h1>
        <h5>Keeps: {{ keeps.length }}</h5>
      </div>
    </div>
    <div class="row" v-masonry>
      <Keep v-for="keep in keeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
  <KeepModal />
</template>

<script>
import { computed, onMounted, onUnmounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { router } from '../router'
export default {
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        if (route.params.id) {
          await vaultsService.GetVaultById(route.params.id)
          await vaultsService.GetVaultKeeps(route.params.id)
        }
      } catch (error) {
        router.push({ path: '/' })
      }
    })
    onUnmounted(() => {
      AppState.activeVault = null
      AppState.vaultKeeps = []
    })
    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),
      async DeleteVault(vid) {
        if (window.confirm('Are you sure you want to delete this?')) {
          router.push({ path: '/' })
          await vaultsService.DeleteVault(vid)
        }
      }
    }
  }
}
</script>

<style>

</style>
