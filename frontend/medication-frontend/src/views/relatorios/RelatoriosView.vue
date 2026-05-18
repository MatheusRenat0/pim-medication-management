<template>
  <div class="animate-in">
    <div class="page-header"><h1 class="page-title">Relatórios</h1><p class="page-subtitle">Relatórios operacionais do sistema</p></div>
    <div class="tabs mb-2">
      <button v-for="tab in tabs" :key="tab.key" class="tab" :class="{ active: activeTab===tab.key }" @click="activeTab=tab.key">{{ tab.label }}</button>
    </div>
    <div class="card">
      <div class="table-actions mb-2">
        <div class="search-box"><svg class="search-icon" width="16" height="16" viewBox="0 0 16 16" fill="none"><circle cx="7" cy="7" r="4.5" stroke="currentColor" stroke-width="1.5"/><path d="M10.5 10.5l3 3" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg><input v-model="busca" placeholder="Filtrar..." /></div>
        <button class="btn btn-outline btn-sm" @click="exportCSV"><svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M21 15v4a2 2 0 01-2 2H5a2 2 0 01-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/></svg>Exportar CSV</button>
      </div>
      <!-- Medicamentos -->
      <div v-if="activeTab==='medicamentos'">
        <table class="data-table"><thead><tr><th>Nome</th><th>Princípio Ativo</th><th>Lote</th><th>Validade</th><th>Tarja</th></tr></thead>
          <tbody><tr v-for="m in filteredData" :key="m.id"><td>{{ m.nome }}</td><td>{{ m.principioAtivo }}</td><td>{{ m.lote }}</td><td>{{ formatDate(m.dataValidade) }}</td><td>{{ m.tarja||'-' }}</td></tr>
            <tr v-if="filteredData.length===0"><td colspan="5" class="empty-cell">Sem dados</td></tr></tbody></table>
      </div>
      <!-- Estoque -->
      <div v-if="activeTab==='estoque'">
        <table class="data-table"><thead><tr><th>Medicamento</th><th>Qtd Disponível</th><th>Nível Mínimo</th><th>Status</th></tr></thead>
          <tbody><tr v-for="e in filteredData" :key="e.id"><td>{{ e.medicamento?.nome||'ID: '+e.medicamentoId }}</td><td>{{ e.quantidadeDisponivel }}</td><td>{{ e.nivelMinimoAlerta }}</td><td><span class="badge" :class="e.quantidadeDisponivel<=e.nivelMinimoAlerta?'badge-warning':'badge-success'">{{ e.quantidadeDisponivel<=e.nivelMinimoAlerta?'Baixo':'Normal' }}</span></td></tr>
            <tr v-if="filteredData.length===0"><td colspan="4" class="empty-cell">Sem dados</td></tr></tbody></table>
      </div>
      <!-- Pedidos -->
      <div v-if="activeTab==='pedidos'">
        <table class="data-table"><thead><tr><th>ID</th><th>Cliente</th><th>Data</th><th>Status</th><th>Valor</th></tr></thead>
          <tbody><tr v-for="p in filteredData" :key="p.id"><td>#{{ p.id }}</td><td>{{ p.cliente?.nome||'ID: '+p.usuarioId }}</td><td>{{ formatDate(p.dataPedido) }}</td><td><span class="badge" :class="p.status==='Entregue'?'badge-success':p.status==='Cancelado'?'badge-error':'badge-warning'">{{ p.status }}</span></td><td>R$ {{ Number(p.valorTotal||0).toFixed(2) }}</td></tr>
            <tr v-if="filteredData.length===0"><td colspan="5" class="empty-cell">Sem dados</td></tr></tbody></table>
      </div>
      <!-- Entregas -->
      <div v-if="activeTab==='entregas'">
        <table class="data-table"><thead><tr><th>ID</th><th>Pedido</th><th>Destino</th><th>Previsão</th><th>Status</th></tr></thead>
          <tbody><tr v-for="e in filteredData" :key="e.id"><td>#{{ e.id }}</td><td>#{{ e.pedidoId }}</td><td>{{ e.enderecoDestino }}</td><td>{{ formatDate(e.dataPrevista) }}</td><td><span class="badge" :class="e.statusLogistico==='Entregue'?'badge-success':e.statusLogistico==='Atrasado'?'badge-error':'badge-info'">{{ e.statusLogistico }}</span></td></tr>
            <tr v-if="filteredData.length===0"><td colspan="5" class="empty-cell">Sem dados</td></tr></tbody></table>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue'
import { useApi } from '../../composables/useApi'
const { get } = useApi()
const tabs = [{key:'medicamentos',label:'Medicamentos'},{key:'estoque',label:'Estoque'},{key:'pedidos',label:'Pedidos'},{key:'entregas',label:'Entregas'}]
const activeTab = ref('medicamentos')
const busca = ref('')
const rawData = ref([])

const filteredData = computed(() => {
  if (!busca.value) return rawData.value
  const q = busca.value.toLowerCase()
  return rawData.value.filter(item => JSON.stringify(item).toLowerCase().includes(q))
})

const formatDate = (d) => d ? new Date(d).toLocaleDateString('pt-BR') : '-'

async function loadTab() {
  try {
    const endpoints = { medicamentos:'/Medicamento', estoque:'/Estoque', pedidos:'/Pedido', entregas:'/Entrega' }
    rawData.value = await get(endpoints[activeTab.value])
  } catch(e) { rawData.value = []; console.error(e) }
}

function exportCSV() {
  if (!rawData.value.length) return
  const keys = Object.keys(rawData.value[0]).filter(k => typeof rawData.value[0][k] !== 'object')
  const csv = [keys.join(','), ...rawData.value.map(row => keys.map(k => `"${row[k]??''}"`).join(','))].join('\n')
  const blob = new Blob([csv], { type: 'text/csv' })
  const url = URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url; a.download = `relatorio_${activeTab.value}.csv`; a.click()
  URL.revokeObjectURL(url)
}

watch(activeTab, () => { busca.value = ''; loadTab() })
onMounted(loadTab)
</script>

<style scoped>
.tabs { display: flex; gap: 4px; border-bottom: 1px solid var(--border); padding-bottom: 0; }
.tab { padding: 10px 18px; font-size: 14px; font-weight: 500; color: var(--text-secondary); background: none; border: none; cursor: pointer; border-bottom: 2px solid transparent; transition: all var(--transition); font-family: var(--font); }
.tab:hover { color: var(--text); }
.tab.active { color: var(--primary); border-bottom-color: var(--primary); font-weight: 600; }
.table-actions { display: flex; align-items: center; justify-content: space-between; gap: 12px; }
.empty-cell { text-align: center; padding: 32px !important; color: var(--text-muted); }
</style>
