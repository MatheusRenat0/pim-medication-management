<template>
  <Teleport to="body">
    <Transition name="modal">
      <div v-if="modelValue" class="modal-overlay" @click.self="closable && $emit('update:modelValue', false)">
        <div class="modal-box" :class="[`modal-${size}`]">
          <div class="modal-header" v-if="title">
            <h3>{{ title }}</h3>
            <button v-if="closable" class="modal-close" @click="$emit('update:modelValue', false)">
              <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body"><slot /></div>
          <div class="modal-footer" v-if="$slots.footer"><slot name="footer" /></div>
        </div>
      </div>
    </Transition>
  </Teleport>
</template>

<script setup>
defineProps({
  modelValue: Boolean,
  title: { type: String, default: '' },
  size: { type: String, default: 'md' },
  closable: { type: Boolean, default: true }
})
defineEmits(['update:modelValue'])
</script>

<style scoped>
.modal-overlay { position: fixed; inset: 0; background: rgba(15,23,42,0.5); backdrop-filter: blur(4px); display: flex; align-items: center; justify-content: center; z-index: 1000; padding: 24px; }
.modal-box { background: #fff; border-radius: var(--radius-xl); box-shadow: var(--shadow-lg); width: 100%; overflow: hidden; animation: modalIn 0.2s ease; }
.modal-sm { max-width: 400px; }
.modal-md { max-width: 560px; }
.modal-lg { max-width: 720px; }
.modal-xl { max-width: 900px; }
.modal-header { display: flex; align-items: center; justify-content: space-between; padding: 20px 24px; border-bottom: 1px solid var(--border); }
.modal-header h3 { font-size: 17px; font-weight: 700; color: var(--text); }
.modal-close { background: none; border: none; color: var(--text-muted); cursor: pointer; padding: 4px; border-radius: 6px; display: flex; transition: all var(--transition); }
.modal-close:hover { background: var(--search-bg); color: var(--text); }
.modal-body { padding: 24px; max-height: 70vh; overflow-y: auto; }
.modal-footer { padding: 16px 24px; border-top: 1px solid var(--border); display: flex; justify-content: flex-end; gap: 10px; }

@keyframes modalIn { from { opacity: 0; transform: scale(0.95) translateY(8px); } to { opacity: 1; transform: scale(1) translateY(0); } }
.modal-enter-active { transition: opacity 0.2s ease; }
.modal-leave-active { transition: opacity 0.15s ease; }
.modal-enter-from, .modal-leave-to { opacity: 0; }
</style>
