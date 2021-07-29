import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  allKeeps: [],
  activeKeep: null,
  activeVault: null,
  activeProfile: null,
  profileKeeps: [],
  profileVaults: [],
  userVaults: [],
  vaultKeeps: [],
  loading: true,
  loaded: false
})
