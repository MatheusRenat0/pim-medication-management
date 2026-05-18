<template>
  <div class="dashboard animate-in">
    <div class="page-header"><h1 class="page-title">Dashboard</h1><p class="page-subtitle">Visão geral do sistema MedFlow</p></div>
    <div class="grid-4 mb-2">
      <StatsCard label="Medicamentos" :value="stats.totalMedicamentos" variant="primary" :icon="iconMed" />
      <StatsCard label="Estoque Baixo" :value="stats.estoqueBaixo" variant="warning" :icon="iconAlert" />
      <StatsCard label="Pedidos Pendentes" :value="stats.pedidosPendentes" variant="error" :icon="iconPedido" />
      <StatsCard label="Receitas Pendentes" :value="stats.receitasPendentes" variant="success" :icon="iconReceita" />
    </div>
    <div class="grid-2">
      <div class="card">
        <h3 class="card-title">Alertas</h3>
        <div v-if="stats.medicamentosVencendo > 0" class="alert-item warning"><span class="badge badge-warning">Atenção</span><span>{{ stats.medicamentosVencendo }} medicamento(s) vencem em 30 dias</span></div>
        <div v-if="stats.medicamentosVencidos > 0" class="alert-item error"><span class="badge badge-error">Crítico</span><span>{{ stats.medicamentosVencidos }} medicamento(s) vencido(s)</span></div>
        <div v-if="stats.estoqueBaixo > 0" class="alert-item warning"><span class="badge badge-warning">Estoque</span><span>{{ stats.estoqueBaixo }} item(ns) com estoque baixo</span></div>
        <div v-if="stats.entregasPendentes > 0" class="alert-item info"><span class="badge badge-info">Entregas</span><span>{{ stats.entregasPendentes }} entrega(s) pendente(s)</span></div>
        <p v-if="noAlerts" class="text-muted" style="padding:16px 0;font-size:14px">Nenhum alerta no momento 🎉</p>
      </div>
      <div class="card">
        <h3 class="card-title">Resumo do Sistema</h3>
        <div class="summary-grid">
          <div class="summary-item"><span class="summary-num">{{ stats.totalUsuarios }}</span><span class="summary-label">Usuários</span></div>
          <div class="summary-item"><span class="summary-num">{{ stats.totalMedicamentos }}</span><span class="summary-label">Medicamentos</span></div>
          <div class="summary-item"><span class="summary-num">{{ stats.totalPedidos }}</span><span class="summary-label">Pedidos</span></div>
          <div class="summary-item"><span class="summary-num">{{ stats.totalEntregas }}</span><span class="summary-label">Entregas</span></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useApi } from '../../composables/useApi'
import StatsCard from '../../components/common/StatsCard.vue'
const { get } = useApi()
const stats = ref({ totalUsuarios:0,totalMedicamentos:0,totalPedidos:0,pedidosPendentes:0,totalEntregas:0,entregasPendentes:0,receitasPendentes:0,estoqueBaixo:0,medicamentosVencidos:0,medicamentosVencendo:0 })
const noAlerts = computed(() => stats.value.medicamentosVencendo===0 && stats.value.medicamentosVencidos===0 && stats.value.estoqueBaixo===0 && stats.value.entregasPendentes===0)
const iconMed = '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M10.5 1.5H8.25A2.25 2.25 0 006 3.75v16.5a2.25 2.25 0 002.25 2.25h7.5A2.25 2.25 0 0018 20.25V3.75a2.25 2.25 0 00-2.25-2.25H13.5m-3 0V3h3V1.5m-3 0h3m-3 18.75h3"/></svg>'
const iconAlert = '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M10.29 3.86L1.82 18a2 2 0 001.71 3h16.94a2 2 0 001.71-3L13.71 3.86a2 2 0 00-3.42 0z"/><line x1="12" y1="9" x2="12" y2="13"/><line x1="12" y1="17" x2="12.01" y2="17"/></svg>'
const iconPedido = '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><circle cx="9" cy="21" r="1"/><circle cx="20" cy="21" r="1"/><path d="M1 1h4l2.68 13.39a2 2 0 002 1.61h9.72a2 2 0 002-1.61L23 6H6"/></svg>'
const iconReceita = '<svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M14 2H6a2 2 0 00-2 2v16a2 2 0 002 2h12a2 2 0 002-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>'
onMounted(async () => { try { stats.value = await get('/Dashboard/stats') } catch(e) { console.error(e) } })
</script>

<style scoped>
.card-title { font-size: 16px; font-weight: 700; color: var(--text); margin-bottom: 16px; }
.alert-item { display: flex; align-items: center; gap: 12px; padding: 12px 0; border-bottom: 1px solid var(--border); font-size: 14px; color: var(--text); }
.alert-item:last-child { border-bottom: none; }
.summary-grid { display: grid; grid-template-columns: 1fr 1fr; gap: 16px; }
.summary-item { text-align: center; padding: 16px; background: var(--search-bg); border-radius: var(--radius-md); }
.summary-num { display: block; font-size: 28px; font-weight: 800; color: var(--text); letter-spacing: -0.03em; }
.summary-label { font-size: 13px; color: var(--text-secondary); }
</style>
