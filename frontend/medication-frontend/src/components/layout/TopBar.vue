<template>
  <header class="topbar">
    <div class="topbar-left">
      <button class="menu-toggle" @click="$emit('toggle-sidebar')">
        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><line x1="3" y1="6" x2="21" y2="6"/><line x1="3" y1="12" x2="21" y2="12"/><line x1="3" y1="18" x2="21" y2="18"/></svg>
      </button>
      <h2 class="topbar-title">{{ pageTitle }}</h2>
    </div>
    <div class="topbar-right">
      <div class="user-info" @click="showProfile = true" style="cursor:pointer" title="Ver perfil">
        <div class="user-avatar">{{ initials }}</div>
        <div class="user-details">
          <span class="user-name">{{ auth.userName }}</span>
          <span class="user-role">{{ auth.userType }}</span>
        </div>
        <svg class="chevron-icon" width="14" height="14" viewBox="0 0 14 14" fill="none"><path d="M4 5.5l3 3 3-3" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/></svg>
      </div>
    </div>

    <!-- Modal de Perfil -->
    <Transition name="fade">
      <div v-if="showProfile" class="profile-overlay" @click.self="showProfile = false">
        <div class="profile-modal">
          <div class="profile-header">
            <h3>Meu Perfil</h3>
            <button class="profile-close" @click="showProfile = false">
              <svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M3 3l10 10M13 3L3 13" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>
            </button>
          </div>
          <div class="profile-avatar-section">
            <div class="profile-avatar-lg">{{ initials }}</div>
            <div>
              <p class="profile-name">{{ profileForm.nome || auth.userName }}</p>
              <span class="profile-role-badge">{{ auth.userType }}</span>
            </div>
          </div>
          <form @submit.prevent="salvarPerfil" class="profile-form">
            <div class="profile-field"><label>Nome</label><input v-model="profileForm.nome" placeholder="Seu nome"></div>
            <div class="profile-field"><label>E-mail</label><input v-model="profileForm.email" type="email" placeholder="seu@email.com"></div>
            <div class="profile-field"><label>CPF</label><input v-model="profileForm.cpf" placeholder="000.000.000-00" maxlength="14"></div>
            <div class="profile-field"><label>Nova Senha <span class="optional">(opcional)</span></label><input v-model="profileForm.senhaNova" type="password" placeholder="Deixe vazio para manter"></div>
            <p v-if="profileMsg" class="profile-msg" :class="{ error: profileError }">{{ profileMsg }}</p>
            <button type="submit" class="profile-save-btn" :disabled="savingProfile">
              {{ savingProfile ? 'Salvando...' : 'Salvar alterações' }}
            </button>
          </form>
        </div>
      </div>
    </Transition>
  </header>
</template>

<script setup>
import { ref, computed, watch } from 'vue'
import { useRoute } from 'vue-router'
import { useAuthStore } from '../../stores/auth'
import { useApi } from '../../composables/useApi'

defineEmits(['toggle-sidebar'])
const route = useRoute()
const auth = useAuthStore()
const { put } = useApi()

const pageTitle = computed(() => route.meta?.title || 'Dashboard')
const initials = computed(() => {
  const name = auth.userName || ''
  return name.split(' ').map(w => w[0]).join('').slice(0,2).toUpperCase() || 'U'
})

// Profile modal
const showProfile = ref(false)
const savingProfile = ref(false)
const profileMsg = ref('')
const profileError = ref(false)
const profileForm = ref({ nome: '', email: '', cpf: '', senhaNova: '' })

watch(showProfile, (v) => {
  if (v) {
    profileForm.value = { nome: auth.userName || '', email: auth.user?.email || '', cpf: auth.user?.cpf || '', senhaNova: '' }
    profileMsg.value = ''
    profileError.value = false
  }
})

const salvarPerfil = async () => {
  savingProfile.value = true
  profileMsg.value = ''
  profileError.value = false
  try {
    const data = await put('/Usuario/perfil', profileForm.value)
    // Atualiza o store local com o nome atualizado
    if (data.nome) {
      auth.user.nome = data.nome
      auth.user.email = data.email
      auth.user.cpf = data.cpf
      localStorage.setItem('medflow_user', JSON.stringify(auth.user))
    }
    profileMsg.value = 'Perfil atualizado com sucesso!'
  } catch (e) {
    profileMsg.value = e.message || 'Erro ao atualizar perfil.'
    profileError.value = true
  } finally {
    savingProfile.value = false
  }
}
</script>

<style scoped>
.topbar { display: flex; align-items: center; justify-content: space-between; padding: 16px 32px; background: #fff; border-bottom: 1px solid var(--border); position: sticky; top: 0; z-index: 100; }
.topbar-left { display: flex; align-items: center; gap: 16px; }
.menu-toggle { background: none; border: none; color: var(--text-secondary); cursor: pointer; padding: 6px; border-radius: var(--radius-sm); display: flex; }
.menu-toggle:hover { background: var(--search-bg); }
.topbar-title { font-size: 18px; font-weight: 700; color: var(--text); letter-spacing: -0.02em; }
.topbar-right { display: flex; align-items: center; gap: 16px; }
.user-info { display: flex; align-items: center; gap: 10px; padding: 6px 10px; border-radius: 10px; transition: background 0.15s; }
.user-info:hover { background: var(--search-bg); }
.user-avatar { width: 36px; height: 36px; border-radius: 50%; background: var(--primary-light); color: var(--primary); font-size: 13px; font-weight: 700; display: flex; align-items: center; justify-content: center; flex-shrink: 0; }
.user-details { display: flex; flex-direction: column; }
.user-name { font-size: 13px; font-weight: 600; color: var(--text); }
.user-role { font-size: 11px; color: var(--text-secondary); }
.chevron-icon { color: var(--text-secondary); flex-shrink: 0; }

/* Profile Modal */
.profile-overlay { position: fixed; inset: 0; background: rgba(0,0,0,0.4); z-index: 500; display: flex; align-items: center; justify-content: center; backdrop-filter: blur(4px); }
.profile-modal { background: #fff; border-radius: 16px; width: 100%; max-width: 420px; padding: 28px; box-shadow: 0 20px 60px rgba(0,0,0,0.2); animation: slideUp 0.25s ease; }
@keyframes slideUp { from { opacity: 0; transform: translateY(16px); } to { opacity: 1; transform: translateY(0); } }
.profile-header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 24px; }
.profile-header h3 { font-size: 18px; font-weight: 700; color: var(--text); font-family: Georgia, serif; }
.profile-close { background: none; border: none; color: var(--text-secondary); cursor: pointer; padding: 4px; border-radius: 6px; display: flex; }
.profile-close:hover { background: var(--search-bg); }
.profile-avatar-section { display: flex; align-items: center; gap: 16px; margin-bottom: 24px; padding-bottom: 20px; border-bottom: 1px solid var(--border); }
.profile-avatar-lg { width: 52px; height: 52px; border-radius: 50%; background: var(--primary-light); color: var(--primary); font-size: 18px; font-weight: 700; display: flex; align-items: center; justify-content: center; flex-shrink: 0; }
.profile-name { font-size: 16px; font-weight: 600; color: var(--text); }
.profile-role-badge { font-size: 11px; font-weight: 600; background: var(--primary-light); color: var(--primary); padding: 2px 10px; border-radius: 100px; }
.profile-form { display: flex; flex-direction: column; gap: 14px; }
.profile-field { display: flex; flex-direction: column; gap: 5px; }
.profile-field label { font-size: 13px; font-weight: 600; color: #374151; }
.profile-field .optional { font-weight: 400; color: #9ca3af; }
.profile-field input { padding: 10px 14px; border-radius: 10px; border: 1.5px solid var(--border); font-size: 14px; color: var(--text); background: #fff; outline: none; transition: border-color 0.15s; font-family: inherit; }
.profile-field input:focus { border-color: var(--primary); }
.profile-msg { font-size: 13px; color: #16a34a; font-weight: 500; }
.profile-msg.error { color: #dc2626; }
.profile-save-btn { width: 100%; background: var(--primary); color: #fff; border: none; padding: 12px 24px; border-radius: 100px; font-size: 14px; font-weight: 600; cursor: pointer; transition: background 0.2s, transform 0.15s; font-family: inherit; margin-top: 4px; }
.profile-save-btn:hover:not(:disabled) { background: #1e40af; transform: translateY(-1px); }
.profile-save-btn:disabled { opacity: 0.7; cursor: not-allowed; }

.fade-enter-active, .fade-leave-active { transition: opacity 0.2s ease; }
.fade-enter-from, .fade-leave-to { opacity: 0; }

@media (max-width: 768px) {
  .topbar { padding: 12px 16px; }
  .user-details { display: none; }
  .chevron-icon { display: none; }
}
</style>
