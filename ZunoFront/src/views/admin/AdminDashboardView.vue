<template>
  <div class="dashboard-layout">
    <div class="dashboard-sidebar-wrap">
      <AdminSidebar />
    </div>

    <div class="dashboard-main-area">
      <div class="page-top-bar">
        <div class="page-title-box">
          <h2>Super Admin — Global Tizim Boshqaruvi</h2>
          <p>Butun platforma ko'rsatkichlari, mehmonxonalar, foydalanuvchilar va umumiy aylanma</p>
        </div>
        <div class="page-actions-box">
          <span class="card-badge" style="color: #ef4444; font-weight: 800;">Global Nazorat</span>
        </div>
      </div>

      <!-- Admin Global Metrics -->
      <div class="admin-kpi-grid">
        <div class="card kpi-card">
          <div class="kpi-icon-wrap">🏨</div>
          <div class="kpi-main-val">156</div>
          <div class="kpi-label-txt">Ro'yxatdan o'tgan mehmonxonalar</div>
          <span class="kpi-sub-positive">+14 ta yangi bu oyda</span>
        </div>

        <div class="card kpi-card">
          <div class="kpi-icon-wrap">👥</div>
          <div class="kpi-main-val">1 856</div>
          <div class="kpi-label-txt">Jami Foydalanuvchilar</div>
          <span class="kpi-sub-positive">+220 ta faol mijoz</span>
        </div>

        <div class="card kpi-card">
          <div class="kpi-icon-wrap">📑</div>
          <div class="kpi-main-val">3 412</div>
          <div class="kpi-label-txt">Jami Bronlar Soni</div>
          <span class="kpi-sub-positive">94% muvaffaqiyatli</span>
        </div>

        <div class="card kpi-card">
          <div class="kpi-icon-wrap">💰</div>
          <div class="kpi-main-val num-green">125,450,000 so'm</div>
          <div class="kpi-label-txt">Umumiy Platforma Aylanmasi</div>
          <span class="kpi-sub-positive">+34% oylik o'sish</span>
        </div>
      </div>

      <!-- Admin Bar Chart: Monthly Turnover -->
      <div class="card monthly-chart-card">
        <div class="card-header">
          <span class="card-title">Oylik aylanma dinamikasi (12 oylik Bar Chart)</span>
          <span class="card-badge">2025 Moliyaviy yil</span>
        </div>

        <div class="admin-bar-chart-container">
          <div v-for="m in monthlyTurnover" :key="m.month" class="bar-item-col">
            <div class="bar-pillar" :style="{ height: m.height + '%' }">
              <span class="bar-hover-val">{{ m.val }}M</span>
            </div>
            <span class="bar-month-title">{{ m.month }}</span>
          </div>
        </div>
      </div>

      <!-- Admin Quick Tables (Hotels & Users Summary) -->
      <div class="admin-tables-two-cols">
        <!-- Hotels snippet -->
        <div class="card">
          <div class="card-header">
            <span class="card-title">Faol Mehmonxonalar</span>
            <router-link to="/admin/hotels" class="view-all-link">Barchasi &rarr;</router-link>
          </div>
          <table class="data-table">
            <thead>
              <tr>
                <th>Mehmonxona</th>
                <th>Shahar</th>
                <th>Xonalar</th>
                <th>Reyting</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="h in state.hotels" :key="h.id">
                <td><b>{{ h.name }}</b></td>
                <td>{{ h.city }}</td>
                <td>{{ h.roomsCount }} ta</td>
                <td><span style="color:#f59e0b; font-weight:bold;">★ {{ h.rating }}</span></td>
              </tr>
            </tbody>
          </table>
        </div>

        <!-- Users snippet -->
        <div class="card">
          <div class="card-header">
            <span class="card-title">Foydalanuvchilar va Rollar</span>
            <router-link to="/admin/users" class="view-all-link">Barchasi &rarr;</router-link>
          </div>
          <table class="data-table">
            <thead>
              <tr>
                <th>Ism</th>
                <th>Email</th>
                <th>Roli</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="u in state.users.slice(0, 4)" :key="u.id">
                <td><b>{{ u.name }}</b></td>
                <td>{{ u.email }}</td>
                <td>
                  <span class="role-pill" :class="u.role.toLowerCase()">{{ u.role }}</span>
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
import { state } from '../../store'
import AdminSidebar from '../../components/layout/AdminSidebar.vue'

const monthlyTurnover = [
  { month: 'Yan', height: 45, val: 55 },
  { month: 'Fev', height: 50, val: 62 },
  { month: 'Mar', height: 65, val: 78 },
  { month: 'Apr', height: 60, val: 72 },
  { month: 'May', height: 95, val: 125 },
  { month: 'Iyun', height: 85, val: 105 },
  { month: 'Iyul', height: 90, val: 115 },
  { month: 'Avg', height: 100, val: 130 },
  { month: 'Sen', height: 80, val: 95 },
  { month: 'Okt', height: 75, val: 90 },
  { month: 'Noy', height: 70, val: 84 },
  { month: 'Dek', height: 85, val: 102 }
]
</script>

<style scoped>
.admin-kpi-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 16px;
}

.kpi-card {
  padding: 18px;
}

.kpi-icon-wrap {
  font-size: 20px;
  margin-bottom: 8px;
}

.kpi-main-val {
  font-size: 22px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1.1;
}

.num-green {
  color: var(--success);
  font-size: 17px;
}

.kpi-label-txt {
  font-size: 11.5px;
  color: var(--text-muted);
  margin-top: 4px;
}

.kpi-sub-positive {
  font-size: 10px;
  color: var(--success);
  font-weight: 700;
  margin-top: 8px;
  display: block;
}

/* Monthly bar chart */
.admin-bar-chart-container {
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  height: 160px;
  gap: 8px;
  padding: 20px 10px 0 10px;
}

.bar-item-col {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
  justify-content: flex-end;
}

.bar-pillar {
  width: 100%;
  max-width: 40px;
  background: var(--primary);
  border-radius: 4px 4px 0 0;
  display: flex;
  align-items: flex-start;
  justify-content: center;
  transition: all 0.3s ease;
  cursor: pointer;
  position: relative;
}

.bar-pillar:hover {
  background: #38bdf8;
}

.bar-hover-val {
  font-size: 9px;
  font-weight: 800;
  color: #fff;
  margin-top: 4px;
}

.bar-month-title {
  font-size: 11px;
  color: var(--text-muted);
  margin-top: 8px;
  font-weight: 600;
}

.admin-tables-two-cols {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 20px;
}

.role-pill {
  font-size: 9.5px;
  font-weight: 800;
  padding: 2px 7px;
  border-radius: 4px;
}

.role-pill.admin { background: rgba(239, 68, 68, 0.12); color: #ef4444; }
.role-pill.hotel { background: rgba(245, 158, 11, 0.12); color: #f59e0b; }
.role-pill.user { background: rgba(37, 99, 235, 0.12); color: #2563eb; }

@media (max-width: 1100px) {
  .admin-kpi-grid {
    grid-template-columns: repeat(2, 1fr);
  }
  .admin-tables-two-cols {
    grid-template-columns: 1fr;
  }
}
</style>
