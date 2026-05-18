<template>
  <div class="legal-page">
    <nav class="legal-nav"><div class="legal-nav-inner">
      <a class="logo" @click="$router.push('/')"><img src="/logo.png" alt="MedFlow" style="width:28px;height:28px;object-fit:contain;" /><span class="logo-text">MedFlow</span></a>
      <button class="btn-back" @click="$router.push('/')">← Voltar</button>
    </div></nav>
    <main class="contact-content">
      <div class="contact-grid">
        <div class="contact-info">
          <div class="section-label">Contato</div>
          <h1>Fale com<br><em>a gente.</em></h1>
          <p class="contact-desc">Estamos prontos para ajudar com dúvidas sobre seu tratamento, entregas ou qualquer funcionalidade do sistema.</p>
          <div class="info-cards">
            <div class="info-card"><div class="info-icon">📍</div><div><strong>Endereço</strong><p>Av. Paulista, 1.000 — 12º andar<br>Bela Vista — São Paulo/SP<br>CEP 01310-100</p></div></div>
            <div class="info-card"><div class="info-icon">📞</div><div><strong>Telefone</strong><p>(11) 3456-7890<br>Seg. a Sex. — 08h às 18h</p></div></div>
            <div class="info-card"><div class="info-icon">✉️</div><div><strong>E-mail</strong><p>contato@medflow.com.br<br>suporte@medflow.com.br</p></div></div>
            <div class="info-card"><div class="info-icon">💬</div><div><strong>WhatsApp</strong><p>(11) 91234-5678<br>Atendimento rápido</p></div></div>
          </div>
        </div>
        <div class="contact-form-wrap">
          <div class="form-card">
            <h2>Envie uma mensagem</h2>
            <p>Responderemos em até 24 horas úteis.</p>
            <form @submit.prevent="enviar">
              <div class="field"><label>Nome completo</label><input v-model="form.nome" placeholder="Seu nome" required></div>
              <div class="field"><label>E-mail</label><input v-model="form.email" type="email" placeholder="seu@email.com" required></div>
              <div class="field"><label>Assunto</label>
                <select v-model="form.assunto">
                  <option value="">Selecione um assunto</option>
                  <option>Dúvida sobre medicamentos</option>
                  <option>Problema com entrega</option>
                  <option>Suporte técnico</option>
                  <option>Cancelamento</option>
                  <option>Outros</option>
                </select>
              </div>
              <div class="field"><label>Mensagem</label><textarea v-model="form.mensagem" rows="4" placeholder="Descreva como podemos ajudar..." required></textarea></div>
              <button type="submit" class="btn-primary" :disabled="enviado">{{ enviado ? '✓ Mensagem enviada!' : 'Enviar mensagem' }}</button>
            </form>
          </div>
        </div>
      </div>
    </main>
    <footer class="lf"><p>© 2026 MedFlow. Todos os direitos reservados.</p></footer>
  </div>
</template>
<script setup>
import { ref, reactive } from 'vue'
const enviado = ref(false)
const form = reactive({ nome:'', email:'', assunto:'', mensagem:'' })
const enviar = () => { enviado.value = true; setTimeout(() => { enviado.value = false; form.nome=''; form.email=''; form.assunto=''; form.mensagem='' }, 3000) }
</script>
<style scoped>
.legal-page{font-family:'Inter','DM Sans',sans-serif;color:#0a0a0a;background:#fff;min-height:100vh;display:flex;flex-direction:column}
.legal-nav{border-bottom:1px solid #e2e8f0;padding:14px 0}.legal-nav-inner{max-width:1200px;margin:0 auto;padding:0 32px;display:flex;justify-content:space-between;align-items:center}
.logo{display:flex;align-items:center;gap:10px;cursor:pointer;text-decoration:none}.logo-icon{width:34px;height:34px;background:#1d4ed8;border-radius:10px;display:flex;align-items:center;justify-content:center}.logo-text{font-size:17px;font-weight:700;color:#0a0a0a}
.btn-back{background:none;border:none;color:#6b7280;font-size:13px;font-weight:500;cursor:pointer;font-family:inherit}.btn-back:hover{color:#1d4ed8}
.contact-content{max-width:1200px;margin:0 auto;padding:64px 32px 80px;flex:1}
.contact-grid{display:grid;grid-template-columns:1fr 1fr;gap:64px;align-items:start}
.section-label{font-size:12px;font-weight:700;letter-spacing:0.08em;text-transform:uppercase;color:#1d4ed8;margin-bottom:14px}
h1{font-family:Georgia,serif;font-size:clamp(1.8rem,3vw,2.6rem);line-height:1.15;letter-spacing:-0.02em;font-weight:700;margin-bottom:20px}
h1 em{font-style:italic;color:#1d4ed8}
.contact-desc{font-size:15px;color:#374151;line-height:1.7;margin-bottom:36px}
.info-cards{display:flex;flex-direction:column;gap:20px}
.info-card{display:flex;gap:14px;align-items:flex-start}
.info-icon{font-size:20px;flex-shrink:0;margin-top:2px}
.info-card strong{display:block;font-size:14px;font-weight:700;color:#0a0a0a;margin-bottom:4px}
.info-card p{font-size:13px;color:#6b7280;line-height:1.6;margin:0}
.form-card{background:#f8fafc;border:1px solid #e2e8f0;border-radius:16px;padding:36px}
.form-card h2{font-size:18px;font-weight:700;margin-bottom:6px}
.form-card>p{font-size:13px;color:#6b7280;margin-bottom:24px}
.field{display:flex;flex-direction:column;gap:6px;margin-bottom:16px}
.field label{font-size:13px;font-weight:600;color:#374151}
.field input,.field select,.field textarea{padding:12px 14px;border-radius:10px;border:1.5px solid #d1d5db;font-size:14px;color:#0a0a0a;background:#fff;outline:none;font-family:inherit;width:100%;transition:border-color .15s}
.field input:focus,.field select:focus,.field textarea:focus{border-color:#2563eb}
.field textarea{resize:vertical;min-height:100px}
.btn-primary{width:100%;background:#2563eb;color:#fff;border:none;padding:14px 24px;border-radius:100px;font-size:14px;font-weight:600;cursor:pointer;transition:background .2s,transform .15s;font-family:inherit}
.btn-primary:hover:not(:disabled){background:#1d4ed8;transform:translateY(-1px)}
.btn-primary:disabled{background:#10b981;cursor:default}
.lf{border-top:1px solid #e2e8f0;padding:20px 0;text-align:center}.lf p{font-size:12px;color:#9ca3af}
@media(max-width:768px){.contact-grid{grid-template-columns:1fr;gap:40px}.contact-content{padding:40px 20px 60px}}
</style>
