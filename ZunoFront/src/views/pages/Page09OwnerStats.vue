<template>
  <div class="page-container page-09-owner-stats">
    <div class="content-container">
      <!-- Screen 09 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 09</span>
        <span class="s-title">STATISTIKA EGASI (Hotel Egasi)</span>
        <span class="s-role">May 2025 hisoboti</span>
      </div>

      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Mehmonxona Moliyaviy va Operatsion Statistikasi</h2>
          <p>Oylik tushumlar, o'rinlar bandligi va bronlar holati tahliliy diagrammasi</p>
        </div>
        <div class="page-actions-box">
          <span class="status-badge status-confirmed">May 2025</span>
        </div>
      </div>

      <!-- 3 Key Metric KPI Cards (From Mockup 09) -->
      <div class="kpi-cards-grid">
        <div class="card kpi-stat-card">
          <div class="kpi-top-icon">🛏️</div>
          <div class="kpi-stat-val">128</div>
          <div class="kpi-stat-lbl">Mavjud O'rinlar</div>
          <span class="trend-badge positive">+100% aktiv fond</span>
        </div>

        <div class="card kpi-stat-card">
          <div class="kpi-top-icon">👥</div>
          <div class="kpi-stat-val">256</div>
          <div class="kpi-stat-lbl">Jami Mehmonlar</div>
          <span class="trend-badge positive">+18% oylik o'sish</span>
        </div>

        <div class="card kpi-stat-card">
          <div class="kpi-top-icon">💰</div>
          <div class="kpi-stat-val green-val">24,500,000 so'm</div>
          <div class="kpi-stat-lbl">Jami Daromad (May)</div>
          <span class="trend-badge positive">+34% o'tgan oyga nisbatan</span>
        </div>
      </div>

      <!-- Charts: Line Graph & Donut -->
      <div class="charts-two-columns">
        <!-- 1. Line Chart: Daromad grafigi (so'm) -->
        <div class="card chart-line-card">
          <div class="card-header">
            <span class="card-title">Daromad grafigi (so'm)</span>
            <span class="card-badge">May 2025 oylik dinamika</span>
          </div>

          <div class="chart-canvas-box">
            <svg viewBox="0 0 500 160" class="svg-line-graph">
              <defs>
                <linearGradient id="ownerLineGrad" x1="0%" y1="0%" x2="0%" y2="100%">
                  <stop offset="0%" stop-color="#38bdf8" stop-opacity="0.35"/>
                  <stop offset="100%" stop-color="#38bdf8" stop-opacity="0"/>
                </linearGradient>
              </defs>
              <line x1="0" y1="40" x2="500" y2="40" stroke="currentColor" stroke-opacity="0.08" />
              <line x1="0" y1="80" x2="500" y2="80" stroke="currentColor" stroke-opacity="0.08" />
              <line x1="0" y1="120" x2="500" y2="120" stroke="currentColor" stroke-opacity="0.08" />

              <polygon fill="url(#ownerLineGrad)" points="0,140 50,120 100,125 150,90 200,105 250,65 300,75 350,45 400,55 450,25 500,15 500,160 0,160" />

              <polyline fill="none" stroke="#2563eb" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"
                points="0,140 50,120 100,125 150,90 200,105 250,65 300,75 350,45 400,55 450,25 500,15" />

              <circle cx="250" cy="65" r="4.5" fill="#2563eb" />
              <circle cx="500" cy="15" r="5" fill="#10b981" />
            </svg>
            <div class="chart-axis-days-row">
              <span>01 May</span>
              <span>07 May</span>
              <span>14 May</span>
              <span>21 May</span>
              <span>28 May</span>
              <span>31 May</span>
            </div>
          </div>
        </div>

        <!-- 2. Donut Chart: Bronlar holati -->
        <div class="card donut-distribution-card">
          <div class="card-header">
            <span class="card-title">Bronlar Holati</span>
            <span class="card-badge">Taqsimot</span>
          </div>

          <div class="donut-chart-flex">
            <svg viewBox="0 0 42 42" class="big-donut-svg">
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#ef4444" stroke-width="5"
                stroke-dasharray="15 85" stroke-dashoffset="0"></circle>
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#f59e0b" stroke-width="5"
                stroke-dasharray="15 85" stroke-dashoffset="-15"></circle>
              <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#10b981" stroke-width="5"
                stroke-dasharray="70 30" stroke-dashoffset="-30"></circle>
            </svg>

            <div class="donut-legends-col">
              <div class="leg-item">
                <span class="leg-box col-conf"></span>
                <span>Confirmed (<b>70%</b>)</span>
              </div>
              <div class="leg-item">
                <span class="leg-box col-pend"></span>
                <span>Pending (<b>15%</b>)</span>
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
</script>

<style scoped>
.page-09-owner-stats {
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
  color: #d97706;
  font-weight: 600;
}

.kpi-cards-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 20px;
  margin-bottom: 24px;
}

.kpi-stat-card {
  padding: 22px;
}

.kpi-top-icon {
  font-size: 24px;
  margin-bottom: 10px;
}

.kpi-stat-val {
  font-size: 24px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1.1;
}

.green-val {
  color: var(--success);
  font-size: 20px;
}

.kpi-stat-lbl {
  font-size: 12px;
  color: var(--text-muted);
  margin-top: 4px;
}

.trend-badge {
  font-size: 11px;
  font-weight: 700;
  margin-top: 10px;
  display: inline-block;
}

.trend-badge.positive {
  color: var(--success);
}

.charts-two-columns {
  display: grid;
  grid-template-columns: 1.7fr 1fr;
  gap: 20px;
}

.svg-line-graph {
  width: 100%;
  height: 150px;
  display: block;
}

.chart-axis-days-row {
  display: flex;
  justify-content: space-between;
  font-size: 10.5px;
  color: var(--text-muted);
  margin-top: 8px;
}

.donut-chart-flex {
  display: flex;
  align-items: center;
  justify-content: space-around;
  padding: 20px 0;
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
  .kpi-cards-grid {
    grid-template-columns: 1fr;
  }
  .charts-two-columns {
    grid-template-columns: 1fr;
  }
}
</style>
