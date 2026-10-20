<template>
  <div class="page-container page-01-home">
    <div class="content-container">
      <!-- Screen 01 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 01</span>
        <span class="s-title">BOSH SAHIFA (Qidiruv & Vitrina)</span>
        <span class="s-role">Aktiv • Barcha mijozlar uchun</span>
      </div>

      <!-- Hero Banner with Real Search Engine -->
      <section class="hero-banner-box">
        <div class="hero-inner">
          <h1 class="hero-title">Orzuyingizdagi mehmonxonani toping</h1>
          <p class="hero-subtitle">Eng yaxshi narxlar va qulay joylar 24/7</p>

          <!-- Search Widget -->
          <div class="hero-search-engine">
            <div class="search-cell">
              <label>📍 Qayerga borasiz?</label>
              <input
                type="text"
                v-model="searchQuery"
                placeholder="Toshkent, Samarqand..."
                class="cell-input"
              />
            </div>

            <div class="search-cell">
              <label>📅 Kirish sanasi</label>
              <input
                type="date"
                v-model="state.checkout.checkIn"
                class="cell-input"
              />
            </div>

            <div class="search-cell">
              <label>📅 Chiqish sanasi</label>
              <input
                type="date"
                v-model="state.checkout.checkOut"
                class="cell-input"
              />
            </div>

            <div class="search-cell">
              <label>👥 Mehmonlar</label>
              <select v-model="state.checkout.guestsCount" class="cell-input">
                <option :value="1">1 mehmon</option>
                <option :value="2">2 mehmon (1 xona)</option>
                <option :value="3">3 mehmon</option>
                <option :value="4">4+ mehmon (Oila)</option>
              </select>
            </div>

            <button class="btn-primary search-submit-btn" @click="handleSearch">
              Qidirish
            </button>
          </div>
        </div>
      </section>

      <!-- Recommended Hotels Section -->
      <section class="recommended-hotels-section">
        <div class="section-header-row">
          <div>
            <h2 class="section-title">Tavsiya etilgan mehmonxonalar</h2>
            <p class="section-desc">Sayohatchilar tomonidan eng ko'p tanlangan va a'lo baholangan maskanlar</p>
          </div>
          <router-link to="/rooms" class="view-all-link">
            Barchasini ko'rish &rarr;
          </router-link>
        </div>

        <div class="hotels-cards-grid">
          <div
            v-for="hotel in filteredHotels"
            :key="hotel.id"
            class="card hotel-vitrina-card"
          >
            <div class="hotel-pic-box">
              <img :src="hotel.image" :alt="hotel.name" />
              <button
                class="fav-heart-btn"
                :class="{ active: hotel.isFavorite }"
                @click="toggleFavorite(hotel.id)"
                title="Sevimlilarga qo'shish"
              >
                {{ hotel.isFavorite ? '❤️' : '🤍' }}
              </button>
              <span v-if="hotel.verified" class="verified-ribbon">✓ Verified</span>
            </div>

            <div class="hotel-card-body">
              <div class="title-stars-row">
                <h3 class="h-name">{{ hotel.name }}</h3>
                <span class="rating-badge">★ {{ hotel.rating }} ({{ hotel.reviewsCount }})</span>
              </div>

              <div class="h-location">📍 {{ hotel.address }}</div>

              <div class="h-amenities-row">
                <span v-for="a in hotel.amenities.slice(0, 3)" :key="a.name" class="mini-tag">
                  {{ a.name }}
                </span>
              </div>

              <div class="h-footer-row">
                <div class="price-wrap">
                  <span class="dan-txt">Boshlang'ich narx:</span>
                  <div class="dan-val">{{ hotel.featuredPriceText }}</div>
                </div>

                <router-link
                  :to="'/hotel/' + hotel.id"
                  class="btn-primary btn-sm"
                  @click="selectHotel(hotel.id)"
                >
                  Batafsil / Xonalar &rarr;
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </section>

      <!-- Trust Features (Bottom 4 cards) -->
      <section class="trust-cards-grid">
        <div class="card trust-card">
          <span class="t-icon">🏷️</span>
          <h4>Eng yaxshi narxlar kafolati</h4>
          <p>Ortiqcha to'lovlarsiz eng qulay va kafolatlangan narxlar.</p>
        </div>
        <div class="card trust-card">
          <span class="t-icon">🔒</span>
          <h4>100% Xavfsiz to'lov</h4>
          <p>Payme, Click, Uzcard va Humo orqali to'liq himoyalangan to'lov.</p>
        </div>
        <div class="card trust-card">
          <span class="t-icon">📞</span>
          <h4>24/7 Qo'llab-quvvatlash</h4>
          <p>Doimiy yordam markazi kechayu-kunduz siz bilan aloqada.</p>
        </div>
        <div class="card trust-card">
          <span class="t-icon">⚡</span>
          <h4>Oson va tezkor bron</h4>
          <p>Atigi 1 daqiqada xonani bron qiling va elektron vaucher oling.</p>
        </div>
      </section>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { state, selectHotel, toggleFavorite } from '../../store'

const router = useRouter()
const searchQuery = ref('')

const filteredHotels = computed(() => {
  if (!searchQuery.value) return state.hotels
  const q = searchQuery.value.toLowerCase()
  return state.hotels.filter(h =>
    h.name.toLowerCase().includes(q) ||
    h.city.toLowerCase().includes(q) ||
    h.address.toLowerCase().includes(q)
  )
})

function handleSearch() {
  if (filteredHotels.value.length > 0) {
    selectHotel(filteredHotels.value[0].id)
    router.push('/rooms')
  }
}
</script>

<style scoped>
.page-01-home {
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
  color: var(--success);
  font-weight: 600;
}

/* Hero */
.hero-banner-box {
  background: linear-gradient(135deg, #0f172a 0%, #1e3a8a 60%, #0b1120 100%);
  border-radius: var(--radius-lg);
  padding: 48px 36px;
  color: #fff;
  box-shadow: 0 10px 30px rgba(15, 23, 42, 0.35);
  margin-bottom: 30px;
  position: relative;
  overflow: hidden;
}

.hero-banner-box::after {
  content: "";
  position: absolute;
  top: 0; right: 0; bottom: 0; left: 0;
  background: radial-gradient(circle at 80% 20%, rgba(56, 189, 248, 0.25) 0%, transparent 60%);
  pointer-events: none;
}

.hero-inner {
  max-width: 900px;
  margin: 0 auto;
  text-align: center;
  position: relative;
  z-index: 2;
}

.hero-title {
  font-size: 34px;
  font-weight: 800;
  letter-spacing: -0.8px;
  margin-bottom: 8px;
}

.hero-subtitle {
  font-size: 15px;
  color: #94a3b8;
  margin-bottom: 28px;
}

.hero-search-engine {
  background: rgba(15, 23, 42, 0.85);
  backdrop-filter: blur(10px);
  border: 1px solid rgba(255, 255, 255, 0.15);
  border-radius: var(--radius-md);
  padding: 12px 16px;
  display: grid;
  grid-template-columns: 1.5fr 1fr 1fr 1.1fr auto;
  gap: 12px;
  align-items: center;
  text-align: left;
}

.search-cell label {
  display: block;
  font-size: 10px;
  font-weight: 700;
  color: #94a3b8;
  text-transform: uppercase;
  margin-bottom: 4px;
}

.cell-input {
  width: 100%;
  background: #1e293b;
  border: 1px solid #334155;
  color: #fff;
  padding: 8px 10px;
  border-radius: var(--radius-sm);
  font-size: 12px;
  outline: none;
}

.cell-input:focus {
  border-color: #38bdf8;
}

.search-submit-btn {
  height: 38px;
  align-self: flex-end;
  padding: 0 20px;
  font-size: 13px;
  font-weight: 800;
}

/* Recommended Hotels */
.recommended-hotels-section {
  margin-bottom: 40px;
}

.section-header-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  margin-bottom: 20px;
}

.section-title {
  font-size: 22px;
  font-weight: 800;
  color: var(--text-main);
  letter-spacing: -0.4px;
}

.section-desc {
  font-size: 13px;
  color: var(--text-muted);
  margin-top: 2px;
}

.view-all-link {
  font-size: 13px;
  font-weight: 700;
  color: var(--primary);
  text-decoration: none;
}

.hotels-cards-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 18px;
}

.hotel-vitrina-card {
  padding: 0;
  overflow: hidden;
  display: flex;
  flex-direction: column;
}

.hotel-pic-box {
  height: 160px;
  position: relative;
  background: #1e293b;
}

.hotel-pic-box img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.fav-heart-btn {
  position: absolute;
  top: 8px;
  right: 8px;
  background: rgba(15, 23, 42, 0.7);
  border: none;
  width: 28px;
  height: 28px;
  border-radius: 50%;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 12px;
}

.verified-ribbon {
  position: absolute;
  bottom: 8px;
  left: 8px;
  background: rgba(16, 185, 129, 0.9);
  color: #fff;
  font-size: 9.5px;
  font-weight: 800;
  padding: 2px 8px;
  border-radius: 3px;
}

.hotel-card-body {
  padding: 16px;
  display: flex;
  flex-direction: column;
  flex: 1;
}

.title-stars-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 4px;
}

.h-name {
  font-size: 14px;
  font-weight: 800;
  color: var(--text-main);
}

.rating-badge {
  color: #f59e0b;
  font-weight: 800;
  font-size: 11px;
}

.h-location {
  font-size: 11.5px;
  color: var(--text-muted);
  margin-bottom: 8px;
}

.h-amenities-row {
  display: flex;
  flex-wrap: wrap;
  gap: 4px;
  margin-bottom: 14px;
}

.mini-tag {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  font-size: 9.5px;
  padding: 2px 6px;
  border-radius: 4px;
  color: var(--text-secondary);
}

.h-footer-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  margin-top: auto;
  border-top: 1px solid var(--border-light);
  padding-top: 12px;
}

.dan-txt {
  font-size: 9.5px;
  color: var(--text-muted);
}

.dan-val {
  font-size: 13px;
  font-weight: 800;
  color: var(--primary);
}

.btn-sm {
  padding: 6px 12px;
  font-size: 11px;
}

/* Trust grid */
.trust-cards-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 16px;
}

.trust-card {
  padding: 20px;
}

.t-icon {
  font-size: 26px;
  margin-bottom: 10px;
  display: block;
}

.trust-card h4 {
  font-size: 14px;
  font-weight: 800;
  color: var(--text-main);
  margin-bottom: 6px;
}

.trust-card p {
  font-size: 12px;
  color: var(--text-muted);
  line-height: 1.4;
}

@media (max-width: 1100px) {
  .hero-search-engine {
    grid-template-columns: 1fr 1fr;
  }
  .hotels-cards-grid, .trust-cards-grid {
    grid-template-columns: repeat(2, 1fr);
  }
}

@media (max-width: 650px) {
  .hero-search-engine {
    grid-template-columns: 1fr;
  }
  .hotels-cards-grid, .trust-cards-grid {
    grid-template-columns: 1fr;
  }
}
</style>
