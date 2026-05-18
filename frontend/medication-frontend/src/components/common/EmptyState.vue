<template>
  <div class="empty-state">
    <div class="empty-icon" v-html="icon || defaultIcon"></div>
    <h3>{{ title }}</h3>
    <p>{{ message }}</p>
    <slot />
  </div>
</template>

<script setup>
defineProps({ title: String, message: String, icon: String })
const defaultIcon = '<svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"><path d="M21 16V8a2 2 0 00-1-1.73l-7-4a2 2 0 00-2 0l-7 4A2 2 0 003 8v8a2 2 0 001 1.73l7 4a2 2 0 002 0l7-4A2 2 0 0021 16z"/><polyline points="3.27 6.96 12 12.01 20.73 6.96"/><line x1="12" y1="22.08" x2="12" y2="12"/></svg>'
</script>

<style scoped>
.empty-state { text-align: center; padding: 48px 24px; }
.empty-icon { color: var(--text-muted); margin-bottom: 16px; display: inline-flex; }
.empty-state h3 { font-size: 16px; font-weight: 600; color: var(--text); margin-bottom: 6px; }
.empty-state p { font-size: 14px; color: var(--text-secondary); margin-bottom: 20px; }
</style>
