<template>
  <div class="entregador-container animate-in">
    <!-- Header info -->
    <div class="mobile-header">
      <div class="header-main">
        <div>
          <span class="welcome-label">Painel do Entregador</span>
          <h1 class="header-name">{{ auth.userName }}</h1>
        </div>
        <div class="status-indicator">
          <span class="pulse-dot"></span> Online
        </div>
      </div>
      <div class="stats-mini">
        <div class="stat-box">
          <span class="stat-num">{{ stats.pendentes }}</span>
          <span class="stat-label">Pendentes</span>
        </div>
        <div class="stat-box">
          <span class="stat-num">{{ stats.emRota }}</span>
          <span class="stat-label">Em Rota</span>
        </div>
        <div class="stat-box">
          <span class="stat-num">{{ stats.concluidas }}</span>
          <span class="stat-label">Concluídas</span>
        </div>
      </div>
    </div>

    <!-- Map Routing Simulation -->
    <div v-if="rotaAtiva" class="map-card card animate-in">
      <div class="card-header-row">
        <h3 class="card-title">Rota Otimizada de Entrega</h3>
        <span class="badge badge-info">Em Rota</span>
      </div>
      <p class="map-desc">Destino atual: <strong>{{ rotaAtiva.enderecoDestino }}</strong></p>
      
      <!-- Visual route polyline simulation -->
      <div class="routing-sim">
        <svg viewBox="0 0 400 150" class="route-svg">
          <!-- Road/Path -->
          <path d="M 30,75 C 100,20 150,130 220,75 C 290,20 340,90 370,75" stroke="#e2e8f0" stroke-width="12" fill="none" stroke-linecap="round"/>
          <path d="M 30,75 C 100,20 150,130 220,75 C 290,20 340,90 370,75" stroke="#3b82f6" stroke-width="4" fill="none" stroke-linecap="round" stroke-dasharray="8,6" class="dash-anim"/>
          <!-- Starting point (MedFlow Hub) -->
          <circle cx="30" cy="75" r="8" fill="#1d4ed8"/>
          <text x="25" y="95" class="map-label">Hub</text>
          <!-- Current Delivery Dot -->
          <circle cx="220" cy="75" r="8" fill="#10b981" class="pulse-ring"/>
          <text x="200" y="98" class="map-label green">Cliente</text>
          <!-- Delivery Truck Icon along path -->
          <g transform="translate(110, 52)">
            <rect x="0" y="0" width="16" height="10" rx="2" fill="#1d4ed8"/>
            <circle cx="4" cy="11" r="2" fill="#000"/>
            <circle cx="12" cy="11" r="2" fill="#000"/>
          </g>
        </svg>
        <div class="routing-nav-instructions">
          <span class="nav-icon">↱</span>
          <span>Siga na rota calculada para o destino. Trânsito livre.</span>
        </div>
      </div>
    </div>

    <!-- Tab navigation -->
    <div class="tabs-navigation">
      <button class="nav-tab" :class="{ active: tabAtiva === 'pendente' }" @click="tabAtiva = 'pendente'">
        Aguardando ({{ entregasFiltradas('Pendente').length }})
      </button>
      <button class="nav-tab" :class="{ active: tabAtiva === 'rota' }" @click="tabAtiva = 'rota'">
        Em Rota ({{ entregasFiltradas('Saiu para Entrega').length }})
      </button>
      <button class="nav-tab" :class="{ active: tabAtiva === 'concluida' }" @click="tabAtiva = 'concluida'">
        Concluídas ({{ entregasFiltradas('Entregue').length }})
      </button>
    </div>

    <!-- Deliveries List -->
    <div class="deliveries-list">
      <div v-for="e in entregasFiltradasTab" :key="e.id" class="delivery-mobile-card card animate-in">
        <div class="card-top">
          <span class="delivery-id">Entrega #{{ e.id }}</span>
          <span class="badge" :class="statusBadge(e.statusLogistico)">{{ e.statusLogistico }}</span>
        </div>
        <div class="card-mid">
          <div class="info-row">
            <span class="label">Pedido:</span>
            <strong>#{{ e.pedidoId }}</strong>
          </div>
          <div class="info-row">
            <span class="label">Destino:</span>
            <span class="address-text">{{ e.enderecoDestino }}</span>
          </div>
          <div class="info-row">
            <span class="label">Previsão:</span>
            <span>{{ formatDate(e.dataPrevista) }}</span>
          </div>
          <div v-if="e.dataRealizada" class="info-row">
            <span class="label">Realizada:</span>
            <span class="green-text">{{ formatDate(e.dataRealizada) }}</span>
          </div>
        </div>
        <div class="card-actions">
          <button v-if="e.statusLogistico === 'Pendente'" class="btn btn-primary btn-block btn-lg" @click="iniciarRota(e)" :disabled="processando === e.id">
            🚚 Iniciar Rota
          </button>
          <button v-if="e.statusLogistico === 'Saiu para Entrega'" class="btn btn-success btn-block btn-lg" @click="confirmarEntrega(e)" :disabled="processando === e.id">
            ✓ Confirmar Entrega
          </button>
          <button class="btn btn-outline btn-block" @click="simularNavegacao(e)">
            📍 Ver Rota Otimizada
          </button>
        </div>
      </div>
      
      <div v-if="entregasFiltradasTab.length === 0" class="empty-state">
        <span class="empty-icon">📭</span>
        <p>Nenhuma entrega nesta lista.</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useApi } from '../../composables/useApi'
import { useToast } from '../../composables/useToast'
import { useAuthStore } from '../../stores/auth'

const { get, api } = useApi()
const toast = useToast()
const auth = useAuthStore()

const entregas = ref([])
const tabAtiva = ref('pendente')
const processando = ref(null)
const rotaAtiva = ref(null)

const stats = computed(() => {
  const p = entregas.value.filter(e => e.statusLogistico === 'Pendente').length
  const r = entregas.value.filter(e => e.statusLogistico === 'Saiu para Entrega').length
  const c = entregas.value.filter(e => e.statusLogistico === 'Entregue').length
  return { pendentes: p, emRota: r, concluidas: c }
})

const formatDate = (d) => {
  if (!d) return '-'
  const date = new Date(d)
  return date.toLocaleString('pt-BR', { dateStyle: 'short', timeStyle: 'short' })
}

function statusBadge(s) {
  if (s === 'Entregue') return 'badge-success'
  if (s === 'Saiu para Entrega') return 'badge-info'
  if (s === 'Atrasado') return 'badge-error'
  return 'badge-warning'
}

const entregasFiltradas = (status) => {
  return entregas.value.filter(e => e.statusLogistico === status)
}

const entregasFiltradasTab = computed(() => {
  if (tabAtiva.value === 'pendente') return entregasFiltradas('Pendente')
  if (tabAtiva.value === 'rota') return entregasFiltradas('Saiu para Entrega')
  return entregasFiltradas('Entregue')
})

const carregar = async () => {
  try {
    entregas.value = await get(`/Entrega/Entregador/${auth.userId}`)
    // Se houver alguma em rota, ativa a simulação dela
    const emRota = entregas.value.find(e => e.statusLogistico === 'Saiu para Entrega')
    if (emRota) rotaAtiva.value = emRota
  } catch (e) {
    console.error(e)
  }
}

const iniciarRota = async (entrega) => {
  processando.value = entrega.id
  try {
    await api.put(`/Entrega/status/${entrega.id}`, JSON.stringify('Saiu para Entrega'), {
      headers: { 'Content-Type': 'application/json' }
    })
    toast.success('Rota iniciada! Status atualizado para "Em Rota".')
    rotaAtiva.value = entrega
    tabAtiva.value = 'rota'
    await carregar()
  } catch (e) {
    toast.error(e.message || 'Erro ao iniciar rota.')
  } finally {
    processando.value = null
  }
}

const confirmarEntrega = async (entrega) => {
  processando.value = entrega.id
  try {
    await api.put(`/Entrega/status/${entrega.id}`, JSON.stringify('Entregue'), {
      headers: { 'Content-Type': 'application/json' }
    })
    toast.success('Entrega confirmada com sucesso!')
    if (rotaAtiva.value?.id === entrega.id) rotaAtiva.value = null
    tabAtiva.value = 'concluida'
    await carregar()
  } catch (e) {
    toast.error(e.message || 'Erro ao confirmar entrega.')
  } finally {
    processando.value = null
  }
}

const simularNavegacao = (entrega) => {
  rotaAtiva.value = entrega
  toast.info('Simulando rota no mapa para esta entrega.')
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

onMounted(carregar)
</script>

<style scoped>
.entregador-container {
  max-width: 480px;
  margin: 0 auto;
  padding: 16px;
  font-family: 'Inter', sans-serif;
}

.mobile-header {
  background: linear-gradient(135deg, #1e3a8a 0%, #1d4ed8 100%);
  color: #fff;
  border-radius: 20px;
  padding: 20px;
  box-shadow: 0 10px 25px rgba(30, 58, 138, 0.15);
  margin-bottom: 20px;
}

.header-main {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 20px;
}

.welcome-label {
  font-size: 11px;
  text-transform: uppercase;
  letter-spacing: 1px;
  opacity: 0.8;
}

.header-name {
  font-size: 20px;
  font-weight: 700;
  margin-top: 2px;
  font-family: Georgia, serif;
}

.status-indicator {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  background: rgba(16, 185, 129, 0.15);
  border: 1px solid rgba(16, 185, 129, 0.3);
  color: #34d399;
  font-size: 12px;
  font-weight: 600;
  padding: 4px 10px;
  border-radius: 100px;
}

.pulse-dot {
  width: 8px;
  height: 8px;
  background-color: #10b981;
  border-radius: 50%;
  animation: pulse 1.8s infinite;
}

@keyframes pulse {
  0% { transform: scale(0.9); box-shadow: 0 0 0 0 rgba(16, 185, 129, 0.7); }
  70% { transform: scale(1); box-shadow: 0 0 0 6px rgba(16, 185, 129, 0); }
  100% { transform: scale(0.9); box-shadow: 0 0 0 0 rgba(16, 185, 129, 0); }
}

.stats-mini {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  background: rgba(255, 255, 255, 0.08);
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 12px;
  padding: 12px;
  text-align: center;
}

.stat-box {
  display: flex;
  flex-direction: column;
}

.stat-box:not(:last-child) {
  border-right: 1px solid rgba(255, 255, 255, 0.15);
}

.stat-num {
  font-size: 18px;
  font-weight: 800;
}

.stat-label {
  font-size: 11px;
  opacity: 0.7;
  margin-top: 2px;
}

/* Map Sim Card */
.map-card {
  margin-bottom: 20px;
  border-color: #bfdbfe;
  background: #f8fafc;
}

.card-header-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12px;
}

.card-title {
  font-size: 14px;
  font-weight: 700;
  color: #1e3a8a;
  font-family: Georgia, serif;
}

.map-desc {
  font-size: 13px;
  color: #374151;
  margin-bottom: 14px;
}

.routing-sim {
  background: #fff;
  border: 1.5px solid #e2e8f0;
  border-radius: 12px;
  padding: 12px;
  overflow: hidden;
}

.route-svg {
  width: 100%;
  height: auto;
  max-height: 120px;
}

.dash-anim {
  stroke-dasharray: 8, 6;
  animation: routeDash 25s linear infinite;
}

@keyframes routeDash {
  to { stroke-dashoffset: -200; }
}

.pulse-ring {
  animation: mapPulse 2s infinite;
}

@keyframes mapPulse {
  0% { r: 6; opacity: 1; }
  50% { r: 10; opacity: 0.6; }
  100% { r: 6; opacity: 1; }
}

.map-label {
  font-size: 10px;
  font-weight: 700;
  fill: #6b7280;
}

.map-label.green {
  fill: #10b981;
}

.routing-nav-instructions {
  display: flex;
  gap: 8px;
  align-items: center;
  background: #eff6ff;
  border: 1px solid #bfdbfe;
  border-radius: 8px;
  padding: 10px;
  margin-top: 10px;
  font-size: 12px;
  color: #1e40af;
  font-weight: 500;
}

.nav-icon {
  font-size: 16px;
  font-weight: 700;
}

/* Tabs */
.tabs-navigation {
  display: flex;
  background: #f1f5f9;
  padding: 4px;
  border-radius: 12px;
  margin-bottom: 16px;
  border: 1px solid #e2e8f0;
}

.nav-tab {
  flex: 1;
  border: none;
  background: none;
  padding: 10px 4px;
  font-size: 12px;
  font-weight: 600;
  color: #475569;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.15s;
  font-family: inherit;
}

.nav-tab.active {
  background: #fff;
  color: #1d4ed8;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
}

/* Delivery Cards */
.deliveries-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.delivery-mobile-card {
  padding: 18px;
  border: 1.5px solid var(--border);
  transition: transform 0.15s, box-shadow 0.15s;
}

.delivery-mobile-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.05);
}

.card-top {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-bottom: 12px;
  border-bottom: 1px solid #e2e8f0;
  margin-bottom: 12px;
}

.delivery-id {
  font-size: 14px;
  font-weight: 700;
  color: #1e3a8a;
}

.card-mid {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-bottom: 16px;
}

.info-row {
  display: flex;
  justify-content: space-between;
  font-size: 13px;
  color: #374151;
}

.info-row .label {
  color: #6b7280;
  font-weight: 500;
}

.address-text {
  text-align: right;
  max-width: 70%;
  font-weight: 500;
}

.green-text {
  color: #10b981;
  font-weight: 600;
}

.card-actions {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.btn-block {
  width: 100%;
}

.btn-lg {
  padding: 12px 20px;
  font-size: 14px;
}

.empty-state {
  text-align: center;
  padding: 40px 20px;
  color: #94a3b8;
}

.empty-icon {
  font-size: 32px;
  margin-bottom: 8px;
  display: block;
}
</style>
