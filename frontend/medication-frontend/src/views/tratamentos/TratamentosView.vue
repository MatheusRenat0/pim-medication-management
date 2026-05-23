<template>
  <div class="animate-in">
    <div class="page-header-row"><div><h1 class="page-title">Tratamentos</h1><p class="page-subtitle">Configure sachês e rotinas de medicamentos</p></div>
      <button v-if="auth.userType !== 'Paciente'" class="btn btn-primary" @click="showCreate=true"><svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Novo Tratamento</button></div>
    <!-- Se não tiver tratamentos ativos e for Paciente, mostra o welcome card para montar a box -->
    <div v-if="tratamentos.length === 0 && auth.userType === 'Paciente'" class="welcome-box-card">
      <div class="welcome-box-icon">📦</div>
      <h3>Você ainda não possui uma Box MedFlow</h3>
      <p>Configure seus medicamentos de rotina e receba todo mês seus sachês organizados de forma prática e segura.</p>
      <div class="welcome-box-actions">
        <button class="btn btn-success" @click="$router.push('/setup-box')">Assinar Box MedFlow Agora</button>
      </div>
    </div>

    <div v-else class="table-wrap">
      <table class="data-table"><thead><tr><th>ID</th><th v-if="auth.userType !== 'Paciente'">Paciente</th><th>Receita</th><th>Data Início</th><th>Ativo</th><th>Sachês</th></tr></thead>
        <tbody>
          <tr v-for="t in tratamentos" :key="t.id"><td>#{{ t.id }}</td><td v-if="auth.userType !== 'Paciente'">{{ t.usuario?.nome || 'ID: '+t.usuarioId }}</td><td>#{{ t.receitaId }}</td>
            <td>{{ formatDate(t.dataInicio) }}</td><td><span class="badge" :class="t.ativo?'badge-success':'badge-neutral'">{{ t.ativo?'Ativo':'Inativo' }}</span></td>
            <td>{{ t.saches?.length || 0 }} sachê(s)</td></tr>
          <tr v-if="tratamentos.length===0"><td :colspan="auth.userType === 'Paciente' ? 5 : 6" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhum tratamento encontrado</td></tr>
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
async function carregar() {
  try {
    if (auth.userType === 'Paciente') {
      tratamentos.value = await get(`/Tratamento/Usuario/${auth.userId}`)
    } else {
      tratamentos.value = await get('/Tratamento')
    }
  } catch(e) {
    console.error(e)
  }
}
onMounted(() => { createData.value.usuarioId = auth.userId || 0; carregar() })
</script>

<style scoped>
.sache-row { display: flex; align-items: center; gap: 8px; margin-bottom: 8px; }

/* Welcome empty state card */
.welcome-box-card {
  background: #fff;
  border: 1.5px solid var(--border);
  border-radius: 16px;
  padding: 48px 32px;
  text-align: center;
  max-width: 520px;
  margin: 40px auto;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.04);
}
.welcome-box-icon {
  font-size: 44px;
  margin-bottom: 16px;
}
.welcome-box-card h3 {
  font-family: Georgia, serif;
  font-size: 18px;
  font-weight: 700;
  color: var(--text);
  margin-bottom: 8px;
}
.welcome-box-card p {
  font-size: 13px;
  color: var(--text-secondary);
  line-height: 1.6;
  margin-bottom: 24px;
}
.welcome-box-actions {
  display: flex;
  justify-content: center;
  gap: 12px;
  flex-wrap: wrap;
}
</style>
