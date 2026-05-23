<template>
  <div v-if="auth.userType === 'Entregador'">
    <EntregadorView />
  </div>
  <div v-else class="animate-in">
    <div class="page-header-row"><div><h1 class="page-title">Entregas</h1><p class="page-subtitle">Rastreamento e status das suas entregas</p></div>
      <button v-if="auth.userType !== 'Paciente'" class="btn btn-primary" @click="abrirModal()"><svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Nova Entrega</button></div>
    <div class="grid-2 mb-2" v-if="entregaSelecionada">
      <div class="card tracking-card">
        <h3 class="card-title">Rastreamento — Entrega #{{ entregaSelecionada.id }}</h3>
        <div class="tracking-timeline">
          <div class="timeline-step" :class="{ active: stepAtivo >= 1, done: stepAtivo > 1 }"><div class="step-dot"></div><div class="step-info"><strong>Em Preparação</strong><span>Pedido sendo preparado</span></div></div>
          <div class="timeline-step" :class="{ active: stepAtivo >= 2, done: stepAtivo > 2 }"><div class="step-dot"></div><div class="step-info"><strong>Saiu para Entrega</strong><span>A caminho do destino</span></div></div>
          <div class="timeline-step" :class="{ active: stepAtivo >= 3 }"><div class="step-dot"></div><div class="step-info"><strong>Entregue</strong><span>{{ entregaSelecionada.dataRealizada ? formatDate(entregaSelecionada.dataRealizada) : 'Aguardando confirmação' }}</span></div></div>
        </div>
        <div v-if="entregaSelecionada.statusLogistico==='Atrasado'" class="alert-chip error mt-1"><span class="badge badge-error">Atrasado</span>Entrega fora do prazo previsto</div>
      </div>
      <div class="card">
        <h3 class="card-title">Detalhes da Entrega</h3>
        <div class="detail-grid">
          <div class="detail-item"><span class="detail-label">Pedido</span><span>#{{ entregaSelecionada.pedidoId }}</span></div>
          <div class="detail-item" v-if="auth.userType !== 'Paciente'"><span class="detail-label">Entregador</span><span>{{ entregaSelecionada.entregador?.nome || 'ID: '+entregaSelecionada.entregadorId }}</span></div>
          <div class="detail-item"><span class="detail-label">Destino</span><span>{{ entregaSelecionada.enderecoDestino }}</span></div>
          <div class="detail-item"><span class="detail-label">Previsão</span><span>{{ formatDate(entregaSelecionada.dataPrevista) }}</span></div>
          <div class="detail-item"><span class="detail-label">Status</span><span class="badge" :class="statusBadge(entregaSelecionada.statusLogistico)">{{ entregaSelecionada.statusLogistico }}</span></div>
        </div>
        <div class="map-placeholder"><svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"><path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0118 0z"/><circle cx="12" cy="10" r="3"/></svg><p>Integração com mapa disponível em breve</p></div>
        <button class="btn btn-outline btn-sm mt-1" @click="entregaSelecionada=null">Fechar rastreamento</button>
      </div>
    </div>
    <div class="table-wrap">
      <table class="data-table"><thead><tr><th>ID</th><th>Pedido</th><th v-if="auth.userType !== 'Paciente'">Entregador</th><th>Destino</th><th>Previsão</th><th>Status</th><th style="width:140px">Ações</th></tr></thead>
        <tbody>
          <tr v-for="e in entregas" :key="e.id"><td>#{{ e.id }}</td><td>#{{ e.pedidoId }}</td><td v-if="auth.userType !== 'Paciente'">{{ e.entregador?.nome || 'ID: '+e.entregadorId }}</td><td class="truncate" style="max-width:180px">{{ e.enderecoDestino }}</td>
            <td>{{ formatDate(e.dataPrevista) }}</td><td><span class="badge" :class="statusBadge(e.statusLogistico)">{{ e.statusLogistico }}</span></td>
            <td><div class="actions">
              <button class="btn btn-outline btn-sm" @click="entregaSelecionada=e"><svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0118 0z"/><circle cx="12" cy="10" r="3"/></svg>Rastrear</button>
              <button v-if="auth.userType !== 'Paciente'" class="btn btn-ghost btn-icon btn-sm" @click="abrirModal(e)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M11 4H4a2 2 0 00-2 2v14a2 2 0 002 2h14a2 2 0 002-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 013 3L12 15l-4 1 1-4 9.5-9.5z"/></svg></button>
            </div></td></tr>
          <tr v-if="entregas.length===0"><td :colspan="auth.userType === 'Paciente' ? 6 : 7" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhuma entrega encontrada</td></tr>
        </tbody></table>
    </div>
    <Modal v-model="showModal" :title="editando?'Editar Entrega':'Nova Entrega'" size="md">
      <div class="form-row"><div class="form-group"><label class="form-label">ID do Pedido</label><input class="form-input" type="number" v-model.number="formData.pedidoId"></div><div class="form-group"><label class="form-label">ID do Entregador</label><input class="form-input" type="number" v-model.number="formData.entregadorId"></div></div>
      <div class="form-group"><label class="form-label">Endereço Destino</label><input class="form-input" v-model="formData.enderecoDestino"></div>
      <div class="form-row"><div class="form-group"><label class="form-label">Data Prevista</label><input class="form-input" type="date" v-model="formData.dataPrevista"></div><div class="form-group"><label class="form-label">Status</label><select class="form-select" v-model="formData.statusLogistico"><option>Pendente</option><option>Em Preparação</option><option>Saiu para Entrega</option><option>Entregue</option><option>Atrasado</option></select></div></div>
      <template #footer><button class="btn btn-outline" @click="showModal=false">Cancelar</button><button class="btn btn-primary" @click="salvar" :disabled="salvando">{{ salvando?'Salvando...':'Salvar' }}</button></template>
    </Modal>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useApi } from '../../composables/useApi'
import { useToast } from '../../composables/useToast'
import { useAuthStore } from '../../stores/auth'
import Modal from '../../components/common/Modal.vue'
import EntregadorView from './EntregadorView.vue'
const { get, post, put } = useApi()
const toast = useToast()
const auth = useAuthStore()
const entregas = ref([])
const entregaSelecionada = ref(null)
const showModal = ref(false)
const editando = ref(false)
const salvando = ref(false)
const formData = ref({ pedidoId:0, entregadorId:0, enderecoDestino:'', dataPrevista:'', statusLogistico:'Pendente' })
const formatDate = (d) => d ? new Date(d).toLocaleDateString('pt-BR') : '-'
function statusBadge(s) { return s==='Entregue'?'badge-success':s==='Atrasado'?'badge-error':s==='Saiu para Entrega'?'badge-info':s==='Em Preparação'?'badge-warning':'badge-neutral' }
const stepAtivo = computed(() => { const s = entregaSelecionada.value?.statusLogistico; if(s==='Entregue') return 3; if(s==='Saiu para Entrega') return 2; return 1 })
function abrirModal(e) { editando.value = !!e; formData.value = e ? { ...e, dataPrevista: e.dataPrevista?.split('T')[0] } : { pedidoId:0, entregadorId:0, enderecoDestino:'', dataPrevista:'', statusLogistico:'Pendente' }; showModal.value = true }
async function salvar() { salvando.value = true; try { if (editando.value) { await put(`/Entrega/${formData.value.id}`, formData.value); toast.success('Entrega atualizada!') } else { await post('/Entrega', formData.value); toast.success('Entrega criada!') } showModal.value = false; await carregar() } catch(e) { toast.error(e.message) } finally { salvando.value = false } }
async function carregar() {
  try {
    if (auth.userType === 'Paciente') {
      entregas.value = await get(`/Entrega/Usuario/${auth.userId}`)
    } else {
      entregas.value = await get('/Entrega')
    }
  } catch(e) {
    console.error(e)
  }
}
onMounted(carregar)
</script>

<style scoped>
.actions { display: flex; gap: 6px; }
.card-title { font-size: 16px; font-weight: 700; color: var(--text); margin-bottom: 16px; }
.tracking-card { background: #fff; }
.tracking-timeline { display: flex; flex-direction: column; gap: 0; position: relative; }
.timeline-step { display: flex; align-items: flex-start; gap: 16px; padding: 16px 0; position: relative; opacity: 0.4; }
.timeline-step.active, .timeline-step.done { opacity: 1; }
.timeline-step:not(:last-child)::after { content: ''; position: absolute; left: 11px; top: 40px; bottom: -8px; width: 2px; background: var(--border); }
.timeline-step.done:not(:last-child)::after { background: var(--success); }
.step-dot { width: 24px; height: 24px; border-radius: 50%; border: 2px solid var(--border); background: #fff; flex-shrink: 0; display: flex; align-items: center; justify-content: center; }
.timeline-step.active .step-dot { border-color: var(--primary); background: var(--primary); }
.timeline-step.done .step-dot { border-color: var(--success); background: var(--success); }
.step-info { display: flex; flex-direction: column; }
.step-info strong { font-size: 14px; color: var(--text); }
.step-info span { font-size: 12px; color: var(--text-secondary); }
.detail-grid { display: flex; flex-direction: column; gap: 12px; }
.detail-item { display: flex; justify-content: space-between; align-items: center; font-size: 14px; }
.detail-label { color: var(--text-secondary); font-weight: 500; }
.alert-chip { display: flex; align-items: center; gap: 8px; padding: 8px 14px; border-radius: var(--radius); font-size: 13px; }
.alert-chip.error { background: var(--error-light); color: #991B1B; border: 1px solid var(--error-border); }
.map-placeholder { margin-top: 16px; padding: 32px; background: var(--search-bg); border-radius: var(--radius-lg); text-align: center; color: var(--text-muted); }
.map-placeholder p { font-size: 13px; margin-top: 8px; }
</style>
