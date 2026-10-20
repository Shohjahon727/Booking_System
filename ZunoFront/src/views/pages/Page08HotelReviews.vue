<template>
  <div class="page-container page-08-reviews">
    <div class="content-container">
      <!-- Screen 08 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 08</span>
        <span class="s-title">SHARHLAR (HOTEL EGASI)</span>
        <span class="s-role">Reyting va sharhlar tahlili</span>
      </div>

      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Mijozlar Sharhlari va Baholari</h2>
          <p>Mehmonlar tomonidan qoldirilgan fikr-mulohazalar va reyting dinamikasi</p>
        </div>
        <div class="page-actions-box">
          <span class="status-badge status-confirmed">★ 4.6 (120 ta baho)</span>
        </div>
      </div>

      <!-- Rating Breakdown Box (From Mockup 08) -->
      <div class="card rating-summary-big-card">
        <div class="score-display-column">
          <div class="big-score">4.6</div>
          <div class="stars-gold-lg">★★★★★</div>
          <span class="baho-count-lbl">120 ta sharh</span>
        </div>

        <div class="progress-bars-column">
          <div v-for="b in state.ratingsBreakdown" :key="b.star" class="star-rating-row">
            <span class="star-lbl">{{ b.star }} yulduz</span>
            <div class="bar-track">
              <div class="bar-fill" :style="{ width: b.percent + '%' }"></div>
            </div>
            <span class="count-tag">{{ b.count }} ta ({{ b.percent }}%)</span>
          </div>
        </div>
      </div>

      <!-- Reviews Feed List -->
      <div class="reviews-feed-container">
        <div
          v-for="rev in state.reviews"
          :key="rev.id"
          class="card review-item-card"
        >
          <div class="review-top-meta">
            <div class="user-avatar-initials">{{ rev.avatarText }}</div>
            <div>
              <h4 class="user-display-name">{{ rev.author }}</h4>
              <span class="review-post-date">📅 {{ rev.date }}</span>
            </div>
            <div class="review-gold-stars">
              {{ '★'.repeat(rev.rating) }}{{ '☆'.repeat(5 - rev.rating) }}
            </div>
          </div>

          <p class="review-body-paragraph">{{ rev.comment }}</p>

          <div class="review-reply-strip">
            <input
              type="text"
              placeholder="Mehmonxona ma'muriyati nomidan javob yozish..."
              class="form-input reply-box-field"
              @keyup.enter="handleReply"
            />
            <button class="btn-primary" @click="handleReply">
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

function handleReply() {
  alert("Javobingiz muvaffaqiyatli saqlandi va e'lon qilindi!")
}
</script>

<style scoped>
.page-08-reviews {
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

/* Rating summary card */
.rating-summary-big-card {
  display: flex;
  align-items: center;
  gap: 32px;
  padding: 24px;
  margin-bottom: 24px;
  flex-direction: row;
}

.score-display-column {
  text-align: center;
  border-right: 1px solid var(--border-light);
  padding-right: 32px;
}

.big-score {
  font-size: 44px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1;
}

.stars-gold-lg {
  color: #f59e0b;
  font-size: 16px;
  margin: 4px 0;
}

.baho-count-lbl {
  font-size: 12px;
  color: var(--text-muted);
}

.progress-bars-column {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.star-rating-row {
  display: flex;
  align-items: center;
  gap: 12px;
  font-size: 12.5px;
  color: var(--text-secondary);
}

.star-lbl {
  width: 70px;
  font-weight: 600;
}

.bar-track {
  flex: 1;
  height: 6px;
  background: var(--bg-subtle);
  border-radius: 3px;
  overflow: hidden;
}

.bar-fill {
  height: 100%;
  background: #f59e0b;
  border-radius: 3px;
}

.count-tag {
  width: 90px;
  text-align: right;
  font-size: 11.5px;
  color: var(--text-muted);
}

/* Reviews Feed */
.reviews-feed-container {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.review-item-card {
  padding: 18px;
}

.review-top-meta {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 10px;
}

.user-avatar-initials {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  background: var(--primary);
  color: #fff;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 13px;
}

.user-display-name {
  font-size: 14px;
  font-weight: 800;
  color: var(--text-main);
}

.review-post-date {
  font-size: 11px;
  color: var(--text-muted);
}

.review-gold-stars {
  margin-left: auto;
  color: #f59e0b;
  font-size: 14px;
}

.review-body-paragraph {
  font-size: 13px;
  color: var(--text-secondary);
  line-height: 1.5;
  margin-bottom: 14px;
}

.review-reply-strip {
  display: flex;
  gap: 10px;
  border-top: 1px solid var(--border-light);
  padding-top: 12px;
}

.reply-box-field {
  flex: 1;
}

@media (max-width: 768px) {
  .rating-summary-big-card {
    flex-direction: column;
    align-items: stretch;
  }
  .score-display-column {
    border-right: none;
    border-bottom: 1px solid var(--border-light);
    padding-bottom: 16px;
    padding-right: 0;
  }
}
</style>
