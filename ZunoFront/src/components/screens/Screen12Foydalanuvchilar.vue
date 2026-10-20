<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">12</span> FOYDALANUVCHILAR
      </span>
      <span class="card-badge">Jami 1 856 ta</span>
    </div>

    <!-- Users Table -->
    <div class="table-responsive-box">
      <table class="data-table">
        <thead>
          <tr>
            <th>Foydalanuvchi</th>
            <th>Email</th>
            <th>Roli</th>
            <th>Status</th>
            <th>Amallar</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="u in state.users" :key="u.id">
            <td class="user-name-col">
              <b>{{ u.name }}</b>
            </td>
            <td class="email-col">{{ u.email }}</td>
            <td>
              <span
                class="role-badge"
                :class="{
                  'role-admin': u.role === 'Admin',
                  'role-hotel': u.role === 'Hotel',
                  'role-user': u.role === 'User'
                }"
              >
                {{ u.role }}
              </span>
            </td>
            <td>
              <span class="status-badge status-confirmed">{{ u.status }}</span>
            </td>
            <td>
              <button class="action-btn-mini" @click="handleEditUser(u)">⚙️</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Pagination -->
    <div class="pagination-footer-row">
      <button class="page-nav-btn">&lt;</button>
      <button class="page-num-btn">1</button>
      <button class="page-num-btn active">2</button>
      <button class="page-num-btn">3</button>
      <span class="page-ellipsis">...</span>
      <button class="page-num-btn">46</button>
      <button class="page-nav-btn">&gt;</button>
    </div>
  </div>
</template>

<script setup>
import { state } from '../../store'

function handleEditUser(user) {
  alert(`${user.name} ning ruxsatlari boshqarish oynasi.`)
}
</script>

<style scoped>
.screen-card {
  height: 100%;
}

.screen-num {
  font-weight: 800;
  color: var(--primary);
  margin-right: 2px;
}

.table-responsive-box {
  overflow-x: auto;
  flex: 1;
}

.user-name-col {
  white-space: nowrap;
  color: var(--text-main);
}

.email-col {
  color: var(--text-muted);
  font-size: 9.5px;
}

.role-badge {
  font-size: 8.5px;
  font-weight: 800;
  padding: 2px 6px;
  border-radius: 4px;
}

.role-admin {
  color: #ef4444;
  background: rgba(239, 68, 68, 0.12);
}

.role-hotel {
  color: #f59e0b;
  background: rgba(245, 158, 11, 0.12);
}

.role-user {
  color: #2563eb;
  background: rgba(37, 99, 235, 0.12);
}

.action-btn-mini {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 2px 5px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 9px;
}

.pagination-footer-row {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 4px;
  margin-top: 8px;
}

.page-nav-btn, .page-num-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-muted);
  width: 22px;
  height: 22px;
  border-radius: 4px;
  font-size: 9px;
  font-weight: 700;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

.page-num-btn.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.page-ellipsis {
  font-size: 9px;
  color: var(--text-muted);
}
</style>
