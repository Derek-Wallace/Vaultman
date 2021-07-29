<template>
  <div
    class="modal fade"
    id="activeKeep"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content container-fluid">
        <div class="modal-body d-flex row" v-if="keep != null">
          <div class="col-6">
            <img :src="keep.img" class="img-fluid" alt="keep">
          </div>
          <div class="col-6">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
            <div class="row mt-5">
              <div class="col-12">
                <h1>{{ keep.name }}</h1>
              </div>
              <div class="col-12 d-flex align-items-center">
                <img :src="keep.creator.picture"
                     role="button"
                     @click="SetActiveProfile(keep.creatorId)"
                     height="40"
                     class="rounded-circle mr-2"
                     alt="creator"
                >
                {{ keep.creator.name }}
              </div>
              <div class="col-12 mt-5 pb-5 border-bottom">
                <p>{{ keep.description }}</p>
              </div>
              <div class="col-12 mt-2">
                <div class="d-flex justify-content-around">
                  <p class="mdi mdi-eye" title="Views">
                    {{ keep.views }}
                  </p>
                  <p class="mdi mdi-safe-square-outline" title="Vaulted">
                    {{ keep.keeps }}
                  </p>
                  <p class="mdi mdi-share" title="Shares">
                    {{ keep.shares }}
                  </p>
                </div>
              </div>
              <div class="col-12 mt-2 d-flex align-items-center">
                <div class="dropdown">
                  <button class="btn btn-primary dropdown-toggle"
                          type="button"
                          id="dropdownMenuButton"
                          data-toggle="dropdown"
                          aria-haspopup="true"
                          aria-expanded="false"
                  >
                    Add to Vault
                  </button>
                  <button v-if="vault != null && vault.creatorId == account.id" @click="DeleteVaultKeep(keep.vaultKeepId)" class=" ml-2 btn btn-danger">
                    Remove from Vault
                  </button>
                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <a v-for="vault in userVaults" :key="vault.id" class="dropdown-item" @click="CreateVaultKeep(vault.id, keep.id)" href="#">{{ vault.name }}</a>
                  </div>
                </div>
                <div v-if="account.id == keep.creatorId" @click="DeleteKeep(keep.id)" role="button" class="mdi mdi-trash-can-outline delete ml-4 text-danger"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import { profilesService } from '../services/ProfilesService'
import { router } from '../router'
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      userVaults: computed(() => AppState.userVaults),
      keep: computed(() => AppState.activeKeep),
      async DeleteKeep(kid) {
        try {
          if (window.confirm('Are you sure you want to delete this?')) {
            $('#activeKeep').modal('hide')
            await keepsService.DeleteKeep(kid)
          }
        } catch (error) {
          window.alert(error)
        }
      },
      async SetActiveProfile(pid) {
        try {
          $('#activeKeep').modal('hide')
          await profilesService.GetProfileById(pid)
          router.push({ path: '/profile/' + pid })
        } catch (error) {
          window.alert(error)
        }
      },
      async CreateVaultKeep(vid, kid) {
        try {
          await vaultKeepsService.CreateVaultKeep(vid, kid)
        } catch (error) {
          window.alert(error)
        }
      },
      async DeleteVaultKeep(vkid) {
        try {
          if (window.confirm('Are you sure you want to delete this?')) {
            $('#activeKeep').modal('hide')
            await vaultKeepsService.DeleteVaultKeep(vkid)
          }
        } catch (error) {
          window.alert(error)
        }
      }
    }
  }
}
</script>

<style scoped>
.modal-dialog{
  max-width: 1000px;
}
.delete{
  font-size: 25px;
}
</style>
