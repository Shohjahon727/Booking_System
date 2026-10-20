<template>
  <div class="home-view-page">
    <div class="content-container">
      <!-- 1. Hero Section with Real Search Box -->
      <section class="hero-banner-section">
        <div class="hero-overlay-content">
          <span class="hero-pill-badge">✨ O'zbekiston bo'ylab 150+ mehmonxonalar</span>
          <h1 class="hero-main-heading">Orzuyingizdagi mehmonxonani toping</h1>
          <p class="hero-subheading">Eng yaxshi narxlar, qulay joylar va kafolatlangan bron qilish 24/7</p>

          <!-- Interactive Search Box -->
          <div class="main-search-card">
            <div class="search-input-field">
              <label>📍 Qayerga borasiz?</label>
              <input
                type="text"
                v-model="searchQuery"
                placeholder="Shahar yoki mehmonxona..."
                class="s-input"
              />
            </div>

            <div class="search-input-field">
              <label>📅 Kirish sanasi</label>
              <input
                type="date"
                v-model="state.checkout.checkIn"
                class="s-input"
              />
            </div>

            <div class="search-input-field">
              <label>📅 Chiqish sanasi</label>
              <input
                type="date"
                v-model="state.checkout.checkOut"
                class="s-input"
              />
            </div>

            <div class="search-input-field">
              <label>👥 Mehmonlar</label>
              <select v-model="state.checkout.guestsCount" class="s-input">
                <option :value="1">1 mehmon</option>
                <option :value="2">2 mehmon (1 xona)</option>
                <option :value="3">3 mehmon</option>
                <option :value="4">4+ mehmon (Oila)</option>
              </select>
            </div>

            <button class="btn-primary search-action-btn" @click="handleSearchSubmit">
              🔍 Qidirish
            </button>
          </div>
        </div>
      </section>

      <!-- 2. City Filter Pills -->
      <section class="quick-cities-row">
        <span class="cities-label">Ommabop shaharlar:</span>
        <button
          v-for="city in ['Barchasi', 'Toshkent', 'Samarqand', 'Buxoro', 'Xiva']"
          :key="city"
          class="city-chip"
          :class="{ active: selectedCity === city }"
          @click="selectedCity = city"
        >
          {{ city }}
        </button>
      </section>

      <!-- 3. Featured Hotels Grid -->
      <section class="featured-hotels-section">
        <div class="section-title-bar">
          <div>
            <h2 class="section-heading">Tavsiya etilgan mehmonxonalar</h2>
            <p class="section-sub">Sayohatchilar tomonidan eng yuqori baholangan maskanlar</p>
          </div>
          <router-link to="/hotels" class="view-all-link">
            Barchasini ko'rish &rarr;
          </router-link>
        </div>

        <div class="hotels-grid-layout">
          <div
            v-for="hotel in displayedHotels"
            :key="hotel.id"
            class="hotel-main-card"
          >
            <div class="card-img-box">
              <img :src="hotel.image" :alt="hotel.name" />
              <button
                class="card-fav-btn"
                :class="{ active: hotel.isFavorite }"
                @click="toggleFavorite(hotel.id)"
                title="Sevimlilarga qo'shish"
              >
                {{ hotel.isFavorite ? '❤️' : '🤍' }}
              </button>
              <span v-if="hotel.verified" class="card-verified-tag">✓ Verified</span>
            </div>

            <div class="card-body-box">
              <div class="card-top-row">
                <h3 class="hotel-name">{{ hotel.name }}</h3>
                <span class="hotel-rating-badge">★ {{ hotel.rating }}</span>
              </div>

              <div class="hotel-loc">📍 {{ hotel.address }}</div>

              <div class="hotel-amenities-tags">
                <span v-for="a in hotel.amenities.slice(0, 4)" :key="a.name" class="mini-amenity-tag">
                  {{ a.name }}
                </span>
              </div>

              <div class="card-footer-price-row">
                <div>
                  <span class="price-from-txt">Boshlang'ich narx:</span>
                  <div class="exact-price-txt">{{ hotel.featuredPriceText }}</div>
                </div>
                <router-link :to="'/hotels/' + hotel.id" class="btn-primary view-btn" @click="selectHotel(hotel.id)">
                  Xonalarni ko'rish
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </section>

      <!-- 4. Why Choose Us -->
      <section class="why-us-section">
        <h2 class="why-heading">Nima uchun ZunoBooking?</h2>
        <div class="why-cards-grid">
          <div class="why-card">
            <span class="why-icon">🏷️</span>
            <h4>Eng yaxshi narxlar kafolati</h4>
            <p>Hech qanday ortiqcha komissiyasiz to'g'ridan-to'g'ri mehmonxona narxlarida bron qiling.</p>
          </div>
          <div class="why-card">
            <span class="why-icon">🔒</span>
            <h4>100% Xavfsiz to'lov</h4>
            <p>Payme, Click, Uzcard va Humo orqali to'liq xavfsiz va shifrlangan to'lov tizimi.</p>
          </div>
          <div class="why-card">
            <span class="why-icon">📞</span>
            <h4>24/7 Qo'llab-quvvatlash</h4>
            <p>Sizga istalgan vaqtda yordam berishga tayyor bo'lgan kechayu-kunduz aloqa markazi.</p>
          </div>
          <div class="why-card">
            <span class="why-icon">⚡</span>
            <h4>Bir lahzada tasdiqlash</h4>
            <p>Bron qilish jarayoni 1 daqiqadan kam vaqt oladi va vaucher bir zumda yuboriladi.</p>
          </div>
        </div>
      </section>

      <!-- 5. Partner Call to action -->
      <section class="partner-cta-card">
        <div class="partner-cta-content">
          <h3>Mehmonxona egasimisiz?</h3>
          <p>O'z mehmonxonangizni ZunoBooking tizimiga ulab, minglab yangi mehmonlarni jalb eting va bronlarni oson boshqaring.</p>
          <router-link to="/partner" class="btn-primary partner-join-btn" @click="setRole('HOTEL')">
            🏨 Hotel Egasi kabinetiga o'tish
          </router-link>
        </div>
      </section>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { state, selectHotel, toggleFavorite, setRole } from '../store'

const router = useRouter()
const searchQuery = ref('')
const selectedCity = ref('Barchasi')

const displayedHotels = computed(() => {
  return state.hotels.filter(h => {
    const matchCity = selectedCity.value === 'Barchasi' || h.city === selectedCity.value
    const matchQuery = !searchQuery.value ||
      h.name.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
      h.city.toLowerCase().includes(searchQuery.value.toLowerCase())
    return matchCity && matchQuery
  })
})

function handleSearchSubmit() {
  router.push({ path: '/hotels', query: { q: searchQuery.value, city: selectedCity.value } })
}
</script>

<style scoped>
.home-view-page {
  padding-bottom: 40px;
}

/* Hero */
.hero-banner-section {
  background: linear-gradient(135deg, #0b1120 0%, #1e3a8a 50%, #0f172a 100%);
  border-radius: var(--radius-lg);
  padding: 44px 32px;
  color: #fff;
  position: relative;
  overflow: hidden;
  box-shadow: 0 10px 30px rgba(15, 23, 42, 0.4);
  margin-bottom: 24px;
}

.hero-banner-section::before {
  content: "";
  position: absolute;
  top: 0; right: 0; bottom: 0; left: 0;
  background-image: radial-gradient(circle at 85% 15%, rgba(56, 189, 248, 0.25) 0%, transparent 60%);
  pointer-events: none;
}

.hero-overlay-content {
  position: relative;
  z-index: 2;
  max-width: 900px;
  margin: 0 auto;
  text-align: center;
}

.hero-pill-badge {
  display: inline-block;
  background: rgba(255, 255, 255, 0.15);
  backdrop-filter: blur(6px);
  padding: 4px 14px;
  border-radius: var(--radius-full);
  font-size: 11px;
  font-weight: 700;
  color: #38bdf8;
  margin-bottom: 12px;
}

.hero-main-heading {
  font-size: 32px;
  font-weight: 800;
  letter-spacing: -0.8px;
  margin-bottom: 8px;
  line-height: 1.2;
}

.hero-subheading {
  font-size: 14px;
  color: #94a3b8;
  margin-bottom: 28px;
}

/* Search card */
.main-search-card {
  background: rgba(15, 23, 42, 0.85);
  backdrop-filter: blur(12px);
  border: 1px solid rgba(255, 255, 255, 0.15);
  border-radius: var(--radius-md);
  padding: 14px;
  display: grid;
  grid-template-columns: 1.5fr 1fr 1fr 1.1fr auto;
  gap: 12px;
  align-items: center;
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.35);
  text-align: left;
}

.search-input-field label {
  display: block;
  font-size: 10px;
  font-weight: 700;
  color: #94a3b8;
  text-transform: uppercase;
  margin-bottom: 4px;
}

.s-input {
  width: 100%;
  background: #1e293b;
  border: 1px solid #334155;
  color: #fff;
  padding: 8px 12px;
  border-radius: var(--radius-sm);
  font-size: 12px;
  outline: none;
}

.s-input:focus {
  border-color: #38bdf8;
}

.search-action-btn {
  height: 38px;
  align-self: flex-end;
  padding: 0 20px;
  font-size: 13px;
  font-weight: 800;
}

/* Quick cities */
.quick-cities-row {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 30px;
  overflow-x: auto;
  padding-bottom: 4px;
}

.cities-label {
  font-size: 12px;
  font-weight: 700;
  color: var(--text-muted);
}

.city-chip {
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  color: var(--text-secondary);
  font-size: 11.5px;
  font-weight: 600;
  padding: 5px 14px;
  border-radius: var(--radius-full);
  cursor: pointer;
  transition: all 0.15s;
}

.city-chip:hover {
  border-color: var(--primary);
  color: var(--primary);
}

.city-chip.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

/* Featured Hotels */
.featured-hotels-section {
  margin-bottom: 48px;
}

.section-title-bar {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  margin-bottom: 20px;
}

.section-heading {
  font-size: 20px;
  font-weight: 800;
  color: var(--text-main);
  letter-spacing: -0.4px;
}

.section-sub {
  font-size: 12.5px;
  color: var(--text-muted);
  margin-top: 3px;
}

.view-all-link {
  font-size: 12.5px;
  font-weight: 700;
  color: var(--primary);
  text-decoration: none;
}

.hotels-grid-layout {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 18px;
}

.hotel-main-card {
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  overflow: hidden;
  box-shadow: var(--shadow-card);
  transition: all 0.2s ease;
  display: flex;
  flex-direction: column;
}

.hotel-main-card:hover {
  transform: translateY(-3px);
  box-shadow: var(--shadow-lg);
  border-color: var(--primary);
}

.card-img-box {
  height: 150px;
  position: relative;
  background: #1e293b;
}

.card-img-box img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.card-fav-btn {
  position: absolute;
  top: 8px;
  right: 8px;
  background: rgba(15, 23, 42, 0.65);
  border: none;
  width: 28px;
  height: 28px;
  border-radius: 50%;
  font-size: 12px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
}

.card-verified-tag {
  position: absolute;
  bottom: 8px;
  left: 8px;
  background: rgba(16, 185, 129, 0.9);
  color: #fff;
  font-size: 9px;
  font-weight: 800;
  padding: 2px 7px;
  border-radius: 3px;
}

.card-body-box {
  padding: 14px;
  display: flex;
  flex-direction: column;
  flex: 1;
}

.card-top-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 4px;
}

.hotel-name {
  font-size: 13.5px;
  font-weight: 800;
  color: var(--text-main);
}

.hotel-rating-badge {
  background: #fffbeb;
  color: #d97706;
  font-size: 11px;
  font-weight: 800;
  padding: 2px 6px;
  border-radius: 4px;
}

.hotel-loc {
  font-size: 11px;
  color: var(--text-muted);
  margin-bottom: 10px;
}

.hotel-amenities-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 4px;
  margin-bottom: 14px;
}

.mini-amenity-tag {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 2px 6px;
  border-radius: 4px;
  font-size: 9.5px;
  color: var(--text-secondary);
}

.card-footer-price-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  margin-top: auto;
  border-top: 1px solid var(--border-light);
  padding-top: 10px;
}

.price-from-txt {
  font-size: 9.5px;
  color: var(--text-muted);
}

.exact-price-txt {
  font-size: 12.5px;
  font-weight: 800;
  color: var(--primary);
}

.view-btn {
  padding: 6px 12px;
  font-size: 11px;
}

/* Why us */
.why-us-section {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-lg);
  padding: 36px 28px;
  margin-bottom: 40px;
  text-align: center;
}

.why-heading {
  font-size: 22px;
  font-weight: 800;
  color: var(--text-main);
  margin-bottom: 24px;
}

.why-cards-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20px;
  text-align: left;
}

.why-card {
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  padding: 18px;
}

.why-icon {
  font-size: 26px;
  margin-bottom: 10px;
  display: block;
}

.why-card h4 {
  font-size: 13.5px;
  font-weight: 800;
  color: var(--text-main);
  margin-bottom: 6px;
}

.why-card p {
  font-size: 11.5px;
  color: var(--text-muted);
  line-height: 1.4;
}

/* Partner CTA */
.partner-cta-card {
  background: linear-gradient(135deg, #1e3a8a 0%, #0f172a 100%);
  border-radius: var(--radius-lg);
  padding: 30px;
  color: #fff;
  text-align: center;
}

.partner-cta-content {
  max-width: 650px;
  margin: 0 auto;
}

.partner-cta-content h3 {
  font-size: 20px;
  font-weight: 800;
  margin-bottom: 8px;
}

.partner-cta-content p {
  font-size: 12.5px;
  color: #94a3b8;
  margin-bottom: 18px;
}

.partner-join-btn {
  background: #2563eb;
  padding: 10px 24px;
  font-size: 13px;
}

@media (max-width: 1100px) {
  .hotels-grid-layout, .why-cards-grid {
    grid-template-columns: repeat(2, 1fr);
  }
  .main-search-card {
    grid-template-columns: 1fr 1fr;
  }
}

@media (max-width: 650px) {
  .hotels-grid-layout, .why-cards-grid {
    grid-template-columns: 1fr;
  }
  .main-search-card {
    grid-template-columns: 1fr;
  }
}
</style>
