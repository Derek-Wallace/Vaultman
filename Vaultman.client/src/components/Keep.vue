<template>
  <div class="col-lg-2 col-6">
    <div>
      <img role="button"
           v-if="keep.vaultKeepId"
           data-toggle="modal"
           data-target="#activeKeep"
           :src="keep.img"
           class="img-fluid m-2 rounded shadow keep"
           alt="keep"
           @click="activateVaultKeep(keep)"
      >
      <img role="button"
           v-else
           data-toggle="modal"
           data-target="#activeKeep"
           :src="keep.img"
           class="img-fluid m-2 rounded shadow keep"
           alt="keep"
           @click="activateKeep(keep.id)"
      >
      <p class="info text-white">
        <img role="button"
             v-if="profile == null"
             :src="keep.creator.picture"
             @click="SetActiveProfile(keep.creatorId)"
             class="rounded-circle"
             height="30"
             alt="creator"
        > {{ keep.name.substring(0,15) }}
      </p>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { router } from '../router'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {
      profile: computed(() => AppState.activeProfile),
      async activateKeep(kid) {
        try {
          AppState.activeKeep = null
          await keepsService.GetKeepById(kid)
          await profilesService.setUserVaults(AppState.account.id)
        } catch (error) {
          window.alert(error)
        }
      },
      activateVaultKeep(keep) {
        AppState.activeKeep = keep
      },
      async SetActiveProfile(pid) {
        try {
          await profilesService.GetProfileById(pid)
          router.push({ path: '/profile/' + pid })
        } catch (error) {
          window.alert(error)
        }
      }
    }
  }
}
</script>

<style>
.info {
  position: absolute;
  bottom: 8px;
  left: 30px;
  -webkit-text-stroke: 1px black;
  font-weight:900;
  font-size: 20px;

}
.keep:hover{
  transform: scale(1.02);
  transition: 200ms;
}
</style>
