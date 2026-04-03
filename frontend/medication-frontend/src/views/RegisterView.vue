<template>
  <div class="auth-layout">
    <div class="auth-container">
      <button class="btn-back" @click="$router.push('/login')">← Voltar para Login</button>
      
      <div class="auth-card">
        <div class="logo-header">
          <div class="logo-mark">M</div>
          <h2>Crie sua conta</h2>
          <p>Junte-se ao MedFlow e simplifique sua saúde.</p>
        </div>

        <form @submit.prevent="fazerCadastro" class="auth-form">
          <div class="form-group">
            <label>Nome Completo</label>
            <input 
              type="text" 
              v-model="nome" 
              placeholder="Ex: João da Silva" 
              required
            >
          </div>

          <div class="form-group">
            <label>CPF</label>
            <input 
              type="text" 
              v-model="cpf" 
              placeholder="000.000.000-00" 
              required
            >
          </div>

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
              placeholder="Crie uma senha forte" 
              required
            >
          </div>

          <button type="submit" class="btn-primary">Finalizar Cadastro</button>
        </form>

        <div class="auth-footer">
          <p>Já tem uma conta? <a href="#" @click.prevent="$router.push('/login')">Faça login</a></p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const nome = ref('')
const cpf = ref('')
const email = ref('')
const senha = ref('')

const fazerCadastro = async () => {
  // 🚀 AQUI É ONDE O C# VAI ENTRAR DEPOIS (POST para /api/Usuario/Cadastrar)
  // Simulando o sucesso do cadastro:
  
  if(nome.value && cpf.value && email.value && senha.value) {
    console.log("Simulando cadastro para:", nome.value)
    
    // Guardamos o ID no navegador (localStorage) como se o backend tivesse retornado o ID 2
    localStorage.setItem('usuarioId', '2')
    
    alert('Cadastro realizado com sucesso! Bem-vindo ao MedFlow.')
    
    // Redirecionamos o paciente direto para montar a box dele
    router.push('/setup-box')
  } else {
    alert("Por favor, preencha todos os campos!")
  }
}
</script>

<style scoped>
/* O CSS é praticamente idêntico ao do Login para manter a consistência */
.auth-layout {
  min-height: 100vh;
  background-color: #f9fbfd;
  display: flex;
  align-items: center;
  justify-content: center;
  font-family: 'DM Sans', sans-serif;
  padding: 40px 20px;
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