<template>
  <div class="animate-in">
    <div class="page-header-row"><div><h1 class="page-title">Medicamentos</h1><p class="page-subtitle">Cadastro e controle de validade</p></div>
      <div class="page-actions"><div class="search-box"><svg class="search-icon" width="16" height="16" viewBox="0 0 16 16" fill="none"><circle cx="7" cy="7" r="4.5" stroke="currentColor" stroke-width="1.5"/><path d="M10.5 10.5l3 3" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg><input v-model="busca" placeholder="Buscar medicamento..." /></div>
        <button class="btn btn-primary" @click="abrirModal()"><svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Novo Medicamento</button></div>
    </div>
    <div class="table-wrap">
      <table class="data-table"><thead><tr><th>Nome</th><th>Princípio Ativo</th><th>Lote</th><th>Fabricação</th><th>Validade</th><th>Tarja</th><th style="width:100px">Ações</th></tr></thead>
        <tbody>
          <tr v-for="m in filtrados" :key="m.id" :class="rowClass(m)"><td><strong>{{ m.nome }}</strong></td><td>{{ m.principioAtivo }}</td><td>{{ m.lote }}</td>
            <td>{{ formatDate(m.dataFabricacao) }}</td><td><span :class="validadeClass(m)">{{ formatDate(m.dataValidade) }}</span></td>
            <td><span class="badge" :class="tarjaBadge(m.tarja)">{{ m.tarja || 'Sem tarja' }}</span></td>
            <td><div class="actions"><button class="btn btn-ghost btn-icon btn-sm" @click="abrirModal(m)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M11 4H4a2 2 0 00-2 2v14a2 2 0 002 2h14a2 2 0 002-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 013 3L12 15l-4 1 1-4 9.5-9.5z"/></svg></button>
              <button class="btn btn-ghost btn-icon btn-sm" @click="confirmarExclusao(m)"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 01-2 2H7a2 2 0 01-2-2V6m3 0V4a2 2 0 012-2h4a2 2 0 012 2v2"/></svg></button></div></td></tr>
          <tr v-if="filtrados.length===0"><td colspan="7" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhum medicamento encontrado</td></tr>
        </tbody></table>
    </div>
    <Modal v-model="showModal" :title="editando?'Editar Medicamento':'Novo Medicamento'" size="lg">
      <form @submit.prevent="salvar">
        <div class="form-row"><div class="form-group"><label class="form-label">Nome</label><input class="form-input" v-model="formData.nome" required></div><div class="form-group"><label class="form-label">Princípio Ativo</label><input class="form-input" v-model="formData.principioAtivo" required></div></div>
        <div class="form-row"><div class="form-group"><label class="form-label">Lote</label><input class="form-input" v-model="formData.lote" required></div><div class="form-group"><label class="form-label">Tarja</label><select class="form-select" v-model="formData.tarja"><option value="">Sem tarja</option><option value="Branca">Branca</option><option value="Amarela">Amarela</option><option value="Vermelha">Vermelha</option><option value="Preta">Preta</option></select></div></div>
        <div class="form-row"><div class="form-group"><label class="form-label">Data Fabricação</label><input class="form-input" type="date" v-model="formData.dataFabricacao" required></div><div class="form-group"><label class="form-label">Data Validade</label><input class="form-input" type="date" v-model="formData.dataValidade" required></div></div>
      </form>
      <template #footer><button class="btn btn-outline" @click="showModal=false">Cancelar</button><button class="btn btn-primary" @click="salvar" :disabled="salvando">{{ salvando?'Salvando...':'Salvar' }}</button></template>
    </Modal>
    <Modal v-model="showConfirm" title="Confirmar Exclusão" size="sm">
      <p>Deseja excluir <strong>{{ excluindo?.nome }}</strong>?</p>
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
const medicamentos = ref([])
const busca = ref('')
const showModal = ref(false)
const showConfirm = ref(false)
const editando = ref(false)
const salvando = ref(false)
const excluindo = ref(null)
const formData = ref({ nome:'',principioAtivo:'',lote:'',tarja:'',dataFabricacao:'',dataValidade:'' })
const filtrados = computed(() => medicamentos.value.filter(m => m.nome?.toLowerCase().includes(busca.value.toLowerCase()) || m.principioAtivo?.toLowerCase().includes(busca.value.toLowerCase())))
const formatDate = (d) => d ? new Date(d).toLocaleDateString('pt-BR') : '-'
function diasParaVencer(m) { return Math.ceil((new Date(m.dataValidade) - new Date()) / 86400000) }
function validadeClass(m) { const d = diasParaVencer(m); return d < 0 ? 'text-error' : d <= 30 ? 'text-warning' : '' }
function rowClass(m) { const d = diasParaVencer(m); return d < 0 ? 'row-error' : d <= 30 ? 'row-warning' : '' }
function tarjaBadge(t) { if (t==='Preta') return 'badge-error'; if (t==='Vermelha') return 'badge-error'; if (t==='Amarela') return 'badge-warning'; return 'badge-neutral' }
function abrirModal(m) { editando.value = !!m; formData.value = m ? { ...m, dataFabricacao: m.dataFabricacao?.split('T')[0], dataValidade: m.dataValidade?.split('T')[0] } : { nome:'',principioAtivo:'',lote:'',tarja:'',dataFabricacao:'',dataValidade:'' }; showModal.value = true }
function confirmarExclusao(m) { excluindo.value = m; showConfirm.value = true }
async function salvar() { salvando.value = true; try { if (editando.value) { await put(`/Medicamento/${formData.value.id}`, formData.value); toast.success('Medicamento atualizado!') } else { await post('/Medicamento', formData.value); toast.success('Medicamento criado!') } showModal.value = false; await carregar() } catch(e) { toast.error(e.message) } finally { salvando.value = false } }
async function excluir() { try { await del(`/Medicamento/${excluindo.value.id}`); toast.success('Medicamento excluído!'); showConfirm.value = false; await carregar() } catch(e) { toast.error(e.message) } }
async function carregar() { try { medicamentos.value = await get('/Medicamento') } catch(e) { console.error(e) } }
onMounted(carregar)
</script>

<style scoped>
.actions { display: flex; gap: 4px; }
.row-error td { background: #FEF2F2 !important; }
.row-warning td { background: #FFFBEB !important; }
</style>
