<template>
  <header class="main-navbar">
    <!-- Top System Banner with Role Quick-Switcher -->
    <div class="top-system-strip">
      <div class="content-container strip-flex">
        <div class="strip-left">
          <span class="system-badge">MEHMONXONA BRON QILISH TIZIMI — ZunoBookingSystem</span>
        </div>

        <div class="strip-right">
          <!-- Role Quick Switcher for testing/demo -->
          <div class="quick-role-switcher">
            <span class="switcher-lbl">Demo Kirish (Rolni tanlang):</span>
            <button
              class="quick-pill"
              :class="{ active: state.isAuthenticated && state.currentUser?.role === 'USER' }"
              @click="handleQuickLogin('USER')"
            >
              👤 Mijoz (Mehmon)
            </button>
            <button
              class="quick-pill"
              :class="{ active: state.isAuthenticated && state.currentUser?.role === 'HOTEL' }"
              @click="handleQuickLogin('HOTEL')"
            >
              🏨 Hotel Egasi
            </button>
            <button
              class="quick-pill"
              :class="{ active: state.isAuthenticated && state.currentUser?.role === 'ADMIN' }"
              @click="handleQuickLogin('ADMIN')"
            >
              ⚡ Admin
            </button>
            <button
              v-if="state.isAuthenticated"
              class="quick-pill logout-pill"
              @click="handleLogout"
            >
              🚪 Chiqish
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- Main Navigation Bar -->
    <div class="content-container navbar-main-row">
      <!-- 1. Brand Logo -->
      <router-link to="/" class="brand-link">
        <div class="brand-logo-icon">
          <svg viewBox="0 0 24 24" width="22" height="22" fill="currentColor">
            <path d="M19 2H9c-1.1 0-2 .9-2 2v3H5c-1.1 0-2 .9-2 2v11c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V4c0-1.1-.9-2-2-2zM9 4h10v16H9V4zM5 9h2v11H5V9zm6 2h2v2h-2v-2zm0 4h2v2h-2v-2zm4-4h2v2h-2v-2zm0 4h2v2h-2v-2z"/>
          </svg>
        </div>
        <div class="brand-title-wrap">
          <span class="brand-name">ZunoBooking</span>
          <span class="brand-desc">Mehmonxona tizimi</span>
        </div>
      </router-link>

      <!-- 2. Dynamic Navigation Links Based On Authentication and Role -->
      <nav class="nav-links-menu">
        <!-- Common Public Links -->
        <router-link to="/" class="nav-item" active-class="active" exact>
          🏠 01. Bosh sahifa
        </router-link>
        <router-link to="/hotel/1" class="nav-item" active-class="active">
          🏨 02. Mehmonxona detali
        </router-link>
        <router-link to="/rooms" class="nav-item" active-class="active">
          🛏️ 03. Xonalarni tanlash
        </router-link>
        <router-link to="/booking" class="nav-item" active-class="active">
          📅 04. Bron qilish
        </router-link>

        <!-- DYNAMIC LINKS: IF LOGGED IN AS USER (MEHMON) -->
        <template v-if="state.isAuthenticated && (state.currentUser?.role === 'USER' || state.currentUser?.role === 'ADMIN')">
          <router-link to="/my-bookings" class="nav-item role-nav-highlight" active-class="active">
            📋 05. Bronlarim
            <span class="count-bubble">{{ state.bookings.length }}</span>
          </router-link>
          <router-link to="/favorites" class="nav-item role-nav-highlight" active-class="active">
            ❤️ 15. Sevimlilar
          </router-link>
          <router-link to="/messages" class="nav-item role-nav-highlight" active-class="active">
            💬 16. Xabarlar
          </router-link>
        </template>

        <!-- DYNAMIC LINKS: IF LOGGED IN AS HOTEL (HOTEL EGASI) -->
        <template v-if="state.isAuthenticated && (state.currentUser?.role === 'HOTEL' || state.currentUser?.role === 'ADMIN')">
          <router-link to="/partner/calendar" class="nav-item partner-nav-highlight" active-class="active">
            📆 06. Xonalar Taqvimi
          </router-link>
          <router-link to="/partner/bookings" class="nav-item partner-nav-highlight" active-class="active">
            📑 07. Bronlar Boshqaruvi
          </router-link>
          <router-link to="/partner/reviews" class="nav-item partner-nav-highlight" active-class="active">
            ⭐ 08. Sharhlar
          </router-link>
          <router-link to="/partner/stats" class="nav-item partner-nav-highlight" active-class="active">
            📊 09. Statistika Egasi
          </router-link>
          <router-link to="/partner/rooms" class="nav-item partner-nav-highlight" active-class="active">
            🛏️ 11. Xonalar Jadvali
          </router-link>
        </template>

        <!-- DYNAMIC LINKS: IF LOGGED IN AS ADMIN (SUPER ADMIN) -->
        <template v-if="state.isAuthenticated && state.currentUser?.role === 'ADMIN'">
          <router-link to="/admin/dashboard" class="nav-item admin-nav-highlight" active-class="active">
            🛡️ 10. Admin Dashboard
          </router-link>
          <router-link to="/admin/users" class="nav-item admin-nav-highlight" active-class="active">
            👥 12. Foydalanuvchilar
          </router-link>
        </template>

        <!-- Payment Link -->
        <router-link to="/payment" class="nav-item" active-class="active">
          💳 14. To'lov
        </router-link>
      </nav>

      <!-- 3. Right: Theme, Language, Profile / Login -->
      <div class="nav-actions-right">
        <!-- Theme Switch -->
        <div class="theme-switch-box" title="Qorong'i / Yorug' rejim">
          <label class="switch">
            <input type="checkbox" :checked="state.darkMode" @change="toggleDarkMode" />
            <span class="slider"></span>
          </label>
        </div>

        <!-- Language Selector -->
        <select v-model="state.language" class="lang-select">
          <option value="UZ">UZ</option>
          <option value="RU">RU</option>
          <option value="EN">EN</option>
        </select>

        <!-- If Authenticated: Show Profile Pill -->
        <div v-if="state.isAuthenticated" class="user-pill-profile">
          <div class="avatar-badge">{{ state.currentUser.avatar }}</div>
          <div class="user-info">
            <span class="u-name">{{ state.currentUser.name }}</span>
            <span class="u-role-tag" :class="state.currentUser.role.toLowerCase()">
              {{ state.currentUser.role }}
            </span>
          </div>
          <button class="logout-icon-btn" @click="handleLogout" title="Chiqish">
            ✕
          </button>
        </div>

        <!-- If Not Authenticated: Show Login Button -->
        <div v-else class="auth-buttons-group">
          <router-link to="/login" class="btn-primary login-nav-btn">
            🔐 13. Kirish
          </router-link>
        </div>
      </div>
    </div>
  </header>
</template>

<script setup>
import { useRouter } from 'vue-router'
import { state, toggleDarkMode, login, logout } from '../../store'

const router = useRouter()

function handleQuickLogin(role) {
  login(role)
  if (role === 'ADMIN') router.push('/admin/dashboard')
  else if (role === 'HOTEL') router.push('/partner/stats')
  else router.push('/my-bookings')
}

function handleLogout() {
  logout()
  router.push('/')
}
</script>

<style scoped>
.main-navbar {
  background: var(--header-bg);
  border-bottom: 1px solid var(--border-subtle);
  position: sticky;
  top: 0;
  z-index: 100;
  box-shadow: var(--shadow-sm);
  margin-bottom: 24px;
}

/* Top Strip */
.top-system-strip {
  background: #0f172a;
  color: #94a3b8;
  padding: 6px 0;
  font-size: 11px;
  border-bottom: 1px solid #1e293b;
}

.strip-flex {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.system-badge {
  font-weight: 700;
  color: #38bdf8;
  letter-spacing: 0.3px;
}

.quick-role-switcher {
  display: flex;
  align-items: center;
  gap: 6px;
}

.switcher-lbl {
  color: #64748b;
  font-size: 10px;
  text-transform: uppercase;
  font-weight: 700;
}

.quick-pill {
  background: #1e293b;
  border: 1px solid #334155;
  color: #cbd5e1;
  font-size: 10.5px;
  font-weight: 700;
  padding: 2px 8px;
  border-radius: var(--radius-full);
  cursor: pointer;
  transition: all 0.15s;
}

.quick-pill:hover {
  background: #334155;
  color: #fff;
}

.quick-pill.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.logout-pill {
  background: rgba(239, 68, 68, 0.2);
  color: #ef4444;
  border-color: rgba(239, 68, 68, 0.4);
}

/* Main Navbar Row */
.navbar-main-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding-top: 10px;
  padding-bottom: 10px;
  gap: 16px;
}

.brand-link {
  display: flex;
  align-items: center;
  gap: 10px;
  text-decoration: none;
}

.brand-logo-icon {
  width: 38px;
  height: 38px;
  background: var(--primary-light);
  color: var(--primary);
  border-radius: var(--radius-sm);
  display: flex;
  align-items: center;
  justify-content: center;
}

.brand-title-wrap {
  display: flex;
  flex-direction: column;
}

.brand-name {
  font-size: 16px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1.1;
}

.brand-desc {
  font-size: 10px;
  color: var(--primary);
  font-weight: 700;
}

/* Navigation Links */
.nav-links-menu {
  display: flex;
  align-items: center;
  gap: 4px;
  flex-wrap: wrap;
}

.nav-item {
  padding: 6px 10px;
  border-radius: var(--radius-sm);
  font-size: 11.5px;
  font-weight: 600;
  color: var(--text-secondary);
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  gap: 4px;
  transition: all 0.15s;
}

.nav-item:hover {
  background: var(--bg-subtle);
  color: var(--text-main);
}

.nav-item.active {
  background: var(--primary-light);
  color: var(--primary);
  font-weight: 700;
}

.role-nav-highlight {
  color: #0284c7;
}

.partner-nav-highlight {
  color: #d97706;
}

.admin-nav-highlight {
  color: #dc2626;
}

.count-bubble {
  background: var(--primary);
  color: #fff;
  font-size: 9px;
  padding: 1px 5px;
  border-radius: 10px;
}

/* Right Actions */
.nav-actions-right {
  display: flex;
  align-items: center;
  gap: 12px;
}

.lang-select {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-main);
  padding: 5px 8px;
  border-radius: var(--radius-sm);
  font-size: 11px;
  font-weight: 700;
  cursor: pointer;
}

.user-pill-profile {
  display: flex;
  align-items: center;
  gap: 8px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 3px 8px;
  border-radius: var(--radius-full);
}

.avatar-badge {
  width: 26px;
  height: 26px;
  border-radius: 50%;
  background: var(--primary);
  color: #fff;
  font-size: 10px;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
}

.user-info {
  display: flex;
  flex-direction: column;
}

.u-name {
  font-size: 11px;
  font-weight: 700;
  color: var(--text-main);
  line-height: 1;
}

.u-role-tag {
  font-size: 9px;
  font-weight: 700;
}

.u-role-tag.admin { color: #ef4444; }
.u-role-tag.hotel { color: #f59e0b; }
.u-role-tag.user { color: #2563eb; }

.logout-icon-btn {
  background: transparent;
  border: none;
  color: var(--text-muted);
  cursor: pointer;
  font-size: 11px;
  padding: 2px;
}

.logout-icon-btn:hover {
  color: var(--danger);
}

.login-nav-btn {
  padding: 6px 14px;
  font-size: 12px;
}

@media (max-width: 1200px) {
  .nav-links-menu {
    display: none;
  }
}
</style>
