<template>
  <div class="animate-in">
    <div class="page-header-row"><div><h1 class="page-title">Pedidos</h1><p class="page-subtitle">Gerencie pedidos de medicamentos</p></div>
      <button class="btn btn-primary" @click="abrirModal()"><svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Novo Pedido</button></div>
    <div class="table-wrap">
      <table class="data-table"><thead><tr><th>ID</th><th>Cliente</th><th>Data</th><th>Status</th><th>Valor Total</th><th style="width:100px">Ações</th></tr></thead>
        <tbody>
          <tr v-for="p in pedidos" :key="p.id"><td>#{{ p.id }}</td><td>{{ p.cliente?.nome || 'ID: '+p.usuarioId }}</td><td>{{ formatDate(p.dataPedido) }}</td>
            <td><span class="badge" :class="statusBadge(p.status)">{{ p.status }}</span></td>
            <td>R$ {{ Number(p.valorTotal||0).toFixed(2) }}</td>
            <td><div class="actions"><button class="btn btn-ghost btn-icon btn-sm" @click="abrirModal(p)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M11 4H4a2 2 0 00-2 2v14a2 2 0 002 2h14a2 2 0 002-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 013 3L12 15l-4 1 1-4 9.5-9.5z"/></svg></button>
              <button class="btn btn-ghost btn-icon btn-sm" @click="confirmarExclusao(p)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 01-2 2H7a2 2 0 01-2-2V6m3 0V4a2 2 0 012-2h4a2 2 0 012 2v2"/></svg></button></div></td></tr>
          <tr v-if="pedidos.length===0"><td colspan="6" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhum pedido encontrado</td></tr>
        </tbody></table>
    </div>
    <Modal v-model="showModal" :title="editando?'Editar Pedido':'Novo Pedido'" size="md">
      <div class="form-group"><label class="form-label">ID do Cliente</label><input class="form-input" type="number" v-model.number="formData.usuarioId"></div>
      <div class="form-row"><div class="form-group"><label class="form-label">Status</label><select class="form-select" v-model="formData.status"><option>Pendente</option><option>Em Preparo</option><option>Enviado</option><option>Entregue</option><option>Cancelado</option></select></div><div class="form-group"><label class="form-label">Valor Total (R$)</label><input class="form-input" type="number" step="0.01" v-model.number="formData.valorTotal"></div></div>
      <template #footer><button class="btn btn-outline" @click="showModal=false">Cancelar</button><button class="btn btn-primary" @click="salvar" :disabled="salvando">{{ salvando?'Salvando...':'Salvar' }}</button></template>
    </Modal>
    <Modal v-model="showConfirm" title="Confirmar Exclusão" size="sm">
      <p>Deseja excluir o pedido <strong>#{{ excluindo?.id }}</strong>?</p>
      <template #footer><button class="btn btn-outline" @click="showConfirm=false">Cancelar</button><button class="btn btn-danger" @click="excluir">Excluir</button></template>
    </Modal>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useApi } from '../../composables/useApi'
import { useToast } from '../../composables/useToast'
import Modal from '../../components/common/Modal.vue'
const { get, post, put, del } = useApi()
const toast = useToast()
const pedidos = ref([])
const showModal = ref(false)
const showConfirm = ref(false)
const editando = ref(false)
const salvando = ref(false)
const excluindo = ref(null)
const formData = ref({ usuarioId:0, status:'Pendente', valorTotal:0 })
const formatDate = (d) => d ? new Date(d).toLocaleDateString('pt-BR') : '-'
function statusBadge(s) { return s==='Entregue'?'badge-success':s==='Cancelado'?'badge-error':s==='Pendente'?'badge-warning':'badge-info' }
function abrirModal(p) { editando.value = !!p; formData.value = p ? { ...p } : { usuarioId:0, status:'Pendente', valorTotal:0 }; showModal.value = true }
function confirmarExclusao(p) { excluindo.value = p; showConfirm.value = true }
async function salvar() { salvando.value = true; try { if (editando.value) { await put(`/Pedido/${formData.value.id}`, formData.value); toast.success('Pedido atualizado!') } else { await post('/Pedido', formData.value); toast.success('Pedido criado!') } showModal.value = false; await carregar() } catch(e) { toast.error(e.message) } finally { salvando.value = false } }
async function excluir() { try { await del(`/Pedido/${excluindo.value.id}`); toast.success('Pedido excluído!'); showConfirm.value = false; await carregar() } catch(e) { toast.error(e.message) } }
async function carregar() { try { pedidos.value = await get('/Pedido') } catch(e) { console.error(e) } }
onMounted(carregar)
</script>

<style scoped>.actions { display: flex; gap: 4px; }</style>
