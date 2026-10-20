<template>
  <aside class="master-sidebar">
    <!-- Top Master View Button -->
    <button
      class="blueprint-master-btn"
      :class="{ active: state.activeView === 'ALL' }"
      @click="setActiveView('ALL')"
    >
      <span class="btn-icon">🏠</span>
      <span>Bosh sablon</span>
      <span class="view-tag">16 Ekran</span>
    </button>

    <div class="sidebar-scrollable">
      <!-- 1. USER SECTION -->
      <div class="nav-section">
        <div class="nav-section-title">
          <span>USER (Mijoz / Qidirish)</span>
          <span class="role-pill">Mehmon</span>
        </div>
        <ul class="nav-items">
          <li
            class="nav-item"
            :class="{ active: state.activeView === '01' }"
            @click="navigateToScreen('01')"
          >
            <span class="item-icon">🔍</span>
            <span class="item-label">Bosh sahifa & Qidirish</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '02' }"
            @click="navigateToScreen('02')"
          >
            <span class="item-icon">🏨</span>
            <span class="item-label">Mehmonxona detali</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '03' }"
            @click="navigateToScreen('03')"
          >
            <span class="item-icon">🛏️</span>
            <span class="item-label">Xonalarni tanlash</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '04' }"
            @click="navigateToScreen('04')"
          >
            <span class="item-icon">📅</span>
            <span class="item-label">Bron qilish & Hisob</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '05' }"
            @click="navigateToScreen('05')"
          >
            <span class="item-icon">📋</span>
            <span class="item-label">Bronlarim holati</span>
            <span class="counter-badge">{{ state.bookings.length }}</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '15' }"
            @click="navigateToScreen('15')"
          >
            <span class="item-icon">❤️</span>
            <span class="item-label">Sevimlilar (Wishlist)</span>
            <span class="counter-badge">{{ favoriteCount }}</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '16' }"
            @click="navigateToScreen('16')"
          >
            <span class="item-icon">💬</span>
            <span class="item-label">Xabarlar & Chat</span>
            <span class="counter-badge counter-green">1</span>
          </li>
        </ul>
      </div>

      <!-- 2. HOTEL OWNER SECTION -->
      <div class="nav-section">
        <div class="nav-section-title">
          <span>HOTEL EGASI (Partner)</span>
          <span class="role-pill">Egasi</span>
        </div>
        <ul class="nav-items">
          <li
            class="nav-item"
            :class="{ active: state.activeView === '02' }"
            @click="navigateToScreen('02')"
          >
            <span class="item-icon">🏢</span>
            <span class="item-label">Mening mehmonxonam</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '03' }"
            @click="navigateToScreen('03')"
          >
            <span class="item-icon">🛏️</span>
            <span class="item-label">Xonalarim ro'yxati</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '07' }"
            @click="navigateToScreen('07')"
          >
            <span class="item-icon">📑</span>
            <span class="item-label">Bronlarni boshqarish</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '06' }"
            @click="navigateToScreen('06')"
          >
            <span class="item-icon">📆</span>
            <span class="item-label">Xonalar taqvimi</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '08' }"
            @click="navigateToScreen('08')"
          >
            <span class="item-icon">⭐</span>
            <span class="item-label">Sharhlar & Reyting</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '09' }"
            @click="navigateToScreen('09')"
          >
            <span class="item-icon">📊</span>
            <span class="item-label">Egasi statistikasi</span>
          </li>
        </ul>
      </div>

      <!-- 3. ADMIN SECTION -->
      <div class="nav-section">
        <div class="nav-section-title">
          <span>ADMIN (Ma'muriyat)</span>
          <span class="role-pill role-admin">Admin</span>
        </div>
        <ul class="nav-items">
          <li
            class="nav-item"
            :class="{ active: state.activeView === '10' }"
            @click="navigateToScreen('10')"
          >
            <span class="item-icon">📊</span>
            <span class="item-label">Admin Dashboard</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '11' }"
            @click="navigateToScreen('11')"
          >
            <span class="item-icon">🏨</span>
            <span class="item-label">Xonalar jadvali</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '12' }"
            @click="navigateToScreen('12')"
          >
            <span class="item-icon">👥</span>
            <span class="item-label">Foydalanuvchilar</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '13' }"
            @click="navigateToScreen('13')"
          >
            <span class="item-icon">🔐</span>
            <span class="item-label">Kirish / Ro'yxatdan o'tish</span>
          </li>
          <li
            class="nav-item"
            :class="{ active: state.activeView === '14' }"
            @click="navigateToScreen('14')"
          >
            <span class="item-icon">💳</span>
            <span class="item-label">To'lov sahifasi</span>
          </li>
        </ul>
      </div>
    </div>

    <!-- Bottom Support Box -->
    <div class="sidebar-bottom-support">
      <div class="support-content">
        <div class="support-title">Yordam kerakmi?</div>
        <div class="support-desc">24/7 qo'llab-quvvatlash</div>
      </div>
      <button class="support-action-btn" @click="openSupportModal">
        24/7
      </button>
    </div>
  </aside>
</template>

<script setup>
import { computed } from 'vue'
import { state, setActiveView } from '../store'

const favoriteCount = computed(() => state.hotels.filter(h => h.isFavorite).length)

function navigateToScreen(screenId) {
  setActiveView(screenId)
}

function openSupportModal() {
  state.modals.support = true
}
</script>

<style scoped>
.master-sidebar {
  background: var(--sidebar-bg);
  border: 1px solid var(--sidebar-border);
  border-radius: var(--radius-md);
  padding: 14px;
  display: flex;
  flex-direction: column;
  box-shadow: var(--shadow-sm);
  height: calc(100vh - 110px);
  position: sticky;
  top: 90px;
}

.blueprint-master-btn {
  background: var(--primary);
  color: #ffffff;
  border: none;
  padding: 10px 14px;
  border-radius: var(--radius-sm);
  font-weight: 700;
  font-size: 12px;
  display: flex;
  align-items: center;
  gap: 10px;
  cursor: pointer;
  margin-bottom: 14px;
  box-shadow: 0 2px 6px rgba(37, 99, 235, 0.3);
  transition: all 0.15s ease;
}

.blueprint-master-btn:hover {
  background: var(--primary-hover);
  transform: translateY(-1px);
}

.blueprint-master-btn.active {
  background: #1d4ed8;
  outline: 2px solid rgba(37, 99, 235, 0.5);
}

.view-tag {
  margin-left: auto;
  background: rgba(255, 255, 255, 0.25);
  font-size: 9.5px;
  padding: 2px 6px;
  border-radius: 10px;
}

.sidebar-scrollable {
  flex: 1;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 16px;
  padding-right: 4px;
}

.nav-section {
  display: flex;
  flex-direction: column;
}

.nav-section-title {
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 10px;
  font-weight: 800;
  text-transform: uppercase;
  color: var(--text-muted);
  letter-spacing: 0.5px;
  padding: 0 4px 6px 4px;
  border-bottom: 1px solid var(--border-light);
  margin-bottom: 6px;
}

.role-pill {
  font-size: 8.5px;
  font-weight: 700;
  background: var(--bg-subtle);
  color: var(--text-muted);
  padding: 2px 6px;
  border-radius: 8px;
}

.role-admin {
  background: var(--danger-bg);
  color: var(--danger);
}

.nav-items {
  list-style: none;
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.nav-item {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 6px 10px;
  border-radius: var(--radius-sm);
  color: var(--text-secondary);
  font-size: 11.5px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.12s ease;
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

.item-icon {
  font-size: 13px;
  width: 18px;
  text-align: center;
}

.item-label {
  flex: 1;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.counter-badge {
  font-size: 9px;
  background: var(--bg-subtle);
  color: var(--text-muted);
  padding: 2px 6px;
  border-radius: 10px;
  font-weight: 700;
}

.counter-green {
  background: var(--success-bg);
  color: var(--success);
}

/* Bottom Support Box */
.sidebar-bottom-support {
  margin-top: auto;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 10px 12px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.support-title {
  font-size: 11px;
  font-weight: 700;
  color: var(--text-main);
}

.support-desc {
  font-size: 9.5px;
  color: var(--text-muted);
}

.support-action-btn {
  background: var(--primary);
  color: #fff;
  border: none;
  padding: 6px 12px;
  border-radius: var(--radius-sm);
  font-weight: 800;
  font-size: 11px;
  cursor: pointer;
  transition: background 0.15s;
}

.support-action-btn:hover {
  background: var(--primary-hover);
}
</style>
