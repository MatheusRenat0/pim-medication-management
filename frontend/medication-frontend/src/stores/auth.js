import { defineStore } from 'pinia'
import { useApi } from '../composables/useApi'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: JSON.parse(localStorage.getItem('medflow_user') || 'null'),
  }),
  getters: {
    isAuthenticated: (state) => !!state.user,
    userName: (state) => state.user?.nome || '',
    userType: (state) => state.user?.tipo || '',
    userId: (state) => state.user?.usuarioId || null,
    isAdmin: (state) => state.user?.tipo === 'Administrador',
    isFuncionario: (state) => state.user?.tipo === 'Funcionario' || state.user?.tipo === 'Administrador',
  },
  actions: {
    async login(email, senha) {
      const { post } = useApi()
      const data = await post('/Usuario/login', { email, senha })
      this.user = { usuarioId: data.usuarioId, nome: data.nome, tipo: data.tipo }
      localStorage.setItem('medflow_user', JSON.stringify(this.user))
      return data
    },
    async register(nome, email, cpf, senha) {
      const { post } = useApi()
      const data = await post('/Usuario/cadastrar', { nome, email, cpf, senha })
      this.user = { usuarioId: data.usuarioId, nome: data.nome, tipo: data.tipo }
      localStorage.setItem('medflow_user', JSON.stringify(this.user))
      return data
    },
    logout() {
      this.user = null
      localStorage.removeItem('medflow_user')
    }
  }
})
