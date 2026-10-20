<template>
  <div class="favorites-page">
    <div class="content-container">
      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Sevimli Mehmonxonalar (Wishlist)</h2>
          <p>Keyinchalik bron qilish uchun saqlab qo'ygan mehmonxonalaringiz</p>
        </div>
        <div class="page-actions-box">
          <span class="card-badge">{{ favoriteHotels.length }} ta saqlangan</span>
        </div>
      </div>

      <div v-if="favoriteHotels.length === 0" class="card empty-fav-box">
        <span class="empty-emoji">🤍</span>
        <h3>Sevimlilar ro'yxatingiz bo'sh</h3>
        <p>O'zingizga yoqqan mehmonxonalarni yurakcha belgisini bosib bu yerga saqlashingiz mumkin.</p>
        <router-link to="/hotels" class="btn-primary" style="margin-top: 14px;">
          Mehmonxonalarni ko'rish
        </router-link>
      </div>

      <div v-else class="fav-grid">
        <div v-for="h in favoriteHotels" :key="h.id" class="hotel-main-card card">
          <div class="card-img-box">
            <img :src="h.image" :alt="h.name" />
            <button class="card-fav-btn active" @click="toggleFavorite(h.id)" title="Olib tashlash">
              ❤️
            </button>
          </div>
          <div class="card-body-box">
            <div class="card-top-row">
              <h3 class="hotel-name">{{ h.name }}</h3>
              <span class="hotel-rating-badge">★ {{ h.rating }}</span>
            </div>
            <div class="hotel-loc">📍 {{ h.address }}</div>
            <div class="card-footer-price-row">
              <div>
                <span class="price-from-txt">Narxi:</span>
                <div class="exact-price-txt">{{ h.featuredPriceText }}</div>
              </div>
              <router-link :to="'/hotels/' + h.id" class="btn-primary view-btn" @click="selectHotel(h.id)">
                Bron qilish
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { favoriteHotels, toggleFavorite, selectHotel } from '../store'
</script>

<style scoped>
.favorites-page {
  padding-bottom: 50px;
}

.empty-fav-box {
  text-align: center;
  padding: 50px;
}

.empty-emoji {
  font-size: 40px;
  display: block;
  margin-bottom: 12px;
}

.fav-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 20px;
}

.hotel-main-card {
  padding: 0;
  overflow: hidden;
}

.card-img-box {
  height: 160px;
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
  background: rgba(15, 23, 42, 0.7);
  border: none;
  width: 28px;
  height: 28px;
  border-radius: 50%;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
}

.card-body-box {
  padding: 16px;
  display: flex;
  flex-direction: column;
}

.card-top-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.hotel-name {
  font-size: 15px;
  font-weight: 800;
  color: var(--text-main);
}

.hotel-rating-badge {
  color: #f59e0b;
  font-weight: 800;
  font-size: 11px;
}

.hotel-loc {
  font-size: 11.5px;
  color: var(--text-muted);
  margin: 4px 0 14px 0;
}

.card-footer-price-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  border-top: 1px solid var(--border-light);
  padding-top: 12px;
}

.exact-price-txt {
  font-size: 13.5px;
  font-weight: 800;
  color: var(--primary);
}

@media (max-width: 900px) {
  .fav-grid {
    grid-template-columns: 1fr;
  }
}
</style>
