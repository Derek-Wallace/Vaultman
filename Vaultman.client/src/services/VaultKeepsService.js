import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultKeepsService {
  async CreateVaultKeep(vid, kid) {
    const newVaultKeep = { vaultId: vid, keepId: kid }
    await api.post('api/vaultkeeps', newVaultKeep)
    window.alert('success')
  }

  async DeleteVaultKeep(vkid) {
    await api.delete('api/vaultkeeps/' + vkid)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(v => v.id !== vkid)
  }
}

export const vaultKeepsService = new VaultKeepsService()
