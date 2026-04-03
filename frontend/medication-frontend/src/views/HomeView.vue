<template>
  <div class="landing">

    <!-- NAVBAR -->
    <nav class="navbar" :class="{ scrolled: isScrolled }">
      <div class="nav-inner">
        <div class="logo">
          <span class="logo-mark">M</span>
          <span class="logo-text">MedFlow</span>
        </div>
        <div class="nav-links">
          <a href="#como-funciona" @click.prevent="scrollTo('como-funciona')">Como funciona</a>
          <a href="#planos" @click.prevent="scrollTo('planos')">Planos</a>
          <button @click="$router.push('/setup-box')" class="btn-nav">Começar agora</button>
        </div>
        <button class="hamburger" @click="menuOpen = !menuOpen" :class="{ active: menuOpen }">
          <span></span><span></span>
        </button>
      </div>
      <div class="mobile-menu" :class="{ open: menuOpen }">
        <a href="#como-funciona" @click="menuOpen = false; scrollTo('como-funciona')">Como funciona</a>
        <a href="#planos" @click="menuOpen = false; scrollTo('planos')">Planos</a>
        <button @click="$router.push('/setup-box')" class="btn-nav">Começar agora</button>
      </div>
    </nav>

    <!-- HERO -->
    <header class="hero">
      <div class="hero-inner">
        <h1>
          Tomar remédio<br>
          <em>na hora certa</em><br>
          sem precisar lembrar.
        </h1>
        <p class="hero-sub">
          Seus medicamentos chegam separados por dia e horário,<br class="br-desktop">
          em sachês prontos pra usar. Você só abre e toma.
        </p>
        <div class="hero-actions">
          <button @click="$router.push('/setup-box')" class="btn-primary">
            Monte sua box
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
              <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                stroke-linejoin="round" />
            </svg>
          </button>
          <a href="#como-funciona" @click.prevent="scrollTo('como-funciona')" class="link-secondary">Ver como
            funciona</a>
        </div>
        <p class="hero-note">Sem contrato. Cancele quando quiser.</p>
      </div>

      <div class="hero-visual">
        <div class="box-mockup">
          <div class="box-day" v-for="(day, i) in mockDays" :key="i" :style="{ animationDelay: `${i * 0.08}s` }">
            <span class="day-label">{{ day.label }}</span>
            <div class="sachets">
              <div class="sachet" v-for="(s, j) in day.sachets" :key="j" :style="{ background: s.color }">
                <span>{{ s.time }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </header>

    <!-- PROVA SOCIAL -->
    <section class="social-proof">
      <div class="proof-inner">
        <div class="avatars">
          <div class="avatar" v-for="n in 5" :key="n" :style="{ background: avatarColors[n - 1] }">{{ avatarLetters[n - 1]
            }}</div>
        </div>
        <p><strong>+2.400 pessoas</strong> já não precisam mais lembrar de tomar remédio.</p>
      </div>
    </section>

    <!-- COMO FUNCIONA -->
    <section class="how" id="como-funciona">
      <div class="section-inner">
        <div class="section-label">Como funciona</div>
        <h2>Três passos.<br>Uma rotina mais tranquila.</h2>
        <div class="steps">
          <div class="step" v-for="(step, i) in steps" :key="i">
            <div class="step-number">{{ String(i + 1).padStart(2, '0') }}</div>
            <div class="step-content">
              <h3>{{ step.title }}</h3>
              <p>{{ step.desc }}</p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- BENEFÍCIOS -->
    <section class="benefits">
      <div class="section-inner">
        <div class="benefits-grid">
          <div class="benefit-main">
            <div class="section-label">Por que funciona</div>
            <h2>Pensado pra quem esquece.<br>E pra quem não quer esquecer.</h2>
            <p>A gente não julga. Manter uma rotina de medicamentos é difícil. Por isso organizamos tudo pra você, sem
              complicação.</p>
            <button @click="$router.push('/setup-box')" class="btn-primary">
              Quero experimentar
              <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                  stroke-linejoin="round" />
              </svg>
            </button>
          </div>
          <div class="benefit-cards">
            <div class="benefit-card" v-for="(b, i) in benefits" :key="i">
              <div class="benefit-icon">{{ b.icon }}</div>
              <h4>{{ b.title }}</h4>
              <p>{{ b.desc }}</p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- DEPOIMENTO -->
    <section class="testimonial">
      <div class="section-inner">
        <blockquote>
          <p>"Minha mãe tem 73 anos e tomava seis remédios diferentes. Era um caos. Desde que começamos com o MedFlow,
            ela mesma consegue se organizar. Isso vale muito."</p>
          <cite>
            <div class="cite-avatar" style="background: #d4e8d4">R</div>
            <div>
              <strong>Renata M.</strong>
              <span>São Paulo, SP</span>
            </div>
          </cite>
        </blockquote>
      </div>
    </section>

    <!-- PLANOS -->
    <section class="pricing" id="planos">
      <div class="section-inner">
        <div class="section-label">Planos</div>
        <h2>Simples assim.</h2>
        <div class="plans">
          <div class="plan" v-for="(plan, i) in plans" :key="i" :class="{ featured: plan.featured }">
            <div class="plan-tag" v-if="plan.featured">Mais escolhido</div>
            <h3>{{ plan.name }}</h3>
            <div class="plan-price">
              <span class="currency">R$</span>
              <span class="amount">{{ plan.price }}</span>
              <span class="period">/mês</span>
            </div>
            <p class="plan-desc">{{ plan.desc }}</p>
            <ul>
              <li v-for="(item, j) in plan.items" :key="j">
                <svg width="16" height="16" viewBox="0 0 16 16">
                  <path d="M3 8l3.5 3.5L13 5" stroke="#4a9b6f" stroke-width="1.5" stroke-linecap="round"
                    stroke-linejoin="round" fill="none" />
                </svg>
                {{ item }}
              </li>
            </ul>
            <button @click="$router.push('/setup-box')" :class="plan.featured ? 'btn-primary' : 'btn-outline'">
              Começar com este plano
            </button>
          </div>
        </div>
      </div>
    </section>

    <!-- CTA FINAL -->
    <section class="final-cta">
      <div class="section-inner">
        <h2>Pronto pra simplificar<br>sua rotina de remédios?</h2>
        <p>Leva menos de 5 minutos pra montar sua box. Sem burocracia.</p>
        <button @click="$router.push('/setup-box')" class="btn-primary large">
          Montar minha box agora
          <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
            <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
              stroke-linejoin="round" />
          </svg>
        </button>
        <p class="final-note">Sem contrato · Entrega em todo o Brasil · Cancele a qualquer momento</p>
      </div>
    </section>

    <!-- FOOTER -->
    <footer class="footer">
      <div class="footer-inner">
        <div class="logo">
          <span class="logo-mark">M</span>
          <span class="logo-text">MedFlow</span>
        </div>
        <p>Feito com cuidado para quem cuida da saúde.</p>
        <div class="footer-links">
          <a href="#">Termos de uso</a>
          <a href="#">Privacidade</a>
          <a href="#">Contato</a>
        </div>
      </div>
    </footer>

  </div>
</template>

<script>
export default {
  name: 'LandingPage',
  data() {
    return {
      isScrolled: false,
      menuOpen: false,
      avatarColors: ['#d4e8d4', '#e8d4d4', '#d4d8e8', '#e8e4d4', '#d4e8e4'],
      avatarLetters: ['A', 'L', 'M', 'P', 'C'],
      mockDays: [
        { label: 'Seg', sachets: [{ time: '07h', color: '#bbf7d0' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Ter', sachets: [{ time: '07h', color: '#bbf7d0' }, { time: '14h', color: '#fde68a' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Qua', sachets: [{ time: '07h', color: '#bbf7d0' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Qui', sachets: [{ time: '07h', color: '#bbf7d0' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Sex', sachets: [{ time: '07h', color: '#bbf7d0' }, { time: '14h', color: '#fde68a' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Sáb', sachets: [{ time: '07h', color: '#bbf7d0' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Dom', sachets: [{ time: '07h', color: '#bbf7d0' }] },
      ],
      steps: [
        { title: 'Você nos conta seus medicamentos', desc: 'Informe os remédios, doses e horários. Nosso farmacêutico revisa tudo antes de montar.' },
        { title: 'A gente organiza tudo em sachês', desc: 'Cada sachê é identificado com o dia e o horário. Sem confusão, sem erro.' },
        { title: 'Receba em casa todo mês', desc: 'Entregamos em qualquer lugar do Brasil. Você fica no controle sem precisar se preocupar.' },
      ],
      benefits: [
        { icon: '◎', title: 'Sachês por horário', desc: 'Manhã, tarde, noite — cada dose já separada. Você só abre e toma.' },
        { icon: '⟳', title: 'Entrega automática', desc: 'Nunca mais corra pra farmácia no último dia. Chega antes de acabar.' },
        { icon: '⌥', title: 'Revisão farmacêutica', desc: 'Um farmacêutico confere cada box antes de sair. Segurança real.' },
        { icon: '◈', title: 'Sem contrato', desc: 'Assine, pause ou cancele quando quiser. Sem multa, sem burocracia.' },
      ],
      plans: [
        {
          name: 'Essencial',
          price: '89',
          desc: 'Pra quem tem poucos medicamentos e quer começar simples.',
          featured: false,
          items: ['Até 3 medicamentos', 'Sachês diários', 'Entrega mensal', 'Suporte por chat'],
        },
        {
          name: 'Completo',
          price: '149',
          desc: 'O mais escolhido por quem tem rotina de tratamento contínuo.',
          featured: true,
          items: ['Medicamentos ilimitados', 'Sachês por horário', 'Revisão farmacêutica', 'Entrega mensal', 'Suporte prioritário'],
        },
        {
          name: 'Família',
          price: '229',
          desc: 'Para quem cuida de mais de uma pessoa em casa.',
          featured: false,
          items: ['Até 2 pessoas', 'Medicamentos ilimitados', 'Sachês por horário', 'Revisão farmacêutica', 'Entrega mensal'],
        },
      ],
    };
  },
  mounted() {
    window.addEventListener('scroll', this.handleScroll);
  },
  beforeUnmount() {
    window.removeEventListener('scroll', this.handleScroll);
  },
  methods: {
    handleScroll() {
      this.isScrolled = window.scrollY > 40;
    },
    scrollTo(id) {
      const el = document.getElementById(id);
      if (el) el.scrollIntoView({ behavior: 'smooth', block: 'start' });
    },
  },
};
</script>

<style scoped>
/* ─── TOKENS ─────────────────────────────────── */
/* Usando .landing como escopo para evitar conflito com CSS externo */
.landing {
  --green: #166534;
  --green-hover: #14532d;
  --green-light: #22c55e;
  --green-pale: #f0fdf4;
  --green-pale-border: #86efac;
  --text: #0a0a0a;
  --text-secondary: #1f2937;
  --muted: #374151;
  --border: #d1d5db;
  --white: #ffffff;
  --bg-subtle: #f1f5f9;
  --radius: 16px;
  --font-display: 'Georgia', serif;
  --font-body: 'DM Sans', 'Helvetica Neue', sans-serif;
  --transition: 0.2s ease;
}

/* ─── RESET ───────────────────────────────────── */
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

.landing {
  font-family: 'DM Sans', 'Helvetica Neue', sans-serif;
  color: #0a0a0a;
  background: #ffffff;
  overflow-x: hidden;
}

/* ─── NAVBAR ──────────────────────────────────── */
.navbar {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 100;
  transition: background var(--transition), box-shadow var(--transition);
}

.navbar.scrolled {
  background: rgba(255, 255, 255, 0.95);
  backdrop-filter: blur(12px);
  box-shadow: 0 1px 0 var(--border);
}

.nav-inner {
  max-width: 1500px;
  margin: 0 auto;
  padding: 20px 32px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.logo {
  display: flex;
  align-items: center;
  gap: 8px;
  text-decoration: none;
}

.logo-mark {
  width: 32px;
  height: 32px;
  background: var(--green);
  color: white;
  border-radius: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 700;
  font-size: 16px;
}

.logo-text {
  font-size: 18px;
  font-weight: 600;
  color: var(--text);
}

.nav-links {
  display: flex;
  align-items: center;
  gap: 32px;
}

.nav-links a {
  color: var(--text-secondary);
  text-decoration: none;
  font-size: 15px;
  font-weight: 500;
  transition: color var(--transition);
}

.nav-links a:hover {
  color: var(--text);
}

.btn-nav {
  background: #166534;
  color: #ffffff;
  border: none;
  padding: 10px 20px;
  border-radius: 100px;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s ease;
}

.btn-nav:hover {
  background: #14532d;
}

.hamburger {
  display: none;
  flex-direction: column;
  gap: 5px;
  background: none;
  border: none;
  cursor: pointer;
  padding: 4px;
}

.hamburger span {
  display: block;
  width: 22px;
  height: 2px;
  background: var(--text);
  border-radius: 2px;
  transition: transform 0.2s;
}

.hamburger.active span:first-child {
  transform: translateY(7px) rotate(45deg);
}

.hamburger.active span:last-child {
  transform: translateY(-7px) rotate(-45deg);
}

.mobile-menu {
  display: none;
  flex-direction: column;
  padding: 16px 32px 24px;
  gap: 16px;
  background: white;
  border-top: 1px solid var(--border);
}

.mobile-menu.open {
  display: flex;
}

.mobile-menu a {
  color: var(--muted);
  text-decoration: none;
  font-size: 16px;
}

/* ─── HERO ────────────────────────────────────── */
.hero {
  min-height: 100vh;
  display: flex;
  align-items: center;
  padding: 120px 32px 80px;
  max-width: 1550px;
  margin: 0 auto;
  gap: 60px;
}

.hero-inner {
  flex: 1;
}

.hero-visual {
  flex: 1;
  display: flex;
  justify-content: center;
}

.hero-badge {
  display: inline-flex;
  align-items: center;
  gap: 8px;
  background: var(--green-pale);
  color: var(--green);
  font-size: 13px;
  font-weight: 600;
  padding: 6px 14px;
  border-radius: 100px;
  border: 1px solid var(--green-pale-border);
  margin-bottom: 28px;
}

.hero h1 {
  font-family: 'Georgia', serif;
  font-size: clamp(2.4rem, 5vw, 3.8rem);
  line-height: 1.15;
  letter-spacing: -0.02em;
  color: #0a0a0a;
  font-weight: 700;
  margin-bottom: 24px;
}

.hero h1 em {
  font-style: italic;
  color: #166534;
}

.hero-sub {
  font-size: 17px;
  color: #1f2937;
  line-height: 1.7;
  margin-bottom: 36px;
  font-weight: 400;
}

.hero-actions {
  display: flex;
  align-items: center;
  gap: 20px;
  flex-wrap: wrap;
  margin-bottom: 16px;
}

.btn-primary {
  display: inline-flex;
  align-items: center;
  gap: 8px;
  background: #166534;
  color: #ffffff;
  border: none;
  padding: 14px 24px;
  border-radius: 100px;
  font-size: 15px;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s ease, transform 0.2s ease;
}

.btn-primary:hover {
  background: #14532d;
  transform: translateY(-1px);
}

.btn-primary.large {
  padding: 16px 32px;
  font-size: 16px;
}

.btn-outline {
  display: inline-flex;
  align-items: center;
  gap: 8px;
  background: #ffffff;
  color: #0a0a0a;
  border: 1.5px solid #d1d5db;
  padding: 13px 24px;
  border-radius: 100px;
  font-size: 15px;
  font-weight: 600;
  cursor: pointer;
  transition: border-color 0.2s ease, background 0.2s ease;
  width: 100%;
  justify-content: center;
}

.btn-outline:hover {
  border-color: #166534;
  color: #166534;
  background: #f0fdf4;
}

.link-secondary {
  color: #1f2937;
  font-size: 15px;
  font-weight: 600;
  text-decoration: underline;
  text-decoration-color: #9ca3af;
  cursor: pointer;
  transition: color var(--transition), text-decoration-color var(--transition);
}

.link-secondary:hover {
  color: var(--green);
  text-decoration-color: var(--green);
}

.hero-note {
  font-size: 13px;
  color: var(--muted);
  font-weight: 500;
}

/* ─── BOX MOCKUP ──────────────────────────────── */
.box-mockup {
  background: #e8edf2;
  border: 1px solid #c8d3de;
  border-radius: 24px;
  padding: 24px;
  display: flex;
  flex-direction: column;
  gap: 12px;
  width: 100%;
  max-width: 360px;
  box-shadow: 0 20px 60px rgba(0, 0, 0, 0.13);
}

.box-day {
  display: flex;
  align-items: center;
  gap: 12px;
  animation: fadeSlideIn 0.5s ease both;
}

@keyframes fadeSlideIn {
  from {
    opacity: 0;
    transform: translateX(12px);
  }

  to {
    opacity: 1;
    transform: translateX(0);
  }
}

.day-label {
  font-size: 12px;
  font-weight: 700;
  color: var(--text-secondary);
  width: 28px;
  flex-shrink: 0;
}

.sachets {
  display: flex;
  gap: 6px;
}

.sachet {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 6px 10px;
  border-radius: 8px;
  font-size: 11px;
  font-weight: 800;
  color: #0a0a0a;
  border: 1px solid rgba(0, 0, 0, 0.12);
}

/* ─── SOCIAL PROOF ────────────────────────────── */
.social-proof {
  border-top: 1px solid var(--border);
  border-bottom: 1px solid var(--border);
  padding: 28px 32px;
}

.proof-inner {
  max-width: 1500px;
  margin: 0 auto;
  display: flex;
  align-items: center;
  gap: 16px;
}

.avatars {
  display: flex;
}

.avatar {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 13px;
  font-weight: 600;
  color: #374151;
  border: 2px solid white;
  margin-left: -8px;
}

.avatar:first-child {
  margin-left: 0;
}

.proof-inner p {
  font-size: 15px;
  color: var(--text-secondary);
  font-weight: 500;
}

.proof-inner p strong {
  color: var(--text);
}

/* ─── SECTIONS SHARED ─────────────────────────── */
.section-inner {
  max-width: 1500px;
  margin: 0 auto;
  padding: 0 32px;
}

.section-label {
  font-size: 12px;
  font-weight: 700;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: #166534;
  margin-bottom: 16px;
}

section h2 {
  font-family: 'Georgia', serif;
  font-size: clamp(1.8rem, 3.5vw, 2.8rem);
  line-height: 1.2;
  letter-spacing: -0.02em;
  margin-bottom: 20px;
  color: #0a0a0a;
  font-weight: 700;
}

/* ─── HOW IT WORKS ────────────────────────────── */
.how {
  padding: 100px 0;
  background: #f0fdf4;
  border-top: 2px solid #bbf7d0;
  border-bottom: 2px solid #bbf7d0;
}

.how h2 {
  margin-bottom: 56px;
}

.steps {
  display: flex;
  flex-direction: column;
  gap: 0;
  position: relative;
}

.steps::before {
  content: '';
  position: absolute;
  left: 20px;
  top: 40px;
  bottom: 40px;
  width: 1px;
  background: var(--border);
}

.step {
  display: flex;
  gap: 28px;
  padding: 32px 0;
  position: relative;
}

.step+.step {
  border-top: 1px solid rgba(0, 0, 0, 0.06);
}

.step-number {
  font-size: 13px;
  font-weight: 700;
  color: var(--green);
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: white;
  border: 2px solid var(--green);
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  position: relative;
  z-index: 1;
}

.step-content h3 {
  font-size: 17px;
  font-weight: 600;
  margin-bottom: 6px;
  color: var(--text);
}

.step-content p {
  font-size: 15px;
  color: var(--text-secondary);
  line-height: 1.6;
}

/* ─── BENEFITS ────────────────────────────────── */
.benefits {
  padding: 100px 0;
}

.benefits-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 80px;
  align-items: center;
}

.benefit-main p {
  font-size: 16px;
  color: var(--text-secondary);
  line-height: 1.7;
  margin-bottom: 32px;
}

.benefit-cards {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 16px;
}

.benefit-card {
  background: var(--bg-subtle);
  border: 1.5px solid var(--border);
  border-radius: var(--radius);
  padding: 24px;
  transition: box-shadow var(--transition), transform var(--transition);
}

.benefit-card:hover {
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
  transform: translateY(-2px);
}

.benefit-icon {
  font-size: 20px;
  margin-bottom: 12px;
  color: var(--green);
}

.benefit-card h4 {
  font-size: 15px;
  font-weight: 700;
  margin-bottom: 6px;
  color: var(--text);
}

.benefit-card p {
  font-size: 14px;
  color: var(--text-secondary);
  line-height: 1.6;
}

/* ─── TESTIMONIAL ─────────────────────────────── */
.testimonial {
  padding: 80px 0;
  background: #f0fdf4;
  border-top: 2px solid #bbf7d0;
  border-bottom: 2px solid #bbf7d0;
}

blockquote {
  max-width: 680px;
  margin: 0 auto;
  text-align: center;
}

blockquote p {
  font-family: var(--font-display);
  font-size: clamp(1.2rem, 2.5vw, 1.5rem);
  line-height: 1.6;
  color: var(--text);
  margin-bottom: 28px;
  font-style: italic;
}

cite {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 12px;
  font-style: normal;
}

.cite-avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 15px;
  font-weight: 700;
  color: #1f2937;
  border: 2px solid var(--green-pale-border);
}

cite strong {
  display: block;
  font-size: 15px;
  color: var(--text);
}

cite span {
  font-size: 13px;
  color: var(--muted);
  font-weight: 500;
}

/* ─── PRICING ─────────────────────────────────── */
.pricing {
  padding: 100px 0;
  background: #ffffff;
}

.pricing h2 {
  margin-bottom: 12px;
  color: #0a0a0a !important;
}

.pricing .section-label {
  color: #166534 !important;
}

.pricing>.section-inner>p {
  color: #1f2937;
  margin-bottom: 48px;
}

.plans {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 20px;
  align-items: start;
}

.plan {
  border: 1px solid #d1d5db;
  border-radius: 20px;
  padding: 32px;
  position: relative;
  transition: box-shadow 0.2s ease, transform 0.2s ease;
  background: #ffffff;
}

.plan:hover {
  box-shadow: 0 12px 40px rgba(0, 0, 0, 0.10);
  transform: translateY(-2px);
}

.plan.featured {
  border: 2px solid #166534;
  background: #f0fdf4;
}

.plan-tag {
  position: absolute;
  top: -13px;
  left: 50%;
  transform: translateX(-50%);
  background: #166534;
  color: #ffffff;
  font-size: 11px;
  font-weight: 700;
  padding: 5px 14px;
  border-radius: 100px;
  white-space: nowrap;
  letter-spacing: 0.04em;
}

.plan h3 {
  font-size: 15px;
  font-weight: 700;
  margin-bottom: 16px;
  color: #0a0a0a;
  letter-spacing: 0.01em;
  text-transform: uppercase;
}

.plan-price {
  display: flex;
  align-items: baseline;
  gap: 2px;
  margin-bottom: 10px;
}

.currency {
  font-size: 18px;
  color: #374151;
  font-weight: 700;
}

.amount {
  font-size: 52px;
  font-weight: 800;
  line-height: 1;
  color: #0a0a0a;
  letter-spacing: -2px;
}

.period {
  font-size: 14px;
  color: #6b7280;
  margin-left: 4px;
  font-weight: 500;
}

.plan-desc {
  font-size: 14px;
  color: #374151;
  line-height: 1.6;
  margin-bottom: 24px;
  padding-bottom: 24px;
  border-bottom: 1px solid #e5e7eb;
}

.plan.featured .plan-desc {
  border-bottom-color: #bbf7d0;
}

.plan ul {
  list-style: none;
  margin-bottom: 28px;
  display: flex;
  flex-direction: column;
  gap: 12px;
  padding: 0;
}

.plan li {
  display: flex;
  align-items: center;
  gap: 10px;
  font-size: 14px;
  color: #1f2937;
  font-weight: 500;
}

.plan li svg {
  flex-shrink: 0;
}

/* ─── FINAL CTA ───────────────────────────────── */
.final-cta {
  padding: 120px 0;
  text-align: center;
  background: #0f1a14;
  color: #ffffff;
}

.final-cta h2 {
  font-family: 'Georgia', serif;
  color: #ffffff !important;
  margin-bottom: 16px;
}

.final-cta>.section-inner>p {
  font-size: 17px;
  color: #86efac;
  margin-bottom: 40px;
  font-weight: 500;
}

.final-cta .btn-primary {
  background: #ffffff;
  color: #0f1a14;
}

.final-cta .btn-primary:hover {
  background: #f0fdf4;
}

.final-note {
  margin-top: 20px;
  font-size: 13px;
  color: #4ade80;
  font-weight: 500;
}

/* ─── FOOTER ──────────────────────────────────── */
.footer {
  padding: 40px 0;
  border-top: 1px solid var(--border);
}

.footer-inner {
  max-width: 1500px;
  margin: 0 auto;
  padding: 0 32px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  flex-wrap: wrap;
  gap: 16px;
}

.footer p {
  font-size: 14px;
  color: var(--text-secondary);
  font-weight: 500;
}

.footer-links {
  display: flex;
  gap: 24px;
}

.footer-links a {
  font-size: 13px;
  color: var(--muted);
  text-decoration: none;
  font-weight: 500;
  transition: color var(--transition);
}

.footer-links a:hover {
  color: var(--text);
}

/* ─── RESPONSIVE ──────────────────────────────── */
@media (max-width: 900px) {
  .hero {
    flex-direction: column;
    padding: 100px 24px 60px;
    text-align: center;
  }

  .hero-actions {
    justify-content: center;
  }

  .hero-visual {
    width: 100%;
  }

  .box-mockup {
    max-width: 100%;
  }

  .benefits-grid {
    grid-template-columns: 1fr;
    gap: 48px;
  }

  .plans {
    grid-template-columns: 1fr;
    max-width: 400px;
    margin: 0 auto;
  }

  .nav-links {
    display: none;
  }

  .hamburger {
    display: flex;
  }

  .proof-inner {
    flex-direction: column;
    text-align: center;
  }

  .footer-inner {
    flex-direction: column;
    text-align: center;
  }

  .footer-links {
    justify-content: center;
  }
}

@media (max-width: 600px) {
  .benefit-cards {
    grid-template-columns: 1fr;
  }

  section h2 {
    font-size: 1.8rem;
  }

  .how,
  .benefits,
  .pricing {
    padding: 72px 0;
  }

  .section-inner {
    padding: 0 20px;
  }
}

@media (max-width: 1200px) {
  /* Dá um "respiro" nas laterais para não grudar na tela */
  .section-inner, 
  .nav-inner, 
  .hero, 
  .proof-inner, 
  .footer-inner {
    padding: 0 40px; 
  }

  /* Diminui um pouco o título gigante */
  .hero h1 {
    font-size: 3rem;
  }

  /* Ajusta o grid de benefícios para não ficar espremido */
  .benefits-grid {
    gap: 40px;
  }
}

</style>