<template>
  <div class="landing">

    <!-- NAVBAR -->
    <nav class="navbar" :class="{ scrolled: isScrolled }">
      <div class="nav-inner">

        <a class="logo" href="#">
          <div class="logo-icon">
            <img src="/logo.png" alt="MedFlow" style="width:28px;height:28px;object-fit:contain;" />
          </div>
          <span class="logo-text">MedFlow</span>
        </a>

        <div class="nav-links">
          <a
            href="#como-funciona"
            class="nav-link"
            :class="{ active: activeSection === 'como-funciona' }"
            @click.prevent="scrollTo('como-funciona')"
          >Como funciona</a>
          <a
            href="#planos"
            class="nav-link"
            :class="{ active: activeSection === 'planos' }"
            @click.prevent="scrollTo('planos')"
          >Planos</a>
          <a
            href="#familias"
            class="nav-link"
            :class="{ active: activeSection === 'familias' }"
            @click.prevent="scrollTo('familias')"
          >Para famílias</a>
        </div>

        <div class="nav-actions">
          <button class="btn-ghost" @click="$router.push('/login')">Entrar</button>
          <div class="nav-divider"></div>
          <button @click="$router.push('/setup-box')" class="btn-cta">
            Montar minha box
            <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
              <path d="M2.5 7h9M8 3.5l3.5 3.5L8 10.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
          </button>
        </div>

        <button class="hamburger" @click="menuOpen = !menuOpen" :class="{ active: menuOpen }">
          <span></span><span></span>
        </button>
      </div>

      <div class="mobile-menu" :class="{ open: menuOpen }">
        <a href="#como-funciona" @click="menuOpen = false; scrollTo('como-funciona')" class="mobile-link">Como funciona</a>
        <a href="#planos" @click="menuOpen = false; scrollTo('planos')" class="mobile-link">Planos</a>
        <a href="#familias" @click="menuOpen = false; scrollTo('familias')" class="mobile-link">Para famílias</a>
        <div class="mobile-divider"></div>
        <button class="btn-ghost-mobile" @click="$router.push('/login')">Entrar</button>
        <button @click="$router.push('/setup-box')" class="btn-cta mobile-cta">
          Montar minha box
          <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
            <path d="M2.5 7h9M8 3.5l3.5 3.5L8 10.5" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
        </button>
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
            <div class="cite-avatar" style="background: #dbeafe">R</div>
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
                  <path d="M3 8l3.5 3.5L13 5" stroke="#2563eb" stroke-width="1.5" stroke-linecap="round"
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

    <!-- PARA FAMÍLIAS -->
    <section class="families" id="familias">
      <div class="section-inner">
        <div class="families-grid">
          <div class="families-text">
            <div class="section-label">Para famílias</div>
            <h2>Cuide de quem<br>você <em>ama.</em></h2>
            <p class="families-desc">
              Gerencie a medicação de toda a família em uma única conta.
              Cada pessoa recebe seus próprios sachês organizados por horário,
              sem confusão entre remédios.
            </p>
            <div class="families-features">
              <div class="family-feature">
                <div class="ff-icon">👨‍👩‍👧</div>
                <div>
                  <strong>Até 4 pessoas por plano</strong>
                  <p>Adicione pais, avós ou filhos. Cada um com sua rotina personalizada.</p>
                </div>
              </div>
              <div class="family-feature">
                <div class="ff-icon">📋</div>
                <div>
                  <strong>Controle centralizado</strong>
                  <p>Veja todos os tratamentos em um único painel. Receba alertas de cada membro.</p>
                </div>
              </div>
              <div class="family-feature">
                <div class="ff-icon">🚚</div>
                <div>
                  <strong>Uma entrega, toda a família</strong>
                  <p>Todas as boxes chegam juntas, reduzindo custo de frete e simplificando a logística.</p>
                </div>
              </div>
            </div>
            <button @click="$router.push('/setup-box')" class="btn-primary">
              Montar box da família
              <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <path d="M3 8h10M9 4l4 4-4 4" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"
                  stroke-linejoin="round" />
              </svg>
            </button>
          </div>
          <div class="families-visual">
            <div class="family-card">
              <div class="fc-header"><div class="fc-avatar" style="background:#bfdbfe">M</div><div><strong>Maria, 73 anos</strong><span>6 medicamentos · 3x ao dia</span></div></div>
              <div class="fc-sachets"><span class="fc-sachet" style="background:#bfdbfe">07h</span><span class="fc-sachet" style="background:#fde68a">13h</span><span class="fc-sachet" style="background:#e9d5ff">21h</span></div>
            </div>
            <div class="family-card">
              <div class="fc-header"><div class="fc-avatar" style="background:#fce7f3">J</div><div><strong>José, 76 anos</strong><span>4 medicamentos · 2x ao dia</span></div></div>
              <div class="fc-sachets"><span class="fc-sachet" style="background:#bfdbfe">08h</span><span class="fc-sachet" style="background:#e9d5ff">20h</span></div>
            </div>
            <div class="family-card">
              <div class="fc-header"><div class="fc-avatar" style="background:#d9f99d">A</div><div><strong>Ana, 45 anos</strong><span>2 medicamentos · 1x ao dia</span></div></div>
              <div class="fc-sachets"><span class="fc-sachet" style="background:#bfdbfe">07h</span></div>
            </div>
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
          <img src="/logo.png" alt="MedFlow" style="width:32px;height:32px;object-fit:contain;" />
          <span class="logo-text">MedFlow</span>
        </div>
        <p>Feito com cuidado para quem cuida da saúde.</p>
        <div class="footer-links">
          <a href="#" @click.prevent="$router.push('/termos')">Termos de uso</a>
          <a href="#" @click.prevent="$router.push('/privacidade')">Privacidade</a>
          <a href="#" @click.prevent="$router.push('/contato')">Contato</a>
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
      activeSection: '',
      avatarColors: ['#dbeafe', '#fce7f3', '#dbeafe', '#fef9c3', '#dbeafe'],
      avatarLetters: ['A', 'L', 'M', 'P', 'C'],
      mockDays: [
        { label: 'Seg', sachets: [{ time: '07h', color: '#bfdbfe' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Ter', sachets: [{ time: '07h', color: '#bfdbfe' }, { time: '14h', color: '#fde68a' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Qua', sachets: [{ time: '07h', color: '#bfdbfe' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Qui', sachets: [{ time: '07h', color: '#bfdbfe' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Sex', sachets: [{ time: '07h', color: '#bfdbfe' }, { time: '14h', color: '#fde68a' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Sáb', sachets: [{ time: '07h', color: '#bfdbfe' }, { time: '22h', color: '#e9d5ff' }] },
        { label: 'Dom', sachets: [{ time: '07h', color: '#bfdbfe' }] },
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
    this.observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) this.activeSection = entry.target.id;
        });
      },
      { threshold: 0.3 }
    );
    ['como-funciona', 'planos', 'familias'].forEach((id) => {
      const el = document.getElementById(id);
      if (el) this.observer.observe(el);
    });
  },
  beforeUnmount() {
    window.removeEventListener('scroll', this.handleScroll);
    if (this.observer) this.observer.disconnect();
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
.landing {
  --blue: #1d4ed8;
  --blue-hover: #1e40af;
  --blue-light: #3b82f6;
  --blue-pale: #eff6ff;
  --blue-pale-border: #bfdbfe;
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
  transition: background var(--transition), box-shadow var(--transition), border-color var(--transition);
}

.nav-inner {
  max-width: 1500px;
  margin: 0 auto;
  padding: 12px 32px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 8px;
}

.navbar.scrolled .nav-inner {
  background: rgba(255, 255, 255, 0.97);
  backdrop-filter: blur(16px);
  box-shadow: 0 4px 24px rgba(0, 0, 0, 0.07);
  border-bottom: 1px solid #e2e8f0;
}

.logo {
  display: flex;
  align-items: center;
  gap: 10px;
  text-decoration: none;
  flex-shrink: 0;
  margin-right: 8px;
}

.logo-icon {
  width: 34px;
  height: 34px;
  background: #1d4ed8;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}

.logo-text {
  font-size: 17px;
  font-weight: 700;
  color: #0a0a0a;
  letter-spacing: -0.3px;
}

.logo-badge {
  font-size: 10px;
  font-weight: 700;
  letter-spacing: 0.06em;
  text-transform: uppercase;
  background: #eff6ff;
  color: #1d4ed8;
  border: 1px solid #bfdbfe;
  border-radius: 100px;
  padding: 2px 8px;
}

.nav-links {
  display: flex;
  align-items: center;
  gap: 2px;
  flex: 1;
  justify-content: center;
}

.nav-link {
  font-size: 14px;
  font-weight: 500;
  color: #374151;
  text-decoration: none;
  padding: 7px 14px;
  border-radius: 8px;
  transition: background 0.15s, color 0.15s;
  cursor: pointer;
}

.nav-link:hover {
  background: #f1f5f9;
  color: #0a0a0a;
}

.nav-link.active {
  background: #eff6ff;
  color: #1d4ed8;
  font-weight: 600;
}

.nav-actions {
  display: flex;
  align-items: center;
  gap: 8px;
  flex-shrink: 0;
}

.btn-ghost {
  font-size: 14px;
  font-weight: 500;
  color: #374151;
  background: none;
  border: none;
  padding: 8px 14px;
  border-radius: 8px;
  cursor: pointer;
  transition: background 0.15s, color 0.15s;
}

.btn-ghost:hover {
  background: #f1f5f9;
  color: #0a0a0a;
}

.nav-divider {
  width: 1px;
  height: 20px;
  background: #e2e8f0;
}

.btn-cta {
  font-size: 14px;
  font-weight: 600;
  color: #fff;
  background: #1d4ed8;
  border: none;
  padding: 9px 18px;
  border-radius: 10px;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  gap: 6px;
  transition: background 0.15s, transform 0.15s;
  letter-spacing: -0.1px;
  white-space: nowrap;
}

.btn-cta:hover {
  background: #1e40af;
  transform: translateY(-1px);
}

.hamburger {
  display: none;
  flex-direction: column;
  gap: 5px;
  background: none;
  border: none;
  cursor: pointer;
  padding: 6px;
  margin-left: auto;
}

.hamburger span {
  display: block;
  width: 22px;
  height: 2px;
  background: #0a0a0a;
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
  padding: 8px 16px 20px;
  gap: 2px;
  background: #ffffff;
  border-top: 1px solid #e2e8f0;
  box-shadow: 0 8px 24px rgba(0,0,0,0.08);
}

.mobile-menu.open {
  display: flex;
}

.mobile-link {
  font-size: 15px;
  font-weight: 500;
  color: #374151;
  text-decoration: none;
  padding: 11px 14px;
  border-radius: 8px;
  transition: background 0.15s;
  display: block;
}

.mobile-link:hover {
  background: #f1f5f9;
  color: #0a0a0a;
}

.mobile-divider {
  height: 1px;
  background: #e2e8f0;
  margin: 8px 0;
}

.btn-ghost-mobile {
  font-size: 15px;
  font-weight: 500;
  color: #374151;
  background: none;
  border: none;
  padding: 11px 14px;
  border-radius: 8px;
  cursor: pointer;
  text-align: left;
  width: 100%;
  transition: background 0.15s;
}

.btn-ghost-mobile:hover {
  background: #f1f5f9;
}

.mobile-cta {
  margin-top: 6px;
  justify-content: center;
  width: 100%;
  padding: 13px 18px;
  border-radius: 10px;
  font-size: 15px;
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
  background: var(--blue-pale);
  color: var(--blue);
  font-size: 13px;
  font-weight: 600;
  padding: 6px 14px;
  border-radius: 100px;
  border: 1px solid var(--blue-pale-border);
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
  color: #1d4ed8;
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
  background: #1d4ed8;
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
  background: #1e40af;
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
  border-color: #1d4ed8;
  color: #1d4ed8;
  background: #eff6ff;
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
  color: var(--blue);
  text-decoration-color: var(--blue);
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
  color: #1d4ed8;
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
  background: #eff6ff;
  border-top: 2px solid #bfdbfe;
  border-bottom: 2px solid #bfdbfe;
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
  color: var(--blue);
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: white;
  border: 2px solid var(--blue);
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

.benefit-icon {
  font-size: 20px;
  margin-bottom: 12px;
  color: var(--blue);
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
  background: #eff6ff;
  border-top: 2px solid #bfdbfe;
  border-bottom: 2px solid #bfdbfe;
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
  border: 2px solid #bfdbfe;
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
  color: #1d4ed8 !important;
}

.pricing > .section-inner > p {
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
  border: 2px solid #1d4ed8;
  background: #eff6ff;
}

.plan-tag {
  position: absolute;
  top: -13px;
  left: 50%;
  transform: translateX(-50%);
  background: #1d4ed8;
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
  border-bottom-color: #bfdbfe;
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
  background: #0f172a;
  color: #ffffff;
}

.final-cta h2 {
  font-family: 'Georgia', serif;
  color: #ffffff !important;
  margin-bottom: 16px;
}

.final-cta > .section-inner > p {
  font-size: 17px;
  color: #93c5fd;
  margin-bottom: 40px;
  font-weight: 500;
}

.final-cta .btn-primary {
  background: #ffffff;
  color: #0f172a;
}

.final-cta .btn-primary:hover {
  background: #eff6ff;
}

.final-note {
  margin-top: 20px;
  font-size: 13px;
  color: #60a5fa;
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
@media (max-width: 1200px) {
  .section-inner,
  .proof-inner,
  .footer-inner {
    padding: 0 40px;
  }

  .hero {
    padding: 120px 40px 80px;
  }

  .hero h1 {
    font-size: 3rem;
  }

  .benefits-grid {
    gap: 40px;
  }
}

@media (max-width: 900px) {
  .nav-links {
    display: none;
  }

  .nav-actions {
    display: none;
  }

  .hamburger {
    display: flex;
  }

  .hero {
    flex-direction: column;
    padding: 90px 20px 60px;
    text-align: center;
    gap: 40px;
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
    max-width: 440px;
    margin: 0 auto;
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

  .section-inner {
    padding: 0 20px;
  }
}

@media (max-width: 600px) {
  .nav-inner {
    padding: 10px 16px;
  }

  .logo-badge {
    display: none;
  }

  .benefit-cards {
    grid-template-columns: 1fr;
  }

  section h2 {
    font-size: 1.7rem;
  }

  .how,
  .benefits,
  .pricing {
    padding: 64px 0;
  }

  .final-cta {
    padding: 80px 0;
  }

  .hero h1 {
    font-size: 2.1rem;
  }

  .hero-sub {
    font-size: 15px;
  }

  .amount {
    font-size: 44px;
  }
}

/* ─── PARA FAMÍLIAS ───────────────────────────── */
.families {
  padding: 100px 0;
  background: #f8fafc;
  border-top: 1px solid #e2e8f0;
  border-bottom: 1px solid #e2e8f0;
}

.families h2 {
  font-family: 'Georgia', serif;
  font-size: clamp(1.8rem, 3vw, 2.6rem);
  line-height: 1.15;
  letter-spacing: -0.02em;
  color: #0a0a0a;
  font-weight: 700;
  margin-bottom: 20px;
}

.families h2 em {
  font-style: italic;
  color: #1d4ed8;
}

.families-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 64px;
  align-items: center;
}

.families-desc {
  font-size: 16px;
  color: #374151;
  line-height: 1.7;
  margin-bottom: 32px;
}

.families-features {
  display: flex;
  flex-direction: column;
  gap: 20px;
  margin-bottom: 36px;
}

.family-feature {
  display: flex;
  gap: 14px;
  align-items: flex-start;
}

.ff-icon {
  font-size: 22px;
  flex-shrink: 0;
  margin-top: 2px;
}

.family-feature strong {
  display: block;
  font-size: 14px;
  font-weight: 700;
  color: #0a0a0a;
  margin-bottom: 4px;
}

.family-feature p {
  font-size: 13px;
  color: #6b7280;
  line-height: 1.5;
}

.families-visual {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.family-card {
  background: #ffffff;
  border: 1px solid #e2e8f0;
  border-radius: 16px;
  padding: 20px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.04);
  transition: transform 0.2s, box-shadow 0.2s;
}

.family-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 24px rgba(0,0,0,0.08);
}

.fc-header {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 14px;
}

.fc-avatar {
  width: 38px;
  height: 38px;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 700;
  font-size: 16px;
  color: #1e3a8a;
  flex-shrink: 0;
}

.fc-header strong {
  display: block;
  font-size: 14px;
  color: #0a0a0a;
}

.fc-header span {
  font-size: 12px;
  color: #6b7280;
}

.fc-sachets {
  display: flex;
  gap: 8px;
}

.fc-sachet {
  padding: 6px 14px;
  border-radius: 8px;
  font-size: 12px;
  font-weight: 700;
  color: #0a0a0a;
  border: 1px solid rgba(0,0,0,0.08);
}

@media (max-width: 900px) {
  .families-grid {
    grid-template-columns: 1fr;
    gap: 40px;
  }
}
</style>