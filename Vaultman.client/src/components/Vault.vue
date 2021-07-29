<template>
  <div role="button" @click="SetActiveVault(vault.id)" class="col-lg-2 col-5 m-1 vault rounded border shadow d-flex justify-content-center align-items-center text-light">
    {{ vault.name.substring(0,15) }}
  </div>
</template>

<script>
import { router } from '../router'
import { vaultsService } from '../services/VaultsService'
export default {
  props: { vault: { type: Object, required: true } },
  setup() {
    return {
      async SetActiveVault(vid) {
        try {
          await vaultsService.GetVaultById(vid)
          router.push({ path: '/vault/' + vid })
        } catch (error) {
          window.alert(error)
        }
      }
    }
  }
}
</script>

<style>
.vault {
  height: 100px;
  width: 100px;
  background-color: rgb(38, 49, 99);
}
</style>
