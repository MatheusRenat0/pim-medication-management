<template>
  <div class="upload-container">
    <h2>Validação Médica 🩺</h2>
    <p>Para montarmos a sua Box de Medicamentos, precisamos da foto ou PDF da sua receita.</p>

    <div class="box-upload">
      <input 
        type="file" 
        @change="capturarArquivo" 
        accept="image/png, image/jpeg, application/pdf"
      />
    </div>

    <button 
      class="btn-continuar" 
      @click="enviarReceita" 
      :disabled="!arquivoSelecionado"
    >
      Enviar e Montar Minha Box
    </button>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const arquivoSelecionado = ref(null)

// Função que roda assim que o usuário escolhe a foto no celular/PC
const capturarArquivo = (evento) => {
  arquivoSelecionado.value = evento.target.files[0]
}

// Função que envia para o C#
const enviarReceita = async () => {
  // Pega o ID do usuário que guardamos no Login
  const usuarioId = localStorage.getItem('usuarioId')

  if (!usuarioId) {
    alert("Sessão expirada. Faça login novamente.")
    router.push('/login')
    return
  }

  // FormData é o "caminhão de entrega" para arquivos físicos no JavaScript
  const formData = new FormData()
  formData.append('usuarioId', usuarioId)
  formData.append('arquivo', arquivoSelecionado.value)

  try {
    // 🔥 Bate na rota de Upload do C#
    const resposta = await fetch('http://localhost:5205/api/Receita/upload', {
      method: 'POST',
      // IMPORTANTE: Ao usar FormData, não colocamos 'Content-Type' no header. 
      // O próprio navegador cuida de avisar o C# que é um arquivo multipart!
      body: formData 
    })

    const dados = await resposta.json()

    if (resposta.ok) {
      alert('Receita enviada com sucesso!')
      
      // Guardamos o ID da Receita gerada, pois vamos precisar dela na próxima tela!
      localStorage.setItem('receitaId', dados.receitaId)
      
      // Manda o paciente para a tela de escolher os remédios
      router.push('/setup-box')
    } else {
      alert("Erro do servidor: " + dados.message)
    }

  } catch (erro) {
    console.error("Erro no upload:", erro)
    alert("Falha na comunicação. O backend está rodando?")
  }
}
</script>

<style scoped>
.upload-container {
  max-width: 500px;
  margin: 50px auto;
  text-align: center;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 12px;
  box-shadow: 0 4px 6px rgba(0,0,0,0.1);
}

.box-upload {
  margin: 30px 0;
  padding: 40px;
  border: 2px dashed #007bff;
  border-radius: 8px;
  background-color: white;
}

.btn-continuar {
  width: 100%;
  padding: 12px;
  background-color: #28a745;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 16px;
  cursor: pointer;
  font-weight: bold;
}

.btn-continuar:disabled {
  background-color: #cccccc;
  cursor: not-allowed;
}
</style>