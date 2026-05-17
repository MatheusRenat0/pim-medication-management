<template>
  <div class="app-layout" :class="{ 'sidebar-collapsed': collapsed }">
    <Sidebar :collapsed="collapsed" @toggle="collapsed = !collapsed" />
    <div class="main-area">
      <TopBar @toggle-sidebar="collapsed = !collapsed" />
      <main class="main-content">
        <slot />
      </main>
    </div>
    <Toast />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import Sidebar from './Sidebar.vue'
import TopBar from './TopBar.vue'
import Toast from '../common/Toast.vue'

const collapsed = ref(false)
</script>

<style scoped>
.app-layout { display: flex; min-height: 100vh; background: var(--bg); }
.main-area { flex: 1; display: flex; flex-direction: column; min-width: 0; margin-left: 260px; transition: margin-left 0.3s ease; }
.sidebar-collapsed .main-area { margin-left: 72px; }
.main-content { flex: 1; padding: 28px 32px; overflow-y: auto; }

@media (max-width: 768px) {
  .main-area { margin-left: 0 !important; }
  .main-content { padding: 20px 16px; }
}
</style>
