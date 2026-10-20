<template>
  <aside class="dashboard-sidebar">
    <div class="sidebar-admin-profile">
      <div class="admin-badge-box">🛡️</div>
      <div class="admin-profile-text">
        <h4 class="admin-title">Boshqaruv Paneli</h4>
        <span class="admin-tag">SUPER ADMIN</span>
      </div>
    </div>

    <div class="sidebar-nav-list">
      <router-link to="/admin" class="s-nav-item" active-class="active" exact>
        <span class="s-icon">📊</span>
        <span>Global Dashboard</span>
      </router-link>

      <router-link to="/admin/hotels" class="s-nav-item" active-class="active">
        <span class="s-icon">🏨</span>
        <span>Mehmonxonalar</span>
        <span class="s-badge">{{ state.hotels.length }}</span>
      </router-link>

      <router-link to="/admin/users" class="s-nav-item" active-class="active">
        <span class="s-icon">👥</span>
        <span>Foydalanuvchilar & Rollar</span>
        <span class="s-badge">{{ state.users.length }}</span>
      </router-link>

      <router-link to="/partner" class="s-nav-item">
        <span class="s-icon">🏢</span>
        <span>Hotel Egasi rejimiga o'tish</span>
      </router-link>

      <router-link to="/blueprint" class="s-nav-item">
        <span class="s-icon">📐</span>
        <span>16 Ekranli Master Blueprint</span>
      </router-link>
    </div>

    <div class="sidebar-system-kpi">
      <div class="kpi-mini-label">Jami Tizim Aylanmasi:</div>
      <div class="kpi-mini-val">125,450,000 so'm</div>
    </div>
  </aside>
</template>

<script setup>
import { state } from '../../store'
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

.sidebar-admin-profile {
  display: flex;
  align-items: center;
  gap: 10px;
  padding-bottom: 12px;
  border-bottom: 1px solid var(--border-light);
}

.admin-badge-box {
  width: 36px;
  height: 36px;
  background: rgba(239, 68, 68, 0.12);
  color: #ef4444;
  border-radius: var(--radius-sm);
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 16px;
}

.admin-title {
  font-size: 13px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1.2;
}

.admin-tag {
  font-size: 9.5px;
  color: #ef4444;
  font-weight: 800;
  letter-spacing: 0.5px;
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
  background: rgba(239, 68, 68, 0.12);
  color: #ef4444;
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

.sidebar-system-kpi {
  margin-top: auto;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 10px 12px;
  text-align: center;
}

.kpi-mini-label {
  font-size: 9px;
  color: var(--text-muted);
  text-transform: uppercase;
  margin-bottom: 2px;
}

.kpi-mini-val {
  font-size: 13px;
  font-weight: 800;
  color: var(--success);
}
</style>
