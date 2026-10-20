<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">09</span> STATISTIKA EGASI
      </span>
      <span class="card-badge">May 2025</span>
    </div>

    <!-- 3 KPI Metric Cards -->
    <div class="kpi-three-grid">
      <div class="kpi-mini-box">
        <div class="kpi-lbl">O'rinlar</div>
        <div class="kpi-num">128</div>
      </div>
      <div class="kpi-mini-box">
        <div class="kpi-lbl">Jami mehmonlar</div>
        <div class="kpi-num">256</div>
      </div>
      <div class="kpi-mini-box">
        <div class="kpi-lbl">Jami daromad</div>
        <div class="kpi-num num-green">24,500,000 so'm</div>
      </div>
    </div>

    <!-- Line Chart: Daromad grafigi (so'm) -->
    <div class="chart-container-box">
      <div class="chart-title-line">Daromad grafigi (so'm)</div>
      <svg viewBox="0 0 320 80" class="line-chart-svg">
        <defs>
          <linearGradient id="gradOwner" x1="0%" y1="0%" x2="0%" y2="100%">
            <stop offset="0%" stop-color="#38bdf8" stop-opacity="0.35"/>
            <stop offset="100%" stop-color="#38bdf8" stop-opacity="0"/>
          </linearGradient>
        </defs>
        <!-- Grid lines -->
        <line x1="0" y1="20" x2="320" y2="20" stroke="currentColor" stroke-opacity="0.1" />
        <line x1="0" y1="50" x2="320" y2="50" stroke="currentColor" stroke-opacity="0.1" />
        <!-- Fill Area -->
        <polygon fill="url(#gradOwner)" points="0,70 20,60 50,55 90,40 130,48 170,28 210,34 250,18 290,24 320,12 320,80 0,80" />
        <!-- Stroke Line -->
        <polyline fill="none" stroke="#2563eb" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"
          points="0,70 20,60 50,55 90,40 130,48 170,28 210,34 250,18 290,24 320,12" />
        <!-- Active Points -->
        <circle cx="170" cy="28" r="3.5" fill="#2563eb" />
        <circle cx="320" cy="12" r="4" fill="#10b981" />
      </svg>
      <div class="chart-axis-labels">
        <span>01 May</span>
        <span>08 May</span>
        <span>15 May</span>
        <span>22 May</span>
        <span>31 May</span>
      </div>
    </div>

    <!-- Donut Chart & Legend: Bronlar holati -->
    <div class="status-distribution-box">
      <div class="distribution-title">Bronlar holati taqsimoti:</div>
      <div class="donut-and-legend-row">
        <!-- SVG Donut Chart -->
        <svg viewBox="0 0 42 42" class="donut-svg">
          <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#ef4444" stroke-width="5"
            stroke-dasharray="15 85" stroke-dashoffset="0"></circle>
          <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#f59e0b" stroke-width="5"
            stroke-dasharray="15 85" stroke-dashoffset="-15"></circle>
          <circle cx="21" cy="21" r="15.91549430918954" fill="transparent" stroke="#10b981" stroke-width="5"
            stroke-dasharray="70 30" stroke-dashoffset="-30"></circle>
        </svg>

        <div class="donut-legend-col">
          <div class="donut-legend-item">
            <span class="legend-color-box col-confirmed"></span>
            <span>Confirmed (70%)</span>
          </div>
          <div class="donut-legend-item">
            <span class="legend-color-box col-pending"></span>
            <span>Pending (15%)</span>
          </div>
          <div class="donut-legend-item">
            <span class="legend-color-box col-cancelled"></span>
            <span>Cancelled (15%)</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
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
  font-size: 11.5px;
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
  font-size: 9px;
  font-weight: 700;
  color: var(--text-muted);
  margin-bottom: 4px;
}

.line-chart-svg {
  width: 100%;
  height: 52px;
  display: block;
}

.chart-axis-labels {
  display: flex;
  justify-content: space-between;
  font-size: 7.5px;
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
  gap: 12px;
}

.donut-svg {
  width: 42px;
  height: 42px;
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
  font-size: 8.5px;
  color: var(--text-secondary);
}

.legend-color-box {
  width: 8px;
  height: 8px;
  border-radius: 2px;
}

.col-confirmed { background: #10b981; }
.col-pending { background: #f59e0b; }
.col-cancelled { background: #ef4444; }
</style>
