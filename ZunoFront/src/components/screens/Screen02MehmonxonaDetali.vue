<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">02</span> MEHMONXONA DETALI
      </span>
      <div class="header-action-icons">
        <button
          class="icon-tiny-btn"
          :class="{ active: selectedHotel.isFavorite }"
          @click="toggleFavorite(selectedHotel.id)"
          title="Sevimlilar"
        >
          {{ selectedHotel.isFavorite ? '❤️' : '🤍' }}
        </button>
        <button class="icon-tiny-btn" @click="handleShare" title="Ulashish">
          🔗
        </button>
      </div>
    </div>

    <!-- Photo Gallery Grid -->
    <div class="detail-gallery-grid">
      <div class="gallery-hero-image">
        <img :src="selectedHotel.gallery[0]" :alt="selectedHotel.name" />
        <span class="verified-tag">✓ Tasdiqlangan</span>
      </div>
      <div class="gallery-side-thumbs">
        <div class="gallery-mini-thumb">
          <img :src="selectedHotel.gallery[1]" alt="Thumb 1" />
        </div>
        <div class="gallery-mini-thumb">
          <img :src="selectedHotel.gallery[2]" alt="Thumb 2" />
          <div class="gallery-overlay-badge">+12 Rasm</div>
        </div>
      </div>
    </div>

    <!-- Hotel Info -->
    <div class="hotel-info-section">
      <div class="hotel-title-line">
        <h3 class="hotel-detail-name">{{ selectedHotel.name }}</h3>
        <div class="hotel-stars-box">
          <span class="stars-gold">★★★★★</span>
          <span class="reviews-count">({{ selectedHotel.reviewsCount }} ta sharh)</span>
        </div>
      </div>
      <div class="hotel-detail-location">
        📍 {{ selectedHotel.address }}
      </div>
    </div>

    <!-- Facilities Chips -->
    <div class="amenities-chips-row">
      <span v-for="amenity in selectedHotel.amenities" :key="amenity.name" class="amenity-chip">
        <span class="amenity-icon">{{ getAmenityIcon(amenity.icon) }}</span>
        <span>{{ amenity.name }}</span>
      </span>
    </div>

    <!-- Detail Navigation Tabs -->
    <div class="detail-tabs-nav">
      <button
        v-for="tab in tabs"
        :key="tab"
        class="detail-tab-btn"
        :class="{ active: activeTab === tab }"
        @click="activeTab = tab"
      >
        {{ tab }}
      </button>
    </div>

    <!-- Tab Content -->
    <div class="tab-body-content">
      <p v-if="activeTab === 'Tavsif'" class="desc-paragraph">
        {{ selectedHotel.description }}
      </p>
      <div v-else-if="activeTab === 'Xonalar'" class="rooms-preview-list">
        <span>4 toifadagi xonalar mavjud (Deluxe, Suite, Standard, Twin)</span>
      </div>
      <div v-else-if="activeTab === 'Qulayliklar'" class="facilities-preview">
        <span>Bepul transfer, 24/7 xizmat, fitness zal va SPA markazi</span>
      </div>
      <div v-else-if="activeTab === 'Sharhlar'" class="reviews-preview">
        <span>Mehmonlar tomonidan o'rtacha 4.8 ball baholangan</span>
      </div>
      <div v-else-if="activeTab === 'Joylashuv'" class="location-preview">
        <span>Markaziy metro bekatiga 500 metr, Aeroportga 15 daqiqa</span>
      </div>
    </div>

    <!-- Price and Action Box -->
    <div class="price-action-box">
      <div class="price-label-col">
        <span class="price-muted-label">Boshlang'ich narx:</span>
        <div class="price-big-sum">
          dan {{ formatSum(selectedHotel.pricePerNight) }} so'm <span class="per-night">/ 1 kecha</span>
        </div>
      </div>
      <button class="btn-primary view-rooms-action" @click="scrollToRooms">
        Xonalarni ko'rish
      </button>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { state, selectedHotel, toggleFavorite, setActiveView } from '../../store'

const tabs = ['Tavsif', 'Xonalar', 'Qulayliklar', 'Sharhlar', 'Joylashuv']
const activeTab = ref('Tavsif')

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function getAmenityIcon(type) {
  switch (type) {
    case 'wifi': return '📶'
    case 'waves': return '🏊'
    case 'car': return '🅿️'
    case 'utensils': return '🍽️'
    case 'sparkles': return '💆'
    case 'dumbbell': return '🏋️'
    case 'coffee': return '☕'
    case 'wind': return '❄️'
    default: return '✨'
  }
}

function handleShare() {
  navigator.clipboard?.writeText(window.location.href)
  alert("Mehmonxona havolasi nusxalandi!")
}

function scrollToRooms() {
  setActiveView('03')
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

.header-action-icons {
  display: flex;
  gap: 6px;
}

.icon-tiny-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 24px;
  height: 24px;
  border-radius: 4px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  font-size: 11px;
}

/* Gallery */
.detail-gallery-grid {
  display: grid;
  grid-template-columns: 2fr 1fr;
  gap: 6px;
  height: 115px;
  margin-bottom: 8px;
}

.gallery-hero-image {
  position: relative;
  border-radius: var(--radius-sm);
  overflow: hidden;
  background: #1e293b;
}

.gallery-hero-image img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.verified-tag {
  position: absolute;
  top: 6px;
  left: 6px;
  background: rgba(16, 185, 129, 0.9);
  color: #fff;
  font-size: 8.5px;
  font-weight: 700;
  padding: 2px 6px;
  border-radius: 3px;
}

.gallery-side-thumbs {
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.gallery-mini-thumb {
  flex: 1;
  position: relative;
  border-radius: var(--radius-sm);
  overflow: hidden;
  background: #1e293b;
}

.gallery-mini-thumb img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.gallery-overlay-badge {
  position: absolute;
  inset: 0;
  background: rgba(15, 23, 42, 0.75);
  color: #38bdf8;
  font-weight: 700;
  font-size: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

/* Info */
.hotel-info-section {
  margin-bottom: 6px;
}

.hotel-title-line {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.hotel-detail-name {
  font-size: 12px;
  font-weight: 800;
  color: var(--text-main);
}

.stars-gold {
  color: #f59e0b;
  font-size: 10px;
}

.reviews-count {
  font-size: 9px;
  color: var(--text-muted);
  margin-left: 4px;
}

.hotel-detail-location {
  font-size: 9.5px;
  color: var(--text-muted);
  margin-top: 2px;
}

/* Amenities */
.amenities-chips-row {
  display: flex;
  flex-wrap: wrap;
  gap: 4px;
  margin-bottom: 8px;
}

.amenity-chip {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 2px 6px;
  border-radius: 4px;
  font-size: 9px;
  color: var(--text-secondary);
  display: inline-flex;
  align-items: center;
  gap: 4px;
}

/* Tabs */
.detail-tabs-nav {
  display: flex;
  gap: 4px;
  border-bottom: 1px solid var(--border-light);
  margin-bottom: 6px;
}

.detail-tab-btn {
  background: transparent;
  border: none;
  font-size: 9.5px;
  font-weight: 600;
  color: var(--text-muted);
  padding: 3px 6px;
  cursor: pointer;
  border-bottom: 2px solid transparent;
}

.detail-tab-btn.active {
  color: var(--primary);
  border-color: var(--primary);
  font-weight: 700;
}

.tab-body-content {
  min-height: 38px;
  margin-bottom: 8px;
  font-size: 9.5px;
  color: var(--text-secondary);
  line-height: 1.35;
}

/* Price Box */
.price-action-box {
  margin-top: auto;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 8px 10px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.price-muted-label {
  font-size: 8.5px;
  color: var(--text-muted);
}

.price-big-sum {
  font-size: 11.5px;
  font-weight: 800;
  color: var(--primary);
}

.per-night {
  font-size: 8.5px;
  color: var(--text-muted);
  font-weight: normal;
}

.view-rooms-action {
  padding: 6px 12px;
  font-size: 10px;
}
</style>
