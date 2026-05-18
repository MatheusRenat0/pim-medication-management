<template>
  <div class="legal-page">
    <nav class="legal-nav">
      <div class="legal-nav-inner">
        <a class="logo" @click="$router.push('/')">
          <img src="/logo.png" alt="MedFlow" style="width:28px;height:28px;object-fit:contain;" />
          <span class="logo-text">MedFlow</span>
        </a>
        <button class="btn-back" @click="$router.push('/')">← Voltar para a home</button>
      </div>
    </nav>

    <main class="legal-content">
      <h1>Termos de Uso</h1>
      <p class="updated">Última atualização: 15 de maio de 2026</p>

      <section>
        <h2>1. Aceitação dos Termos</h2>
        <p>Ao acessar e utilizar a plataforma MedFlow, o usuário declara que leu, compreendeu e concorda com os presentes Termos de Uso. Caso não concorde com qualquer disposição, recomenda-se que interrompa imediatamente o uso da plataforma.</p>
      </section>

      <section>
        <h2>2. Descrição do Serviço</h2>
        <p>O MedFlow é uma plataforma digital de gestão de medicamentos que oferece os seguintes serviços:</p>
        <ul>
          <li>Organização de medicamentos em sachês fracionados por dia e horário;</li>
          <li>Gerenciamento de receitas médicas digitais;</li>
          <li>Controle de estoque farmacêutico;</li>
          <li>Acompanhamento de tratamentos contínuos;</li>
          <li>Entrega domiciliar de medicamentos.</li>
        </ul>
        <p>O MedFlow <strong>não substitui consultas médicas</strong>, diagnósticos ou prescrições. Todo tratamento deve ser prescrito por um profissional de saúde habilitado.</p>
      </section>

      <section>
        <h2>3. Cadastro e Responsabilidades do Usuário</h2>
        <p>Para utilizar os serviços, o usuário deve:</p>
        <ul>
          <li>Fornecer informações verdadeiras, completas e atualizadas no momento do cadastro;</li>
          <li>Manter a confidencialidade de sua senha de acesso;</li>
          <li>Informar imediatamente qualquer uso não autorizado de sua conta;</li>
          <li>Ter no mínimo 18 anos ou ser representado por responsável legal.</li>
        </ul>
        <p>O usuário é integralmente responsável por todas as atividades realizadas em sua conta.</p>
      </section>

      <section>
        <h2>4. Receitas Médicas</h2>
        <p>O envio de receitas médicas pela plataforma está sujeito às seguintes condições:</p>
        <ul>
          <li>Toda receita enviada será validada por um farmacêutico responsável (CRF/SP) antes da dispensação;</li>
          <li>Receitas de medicamentos controlados (Portaria 344/98 da ANVISA) devem seguir as normas vigentes de validade e retenção;</li>
          <li>O MedFlow reserva-se o direito de recusar receitas ilegíveis, vencidas ou com indícios de adulteração;</li>
          <li>A responsabilidade pela prescrição é exclusivamente do profissional de saúde prescritor.</li>
        </ul>
      </section>

      <section>
        <h2>5. Uso Adequado da Plataforma</h2>
        <p>É vedado ao usuário:</p>
        <ul>
          <li>Utilizar a plataforma para fins ilícitos ou que violem a legislação brasileira;</li>
          <li>Compartilhar medicamentos ou receitas de terceiros sem autorização;</li>
          <li>Tentar acessar áreas restritas do sistema sem permissão;</li>
          <li>Utilizar meios automatizados para acessar ou coletar dados da plataforma.</li>
        </ul>
      </section>

      <section>
        <h2>6. Entrega de Medicamentos</h2>
        <p>O serviço de entrega é realizado conforme disponibilidade logística e está sujeito a:</p>
        <ul>
          <li>Prazo estimado de entrega informado no momento do pedido;</li>
          <li>Verificação de identidade do destinatário no ato da entrega;</li>
          <li>Restrições geográficas conforme área de cobertura;</li>
          <li>Condições climáticas ou operacionais que possam afetar o prazo.</li>
        </ul>
      </section>

      <section>
        <h2>7. Limitação de Responsabilidade</h2>
        <p>O MedFlow não se responsabiliza por:</p>
        <ul>
          <li>Reações adversas decorrentes do uso de medicamentos prescritos por profissionais de saúde;</li>
          <li>Uso indevido dos medicamentos pelo paciente ou terceiros;</li>
          <li>Interrupções temporárias do serviço por manutenção ou força maior;</li>
          <li>Danos resultantes de informações incorretas fornecidas pelo usuário.</li>
        </ul>
      </section>

      <section>
        <h2>8. Cancelamento e Reembolso</h2>
        <p>O usuário pode cancelar sua assinatura a qualquer momento, sem multa ou penalidade. Pedidos já processados e em rota de entrega não poderão ser cancelados. Reembolsos seguem as normas do Código de Defesa do Consumidor (Lei nº 8.078/1990).</p>
      </section>

      <section>
        <h2>9. Alterações nos Termos</h2>
        <p>O MedFlow reserva-se o direito de alterar estes Termos de Uso a qualquer momento, notificando os usuários por e-mail ou pela plataforma com antecedência mínima de 15 dias.</p>
      </section>

      <section>
        <h2>10. Legislação Aplicável e Foro</h2>
        <p>Estes Termos são regidos pela legislação da República Federativa do Brasil. Fica eleito o foro da Comarca de São Paulo/SP para dirimir quaisquer controvérsias.</p>
      </section>
    </main>

    <footer class="legal-footer">
      <p>© 2026 MedFlow. Todos os direitos reservados.</p>
    </footer>
  </div>
</template>

<script setup>
</script>

<style scoped>
.legal-page { font-family: 'Inter', 'DM Sans', sans-serif; color: #0a0a0a; background: #fff; min-height: 100vh; display: flex; flex-direction: column; }
.legal-nav { border-bottom: 1px solid #e2e8f0; padding: 14px 0; }
.legal-nav-inner { max-width: 800px; margin: 0 auto; padding: 0 32px; display: flex; justify-content: space-between; align-items: center; }
.logo { display: flex; align-items: center; gap: 10px; cursor: pointer; text-decoration: none; }
.logo-icon { width: 34px; height: 34px; background: #1d4ed8; border-radius: 10px; display: flex; align-items: center; justify-content: center; }
.logo-text { font-size: 17px; font-weight: 700; color: #0a0a0a; }
.btn-back { background: none; border: none; color: #6b7280; font-size: 13px; font-weight: 500; cursor: pointer; font-family: inherit; }
.btn-back:hover { color: #1d4ed8; }
.legal-content { max-width: 800px; margin: 0 auto; padding: 48px 32px 80px; flex: 1; }
.legal-content h1 { font-family: Georgia, serif; font-size: 2rem; font-weight: 700; color: #0a0a0a; margin-bottom: 8px; letter-spacing: -0.02em; }
.updated { font-size: 13px; color: #6b7280; margin-bottom: 40px; }
.legal-content section { margin-bottom: 32px; }
.legal-content h2 { font-size: 16px; font-weight: 700; color: #0a0a0a; margin-bottom: 12px; }
.legal-content p { font-size: 14px; line-height: 1.8; color: #374151; margin-bottom: 12px; }
.legal-content ul { list-style: none; padding: 0; margin-bottom: 12px; }
.legal-content li { font-size: 14px; line-height: 1.8; color: #374151; padding-left: 20px; position: relative; margin-bottom: 4px; }
.legal-content li::before { content: '•'; position: absolute; left: 4px; color: #2563eb; font-weight: bold; }
.legal-footer { border-top: 1px solid #e2e8f0; padding: 20px 0; text-align: center; }
.legal-footer p { font-size: 12px; color: #9ca3af; }
@media (max-width: 600px) { .legal-content { padding: 32px 20px 60px; } .legal-content h1 { font-size: 1.6rem; } }
</style>
