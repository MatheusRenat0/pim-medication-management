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
          Sua saúde,<br><em>organizada</em><br>pra você.
        </h1>
        <p class="tagline">
          Medicamentos separados por dia e horário,<br>
          entregues direto na sua porta todo mês.
        </p>

        <div class="testimonial">
          <p>"Desde que comecei com o MedFlow, nunca mais esqueci um remédio."</p>
          <div class="testimonial-author">
            <span class="avatar">R</span>
            <div>
              <strong>Renata M.</strong>
              <span>São Paulo, SP</span>
            </div>
          </div>
        </div>
      </div>

      <p class="panel-note">Sem contrato · Cancele quando quiser</p>
    </aside>

    <!-- Painel direito -->
    <main class="panel-right">
      <div class="form-container">

        <button class="btn-back" @click="$router.push('/')">
          <svg width="14" height="14" viewBox="0 0 16 16" fill="none">
            <path d="M10 3L5 8l5 5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
              stroke-linejoin="round" />
          </svg>
          Voltar para a home
        </button>

        <div class="form-header">
          <h2>Bem-vindo de volta</h2>
          <p>Entre na sua conta para acessar sua box.</p>
        </div>

        <form @submit.prevent="fazerLogin" novalidate>

          <div class="field">
            <label for="email">E-mail</label>
            <input id="email" type="email" v-model="form.email" placeholder="seu@email.com"
              :class="{ error: errors.email }" @blur="validate('email')">
            <span class="err" v-if="errors.email">{{ errors.email }}</span>
          </div>

          <div class="field">
            <div class="label-row">
              <label for="senha">Senha</label>
              <a href="#" class="forgot" @click.prevent>Esqueceu a senha?</a>
            </div>
            <div class="pass-wrap">
              <input id="senha" :type="showPass ? 'text' : 'password'" v-model="form.senha" placeholder="••••••••"
                :class="{ error: errors.senha }" @blur="validate('senha')">
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
            <span class="err" v-if="errors.senha">{{ errors.senha }}</span>
          </div>

          <div v-if="loginError" class="alert-error">
            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"
              stroke-linecap="round" stroke-linejoin="round">
              <circle cx="12" cy="12" r="10" />
              <line x1="12" y1="8" x2="12" y2="12" />
              <line x1="12" y1="16" x2="12.01" y2="16" />
            </svg>
            {{ loginError }}
          </div>

          <button type="submit" class="btn-primary" :disabled="loading">
            <span v-if="!loading">Entrar</span>
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

        <p class="signup-link">
          Ainda não tem conta?
          <a href="#" @click.prevent="$router.push('/cadastro')">Cadastre-se grátis</a>
        </p>

      </div>
    </main>

  </div>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const showPass = ref(false)
const loading = ref(false)
const loginError = ref('')

const form = reactive({ email: '', senha: '' })
const errors = reactive({ email: '', senha: '' })

function validate(field) {
  errors[field] = ''
  if (field === 'email' && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email))
    errors.email = 'E-mail inválido.'
  if (field === 'senha' && form.senha.length < 1)
    errors.senha = 'Informe sua senha.'
}

function validateAll() {
  ;['email', 'senha'].forEach(validate)
  return Object.values(errors).every(e => !e)
}

const fazerLogin = async () => {
  loginError.value = ''
  if (!validateAll()) return

  loading.value = true

  // 🚀 AQUI ENTRA O C# — POST para /api/Usuario/Login
  // const res = await fetch('/api/Usuario/Login', {
  //   method: 'POST',
  //   headers: { 'Content-Type': 'application/json' },
  //   body: JSON.stringify({ email: form.email, senha: form.senha })
  // })
  // if (!res.ok) {
  //   loginError.value = 'E-mail ou senha incorretos.'
  //   loading.value = false
  //   return
  // }
  // const data = await res.json()
  // localStorage.setItem('usuarioId', data.id)
  // router.push('/setup-box')

  // Simulação:
  await new Promise(r => setTimeout(r, 1200))
  localStorage.setItem('usuarioId', '1')
  loading.value = false
  router.push('/setup-box')
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
  font-size: clamp(1.8rem, 2.4vw, 2.6rem);
  line-height: 1.15;
  color: #fff;
  font-weight: 700;
  letter-spacing: -0.03em;
  margin-bottom: 16px;
}

.headline em {
  font-style: italic;
  color: #4ade80;
}

.tagline {
  font-size: 14px;
  color: #86efac;
  line-height: 1.7;
  margin-bottom: 52px;
}

/* Depoimento */
.testimonial {
  border-left: 2px solid #4ade80;
  padding-left: 20px;
}

.testimonial p {
  font-family: Georgia, serif;
  font-style: italic;
  font-size: 15px;
  color: #f0fdf4;
  line-height: 1.6;
  margin-bottom: 16px;
}

.testimonial-author {
  display: flex;
  align-items: center;
  gap: 12px;
}

.avatar {
  width: 34px;
  height: 34px;
  border-radius: 50%;
  background: #166534;
  border: 1.5px solid #4ade80;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 13px;
  font-weight: 700;
  color: #bbf7d0;
  flex-shrink: 0;
}

.testimonial-author strong {
  display: block;
  font-size: 13px;
  font-weight: 600;
  color: #f0fdf4;
}

.testimonial-author span {
  font-size: 12px;
  color: #86efac;
}

.panel-note {
  font-size: 12px;
  color: #4ade80;
  font-weight: 500;
  margin-top: 40px;
}

/* ── Painel direito ───────────────────────── */
.panel-right {
  background: #fff;
  display: flex;
  justify-content: center;
  align-items: center;
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

/* ── Campos ───────────────────────────────── */
.field {
  display: flex;
  flex-direction: column;
  gap: 6px;
  margin-bottom: 16px;
}

.label-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.field label {
  font-size: 13px;
  font-weight: 600;
  color: #374151;
}

.forgot {
  font-size: 12px;
  color: #166534;
  font-weight: 500;
  text-decoration: none;
  transition: opacity 0.15s;
}

.forgot:hover {
  opacity: 0.7;
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

/* ── Erro de login ────────────────────────── */
.alert-error {
  display: flex;
  align-items: center;
  gap: 8px;
  background: #fef2f2;
  border: 1px solid #fecaca;
  border-radius: 8px;
  padding: 10px 14px;
  font-size: 13px;
  color: #dc2626;
  margin-bottom: 16px;
}

/* ── Botões ───────────────────────────────── */
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
  margin-top: 4px;
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

/* ── Signup link ──────────────────────────── */
.signup-link {
  text-align: center;
  margin-top: 20px;
  font-size: 13px;
  color: #6b7280;
}

.signup-link a {
  color: #166534;
  font-weight: 600;
  text-decoration: none;
}

.signup-link a:hover {
  text-decoration: underline;
}

/* ── Responsivo ───────────────────────────── */
@media (max-width: 768px) {
  .page {
    grid-template-columns: 1fr;
  }

  .panel-left {
    display: none;
  }

  .panel-right {
    padding: 40px 24px;
    align-items: flex-start;
    min-height: 100vh;
  }
}
</style>