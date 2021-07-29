<template>
  <div class="modal fade" id="CreateKeep" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
        <div class="modal-body">
          <form @submit="CreateKeep">
            <label for="name">Keep Name</label>
            <input required v-model="state.newKeep.name" class="form-control" type="text">
            <label for="description">Description</label>
            <input v-model="state.newKeep.description" class="form-control" type="text">
            <label for="img">Image Url</label>
            <input required v-model="state.newKeep.img" class="form-control" type="text">
            <button class="btn btn-success">
              Create keep
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
export default {
  setup() {
    const state = reactive({
      newKeep: {
        name: '',
        description: '',
        img: ''
      }
    })
    return {
      state,
      async CreateKeep() {
        $('#CreateKeep').modal('hide')
        await keepsService.CreateKeep(state.newKeep)
      }
    }
  }
}
</script>

<style>

</style>
