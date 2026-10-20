<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">10</span> ADMIN DASHBOARD
      </span>
      <span class="card-badge" style="color: #ef4444; font-weight: bold;">Global</span>
    </div>

    <!-- 3 Admin KPI Metric Cards -->
    <div class="kpi-three-grid">
      <div class="kpi-mini-box">
        <div class="kpi-lbl">Mehmonxonalar</div>
        <div class="kpi-num">156</div>
      </div>
      <div class="kpi-mini-box">
        <div class="kpi-lbl">Foydalanuvchilar</div>
        <div class="kpi-num">1 456</div>
      </div>
      <div class="kpi-mini-box">
        <div class="kpi-lbl">Jami daromad</div>
        <div class="kpi-num num-green">125,450,000 so'm</div>
      </div>
    </div>

    <!-- Monthly Revenue Bar Chart -->
    <div class="chart-container-box">
      <div class="chart-title-line">Daromad grafigi (oylik)</div>
      <div class="bar-chart-bars-wrap">
        <div v-for="bar in monthlyStats" :key="bar.month" class="bar-col">
          <div class="bar-fill" :style="{ height: bar.height + '%' }"></div>
          <span class="bar-month-lbl">{{ bar.month }}</span>
        </div>
      </div>
    </div>

    <!-- Donut Chart & Legend -->
    <div class="status-distribution-box">
      <div class="distribution-title">Bronlar ulushi:</div>
      <div class="donut-and-legend-row">
        <svg viewBox="0 0 42 42" class="donut-svg">
          <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#ef4444" stroke-width="5"
            stroke-dasharray="15 85" stroke-dashoffset="0"></circle>
          <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#f59e0b" stroke-width="5"
            stroke-dasharray="20 80" stroke-dashoffset="-15"></circle>
          <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#10b981" stroke-width="5"
            stroke-dasharray="65 35" stroke-dashoffset="-35"></circle>
        </svg>

        <div class="donut-legend-col">
          <div class="donut-legend-item">
            <span class="legend-color-box col-confirmed"></span>
            <span>Tasdiqlangan (65%)</span>
          </div>
          <div class="donut-legend-item">
            <span class="legend-color-box col-pending"></span>
            <span>Kutilmoqda (20%)</span>
          </div>
          <div class="donut-legend-item">
            <span class="legend-color-box col-cancelled"></span>
            <span>Bekor qilingan (15%)</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
const monthlyStats = [
  { month: 'Yan', height: 45 },
  { month: 'Fev', height: 55 },
  { month: 'Mar', height: 70 },
  { month: 'Apr', height: 65 },
  { month: 'May', height: 90 },
  { month: 'Iyun', height: 85 },
  { month: 'Iyul', height: 95 },
  { month: 'Avg', height: 100 },
  { month: 'Sen', height: 80 },
  { month: 'Okt', height: 75 }
]
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

.kpi-three-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 6px;
  margin-bottom: 8px;
}

.kpi-mini-box {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
  text-align: center;
}

.kpi-lbl {
  font-size: 8px;
  color: var(--text-muted);
  text-transform: uppercase;
  margin-bottom: 2px;
}

.kpi-num {
  font-size: 11px;
  font-weight: 800;
  color: var(--text-main);
  white-space: nowrap;
}

.num-green {
  color: var(--success);
}

.chart-container-box {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
  margin-bottom: 8px;
}

.chart-title-line {
  font-size: 8.5px;
  font-weight: 700;
  color: var(--text-muted);
  margin-bottom: 4px;
}

.bar-chart-bars-wrap {
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  height: 55px;
  gap: 4px;
  padding-top: 6px;
}

.bar-col {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
  justify-content: flex-end;
}

.bar-fill {
  width: 100%;
  background: #2563eb;
  border-radius: 2px 2px 0 0;
  transition: height 0.3s ease;
}

.bar-col:nth-child(even) .bar-fill {
  background: #38bdf8;
}

.bar-month-lbl {
  font-size: 7px;
  color: var(--text-light);
  margin-top: 2px;
}

.status-distribution-box {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
  margin-top: auto;
}

.distribution-title {
  font-size: 8.5px;
  font-weight: 700;
  color: var(--text-muted);
  margin-bottom: 4px;
}

.donut-and-legend-row {
  display: flex;
  align-items: center;
  gap: 10px;
}

.donut-svg {
  width: 38px;
  height: 38px;
  transform: rotate(-90deg);
}

.donut-legend-col {
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.donut-legend-item {
  display: flex;
  align-items: center;
  gap: 6px;
  font-size: 8px;
  color: var(--text-secondary);
}

.legend-color-box {
  width: 7px;
  height: 7px;
  border-radius: 2px;
}

.col-confirmed { background: #10b981; }
.col-pending { background: #f59e0b; }
.col-cancelled { background: #ef4444; }
</style>
