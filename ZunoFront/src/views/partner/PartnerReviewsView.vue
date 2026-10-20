<template>
  <div class="dashboard-layout">
    <div class="dashboard-sidebar-wrap">
      <PartnerSidebar />
    </div>

    <div class="dashboard-main-area">
      <div class="page-top-bar">
        <div class="page-title-box">
          <h2>Mijozlar Sharhlari va Reyting Tahlili</h2>
          <p>Mehmonlar qoldirgan baholar va ularga rasmiy javob qaytarish</p>
        </div>
        <div class="page-actions-box">
          <span class="status-badge status-confirmed">★ 4.8 A'lo daraja</span>
        </div>
      </div>

      <!-- Ratings Breakdown Card -->
      <div class="card ratings-breakdown-card">
        <div class="score-summary-side">
          <div class="big-rating-number">4.8</div>
          <div class="stars-gold">★★★★★</div>
          <div class="total-reviews-count">120 ta tasdiqlangan sharh</div>
        </div>

        <div class="progress-bars-side">
          <div v-for="b in state.ratingsBreakdown" :key="b.star" class="progress-row-item">
            <span class="star-label">{{ b.star }} yulduz</span>
            <div class="progress-bar-track">
              <div class="progress-bar-fill" :style="{ width: b.percent + '%' }"></div>
            </div>
            <span class="star-percent">{{ b.count }} ta ({{ b.percent }}%)</span>
          </div>
        </div>
      </div>

      <!-- Reviews List -->
      <div class="reviews-management-column">
        <div v-for="rev in state.reviews" :key="rev.id" class="card single-review-manage-card">
          <div class="review-head-line">
            <div class="user-meta-left">
              <div class="u-avatar-badge">{{ rev.avatarText }}</div>
              <div>
                <h4 class="u-full-name">{{ rev.author }}</h4>
                <span class="u-date-txt">📅 {{ rev.date }}</span>
              </div>
            </div>

            <div class="review-stars-right">
              {{ '★'.repeat(rev.rating) }}{{ '☆'.repeat(5 - rev.rating) }}
            </div>
          </div>

          <p class="review-body-text">{{ rev.comment }}</p>

          <div class="review-reply-section">
            <input
              type="text"
              placeholder="Mehmonxona nomidan javob qaytarish..."
              class="form-input reply-field"
              @keyup.enter="handleReply"
            />
            <button class="btn-primary btn-reply" @click="handleReply">
              Javob berish
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { state } from '../../store'
import PartnerSidebar from '../../components/layout/PartnerSidebar.vue'

function handleReply() {
  alert("Javobingiz muvaffaqiyatli chop etildi!")
}
</script>

<style scoped>
.ratings-breakdown-card {
  display: flex;
  align-items: center;
  gap: 30px;
  flex-direction: row;
}

.score-summary-side {
  text-align: center;
  border-right: 1px solid var(--border-light);
  padding-right: 30px;
}

.big-rating-number {
  font-size: 38px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1;
}

.stars-gold {
  color: #f59e0b;
  font-size: 14px;
  margin: 4px 0;
}

.total-reviews-count {
  font-size: 11px;
  color: var(--text-muted);
}

.progress-bars-side {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.progress-row-item {
  display: flex;
  align-items: center;
  gap: 12px;
  font-size: 12px;
  color: var(--text-secondary);
}

.star-label {
  width: 60px;
  font-weight: 600;
}

.progress-bar-track {
  flex: 1;
  height: 6px;
  background: var(--bg-subtle);
  border-radius: 3px;
  overflow: hidden;
}

.progress-bar-fill {
  height: 100%;
  background: #f59e0b;
  border-radius: 3px;
}

.star-percent {
  width: 90px;
  text-align: right;
  font-size: 11px;
  color: var(--text-muted);
}

.reviews-management-column {
  display: flex;
  flex-direction: column;
  gap: 14px;
}

.review-head-line {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 10px;
}

.user-meta-left {
  display: flex;
  align-items: center;
  gap: 10px;
}

.u-avatar-badge {
  width: 34px;
  height: 34px;
  border-radius: 50%;
  background: var(--primary);
  color: #fff;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
}

.u-full-name {
  font-size: 13.5px;
  font-weight: 800;
  color: var(--text-main);
}

.u-date-txt {
  font-size: 10.5px;
  color: var(--text-muted);
}

.review-stars-right {
  color: #f59e0b;
  font-size: 13px;
}

.review-body-text {
  font-size: 12.5px;
  color: var(--text-secondary);
  line-height: 1.4;
  margin-bottom: 14px;
}

.review-reply-section {
  display: flex;
  gap: 10px;
  border-top: 1px solid var(--border-light);
  padding-top: 10px;
}

.reply-field {
  flex: 1;
}

.btn-reply {
  padding: 6px 14px;
  font-size: 11.5px;
}

@media (max-width: 768px) {
  .ratings-breakdown-card {
    flex-direction: column;
    align-items: stretch;
  }
  .score-summary-side {
    border-right: none;
    border-bottom: 1px solid var(--border-light);
    padding-bottom: 14px;
    padding-right: 0;
  }
}
</style>
