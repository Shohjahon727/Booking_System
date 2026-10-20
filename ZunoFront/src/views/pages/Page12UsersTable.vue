<template>
  <div class="page-container page-12-users">
    <div class="content-container">
      <!-- Screen 12 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 12</span>
        <span class="s-title">FOYDALANUVCHILAR VA ROLLAR</span>
        <span class="s-role">Ma'muriyat Nazorati</span>
      </div>

      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Tizim Foydalanuvchilari va Rollar Nazorati</h2>
          <p>Super Admin, Hotel egalari va ro'yxatdan o'tgan mijozlar hisoblari</p>
        </div>
        <div class="page-actions-box">
          <span class="card-badge">Jami 1 856 ta foydalanuvchi</span>
        </div>
      </div>

      <!-- Filter Controls Toolbar -->
      <div class="card toolbar-actions-strip">
        <div class="role-selector-btns">
          <button
            v-for="r in ['Barchasi', 'Admin', 'Hotel', 'User']"
            :key="r"
            class="r-filter-btn"
            :class="{ active: currentFilter === r }"
            @click="currentFilter = r"
          >
            {{ r }}
          </button>
        </div>

        <input
          type="text"
          v-model="searchQuery"
          placeholder="Foydalanuvchi ismi yoki email..."
          class="form-input search-user-field"
        />
      </div>

      <!-- Users Table (From Mockup 12) -->
      <div class="card">
        <div class="table-responsive">
          <table class="data-table">
            <thead>
              <tr>
                <th>Foydalanuvchi / Mehmonxona</th>
                <th>Roli / Shahar</th>
                <th>Telefon raqam</th>
                <th>Ro'yxatdan o'tgan</th>
                <th>Status</th>
                <th>Amallar</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="u in filteredUsers" :key="u.id">
                <td>
                  <div class="user-meta-td">
                    <b>{{ u.name }}</b>
                    <small>{{ u.email }}</small>
                  </div>
                </td>
                <td>
                  <span class="role-tag-badge" :class="u.role.toLowerCase()">
                    {{ u.role }}
                  </span>
                </td>
                <td>{{ u.phone }}</td>
                <td>📅 {{ u.registeredDate }}</td>
                <td>
                  <span class="status-badge status-confirmed">{{ u.status }}</span>
                </td>
                <td>
                  <button class="action-btn" @click="changeUserRole(u)" title="Rolni o'zgartirish">⚙️</button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <!-- Pagination (From Mockup 12: < 1 [2] 3 ... 46 >) -->
        <div class="pagination-container">
          <button class="page-btn">&lt;</button>
          <button class="page-btn">1</button>
          <button class="page-btn active">2</button>
          <button class="page-btn">3</button>
          <span class="page-dots">...</span>
          <button class="page-btn">46</button>
          <button class="page-btn">&gt;</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { state } from '../../store'

const currentFilter = ref('Barchasi')
const searchQuery = ref('')

const filteredUsers = computed(() => {
  return state.users.filter(u => {
    const matchRole = currentFilter.value === 'Barchasi' || u.role === currentFilter.value
    const matchSearch = !searchQuery.value ||
      u.name.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
      u.email.toLowerCase().includes(searchQuery.value.toLowerCase())
    return matchRole && matchSearch
  })
})

function changeUserRole(u) {
  const newR = prompt(`${u.name} uchun yangi rolni kiriting (Admin, Hotel, User):`, u.role)
  if (newR) {
    u.role = newR
  }
}
</script>

<style scoped>
.page-12-users {
  padding-bottom: 50px;
}

.screen-identifier-pill {
  display: inline-flex;
  align-items: center;
  gap: 8px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 4px 12px;
  border-radius: var(--radius-full);
  margin-bottom: 16px;
  font-size: 11px;
}

.s-badge {
  background: var(--primary);
  color: #fff;
  font-weight: 800;
  padding: 2px 6px;
  border-radius: 4px;
}

.s-title {
  font-weight: 700;
  color: var(--text-main);
}

.s-role {
  color: #ef4444;
  font-weight: 600;
}

.toolbar-actions-strip {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 18px;
  margin-bottom: 20px;
  flex-direction: row;
}

.role-selector-btns {
  display: flex;
  gap: 6px;
}

.r-filter-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-muted);
  font-size: 11.5px;
  font-weight: 700;
  padding: 6px 14px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  transition: all 0.15s;
}

.r-filter-btn.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.search-user-field {
  width: 260px;
}

.user-meta-td {
  display: flex;
  flex-direction: column;
}

.user-meta-td b {
  color: var(--text-main);
  font-size: 13px;
}

.user-meta-td small {
  color: var(--text-muted);
  font-size: 11px;
}

.role-tag-badge {
  font-size: 10px;
  font-weight: 800;
  padding: 3px 8px;
  border-radius: 4px;
}

.role-tag-badge.admin { background: rgba(239, 68, 68, 0.15); color: #ef4444; }
.role-tag-badge.hotel { background: rgba(245, 158, 11, 0.15); color: #f59e0b; }
.role-tag-badge.user { background: rgba(37, 99, 235, 0.15); color: #2563eb; }

.action-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 28px;
  height: 28px;
  border-radius: 4px;
  cursor: pointer;
}

.pagination-container {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 6px;
  padding-top: 16px;
  margin-top: 14px;
  border-top: 1px solid var(--border-light);
}

.page-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-muted);
  width: 28px;
  height: 28px;
  border-radius: 4px;
  font-size: 11px;
  font-weight: 700;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
}

.page-btn.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.page-dots {
  color: var(--text-muted);
  font-size: 12px;
}

@media (max-width: 800px) {
  .toolbar-actions-strip {
    flex-direction: column;
    align-items: stretch;
    gap: 12px;
  }
}
</style>
