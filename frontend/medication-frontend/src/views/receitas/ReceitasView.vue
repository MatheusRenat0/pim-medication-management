<template>
  <div class="animate-in">
    <div class="page-header-row">
      <div>
        <h1 class="page-title">Receitas Médicas</h1>
        <p class="page-subtitle">Upload e validação de receitas</p>
      </div>
      <div style="display: flex; gap: 12px;">
        <button v-if="auth.userType === 'Paciente'" class="btn btn-success" @click="$router.push('/setup-box')">
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" style="margin-right: 4px"><rect x="3" y="9" width="18" height="12" rx="2" ry="2"/><path d="M12 2L2 7l10 5 10-5-10-5z"/></svg>
          Montar Minha Box 📦
        </button>
        <button class="btn btn-primary" @click="showUpload=true">
          <svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 11V3M4 7l4-4 4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/><path d="M2 13h12" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>
          Upload Receita
        </button>
      </div>
    </div>

    <!-- Banner explicativo / welcome para compra de remédio (Paciente) -->
    <div v-if="auth.userType === 'Paciente'" class="purchase-banner mb-2">
      <div class="banner-body">
        <span class="banner-badge">Facilidade</span>
        <h3>Pronto para assinar seus medicamentos?</h3>
        <p>Receba todo mês sua dose organizada por dia e horário em sachês biodegradáveis direto no seu endereço.</p>
      </div>
      <button class="btn btn-success" @click="$router.push('/setup-box')">
        Montar Box MedFlow 📦
      </button>
    </div>

    <!-- Se não tiver receitas, exibe um belo estado vazio -->
    <div v-if="receitas.length === 0 && auth.userType === 'Paciente'" class="welcome-box-card">
      <div class="welcome-box-icon">🏥</div>
      <h3>Nenhuma receita enviada ainda</h3>
      <p>Envie sua receita médica para validação farmacêutica ou configure sua Box diretamente para receber seus sachês mensais organizados.</p>
      <div class="welcome-box-actions">
        <button class="btn btn-success" @click="$router.push('/setup-box')">Montar Minha Box de Remédios</button>
        <button class="btn btn-outline" @click="showUpload=true">Fazer Upload de Receita</button>
      </div>
    </div>

    <div v-else class="table-wrap">
      <table class="data-table"><thead><tr><th>ID</th><th v-if="auth.userType !== 'Paciente'">Paciente</th><th>Data Envio</th><th>Arquivo</th><th>Status</th><th v-if="auth.userType !== 'Paciente'" style="width:160px">Ações</th></tr></thead>
        <tbody>
          <tr v-for="r in receitas" :key="r.id"><td>#{{ r.id }}</td><td v-if="auth.userType !== 'Paciente'">{{ r.usuario?.nome || 'ID: '+r.usuarioId }}</td><td>{{ formatDate(r.dataEnvio) }}</td>
            <td><a v-if="r.caminhoArquivo" :href="'http://localhost:5205'+r.caminhoArquivo" target="_blank" class="link">Ver arquivo</a><span v-else>-</span></td>
            <td><span class="badge" :class="statusBadge(r.status)">{{ r.status }}</span></td>
            <td v-if="auth.userType !== 'Paciente'"><div class="actions" v-if="r.status==='Pendente'">
              <button class="btn btn-success btn-sm" @click="analisar(r.id,'Aprovada')"><svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round"><path d="M20 6L9 17l-5-5"/></svg>Aprovar</button>
              <button class="btn btn-danger btn-sm" @click="analisar(r.id,'Recusada')"><svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>Recusar</button>
            </div></td></tr>
          <tr v-if="receitas.length===0"><td :colspan="auth.userType === 'Paciente' ? 4 : 6" style="text-align:center;padding:32px;color:var(--text-muted)">Nenhuma receita encontrada</td></tr>
        </tbody></table>
    </div>
    <Modal v-model="showUpload" title="Upload de Receita" size="md">
      <div class="upload-zone" :class="{ 'has-file': arquivo, dragging }" @dragover.prevent="dragging=true" @dragleave.prevent="dragging=false" @drop.prevent="onDrop" @click="$refs.fileInput.click()">
        <input ref="fileInput" type="file" accept="image/*,.pdf" hidden @change="onFileChange" />
        <template v-if="!arquivo"><p class="upload-title">Arraste a receita aqui</p><p class="upload-sub">ou <span class="upload-link">clique para selecionar</span></p><p class="upload-hint">JPG, PNG ou PDF · Máx. 10 MB</p></template>
        <template v-else><p class="upload-title">✓ {{ arquivo.name }}</p><button class="upload-change" @click.stop="$refs.fileInput.click()">Trocar arquivo</button></template>
      </div>
      <template #footer><button class="btn btn-outline" @click="showUpload=false">Cancelar</button><button class="btn btn-primary" @click="enviar" :disabled="!arquivo||enviando">{{ enviando?'Enviando...':'Enviar Receita' }}</button></template>
    </Modal>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useApi } from '../../composables/useApi'
import { useToast } from '../../composables/useToast'
import { useAuthStore } from '../../stores/auth'
import Modal from '../../components/common/Modal.vue'
const { get, upload, api } = useApi()
const toast = useToast()
const auth = useAuthStore()
const receitas = ref([])
const showUpload = ref(false)
const arquivo = ref(null)
const dragging = ref(false)
const enviando = ref(false)
const formatDate = (d) => d ? new Date(d).toLocaleDateString('pt-BR') : '-'
function statusBadge(s) { return s==='Aprovada'?'badge-success':s==='Recusada'?'badge-error':'badge-warning' }
function onFileChange(e) { arquivo.value = e.target.files[0] }
function onDrop(e) { dragging.value = false; arquivo.value = e.dataTransfer.files[0] }
async function enviar() { enviando.value = true; try { const fd = new FormData(); fd.append('usuarioId', auth.userId); fd.append('arquivo', arquivo.value); await upload('/Receita/upload', fd); toast.success('Receita enviada!'); showUpload.value = false; arquivo.value = null; await carregar() } catch(e) { toast.error(e.message) } finally { enviando.value = false } }
async function analisar(id, status) { try { await api.put(`/Receita/analise/${id}`, JSON.stringify(status), { headers: {'Content-Type':'application/json'} }); toast.success(`Receita ${status.toLowerCase()}!`); await carregar() } catch(e) { toast.error(e.message) } }
async function carregar() {
  try {
    if (auth.userType === 'Paciente') {
      receitas.value = await get(`/Receita/usuario/${auth.userId}`)
    } else {
      receitas.value = await get('/Receita/pendentes')
    }
  } catch(e) {
    console.error(e)
  }
}
onMounted(carregar)
</script>

<style scoped>
.actions { display: flex; gap: 6px; }
.link { color: var(--primary); text-decoration: none; font-weight: 500; }
.link:hover { text-decoration: underline; }
.upload-zone { border: 2px dashed var(--border); border-radius: var(--radius-lg); padding: 48px 24px; text-align: center; cursor: pointer; transition: all var(--transition); }
.upload-zone:hover, .upload-zone.dragging { border-color: var(--primary); background: var(--primary-light); }
.upload-zone.has-file { border-style: solid; border-color: var(--primary-border); background: var(--primary-light); }
.upload-title { font-size: 16px; font-weight: 600; color: var(--text); margin-bottom: 4px; }
.upload-sub { font-size: 14px; color: var(--text-secondary); margin-bottom: 8px; }
.upload-link { color: var(--primary); font-weight: 600; text-decoration: underline; }
.upload-hint { font-size: 12px; color: var(--text-muted); }
.upload-change { background: none; border: none; color: var(--text-secondary); font-size: 13px; text-decoration: underline; cursor: pointer; margin-top: 8px; font-family: var(--font); }

/* Purchase Banner */
.purchase-banner {
  background: linear-gradient(135deg, #eff6ff 0%, #dbeafe 100%);
  border: 1px solid #bfdbfe;
  border-radius: 16px;
  padding: 24px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 16px;
}
.banner-body {
  flex: 1;
  min-width: 280px;
}
.banner-badge {
  display: inline-block;
  font-size: 11px;
  font-weight: 700;
  text-transform: uppercase;
  background: #2563eb;
  color: #fff;
  padding: 2px 8px;
  border-radius: 100px;
  margin-bottom: 8px;
}
.purchase-banner h3 {
  font-family: Georgia, serif;
  font-size: 18px;
  font-weight: 700;
  color: #1e3a8a;
  margin-bottom: 6px;
}
.purchase-banner p {
  font-size: 13px;
  color: #4b5563;
  line-height: 1.6;
}

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
