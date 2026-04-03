import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import InventoryView from '../views/InventoryView.vue'

const routes = [
  { path: '/', name: 'home', component: HomeView },
  { path: '/estoque', name: 'estoque', component: InventoryView },

  { 
    path: '/setup-box', 
    name: 'setup-box', 
    component: () => import('../views/SetupBoxView.vue') },

  { path: '/login', 
    name: 'login', 
    component: () => import('../views/LoginView.vue') },

  { path: '/cadastro', 
    name: 'cadastro', 
    component: () => import('../views/RegisterView.vue') }
  
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router