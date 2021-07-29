import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async GetAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.allKeeps = res.data
  }

  async GetKeepById(kid) {
    const res = await api.get('api/keeps/' + kid)
    AppState.activeKeep = res.data
  }

  async DeleteKeep(kid) {
    await api.delete('api/keeps/' + kid)
    AppState.allKeeps = AppState.allKeeps.filter(k => k.id !== kid)
  }

  async CreateKeep(newKeep) {
    const res = await api.post('api/keeps', newKeep)
    AppState.allKeeps.push(res.data)
    AppState.profileKeeps.push(res.data)
  }

  loading() {
    AppState.loading = true
    AppState.loaded = false
    setTimeout(this.loaded, 6000)
  }

  loaded() {
    AppState.loading = false
    AppState.loaded = true
  }
}

export const keepsService = new KeepsService()
