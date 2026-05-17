<template>
  <div class="animate-in">
    <div class="page-header-row"><div><h1 class="page-title">Tratamentos</h1><p class="page-subtitle">Configure sachês e rotinas de medicamentos</p></div>
      <button class="btn btn-primary" @click="showCreate=true"><svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Novo Tratamento</button></div>
    <div class="table-wrap">
      <table class="data-table"><thead><tr><th>ID</th><th>Paciente</th><th>Receita</th><th>Data Início</th><th>Ativo</th><th>Sachês</th></tr></thead>
        <tbody>
          <tr v-for="t in tratamentos" :key="t.id"><td>#{{ t.id }}</td><td>{{ t.usuario?.nome || 'ID: '+t.usuarioId }}</td><td>#{{ t.receitaId }}</td>
            <td>{{ formatDate(t.dataInicio) }}</td><td><span class="badge" :class="t.ativo?'badge-success':'badge-neutral'">{{ t.ativo?'Ativo':'Inativo' }}</span></td>
            <td>{{ t.saches?.length || 0 }} sachê(s)</td></tr>
          <tr v-if="tratamentos.length===0"><td colspan="6" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhum tratamento encontrado</td></tr>
        </tbody></table>
    </div>
    <Modal v-model="showCreate" title="Novo Tratamento" size="lg">
      <div class="form-row"><div class="form-group"><label class="form-label">ID do Paciente</label><input class="form-input" type="number" v-model.number="createData.usuarioId"></div><div class="form-group"><label class="form-label">ID da Receita</label><input class="form-input" type="number" v-model.number="createData.receitaId"></div></div>
      <h4 style="margin:16px 0 8px;font-size:14px;font-weight:600">Sachês</h4>
      <div v-for="(s,i) in createData.saches" :key="i" class="sache-row">
        <input class="form-input" type="number" v-model.number="s.medicamentoId" placeholder="Med. ID" style="width:120px">
        <input class="form-input" type="time" v-model="s.horario" style="width:140px">
        <input class="form-input" type="number" v-model.number="s.quantidadeComprimidos" placeholder="Qtd" style="width:80px" min="1">
        <button class="btn btn-ghost btn-sm" @click="createData.saches.splice(i,1)"><svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg></button>
      </div>
      <button class="btn btn-outline btn-sm mt-1" @click="createData.saches.push({medicamentoId:0,horario:'',quantidadeComprimidos:1})"><svg width="14" height="14" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Adicionar Sachê</button>
      <template #footer><button class="btn btn-outline" @click="showCreate=false">Cancelar</button><button class="btn btn-primary" @click="salvar" :disabled="salvando">{{ salvando?'Salvando...':'Criar Tratamento' }}</button></template>
    </Modal>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useApi } from '../../composables/useApi'
import { useToast } from '../../composables/useToast'
import { useAuthStore } from '../../stores/auth'
import Modal from '../../components/common/Modal.vue'
const { get, post } = useApi()
const toast = useToast()
const auth = useAuthStore()
const tratamentos = ref([])
const showCreate = ref(false)
const salvando = ref(false)
const createData = ref({ usuarioId: 0, receitaId: 0, saches: [{ medicamentoId: 0, horario: '', quantidadeComprimidos: 1 }] })
const formatDate = (d) => d ? new Date(d).toLocaleDateString('pt-BR') : '-'
async function salvar() { salvando.value = true; try { await post('/Tratamento', createData.value); toast.success('Tratamento criado!'); showCreate.value = false; createData.value = { usuarioId:0, receitaId:0, saches:[{medicamentoId:0,horario:'',quantidadeComprimidos:1}] }; await carregar() } catch(e) { toast.error(e.message) } finally { salvando.value = false } }
async function carregar() { try { const uid = auth.userId; if(uid) { tratamentos.value = await get(`/Tratamento/Usuario/${uid}`) } } catch(e) { console.error(e) } }
onMounted(() => { createData.value.usuarioId = auth.userId || 0; carregar() })
</script>

<style scoped>
.sache-row { display: flex; align-items: center; gap: 8px; margin-bottom: 8px; }
</style>
