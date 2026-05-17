<template>
  <div class="animate-in">
    <div class="page-header-row"><div><h1 class="page-title">Estoque</h1><p class="page-subtitle">Controle de movimentação e alertas</p></div>
      <button class="btn btn-primary" @click="abrirModal()"><svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Novo Registro</button></div>
    <div v-if="alertas.length" class="alerts-bar mb-2">
      <div v-for="a in alertas" :key="a.id" class="alert-chip"><span class="badge badge-warning">Baixo</span> {{ a.medicamento?.nome }} — {{ a.quantidadeDisponivel }} un. (mín: {{ a.nivelMinimoAlerta }})</div>
    </div>
    <div class="table-wrap">
      <table class="data-table"><thead><tr><th>Medicamento</th><th>Qtd Disponível</th><th>Nível Mínimo</th><th>Status</th><th style="width:100px">Ações</th></tr></thead>
        <tbody>
          <tr v-for="e in estoques" :key="e.id"><td><strong>{{ e.medicamento?.nome || 'ID: '+e.medicamentoId }}</strong></td><td>{{ e.quantidadeDisponivel }}</td><td>{{ e.nivelMinimoAlerta }}</td>
            <td><span class="badge" :class="statusBadge(e)">{{ statusText(e) }}</span></td>
            <td><div class="actions"><button class="btn btn-ghost btn-icon btn-sm" @click="abrirModal(e)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M11 4H4a2 2 0 00-2 2v14a2 2 0 002 2h14a2 2 0 002-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 013 3L12 15l-4 1 1-4 9.5-9.5z"/></svg></button>
              <button class="btn btn-ghost btn-icon btn-sm" @click="confirmarExclusao(e)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 01-2 2H7a2 2 0 01-2-2V6m3 0V4a2 2 0 012-2h4a2 2 0 012 2v2"/></svg></button></div></td></tr>
          <tr v-if="estoques.length===0"><td colspan="5" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhum registro de estoque</td></tr>
        </tbody></table>
    </div>
    <Modal v-model="showModal" :title="editando?'Editar Estoque':'Novo Registro de Estoque'" size="md">
      <div class="form-group"><label class="form-label">Medicamento (ID)</label><input class="form-input" type="number" v-model.number="formData.medicamentoId" required></div>
      <div class="form-row"><div class="form-group"><label class="form-label">Quantidade Disponível</label><input class="form-input" type="number" v-model.number="formData.quantidadeDisponivel" required></div><div class="form-group"><label class="form-label">Nível Mínimo Alerta</label><input class="form-input" type="number" v-model.number="formData.nivelMinimoAlerta" required></div></div>
      <template #footer><button class="btn btn-outline" @click="showModal=false">Cancelar</button><button class="btn btn-primary" @click="salvar" :disabled="salvando">{{ salvando?'Salvando...':'Salvar' }}</button></template>
    </Modal>
    <Modal v-model="showConfirm" title="Confirmar Exclusão" size="sm">
      <p>Deseja excluir este registro de estoque?</p>
      <template #footer><button class="btn btn-outline" @click="showConfirm=false">Cancelar</button><button class="btn btn-danger" @click="excluir">Excluir</button></template>
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
onMounted(carregar)
</script>

<style scoped>
.actions { display: flex; gap: 4px; }
.alerts-bar { display: flex; flex-wrap: wrap; gap: 8px; }
.alert-chip { display: flex; align-items: center; gap: 8px; padding: 8px 14px; background: var(--warning-light); border: 1px solid var(--warning-border); border-radius: var(--radius); font-size: 13px; color: #92400E; }
</style>
