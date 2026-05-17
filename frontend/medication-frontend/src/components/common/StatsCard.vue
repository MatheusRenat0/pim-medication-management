<template>
  <div class="stats-card" :class="[`stats-${variant}`]">
    <div class="stats-icon" v-html="icon"></div>
    <div class="stats-body">
      <span class="stats-label">{{ label }}</span>
      <span class="stats-value">{{ value }}</span>
    </div>
  </div>
</template>

<script setup>
defineProps({
  label: String,
  value: [String, Number],
  icon: String,
  variant: { type: String, default: 'primary' }
})
</script>

<style scoped>
.stats-card { display: flex; align-items: center; gap: 16px; padding: 20px 22px; background: #fff; border: 1px solid var(--border); border-radius: var(--radius-lg); box-shadow: var(--shadow-sm); transition: all var(--transition); }
.stats-card:hover { box-shadow: var(--shadow-md); transform: translateY(-2px); }
.stats-icon { width: 48px; height: 48px; border-radius: var(--radius-md); display: flex; align-items: center; justify-content: center; flex-shrink: 0; }
.stats-primary .stats-icon { background: var(--primary-light); color: var(--primary); }
.stats-success .stats-icon { background: var(--success-light); color: var(--success); }
.stats-warning .stats-icon { background: var(--warning-light); color: var(--warning); }
.stats-error .stats-icon { background: var(--error-light); color: var(--error); }
.stats-body { display: flex; flex-direction: column; }
.stats-label { font-size: 13px; color: var(--text-secondary); font-weight: 500; }
.stats-value { font-size: 28px; font-weight: 800; color: var(--text); letter-spacing: -0.03em; line-height: 1.2; }
</style>
