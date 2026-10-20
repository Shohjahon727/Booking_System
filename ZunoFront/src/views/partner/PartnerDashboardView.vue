<template>
  <div class="dashboard-layout">
    <div class="dashboard-sidebar-wrap">
      <PartnerSidebar />
    </div>

    <div class="dashboard-main-area">
      <!-- Top Title -->
      <div class="page-top-bar">
        <div class="page-title-box">
          <h2>Grand Plaza Hotel — Boshqaruv Paneli</h2>
          <p>Bugungi bandlik holati, tushumlar va mijozlar faolligi tahlili</p>
        </div>
        <div class="page-actions-box">
          <span class="status-badge status-confirmed">● Tizim faol</span>
        </div>
      </div>

      <!-- KPI Metrics Row -->
      <div class="kpi-cards-grid">
        <div class="card kpi-card">
          <span class="kpi-icon-badge">🛏️</span>
          <div class="kpi-val">128</div>
          <div class="kpi-label">Jami O'rinlar</div>
          <span class="kpi-trend positive">+12% o'tgan oyga nisbatan</span>
        </div>

        <div class="card kpi-card">
          <span class="kpi-icon-badge">👥</span>
          <div class="kpi-val">256</div>
          <div class="kpi-label">Jami Mehmonlar</div>
          <span class="kpi-trend positive">+18% yangi mijozlar</span>
        </div>

        <div class="card kpi-card">
          <span class="kpi-icon-badge">📈</span>
          <div class="kpi-val">68%</div>
          <div class="kpi-label">O'rtacha Bandlik</div>
          <span class="kpi-trend positive">Yuqori ko'rsatkich</span>
        </div>

        <div class="card kpi-card">
          <span class="kpi-icon-badge">💰</span>
          <div class="kpi-val num-green">24,500,000 so'm</div>
          <div class="kpi-label">Oylik Tushum (May)</div>
          <span class="kpi-trend positive">+34% o'sish</span>
        </div>
      </div>

      <!-- Charts Row: Revenue Line Graph & Booking Status Donut -->
      <div class="charts-two-cols-row">
        <!-- Revenue Line Chart -->
        <div class="card chart-large-card">
          <div class="card-header">
            <span class="card-title">Daromad o'sish dinamikasi (May 2025)</span>
            <span class="card-badge">Oylik tahlil</span>
          </div>

          <div class="chart-drawing-box">
            <svg viewBox="0 0 600 180" class="full-line-chart-svg">
              <defs>
                <linearGradient id="ownerGradFull" x1="0%" y1="0%" x2="0%" y2="100%">
                  <stop offset="0%" stop-color="#38bdf8" stop-opacity="0.4"/>
                  <stop offset="100%" stop-color="#38bdf8" stop-opacity="0"/>
                </linearGradient>
              </defs>
              <!-- Horizontal grid lines -->
              <line x1="0" y1="40" x2="600" y2="40" stroke="currentColor" stroke-opacity="0.08" />
              <line x1="0" y1="90" x2="600" y2="90" stroke="currentColor" stroke-opacity="0.08" />
              <line x1="0" y1="140" x2="600" y2="140" stroke="currentColor" stroke-opacity="0.08" />

              <!-- Area Fill -->
              <polygon fill="url(#ownerGradFull)" points="0,150 60,130 120,120 180,95 240,110 300,70 360,85 420,50 480,65 540,30 600,20 600,180 0,180" />

              <!-- Stroke Line -->
              <polyline fill="none" stroke="#2563eb" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"
                points="0,150 60,130 120,120 180,95 240,110 300,70 360,85 420,50 480,65 540,30 600,20" />

              <!-- Points -->
              <circle cx="300" cy="70" r="5" fill="#2563eb" />
              <circle cx="600" cy="20" r="6" fill="#10b981" />
            </svg>
            <div class="chart-axis-days">
              <span>01 May</span>
              <span>05 May</span>
              <span>10 May</span>
              <span>15 May</span>
              <span>20 May</span>
              <span>25 May</span>
              <span>31 May</span>
            </div>
          </div>
        </div>

        <!-- Donut Chart: Status distribution -->
        <div class="card donut-card">
          <div class="card-header">
            <span class="card-title">Bronlar Holati</span>
            <span class="card-badge">Taqsimot</span>
          </div>

          <div class="donut-chart-wrap">
            <svg viewBox="0 0 42 42" class="big-donut-svg">
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#ef4444" stroke-width="5"
                stroke-dasharray="15 85" stroke-dashoffset="0"></circle>
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#f59e0b" stroke-width="5"
                stroke-dasharray="15 85" stroke-dashoffset="-15"></circle>
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#10b981" stroke-width="5"
                stroke-dasharray="70 30" stroke-dashoffset="-30"></circle>
            </svg>

            <div class="donut-legends-list">
              <div class="d-leg-item">
                <span class="leg-box col-conf"></span>
                <span>Tasdiqlangan: <b>70%</b></span>
              </div>
              <div class="d-leg-item">
                <span class="leg-box col-pend"></span>
                <span>Kutilmoqda: <b>15%</b></span>
              </div>
              <div class="d-leg-item">
                <span class="leg-box col-canc"></span>
                <span>Bekor qilingan: <b>15%</b></span>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Recent Bookings Table Card -->
      <div class="card">
        <div class="card-header">
          <span class="card-title">So'nggi qabul qilingan bronlar</span>
          <router-link to="/partner/bookings" class="view-all-link">Barchasini boshqarish &rarr;</router-link>
        </div>

        <div class="table-responsive">
          <table class="data-table">
            <thead>
              <tr>
                <th>Bron ID</th>
                <th>Mijoz</th>
                <th>Xona toifasi</th>
                <th>Kirish — Chiqish</th>
                <th>Jami</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="b in state.bookings.slice(0, 4)" :key="b.id">
                <td><b>{{ b.id }}</b></td>
                <td>{{ b.guestName }}</td>
                <td>{{ b.roomName }}</td>
                <td>{{ b.checkIn }} — {{ b.checkOut }}</td>
                <td class="sum-col">{{ formatSum(b.totalPrice) }} so'm</td>
                <td>
                  <span :class="['status-badge', b.status]">{{ b.status }}</span>
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
import PartnerSidebar from '../../components/layout/PartnerSidebar.vue'

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}
</script>

<style scoped>
.kpi-cards-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 16px;
}

.kpi-card {
  padding: 18px;
}

.kpi-icon-badge {
  font-size: 20px;
  margin-bottom: 8px;
  display: block;
}

.kpi-val {
  font-size: 20px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1.1;
}

.num-green {
  color: var(--success);
  font-size: 17px;
}

.kpi-label {
  font-size: 11.5px;
  color: var(--text-muted);
  margin-top: 4px;
}

.kpi-trend {
  font-size: 10px;
  font-weight: 700;
  margin-top: 8px;
  display: block;
}

.kpi-trend.positive {
  color: var(--success);
}

.charts-two-cols-row {
  display: grid;
  grid-template-columns: 1.7fr 1fr;
  gap: 20px;
}

.full-line-chart-svg {
  width: 100%;
  height: 140px;
  display: block;
}

.chart-axis-days {
  display: flex;
  justify-content: space-between;
  font-size: 10px;
  color: var(--text-muted);
  margin-top: 6px;
}

.donut-chart-wrap {
  display: flex;
  align-items: center;
  justify-content: space-around;
  padding: 16px 0;
}

.big-donut-svg {
  width: 100px;
  height: 100px;
  transform: rotate(-90deg);
}

.donut-legends-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.d-leg-item {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 12px;
  color: var(--text-secondary);
}

.leg-box {
  width: 10px;
  height: 10px;
  border-radius: 2px;
}

.col-conf { background: #10b981; }
.col-pend { background: #f59e0b; }
.col-canc { background: #ef4444; }

.sum-col {
  font-weight: 800;
  color: var(--primary);
}

@media (max-width: 1100px) {
  .kpi-cards-grid {
    grid-template-columns: repeat(2, 1fr);
  }
  .charts-two-cols-row {
    grid-template-columns: 1fr;
  }
}
</style>
