<template>
  <div class="dashboard-layout">
    <div class="dashboard-sidebar-wrap">
      <AdminSidebar />
    </div>

    <div class="dashboard-main-area">
      <div class="page-top-bar">
        <div class="page-title-box">
          <h2>Foydalanuvchilar va Rollarni Boshqarish</h2>
          <p>Tizim ma'murlari, mehmonxona egalari va ro'yxatdan o'tgan mijozlar ro'yxati</p>
        </div>
        <div class="page-actions-box">
          <span class="card-badge">Jami {{ state.users.length }} ta foydalanuvchi</span>
        </div>
      </div>

      <!-- Users Filter Toolbar -->
      <div class="card toolbar-box">
        <div class="filter-roles">
          <button
            v-for="r in ['Barchasi', 'Admin', 'Hotel', 'User']"
            :key="r"
            class="r-btn"
            :class="{ active: selectedRole === r }"
            @click="selectedRole = r"
          >
            {{ r }}
          </button>
        </div>

        <input
          type="text"
          v-model="search"
          placeholder="Ism yoki email bo'yicha qidiruv..."
          class="form-input search-user-box"
        />
      </div>

      <!-- Users Table -->
      <div class="card">
        <div class="table-responsive">
          <table class="data-table">
            <thead>
              <tr>
                <th>Foydalanuvchi</th>
                <th>Email</th>
                <th>Telefon</th>
                <th>Roli</th>
                <th>Ro'yxatdan o'tgan</th>
                <th>Status</th>
                <th>Amallar</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="u in filteredUsers" :key="u.id">
                <td><b>{{ u.name }}</b></td>
                <td>{{ u.email }}</td>
                <td>{{ u.phone }}</td>
                <td>
                  <span class="role-badge" :class="u.role.toLowerCase()">{{ u.role }}</span>
                </td>
                <td>{{ u.registeredDate }}</td>
                <td><span class="status-badge status-confirmed">{{ u.status }}</span></td>
                <td>
                  <button class="btn-action" @click="changeRole(u)" title="Rolni o'zgartirish">⚙️</button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { state } from '../../store'
import AdminSidebar from '../../components/layout/AdminSidebar.vue'

const selectedRole = ref('Barchasi')
const search = ref('')

const filteredUsers = computed(() => {
  return state.users.filter(u => {
    const matchRole = selectedRole.value === 'Barchasi' || u.role === selectedRole.value
    const matchSearch = !search.value ||
      u.name.toLowerCase().includes(search.value.toLowerCase()) ||
      u.email.toLowerCase().includes(search.value.toLowerCase())
    return matchRole && matchSearch
  })
})

function changeRole(u) {
  const newRole = prompt(`${u.name} uchun yangi rolni kiriting (Admin, Hotel, User):`, u.role)
  if (newRole) {
    u.role = newRole
  }
}
</script>

<style scoped>
.toolbar-box {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 18px;
  flex-direction: row;
}

.filter-roles {
  display: flex;
  gap: 6px;
}

.r-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-muted);
  font-size: 11px;
  font-weight: 700;
  padding: 6px 12px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  transition: all 0.15s;
}

.r-btn.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.search-user-box {
  width: 250px;
}

.role-badge {
  font-size: 10px;
  font-weight: 800;
  padding: 2px 7px;
  border-radius: 4px;
}

.role-badge.admin { background: rgba(239, 68, 68, 0.15); color: #ef4444; }
.role-badge.hotel { background: rgba(245, 158, 11, 0.15); color: #f59e0b; }
.role-badge.user { background: rgba(37, 99, 235, 0.15); color: #2563eb; }

.btn-action {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 28px;
  height: 28px;
  border-radius: 4px;
  cursor: pointer;
}
</style>
