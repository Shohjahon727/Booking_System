<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">01</span> BOSH SAHIFA
      </span>
      <span class="card-badge badge-active">Aktiv • Barcha mijozlar uchun</span>
    </div>

    <!-- Hero Banner Card -->
    <div class="hero-banner-container">
      <div class="hero-skyline-overlay">
        <h2 class="hero-headline">Orzuyingizdagi mehmonxonani toping</h2>
        <p class="hero-subline">Eng yaxshi narxlar va qulay joylar 24/7</p>

        <!-- Search Bar -->
        <div class="hero-search-bar">
          <div class="search-col">
            <label>Qayerga borasiz?</label>
            <input
              type="text"
              v-model="searchQuery"
              placeholder="Shahar yoki mehmonxona..."
              class="search-input"
            />
          </div>
          <div class="search-col">
            <label>Kirish sanasi</label>
            <input
              type="date"
              v-model="state.checkout.checkIn"
              class="search-input"
            />
          </div>
          <div class="search-col">
            <label>Chiqish sanasi</label>
            <input
              type="date"
              v-model="state.checkout.checkOut"
              class="search-input"
            />
          </div>
          <div class="search-col">
            <label>Mehmonlar</label>
            <select v-model="state.checkout.guestsCount" class="search-input">
              <option :value="1">1 mehmon</option>
              <option :value="2">2 mehmon</option>
              <option :value="3">3 mehmon</option>
              <option :value="4">4+ mehmon</option>
            </select>
          </div>
          <button class="btn-search-hero" @click="handleSearch">
            Qidirish
          </button>
        </div>
      </div>
    </div>

    <!-- Recommended Hotels Subsection -->
    <div class="recommended-header">
      <h3 class="section-title">Tavsiya etilgan mehmonxonalar</h3>
      <button class="view-all-link" @click="setActiveView('ALL')">Barchasini ko'rish</button>
    </div>

    <div class="hotels-cards-grid">
      <div
        v-for="hotel in filteredHotels"
        :key="hotel.id"
        class="hotel-card-item"
        :class="{ active: state.selectedHotelId === hotel.id }"
        @click="selectHotelCard(hotel.id)"
      >
        <div class="hotel-img-wrapper">
          <img :src="hotel.image" :alt="hotel.name" class="hotel-thumb-img" />
          <button
            class="fav-badge-btn"
            :class="{ favorited: hotel.isFavorite }"
            @click.stop="toggleFavorite(hotel.id)"
            title="Sevimlilarga qo'shish"
          >
            {{ hotel.isFavorite ? '❤️' : '🤍' }}
          </button>
        </div>
        <div class="hotel-item-body">
          <div class="hotel-name-row">
            <h4 class="hotel-title">{{ hotel.name }}</h4>
            <span class="hotel-city-badge">{{ hotel.city }}</span>
          </div>
          <div class="hotel-location-text">📍 {{ hotel.address.split(',')[0] }}</div>
          <div class="hotel-meta-footer">
            <span class="rating-stars">★ {{ hotel.rating }} ({{ hotel.reviewsCount }} ta baho)</span>
            <span class="hotel-price-dan">{{ hotel.featuredPriceText }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { state, selectHotel, toggleFavorite, setActiveView } from '../../store'

const searchQuery = ref('')

const filteredHotels = computed(() => {
  if (!searchQuery.value && !state.searchGlobal) return state.hotels
  const q = (searchQuery.value || state.searchGlobal).toLowerCase()
  return state.hotels.filter(h =>
    h.name.toLowerCase().includes(q) ||
    h.city.toLowerCase().includes(q) ||
    h.address.toLowerCase().includes(q)
  )
})

function selectHotelCard(id) {
  selectHotel(id)
}

function handleSearch() {
  if (filteredHotels.value.length > 0) {
    selectHotel(filteredHotels.value[0].id)
  }
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

/* Hero */
.hero-banner-container {
  background: linear-gradient(135deg, #0f172a 0%, #1e3a8a 100%);
  border-radius: var(--radius-sm);
  padding: 14px;
  color: #fff;
  margin-bottom: 12px;
  position: relative;
  overflow: hidden;
  box-shadow: 0 4px 14px rgba(15, 23, 42, 0.4);
}

.hero-banner-container::after {
  content: "";
  position: absolute;
  top: 0; right: 0; bottom: 0; left: 0;
  background-image: radial-gradient(circle at 80% 20%, rgba(56, 189, 248, 0.25) 0%, transparent 60%);
  pointer-events: none;
}

.hero-headline {
  font-size: 14px;
  font-weight: 800;
  margin-bottom: 2px;
  letter-spacing: -0.3px;
}

.hero-subline {
  font-size: 10px;
  color: #94a3b8;
  margin-bottom: 10px;
}

/* Search Bar */
.hero-search-bar {
  background: rgba(15, 23, 42, 0.85);
  backdrop-filter: blur(8px);
  border: 1px solid rgba(255, 255, 255, 0.15);
  border-radius: 6px;
  padding: 6px 8px;
  display: grid;
  grid-template-columns: 1.2fr 1fr 1fr 1fr auto;
  gap: 6px;
  align-items: center;
}

.search-col label {
  display: block;
  font-size: 8.5px;
  color: #94a3b8;
  margin-bottom: 2px;
  font-weight: 600;
  text-transform: uppercase;
}

.search-input {
  width: 100%;
  background: #1e293b;
  border: 1px solid #334155;
  color: #fff;
  padding: 4px 6px;
  border-radius: 4px;
  font-size: 10px;
  outline: none;
}

.search-input:focus {
  border-color: #38bdf8;
}

.btn-search-hero {
  background: #2563eb;
  color: #fff;
  border: none;
  padding: 8px 12px;
  border-radius: 4px;
  font-weight: 700;
  font-size: 10.5px;
  cursor: pointer;
  align-self: flex-end;
  height: 30px;
  transition: background 0.15s;
}

.btn-search-hero:hover {
  background: #1d4ed8;
}

/* Recommended Hotels */
.recommended-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
}

.section-title {
  font-size: 11px;
  font-weight: 700;
  color: var(--text-main);
}

.view-all-link {
  background: transparent;
  border: none;
  color: var(--primary);
  font-size: 10px;
  font-weight: 600;
  cursor: pointer;
}

.hotels-cards-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 8px;
}

.hotel-card-item {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  overflow: hidden;
  cursor: pointer;
  transition: all 0.15s ease;
  display: flex;
  flex-direction: column;
}

.hotel-card-item:hover {
  border-color: var(--primary);
  transform: translateY(-2px);
}

.hotel-card-item.active {
  border-color: var(--primary);
  box-shadow: 0 0 0 2px var(--primary-light);
}

.hotel-img-wrapper {
  height: 64px;
  position: relative;
  background: #1e293b;
}

.hotel-thumb-img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.fav-badge-btn {
  position: absolute;
  top: 4px;
  right: 4px;
  background: rgba(15, 23, 42, 0.65);
  border: none;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  font-size: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

.hotel-item-body {
  padding: 6px;
  display: flex;
  flex-direction: column;
  flex: 1;
}

.hotel-name-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 4px;
}

.hotel-title {
  font-size: 10.5px;
  font-weight: 700;
  color: var(--text-main);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.hotel-city-badge {
  font-size: 8px;
  background: var(--card-bg);
  padding: 1px 4px;
  border-radius: 3px;
  color: var(--text-muted);
}

.hotel-location-text {
  font-size: 9px;
  color: var(--text-muted);
  margin-top: 1px;
  margin-bottom: 4px;
}

.hotel-meta-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: auto;
  font-size: 9px;
}

.rating-stars {
  color: #f59e0b;
  font-weight: 700;
}

.hotel-price-dan {
  font-weight: 700;
  color: var(--primary);
}

@media (max-width: 900px) {
  .hero-search-bar {
    grid-template-columns: 1fr;
  }
  .hotels-cards-grid {
    grid-template-columns: repeat(2, 1fr);
  }
}
</style>
