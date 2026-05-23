<template>
  <div class="page">
    <aside class="panel-left">
      <div class="logo"><img src="/logo.png" alt="MedFlow" class="logo-mark-img" /><span class="logo-name">MedFlow</span></div>
      <div class="panel-body">
        <h1 class="headline">Sua saúde,<br><em>organizada</em><br>pra você.</h1>
        <p class="tagline">Medicamentos separados por dia e horário,<br>entregues direto na sua porta todo mês.</p>
        <!-- Sachet Mockup Card Carousel -->
        <div class="carousel-container">
          <Transition name="card-fade" mode="out-in">
            <div :key="currentCard.title" class="sachet-mockup-card">
              <div class="card-glow"></div>
              <div class="sachet-header">
                <span class="sachet-brand">MedFlow Box</span>
                <span class="sachet-time-badge">{{ currentCard.time }}</span>
              </div>
              <div class="sachet-body">
                <span class="sachet-patient">PACIENTE: Maria Paciente</span>
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
        <button class="btn-back" @click="$router.push('/')">← Voltar para a home</button>
        <div class="form-header"><h2>Bem-vindo de volta</h2><p>Entre na sua conta para acessar o sistema.</p></div>
        <form @submit.prevent="fazerLogin" novalidate>
          <div class="field"><label for="email">E-mail</label><input id="email" type="email" v-model="form.email" placeholder="seu@email.com" :class="{ error: errors.email }" @blur="validate('email')"><span class="err" v-if="errors.email">{{ errors.email }}</span></div>
          <div class="field"><label for="senha">Senha</label>
            <div class="pass-wrap"><input id="senha" :type="showPass?'text':'password'" v-model="form.senha" placeholder="••••••••" :class="{ error: errors.senha }" @blur="validate('senha')"><button type="button" class="pass-toggle" @click="showPass=!showPass"><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/><circle cx="12" cy="12" r="3"/></svg></button></div>
            <span class="err" v-if="errors.senha">{{ errors.senha }}</span></div>
          <div v-if="loginError" class="alert-error">{{ loginError }}</div>
          <button type="submit" class="btn-primary-full" :disabled="loading"><span v-if="!loading">Entrar →</span><span v-else class="spin"></span></button>
        </form>
        <p class="signup-link">Ainda não tem conta? <a href="#" @click.prevent="$router.push('/cadastro')">Cadastre-se grátis</a></p>
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
const loginError = ref('')
const form = reactive({ email: '', senha: '' })
const errors = reactive({ email: '', senha: '' })

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

function validate(f) { errors[f]=''; if(f==='email'&&!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) errors.email='E-mail inválido.'; if(f==='senha'&&form.senha.length<1) errors.senha='Informe sua senha.' }
function validateAll(){ ['email','senha'].forEach(validate); return Object.values(errors).every(e=>!e) }
const fazerLogin = async () => {
  loginError.value = ''
  if (!validateAll()) return
  loading.value = true
  try {
    await auth.login(form.email, form.senha)
    const redirect = route.query.redirect || (auth.userType === 'Paciente' ? '/receitas' : auth.userType === 'Entregador' ? '/entregas' : '/dashboard')
    router.push(redirect)
  }
  catch (err) { loginError.value = err.message || 'E-mail ou senha incorretos.' }
  finally { loading.value = false }
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
.headline{font-family:Georgia,serif;font-size:clamp(1.8rem,2.4vw,2.6rem);line-height:1.15;color:#fff;font-weight:700;letter-spacing:-0.03em;margin-bottom:16px}
.headline em{font-style:italic;color:#93C5FD}
.tagline{font-size:14px;color:#93B4F5;line-height:1.7;margin-bottom:32px}

/* Sachet Mockup Card */
/* Sachet Mockup Card */
.carousel-container {
  position: relative;
  min-height: 240px;
  width: 100%;
  max-width: 320px;
  margin-top: 10px;
}

.sachet-mockup-card {
  position: relative;
  background: rgba(255, 255, 255, 0.13);
  border: 1.5px solid rgba(255, 255, 255, 0.25);
  border-radius: 20px;
  padding: 24px;
  width: 100%;
  box-shadow: 0 25px 55px rgba(0, 0, 0, 0.35);
  backdrop-filter: blur(16px);
  overflow: hidden;
  animation: floatCard 5s ease-in-out infinite;
}

@keyframes floatCard {
  0%, 100% { transform: translateY(0) rotate(0deg); }
  50% { transform: translateY(-8px) rotate(1deg); }
}

/* Card Transitions */
.card-fade-enter-active, .card-fade-leave-active {
  transition: all 0.5s cubic-bezier(0.4, 0, 0.2, 1);
  position: absolute;
  left: 0;
  right: 0;
}
.card-fade-enter-from {
  opacity: 0;
  transform: translateY(16px) scale(0.96);
}
.card-fade-leave-to {
  opacity: 0;
  transform: translateY(-16px) scale(0.96);
}

.card-glow {
  position: absolute;
  top: -50%;
  left: -50%;
  width: 200%;
  height: 200%;
  background: radial-gradient(circle, rgba(96, 165, 250, 0.25) 0%, transparent 60%);
  pointer-events: none;
}

.sachet-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
  border-bottom: 1px solid rgba(255, 255, 255, 0.15);
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
  background: rgba(255, 255, 255, 0.2);
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
  color: rgba(255, 255, 255, 0.6);
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
  color: #dbeafe;
  line-height: 1.5;
}

.sachet-footer-row {
  display: flex;
  justify-content: space-between;
  font-size: 11px;
  color: rgba(255, 255, 255, 0.7);
  margin-top: 8px;
  font-weight: 500;
}

.sachet-barcode {
  display: flex;
  gap: 3px;
  justify-content: center;
  opacity: 0.45;
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

.panel-right{background:#fff;display:flex;justify-content:center;align-items:center;padding:48px 40px;overflow-y:auto}
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
.pass-wrap{position:relative}
.pass-wrap input{padding-right:42px}
.pass-toggle{position:absolute;right:12px;top:50%;transform:translateY(-50%);background:none;border:none;cursor:pointer;color:#9ca3af;display:flex;padding:0;transition:color .15s}
.pass-toggle:hover{color:#2563EB}
.alert-error{display:flex;align-items:center;gap:8px;background:#fef2f2;border:1px solid #fecaca;border-radius:8px;padding:10px 14px;font-size:13px;color:#dc2626;margin-bottom:16px}
.btn-primary-full{width:100%;background:#2563EB;color:#fff;border:none;padding:14px 24px;border-radius:100px;font-size:14px;font-weight:600;cursor:pointer;display:flex;align-items:center;justify-content:center;gap:8px;transition:background .2s,transform .15s;font-family:inherit;margin-top:4px}
.btn-primary-full:hover:not(:disabled){background:#1D4ED8;transform:translateY(-1px)}
.btn-primary-full:disabled{opacity:.7;cursor:not-allowed}
.spin{width:16px;height:16px;border:2px solid rgba(255,255,255,.3);border-top-color:#fff;border-radius:50%;animation:spin .7s linear infinite}
@keyframes spin{to{transform:rotate(360deg)}}
.signup-link{text-align:center;margin-top:20px;font-size:13px;color:#6b7280}
.signup-link a{color:#2563EB;font-weight:600;text-decoration:none}
.signup-link a:hover{text-decoration:underline}
@media(max-width:768px){.page{grid-template-columns:1fr}.panel-left{display:none}.panel-right{padding:40px 24px;align-items:flex-start;min-height:100vh}}
</style>
