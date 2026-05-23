import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5205/api',
  headers: { 'Content-Type': 'application/json' }
})

// RF03 – Envia o ID do usuário logado em toda requisição para o backend verificar o perfil
api.interceptors.request.use((config) => {
  const userData = localStorage.getItem('medflow_user')
  if (userData) {
    const user = JSON.parse(userData)
    if (user?.usuarioId) {
      config.headers['X-User-Id'] = user.usuarioId
    }
  }
  return config
})

api.interceptors.response.use(
  response => response,
  error => {
    const msg = error.response?.data?.mensagem || error.response?.data?.message || error.message || 'Erro de conexão com o servidor.'
    return Promise.reject({ message: msg, status: error.response?.status })
  }
)

export function useApi() {
  const get = (url) => api.get(url).then(r => r.data)
  const post = (url, data) => api.post(url, data).then(r => r.data)
  const put = (url, data) => api.put(url, data).then(r => r.data)
  const del = (url) => api.delete(url).then(r => r.data)
  const upload = (url, formData) => api.post(url, formData, { headers: { 'Content-Type': 'multipart/form-data' } }).then(r => r.data)

  return { get, post, put, del, upload, api }
}
