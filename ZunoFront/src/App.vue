<template>
  <div class="app-wrapper" :class="{ 'dark-theme': state.darkMode }">
    <!-- 1. Global Navigation Bar -->
    <AppNavbar />

    <!-- 2. Dynamic Router View (Full-page views with real routing) -->
    <main class="app-main-content">
      <router-view />
    </main>

    <!-- 3. Global Master Footer -->
    <FooterSection />

    <!-- 4. Global Modals -->
    <AddBookingModal />
    <AddRoomModal />
    <SupportModal />
  </div>
</template>

<script setup>
import { state } from './store'
import AppNavbar from './components/layout/AppNavbar.vue'
import FooterSection from './components/FooterSection.vue'

// Global Modals
import AddBookingModal from './components/modals/AddBookingModal.vue'
import AddRoomModal from './components/modals/AddRoomModal.vue'
import SupportModal from './components/modals/SupportModal.vue'
</script>

<style scoped>
.app-main-content {
  flex: 1;
  min-height: calc(100vh - 160px);
}
</style>
