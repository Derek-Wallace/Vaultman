import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfilesService {
  async GetProfileById(pid) {
    const res = await api.get('api/profiles/' + pid)
    AppState.activeProfile = res.data
    this.GetVaultsByProfile(pid)
    this.GetKeepsByProfile(pid)
  }

  async setUserVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.userVaults = res.data
  }

  async GetKeepsByProfile(pid) {
    const res = await api.get(`api/profiles/${pid}/keeps`)
    AppState.profileKeeps = res.data
  }

  async GetVaultsByProfile(pid) {
    const res = await api.get(`api/profiles/${pid}/vaults`)
    AppState.profileVaults = res.data
  }
}

export const profilesService = new ProfilesService()
