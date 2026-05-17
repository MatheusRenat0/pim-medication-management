import { reactive, toRefs } from 'vue'

const toasts = reactive({ items: [] })
let id = 0

export function useToast() {
  const show = (message, type = 'info', duration = 4000) => {
    const toast = { id: ++id, message, type, visible: true }
    toasts.items.push(toast)
    if (duration > 0) setTimeout(() => remove(toast.id), duration)
  }

  const success = (msg) => show(msg, 'success')
  const error = (msg) => show(msg, 'error')
  const warning = (msg) => show(msg, 'warning')
  const info = (msg) => show(msg, 'info')
  const remove = (toastId) => { toasts.items = toasts.items.filter(t => t.id !== toastId) }

  return { ...toRefs(toasts), show, success, error, warning, info, remove }
}
