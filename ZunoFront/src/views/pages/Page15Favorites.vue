<template>
  <div class="favorites-page page-container">
    <div class="content-container">
      <!-- Breadcrumb Navigation -->
      <nav class="breadcrumb-bar">
        <router-link to="/" class="b-link">Bosh sahifa</router-link>
        <span class="b-sep">/</span>
        <span class="b-current">15. Sevimlilar (Wishlist)</span>
      </nav>

      <!-- Page Header -->
      <div class="page-header-flex">
        <div class="header-titles">
          <div class="badge-tag">❤️ MEHMON KABINETI</div>
          <h1 class="page-title">Mening Sevimli Mehmonxonalarim</h1>
          <p class="page-subtitle">
            Siz saqlagan va kelajakdagi sayohatlaringiz uchun rejalashtirgan sara mehmonxonalar to'plami.
          </p>
        </div>

        <div class="header-stats-chips">
          <div class="stat-chip">
            <span class="chip-num">{{ favoriteHotelsList.length }}</span>
            <span class="chip-label">Saqlangan</span>
          </div>
          <div class="stat-chip highlight">
            <span class="chip-num">4.8</span>
            <span class="chip-label">O'rtacha ball</span>
          </div>
        </div>
      </div>

      <!-- Filters & Control Strip -->
      <div class="filter-strip card-surface" v-if="favoriteHotelsList.length > 0">
        <div class="filter-group">
          <span class="filter-lbl">Shahar bo'yicha:</span>
          <div class="filter-pills">
            <button
              v-for="city in cities"
              :key="city"
              class="pill-btn"
              :class="{ active: selectedCity === city }"
              @click="selectedCity = city"
            >
              {{ city }}
            </button>
          </div>
        </div>

        <div class="filter-group sort-group">
          <span class="filter-lbl">Saralash:</span>
          <select v-model="sortBy" class="custom-select">
            <option value="default">Odatiy tartib</option>
            <option value="price-asc">Narx: Arzondan qimmatga</option>
            <option value="price-desc">Narx: Qimmatdan arzonga</option>
            <option value="rating">Eng yuqori reyting</option>
          </select>
        </div>
      </div>

      <!-- Favorites Grid -->
      <div v-if="filteredFavorites.length > 0" class="hotels-grid">
        <div
          v-for="hotel in filteredFavorites"
          :key="hotel.id"
          class="hotel-fav-card card-surface"
        >
          <!-- Hotel Image & Badges -->
          <div class="card-img-wrap">
            <img :src="hotel.image" :alt="hotel.name" class="hotel-photo" />
            <div class="badge-overlay">
              <span class="rating-pill">⭐ {{ hotel.rating }} ({{ hotel.reviewsCount }})</span>
              <span v-if="hotel.badge" class="special-pill">{{ hotel.badge }}</span>
            </div>
            <button
              class="remove-fav-btn"
              title="Sevimlilardan o'chirish"
              @click="handleRemoveFavorite(hotel.id)"
            >
              ❤️
            </button>
          </div>

          <!-- Hotel Body -->
          <div class="card-body">
            <div class="hotel-location">
              <span class="loc-pin">📍</span> {{ hotel.city }}, {{ hotel.address }}
            </div>
            <h3 class="hotel-title">{{ hotel.name }}</h3>
            <p class="hotel-desc">{{ hotel.description }}</p>

            <!-- Amenities Badges -->
            <div class="amenities-row">
              <span v-for="amenity in hotel.amenities.slice(0, 4)" :key="amenity" class="amenity-chip">
                ✓ {{ amenity }}
              </span>
              <span v-if="hotel.amenities.length > 4" class="amenity-chip more">
                +{{ hotel.amenities.length - 4 }} ko'proq
              </span>
            </div>

            <!-- Price & Actions Footer -->
            <div class="card-bottom-flex">
              <div class="price-block">
                <span class="price-from">Boshlang'ich narx:</span>
                <div class="price-val">
                  <span class="val-sum">{{ formatSum(hotel.pricePerNight) }}</span>
                  <span class="val-unit">so'm / tun</span>
                </div>
              </div>

              <div class="action-buttons">
                <button class="btn btn-outline" @click="handleViewHotel(hotel.id)">
                  Batafsil
                </button>
                <button class="btn btn-primary" @click="handleBookNow(hotel.id)">
                  Bron qilish ➔
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Empty State -->
      <div v-else class="empty-favorites-box card-surface">
        <div class="empty-icon-wrap">
          <span class="big-heart">💔</span>
        </div>
        <h3 class="empty-title">
          {{ selectedCity === 'Barchasi' ? 'Hozircha hech qanday mehmonxona saqlanmagan' : selectedCity + " bo'yicha sevimlilar topilmadi" }}
        </h3>
        <p class="empty-desc">
          O'zingizga ma'qul kelgan mehmonxonalarni sevimlilar ro'yxatiga qo'shing va ularni istalgan vaqtda osongina bron qiling.
        </p>
        <div class="empty-actions">
          <router-link to="/" class="btn btn-primary">
            🏨 Mehmonxonalarni qidirish
          </router-link>
          <button class="btn btn-outline" @click="restoreDefaultFavorites">
            ✨ Barcha mehmonxonalarni sevimlilarga qo'shish (Demo)
          </button>
        </div>
      </div>

      <!-- Quick Helpful Tips -->
      <div class="tips-banner card-surface">
        <div class="tip-icon">💡</div>
        <div class="tip-text">
          <strong>Foydali eslatma:</strong> Sevimlilar ro'yxatidagi narxlar real vaqt rejimida yangilanadi. Xona narxlari o'zgarishi yoki aksiyalar e'lon qilinishi bilan narx dinamikasini ushbu sahifada kuzatib borishingiz mumkin.
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { state, selectHotel, toggleFavorite } from '../../store'

const router = useRouter()

const selectedCity = ref('Barchasi')
const sortBy = ref('default')

const cities = ['Barchasi', 'Toshkent', 'Samarqand', 'Buxoro']

// Get favorites from store
const favoriteHotelsList = computed(() => {
  return state.hotels.filter(h => h.isFavorite)
})

// Filtered and sorted
const filteredFavorites = computed(() => {
  let list = favoriteHotelsList.value

  if (selectedCity.value !== 'Barchasi') {
    list = list.filter(h => h.city.toLowerCase() === selectedCity.value.toLowerCase())
  }

  if (sortBy.value === 'price-asc') {
    return [...list].sort((a, b) => a.pricePerNight - b.pricePerNight)
  } else if (sortBy.value === 'price-desc') {
    return [...list].sort((a, b) => b.pricePerNight - a.pricePerNight)
  } else if (sortBy.value === 'rating') {
    return [...list].sort((a, b) => b.rating - a.rating)
  }

  return list
})

function formatSum(num) {
  if (!num) return '0'
  return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ' ')
}

function handleRemoveFavorite(hotelId) {
  toggleFavorite(hotelId)
}

function handleViewHotel(hotelId) {
  selectHotel(hotelId)
  router.push(`/hotel/${hotelId}`)
}

function handleBookNow(hotelId) {
  selectHotel(hotelId)
  router.push('/booking')
}

function restoreDefaultFavorites() {
  state.hotels.forEach(h => {
    h.isFavorite = true
  })
}
</script>

<style scoped>
.favorites-page {
  padding-bottom: 60px;
}

.breadcrumb-bar {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 13px;
  color: #64748b;
  margin-bottom: 20px;
}

.b-link {
  color: #3b82f6;
  text-decoration: none;
}

.b-link:hover {
  text-decoration: underline;
}

.b-sep {
  color: #cbd5e1;
}

.b-current {
  color: #1e293b;
  font-weight: 600;
}

/* Page Header */
.page-header-flex {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  margin-bottom: 24px;
  gap: 20px;
}

.badge-tag {
  display: inline-block;
  background: #fee2e2;
  color: #dc2626;
  font-size: 11px;
  font-weight: 700;
  padding: 4px 10px;
  border-radius: 6px;
  margin-bottom: 8px;
  letter-spacing: 0.5px;
}

.page-title {
  font-size: 26px;
  font-weight: 800;
  color: #0f172a;
  margin-bottom: 6px;
}

.page-subtitle {
  font-size: 14px;
  color: #64748b;
}

.header-stats-chips {
  display: flex;
  gap: 12px;
}

.stat-chip {
  background: #ffffff;
  border: 1px solid #e2e8f0;
  border-radius: 12px;
  padding: 10px 18px;
  text-align: center;
  box-shadow: 0 1px 3px rgba(0,0,0,0.05);
}

.stat-chip.highlight {
  background: #eff6ff;
  border-color: #bfdbfe;
}

.chip-num {
  display: block;
  font-size: 20px;
  font-weight: 800;
  color: #1e293b;
}

.stat-chip.highlight .chip-num {
  color: #2563eb;
}

.chip-label {
  font-size: 11px;
  color: #64748b;
  font-weight: 600;
  text-transform: uppercase;
}

/* Filter Strip */
.filter-strip {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  padding: 14px 20px;
  border-radius: 12px;
  margin-bottom: 28px;
  gap: 16px;
}

.filter-group {
  display: flex;
  align-items: center;
  gap: 12px;
}

.filter-lbl {
  font-size: 13px;
  font-weight: 600;
  color: #475569;
}

.filter-pills {
  display: flex;
  gap: 8px;
}

.pill-btn {
  background: #f1f5f9;
  border: 1px solid #e2e8f0;
  color: #475569;
  padding: 6px 14px;
  border-radius: 20px;
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s;
}

.pill-btn:hover {
  background: #e2e8f0;
}

.pill-btn.active {
  background: #2563eb;
  border-color: #2563eb;
  color: #ffffff;
}

.custom-select {
  padding: 8px 12px;
  font-size: 13px;
  border: 1px solid #cbd5e1;
  border-radius: 8px;
  background: #ffffff;
  color: #1e293b;
  outline: none;
  cursor: pointer;
}

/* Hotels Grid */
.hotels-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(360px, 1fr));
  gap: 24px;
  margin-bottom: 32px;
}

.hotel-fav-card {
  border-radius: 16px;
  overflow: hidden;
  transition: transform 0.2s, box-shadow 0.2s;
  display: flex;
  flex-direction: column;
}

.hotel-fav-card:hover {
  transform: translateY(-3px);
  box-shadow: 0 10px 25px -5px rgba(0, 0, 0, 0.1);
}

.card-img-wrap {
  position: relative;
  width: 100%;
  height: 200px;
  overflow: hidden;
}

.hotel-photo {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.badge-overlay {
  position: absolute;
  top: 12px;
  left: 12px;
  display: flex;
  gap: 6px;
}

.rating-pill {
  background: rgba(15, 23, 42, 0.85);
  color: #ffffff;
  font-size: 12px;
  font-weight: 700;
  padding: 4px 8px;
  border-radius: 6px;
  backdrop-filter: blur(4px);
}

.special-pill {
  background: #22c55e;
  color: #ffffff;
  font-size: 11px;
  font-weight: 700;
  padding: 4px 8px;
  border-radius: 6px;
}

.remove-fav-btn {
  position: absolute;
  top: 12px;
  right: 12px;
  width: 36px;
  height: 36px;
  border-radius: 50%;
  background: #ffffff;
  border: none;
  font-size: 18px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  box-shadow: 0 2px 8px rgba(0,0,0,0.2);
  transition: transform 0.15s;
}

.remove-fav-btn:hover {
  transform: scale(1.15);
}

.card-body {
  padding: 18px;
  display: flex;
  flex-direction: column;
  flex: 1;
}

.hotel-location {
  font-size: 12px;
  color: #64748b;
  margin-bottom: 6px;
}

.hotel-title {
  font-size: 18px;
  font-weight: 700;
  color: #0f172a;
  margin-bottom: 8px;
}

.hotel-desc {
  font-size: 13px;
  color: #475569;
  line-height: 1.5;
  margin-bottom: 14px;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.amenities-row {
  display: flex;
  flex-wrap: wrap;
  gap: 6px;
  margin-bottom: 18px;
}

.amenity-chip {
  background: #f1f5f9;
  color: #475569;
  font-size: 11px;
  font-weight: 600;
  padding: 4px 8px;
  border-radius: 6px;
}

.amenity-chip.more {
  background: #e2e8f0;
  color: #334155;
}

.card-bottom-flex {
  margin-top: auto;
  padding-top: 14px;
  border-top: 1px solid #f1f5f9;
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  gap: 12px;
}

.price-from {
  display: block;
  font-size: 11px;
  color: #64748b;
  margin-bottom: 2px;
}

.val-sum {
  font-size: 18px;
  font-weight: 800;
  color: #0f172a;
}

.val-unit {
  font-size: 12px;
  color: #64748b;
  margin-left: 4px;
}

.action-buttons {
  display: flex;
  gap: 8px;
}

.btn {
  padding: 8px 14px;
  font-size: 13px;
  font-weight: 600;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  transition: all 0.2s;
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

.btn-primary {
  background: #2563eb;
  color: #ffffff;
}

.btn-primary:hover {
  background: #1d4ed8;
}

.btn-outline {
  background: transparent;
  border: 1px solid #cbd5e1;
  color: #334155;
}

.btn-outline:hover {
  background: #f8fafc;
  border-color: #94a3b8;
}

/* Empty State */
.empty-favorites-box {
  text-align: center;
  padding: 60px 24px;
  border-radius: 16px;
  margin-bottom: 30px;
}

.empty-icon-wrap {
  margin-bottom: 16px;
}

.big-heart {
  font-size: 56px;
}

.empty-title {
  font-size: 20px;
  font-weight: 700;
  color: #1e293b;
  margin-bottom: 8px;
}

.empty-desc {
  font-size: 14px;
  color: #64748b;
  max-width: 480px;
  margin: 0 auto 24px;
  line-height: 1.6;
}

.empty-actions {
  display: flex;
  justify-content: center;
  gap: 12px;
  flex-wrap: wrap;
}

/* Tips Banner */
.tips-banner {
  display: flex;
  align-items: flex-start;
  gap: 14px;
  padding: 16px 20px;
  border-radius: 12px;
  border-left: 4px solid #2563eb;
}

.tip-icon {
  font-size: 22px;
}

.tip-text {
  font-size: 13px;
  color: #475569;
  line-height: 1.5;
}

@media (max-width: 768px) {
  .page-header-flex {
    flex-direction: column;
    align-items: flex-start;
  }
  .filter-strip {
    flex-direction: column;
    align-items: stretch;
  }
  .card-bottom-flex {
    flex-direction: column;
    align-items: stretch;
  }
  .action-buttons {
    margin-top: 10px;
  }
  .action-buttons .btn {
    flex: 1;
  }
}
</style>
