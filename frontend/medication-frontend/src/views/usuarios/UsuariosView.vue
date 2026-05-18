<template>
  <div class="animate-in">
    <div class="page-header-row"><div><h1 class="page-title">Usuários</h1><p class="page-subtitle">Gerencie os usuários do sistema</p></div>
      <div class="page-actions"><div class="search-box"><svg class="search-icon" width="16" height="16" viewBox="0 0 16 16" fill="none"><circle cx="7" cy="7" r="4.5" stroke="currentColor" stroke-width="1.5"/><path d="M10.5 10.5l3 3" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg><input v-model="busca" placeholder="Buscar usuário..." /></div>
        <button class="btn btn-primary" @click="abrirModal()"><svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>Novo Usuário</button></div>
    </div>
    <div class="table-wrap">
      <table class="data-table"><thead><tr><th>Nome</th><th>E-mail</th><th>CPF</th><th>Tipo</th><th>Cadastro</th><th style="width:100px">Ações</th></tr></thead>
        <tbody>
          <tr v-for="u in filtrados" :key="u.id"><td><strong>{{ u.nome }}</strong></td><td>{{ u.email }}</td><td>{{ u.cpf }}</td>
            <td><span class="badge" :class="badgeTipo(u.tipo)">{{ u.tipo }}</span></td>
            <td>{{ formatDate(u.dataCadastro) }}</td>
            <td><div class="actions"><button class="btn btn-ghost btn-icon btn-sm" @click="abrirModal(u)" title="Editar"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M11 4H4a2 2 0 00-2 2v14a2 2 0 002 2h14a2 2 0 002-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 013 3L12 15l-4 1 1-4 9.5-9.5z"/></svg></button>
              <button class="btn btn-ghost btn-icon btn-sm" @click="confirmarExclusao(u)" title="Excluir"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 01-2 2H7a2 2 0 01-2-2V6m3 0V4a2 2 0 012-2h4a2 2 0 012 2v2"/></svg></button></div></td></tr>
          <tr v-if="filtrados.length===0"><td colspan="6" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhum usuário encontrado</td></tr>
        </tbody></table>
    </div>
    <Modal v-model="showModal" :title="editando?'Editar Usuário':'Novo Usuário'" size="md">
      <form @submit.prevent="salvar">
        <div class="form-group"><label class="form-label">Nome</label><input class="form-input" v-model="formData.nome" required></div>
        <div class="form-row"><div class="form-group"><label class="form-label">E-mail</label><input class="form-input" type="email" v-model="formData.email" required></div><div class="form-group"><label class="form-label">CPF</label><input class="form-input" v-model="formData.cpf" maxlength="14"></div></div>
        <div class="form-row"><div class="form-group"><label class="form-label">Tipo</label><select class="form-select" v-model="formData.tipo"><option value="Paciente">Paciente</option><option value="Funcionario">Funcionário</option><option value="Administrador">Administrador</option></select></div><div class="form-group" v-if="!editando"><label class="form-label">Senha</label><input class="form-input" type="password" v-model="formData.senhaHash"></div></div>
      </form>
      <template #footer><button class="btn btn-outline" @click="showModal=false">Cancelar</button><button class="btn btn-primary" @click="salvar" :disabled="salvando">{{ salvando?'Salvando...':'Salvar' }}</button></template>
    </Modal>
    <Modal v-model="showConfirm" title="Confirmar Exclusão" size="sm">
      <p>Deseja excluir o usuário <strong>{{ excluindo?.nome }}</strong>?</p>
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
const usuarios = ref([])
const busca = ref('')
const showModal = ref(false)
const showConfirm = ref(false)
const editando = ref(false)
const salvando = ref(false)
const excluindo = ref(null)
const formData = ref({ nome:'',email:'',cpf:'',tipo:'Paciente',senhaHash:'' })
const filtrados = computed(() => usuarios.value.filter(u => u.nome?.toLowerCase().includes(busca.value.toLowerCase()) || u.email?.toLowerCase().includes(busca.value.toLowerCase())))
const badgeTipo = (t) => t==='Administrador'?'badge-error':t==='Funcionario'?'badge-info':'badge-neutral'
const formatDate = (d) => d ? new Date(d).toLocaleDateString('pt-BR') : '-'
function abrirModal(u) { editando.value = !!u; formData.value = u ? { ...u } : { nome:'',email:'',cpf:'',tipo:'Paciente',senhaHash:'' }; showModal.value = true }
function confirmarExclusao(u) { excluindo.value = u; showConfirm.value = true }
async function salvar() { salvando.value = true; try { if (editando.value) { await put(`/Usuario/${formData.value.id}`, formData.value); toast.success('Usuário atualizado!') } else { await post('/Usuario/cadastrar', { nome:formData.value.nome,email:formData.value.email,cpf:formData.value.cpf,senha:formData.value.senhaHash }); toast.success('Usuário criado!') } showModal.value = false; await carregar() } catch(e) { toast.error(e.message) } finally { salvando.value = false } }
async function excluir() { try { await del(`/Usuario/${excluindo.value.id}`); toast.success('Usuário excluído!'); showConfirm.value = false; await carregar() } catch(e) { toast.error(e.message) } }
async function carregar() { try { usuarios.value = await get('/Usuario') } catch(e) { console.error(e) } }
onMounted(carregar)
</script>

<style scoped>
.actions { display: flex; gap: 4px; }
</style>
