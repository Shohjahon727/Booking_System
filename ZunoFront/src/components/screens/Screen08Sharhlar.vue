<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">08</span> SHARHLAR (HOTEL EGASI)
      </span>
      <span class="card-badge">★ 4.6 (120 ta)</span>
    </div>

    <!-- Rating Summary & Progress Bars -->
    <div class="ratings-overview-box">
      <div class="rating-big-score-col">
        <div class="score-number">4.6</div>
        <div class="stars-gold-big">★★★★★</div>
        <div class="total-reviews-lbl">120 ta baho</div>
      </div>

      <div class="ratings-bars-col">
        <div v-for="b in state.ratingsBreakdown" :key="b.star" class="star-progress-row">
          <span class="star-num">{{ b.star }}★</span>
          <div class="progress-track">
            <div class="progress-fill" :style="{ width: b.percent + '%' }"></div>
          </div>
          <span class="star-count-txt">{{ b.count }}</span>
        </div>
      </div>
    </div>

    <!-- Reviews List -->
    <div class="reviews-items-container">
      <div v-for="rev in state.reviews" :key="rev.id" class="review-comment-card">
        <div class="rev-header">
          <div class="rev-author-info">
            <div class="rev-avatar">{{ rev.avatarText }}</div>
            <div>
              <div class="rev-author-name">{{ rev.author }}</div>
              <div class="rev-date-txt">{{ rev.date }}</div>
            </div>
          </div>
          <div class="rev-stars">
            {{ '★'.repeat(rev.rating) }}{{ '☆'.repeat(5 - rev.rating) }}
          </div>
        </div>
        <p class="rev-text">{{ rev.comment }}</p>
      </div>
    </div>

    <!-- Add Review Form Formlet -->
    <div class="add-review-mini-row">
      <input
        type="text"
        v-model="newCommentText"
        placeholder="Sharh yozish..."
        class="quick-review-input"
        @keyup.enter="handleSendReview"
      />
      <button class="btn-primary send-rev-btn" @click="handleSendReview">
        Yuborish
      </button>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { state, addReview } from '../../store'

const newCommentText = ref('')

function handleSendReview() {
  if (!newCommentText.value.trim()) return
  addReview({
    rating: 5,
    comment: newCommentText.value.trim()
  })
  newCommentText.value = ''
}
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

.ratings-overview-box {
  display: flex;
  align-items: center;
  gap: 12px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 8px 10px;
  margin-bottom: 8px;
}

.rating-big-score-col {
  text-align: center;
  border-right: 1px solid var(--border-light);
  padding-right: 10px;
}

.score-number {
  font-size: 20px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1;
}

.stars-gold-big {
  color: #f59e0b;
  font-size: 10px;
  margin: 2px 0;
}

.total-reviews-lbl {
  font-size: 8px;
  color: var(--text-muted);
}

.ratings-bars-col {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.star-progress-row {
  display: flex;
  align-items: center;
  gap: 6px;
  font-size: 8.5px;
}

.star-num {
  width: 14px;
  color: var(--text-muted);
  font-weight: 700;
}

.progress-track {
  flex: 1;
  height: 4px;
  background: #cbd5e1;
  border-radius: 2px;
  overflow: hidden;
}

html.dark-theme .progress-track {
  background: #334155;
}

.progress-fill {
  height: 100%;
  background: #f59e0b;
  border-radius: 2px;
}

.star-count-txt {
  width: 16px;
  text-align: right;
  color: var(--text-muted);
}

.reviews-items-container {
  display: flex;
  flex-direction: column;
  gap: 6px;
  flex: 1;
}

.review-comment-card {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
}

.rev-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 3px;
}

.rev-author-info {
  display: flex;
  align-items: center;
  gap: 6px;
}

.rev-avatar {
  width: 20px;
  height: 20px;
  border-radius: 50%;
  background: var(--primary);
  color: #fff;
  font-size: 8.5px;
  font-weight: 700;
  display: flex;
  align-items: center;
  justify-content: center;
}

.rev-author-name {
  font-size: 9.5px;
  font-weight: 700;
  color: var(--text-main);
  line-height: 1.1;
}

.rev-date-txt {
  font-size: 7.5px;
  color: var(--text-muted);
}

.rev-stars {
  color: #f59e0b;
  font-size: 9px;
}

.rev-text {
  font-size: 9px;
  color: var(--text-secondary);
  line-height: 1.3;
}

.add-review-mini-row {
  display: flex;
  gap: 6px;
  margin-top: 8px;
}

.quick-review-input {
  flex: 1;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 4px 8px;
  border-radius: 4px;
  font-size: 9.5px;
  outline: none;
  color: var(--text-main);
}

.send-rev-btn {
  padding: 4px 10px;
  font-size: 9px;
}
</style>
