import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '../stores/auth'
import HomeView from '../views/HomeView.vue'

const routes = [
  { path: '/', name: 'home', component: HomeView, meta: { public: true } },
  { path: '/login', name: 'login', component: () => import('../views/auth/LoginView.vue'), meta: { public: true } },
  { path: '/cadastro', name: 'cadastro', component: () => import('../views/auth/RegisterView.vue'), meta: { public: true } },
  { path: '/dashboard', name: 'dashboard', component: () => import('../views/dashboard/DashboardView.vue'), meta: { title: 'Dashboard', requiresAuth: true, roles: ['Administrador', 'Funcionario'] } },
  { path: '/usuarios', name: 'usuarios', component: () => import('../views/usuarios/UsuariosView.vue'), meta: { title: 'Usuários', requiresAuth: true, roles: ['Administrador'] } },
  { path: '/medicamentos', name: 'medicamentos', component: () => import('../views/medicamentos/MedicamentosView.vue'), meta: { title: 'Medicamentos', requiresAuth: true, roles: ['Administrador', 'Funcionario'] } },
  { path: '/estoque', name: 'estoque', component: () => import('../views/estoque/EstoqueView.vue'), meta: { title: 'Estoque', requiresAuth: true, roles: ['Administrador', 'Funcionario'] } },
  { path: '/receitas', name: 'receitas', component: () => import('../views/receitas/ReceitasView.vue'), meta: { title: 'Receitas', requiresAuth: true } },
  { path: '/tratamentos', name: 'tratamentos', component: () => import('../views/tratamentos/TratamentosView.vue'), meta: { title: 'Tratamentos', requiresAuth: true } },
  { path: '/pedidos', name: 'pedidos', component: () => import('../views/pedidos/PedidosView.vue'), meta: { title: 'Pedidos', requiresAuth: true, roles: ['Administrador', 'Funcionario'] } },
  { path: '/entregas', name: 'entregas', component: () => import('../views/entregas/EntregasView.vue'), meta: { title: 'Entregas', requiresAuth: true } },
  { path: '/relatorios', name: 'relatorios', component: () => import('../views/relatorios/RelatoriosView.vue'), meta: { title: 'Relatórios', requiresAuth: true, roles: ['Administrador', 'Funcionario'] } },
  { path: '/setup-box', name: 'setup-box', component: () => import('../views/SetupBoxView.vue'), meta: { public: true } },
  { path: '/termos', name: 'termos', component: () => import('../views/TermosView.vue'), meta: { public: true } },
  { path: '/privacidade', name: 'privacidade', component: () => import('../views/PrivacidadeView.vue'), meta: { public: true } },
  { path: '/contato', name: 'contato', component: () => import('../views/ContatoView.vue'), meta: { public: true } },
  { path: '/:pathMatch(.*)*', redirect: '/' }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const auth = useAuthStore()
  if (to.meta.requiresAuth && !auth.isAuthenticated) {
    return next('/login')
  }
  if (to.meta.roles && !to.meta.roles.includes(auth.userType)) {
    return next(auth.userType === 'Paciente' ? '/receitas' : auth.userType === 'Entregador' ? '/entregas' : '/dashboard')
  }
  if ((to.name === 'login' || to.name === 'cadastro') && auth.isAuthenticated) {
    return next(auth.userType === 'Paciente' ? '/receitas' : auth.userType === 'Entregador' ? '/entregas' : '/dashboard')
  }
  next()
})

export default router