<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">15</span> SEVIMLILAR (WISHLIST)
      </span>
      <span class="card-badge">{{ favoriteHotels.length }} ta saqlangan</span>
    </div>

    <div v-if="favoriteHotels.length === 0" class="empty-favs">
      <span>Hozircha sevimli mehmonxonalar yo'q.</span>
    </div>

    <div v-else class="favorites-grid">
      <div v-for="hotel in favoriteHotels" :key="hotel.id" class="fav-hotel-item">
        <div class="fav-thumb">
          <img :src="hotel.image" :alt="hotel.name" />
          <button class="remove-fav-btn" @click="toggleFavorite(hotel.id)" title="Olib tashlash">
            ❤️
          </button>
        </div>
        <div class="fav-info">
          <div class="fav-name">{{ hotel.name }}</div>
          <div class="fav-city">📍 {{ hotel.city }} • ★ {{ hotel.rating }}</div>
          <div class="fav-price">{{ hotel.featuredPriceText }}</div>
        </div>
        <button class="btn-primary fav-book-btn" @click="handleBookHotel(hotel.id)">
          Bron qilish
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { favoriteHotels, toggleFavorite, selectHotel, setActiveView } from '../../store'

function handleBookHotel(hotelId) {
  selectHotel(hotelId)
  setActiveView('04')
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

.favorites-grid {
  display: flex;
  flex-direction: column;
  gap: 6px;
  flex: 1;
}

.fav-hotel-item {
  display: flex;
  align-items: center;
  gap: 8px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
}

.fav-thumb {
  width: 44px;
  height: 38px;
  border-radius: 4px;
  overflow: hidden;
  position: relative;
  background: #1e293b;
  flex-shrink: 0;
}

.fav-thumb img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.remove-fav-btn {
  position: absolute;
  top: 2px;
  right: 2px;
  background: rgba(15, 23, 42, 0.7);
  border: none;
  border-radius: 50%;
  font-size: 8px;
  cursor: pointer;
  width: 16px;
  height: 16px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.fav-info {
  flex: 1;
  min-width: 0;
}

.fav-name {
  font-size: 10.5px;
  font-weight: 700;
  color: var(--text-main);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.fav-city {
  font-size: 8.5px;
  color: var(--text-muted);
}

.fav-price {
  font-size: 9px;
  font-weight: 700;
  color: var(--primary);
}

.fav-book-btn {
  padding: 4px 8px;
  font-size: 8.5px;
  flex-shrink: 0;
}

.empty-favs {
  text-align: center;
  padding: 20px;
  font-size: 10px;
  color: var(--text-muted);
}
</style>
