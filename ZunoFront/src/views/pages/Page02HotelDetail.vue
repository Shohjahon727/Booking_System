<template>
  <div class="page-container page-02-hotel-detail">
    <div class="content-container">
      <!-- Screen 02 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 02</span>
        <span class="s-title">MEHMONXONA DETALI</span>
        <span class="s-role">Grand Plaza Hotel</span>
      </div>

      <!-- Hotel Title Bar -->
      <div class="hotel-title-banner">
        <div>
          <div class="title-with-verified">
            <h1>{{ currentHotel.name }}</h1>
            <span class="verified-tag">✓ Tasdiqlangan (Verified)</span>
          </div>
          <div class="hotel-subtitle-meta">
            <span class="stars-gold">★★★★★</span>
            <span class="rating-text">★ {{ currentHotel.rating }} ({{ currentHotel.reviewsCount }} ta sharh)</span>
            <span class="dot-sep">•</span>
            <span class="address-text">📍 {{ currentHotel.address }}</span>
          </div>
        </div>

        <div class="action-buttons-wrap">
          <button
            class="btn-secondary"
            :class="{ active: currentHotel.isFavorite }"
            @click="toggleFavorite(currentHotel.id)"
          >
            {{ currentHotel.isFavorite ? '❤️ Saqlangan' : '🤍 Sevimlilarga qo\'shish' }}
          </button>
          <button class="btn-secondary" @click="handleShare">🔗 Ulashish</button>
        </div>
      </div>

      <!-- Gallery Showcase (From Mockup 02) -->
      <div class="hotel-gallery-container">
        <div class="gallery-hero-photo">
          <img :src="currentHotel.gallery[0]" :alt="currentHotel.name" />
          <span class="hero-label-badge">Asosiy Rasm (Hero)</span>
        </div>

        <div class="gallery-thumbs-column">
          <div v-for="(img, idx) in currentHotel.gallery.slice(1, 3)" :key="idx" class="gallery-thumb-item">
            <img :src="img" :alt="'Photo ' + idx" />
          </div>
        </div>

        <div class="gallery-thumbs-column">
          <div class="gallery-thumb-item">
            <img :src="currentHotel.gallery[3]" alt="Photo 3" />
          </div>
          <div class="gallery-thumb-item with-overlay">
            <img :src="currentHotel.gallery[4]" alt="Photo 4" />
            <div class="gallery-more-overlay">+12 Rasm</div>
          </div>
        </div>
      </div>

      <!-- Amenities Chips Row (Wi-Fi, Basseyn, Parking, Restoran, Spa, Fitness) -->
      <div class="amenities-chips-container card">
        <span class="amenities-row-label">Qulayliklar:</span>
        <div class="amenities-chips-list">
          <span v-for="a in currentHotel.amenities" :key="a.name" class="amenity-chip-item">
            <span class="chip-icon">{{ getAmenityIcon(a.icon) }}</span>
            <span>{{ a.name }}</span>
          </span>
          <span class="amenity-chip-item">
            <span class="chip-icon">❄️</span>
            <span>Konditsioner</span>
          </span>
          <span class="amenity-chip-item">
            <span class="chip-icon">🍳</span>
            <span>Nonushta</span>
          </span>
        </div>
      </div>

      <!-- Tabs & Content Layout -->
      <div class="detail-content-grid">
        <div class="detail-tabs-main">
          <!-- Navigation Tabs Bar -->
          <div class="detail-tabs-row">
            <button
              v-for="t in ['Tavsif', 'Xonalar', 'Qulayliklar', 'Sharhlar', 'Joylashuv']"
              :key="t"
              class="tab-btn-lg"
              :class="{ active: currentTab === t }"
              @click="currentTab = t"
            >
              {{ t }}
            </button>
          </div>

          <!-- Tab Content 1: Tavsif -->
          <div v-if="currentTab === 'Tavsif'" class="card tab-pane-card">
            <h3 class="pane-title">Mehmonxona haqida tavsif</h3>
            <p class="pane-paragraph">{{ currentHotel.description }}</p>
            <div class="highlight-boxes-grid">
              <div class="h-box">
                <span class="h-icon">⭐</span>
                <h5>Yuqori xizmat darajasi</h5>
                <p>24/7 kutib olish xizmati va malakali xodimlar.</p>
              </div>
              <div class="h-box">
                <span class="h-icon">🚇</span>
                <h5>Qulay logistika</h5>
                <p>Metro bekati, aeroport va temir yo'l vokzali yaqinida.</p>
              </div>
            </div>
          </div>

          <!-- Tab Content 2: Xonalar (Link to Screen 03) -->
          <div v-else-if="currentTab === 'Xonalar'" class="card tab-pane-card">
            <h3 class="pane-title">Mavjud xonalar toifalari</h3>
            <p class="pane-paragraph">O'zingizga ma'qul xona turini tanlang va darhol bron qiling:</p>
            <div class="rooms-preview-grid">
              <div v-for="r in state.rooms" :key="r.id" class="room-preview-item">
                <img :src="r.image" :alt="r.name" class="r-thumb" />
                <div class="r-info">
                  <h4>{{ r.name }}</h4>
                  <span>👥 {{ r.capacity }} kishi • 📐 {{ r.size }}</span>
                  <div class="r-price">{{ formatSum(r.pricePerNight) }} so'm / kecha</div>
                </div>
                <router-link to="/rooms" class="btn-primary" @click="selectRoom(r.id)">
                  Tanlash &rarr;
                </router-link>
              </div>
            </div>
          </div>

          <!-- Tab Content 3: Qulayliklar -->
          <div v-else-if="currentTab === 'Qulayliklar'" class="card tab-pane-card">
            <h3 class="pane-title">Mehmonxona qulayliklari</h3>
            <div class="full-amenities-grid">
              <div class="amenity-card">📶 Bepul yuqori tezlikdagi Wi-Fi</div>
              <div class="amenity-card">🏊 Ochiq va yopiq suzish havzasi</div>
              <div class="amenity-card">🅿️ Qo'riqlanadigan bepul avtoturargoh</div>
              <div class="amenity-card">🍽️ Restoran va milliy taomlar</div>
              <div class="amenity-card">💆 Fin saunasi va SPA massaj xizmati</div>
              <div class="amenity-card">🏋️ Fitness zal va trenajyorlar</div>
            </div>
          </div>

          <!-- Tab Content 4: Sharhlar -->
          <div v-else-if="currentTab === 'Sharhlar'" class="card tab-pane-card">
            <h3 class="pane-title">Mijozlar sharhlari (120 ta baho)</h3>
            <div class="reviews-items-list">
              <div v-for="rev in state.reviews" :key="rev.id" class="rev-card">
                <div class="rev-top">
                  <b>{{ rev.author }}</b>
                  <span class="stars-gold">★★★★★</span>
                </div>
                <p>{{ rev.comment }}</p>
                <small class="rev-date">{{ rev.date }}</small>
              </div>
            </div>
          </div>

          <!-- Tab Content 5: Joylashuv -->
          <div v-else-if="currentTab === 'Joylashuv'" class="card tab-pane-card">
            <h3 class="pane-title">Joylashuv va hudud</h3>
            <p>📍 {{ currentHotel.address }}</p>
            <div class="map-placeholder">
              <span>🗺️ Interaktiv xaritada joylashuv ko'rinishi</span>
            </div>
          </div>
        </div>

        <!-- Right Side: Booking Action Card -->
        <aside class="detail-sidebar-action">
          <div class="card booking-action-box">
            <span class="action-box-lbl">Boshlang'ich narx:</span>
            <div class="action-box-price">
              dan {{ formatSum(currentHotel.pricePerNight) }} so'm <small>/ 1 kecha</small>
            </div>

            <div class="action-box-dates">
              <div class="date-row">
                <span>Kirish sanasi:</span>
                <b>{{ state.checkout.checkIn }}</b>
              </div>
              <div class="date-row">
                <span>Chiqish sanasi:</span>
                <b>{{ state.checkout.checkOut }}</b>
              </div>
              <div class="date-row">
                <span>Mehmonlar soni:</span>
                <b>{{ state.checkout.guestsCount }} kishi</b>
              </div>
            </div>

            <router-link to="/rooms" class="btn-primary full-action-btn">
              🛏️ Xonalarni ko'rish &rarr;
            </router-link>

            <router-link to="/booking" class="btn-secondary full-action-btn" style="margin-top: 8px;">
              ⚡ Tezkor bron qilish
            </router-link>

            <div class="guarantee-note">
              🔒 Qaytarilishi kafolatlangan to'lov tizimi
            </div>
          </div>
        </aside>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRoute } from 'vue-router'
import { state, selectRoom, toggleFavorite } from '../../store'

const route = useRoute()
const currentTab = ref('Tavsif')

const currentHotel = computed(() => {
  const id = Number(route.params.id) || 1
  return state.hotels.find(h => h.id === id) || state.hotels[0]
})

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function getAmenityIcon(icon) {
  switch (icon) {
    case 'wifi': return '📶'
    case 'waves': return '🏊'
    case 'car': return '🅿️'
    case 'utensils': return '🍽️'
    case 'sparkles': return '💆'
    case 'dumbbell': return '🏋️'
    default: return '✨'
  }
}

function handleShare() {
  navigator.clipboard?.writeText(window.location.href)
  alert("Havola nusxalandi!")
}
</script>

<style scoped>
.page-02-hotel-detail {
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
  color: var(--primary);
  font-weight: 600;
}

/* Title banner */
.hotel-title-banner {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 20px;
}

.title-with-verified {
  display: flex;
  align-items: center;
  gap: 12px;
}

.title-with-verified h1 {
  font-size: 26px;
  font-weight: 800;
  color: var(--text-main);
  letter-spacing: -0.4px;
}

.verified-tag {
  background: rgba(16, 185, 129, 0.15);
  color: var(--success);
  border: 1px solid rgba(16, 185, 129, 0.3);
  font-size: 10px;
  font-weight: 800;
  padding: 3px 8px;
  border-radius: var(--radius-sm);
}

.hotel-subtitle-meta {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-top: 4px;
  font-size: 12.5px;
}

.stars-gold {
  color: #f59e0b;
}

.rating-text {
  font-weight: 700;
  color: #f59e0b;
}

.dot-sep {
  color: var(--text-light);
}

.address-text {
  color: var(--text-muted);
}

.action-buttons-wrap {
  display: flex;
  gap: 8px;
}

/* Gallery Showcase */
.hotel-gallery-container {
  display: grid;
  grid-template-columns: 2.2fr 1fr 1fr;
  gap: 12px;
  height: 320px;
  border-radius: var(--radius-md);
  overflow: hidden;
  margin-bottom: 20px;
}

.gallery-hero-photo {
  position: relative;
  background: #1e293b;
}

.gallery-hero-photo img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.hero-label-badge {
  position: absolute;
  top: 10px;
  left: 10px;
  background: rgba(15, 23, 42, 0.8);
  color: #38bdf8;
  font-size: 10px;
  font-weight: 700;
  padding: 3px 8px;
  border-radius: 4px;
}

.gallery-thumbs-column {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.gallery-thumb-item {
  flex: 1;
  position: relative;
  background: #1e293b;
  overflow: hidden;
}

.gallery-thumb-item img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.2s;
}

.gallery-thumb-item:hover img {
  transform: scale(1.05);
}

.gallery-thumb-item.with-overlay {
  position: relative;
}

.gallery-more-overlay {
  position: absolute;
  inset: 0;
  background: rgba(15, 23, 42, 0.75);
  color: #38bdf8;
  font-size: 13px;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

/* Amenities Chips */
.amenities-chips-container {
  display: flex;
  align-items: center;
  gap: 16px;
  padding: 12px 20px;
  margin-bottom: 24px;
}

.amenities-row-label {
  font-size: 11px;
  font-weight: 800;
  text-transform: uppercase;
  color: var(--text-muted);
}

.amenities-chips-list {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
}

.amenity-chip-item {
  display: flex;
  align-items: center;
  gap: 6px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 4px 10px;
  border-radius: var(--radius-sm);
  font-size: 11.5px;
  color: var(--text-main);
  font-weight: 600;
}

/* Tabs & Content */
.detail-content-grid {
  display: grid;
  grid-template-columns: 1fr 340px;
  gap: 24px;
}

.detail-tabs-row {
  display: flex;
  gap: 8px;
  border-bottom: 2px solid var(--border-light);
  margin-bottom: 20px;
}

.tab-btn-lg {
  background: transparent;
  border: none;
  font-size: 13.5px;
  font-weight: 700;
  color: var(--text-muted);
  padding: 10px 18px;
  cursor: pointer;
  border-bottom: 2px solid transparent;
  margin-bottom: -2px;
  transition: all 0.15s;
}

.tab-btn-lg.active {
  color: var(--primary);
  border-color: var(--primary);
}

.pane-title {
  font-size: 16px;
  font-weight: 800;
  color: var(--text-main);
  margin-bottom: 12px;
}

.pane-paragraph {
  font-size: 13px;
  color: var(--text-secondary);
  line-height: 1.5;
  margin-bottom: 20px;
}

.highlight-boxes-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 16px;
}

.h-box {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 14px;
}

.h-box h5 {
  font-size: 13px;
  font-weight: 800;
  color: var(--text-main);
  margin: 6px 0 2px 0;
}

.h-box p {
  font-size: 11.5px;
  color: var(--text-muted);
}

.rooms-preview-grid {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.room-preview-item {
  display: flex;
  align-items: center;
  gap: 14px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 10px 14px;
}

.r-thumb {
  width: 70px;
  height: 50px;
  border-radius: 4px;
  object-fit: cover;
}

.r-info {
  flex: 1;
}

.r-info h4 {
  font-size: 13.5px;
  font-weight: 800;
  color: var(--text-main);
}

.r-info span {
  font-size: 11px;
  color: var(--text-muted);
}

.r-price {
  font-size: 12px;
  font-weight: 800;
  color: var(--primary);
  margin-top: 2px;
}

.full-amenities-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
}

.amenity-card {
  background: var(--bg-subtle);
  padding: 10px 14px;
  border-radius: var(--radius-sm);
  font-size: 12.5px;
  font-weight: 600;
}

.reviews-items-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.rev-card {
  background: var(--bg-subtle);
  padding: 12px 14px;
  border-radius: var(--radius-sm);
}

.rev-top {
  display: flex;
  justify-content: space-between;
  margin-bottom: 6px;
}

.rev-date {
  font-size: 10px;
  color: var(--text-muted);
  margin-top: 6px;
  display: block;
}

.map-placeholder {
  height: 200px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  display: flex;
  align-items: center;
  justify-content: center;
  color: var(--text-muted);
  font-size: 13px;
  font-weight: 700;
}

/* Right Aside */
.booking-action-box {
  padding: 20px;
}

.action-box-lbl {
  font-size: 11px;
  color: var(--text-muted);
}

.action-box-price {
  font-size: 22px;
  font-weight: 800;
  color: var(--primary);
  margin-bottom: 16px;
}

.action-box-price small {
  font-size: 12px;
  color: var(--text-muted);
  font-weight: normal;
}

.action-box-dates {
  background: var(--bg-subtle);
  border-radius: var(--radius-sm);
  padding: 12px;
  display: flex;
  flex-direction: column;
  gap: 6px;
  margin-bottom: 18px;
}

.date-row {
  display: flex;
  justify-content: space-between;
  font-size: 11.5px;
  color: var(--text-secondary);
}

.date-row b {
  color: var(--text-main);
}

.full-action-btn {
  width: 100%;
  padding: 10px;
  font-size: 13px;
}

.guarantee-note {
  text-align: center;
  font-size: 10px;
  color: var(--text-muted);
  margin-top: 12px;
}

@media (max-width: 900px) {
  .hotel-gallery-container {
    grid-template-columns: 1fr;
    height: auto;
  }
  .detail-content-grid {
    grid-template-columns: 1fr;
  }
}
</style>
