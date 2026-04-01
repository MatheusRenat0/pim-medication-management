import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import InventoryView from '../views/InventoryView.vue'

const routes = [
  { path: '/', name: 'home', component: HomeView },
  { path: '/estoque', name: 'estoque', component: InventoryView },
  
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router