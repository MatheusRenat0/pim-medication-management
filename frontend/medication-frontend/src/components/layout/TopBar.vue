<template>
  <header class="topbar">
    <div class="topbar-left">
      <button class="menu-toggle" @click="$emit('toggle-sidebar')">
        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><line x1="3" y1="6" x2="21" y2="6"/><line x1="3" y1="12" x2="21" y2="12"/><line x1="3" y1="18" x2="21" y2="18"/></svg>
      </button>
      <h2 class="topbar-title">{{ pageTitle }}</h2>
    </div>
    <div class="topbar-right">
      <div class="user-info">
        <div class="user-avatar">{{ initials }}</div>
        <div class="user-details">
          <span class="user-name">{{ auth.userName }}</span>
          <span class="user-role">{{ auth.userType }}</span>
        </div>
      </div>
    </div>
  </header>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useAuthStore } from '../../stores/auth'

defineEmits(['toggle-sidebar'])
const route = useRoute()
const auth = useAuthStore()

const pageTitle = computed(() => route.meta?.title || 'Dashboard')
const initials = computed(() => {
  const name = auth.userName || ''
  return name.split(' ').map(w => w[0]).join('').slice(0,2).toUpperCase() || 'U'
})
</script>

<style scoped>
.topbar { display: flex; align-items: center; justify-content: space-between; padding: 16px 32px; background: #fff; border-bottom: 1px solid var(--border); position: sticky; top: 0; z-index: 100; }
.topbar-left { display: flex; align-items: center; gap: 16px; }
.menu-toggle { display: none; background: none; border: none; color: var(--text-secondary); cursor: pointer; padding: 6px; border-radius: var(--radius-sm); }
.menu-toggle:hover { background: var(--search-bg); }
.topbar-title { font-size: 18px; font-weight: 700; color: var(--text); letter-spacing: -0.02em; }
.topbar-right { display: flex; align-items: center; gap: 16px; }
.user-info { display: flex; align-items: center; gap: 10px; }
.user-avatar { width: 36px; height: 36px; border-radius: 50%; background: var(--primary-light); color: var(--primary); font-size: 13px; font-weight: 700; display: flex; align-items: center; justify-content: center; flex-shrink: 0; }
.user-details { display: flex; flex-direction: column; }
.user-name { font-size: 13px; font-weight: 600; color: var(--text); }
.user-role { font-size: 11px; color: var(--text-secondary); }

@media (max-width: 768px) {
  .menu-toggle { display: flex; }
  .topbar { padding: 12px 16px; }
  .user-details { display: none; }
}
</style>
