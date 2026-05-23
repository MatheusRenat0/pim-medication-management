<template>
  <aside class="sidebar" :class="{ collapsed }">
    <div class="sidebar-header">
      <div class="logo" @click="$router.push('/dashboard')">
      <div class="logo-icon">
          <img src="/logo.png" alt="MedFlow" style="width:28px;height:28px;object-fit:contain;" />
        </div>
        <span v-if="!collapsed" class="logo-text">MedFlow</span>
      </div>
      <button class="collapse-btn" @click="$emit('toggle')">
        <svg width="18" height="18" viewBox="0 0 18 18" fill="none"><path :d="collapsed?'M6 4l6 5-6 5':'M12 4L6 9l6 5'" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/></svg>
      </button>
    </div>

    <nav class="sidebar-nav">
      <div class="nav-section" v-for="section in filteredMenu" :key="section.label">
        <span v-if="!collapsed" class="nav-section-label">{{ section.label }}</span>
        <router-link v-for="item in section.items" :key="item.to" :to="item.to" class="nav-item" :class="{ active: $route.path === item.to }">
          <span class="nav-icon" v-html="item.icon"></span>
          <span v-if="!collapsed" class="nav-label">{{ item.name }}</span>
        </router-link>
      </div>
    </nav>

    <div class="sidebar-footer">
      <button class="nav-item logout-btn" @click="handleLogout">
        <span class="nav-icon"><svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9 21H5a2 2 0 01-2-2V5a2 2 0 012-2h4"/><polyline points="16 17 21 12 16 7"/><line x1="21" y1="12" x2="9" y2="12"/></svg></span>
        <span v-if="!collapsed" class="nav-label">Sair</span>
      </button>
    </div>
  </aside>

  <!-- Mobile overlay -->
  <div v-if="mobileOpen" class="sidebar-overlay" @click="$emit('toggle')"></div>
</template>

<script setup>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../../stores/auth'

const props = defineProps({ collapsed: Boolean })
defineEmits(['toggle'])

const router = useRouter()
const auth = useAuthStore()
const mobileOpen = computed(() => false)

const iconDashboard = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="7" height="7"/><rect x="14" y="3" width="7" height="7"/><rect x="14" y="14" width="7" height="7"/><rect x="3" y="14" width="7" height="7"/></svg>'
const iconUsers = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M17 21v-2a4 4 0 00-4-4H5a4 4 0 00-4-4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 00-3-3.87"/><path d="M16 3.13a4 4 0 010 7.75"/></svg>'
const iconMed = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M10.5 1.5H8.25A2.25 2.25 0 006 3.75v16.5a2.25 2.25 0 002.25 2.25h7.5A2.25 2.25 0 0018 20.25V3.75a2.25 2.25 0 00-2.25-2.25H13.5m-3 0V3h3V1.5m-3 0h3m-3 18.75h3"/></svg>'
const iconStock = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 16V8a2 2 0 00-1-1.73l-7-4a2 2 0 00-2 0l-7 4A2 2 0 003 8v8a2 2 0 001 1.73l7 4a2 2 0 002 0l7-4A2 2 0 0021 16z"/><polyline points="3.27 6.96 12 12.01 20.73 6.96"/><line x1="12" y1="22.08" x2="12" y2="12"/></svg>'
const iconReceita = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14 2H6a2 2 0 00-2 2v16a2 2 0 002 2h12a2 2 0 002-2V8z"/><polyline points="14 2 14 8 20 8"/><line x1="16" y1="13" x2="8" y2="13"/><line x1="16" y1="17" x2="8" y2="17"/></svg>'
const iconTrat = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M22 12h-4l-3 9L9 3l-3 9H2"/></svg>'
const iconPedido = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="9" cy="21" r="1"/><circle cx="20" cy="21" r="1"/><path d="M1 1h4l2.68 13.39a2 2 0 002 1.61h9.72a2 2 0 002-1.61L23 6H6"/></svg>'
const iconEntrega = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="1" y="3" width="15" height="13"/><polygon points="16 8 20 8 23 11 23 16 16 16 16 8"/><circle cx="5.5" cy="18.5" r="2.5"/><circle cx="18.5" cy="18.5" r="2.5"/></svg>'
const iconReport = '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="18" y1="20" x2="18" y2="10"/><line x1="12" y1="20" x2="12" y2="4"/><line x1="6" y1="20" x2="6" y2="14"/></svg>'

const menuSections = [
  { label: 'Principal', items: [
    { name: 'Dashboard', to: '/dashboard', icon: iconDashboard, roles: ['all'] },
  ]},
  { label: 'Cadastros', items: [
    { name: 'Usuários', to: '/usuarios', icon: iconUsers, roles: ['Administrador'] },
    { name: 'Medicamentos', to: '/medicamentos', icon: iconMed, roles: ['Administrador','Funcionario'] },
    { name: 'Estoque', to: '/estoque', icon: iconStock, roles: ['Administrador','Funcionario'] },
  ]},
  { label: 'Operações', items: [
    { name: 'Receitas', to: '/receitas', icon: iconReceita, roles: ['all'] },
    { name: 'Tratamentos', to: '/tratamentos', icon: iconTrat, roles: ['all'] },
    { name: 'Pedidos', to: '/pedidos', icon: iconPedido, roles: ['Administrador','Funcionario'] },
    { name: 'Entregas', to: '/entregas', icon: iconEntrega, roles: ['all'] },
  ]},
  { label: 'Análises', items: [
    { name: 'Relatórios', to: '/relatorios', icon: iconReport, roles: ['Administrador','Funcionario'] },
  ]},
]

const filteredMenu = computed(() => {
  const tipo = auth.userType
  return menuSections.map(section => ({
    ...section,
    items: section.items.filter(item => item.roles.includes('all') || item.roles.includes(tipo))
  })).filter(section => section.items.length > 0)
})

const handleLogout = () => {
  auth.logout()
  router.push('/login')
}
</script>

<style scoped>
.sidebar { position: fixed; top: 0; left: 0; bottom: 0; width: 260px; background: var(--sidebar-bg); display: flex; flex-direction: column; z-index: 300; transition: width 0.3s ease; overflow: hidden; }
.sidebar.collapsed { width: 72px; }

.sidebar-header { display: flex; align-items: center; justify-content: space-between; padding: 20px 16px 16px; }
.logo { display: flex; align-items: center; gap: 10px; cursor: pointer; text-decoration: none; }
.logo-icon { width: 36px; height: 36px; background: var(--primary); border-radius: 10px; display: flex; align-items: center; justify-content: center; flex-shrink: 0; }
.logo-text { font-size: 18px; font-weight: 700; color: #fff; letter-spacing: -0.3px; white-space: nowrap; }
.collapse-btn { background: none; border: none; color: var(--sidebar-text); cursor: pointer; padding: 6px; border-radius: 6px; display: flex; transition: all var(--transition); }
.collapse-btn:hover { background: rgba(255,255,255,0.1); color: #fff; }
.collapsed .collapse-btn { display: none; }

.sidebar-nav { flex: 1; overflow-y: auto; padding: 8px 12px; }
.nav-section { margin-bottom: 8px; }
.nav-section-label { font-size: 11px; font-weight: 700; text-transform: uppercase; letter-spacing: 0.08em; color: rgba(147,180,245,0.5); padding: 12px 12px 6px; display: block; }

.nav-item { display: flex; align-items: center; gap: 12px; padding: 10px 12px; border-radius: var(--radius); color: var(--sidebar-text); text-decoration: none; font-size: 14px; font-weight: 500; transition: all var(--transition); cursor: pointer; border: none; background: none; width: 100%; font-family: var(--font); }
.nav-item:hover { background: rgba(255,255,255,0.08); color: #fff; }
.nav-item.active { background: var(--sidebar-active); color: #fff; font-weight: 600; }
.nav-icon { width: 20px; height: 20px; flex-shrink: 0; display: flex; align-items: center; justify-content: center; }
.nav-label { white-space: nowrap; }

.sidebar-footer { padding: 12px; border-top: 1px solid rgba(255,255,255,0.08); }
.logout-btn { color: rgba(255,255,255,0.5); }
.logout-btn:hover { color: var(--error-border); background: rgba(239,68,68,0.1); }

.sidebar-overlay { display: none; }

@media (max-width: 768px) {
  .sidebar { transform: translateX(-100%); width: 260px !important; }
  .sidebar:not(.collapsed) { transform: translateX(0); }
  .sidebar-overlay { display: block; position: fixed; inset: 0; background: rgba(0,0,0,0.4); z-index: 299; }
}
</style>
