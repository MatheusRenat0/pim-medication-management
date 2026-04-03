<template>
  <div class="auth-layout">
    <div class="auth-container">
      <button class="btn-back" @click="$router.push('/')">← Voltar para a Home</button>
      
      <div class="auth-card">
        <div class="logo-header">
          <div class="logo-mark">M</div>
          <h2>Bem-vindo ao MedFlow</h2>
          <p>Acesse sua conta para gerenciar suas assinaturas.</p>
        </div>

        <form @submit.prevent="fazerLogin" class="auth-form">
          <div class="form-group">
            <label>E-mail</label>
            <input 
              type="email" 
              v-model="email" 
              placeholder="seu@email.com" 
              required
            >
          </div>

          <div class="form-group">
            <label>Senha</label>
            <input 
              type="password" 
              v-model="senha" 
              placeholder="••••••••" 
              required
            >
          </div>

          <button type="submit" class="btn-primary">Entrar</button>
        </form>

        <div class="auth-footer">
          <p>Ainda não tem uma conta? <a href="#" @click.prevent="$router.push('/cadastro')">Cadastre-se agora</a></p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const email = ref('')
const senha = ref('')

const fazerLogin = async () => {
  // 🚀 AQUI É ONDE O C# VAI ENTRAR DEPOIS!
  // Por enquanto, vamos simular que o C# respondeu com sucesso:
  
  if(email.value && senha.value) {
    console.log("Simulando login para:", email.value)
    
    // Guardamos o ID no navegador (localStorage)
    localStorage.setItem('usuarioId', '1')
    
    // Redirecionamos o paciente para montar a box
    router.push('/setup-box')
  } else {
    alert("Preencha e-mail e senha!")
  }
}

const irParaCadastro = () => {
  // router.push('/cadastro') // Em breve!
  alert("Tela de cadastro será construída no próximo passo!")
}
</script>

<style scoped>
.auth-layout {
  min-height: 100vh;
  background-color: #f9fbfd;
  display: flex;
  align-items: center;
  justify-content: center;
  font-family: 'DM Sans', sans-serif;
  padding: 20px;
}

.auth-container {
  width: 100%;
  max-width: 440px;
}

.btn-back {
  background: none;
  border: none;
  color: #166534;
  font-weight: 600;
  font-size: 14px;
  cursor: pointer;
  margin-bottom: 20px;
}

.auth-card {
  background: #ffffff;
  border-radius: 20px;
  padding: 40px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.05);
  border: 1px solid #e5e7eb;
}

.logo-header {
  text-align: center;
  margin-bottom: 32px;
}

.logo-mark {
  width: 48px;
  height: 48px;
  background: #166534;
  color: white;
  border-radius: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 700;
  font-size: 24px;
  margin: 0 auto 16px;
}

.logo-header h2 {
  font-size: 24px;
  color: #0a0a0a;
  margin-bottom: 8px;
}

.logo-header p {
  font-size: 14px;
  color: #6b7280;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-bottom: 20px;
}

label {
  font-size: 14px;
  font-weight: 600;
  color: #374151;
}

input {
  padding: 14px 16px;
  border-radius: 10px;
  border: 1.5px solid #d1d5db;
  font-size: 15px;
  outline: none;
  transition: border-color 0.2s;
}

input:focus {
  border-color: #166534;
}

.btn-primary {
  width: 100%;
  background: #166534;
  color: white;
  border: none;
  padding: 16px;
  border-radius: 10px;
  font-weight: 700;
  font-size: 16px;
  cursor: pointer;
  margin-top: 8px;
  transition: background 0.2s;
}

.btn-primary:hover {
  background: #14532d;
}

.auth-footer {
  text-align: center;
  margin-top: 24px;
  font-size: 14px;
  color: #6b7280;
}

.auth-footer a {
  color: #166534;
  font-weight: 600;
  text-decoration: none;
}

.auth-footer a:hover {
  text-decoration: underline;
}
</style>