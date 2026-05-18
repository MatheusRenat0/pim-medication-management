<template>
  <div class="legal-page">
    <nav class="legal-nav"><div class="legal-nav-inner">
      <a class="logo" @click="$router.push('/')"><img src="/logo.png" alt="MedFlow" style="width:28px;height:28px;object-fit:contain;" /><span class="logo-text">MedFlow</span></a>
      <button class="btn-back" @click="$router.push('/')">← Voltar</button>
    </div></nav>
    <main class="lc">
      <h1>Política de Privacidade</h1>
      <p class="upd">Última atualização: 15 de maio de 2026</p>
      <section><h2>1. Introdução</h2><p>A MedFlow Tecnologia em Saúde Ltda., com sede em São Paulo/SP, é a controladora dos dados pessoais tratados por esta plataforma, em conformidade com a <strong>Lei Geral de Proteção de Dados Pessoais (Lei nº 13.709/2018 — LGPD)</strong>.</p></section>
      <section><h2>2. Dados Coletados</h2><ul><li><strong>Identificação:</strong> nome, CPF, e-mail</li><li><strong>Acesso:</strong> senha (criptografada)</li><li><strong>Saúde (dados sensíveis - Art. 5º, II LGPD):</strong> receitas médicas, medicamentos, dosagens e horários</li><li><strong>Entrega:</strong> endereço</li><li><strong>Navegação:</strong> IP, navegador, páginas acessadas</li></ul></section>
      <section><h2>3. Finalidade</h2><ul><li>Criação e gerenciamento de conta</li><li>Montagem de sachês de medicamentos</li><li>Validação farmacêutica de receitas</li><li>Processamento e entrega de pedidos</li><li>Cumprimento de obrigações legais (ANVISA, Vigilância Sanitária)</li></ul></section>
      <section><h2>4. Base Legal (Art. 7º e 11 da LGPD)</h2><ul><li><strong>Consentimento</strong> (Art. 7º, I) — dados de saúde</li><li><strong>Execução de contrato</strong> (Art. 7º, V) — prestação do serviço</li><li><strong>Tutela da saúde</strong> (Art. 11, II, f) — dados sensíveis</li><li><strong>Obrigação legal</strong> (Art. 7º, II) — normas ANVISA</li></ul></section>
      <section><h2>5. Compartilhamento</h2><p>Dados poderão ser compartilhados com farmacêuticos responsáveis, empresas de logística (apenas dados de entrega), autoridades sanitárias e prestadores de TI. O MedFlow <strong>não comercializa</strong> dados pessoais.</p></section>
      <section><h2>6. Segurança (Art. 46 LGPD)</h2><ul><li>Criptografia de senhas e dados sensíveis</li><li>Controle de acesso por perfis</li><li>Monitoramento e registro de acessos</li><li>Backups periódicos</li></ul></section>
      <section><h2>7. Retenção</h2><ul><li>Receitas: prazos da Portaria 344/98 ANVISA</li><li>Transações: 5 anos (CDC)</li><li>Cadastro: enquanto conta ativa</li></ul></section>
      <section><h2>8. Direitos do Titular (Art. 18 LGPD)</h2><p>Acesso, correção, anonimização, portabilidade, eliminação e revogação do consentimento. Contato: <strong>privacidade@medflow.com.br</strong></p></section>
      <section><h2>9. Encarregado (DPO)</h2><p>Lucas Oliveira da Silva — dpo@medflow.com.br — Av. Paulista, 1.000 — São Paulo/SP — CEP 01310-100</p></section>
      <section><h2>10. Legislação</h2><p>Regida pela LGPD (Lei 13.709/2018) e Marco Civil da Internet (Lei 12.965/2014). Foro: Comarca de São Paulo/SP.</p></section>
    </main>
    <footer class="lf"><p>© 2026 MedFlow. Todos os direitos reservados.</p></footer>
  </div>
</template>
<script setup></script>
<style scoped>
.legal-page{font-family:'Inter','DM Sans',sans-serif;color:#0a0a0a;background:#fff;min-height:100vh;display:flex;flex-direction:column}
.legal-nav{border-bottom:1px solid #e2e8f0;padding:14px 0}.legal-nav-inner{max-width:800px;margin:0 auto;padding:0 32px;display:flex;justify-content:space-between;align-items:center}
.logo{display:flex;align-items:center;gap:10px;cursor:pointer;text-decoration:none}.logo-icon{width:34px;height:34px;background:#1d4ed8;border-radius:10px;display:flex;align-items:center;justify-content:center}.logo-text{font-size:17px;font-weight:700;color:#0a0a0a}
.btn-back{background:none;border:none;color:#6b7280;font-size:13px;font-weight:500;cursor:pointer;font-family:inherit}.btn-back:hover{color:#1d4ed8}
.lc{max-width:800px;margin:0 auto;padding:48px 32px 80px;flex:1}.lc h1{font-family:Georgia,serif;font-size:2rem;font-weight:700;margin-bottom:8px;letter-spacing:-0.02em}.upd{font-size:13px;color:#6b7280;margin-bottom:40px}
.lc section{margin-bottom:32px}.lc h2{font-size:16px;font-weight:700;margin-bottom:12px}.lc p{font-size:14px;line-height:1.8;color:#374151;margin-bottom:12px}
.lc ul{list-style:none;padding:0;margin-bottom:12px}.lc li{font-size:14px;line-height:1.8;color:#374151;padding-left:20px;position:relative;margin-bottom:4px}.lc li::before{content:'•';position:absolute;left:4px;color:#2563eb;font-weight:bold}
.lf{border-top:1px solid #e2e8f0;padding:20px 0;text-align:center}.lf p{font-size:12px;color:#9ca3af}
@media(max-width:600px){.lc{padding:32px 20px 60px}.lc h1{font-size:1.6rem}}
</style>
