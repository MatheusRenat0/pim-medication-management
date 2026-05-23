<template>
  <div class="page">
    <aside class="panel-left">
      <div class="logo"><img src="/logo.png" alt="MedFlow" class="logo-mark-img" /><span class="logo-name">MedFlow</span></div>
      <div class="panel-body">
        <h1 class="headline">Comece a <em>cuidar</em><br>da sua saúde hoje.</h1>
        <p class="tagline">Cadastro em menos de 2 minutos.<br>Sem burocracia, sem contrato.</p>        <!-- Sachet Mockup Card Carousel -->
        <div class="carousel-container">
          <Transition name="card-fade" mode="out-in">
            <div :key="currentCard.title" class="sachet-mockup-card">
              <div class="card-glow"></div>
              <div class="sachet-header">
                <span class="sachet-brand">MedFlow Box</span>
                <span class="sachet-time-badge">{{ currentCard.time }}</span>
              </div>
              <div class="sachet-body">
                <span class="sachet-patient">PACIENTE: {{ form.nome || 'Maria Paciente' }}</span>
                <h3 class="sachet-med-title">{{ currentCard.title }}</h3>
                <p class="sachet-med-desc">{{ currentCard.desc }}</p>
                <div class="sachet-footer-row">
                  <span class="sachet-date">{{ currentCard.day }}</span>
                  <span class="sachet-qty">Qtd: {{ currentCard.qty }}</span>
                </div>
              </div>
              <div class="sachet-barcode">
                <div class="bar"></div>
                <div class="bar"></div>
                <div class="bar"></div>
                <div class="bar"></div>
                <div class="bar"></div>
                <div class="bar"></div>
                <div class="bar"></div>
              </div>
            </div>
          </Transition>
        </div>
      </div>
      <div class="panel-slogan">
        <svg width="14" height="14" viewBox="0 0 14 14" fill="none"><path d="M2 7l3 3 7-6" stroke="#60A5FA" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/></svg>
        Sem contrato · Cancele quando quiser
      </div>
    </aside>
    <main class="panel-right">
      <div class="form-container">
        <button class="btn-back" @click="$router.push('/login')">
          <svg width="14" height="14" viewBox="0 0 16 16" fill="none"><path d="M10 3L5 8l5 5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/></svg>
          Voltar para login
        </button>
        <Transition name="fade">
          <div v-if="success" class="success">
            <div class="success-icon"><svg width="26" height="26" viewBox="0 0 24 24" fill="none"><path d="M5 13l4 4L19 7" stroke="#2563EB" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/></svg></div>
            <h2>Conta criada!</h2>
            <p>Bem-vindo ao MedFlow. Acesse o painel agora.</p>
            <button class="btn-primary" @click="irParaDashboard">Acessar Dashboard
              <svg width="15" height="15" viewBox="0 0 16 16" fill="none"><path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/></svg>
            </button>
          </div>
        </Transition>
        <Transition name="fade">
          <div v-if="!success">
            <div class="form-header"><h2>Crie sua conta</h2><p>Junte-se ao MedFlow.</p></div>
            <form @submit.prevent="fazerCadastro" novalidate>
              <div class="field"><label for="nome">Nome completo</label><input id="nome" type="text" v-model="form.nome" placeholder="Ex: João da Silva" :class="{ error: errors.nome }" @blur="validate('nome')"><span class="err" v-if="errors.nome">{{ errors.nome }}</span></div>
              <div class="row-2">
                <div class="field"><label for="cpf">CPF</label><input id="cpf" type="text" v-model="form.cpf" placeholder="000.000.000-00" maxlength="14" :class="{ error: errors.cpf }" @input="maskCpf" @blur="validate('cpf')"><span class="err" v-if="errors.cpf">{{ errors.cpf }}</span></div>
                <div class="field"><label for="email">E-mail</label><input id="email" type="email" v-model="form.email" placeholder="seu@email.com" :class="{ error: errors.email }" @blur="validate('email')"><span class="err" v-if="errors.email">{{ errors.email }}</span></div>
              </div>
              <div class="field"><label for="senha">Senha</label>
                <div class="pass-wrap"><input id="senha" :type="showPass?'text':'password'" v-model="form.senha" placeholder="Crie uma senha forte" :class="{ error: errors.senha }" @blur="validate('senha')"><button type="button" class="pass-toggle" @click="showPass=!showPass"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/><circle cx="12" cy="12" r="3"/></svg></button></div>
                <span class="err" v-if="errors.senha">{{ errors.senha }}</span>
              </div>
              <div v-if="regError" class="alert-error">{{ regError }}</div>
              <button type="submit" class="btn-primary" :disabled="loading"><span v-if="!loading">Criar conta</span><span v-else class="spin"></span></button>
            </form>
            <p class="login-link">Já tem conta? <a href="#" @click.prevent="$router.push('/login')">Faça login</a></p>
          </div>
        </Transition>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useAuthStore } from '../../stores/auth'
const router = useRouter()
const route = useRoute()
const auth = useAuthStore()
const showPass = ref(false)
const loading = ref(false)
const success = ref(false)
const regError = ref('')
const form = reactive({ nome: '', cpf: '', email: '', senha: '' })
const errors = reactive({ nome: '', cpf: '', email: '', senha: '' })

const sachetCards = [
  { title: 'Atorvastatina 20mg', time: '08:00 Manhã', desc: 'Tomar 1 comprimido com água.', day: 'Segunda-feira', qty: '1 comp.' },
  { title: 'Metformina 850mg', time: '12:00 Almoço', desc: 'Tomar junto com a refeição.', day: 'Terça-feira', qty: '1 comp.' },
  { title: 'Losartana Potássica 50mg', time: '20:00 Noite', desc: 'Uso contínuo para controle da pressão.', day: 'Quarta-feira', qty: '1 comp.' },
  { title: 'Melatonina 0.2mg', time: '22:00 Dormir', desc: 'Auxílio para indução do sono.', day: 'Quinta-feira', qty: '1 comp.' }
]

const currentCardIndex = ref(Math.floor(Math.random() * sachetCards.length))
const currentCard = computed(() => sachetCards[currentCardIndex.value])

onMounted(() => {
  setInterval(() => {
    currentCardIndex.value = (currentCardIndex.value + 1) % sachetCards.length
  }, 4000)
})

function maskCpf() { let v = form.cpf.replace(/\D/g, '').slice(0, 11); v = v.replace(/(\d{3})(\d)/, '$1.$2'); v = v.replace(/(\d{3})(\d)/, '$1.$2'); v = v.replace(/(\d{3})(\d{1,2})$/, '$1-$2'); form.cpf = v }
function validate(f) { errors[f] = ''; if (f==='nome' && !form.nome.trim()) errors.nome='Informe seu nome.'; if (f==='cpf' && form.cpf.replace(/\D/g,'').length<11) errors.cpf='CPF inválido.'; if (f==='email' && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) errors.email='E-mail inválido.'; if (f==='senha' && form.senha.length<6) errors.senha='Mínimo 6 caracteres.' }
function validateAll() { ['nome','cpf','email','senha'].forEach(validate); return Object.values(errors).every(e => !e) }

const fazerCadastro = async () => {
  regError.value = ''
  if (!validateAll()) return
  loading.value = true
  try {
    await auth.register(form.nome, form.email, form.cpf, form.senha)
    loading.value = false
    success.value = true
  } catch (err) {
    regError.value = err.message || 'Erro ao cadastrar.'
    loading.value = false
  }
}

const irParaDashboard = () => {
  const redirect = route.query.redirect || (auth.userType === 'Paciente' ? '/receitas' : auth.userType === 'Entregador' ? '/entregas' : '/dashboard')
  router.push(redirect)
}
</script>

<style scoped>
*,*::before,*::after{box-sizing:border-box;margin:0;padding:0}
.page{display:grid;grid-template-columns:1fr 1fr;min-height:100vh;font-family:'Inter','DM Sans',sans-serif;color:#0a0a0a;background:#fff}
.panel-left{background:#1E3A8A;display:flex;flex-direction:column;padding:44px 48px;min-height:100vh}
.logo{display:flex;align-items:center;gap:10px;margin-bottom:52px}
.logo-mark-img{width:38px;height:38px;border-radius:9px;object-fit:contain;background:#fff;border:1.5px solid rgba(255,255,255,0.15)}
.logo-name{font-size:18px;font-weight:600;color:#fff;letter-spacing:-0.3px}
.panel-body{flex:1}
.headline{font-family:Georgia,serif;font-size:clamp(1.6rem,2.2vw,2.2rem);line-height:1.2;color:#fff;font-weight:700;letter-spacing:-0.03em;margin-bottom:14px}
.headline em{font-style:italic;color:#93C5FD}
.tagline{font-size:14px;color:#93B4F5;line-height:1.7;margin-bottom:32px}

/* Sachet Mockup Card */
.sachet-mockup-card {
  position: relative;
  background: rgba(255, 255, 255, 0.08);
  border: 1.5px solid rgba(255, 255, 255, 0.15);
  border-radius: 20px;
  padding: 24px;
  max-width: 320px;
  box-shadow: 0 20px 50px rgba(0, 0, 0, 0.25);
  backdrop-filter: blur(12px);
  overflow: hidden;
  animation: floatCard 5s ease-in-out infinite;
}

@keyframes floatCard {
  0%, 100% { transform: translateY(0) rotate(0deg); }
  50% { transform: translateY(-8px) rotate(1deg); }
}

.card-glow {
  position: absolute;
  top: -50%;
  left: -50%;
  width: 200%;
  height: 200%;
  background: radial-gradient(circle, rgba(96, 165, 250, 0.15) 0%, transparent 60%);
  pointer-events: none;
}

.sachet-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
  padding-bottom: 12px;
}

.sachet-brand {
  font-size: 11px;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 1px;
  color: #93c5fd;
}

.sachet-time-badge {
  font-size: 11px;
  font-weight: 600;
  background: rgba(255, 255, 255, 0.15);
  color: #fff;
  padding: 3px 10px;
  border-radius: 100px;
}

.sachet-body {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-bottom: 20px;
}

.sachet-patient {
  font-size: 10px;
  font-weight: 700;
  color: rgba(255, 255, 255, 0.5);
  letter-spacing: 0.5px;
}

.sachet-med-title {
  font-family: Georgia, serif;
  font-size: 18px;
  font-weight: 700;
  color: #fff;
}

.sachet-med-desc {
  font-size: 12px;
  color: #93b4f5;
  line-height: 1.5;
}

.sachet-footer-row {
  display: flex;
  justify-content: space-between;
  font-size: 11px;
  color: rgba(255, 255, 255, 0.6);
  margin-top: 8px;
  font-weight: 500;
}

.sachet-barcode {
  display: flex;
  gap: 3px;
  justify-content: center;
  opacity: 0.35;
  height: 24px;
}

.sachet-barcode .bar {
  width: 2px;
  background-color: #fff;
}
.sachet-barcode .bar:nth-child(2n) { width: 4px; }
.sachet-barcode .bar:nth-child(3n) { width: 1px; }
.sachet-barcode .bar:nth-child(4n) { width: 5px; }

/* Slogan badge */
.panel-slogan{display:inline-flex;align-items:center;gap:8px;font-size:12px;color:#60A5FA;font-weight:600;background:rgba(96,165,250,0.1);padding:8px 16px;border-radius:100px;border:1px solid rgba(96,165,250,0.2);margin-top:32px}

.panel-right{background:#fff;display:flex;justify-content:center;padding:48px 40px;overflow-y:auto}
.form-container{width:100%;max-width:400px}
.btn-back{display:inline-flex;align-items:center;gap:6px;background:none;border:none;color:#6b7280;font-size:13px;font-weight:500;cursor:pointer;padding:0;margin-bottom:36px;font-family:inherit;transition:color .15s}
.btn-back:hover{color:#1E3A8A}
.form-header{margin-bottom:28px}
.form-header h2{font-family:Georgia,serif;font-size:1.5rem;font-weight:700;color:#0a0a0a;letter-spacing:-0.02em;margin-bottom:5px}
.form-header p{font-size:13px;color:#6b7280}
.field{display:flex;flex-direction:column;gap:6px;margin-bottom:16px}
.field label{font-size:13px;font-weight:600;color:#374151}
.field input{padding:12px 14px;border-radius:10px;border:1.5px solid #d1d5db;font-size:14px;color:#0a0a0a;background:#fff;outline:none;transition:border-color .15s;font-family:inherit;width:100%}
.field input:focus{border-color:#2563EB}
.field input.error{border-color:#ef4444}
.field input::placeholder{color:#9ca3af}
.err{font-size:11px;color:#ef4444}
.row-2{display:grid;grid-template-columns:1fr 1fr;gap:12px}
.pass-wrap{position:relative}
.pass-wrap input{padding-right:42px}
.pass-toggle{position:absolute;right:12px;top:50%;transform:translateY(-50%);background:none;border:none;cursor:pointer;color:#9ca3af;display:flex;padding:0;transition:color .15s}
.pass-toggle:hover{color:#2563EB}
.alert-error{display:flex;align-items:center;gap:8px;background:#fef2f2;border:1px solid #fecaca;border-radius:8px;padding:10px 14px;font-size:13px;color:#dc2626;margin-bottom:16px}
.btn-primary{width:100%;background:#2563EB;color:#fff;border:none;padding:14px 24px;border-radius:100px;font-size:14px;font-weight:600;cursor:pointer;display:flex;align-items:center;justify-content:center;gap:8px;transition:background .2s,transform .15s;font-family:inherit}
.btn-primary:hover:not(:disabled){background:#1D4ED8;transform:translateY(-1px)}
.btn-primary:disabled{opacity:.7;cursor:not-allowed}
.spin{width:16px;height:16px;border:2px solid rgba(255,255,255,.3);border-top-color:#fff;border-radius:50%;animation:spin .7s linear infinite}
@keyframes spin{to{transform:rotate(360deg)}}
.login-link{text-align:center;margin-top:20px;font-size:13px;color:#6b7280}
.login-link a{color:#2563EB;font-weight:600;text-decoration:none}
.login-link a:hover{text-decoration:underline}
.success{text-align:center;padding:20px 0}
.success-icon{width:58px;height:58px;background:#EFF6FF;border:2px solid #BFDBFE;border-radius:50%;display:flex;align-items:center;justify-content:center;margin:0 auto 20px}
.success h2{font-family:Georgia,serif;font-size:1.4rem;color:#0a0a0a;margin-bottom:8px;letter-spacing:-0.02em}
.success p{font-size:14px;color:#6b7280;margin-bottom:28px;line-height:1.6}
.success .btn-primary{max-width:240px;margin:0 auto}
.fade-enter-active,.fade-leave-active{transition:opacity .2s ease}
.fade-enter-from,.fade-leave-to{opacity:0}
@media(max-width:768px){.page{grid-template-columns:1fr}.panel-left{display:none}.panel-right{padding:40px 24px;min-height:100vh}}
</style>
