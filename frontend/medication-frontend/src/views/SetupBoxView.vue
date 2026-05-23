<template>
  <div class="setup-layout">

    <!-- NAVBAR -->
    <nav class="navbar" :class="{ scrolled: isScrolled }">
      <div class="nav-inner">

        <a class="logo" href="#">
          <div class="logo-icon">
            <svg width="18" height="18" viewBox="0 0 18 18" fill="none">
              <path d="M9 3v12M3 9h12" stroke="#fff" stroke-width="2.2" stroke-linecap="round" />
            </svg>
          </div>
          <span class="logo-text">MedFlow</span>
          <span class="logo-badge">Beta</span>
        </a>

        <div class="nav-actions">
          <button class="btn-back" @click="$router.push('/')">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
              <path d="M10 4L6 8l4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                stroke-linejoin="round" />
            </svg>
            Voltar ao início
          </button>
        </div>

      </div>
    </nav>

    <!-- STEPS INDICATOR -->
    <div class="steps-bar">
      <div class="steps-inner">
        <div class="step-pill" :class="{ active: passo >= 1, done: passo > 1 }">
          <span class="pill-num">
            <svg v-if="passo > 1" width="12" height="12" viewBox="0 0 12 12" fill="none">
              <path d="M2 6l2.5 2.5L10 4" stroke="white" stroke-width="1.5" stroke-linecap="round"
                stroke-linejoin="round" />
            </svg>
            <template v-else>01</template>
          </span>
          <span class="pill-label">Receita Médica</span>
        </div>
        <div class="step-divider" :class="{ done: passo > 1 }"></div>
        <div class="step-pill" :class="{ active: passo >= 2 }">
          <span class="pill-num">02</span>
          <span class="pill-label">Seus Medicamentos</span>
        </div>
        <div class="step-divider"></div>
        <div class="step-pill">
          <span class="pill-num">03</span>
          <span class="pill-label">Pagamento</span>
        </div>
      </div>
    </div>

    <!-- ══════════════════════════════════════════ -->
    <!-- PASSO 1: UPLOAD DE RECEITA                 -->
    <!-- ══════════════════════════════════════════ -->
    <main v-if="passo === 1" class="setup-container">
      <div class="setup-content narrow">

        <div class="page-header">
          <div class="section-label">Passo 1 de 3</div>
          <h1>Antes de tudo,<br><em>sua receita médica.</em></h1>
          <p class="hero-sub">
            Ela garante a segurança do seu tratamento. Nosso farmacêutico revisa tudo
            antes de montar a sua box.
          </p>
        </div>

        <!-- ZONA DE UPLOAD -->
        <div class="upload-zone" :class="{ 'has-file': receitaArquivo, dragging: isDragging }"
          @dragover.prevent="isDragging = true" @dragleave.prevent="isDragging = false" @drop.prevent="onDrop"
          @click="$refs.fileInput.click()">
          <input ref="fileInput" type="file" accept="image/*,.pdf" hidden @change="onFileChange" />

          <template v-if="!receitaArquivo">
            <div class="upload-icon-wrap">
              <svg width="40" height="40" viewBox="0 0 40 40" fill="none">
                <rect width="40" height="40" rx="12" fill="#eff6ff" />
                <path d="M20 26V14M14 20l6-6 6 6" stroke="#1d4ed8" stroke-width="1.8" stroke-linecap="round"
                  stroke-linejoin="round" />
                <path d="M13 28h14" stroke="#1d4ed8" stroke-width="1.8" stroke-linecap="round" />
              </svg>
            </div>
            <p class="upload-title">Arraste sua receita aqui</p>
            <p class="upload-sub">ou <span class="upload-link">clique para selecionar</span></p>
            <p class="upload-hint">JPG, PNG ou PDF · Máx. 10 MB</p>
          </template>

          <template v-else>
            <div class="upload-icon-wrap success">
              <svg width="40" height="40" viewBox="0 0 40 40" fill="none">
                <rect width="40" height="40" rx="12" fill="#dbeafe" />
                <path d="M12 20l5.5 5.5L28 15" stroke="#1d4ed8" stroke-width="2" stroke-linecap="round"
                  stroke-linejoin="round" />
              </svg>
            </div>
            <p class="upload-title">Receita enviada!</p>
            <p class="upload-sub file-name">{{ receitaArquivo.name }}</p>
            <button class="upload-change" @click.stop="$refs.fileInput.click()">Trocar arquivo</button>
          </template>
        </div>

        <!-- OPÇÃO CONCIERGE -->
        <div class="concierge-card">
          <div class="concierge-badge">Serviço Premium</div>
          <div class="concierge-body">
            <div class="concierge-icon">◎</div>
            <div>
              <strong>Não quer preencher? A gente faz por você.</strong>
              <p>Mande só a foto da receita. Nosso farmacêutico monta a sua rotina completa e envia para aprovação antes
                de
                produzir.</p>
            </div>
          </div>
          <button class="btn-concierge" @click="ativarConcierge">
            Usar serviço Concierge
          </button>
        </div>

        <div class="step-actions">
          <button class="btn-primary" :disabled="!receitaArquivo" @click="avancarParaPasso2">
            Continuar para os medicamentos
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
              <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                stroke-linejoin="round" />
            </svg>
          </button>
          <p class="step-note">Sem contrato · Cancele quando quiser</p>
        </div>

      </div>
    </main>

    <!-- ══════════════════════════════════════════ -->
    <!-- PASSO 2: CARDÁPIO DE REMÉDIOS              -->
    <!-- ══════════════════════════════════════════ -->
    <main v-if="passo === 2" class="setup-container">
      <div class="setup-content wide">

        <div class="page-header">
          <div class="section-label">Passo 2 de 3</div>
          <h1>Escolha seus<br><em>medicamentos.</em></h1>
          <p class="hero-sub">
            Clique no remédio para adicioná-lo à sua rotina. Depois é só definir o horário.
          </p>
        </div>

        <div class="catalog-layout">

          <!-- CARDÁPIO -->
          <div class="catalog-col">

            <div class="search-wrap">
              <svg class="search-icon" width="16" height="16" viewBox="0 0 16 16" fill="none">
                <circle cx="7" cy="7" r="4.5" stroke="#9ca3af" stroke-width="1.5" />
                <path d="M10.5 10.5l3 3" stroke="#9ca3af" stroke-width="1.5" stroke-linecap="round" />
              </svg>
              <input v-model="busca" type="text" placeholder="Buscar medicamento..." class="search-input" />
            </div>

            <div class="cat-tabs">
              <button v-for="cat in categorias" :key="cat" class="cat-tab" :class="{ active: catAtiva === cat }"
                @click="catAtiva = cat">{{ cat }}</button>
            </div>

            <div class="med-grid">
              <button v-for="med in medicamentosFiltrados" :key="med.id" class="med-card"
                :class="{ selected: isSelecionado(med.id) }" @click="abrirModal(med)">
                <div class="med-card-top">
                  <span class="med-dot" :style="{ background: med.cor }"></span>
                  <span class="med-add-icon">
                    <svg v-if="!isSelecionado(med.id)" width="16" height="16" viewBox="0 0 16 16" fill="none">
                      <path d="M8 3v10M3 8h10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" />
                    </svg>
                    <svg v-else width="16" height="16" viewBox="0 0 16 16" fill="none">
                      <path d="M3 8l3.5 3.5L13 5" stroke="#1d4ed8" stroke-width="1.5" stroke-linecap="round"
                        stroke-linejoin="round" />
                    </svg>
                  </span>
                </div>
                <div class="med-name">{{ med.nome }}</div>
                <div class="med-dose">{{ med.dosagem }}</div>
                <div v-if="isSelecionado(med.id)" class="med-horarios">
                  <span v-for="s in sachesDo(med.id)" :key="s.horario" class="med-horario-tag">{{ s.horario }}</span>
                </div>
              </button>
            </div>

          </div>

          <!-- CARRINHO / ROTINA -->
          <div class="cart-col">
            <div class="cart-card">
              <div class="cart-header">
                <h3>Sua Rotina</h3>
                <span class="cart-count" v-if="sachesSelecionados.length">
                  {{ sachesSelecionados.length }} sachê(s)
                </span>
              </div>

              <div v-if="sachesSelecionados.length === 0" class="empty-state">
                <div class="empty-icon">◈</div>
                <p>Nenhum medicamento<br>adicionado ainda.</p>
              </div>

              <div v-else class="sache-list">
                <div v-for="(s, i) in sachesPorHorario" :key="i" class="sache-item">
                  <div class="sache-time">{{ s.horario }}</div>
                  <div class="sache-info">
                    <strong>{{ s.nome }}</strong>
                    <span>{{ s.quantidadeComprimidos }} comprimido(s)</span>
                  </div>
                  <button class="btn-remove" @click="removerSache(s._index)">
                    <svg width="12" height="12" viewBox="0 0 12 12" fill="none">
                      <path d="M2 2l8 8M10 2L2 10" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" />
                    </svg>
                  </button>
                </div>
              </div>

              <div class="cart-footer" :class="{ visible: sachesSelecionados.length > 0 }">
                <button @click="salvarBox" class="btn-primary">
                  Finalizar Assinatura
                  <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                    <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                      stroke-linejoin="round" />
                  </svg>
                </button>
                <p class="step-note">Sem contrato · Cancele quando quiser</p>
              </div>
            </div>

            <div class="receita-badge">
              <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                <path d="M2 7l3 3 7-6" stroke="#1d4ed8" stroke-width="1.5" stroke-linecap="round"
                  stroke-linejoin="round" />
              </svg>
              Receita enviada e aguardando revisão farmacêutica
            </div>
          </div>

        </div>
      </div>
    </main>

    <!-- ══════════════════════════════════════════ -->
    <!-- MODAL DE HORÁRIO                           -->
    <!-- ══════════════════════════════════════════ -->
    <Transition name="modal">
      <div v-if="modalAberto" class="modal-overlay" @click.self="fecharModal">
        <div class="modal-box">
          <button class="modal-close" @click="fecharModal">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
              <path d="M3 3l10 10M13 3L3 13" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" />
            </svg>
          </button>

          <div class="modal-med-info">
            <span class="modal-dot" :style="{ background: medModal?.cor }"></span>
            <div>
              <strong>{{ medModal?.nome }}</strong>
              <span>{{ medModal?.dosagem }}</span>
            </div>
          </div>

          <div class="modal-form">
            <div class="form-group">
              <label>Horário da tomada</label>
              <input type="time" v-model="modalHorario" />
            </div>
            <div class="form-group">
              <label>Quantidade por sachê</label>
              <div class="qty-control">
                <button @click="modalQtd = Math.max(1, modalQtd - 1)">−</button>
                <span>{{ modalQtd }}</span>
                <button @click="modalQtd++">+</button>
              </div>
            </div>
          </div>

          <button class="btn-primary" @click="confirmarMedicamento" :disabled="!modalHorario">
            Adicionar à rotina
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
              <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                stroke-linejoin="round" />
            </svg>
          </button>
        </div>
      </div>
    </Transition>

    <!-- FOOTER -->
    <footer class="footer">
      <div class="footer-inner">
        <a class="logo" href="#">
          <div class="logo-icon">
            <svg width="18" height="18" viewBox="0 0 18 18" fill="none">
              <path d="M9 3v12M3 9h12" stroke="#fff" stroke-width="2.2" stroke-linecap="round" />
            </svg>
          </div>
          <span class="logo-text">MedFlow</span>
        </a>
        <p>Feito com cuidado para quem cuida da saúde.</p>
        <div class="footer-links">
          <a href="#">Termos de uso</a>
          <a href="#">Privacidade</a>
          <a href="#">Contato</a>
        </div>
      </div>
    </footer>

  </div>
</template>

<script setup>
import { ref, computed, onMounted, onBeforeUnmount } from 'vue'
import { useApi } from '../composables/useApi'

// ── SCROLL ────────────────────────────────────
const isScrolled = ref(false)
const handleScroll = () => { isScrolled.value = window.scrollY > 40 }
onMounted(() => window.addEventListener('scroll', handleScroll))
onBeforeUnmount(() => window.removeEventListener('scroll', handleScroll))

// ── ESTADO GLOBAL ─────────────────────────────
const passo = ref(1)
const receitaArquivo = ref(null)
const isDragging = ref(false)

// ── CATÁLOGO DE MEDICAMENTOS ──────────────────
const medicamentosApi = ref([
  { id: 1, nome: 'Losartana', dosagem: '50 mg', categoria: 'Cardiológico', cor: '#bfdbfe' },
  { id: 2, nome: 'Atenolol', dosagem: '25 mg', categoria: 'Cardiológico', cor: '#bfdbfe' },
  { id: 3, nome: 'Metformina', dosagem: '500 mg', categoria: 'Metabólico', cor: '#fde68a' },
  { id: 4, nome: 'Omeprazol', dosagem: '20 mg', categoria: 'Gástrico', cor: '#d9f99d' },
  { id: 5, nome: 'Sinvastatina', dosagem: '20 mg', categoria: 'Cardiológico', cor: '#bfdbfe' },
  { id: 6, nome: 'Levotiroxina', dosagem: '50 mcg', categoria: 'Hormonal', cor: '#fecdd3' },
  { id: 7, nome: 'Clonazepam', dosagem: '2 mg', categoria: 'Neurológico', cor: '#e9d5ff' },
  { id: 8, nome: 'Escitalopram', dosagem: '10 mg', categoria: 'Neurológico', cor: '#e9d5ff' },
  { id: 9, nome: 'Vitamina D', dosagem: '2000 UI', categoria: 'Suplemento', cor: '#fed7aa' },
  { id: 10, nome: 'Ácido Fólico', dosagem: '5 mg', categoria: 'Suplemento', cor: '#fed7aa' },
  { id: 11, nome: 'AAS', dosagem: '100 mg', categoria: 'Cardiológico', cor: '#bfdbfe' },
  { id: 12, nome: 'Dipirona', dosagem: '500 mg', categoria: 'Analgésico', cor: '#ccfbf1' },
])

const categorias = computed(() => ['Todos', ...new Set(medicamentosApi.value.map(m => m.categoria))])
const catAtiva = ref('Todos')
const busca = ref('')

const medicamentosFiltrados = computed(() =>
  medicamentosApi.value.filter(m => {
    const matchCat = catAtiva.value === 'Todos' || m.categoria === catAtiva.value
    const matchBusca = m.nome.toLowerCase().includes(busca.value.toLowerCase())
    return matchCat && matchBusca
  })
)

// ── SACHÊS / CARRINHO ─────────────────────────
const sachesSelecionados = ref([])

const sachesPorHorario = computed(() =>
  [...sachesSelecionados.value]
    .map((s, i) => ({ ...s, _index: i }))
    .sort((a, b) => a.horario.localeCompare(b.horario))
)

const isSelecionado = (id) => sachesSelecionados.value.some(s => s.medicamentoId === id)
const sachesDo = (id) => sachesSelecionados.value.filter(s => s.medicamentoId === id)
const removerSache = (index) => sachesSelecionados.value.splice(index, 1)

// ── MODAL ─────────────────────────────────────
const modalAberto = ref(false)
const medModal = ref(null)
const modalHorario = ref('')
const modalQtd = ref(1)

const abrirModal = (med) => {
  medModal.value = med
  modalHorario.value = ''
  modalQtd.value = 1
  modalAberto.value = true
}
const fecharModal = () => { modalAberto.value = false }

const confirmarMedicamento = () => {
  if (!modalHorario.value) return
  sachesSelecionados.value.push({
    medicamentoId: medModal.value.id,
    nome: medModal.value.nome,
    horario: modalHorario.value,
    quantidadeComprimidos: modalQtd.value
  })
  fecharModal()
}

// ── PASSO 1: UPLOAD ───────────────────────────
const onFileChange = (e) => {
  const file = e.target.files[0]
  if (file) receitaArquivo.value = file
}
const onDrop = (e) => {
  isDragging.value = false
  const file = e.dataTransfer.files[0]
  if (file) receitaArquivo.value = file
}
const avancarParaPasso2 = () => { passo.value = 2 }
const ativarConcierge = () => {
  alert('Serviço Concierge ativado! Um farmacêutico entrará em contato em até 24h.')
}

// ── SALVAR ────────────────────────────────────
const { get, post } = useApi()

const salvarBox = async () => {
  const payload = {
    usuarioId: 1,
    receitaId: 1,
    saches: sachesSelecionados.value.map(s => ({
      medicamentoId: s.medicamentoId,
      horario: s.horario,
      quantidadeComprimidos: s.quantidadeComprimidos
    }))
  }
  try {
    await post('/Tratamento', payload)
    alert('Sua Box MedFlow foi configurada!')
    sachesSelecionados.value = []
  } catch {
    alert('Erro ao salvar a Box.')
  }
}

onMounted(async () => {
  try {
    medicamentosApi.value = await get('/Medicamento')
  } catch { /* usa dados mockados */ }
})
</script>

<style scoped>
/* ─── TOKENS ─────────────────────────────────── */
.setup-layout {
  --blue: #1d4ed8;
  --blue-hover: #1e40af;
  --blue-pale: #eff6ff;
  --blue-pale-border: #bfdbfe;
  --text: #0a0a0a;
  --text-secondary: #1f2937;
  --muted: #374151;
  --border: #d1d5db;
  --white: #ffffff;
  --bg-subtle: #f8fafc;
  --radius: 16px;
  --font-display: 'Georgia', serif;
  --font-body: 'DM Sans', 'Helvetica Neue', sans-serif;
  --transition: 0.2s ease;

  min-height: 100vh;
  background: var(--white);
  font-family: var(--font-body);
  color: var(--text);
  overflow-x: hidden;
}

* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

/* ─── NAVBAR ─────────────────────────────────── */
.navbar {
  position: sticky;
  top: 0;
  z-index: 200;
  transition: background var(--transition), box-shadow var(--transition);
}

.navbar.scrolled .nav-inner {
  background: rgba(255, 255, 255, 0.97);
  backdrop-filter: blur(16px);
  box-shadow: 0 4px 24px rgba(0, 0, 0, 0.07);
  border-bottom: 1px solid #e2e8f0;
}

.nav-inner {
  max-width: 1200px;
  margin: 0 auto;
  padding: 12px 32px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 16px;
}

.logo {
  display: flex;
  align-items: center;
  gap: 10px;
  text-decoration: none;
  flex-shrink: 0;
}

.logo-icon {
  width: 34px;
  height: 34px;
  background: #1d4ed8;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}

.logo-text {
  font-size: 17px;
  font-weight: 700;
  color: #0a0a0a;
  letter-spacing: -0.3px;
}

.logo-badge {
  font-size: 10px;
  font-weight: 700;
  letter-spacing: 0.06em;
  text-transform: uppercase;
  background: #eff6ff;
  color: #1d4ed8;
  border: 1px solid #bfdbfe;
  border-radius: 100px;
  padding: 2px 8px;
}

.nav-actions {
  display: flex;
  align-items: center;
  gap: 8px;
}

.btn-back {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  background: none;
  border: 1px solid #e2e8f0;
  color: var(--muted);
  font-size: 14px;
  font-weight: 500;
  padding: 8px 14px;
  border-radius: 8px;
  cursor: pointer;
  font-family: var(--font-body);
  transition: background var(--transition), color var(--transition), border-color var(--transition);
}

.btn-back:hover {
  background: #f1f5f9;
  color: var(--text);
  border-color: #d1d5db;
}

/* ─── STEPS BAR ──────────────────────────────── */
.steps-bar {
  border-bottom: 1px solid var(--border);
  background: var(--white);
  padding: 18px 32px;
}

.steps-inner {
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  align-items: center;
}

.step-pill {
  display: flex;
  align-items: center;
  gap: 10px;
  opacity: 0.35;
  transition: opacity var(--transition);
}

.step-pill.active {
  opacity: 1;
}

.step-pill.done {
  opacity: 0.6;
}

.pill-num {
  font-size: 12px;
  font-weight: 800;
  color: var(--blue);
  width: 28px;
  height: 28px;
  border-radius: 50%;
  border: 2px solid var(--blue);
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  transition: background var(--transition);
}

.step-pill.done .pill-num {
  background: var(--blue);
  color: white;
}

.step-pill:not(.active):not(.done) .pill-num {
  color: var(--muted);
  border-color: var(--border);
}

.pill-label {
  font-size: 13px;
  font-weight: 600;
  color: var(--text-secondary);
}

.step-divider {
  flex: 1;
  height: 1px;
  background: var(--border);
  margin: 0 16px;
  max-width: 64px;
  transition: background var(--transition);
}

.step-divider.done {
  background: #bfdbfe;
}

/* ─── CONTAINER PRINCIPAL ────────────────────── */
.setup-container {
  padding: 60px 32px 80px;
}

.setup-content {
  margin: 0 auto;
}

.setup-content.narrow {
  max-width: 560px;
}

.setup-content.wide {
  max-width: 1200px;
}

/* ─── CABEÇALHO ──────────────────────────────── */
.page-header {
  margin-bottom: 48px;
}

.section-label {
  font-size: 12px;
  font-weight: 700;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: var(--blue);
  margin-bottom: 14px;
}

.page-header h1 {
  font-family: var(--font-display);
  font-size: clamp(2rem, 4vw, 3rem);
  line-height: 1.15;
  letter-spacing: -0.02em;
  color: var(--text);
  font-weight: 700;
  margin-bottom: 16px;
}

.page-header h1 em {
  font-style: italic;
  color: var(--blue);
}

.hero-sub {
  font-size: 16px;
  color: var(--muted);
  line-height: 1.7;
}

/* ─── UPLOAD ZONE ────────────────────────────── */
.upload-zone {
  border: 2px dashed var(--border);
  border-radius: var(--radius);
  padding: 56px 32px;
  text-align: center;
  cursor: pointer;
  transition: border-color var(--transition), background var(--transition);
  margin-bottom: 24px;
}

.upload-zone:hover,
.upload-zone.dragging {
  border-color: var(--blue);
  background: var(--blue-pale);
}

.upload-zone.has-file {
  border-style: solid;
  border-color: #bfdbfe;
  background: var(--blue-pale);
}

.upload-icon-wrap {
  display: inline-flex;
  margin-bottom: 20px;
  border-radius: 14px;
  overflow: hidden;
}

.upload-title {
  font-size: 17px;
  font-weight: 700;
  color: var(--text);
  margin-bottom: 6px;
}

.upload-sub {
  font-size: 14px;
  color: var(--muted);
  margin-bottom: 10px;
}

.upload-link {
  color: var(--blue);
  text-decoration: underline;
  font-weight: 600;
}

.upload-hint {
  font-size: 12px;
  color: #9ca3af;
}

.file-name {
  font-weight: 600;
  color: var(--blue);
}

.upload-change {
  margin-top: 8px;
  background: none;
  border: none;
  color: var(--muted);
  font-size: 13px;
  text-decoration: underline;
  cursor: pointer;
  font-family: var(--font-body);
}

/* ─── CONCIERGE CARD ─────────────────────────── */
.concierge-card {
  background: #0f172a;
  border-radius: var(--radius);
  padding: 28px;
  margin-bottom: 36px;
  position: relative;
  overflow: hidden;
}

.concierge-card::before {
  content: '';
  position: absolute;
  top: -40px;
  right: -40px;
  width: 160px;
  height: 160px;
  background: radial-gradient(circle, rgba(59, 130, 246, 0.2) 0%, transparent 70%);
  border-radius: 50%;
}

.concierge-badge {
  display: inline-flex;
  background: #1d4ed8;
  color: #bfdbfe;
  font-size: 10px;
  font-weight: 800;
  letter-spacing: 0.1em;
  text-transform: uppercase;
  padding: 4px 12px;
  border-radius: 100px;
  margin-bottom: 16px;
}

.concierge-body {
  display: flex;
  gap: 16px;
  margin-bottom: 20px;
}

.concierge-icon {
  font-size: 22px;
  color: #60a5fa;
  flex-shrink: 0;
  margin-top: 2px;
}

.concierge-body strong {
  display: block;
  color: #ffffff;
  font-size: 15px;
  margin-bottom: 6px;
}

.concierge-body p {
  color: #93c5fd;
  font-size: 14px;
  line-height: 1.6;
}

.btn-concierge {
  width: 100%;
  background: rgba(255, 255, 255, 0.08);
  color: #ffffff;
  border: 1px solid rgba(255, 255, 255, 0.15);
  padding: 12px 20px;
  border-radius: 100px;
  font-size: 14px;
  font-weight: 700;
  cursor: pointer;
  font-family: var(--font-body);
  transition: background var(--transition);
}

.btn-concierge:hover {
  background: rgba(255, 255, 255, 0.14);
}

/* ─── BOTÃO PRIMÁRIO ─────────────────────────── */
.btn-primary {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  background: var(--blue);
  color: var(--white);
  border: none;
  padding: 15px 28px;
  border-radius: 10px;
  font-size: 15px;
  font-weight: 600;
  cursor: pointer;
  font-family: var(--font-body);
  transition: background var(--transition), transform var(--transition);
  width: 100%;
}

.btn-primary:hover:not(:disabled) {
  background: var(--blue-hover);
  transform: translateY(-1px);
}

.btn-primary:disabled {
  opacity: 0.4;
  cursor: not-allowed;
}

.step-actions {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 14px;
}

.step-note {
  font-size: 12px;
  color: #9ca3af;
  font-weight: 500;
}

/* ─── CATALOG LAYOUT ─────────────────────────── */
.catalog-layout {
  display: grid;
  grid-template-columns: 1fr 340px;
  gap: 32px;
  align-items: start;
}

/* ─── BUSCA ──────────────────────────────────── */
.search-wrap {
  position: relative;
  margin-bottom: 16px;
}

.search-icon {
  position: absolute;
  left: 14px;
  top: 50%;
  transform: translateY(-50%);
  pointer-events: none;
}

.search-input {
  width: 100%;
  padding: 11px 16px 11px 40px;
  border: 1.5px solid var(--border);
  border-radius: 10px;
  font-size: 14px;
  font-family: var(--font-body);
  outline: none;
  color: var(--text);
  transition: border-color var(--transition);
}

.search-input:focus {
  border-color: var(--blue);
}

/* ─── CATEGORY TABS ──────────────────────────── */
.cat-tabs {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
  margin-bottom: 24px;
}

.cat-tab {
  background: var(--bg-subtle);
  border: 1.5px solid var(--border);
  color: var(--muted);
  font-size: 13px;
  font-weight: 600;
  padding: 6px 16px;
  border-radius: 100px;
  cursor: pointer;
  font-family: var(--font-body);
  transition: all var(--transition);
}

.cat-tab:hover {
  border-color: var(--blue);
  color: var(--blue);
}

.cat-tab.active {
  background: var(--blue-pale);
  border-color: #bfdbfe;
  color: var(--blue);
}

/* ─── MED GRID ───────────────────────────────── */
.med-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(160px, 1fr));
  gap: 12px;
}

.med-card {
  background: var(--white);
  border: 1.5px solid var(--border);
  border-radius: 14px;
  padding: 18px;
  text-align: left;
  cursor: pointer;
  font-family: var(--font-body);
  transition: box-shadow var(--transition), border-color var(--transition), transform var(--transition);
}

.med-card:hover {
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.08);
  border-color: #9ca3af;
  transform: translateY(-2px);
}

.med-card.selected {
  border-color: #bfdbfe;
  background: var(--blue-pale);
}

.med-card-top {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 14px;
}

.med-dot {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  flex-shrink: 0;
}

.med-add-icon {
  color: #9ca3af;
  transition: color var(--transition);
}

.med-card.selected .med-add-icon {
  color: var(--blue);
}

.med-card:hover .med-add-icon {
  color: var(--text);
}

.med-name {
  font-size: 14px;
  font-weight: 700;
  color: var(--text);
  margin-bottom: 4px;
  line-height: 1.3;
}

.med-dose {
  font-size: 12px;
  color: var(--muted);
  font-weight: 500;
}

.med-horarios {
  display: flex;
  flex-wrap: wrap;
  gap: 4px;
  margin-top: 10px;
}

.med-horario-tag {
  background: #dbeafe;
  color: var(--blue);
  font-size: 11px;
  font-weight: 700;
  padding: 2px 8px;
  border-radius: 100px;
}

/* ─── CART ───────────────────────────────────── */
.cart-card {
  background: var(--white);
  border: 1.5px solid var(--border);
  border-radius: var(--radius);
  padding: 28px;
  position: sticky;
  top: 88px;
}

.cart-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 20px;
  padding-bottom: 16px;
  border-bottom: 1px solid #f0f0f0;
}

.cart-header h3 {
  font-family: var(--font-display);
  font-size: 1.15rem;
  font-weight: 700;
  color: var(--text);
}

.cart-count {
  background: var(--blue-pale);
  border: 1px solid #bfdbfe;
  color: var(--blue);
  font-size: 11px;
  font-weight: 700;
  padding: 3px 10px;
  border-radius: 100px;
}

.empty-state {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 12px;
  text-align: center;
  padding: 40px 16px;
  background: var(--bg-subtle);
  border: 1.5px dashed var(--border);
  border-radius: 12px;
}

.empty-icon {
  font-size: 24px;
  color: #9ca3af;
}

.empty-state p {
  font-size: 13px;
  color: #9ca3af;
  line-height: 1.6;
}

.sache-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.sache-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 12px 14px;
  background: var(--blue-pale);
  border: 1px solid #bfdbfe;
  border-radius: 10px;
  animation: fadeUp 0.25s ease both;
}

@keyframes fadeUp {
  from {
    opacity: 0;
    transform: translateY(6px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.sache-time {
  background: var(--blue);
  color: white;
  padding: 4px 10px;
  border-radius: 100px;
  font-weight: 700;
  font-size: 12px;
  flex-shrink: 0;
}

.sache-info {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.sache-info strong {
  font-size: 13px;
  font-weight: 700;
  color: var(--text);
}

.sache-info span {
  font-size: 11px;
  color: var(--muted);
}

.btn-remove {
  background: none;
  border: none;
  color: #ef4444;
  cursor: pointer;
  padding: 5px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background var(--transition);
}

.btn-remove:hover {
  background: #fee2e2;
}

.cart-footer {
  margin-top: 20px;
  padding-top: 20px;
  border-top: 1px solid var(--border);
  display: none;
  flex-direction: column;
  align-items: center;
  gap: 10px;
}

.cart-footer.visible {
  display: flex;
}

.receita-badge {
  display: flex;
  align-items: center;
  gap: 8px;
  background: var(--blue-pale);
  border: 1px solid #bfdbfe;
  color: var(--blue);
  font-size: 12px;
  font-weight: 600;
  padding: 10px 16px;
  border-radius: 10px;
  margin-top: 12px;
}

/* ─── MODAL ──────────────────────────────────── */
.modal-overlay {
  position: fixed;
  inset: 0;
  z-index: 300;
  background: rgba(10, 10, 10, 0.5);
  backdrop-filter: blur(4px);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 24px;
}

.modal-box {
  background: var(--white);
  border-radius: 20px;
  padding: 36px;
  width: 100%;
  max-width: 400px;
  position: relative;
  box-shadow: 0 24px 64px rgba(0, 0, 0, 0.2);
}

.modal-close {
  position: absolute;
  top: 20px;
  right: 20px;
  background: var(--bg-subtle);
  border: none;
  width: 32px;
  height: 32px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  color: var(--muted);
  transition: background var(--transition);
}

.modal-close:hover {
  background: #e5e7eb;
}

.modal-med-info {
  display: flex;
  align-items: center;
  gap: 14px;
  margin-bottom: 28px;
  padding-bottom: 24px;
  border-bottom: 1px solid #f0f0f0;
}

.modal-dot {
  width: 14px;
  height: 14px;
  border-radius: 50%;
  flex-shrink: 0;
}

.modal-med-info strong {
  display: block;
  font-size: 16px;
  font-weight: 700;
  color: var(--text);
  margin-bottom: 3px;
}

.modal-med-info span {
  font-size: 13px;
  color: var(--muted);
}

.modal-form {
  margin-bottom: 24px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-bottom: 18px;
}

label {
  font-size: 13px;
  font-weight: 700;
  color: var(--text-secondary);
}

input[type="time"] {
  width: 100%;
  padding: 12px 16px;
  border: 1.5px solid var(--border);
  border-radius: 10px;
  font-size: 16px;
  font-family: var(--font-body);
  outline: none;
  color: var(--text);
  transition: border-color var(--transition);
}

input[type="time"]:focus {
  border-color: var(--blue);
}

.qty-control {
  display: flex;
  align-items: center;
  border: 1.5px solid var(--border);
  border-radius: 10px;
  overflow: hidden;
  width: fit-content;
}

.qty-control button {
  background: var(--bg-subtle);
  border: none;
  width: 44px;
  height: 44px;
  font-size: 20px;
  font-weight: 300;
  cursor: pointer;
  color: var(--text);
  transition: background var(--transition);
  display: flex;
  align-items: center;
  justify-content: center;
}

.qty-control button:hover {
  background: #e5e7eb;
}

.qty-control span {
  width: 52px;
  text-align: center;
  font-size: 16px;
  font-weight: 700;
  color: var(--text);
  border-left: 1.5px solid var(--border);
  border-right: 1.5px solid var(--border);
  padding: 12px 0;
}

/* ─── MODAL TRANSITION ───────────────────────── */
.modal-enter-active {
  transition: opacity 0.2s ease;
}

.modal-leave-active {
  transition: opacity 0.15s ease;
}

.modal-enter-from,
.modal-leave-to {
  opacity: 0;
}

.modal-enter-active .modal-box {
  animation: modalPop 0.25s cubic-bezier(0.34, 1.56, 0.64, 1) both;
}

@keyframes modalPop {
  from {
    transform: scale(0.92) translateY(12px);
    opacity: 0;
  }

  to {
    transform: scale(1) translateY(0);
    opacity: 1;
  }
}

/* ─── FOOTER ─────────────────────────────────── */
.footer {
  padding: 40px 0;
  border-top: 1px solid var(--border);
}

.footer-inner {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 32px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  flex-wrap: wrap;
  gap: 16px;
}

.footer p {
  font-size: 14px;
  color: var(--text-secondary);
  font-weight: 500;
}

.footer-links {
  display: flex;
  gap: 24px;
}

.footer-links a {
  font-size: 13px;
  color: var(--muted);
  text-decoration: none;
  font-weight: 500;
  transition: color var(--transition);
}

.footer-links a:hover {
  color: var(--text);
}

/* ─── RESPONSIVIDADE ─────────────────────────── */
@media (max-width: 1024px) {
  .catalog-layout {
    grid-template-columns: 1fr;
  }

  .cart-card {
    position: static;
  }
}

@media (max-width: 600px) {
  .nav-inner {
    padding: 10px 16px;
  }

  .logo-badge {
    display: none;
  }

  .setup-container {
    padding: 32px 16px 60px;
  }

  .med-grid {
    grid-template-columns: repeat(2, 1fr);
  }

  .pill-label {
    display: none;
  }

  .step-divider {
    max-width: 32px;
    margin: 0 8px;
  }

  .steps-bar {
    padding: 14px 16px;
  }

  .upload-zone {
    padding: 40px 20px;
  }

  .footer-inner {
    flex-direction: column;
    text-align: center;
  }

  .footer-links {
    justify-content: center;
  }
}
</style>