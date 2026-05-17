<template>
  <div class="page">
    <aside class="panel-left">
      <div class="logo"><span class="logo-mark">M</span><span class="logo-name">MedFlow</span></div>
      <div class="panel-body">
        <h1 class="headline">Sua saúde,<br><em>organizada</em><br>pra você.</h1>
        <p class="tagline">Medicamentos separados por dia e horário,<br>entregues direto na sua porta todo mês.</p>
      </div>
      <p class="panel-note">Sem contrato · Cancele quando quiser</p>
    </aside>
    <main class="panel-right">
      <div class="form-container">
        <button class="btn-back" @click="$router.push('/')">← Voltar para a home</button>
        <div class="form-header"><h2>Bem-vindo de volta</h2><p>Entre na sua conta para acessar o sistema.</p></div>
        <form @submit.prevent="fazerLogin" novalidate>
          <div class="field"><label for="email">E-mail</label><input id="email" type="email" v-model="form.email" placeholder="seu@email.com" :class="{ error: errors.email }" @blur="validate('email')"><span class="err" v-if="errors.email">{{ errors.email }}</span></div>
          <div class="field"><label for="senha">Senha</label>
            <div class="pass-wrap"><input id="senha" :type="showPass?'text':'password'" v-model="form.senha" placeholder="••••••••" :class="{ error: errors.senha }" @blur="validate('senha')"><button type="button" class="pass-toggle" @click="showPass=!showPass">👁</button></div>
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
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../../stores/auth'
const router = useRouter()
const auth = useAuthStore()
const showPass = ref(false)
const loading = ref(false)
const loginError = ref('')
const form = reactive({ email: '', senha: '' })
const errors = reactive({ email: '', senha: '' })
function validate(f) { errors[f]=''; if(f==='email'&&!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) errors.email='E-mail inválido.'; if(f==='senha'&&form.senha.length<1) errors.senha='Informe sua senha.' }
function validateAll(){ ['email','senha'].forEach(validate); return Object.values(errors).every(e=>!e) }
const fazerLogin = async () => {
  loginError.value = ''
  if (!validateAll()) return
  loading.value = true
  try { await auth.login(form.email, form.senha); router.push('/dashboard') }
  catch (err) { loginError.value = err.message || 'E-mail ou senha incorretos.' }
  finally { loading.value = false }
}
</script>

<style scoped>
*,*::before,*::after{box-sizing:border-box;margin:0;padding:0}
.page{display:grid;grid-template-columns:1fr 1fr;min-height:100vh;font-family:'Inter','DM Sans',sans-serif;color:#0a0a0a;background:#fff}
.panel-left{background:#1E3A8A;display:flex;flex-direction:column;padding:44px 48px;min-height:100vh}
.logo{display:flex;align-items:center;gap:10px;margin-bottom:52px}
.logo-mark{width:34px;height:34px;background:#2563EB;color:#fff;border-radius:9px;display:flex;align-items:center;justify-content:center;font-weight:700;font-size:17px}
.logo-name{font-size:18px;font-weight:600;color:#fff;letter-spacing:-0.3px}
.panel-body{flex:1}
.headline{font-family:Georgia,serif;font-size:clamp(1.8rem,2.4vw,2.6rem);line-height:1.15;color:#fff;font-weight:700;letter-spacing:-0.03em;margin-bottom:16px}
.headline em{font-style:italic;color:#93C5FD}
.tagline{font-size:14px;color:#93B4F5;line-height:1.7;margin-bottom:52px}
.panel-note{font-size:12px;color:#60A5FA;font-weight:500;margin-top:40px}
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
.pass-toggle{position:absolute;right:12px;top:50%;transform:translateY(-50%);background:none;border:none;cursor:pointer;font-size:14px}
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
