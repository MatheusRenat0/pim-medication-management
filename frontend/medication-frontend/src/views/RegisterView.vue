<template>
  <div class="page">

    <!-- Painel esquerdo -->
    <aside class="panel-left">
      <div class="logo">
        <span class="logo-mark">M</span>
        <span class="logo-name">MedFlow</span>
      </div>

      <div class="panel-body">
        <h1 class="headline">
          Comece a <em>cuidar</em><br>da sua saúde hoje.
        </h1>
        <p class="tagline">
          Cadastro em menos de 2 minutos.<br>Sem burocracia, sem contrato.
        </p>

        <ul class="steps">
          <li v-for="(step, i) in steps" :key="i" class="step">
            <span class="step-num">0{{ i + 1 }}</span>
            <div class="step-text">
              <strong>{{ step.title }}</strong>
              <span>{{ step.desc }}</span>
            </div>
          </li>
        </ul>
      </div>

      <p class="panel-note">Sem contrato · Cancele quando quiser</p>
    </aside>

    <!-- Painel direito -->
    <main class="panel-right">
      <div class="form-container">

        <button class="btn-back" @click="$router.push('/login')">
          <svg width="14" height="14" viewBox="0 0 16 16" fill="none">
            <path d="M10 3L5 8l5 5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
              stroke-linejoin="round" />
          </svg>
          Voltar para login
        </button>

        <!-- Sucesso -->
        <Transition name="fade">
          <div v-if="success" class="success">
            <div class="success-icon">
              <svg width="26" height="26" viewBox="0 0 24 24" fill="none">
                <path d="M5 13l4 4L19 7" stroke="#166534" stroke-width="2.5" stroke-linecap="round"
                  stroke-linejoin="round" />
              </svg>
            </div>
            <h2>Conta criada!</h2>
            <p>Bem-vindo ao MedFlow. Agora vamos montar a sua box.</p>
            <button class="btn-primary" @click="$router.push('/setup-box')">
              Montar minha box
              <svg width="15" height="15" viewBox="0 0 16 16" fill="none">
                <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                  stroke-linejoin="round" />
              </svg>
            </button>
          </div>
        </Transition>

        <!-- Formulário -->
        <Transition name="fade">
          <div v-if="!success">
            <div class="form-header">
              <h2>Crie sua conta</h2>
              <p>Junte-se a +2.400 pessoas que simplificaram sua rotina.</p>
            </div>

            <form @submit.prevent="fazerCadastro" novalidate>

              <div class="field">
                <label for="nome">Nome completo</label>
                <input id="nome" type="text" v-model="form.nome" placeholder="Ex: João da Silva"
                  :class="{ error: errors.nome }" @blur="validate('nome')">
                <span class="err" v-if="errors.nome">{{ errors.nome }}</span>
              </div>

              <div class="row-2">
                <div class="field">
                  <label for="cpf">CPF</label>
                  <input id="cpf" type="text" v-model="form.cpf" placeholder="000.000.000-00" maxlength="14"
                    :class="{ error: errors.cpf }" @input="maskCpf" @blur="validate('cpf')">
                  <span class="err" v-if="errors.cpf">{{ errors.cpf }}</span>
                </div>
                <div class="field">
                  <label for="nasc">Data de nascimento</label>
                  <input id="nasc" type="date" v-model="form.nascimento" :class="{ error: errors.nascimento }"
                    @blur="validate('nascimento')">
                  <span class="err" v-if="errors.nascimento">{{ errors.nascimento }}</span>
                </div>
              </div>

              <div class="field">
                <label for="email">E-mail</label>
                <input id="email" type="email" v-model="form.email" placeholder="seu@email.com"
                  :class="{ error: errors.email }" @blur="validate('email')">
                <span class="err" v-if="errors.email">{{ errors.email }}</span>
              </div>

              <div class="field">
                <label for="senha">Senha</label>
                <div class="pass-wrap">
                  <input id="senha" :type="showPass ? 'text' : 'password'" v-model="form.senha"
                    placeholder="Crie uma senha forte" :class="{ error: errors.senha }" @input="calcStrength"
                    @blur="validate('senha')">
                  <button type="button" class="pass-toggle" @click="showPass = !showPass">
                    <svg v-if="!showPass" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor"
                      stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                      <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z" />
                      <circle cx="12" cy="12" r="3" />
                    </svg>
                    <svg v-else width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor"
                      stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                      <path d="M17.94 17.94A10.07 10.07 0 0112 20c-7 0-11-8-11-8a18.45 18.45 0 015.06-5.94" />
                      <path d="M9.9 4.24A9.12 9.12 0 0112 4c7 0 11 8 11 8a18.5 18.5 0 01-2.16 3.19" />
                      <line x1="1" y1="1" x2="23" y2="23" />
                    </svg>
                  </button>
                </div>
                <div v-if="form.senha" class="strength-bar">
                  <div class="strength-fill" :style="{ width: strengthWidth, background: strengthColor }"></div>
                </div>
                <span v-if="form.senha" class="strength-text" :style="{ color: strengthColor }">{{ strengthText
                  }}</span>
                <span class="err" v-if="errors.senha">{{ errors.senha }}</span>
              </div>

              <label class="check-label">
                <input type="checkbox" v-model="form.termos">
                <span>Li e aceito os <a href="#" @click.prevent>Termos de uso</a> e a <a href="#"
                    @click.prevent>Política de privacidade</a></span>
              </label>
              <span class="err" v-if="errors.termos" style="display:block; margin: -10px 0 14px;">{{ errors.termos
                }}</span>

              <button type="submit" class="btn-primary" :disabled="loading">
                <span v-if="!loading">Criar conta</span>
                <span v-else class="spin"></span>
                <svg v-if="!loading" width="15" height="15" viewBox="0 0 16 16" fill="none">
                  <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                    stroke-linejoin="round" />
                </svg>
              </button>

              <div class="divider"><span>ou continue com</span></div>

              <button type="button" class="btn-google">
                <svg width="16" height="16" viewBox="0 0 24 24">
                  <path
                    d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"
                    fill="#4285F4" />
                  <path
                    d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"
                    fill="#34A853" />
                  <path
                    d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z"
                    fill="#FBBC05" />
                  <path
                    d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z"
                    fill="#EA4335" />
                </svg>
                Continuar com Google
              </button>
            </form>

            <p class="login-link">
              Já tem conta? <a href="#" @click.prevent="$router.push('/login')">Faça login</a>
            </p>
          </div>
        </Transition>

      </div>
    </main>

  </div>
</template>

<script setup>
import { ref, reactive, computed } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const showPass = ref(false)
const loading = ref(false)
const success = ref(false)
const strength = ref(0)

const form = reactive({
  nome: '', cpf: '', nascimento: '', email: '', senha: '', termos: false,
})

const errors = reactive({
  nome: '', cpf: '', nascimento: '', email: '', senha: '', termos: '',
})

const steps = [
  { title: 'Crie sua conta', desc: 'Preencha seus dados básicos.' },
  { title: 'Monte sua box', desc: 'Informe seus medicamentos e horários.' },
  { title: 'Receba em casa', desc: 'Sachês organizados por dia e horário.' },
]

const strengthWidth = computed(() => ['0%', '25%', '50%', '75%', '100%'][strength.value])
const strengthColor = computed(() => ['', '#ef4444', '#f59e0b', '#3b82f6', '#166534'][strength.value])
const strengthText = computed(() => ['', 'Fraca', 'Razoável', 'Boa', 'Forte'][strength.value])

function calcStrength() {
  const v = form.senha
  let s = 0
  if (v.length >= 8) s++
  if (/[A-Z]/.test(v)) s++
  if (/[0-9]/.test(v)) s++
  if (/[^A-Za-z0-9]/.test(v)) s++
  strength.value = s
}

function maskCpf() {
  let v = form.cpf.replace(/\D/g, '').slice(0, 11)
  v = v.replace(/(\d{3})(\d)/, '$1.$2')
  v = v.replace(/(\d{3})(\d)/, '$1.$2')
  v = v.replace(/(\d{3})(\d{1,2})$/, '$1-$2')
  form.cpf = v
}

function validate(field) {
  errors[field] = ''
  if (field === 'nome' && !form.nome.trim()) errors.nome = 'Informe seu nome completo.'
  if (field === 'cpf' && form.cpf.replace(/\D/g, '').length < 11) errors.cpf = 'CPF inválido.'
  if (field === 'nascimento' && !form.nascimento) errors.nascimento = 'Informe a data.'
  if (field === 'email' && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) errors.email = 'E-mail inválido.'
  if (field === 'senha' && form.senha.length < 6) errors.senha = 'Mínimo 6 caracteres.'
  if (field === 'termos' && !form.termos) errors.termos = 'Você precisa aceitar os termos.'
}

function validateAll() {
  ;['nome', 'cpf', 'nascimento', 'email', 'senha', 'termos'].forEach(validate)
  return Object.values(errors).every(e => !e)
}

const fazerCadastro = async () => {
  if (!validateAll()) return
  loading.value = true

  // 🚀 AQUI ENTRA O C# — POST para /api/Usuario/Cadastrar
  // const res = await fetch('/api/Usuario/Cadastrar', {
  //   method: 'POST',
  //   headers: { 'Content-Type': 'application/json' },
  //   body: JSON.stringify({
  //     nome: form.nome, cpf: form.cpf, nascimento: form.nascimento,
  //     email: form.email, senha: form.senha,
  //   })
  // })
  // const data = await res.json()
  // localStorage.setItem('usuarioId', data.id)

  await new Promise(r => setTimeout(r, 1200))
  localStorage.setItem('usuarioId', '2')
  loading.value = false
  success.value = true
}
</script>

<style scoped>
*,
*::before,
*::after {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

.page {
  display: grid;
  grid-template-columns: 1fr 1fr;
  min-height: 100vh;
  font-family: 'DM Sans', 'Helvetica Neue', sans-serif;
  color: #0a0a0a;
  background: #fff;
  /* evita fundo externo vazar */
}

/* ── Painel esquerdo ──────────────────────── */
.panel-left {
  background: #0f1a14;
  display: flex;
  flex-direction: column;
  padding: 44px 48px;
  min-height: 100vh;
}

.logo {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 52px;
}

.logo-mark {
  width: 34px;
  height: 34px;
  background: #22c55e;
  color: #0f1a14;
  border-radius: 9px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 700;
  font-size: 17px;
}

.logo-name {
  font-size: 18px;
  font-weight: 600;
  color: #fff;
  letter-spacing: -0.3px;
}

.panel-body {
  flex: 1;
}

.headline {
  font-family: Georgia, serif;
  font-size: clamp(1.6rem, 2.2vw, 2.2rem);
  line-height: 1.2;
  color: #fff;
  font-weight: 700;
  letter-spacing: -0.03em;
  margin-bottom: 14px;
}

.headline em {
  font-style: italic;
  color: #4ade80;
}

.tagline {
  font-size: 14px;
  color: #86efac;
  line-height: 1.7;
  margin-bottom: 44px;
}

.steps {
  list-style: none;
  display: flex;
  flex-direction: column;
  gap: 24px;
}

.step {
  display: flex;
  align-items: flex-start;
  gap: 14px;
}

.step-num {
  width: 28px;
  height: 28px;
  border-radius: 50%;
  border: 1.5px solid #4ade80;
  color: #4ade80;
  font-size: 10px;
  font-weight: 700;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  margin-top: 1px;
}

.step-text strong {
  display: block;
  font-size: 13px;
  font-weight: 600;
  color: #f0fdf4;
  margin-bottom: 2px;
}

.step-text span {
  font-size: 12px;
  color: #86efac;
  line-height: 1.5;
}

.panel-note {
  font-size: 12px;
  color: #4ade80;
  font-weight: 500;
}

/* ── Painel direito ───────────────────────── */
.panel-right {
  background: #fff;
  display: flex;
  justify-content: center;
  padding: 48px 40px;
  overflow-y: auto;
}

.form-container {
  width: 100%;
  max-width: 400px;
}

.btn-back {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  background: none;
  border: none;
  color: #6b7280;
  font-size: 13px;
  font-weight: 500;
  cursor: pointer;
  padding: 0;
  margin-bottom: 36px;
  font-family: inherit;
  transition: color 0.15s;
}

.btn-back:hover {
  color: #166534;
}

.form-header {
  margin-bottom: 28px;
}

.form-header h2 {
  font-family: Georgia, serif;
  font-size: 1.5rem;
  font-weight: 700;
  color: #0a0a0a;
  letter-spacing: -0.02em;
  margin-bottom: 5px;
}

.form-header p {
  font-size: 13px;
  color: #6b7280;
}

.field {
  display: flex;
  flex-direction: column;
  gap: 6px;
  margin-bottom: 16px;
}

.field label {
  font-size: 13px;
  font-weight: 600;
  color: #374151;
}

.field input {
  padding: 12px 14px;
  border-radius: 10px;
  border: 1.5px solid #d1d5db;
  font-size: 14px;
  color: #0a0a0a;
  background: #fff;
  outline: none;
  transition: border-color 0.15s;
  font-family: inherit;
  width: 100%;
}

.field input:focus {
  border-color: #166534;
}

.field input.error {
  border-color: #ef4444;
}

.field input::placeholder {
  color: #9ca3af;
}

.err {
  font-size: 11px;
  color: #ef4444;
}

.row-2 {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
}

.pass-wrap {
  position: relative;
}

.pass-wrap input {
  padding-right: 42px;
}

.pass-toggle {
  position: absolute;
  right: 12px;
  top: 50%;
  transform: translateY(-50%);
  background: none;
  border: none;
  cursor: pointer;
  color: #9ca3af;
  display: flex;
  padding: 0;
  transition: color 0.15s;
}

.pass-toggle:hover {
  color: #166534;
}

.strength-bar {
  height: 3px;
  border-radius: 2px;
  background: #f3f4f6;
  margin-top: 6px;
  overflow: hidden;
}

.strength-fill {
  height: 100%;
  border-radius: 2px;
  transition: width 0.3s, background 0.3s;
}

.strength-text {
  font-size: 11px;
  font-weight: 500;
  margin-top: 3px;
  display: block;
}

.check-label {
  display: flex;
  align-items: flex-start;
  gap: 10px;
  margin-bottom: 18px;
  cursor: pointer;
}

.check-label input[type="checkbox"] {
  width: 15px;
  height: 15px;
  accent-color: #166534;
  flex-shrink: 0;
  margin-top: 2px;
  cursor: pointer;
}

.check-label span {
  font-size: 12px;
  color: #6b7280;
  line-height: 1.5;
}

.check-label a {
  color: #166534;
  font-weight: 600;
  text-decoration: none;
}

.btn-primary {
  width: 100%;
  background: #166534;
  color: #fff;
  border: none;
  padding: 14px 24px;
  border-radius: 100px;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  transition: background 0.2s, transform 0.15s;
  font-family: inherit;
}

.btn-primary:hover:not(:disabled) {
  background: #14532d;
  transform: translateY(-1px);
}

.btn-primary:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

.spin {
  width: 16px;
  height: 16px;
  border: 2px solid rgba(255, 255, 255, 0.3);
  border-top-color: #fff;
  border-radius: 50%;
  animation: spin 0.7s linear infinite;
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}

.divider {
  display: flex;
  align-items: center;
  gap: 10px;
  margin: 14px 0;
}

.divider::before,
.divider::after {
  content: '';
  flex: 1;
  height: 1px;
  background: #e5e7eb;
}

.divider span {
  font-size: 12px;
  color: #9ca3af;
  white-space: nowrap;
}

.btn-google {
  width: 100%;
  background: #fff;
  color: #374151;
  border: 1.5px solid #d1d5db;
  padding: 12px 24px;
  border-radius: 100px;
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 10px;
  transition: border-color 0.15s, background 0.15s;
  font-family: inherit;
}

.btn-google:hover {
  border-color: #166534;
  background: #f0fdf4;
}

.login-link {
  text-align: center;
  margin-top: 20px;
  font-size: 13px;
  color: #6b7280;
}

.login-link a {
  color: #166534;
  font-weight: 600;
  text-decoration: none;
}

.login-link a:hover {
  text-decoration: underline;
}

.success {
  text-align: center;
  padding: 20px 0;
}

.success-icon {
  width: 58px;
  height: 58px;
  background: #f0fdf4;
  border: 2px solid #bbf7d0;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 auto 20px;
}

.success h2 {
  font-family: Georgia, serif;
  font-size: 1.4rem;
  color: #0a0a0a;
  margin-bottom: 8px;
  letter-spacing: -0.02em;
}

.success p {
  font-size: 14px;
  color: #6b7280;
  margin-bottom: 28px;
  line-height: 1.6;
}

.success .btn-primary {
  max-width: 240px;
  margin: 0 auto;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

@media (max-width: 768px) {
  .page {
    grid-template-columns: 1fr;
  }

  .panel-left {
    display: none;
  }

  .panel-right {
    padding: 40px 24px;
    min-height: 100vh;
  }
}
</style>