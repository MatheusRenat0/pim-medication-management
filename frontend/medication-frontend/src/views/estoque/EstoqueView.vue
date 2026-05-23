<template>
  <div class="animate-in">
    <div class="page-header-row"><div><h1 class="page-title">Estoque</h1><p class="page-subtitle">Controle de movimentação e alertas</p></div>
      <div class="page-actions">
        <button class="btn btn-primary" @click="abrirModal()"><svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Novo Registro</button>
        <button class="btn btn-outline" @click="abrirModalMovimentacao()"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><polyline points="23 6 13.5 15.5 8.5 10.5 1 18"/><polyline points="17 6 23 6 23 12"/></svg>Registrar Movimentação</button>
      </div>
    </div>

    <!-- Alertas de estoque baixo -->
    <div v-if="alertas.length" class="alerts-bar mb-2">
      <div v-for="a in alertas" :key="a.id" class="alert-chip"><span class="badge badge-warning">Baixo</span> {{ a.medicamento?.nome }} — {{ a.quantidadeDisponivel }} un. (mín: {{ a.nivelMinimoAlerta }})</div>
    </div>

    <!-- Tabs: Estoque / Histórico -->
    <div class="tabs mb-2">
      <button class="tab" :class="{ active: tabAtiva === 'estoque' }" @click="tabAtiva = 'estoque'">Estoque Atual</button>
      <button class="tab" :class="{ active: tabAtiva === 'historico' }" @click="tabAtiva = 'historico'; carregarHistorico()">Histórico de Movimentações</button>
    </div>

    <!-- TAB: ESTOQUE ATUAL -->
    <div v-if="tabAtiva === 'estoque'" class="table-wrap">
      <table class="data-table"><thead><tr><th>Medicamento</th><th>Qtd Disponível</th><th>Nível Mínimo</th><th>Status</th><th style="width:100px">Ações</th></tr></thead>
        <tbody>
          <tr v-for="e in estoques" :key="e.id"><td><strong>{{ e.medicamento?.nome || 'ID: '+e.medicamentoId }}</strong></td><td>{{ e.quantidadeDisponivel }}</td><td>{{ e.nivelMinimoAlerta }}</td>
            <td><span class="badge" :class="statusBadge(e)">{{ statusText(e) }}</span></td>
            <td><div class="actions"><button class="btn btn-ghost btn-icon btn-sm" @click="abrirModal(e)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M11 4H4a2 2 0 00-2 2v14a2 2 0 002 2h14a2 2 0 002-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 013 3L12 15l-4 1 1-4 9.5-9.5z"/></svg></button>
              <button class="btn btn-ghost btn-icon btn-sm" @click="confirmarExclusao(e)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 01-2 2H7a2 2 0 01-2-2V6m3 0V4a2 2 0 012-2h4a2 2 0 012 2v2"/></svg></button></div></td></tr>
          <tr v-if="estoques.length===0"><td colspan="5" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhum registro de estoque</td></tr>
        </tbody></table>
    </div>

    <!-- TAB: HISTÓRICO DE MOVIMENTAÇÕES (RF07) -->
    <div v-if="tabAtiva === 'historico'" class="table-wrap">
      <table class="data-table"><thead><tr><th>Data</th><th>Medicamento</th><th>Tipo</th><th>Quantidade</th><th>Observação</th></tr></thead>
        <tbody>
          <tr v-for="m in movimentacoes" :key="m.id">
            <td>{{ formatDate(m.dataMovimentacao) }}</td>
            <td><strong>{{ m.medicamento?.nome || 'ID: '+m.medicamentoId }}</strong></td>
            <td><span class="badge" :class="m.tipo === 'Entrada' ? 'badge-success' : 'badge-error'">{{ m.tipo === 'Entrada' ? '↑ Entrada' : '↓ Saída' }}</span></td>
            <td>{{ m.quantidade }}</td>
            <td>{{ m.observacao || '-' }}</td>
          </tr>
          <tr v-if="movimentacoes.length===0"><td colspan="5" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhuma movimentação registrada</td></tr>
        </tbody></table>
    </div>

    <!-- Modal: Novo/Editar Estoque -->
    <Modal v-model="showModal" :title="editando?'Editar Estoque':'Novo Registro de Estoque'" size="md">
      <div class="form-group"><label class="form-label">Medicamento (ID)</label><input class="form-input" type="number" v-model.number="formData.medicamentoId" required></div>
      <div class="form-row"><div class="form-group"><label class="form-label">Quantidade Disponível</label><input class="form-input" type="number" v-model.number="formData.quantidadeDisponivel" required></div><div class="form-group"><label class="form-label">Nível Mínimo Alerta</label><input class="form-input" type="number" v-model.number="formData.nivelMinimoAlerta" required></div></div>
      <template #footer><button class="btn btn-outline" @click="showModal=false">Cancelar</button><button class="btn btn-primary" @click="salvar" :disabled="salvando">{{ salvando?'Salvando...':'Salvar' }}</button></template>
    </Modal>

    <!-- Modal: Confirmar Exclusão -->
    <Modal v-model="showConfirm" title="Confirmar Exclusão" size="sm">
      <p>Deseja excluir este registro de estoque?</p>
      <template #footer><button class="btn btn-outline" @click="showConfirm=false">Cancelar</button><button class="btn btn-danger" @click="excluir">Excluir</button></template>
    </Modal>

    <!-- Modal: Registrar Movimentação (RF07) -->
    <Modal v-model="showModalMov" title="Registrar Movimentação de Estoque" size="md">
      <div class="form-group"><label class="form-label">Medicamento (ID)</label><input class="form-input" type="number" v-model.number="formMov.medicamentoId" required></div>
      <div class="form-row">
        <div class="form-group"><label class="form-label">Tipo</label>
          <select class="form-select" v-model="formMov.tipo">
            <option value="Entrada">↑ Entrada</option>
            <option value="Saida">↓ Saída</option>
          </select>
        </div>
        <div class="form-group"><label class="form-label">Quantidade</label><input class="form-input" type="number" v-model.number="formMov.quantidade" min="1" required></div>
      </div>
      <div class="form-group"><label class="form-label">Observação (opcional)</label><input class="form-input" v-model="formMov.observacao" placeholder="Ex: Reposição do fornecedor X"></div>
      <template #footer><button class="btn btn-outline" @click="showModalMov=false">Cancelar</button><button class="btn btn-primary" @click="salvarMovimentacao" :disabled="salvandoMov">{{ salvandoMov?'Salvando...':'Registrar' }}</button></template>
    </Modal>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useApi } from '../../composables/useApi'
import { useToast } from '../../composables/useToast'
import Modal from '../../components/common/Modal.vue'
const { get, post, put, del } = useApi()
const toast = useToast()

// ── Estoque ──
const estoques = ref([])
const showModal = ref(false)
const showConfirm = ref(false)
const editando = ref(false)
const salvando = ref(false)
const excluindo = ref(null)
const formData = ref({ medicamentoId:0, quantidadeDisponivel:0, nivelMinimoAlerta:10 })
const alertas = computed(() => estoques.value.filter(e => e.quantidadeDisponivel <= e.nivelMinimoAlerta))
function statusBadge(e) { return e.quantidadeDisponivel <= 0 ? 'badge-error' : e.quantidadeDisponivel <= e.nivelMinimoAlerta ? 'badge-warning' : 'badge-success' }
function statusText(e) { return e.quantidadeDisponivel <= 0 ? 'Esgotado' : e.quantidadeDisponivel <= e.nivelMinimoAlerta ? 'Baixo' : 'Normal' }
function abrirModal(e) { editando.value = !!e; formData.value = e ? { ...e } : { medicamentoId:0, quantidadeDisponivel:0, nivelMinimoAlerta:10 }; showModal.value = true }
function confirmarExclusao(e) { excluindo.value = e; showConfirm.value = true }
async function salvar() { salvando.value = true; try { if (editando.value) { await put(`/Estoque/${formData.value.id}`, formData.value); toast.success('Estoque atualizado!') } else { await post('/Estoque', formData.value); toast.success('Registro criado!') } showModal.value = false; await carregar() } catch(e) { toast.error(e.message) } finally { salvando.value = false } }
async function excluir() { try { await del(`/Estoque/${excluindo.value.id}`); toast.success('Registro excluído!'); showConfirm.value = false; await carregar() } catch(e) { toast.error(e.message) } }
async function carregar() { try { estoques.value = await get('/Estoque') } catch(e) { console.error(e) } }

// ── Movimentações (RF07) ──
const tabAtiva = ref('estoque')
const movimentacoes = ref([])
const showModalMov = ref(false)
const salvandoMov = ref(false)
const formMov = ref({ medicamentoId: 0, tipo: 'Entrada', quantidade: 1, observacao: '' })
const formatDate = (d) => d ? new Date(d).toLocaleString('pt-BR') : '-'

function abrirModalMovimentacao() {
  formMov.value = { medicamentoId: 0, tipo: 'Entrada', quantidade: 1, observacao: '' }
  showModalMov.value = true
}

async function carregarHistorico() {
  try { movimentacoes.value = await get('/MovimentacaoEstoque') } catch(e) { console.error(e) }
}

async function salvarMovimentacao() {
  salvandoMov.value = true
  try {
    await post('/MovimentacaoEstoque', formMov.value)
    toast.success('Movimentação registrada!')
    showModalMov.value = false
    await carregar()
    if (tabAtiva.value === 'historico') await carregarHistorico()
  } catch(e) {
    toast.error(e.message)
  } finally {
    salvandoMov.value = false
  }
}

onMounted(carregar)
</script>

<style scoped>
.actions { display: flex; gap: 4px; }
.alerts-bar { display: flex; flex-wrap: wrap; gap: 8px; }
.alert-chip { display: flex; align-items: center; gap: 8px; padding: 8px 14px; background: var(--warning-light); border: 1px solid var(--warning-border); border-radius: var(--radius); font-size: 13px; color: #92400E; }
.tabs { display: flex; gap: 4px; background: var(--search-bg); padding: 4px; border-radius: var(--radius); width: fit-content; }
.tab { padding: 8px 18px; border: none; background: none; font-size: 13px; font-weight: 600; color: var(--text-secondary); cursor: pointer; border-radius: var(--radius-sm); font-family: var(--font); transition: all var(--transition); }
.tab.active { background: var(--bg-white); color: var(--text); box-shadow: var(--shadow-sm); }
.tab:hover:not(.active) { color: var(--text); }
</style>
