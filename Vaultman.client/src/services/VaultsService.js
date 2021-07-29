import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async GetVaultById(vid) {
    const res = await api.get('api/vaults/' + vid)
    AppState.activeVault = res.data
  }

  async GetVaultKeeps(vid) {
    const res = await api.get(`api/vaults/${vid}/keeps`)
    AppState.vaultKeeps = res.data
  }

  async CreateVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    AppState.profileVaults = res.data
  }

  async DeleteVault(vid) {
    await api.delete('api/vaults/' + vid)
    AppState.activeVault = null
  }
}

export const vaultsService = new VaultsService()
