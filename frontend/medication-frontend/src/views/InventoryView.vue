<script setup>
import { ref, onMounted } from 'vue'

const medicamentos = ref([])

async function carregarMedicamentos() {
  try {
    const resposta = await fetch('http://localhost:5205/api/Medicamento')
    if (resposta.ok) {
      medicamentos.value = await resposta.json()
    }
  } catch (erro) {
    console.error('Erro ao conectar:', erro)
  }
}

onMounted(() => carregarMedicamentos())
</script>

<template>
  <div class="container">
    <h1>📦 Gestão de Estoque</h1>
    <table>
       </table>
    <button @click="$router.push('/')">Voltar para Home</button>
  </div>
</template>

<style scoped>

</style>