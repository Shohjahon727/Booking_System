<template>
  <div class="hotels-list-page">
    <div class="content-container">
      <!-- Page Top Header -->
      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Barcha Mehmonxonalar Katalogi</h2>
          <p>O'zbekiston bo'yicha qulay va sertifikatlangan mehmonxona xonalarini toping</p>
        </div>
        <div class="page-actions-box">
          <span class="found-count">{{ filteredHotels.length }} ta mehmonxona topildi</span>
        </div>
      </div>

      <!-- Main Layout: Filters Sidebar + Hotels List -->
      <div class="catalog-layout">
        <!-- 1. Left Filters Sidebar -->
        <aside class="catalog-filters-sidebar">
          <div class="filter-box">
            <h4 class="filter-box-title">Qidiruv</h4>
            <input
              type="text"
              v-model="searchQuery"
              placeholder="Nomi bo'yicha..."
              class="form-input"
            />
          </div>

          <div class="filter-box">
            <h4 class="filter-box-title">Shahar</h4>
            <div class="filter-checkbox-list">
              <label v-for="c in ['Barchasi', 'Toshkent', 'Samarqand', 'Buxoro']" :key="c" class="check-item">
                <input type="radio" :value="c" v-model="selectedCity" />
                <span>{{ c }}</span>
              </label>
            </div>
          </div>

          <div class="filter-box">
            <h4 class="filter-box-title">Yulduzlar darajasi</h4>
            <div class="filter-checkbox-list">
              <label class="check-item">
                <input type="checkbox" v-model="filterStars[5]" />
                <span>★★★★★ 5 yulduzli</span>
              </label>
              <label class="check-item">
                <input type="checkbox" v-model="filterStars[4]" />
                <span>★★★★☆ 4 yulduzli</span>
              </label>
              <label class="check-item">
                <input type="checkbox" v-model="filterStars[3]" />
                <span>★★★☆☆ 3 yulduzli</span>
              </label>
            </div>
          </div>

          <div class="filter-box">
            <h4 class="filter-box-title">Qulayliklar</h4>
            <div class="filter-checkbox-list">
              <label class="check-item">
                <input type="checkbox" v-model="filterAmenities['Wi-Fi']" />
                <span>📶 Bepul Wi-Fi</span>
              </label>
              <label class="check-item">
                <input type="checkbox" v-model="filterAmenities['Basseyn']" />
                <span>🏊 Basseyn (Pool)</span>
              </label>
              <label class="check-item">
                <input type="checkbox" v-model="filterAmenities['Parking']" />
                <span>🅿️ Avtoturargoh</span>
              </label>
              <label class="check-item">
                <input type="checkbox" v-model="filterAmenities['Restoran']" />
                <span>🍽️ Restoran</span>
              </label>
            </div>
          </div>
        </aside>

        <!-- 2. Right Hotels Content -->
        <main class="catalog-content">
          <!-- Sort Bar -->
          <div class="sort-bar">
            <span class="sort-lbl">Saralash:</span>
            <select v-model="sortBy" class="sort-select">
              <option value="recommended">Tavsiya etilgan</option>
              <option value="price-asc">Narxi: Avval arzonlari</option>
              <option value="price-desc">Narxi: Avval qimmatlari</option>
              <option value="rating">Reyting bo'yicha</option>
            </select>
          </div>

          <!-- Hotels Vertical Rows -->
          <div class="hotels-cards-column">
            <div
              v-for="hotel in sortedHotels"
              :key="hotel.id"
              class="hotel-wide-card"
            >
              <div class="hotel-wide-img">
                <img :src="hotel.image" :alt="hotel.name" />
                <button
                  class="fav-badge-btn"
                  :class="{ active: hotel.isFavorite }"
                  @click="toggleFavorite(hotel.id)"
                >
                  {{ hotel.isFavorite ? '❤️' : '🤍' }}
                </button>
              </div>

              <div class="hotel-wide-info">
                <div class="wide-header-line">
                  <div>
                    <h3 class="wide-hotel-name">{{ hotel.name }}</h3>
                    <div class="wide-hotel-loc">📍 {{ hotel.address }}</div>
                  </div>
                  <div class="wide-rating-badge">
                    <span class="rating-num">★ {{ hotel.rating }}</span>
                    <span class="reviews-count">({{ hotel.reviewsCount }} ta sharh)</span>
                  </div>
                </div>

                <p class="hotel-short-desc">{{ hotel.description }}</p>

                <div class="hotel-amenity-pills">
                  <span v-for="a in hotel.amenities" :key="a.name" class="amenity-pill-item">
                    {{ a.name }}
                  </span>
                </div>

                <div class="wide-card-footer">
                  <div class="price-side-block">
                    <span class="from-lbl">Boshlang'ich narx:</span>
                    <div class="main-price-val">
                      dan {{ formatSum(hotel.pricePerNight) }} so'm <span class="per-night-txt">/ 1 kecha</span>
                    </div>
                  </div>

                  <router-link
                    :to="'/hotels/' + hotel.id"
                    class="btn-primary select-hotel-btn"
                    @click="selectHotel(hotel.id)"
                  >
                    Xonalarni ko'rish &rarr;
                  </router-link>
                </div>
              </div>
            </div>
          </div>
        </main>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive, computed } from 'vue'
import { useRoute } from 'vue-router'
import { state, selectHotel, toggleFavorite } from '../store'

const route = useRoute()
const searchQuery = ref(route.query.q || '')
const selectedCity = ref(route.query.city || 'Barchasi')
const sortBy = ref('recommended')

const filterStars = reactive({ 5: true, 4: true, 3: true })
const filterAmenities = reactive({ 'Wi-Fi': false, 'Basseyn': false, 'Parking': false, 'Restoran': false })

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

const filteredHotels = computed(() => {
  return state.hotels.filter(h => {
    // City filter
    if (selectedCity.value !== 'Barchasi' && h.city !== selectedCity.value) return false
    // Search query
    if (searchQuery.value) {
      const q = searchQuery.value.toLowerCase()
      if (!h.name.toLowerCase().includes(q) && !h.city.toLowerCase().includes(q) && !h.address.toLowerCase().includes(q)) {
        return false
      }
    }
    // Star filter
    if (!filterStars[h.starCount]) return false
    // Amenities filter
    for (const [amenity, isChecked] of Object.entries(filterAmenities)) {
      if (isChecked && !h.amenities.some(a => a.name.includes(amenity))) {
        return false
      }
    }
    return true
  })
})

const sortedHotels = computed(() => {
  const list = [...filteredHotels.value]
  if (sortBy.value === 'price-asc') {
    list.sort((a, b) => a.pricePerNight - b.pricePerNight)
  } else if (sortBy.value === 'price-desc') {
    list.sort((a, b) => b.pricePerNight - a.pricePerNight)
  } else if (sortBy.value === 'rating') {
    list.sort((a, b) => b.rating - a.rating)
  }
  return list
})
</script>

<style scoped>
.hotels-list-page {
  padding-bottom: 40px;
}

.found-count {
  font-size: 12px;
  font-weight: 700;
  color: var(--primary);
  background: var(--primary-light);
  padding: 4px 10px;
  border-radius: var(--radius-sm);
}

.catalog-layout {
  display: grid;
  grid-template-columns: 260px 1fr;
  gap: 20px;
}

/* Sidebar Filters */
.catalog-filters-sidebar {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.filter-box {
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  padding: 16px;
  box-shadow: var(--shadow-sm);
}

.filter-box-title {
  font-size: 12px;
  font-weight: 800;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  color: var(--text-muted);
  margin-bottom: 12px;
}

.filter-checkbox-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.check-item {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 12.5px;
  color: var(--text-secondary);
  cursor: pointer;
}

/* Catalog Content */
.sort-bar {
  display: flex;
  align-items: center;
  justify-content: flex-end;
  gap: 10px;
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-sm);
  padding: 8px 14px;
  margin-bottom: 16px;
}

.sort-lbl {
  font-size: 11.5px;
  font-weight: 700;
  color: var(--text-muted);
}

.sort-select {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-main);
  padding: 4px 10px;
  border-radius: var(--radius-sm);
  font-size: 12px;
  outline: none;
}

.hotels-cards-column {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.hotel-wide-card {
  display: grid;
  grid-template-columns: 220px 1fr;
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  overflow: hidden;
  box-shadow: var(--shadow-card);
  transition: all 0.2s ease;
}

.hotel-wide-card:hover {
  transform: translateY(-2px);
  border-color: var(--primary);
  box-shadow: var(--shadow-lg);
}

.hotel-wide-img {
  position: relative;
  background: #1e293b;
}

.hotel-wide-img img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.fav-badge-btn {
  position: absolute;
  top: 10px;
  right: 10px;
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

.hotel-wide-info {
  padding: 18px;
  display: flex;
  flex-direction: column;
}

.wide-header-line {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 6px;
}

.wide-hotel-name {
  font-size: 16px;
  font-weight: 800;
  color: var(--text-main);
}

.wide-hotel-loc {
  font-size: 11.5px;
  color: var(--text-muted);
  margin-top: 2px;
}

.wide-rating-badge {
  text-align: right;
}

.rating-num {
  font-size: 13px;
  font-weight: 800;
  color: #f59e0b;
}

.reviews-count {
  font-size: 10px;
  color: var(--text-muted);
  margin-left: 4px;
}

.hotel-short-desc {
  font-size: 12px;
  color: var(--text-secondary);
  line-height: 1.4;
  margin-bottom: 12px;
}

.hotel-amenity-pills {
  display: flex;
  flex-wrap: wrap;
  gap: 6px;
  margin-bottom: 16px;
}

.amenity-pill-item {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  font-size: 10.5px;
  padding: 3px 8px;
  border-radius: var(--radius-sm);
  color: var(--text-secondary);
}

.wide-card-footer {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  margin-top: auto;
  border-top: 1px solid var(--border-light);
  padding-top: 12px;
}

.from-lbl {
  font-size: 10.5px;
  color: var(--text-muted);
}

.main-price-val {
  font-size: 16px;
  font-weight: 800;
  color: var(--primary);
}

.per-night-txt {
  font-size: 11px;
  color: var(--text-muted);
  font-weight: normal;
}

.select-hotel-btn {
  padding: 8px 18px;
  font-size: 12px;
}

@media (max-width: 900px) {
  .catalog-layout {
    grid-template-columns: 1fr;
  }
  .hotel-wide-card {
    grid-template-columns: 1fr;
  }
  .hotel-wide-img {
    height: 180px;
  }
}
</style>
