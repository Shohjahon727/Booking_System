<template>
  <div class="hotel-detail-page">
    <div class="content-container">
      <!-- Breadcrumb -->
      <nav class="breadcrumb-nav">
        <router-link to="/">Bosh sahifa</router-link> &gt;
        <router-link to="/hotels">Mehmonxonalar</router-link> &gt;
        <span>{{ currentHotel.name }}</span>
      </nav>

      <!-- Hotel Title & Actions Header -->
      <div class="hotel-main-header">
        <div class="header-titles">
          <div class="title-with-verified">
            <h1>{{ currentHotel.name }}</h1>
            <span v-if="currentHotel.verified" class="verified-tag">✓ Tasdiqlangan</span>
          </div>
          <div class="meta-location-row">
            <span class="stars-gold">★★★★★</span>
            <span class="rating-text">★ {{ currentHotel.rating }} ({{ currentHotel.reviewsCount }} ta sharh)</span>
            <span class="loc-divider">•</span>
            <span class="loc-address">📍 {{ currentHotel.address }}</span>
          </div>
        </div>

        <div class="header-action-btns">
          <button
            class="btn-secondary fav-toggle-btn"
            :class="{ active: currentHotel.isFavorite }"
            @click="toggleFavorite(currentHotel.id)"
          >
            {{ currentHotel.isFavorite ? '❤️ Sevimlilardan chiqarish' : '🤍 Sevimlilarga qo\'shish' }}
          </button>
          <button class="btn-secondary" @click="handleShare">🔗 Ulashish</button>
        </div>
      </div>

      <!-- Gallery Grid -->
      <div class="hotel-gallery-showcase">
        <div class="gallery-hero-pic">
          <img :src="currentHotel.gallery[0]" :alt="currentHotel.name" />
        </div>
        <div class="gallery-quad-grid">
          <div v-for="(img, idx) in currentHotel.gallery.slice(1, 5)" :key="idx" class="gallery-quad-pic">
            <img :src="img" :alt="'Photo ' + idx" />
          </div>
        </div>
      </div>

      <!-- Main Layout: Details Tabs/Rooms + Sticky Booking Box -->
      <div class="detail-split-layout">
        <!-- Left: Tabs & Content -->
        <div class="detail-left-column">
          <!-- Navigation Tabs -->
          <div class="detail-tabs-bar">
            <button
              v-for="t in ['Xonalar', 'Tavsif', 'Qulayliklar', 'Sharhlar', 'Joylashuv']"
              :key="t"
              class="d-tab-btn"
              :class="{ active: activeTab === t }"
              @click="activeTab = t"
            >
              {{ t }}
            </button>
          </div>

          <!-- Tab 1: Rooms Selection (Default) -->
          <section v-if="activeTab === 'Xonalar'" class="rooms-selection-section">
            <h3 class="section-sub-heading">Mavjud xonalar toifalari</h3>
            <div class="rooms-detail-cards-list">
              <div
                v-for="room in state.rooms"
                :key="room.id"
                class="room-detail-card"
                :class="{ selected: state.selectedRoomId === room.id }"
              >
                <div class="room-photo-box">
                  <img :src="room.image" :alt="room.name" />
                </div>

                <div class="room-info-box">
                  <div class="room-name-badge-row">
                    <h4 class="room-title">{{ room.name }}</h4>
                    <span class="room-floor-tag">{{ room.floor }}</span>
                  </div>

                  <div class="room-amenities-bullets">
                    <span>👥 {{ room.capacity }} kishilik</span>
                    <span>📐 {{ room.size }}</span>
                    <span>🛏️ {{ room.beds }}</span>
                    <span v-if="room.breakfastIncluded" class="breakfast-badge">🍳 Bepul nonushta kiritilgan</span>
                  </div>

                  <div class="room-card-bottom-row">
                    <div class="room-price-display">
                      <span class="p-dan">1 kecha uchun:</span>
                      <div class="p-value">{{ formatSum(room.pricePerNight) }} so'm</div>
                    </div>

                    <button
                      class="btn-primary select-room-btn"
                      :class="{ active: state.selectedRoomId === room.id }"
                      @click="chooseRoomAndBook(room.id)"
                    >
                      {{ state.selectedRoomId === room.id ? '✓ Tanlangan (Bron qilish)' : 'Xonani tanlash' }}
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </section>

          <!-- Tab 2: Description -->
          <section v-else-if="activeTab === 'Tavsif'" class="desc-section">
            <h3 class="section-sub-heading">Mehmonxona haqida</h3>
            <p class="hotel-full-description">{{ currentHotel.description }}</p>
            <div class="hotel-highlights-grid">
              <div class="highlight-item">
                <span class="h-icon">🏨</span>
                <div>
                  <h5>Zamonaviy bino</h5>
                  <p>Barcha xonalar shovqinsiz va zamonaviy ta'mirdan chiqqan.</p>
                </div>
              </div>
              <div class="highlight-item">
                <span class="h-icon">📍</span>
                <div>
                  <h5>Ajoyib joylashuv</h5>
                  <p>Shahar markazida, asosiy metro va diqqatga sazovor joylar yaqinida.</p>
                </div>
              </div>
            </div>
          </section>

          <!-- Tab 3: Amenities -->
          <section v-else-if="activeTab === 'Qulayliklar'" class="amenities-section">
            <h3 class="section-sub-heading">Mehmonxonadagi barcha qulayliklar</h3>
            <div class="amenities-full-grid">
              <div v-for="a in currentHotel.amenities" :key="a.name" class="amenity-full-card">
                <span class="a-icon">✨</span>
                <span class="a-name">{{ a.name }}</span>
              </div>
              <div class="amenity-full-card">
                <span class="a-icon">❄️</span>
                <span class="a-name">Konditsioner</span>
              </div>
              <div class="amenity-full-card">
                <span class="a-icon">📺</span>
                <span class="a-name">Smart TV & Kabel TV</span>
              </div>
              <div class="amenity-full-card">
                <span class="a-icon">🧳</span>
                <span class="a-name">Yuk saqlash xonasi</span>
              </div>
            </div>
          </section>

          <!-- Tab 4: Reviews -->
          <section v-else-if="activeTab === 'Sharhlar'" class="reviews-section">
            <h3 class="section-sub-heading">Haqiqiy mehmonlar sharhlari</h3>
            <div class="reviews-breakdown-box">
              <div class="score-col">
                <div class="score-number">{{ currentHotel.rating }}</div>
                <div class="stars-gold">★★★★★</div>
                <div class="score-count">{{ currentHotel.reviewsCount }} ta tekshirilgan sharh</div>
              </div>
              <div class="bars-col">
                <div v-for="b in state.ratingsBreakdown" :key="b.star" class="b-row">
                  <span>{{ b.star }} yulduz</span>
                  <div class="b-track">
                    <div class="b-fill" :style="{ width: b.percent + '%' }"></div>
                  </div>
                  <span>{{ b.percent }}%</span>
                </div>
              </div>
            </div>

            <div class="reviews-list-column">
              <div v-for="rev in state.reviews" :key="rev.id" class="single-review-card">
                <div class="rev-card-head">
                  <div class="rev-u-avatar">{{ rev.avatarText }}</div>
                  <div>
                    <div class="rev-u-name">{{ rev.author }}</div>
                    <div class="rev-u-date">{{ rev.date }}</div>
                  </div>
                  <div class="rev-stars-box">{{ '★'.repeat(rev.rating) }}</div>
                </div>
                <p class="rev-comment-text">{{ rev.comment }}</p>
              </div>
            </div>
          </section>

          <!-- Tab 5: Location -->
          <section v-else-if="activeTab === 'Joylashuv'" class="location-section">
            <h3 class="section-sub-heading">Joylashuv va xarita</h3>
            <p>📍 {{ currentHotel.address }}</p>
            <div class="mock-map-box">
              <span>🗺️ Interaktiv xarita (Toshkent markazi)</span>
            </div>
          </section>
        </div>

        <!-- Right: Sticky Booking Calculator Box -->
        <aside class="sticky-booking-aside">
          <div class="sticky-card">
            <div class="sticky-price-header">
              <span class="s-from">Boshlang'ich narx:</span>
              <div class="s-price">{{ formatSum(selectedRoom.pricePerNight) }} so'm <small>/ kecha</small></div>
            </div>

            <div class="sticky-form">
              <div class="form-group">
                <label>Tanlangan xona</label>
                <select v-model="state.selectedRoomId" class="form-select">
                  <option v-for="r in state.rooms" :key="r.id" :value="r.id">
                    {{ r.name }} ({{ formatSum(r.pricePerNight) }} so'm)
                  </option>
                </select>
              </div>

              <div class="form-row-2">
                <div class="form-group">
                  <label>Kirish sanasi</label>
                  <input type="date" v-model="state.checkout.checkIn" class="form-input" />
                </div>
                <div class="form-group">
                  <label>Chiqish sanasi</label>
                  <input type="date" v-model="state.checkout.checkOut" class="form-input" />
                </div>
              </div>

              <div class="form-group">
                <label>Mehmonlar</label>
                <select v-model="state.checkout.guestsCount" class="form-select">
                  <option :value="1">1 mehmon</option>
                  <option :value="2">2 mehmon</option>
                  <option :value="3">3 mehmon</option>
                  <option :value="4">4 mehmon</option>
                </select>
              </div>

              <div class="price-breakdown-list">
                <div class="p-row">
                  <span>3 kecha × {{ formatSum(selectedRoom.pricePerNight) }}</span>
                  <span>{{ formatSum(selectedRoom.pricePerNight * 3) }} so'm</span>
                </div>
                <div class="p-row">
                  <span>Xizmat haqi va soliqlar</span>
                  <span>120 000 so'm</span>
                </div>
                <div class="p-row total-row">
                  <span>Jami to'lov:</span>
                  <span class="total-colored">{{ formatSum(selectedRoom.pricePerNight * 3 + 120000) }} so'm</span>
                </div>
              </div>

              <button class="btn-primary full-btn" @click="proceedToCheckout">
                ⚡ Bronni rasmiylashtirish &rarr;
              </button>

              <div class="trust-note">
                🔒 100% Xavfsiz to'lov • To'lov qaytarilishi kafolatlangan
              </div>
            </div>
          </div>
        </aside>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { state, selectedRoom, selectRoom, toggleFavorite } from '../store'

const route = useRoute()
const router = useRouter()
const activeTab = ref('Xonalar')

const currentHotel = computed(() => {
  const hotelId = Number(route.params.id) || 1
  return state.hotels.find(h => h.id === hotelId) || state.hotels[0]
})

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function handleShare() {
  navigator.clipboard?.writeText(window.location.href)
  alert("Mehmonxona havolasi nusxalandi!")
}

function chooseRoomAndBook(roomId) {
  selectRoom(roomId)
  router.push('/checkout')
}

function proceedToCheckout() {
  router.push('/checkout')
}
</script>

<style scoped>
.hotel-detail-page {
  padding-bottom: 50px;
}

.breadcrumb-nav {
  font-size: 11.5px;
  color: var(--text-muted);
  margin-bottom: 14px;
}

.breadcrumb-nav a {
  color: var(--primary);
  text-decoration: none;
}

/* Header */
.hotel-main-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 18px;
}

.title-with-verified {
  display: flex;
  align-items: center;
  gap: 12px;
}

.title-with-verified h1 {
  font-size: 24px;
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

.meta-location-row {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-top: 4px;
  font-size: 12px;
}

.stars-gold {
  color: #f59e0b;
}

.rating-text {
  font-weight: 700;
  color: #f59e0b;
}

.loc-divider {
  color: var(--text-light);
}

.loc-address {
  color: var(--text-muted);
}

.header-action-btns {
  display: flex;
  gap: 8px;
}

/* Gallery Showcase */
.hotel-gallery-showcase {
  display: grid;
  grid-template-columns: 2fr 1fr;
  gap: 12px;
  height: 340px;
  border-radius: var(--radius-md);
  overflow: hidden;
  margin-bottom: 24px;
}

.gallery-hero-pic {
  background: #1e293b;
  height: 100%;
}

.gallery-hero-pic img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.gallery-quad-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
  height: 100%;
}

.gallery-quad-pic {
  background: #1e293b;
  overflow: hidden;
}

.gallery-quad-pic img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.2s;
}

.gallery-quad-pic:hover img {
  transform: scale(1.04);
}

/* Split layout */
.detail-split-layout {
  display: grid;
  grid-template-columns: 1fr 360px;
  gap: 24px;
}

/* Tabs */
.detail-tabs-bar {
  display: flex;
  gap: 8px;
  border-bottom: 2px solid var(--border-light);
  margin-bottom: 20px;
}

.d-tab-btn {
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

.d-tab-btn.active {
  color: var(--primary);
  border-color: var(--primary);
}

.section-sub-heading {
  font-size: 16px;
  font-weight: 800;
  color: var(--text-main);
  margin-bottom: 14px;
}

/* Rooms selection */
.rooms-detail-cards-list {
  display: flex;
  flex-direction: column;
  gap: 14px;
}

.room-detail-card {
  display: grid;
  grid-template-columns: 180px 1fr;
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  overflow: hidden;
  box-shadow: var(--shadow-card);
  transition: all 0.15s ease;
}

.room-detail-card:hover {
  border-color: var(--primary);
}

.room-detail-card.selected {
  border-color: var(--primary);
  box-shadow: 0 0 0 2px var(--primary-light);
}

.room-photo-box {
  background: #1e293b;
}

.room-photo-box img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.room-info-box {
  padding: 14px 18px;
  display: flex;
  flex-direction: column;
}

.room-name-badge-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 6px;
}

.room-title {
  font-size: 15px;
  font-weight: 800;
  color: var(--text-main);
}

.room-floor-tag {
  font-size: 10px;
  background: var(--bg-subtle);
  color: var(--text-muted);
  padding: 2px 7px;
  border-radius: 4px;
}

.room-amenities-bullets {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  font-size: 11.5px;
  color: var(--text-muted);
  margin-bottom: 14px;
}

.breakfast-badge {
  color: var(--success);
  font-weight: 700;
}

.room-card-bottom-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  margin-top: auto;
  border-top: 1px solid var(--border-light);
  padding-top: 10px;
}

.p-dan {
  font-size: 10px;
  color: var(--text-muted);
}

.p-value {
  font-size: 15px;
  font-weight: 800;
  color: var(--primary);
}

.select-room-btn {
  padding: 7px 16px;
  font-size: 11.5px;
}

/* Sticky Card */
.sticky-booking-aside {
  position: sticky;
  top: 90px;
  height: fit-content;
}

.sticky-card {
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  padding: 20px;
  box-shadow: var(--shadow-card);
}

.sticky-price-header {
  border-bottom: 1px solid var(--border-light);
  padding-bottom: 12px;
  margin-bottom: 14px;
}

.s-from {
  font-size: 11px;
  color: var(--text-muted);
}

.s-price {
  font-size: 20px;
  font-weight: 800;
  color: var(--primary);
}

.s-price small {
  font-size: 12px;
  color: var(--text-muted);
  font-weight: normal;
}

.form-row-2 {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 8px;
}

.price-breakdown-list {
  background: var(--bg-subtle);
  border-radius: var(--radius-sm);
  padding: 10px 12px;
  display: flex;
  flex-direction: column;
  gap: 6px;
  margin-bottom: 16px;
}

.p-row {
  display: flex;
  justify-content: space-between;
  font-size: 11.5px;
  color: var(--text-secondary);
}

.total-row {
  border-top: 1px solid var(--border-subtle);
  padding-top: 8px;
  font-weight: 800;
  font-size: 13px;
  color: var(--text-main);
}

.total-colored {
  color: var(--success);
  font-size: 14px;
}

.full-btn {
  width: 100%;
  padding: 10px;
  font-size: 13px;
}

.trust-note {
  text-align: center;
  font-size: 9.5px;
  color: var(--text-muted);
  margin-top: 10px;
}

/* Reviews breakdown */
.reviews-breakdown-box {
  display: flex;
  align-items: center;
  gap: 20px;
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  padding: 18px;
  margin-bottom: 20px;
}

.score-col {
  text-align: center;
  border-right: 1px solid var(--border-light);
  padding-right: 20px;
}

.score-number {
  font-size: 32px;
  font-weight: 800;
  color: var(--text-main);
  line-height: 1;
}

.score-count {
  font-size: 11px;
  color: var(--text-muted);
  margin-top: 4px;
}

.bars-col {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.b-row {
  display: flex;
  align-items: center;
  gap: 10px;
  font-size: 11.5px;
  color: var(--text-secondary);
}

.b-track {
  flex: 1;
  height: 6px;
  background: var(--bg-subtle);
  border-radius: 3px;
  overflow: hidden;
}

.b-fill {
  height: 100%;
  background: #f59e0b;
}

.reviews-list-column {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.single-review-card {
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  padding: 14px 18px;
}

.rev-card-head {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 8px;
}

.rev-u-avatar {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  background: var(--primary);
  color: #fff;
  font-size: 11px;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
}

.rev-u-name {
  font-size: 12.5px;
  font-weight: 800;
  color: var(--text-main);
}

.rev-u-date {
  font-size: 10px;
  color: var(--text-muted);
}

.rev-stars-box {
  margin-left: auto;
  color: #f59e0b;
}

.rev-comment-text {
  font-size: 12.5px;
  color: var(--text-secondary);
  line-height: 1.4;
}

.mock-map-box {
  height: 220px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-md);
  display: flex;
  align-items: center;
  justify-content: center;
  color: var(--text-muted);
  font-size: 13px;
  font-weight: 700;
  margin-top: 10px;
}

@media (max-width: 950px) {
  .detail-split-layout {
    grid-template-columns: 1fr;
  }
  .hotel-gallery-showcase {
    grid-template-columns: 1fr;
    height: auto;
  }
}
</style>
