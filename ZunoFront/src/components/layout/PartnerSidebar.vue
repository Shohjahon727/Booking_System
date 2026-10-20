<template>
  <aside class="dashboard-sidebar">
    <div class="sidebar-hotel-profile">
      <div class="hotel-avatar-box">🏨</div>
      <div class="hotel-profile-text">
        <h4 class="hotel-title">Grand Plaza Hotel</h4>
        <span class="hotel-status-tag">● Faol (Partner)</span>
      </div>
    </div>

    <div class="sidebar-nav-list">
      <router-link to="/partner" class="s-nav-item" active-class="active" exact>
        <span class="s-icon">📊</span>
        <span>Dashboard & Statistika</span>
      </router-link>

      <router-link to="/partner/calendar" class="s-nav-item" active-class="active">
        <span class="s-icon">📆</span>
        <span>Xonalar Taqvimi</span>
      </router-link>

      <router-link to="/partner/bookings" class="s-nav-item" active-class="active">
        <span class="s-icon">📑</span>
        <span>Bronlarni Boshqarish</span>
        <span class="s-badge">{{ state.bookings.length }}</span>
      </router-link>

      <router-link to="/partner/rooms" class="s-nav-item" active-class="active">
        <span class="s-icon">🛏️</span>
        <span>Xonalarim & Narxlar</span>
        <span class="s-badge">{{ state.rooms.length }}</span>
      </router-link>

      <router-link to="/partner/reviews" class="s-nav-item" active-class="active">
        <span class="s-icon">⭐</span>
        <span>Sharhlar & Reyting</span>
        <span class="s-badge">★ 4.8</span>
      </router-link>
    </div>

    <!-- Quick action button -->
    <div class="sidebar-action-wrap">
      <button class="btn-primary full-btn" @click="openAddBookingModal">
        + Yangi bron kiritish
      </button>
    </div>
  </aside>
</template>

<script setup>
import { state } from '../../store'

function openAddBookingModal() {
  state.modals.addBooking = true
}
</script>

<style scoped>
.dashboard-sidebar {
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 16px;
  box-shadow: var(--shadow-sm);
}

.sidebar-hotel-profile {
  display: flex;
  align-items: center;
  gap: 10px;
  padding-bottom: 12px;
  border-bottom: 1px solid var(--border-light);
}

.hotel-avatar-box {
  width: 36px;
  height: 36px;
  background: var(--primary-light);
  color: var(--primary);
  border-radius: var(--radius-sm);
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 16px;
}

.hotel-title {
  font-size: 13px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1.2;
}

.hotel-status-tag {
  font-size: 10px;
  color: var(--success);
  font-weight: 600;
}

.sidebar-nav-list {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.s-nav-item {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 8px 12px;
  border-radius: var(--radius-sm);
  font-size: 12.5px;
  font-weight: 600;
  color: var(--text-secondary);
  text-decoration: none;
  transition: all 0.15s;
}

.s-nav-item:hover {
  background: var(--bg-subtle);
  color: var(--text-main);
}

.s-nav-item.active {
  background: var(--primary-light);
  color: var(--primary);
  font-weight: 700;
}

.s-icon {
  font-size: 14px;
  width: 18px;
}

.s-badge {
  margin-left: auto;
  font-size: 9.5px;
  background: var(--bg-subtle);
  padding: 2px 7px;
  border-radius: var(--radius-full);
  color: var(--text-muted);
}

.sidebar-action-wrap {
  margin-top: auto;
  padding-top: 10px;
  border-top: 1px solid var(--border-light);
}

.full-btn {
  width: 100%;
}
</style>
