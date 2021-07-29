<template>
  <div class="modal fade" id="CreateVault" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-body">
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
          <form @submit.prevent="CreateVault()">
            <label for="name">Vault Name:</label>
            <input class="form-control" type="text" required v-model="state.newVault.name">
            <label for="isPrivate">Private?</label>
            <input class="form-control" type="checkbox" v-model="state.newVault.isPrivate">
            <button class="btn btn-success">
              Create Vault
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'
export default {
  setup() {
    const state = reactive({
      newVault: {
        name: '',
        isPrivate: false
      }
    })
    return {
      state,
      async CreateVault() {
        $('#CreateVault').modal('hide')
        await vaultsService.CreateVault(state.newVault)
      }
    }
  }
}
</script>

<style>

</style>
