<template>
  <div class="page-container page-10-admin-dash">
    <div class="content-container">
      <!-- Screen 10 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 10</span>
        <span class="s-title">ADMIN DASHBOARD</span>
        <span class="s-role role-admin">Global Boshqaruv</span>
      </div>

      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Super Admin — Global Tizim Dashboardi</h2>
          <p>O'zbekiston bo'yicha platformadagi barcha mehmonxonalar, foydalanuvchilar va umumiy aylanma</p>
        </div>
        <div class="page-actions-box">
          <router-link to="/admin/users" class="btn-primary">
            👥 Foydalanuvchilarni ko'rish
          </router-link>
        </div>
      </div>

      <!-- 3 Key Metric KPI Cards (From Mockup 10) -->
      <div class="admin-kpi-three-grid">
        <div class="card kpi-card-admin">
          <div class="admin-kpi-icon">🏨</div>
          <div class="admin-kpi-number">156</div>
          <div class="admin-kpi-lbl">Ulangan Mehmonxonalar</div>
          <span class="trend-txt">+14 ta yangi</span>
        </div>

        <div class="card kpi-card-admin">
          <div class="admin-kpi-icon">👥</div>
          <div class="admin-kpi-number">1 456</div>
          <div class="admin-kpi-lbl">Tizim Foydalanuvchilari</div>
          <span class="trend-txt">+180 ta yangi mijoz</span>
        </div>

        <div class="card kpi-card-admin">
          <div class="admin-kpi-icon">💰</div>
          <div class="admin-kpi-number green-money">125,450,000 so'm</div>
          <div class="admin-kpi-lbl">Jami Platforma Daromadi</div>
          <span class="trend-txt">+34% yillik o'sish</span>
        </div>
      </div>

      <!-- Charts: Bar Chart & Donut -->
      <div class="admin-charts-two-cols">
        <!-- 1. Bar Chart: Daromad grafigi (oylik) -->
        <div class="card admin-bar-card">
          <div class="card-header">
            <span class="card-title">Daromad grafigi (oylik)</span>
            <span class="card-badge">2025 Yillik monitoring</span>
          </div>

          <div class="bar-chart-visual-box">
            <div v-for="b in monthlyTurnover" :key="b.m" class="bar-column-unit">
              <div class="bar-rect" :style="{ height: b.h + '%' }">
                <span class="bar-val-tip">{{ b.v }}M</span>
              </div>
              <span class="bar-month-text">{{ b.m }}</span>
            </div>
          </div>
        </div>

        <!-- 2. Donut Chart: Bronlar ulushi -->
        <div class="card admin-donut-card">
          <div class="card-header">
            <span class="card-title">Bronlar Holati</span>
            <span class="card-badge">Ulush</span>
          </div>

          <div class="donut-chart-flex">
            <svg viewBox="0 0 42 42" class="big-donut-svg">
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#ef4444" stroke-width="5"
                stroke-dasharray="15 85" stroke-dashoffset="0"></circle>
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#f59e0b" stroke-width="5"
                stroke-dasharray="20 80" stroke-dashoffset="-15"></circle>
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#10b981" stroke-width="5"
                stroke-dasharray="65 35" stroke-dashoffset="-35"></circle>
            </svg>

            <div class="donut-legends-col">
              <div class="leg-item">
                <span class="leg-box col-conf"></span>
                <span>Confirmed (<b>65%</b>)</span>
              </div>
              <div class="leg-item">
                <span class="leg-box col-pend"></span>
                <span>Pending (<b>20%</b>)</span>
              </div>
              <div class="leg-item">
                <span class="leg-box col-canc"></span>
                <span>Cancelled (<b>15%</b>)</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
const monthlyTurnover = [
  { m: 'Yan', h: 45, v: 55 },
  { m: 'Fev', h: 50, v: 62 },
  { m: 'Mar', h: 65, v: 78 },
  { m: 'Apr', h: 60, v: 72 },
  { m: 'May', h: 95, v: 125 },
  { m: 'Iyun', h: 85, v: 105 },
  { m: 'Iyul', h: 90, v: 115 },
  { m: 'Avg', h: 100, v: 130 },
  { m: 'Sen', h: 80, v: 95 },
  { m: 'Okt', h: 75, v: 90 }
]
</script>

<style scoped>
.page-10-admin-dash {
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

.role-admin {
  color: #ef4444;
  font-weight: 700;
}

.admin-kpi-three-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 20px;
  margin-bottom: 24px;
}

.kpi-card-admin {
  padding: 22px;
}

.admin-kpi-icon {
  font-size: 24px;
  margin-bottom: 8px;
}

.admin-kpi-number {
  font-size: 26px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1.1;
}

.green-money {
  color: var(--success);
  font-size: 20px;
}

.admin-kpi-lbl {
  font-size: 12px;
  color: var(--text-muted);
  margin-top: 4px;
}

.trend-txt {
  font-size: 11px;
  color: var(--success);
  font-weight: 700;
  margin-top: 8px;
  display: inline-block;
}

.admin-charts-two-cols {
  display: grid;
  grid-template-columns: 1.7fr 1fr;
  gap: 20px;
}

.bar-chart-visual-box {
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  height: 180px;
  gap: 10px;
  padding: 20px 10px 0 10px;
}

.bar-column-unit {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
  justify-content: flex-end;
}

.bar-rect {
  width: 100%;
  max-width: 36px;
  background: var(--primary);
  border-radius: 4px 4px 0 0;
  display: flex;
  align-items: flex-start;
  justify-content: center;
  transition: all 0.25s;
  cursor: pointer;
}

.bar-rect:hover {
  background: #38bdf8;
}

.bar-val-tip {
  font-size: 9px;
  font-weight: 800;
  color: #fff;
  margin-top: 4px;
}

.bar-month-text {
  font-size: 11px;
  color: var(--text-muted);
  margin-top: 8px;
  font-weight: 600;
}

.donut-chart-flex {
  display: flex;
  align-items: center;
  justify-content: space-around;
  padding: 24px 0;
}

.big-donut-svg {
  width: 110px;
  height: 110px;
  transform: rotate(-90deg);
}

.donut-legends-col {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.leg-item {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 13px;
  color: var(--text-secondary);
}

.leg-box {
  width: 12px;
  height: 12px;
  border-radius: 3px;
}

.col-conf { background: #10b981; }
.col-pend { background: #f59e0b; }
.col-canc { background: #ef4444; }

@media (max-width: 900px) {
  .admin-kpi-three-grid {
    grid-template-columns: 1fr;
  }
  .admin-charts-two-cols {
    grid-template-columns: 1fr;
  }
}
</style>
