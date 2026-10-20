<template>
  <header class="master-header">
    <!-- Left Section: Logo & Global Search -->
    <div class="header-left">
      <div class="brand-logo" @click="handleLogoClick" title="Bosh sahifaga qaytish">
        <div class="brand-icon-box">
          <svg viewBox="0 0 24 24" width="22" height="22" fill="currentColor">
            <path d="M19 2H9c-1.1 0-2 .9-2 2v3H5c-1.1 0-2 .9-2 2v11c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V4c0-1.1-.9-2-2-2zM9 4h10v16H9V4zM5 9h2v11H5V9zm6 2h2v2h-2v-2zm0 4h2v2h-2v-2zm4-4h2v2h-2v-2zm0 4h2v2h-2v-2z"/>
          </svg>
        </div>
      </div>

      <div class="header-search-wrap">
        <svg class="search-icon" viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2">
          <circle cx="11" cy="11" r="8"></circle>
          <line x1="21" y1="21" x2="16.65" y2="16.65"></line>
        </svg>
        <input
          type="text"
          v-model="state.searchGlobal"
          placeholder="Qidirish... mehmonxona, shahar yoki hudud"
          class="header-search-input"
        />
        <!-- Dark / Light theme switch -->
        <label class="theme-switch" title="Qorong'i / Yorug' rejim">
          <input type="checkbox" :checked="state.darkMode" @change="toggleDarkMode">
          <span class="theme-slider"></span>
        </label>
      </div>
    </div>

    <!-- Center Section: Title & Highlights -->
    <div class="header-center">
      <h1 class="system-main-title">MEHMONXONA BRON QILISH TIZIMI</h1>
      <div class="system-sub-row">
        <span class="system-brand-name">ZunoBookingSystem</span>
        <div class="system-badges">
          <button
            class="header-role-btn"
            :class="{ active: state.currentRole === 'USER' }"
            @click="setRole('USER')"
            title="Mijoz / Mehmon ko'rinishiga o'tish"
          >
            👤 USER
          </button>
          <button
            class="header-role-btn"
            :class="{ active: state.currentRole === 'HOTEL' }"
            @click="setRole('HOTEL')"
            title="Hotel Egasi ko'rinishiga o'tish"
          >
            🏨 HOTEL EGASI
          </button>
          <button
            class="header-role-btn"
            :class="{ active: state.currentRole === 'ADMIN' }"
            @click="setRole('ADMIN')"
            title="Admin ko'rinishiga o'tish"
          >
            ⚡ ADMIN
          </button>
          <span class="pill-badge pill-blue">🗂️ 16 EKRAN</span>
          <span class="pill-badge pill-orange">✨ TO'LIQ VERSIYA</span>
        </div>
      </div>
    </div>

    <!-- Right Section: Feature badges, Notifications, Lang & Profile -->
    <div class="header-right">
      <div class="feature-pills-list">
        <span class="feature-pill">
          <span class="pill-icon">🎨</span> Zamonaviy dizayn
        </span>
        <span class="feature-pill">
          <span class="pill-icon">⚡</span> To'liq funksiya
        </span>
        <span class="feature-pill">
          <span class="pill-icon">🎛️</span> Oson boshqaruv
        </span>
        <span class="feature-pill">
          <span class="pill-icon">🔒</span> Xavfsiz va ishonchli
        </span>
      </div>

      <div class="header-actions">
        <!-- Notifications Bell -->
        <div class="icon-action-btn" @click="showNotif = !showNotif" title="Bildirishnomalar">
          <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M18 8A6 6 0 0 0 6 8c0 7-3 9-3 9h18s-3-2-3-9"></path>
            <path d="M13.73 21a2 2 0 0 1-3.46 0"></path>
          </svg>
          <span v-if="state.unreadNotificationsCount > 0" class="notif-badge">
            {{ state.unreadNotificationsCount }}
          </span>

          <!-- Dropdown -->
          <div v-if="showNotif" class="notif-dropdown" @click.stop>
            <div class="notif-dropdown-header">
              <span>Bildirishnomalar</span>
              <span class="notif-clear-btn" @click="clearNotifications">Tozalash</span>
            </div>
            <div class="notif-list">
              <div v-for="n in state.notifications" :key="n.id" class="notif-item">
                <div class="notif-title">{{ n.title }}</div>
                <div class="notif-text">{{ n.text }}</div>
                <div class="notif-time">{{ n.time }}</div>
              </div>
            </div>
          </div>
        </div>

        <!-- Language Selector -->
        <div class="lang-selector">
          <select v-model="state.language" class="lang-select">
            <option value="UZ">UZ</option>
            <option value="RU">RU</option>
            <option value="EN">EN</option>
          </select>
        </div>

        <!-- Profile User Pill with Role Switcher -->
        <div class="user-profile-pill" @click="showRoleDropdown = !showRoleDropdown">
          <div class="user-avatar-circle">
            <img
              src="https://images.unsplash.com/photo-1534528741775-53994a69daeb?auto=format&fit=crop&w=100&q=80"
              alt="Avatar"
              class="user-avatar-img"
            />
          </div>
          <div class="user-meta">
            <span class="user-name">{{ state.currentUser.name }}</span>
            <span class="user-role-text">{{ state.currentUser.role }}</span>
          </div>
          <span class="caret-down">▾</span>

          <!-- Role Switcher Dropdown -->
          <div v-if="showRoleDropdown" class="profile-dropdown" @click.stop>
            <div class="dropdown-label">Rolni almashtirish:</div>
            <div
              class="dropdown-item"
              :class="{ selected: state.currentRole === 'ADMIN' }"
              @click="setRole('ADMIN'); showRoleDropdown = false"
            >
              🛡️ Admin (Boshqaruv)
            </div>
            <div
              class="dropdown-item"
              :class="{ selected: state.currentRole === 'HOTEL' }"
              @click="setRole('HOTEL'); showRoleDropdown = false"
            >
              🏨 Hotel Egasi (Partner)
            </div>
            <div
              class="dropdown-item"
              :class="{ selected: state.currentRole === 'USER' }"
              @click="setRole('USER'); showRoleDropdown = false"
            >
              👤 Mijoz (Mehmon)
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script setup>
import { ref } from 'vue'
import { state, toggleDarkMode, setRole, setActiveView } from '../store'

const showNotif = ref(false)
const showRoleDropdown = ref(false)

function handleLogoClick() {
  setActiveView('ALL')
}

function clearNotifications() {
  state.unreadNotificationsCount = 0
  state.notifications = []
  showNotif.value = false
}
</script>

<style scoped>
.master-header {
  background: var(--header-bg);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-md);
  margin: 12px 16px;
  padding: 10px 18px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 16px;
  box-shadow: var(--shadow-sm);
  position: relative;
  z-index: 50;
}

/* Left */
.header-left {
  display: flex;
  align-items: center;
  gap: 12px;
}

.brand-icon-box {
  background: var(--primary-light);
  color: var(--primary);
  width: 38px;
  height: 38px;
  border-radius: var(--radius-sm);
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: transform 0.15s;
}

.brand-icon-box:hover {
  transform: scale(1.05);
}

.header-search-wrap {
  display: flex;
  align-items: center;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-full);
  padding: 5px 12px;
  gap: 8px;
  width: 280px;
}

.search-icon {
  color: var(--text-muted);
}

.header-search-input {
  background: transparent;
  border: none;
  outline: none;
  font-size: 11px;
  color: var(--text-main);
  width: 100%;
}

.theme-switch {
  position: relative;
  display: inline-block;
  width: 28px;
  height: 16px;
  flex-shrink: 0;
}

.theme-switch input {
  opacity: 0;
  width: 0;
  height: 0;
}

.theme-slider {
  position: absolute;
  cursor: pointer;
  top: 0; left: 0; right: 0; bottom: 0;
  background-color: #94a3b8;
  transition: .3s;
  border-radius: 20px;
}

.theme-slider:before {
  position: absolute;
  content: "";
  height: 12px;
  width: 12px;
  left: 2px;
  bottom: 2px;
  background-color: white;
  transition: .3s;
  border-radius: 50%;
}

input:checked + .theme-slider {
  background-color: var(--primary);
}

input:checked + .theme-slider:before {
  transform: translateX(12px);
}

/* Center */
.header-center {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.system-main-title {
  font-size: 14px;
  font-weight: 800;
  letter-spacing: 0.5px;
  color: var(--text-main);
  text-transform: uppercase;
}

.system-sub-row {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-top: 2px;
}

.system-brand-name {
  font-size: 11.5px;
  font-weight: 700;
  color: var(--primary);
}

.system-badges {
  display: flex;
  align-items: center;
  gap: 6px;
}

.header-role-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-secondary);
  font-size: 10px;
  font-weight: 700;
  padding: 2px 8px;
  border-radius: 12px;
  cursor: pointer;
  transition: all 0.15s;
}

.header-role-btn.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.pill-badge {
  font-size: 9.5px;
  font-weight: 700;
  padding: 2px 7px;
  border-radius: 10px;
}

.pill-blue {
  background: #eff6ff;
  color: #2563eb;
  border: 1px solid #bfdbfe;
}

.pill-orange {
  background: #fff7ed;
  color: #ea580c;
  border: 1px solid #fed7aa;
}

/* Right */
.header-right {
  display: flex;
  align-items: center;
  gap: 16px;
}

.feature-pills-list {
  display: flex;
  align-items: center;
  gap: 8px;
}

.feature-pill {
  display: flex;
  align-items: center;
  gap: 4px;
  font-size: 10px;
  color: var(--text-secondary);
  font-weight: 500;
  background: var(--bg-subtle);
  padding: 3px 8px;
  border-radius: var(--radius-sm);
  border: 1px solid var(--border-subtle);
}

.header-actions {
  display: flex;
  align-items: center;
  gap: 10px;
}

.icon-action-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-main);
  width: 32px;
  height: 32px;
  border-radius: var(--radius-sm);
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  position: relative;
  transition: background-color 0.15s;
}

.icon-action-btn:hover {
  background: var(--border-subtle);
}

.notif-badge {
  position: absolute;
  top: -4px;
  right: -4px;
  background: var(--danger);
  color: #fff;
  font-size: 9px;
  font-weight: bold;
  width: 16px;
  height: 16px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.notif-dropdown {
  position: absolute;
  top: 40px;
  right: 0;
  width: 260px;
  background: var(--card-bg);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-md);
  box-shadow: var(--shadow-dropdown);
  padding: 10px;
  z-index: 100;
}

.notif-dropdown-header {
  display: flex;
  justify-content: space-between;
  font-size: 11px;
  font-weight: 700;
  margin-bottom: 8px;
  border-bottom: 1px solid var(--border-light);
  padding-bottom: 6px;
}

.notif-clear-btn {
  color: var(--primary);
  cursor: pointer;
  font-size: 10px;
}

.notif-item {
  padding: 6px 0;
  border-bottom: 1px solid var(--border-light);
}

.notif-title {
  font-size: 10.5px;
  font-weight: 600;
  color: var(--text-main);
}

.notif-text {
  font-size: 10px;
  color: var(--text-muted);
}

.notif-time {
  font-size: 9px;
  color: var(--text-light);
  margin-top: 2px;
}

.lang-select {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-main);
  font-size: 10.5px;
  font-weight: 700;
  padding: 5px 8px;
  border-radius: var(--radius-sm);
  outline: none;
  cursor: pointer;
}

.user-profile-pill {
  display: flex;
  align-items: center;
  gap: 8px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 3px 10px 3px 4px;
  border-radius: var(--radius-full);
  cursor: pointer;
  position: relative;
  transition: border-color 0.15s;
}

.user-profile-pill:hover {
  border-color: var(--primary);
}

.user-avatar-circle {
  width: 26px;
  height: 26px;
  border-radius: 50%;
  overflow: hidden;
}

.user-avatar-img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.user-meta {
  display: flex;
  flex-direction: column;
}

.user-name {
  font-size: 11px;
  font-weight: 700;
  color: var(--text-main);
  line-height: 1.1;
}

.user-role-text {
  font-size: 9px;
  font-weight: 600;
  color: var(--primary);
}

.caret-down {
  font-size: 9px;
  color: var(--text-muted);
}

.profile-dropdown {
  position: absolute;
  top: 38px;
  right: 0;
  width: 180px;
  background: var(--card-bg);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  box-shadow: var(--shadow-dropdown);
  padding: 8px;
  z-index: 100;
}

.dropdown-label {
  font-size: 9px;
  color: var(--text-muted);
  font-weight: 700;
  text-transform: uppercase;
  margin-bottom: 6px;
}

.dropdown-item {
  font-size: 10.5px;
  padding: 6px 8px;
  border-radius: 4px;
  color: var(--text-main);
  cursor: pointer;
  margin-bottom: 2px;
}

.dropdown-item:hover {
  background: var(--bg-subtle);
}

.dropdown-item.selected {
  background: var(--primary-light);
  color: var(--primary);
  font-weight: 700;
}

@media (max-width: 1400px) {
  .feature-pills-list {
    display: none;
  }
}

@media (max-width: 1000px) {
  .master-header {
    flex-direction: column;
    align-items: stretch;
  }
  .header-left, .header-center, .header-right {
    justify-content: center;
  }
}
</style>
