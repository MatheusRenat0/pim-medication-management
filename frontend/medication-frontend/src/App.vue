<template>
  <component :is="layout">
    <router-view />
  </component>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import AppLayout from './components/layout/AppLayout.vue'

const route = useRoute()
const layout = computed(() => route.meta.public ? 'div' : AppLayout)
</script>

<style>
@import './assets/styles/global.css';
</style>