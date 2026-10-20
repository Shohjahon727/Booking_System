<template>
  <div class="page-container page-03-rooms">
    <div class="content-container">
      <!-- Screen 03 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 03</span>
        <span class="s-title">XONALARNI TANLASH</span>
        <span class="s-role">Grand Plaza Hotel (4 toifa)</span>
      </div>

      <!-- Filter Controls Bar -->
      <div class="card filter-bar-card">
        <div class="filter-item">
          <span class="filter-lbl">📅 Kirish sanasi:</span>
          <input type="date" v-model="state.checkout.checkIn" class="filter-input-date" />
        </div>
        <div class="filter-item">
          <span class="filter-lbl">📅 Chiqish sanasi:</span>
          <input type="date" v-model="state.checkout.checkOut" class="filter-input-date" />
        </div>
        <div class="filter-item">
          <span class="filter-lbl">👥 Mehmonlar:</span>
          <select v-model="state.checkout.guestsCount" class="filter-select-guests">
            <option :value="1">1 mehmon</option>
            <option :value="2">2 mehmon</option>
            <option :value="3">3 mehmon</option>
            <option :value="4">4 mehmon</option>
          </select>
        </div>
      </div>

      <!-- Rooms Full Cards List -->
      <div class="rooms-vertical-cards-list">
        <div
          v-for="room in state.rooms"
          :key="room.id"
          class="card room-full-card"
          :class="{ selected: state.selectedRoomId === room.id }"
        >
          <div class="room-photo-left">
            <img :src="room.image" :alt="room.name" />
            <span class="room-type-tag">{{ room.type }}</span>
          </div>

          <div class="room-body-center">
            <div class="room-title-floor-row">
              <h3 class="room-name">{{ room.name }}</h3>
              <span class="room-floor-badge">{{ room.floor }}</span>
            </div>

            <div class="room-features-grid">
              <div class="feat-item">👥 Sig'imi: <b>{{ room.capacity }} kishi</b></div>
              <div class="feat-item">📐 Maydoni: <b>{{ room.size }}</b></div>
              <div class="feat-item">🛏️ Karavot turi: <b>{{ room.beds }}</b></div>
              <div v-if="room.breakfastIncluded" class="feat-item text-green">
                🍳 Nonushta kiritilgan (Shved stoli)
              </div>
            </div>

            <p class="room-mini-desc">
              Konditsioner, xususiy vanna xonasi, yuqori tezlikdagi Wi-Fi, mini-bar va choy/qahva burchagi mavjud.
            </p>
          </div>

          <div class="room-action-right">
            <span class="from-price-lbl">Boshlang'ich narx:</span>
            <div class="exact-room-price">
              {{ formatSum(room.pricePerNight) }} so'm
            </div>
            <span class="per-night-note">/ 1 kecha uchun</span>

            <button
              class="btn-primary select-book-btn"
              @click="chooseAndProceed(room.id)"
            >
              Tanlash va Bron qilish &rarr;
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router'
import { state, selectRoom } from '../../store'

const router = useRouter()

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function chooseAndProceed(roomId) {
  selectRoom(roomId)
  router.push('/booking')
}
</script>

<style scoped>
.page-03-rooms {
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

/* Filter bar */
.filter-bar-card {
  display: flex;
  align-items: center;
  gap: 20px;
  padding: 14px 20px;
  margin-bottom: 24px;
  flex-direction: row;
}

.filter-item {
  display: flex;
  align-items: center;
  gap: 8px;
}

.filter-lbl {
  font-size: 12px;
  font-weight: 700;
  color: var(--text-muted);
}

.filter-input-date, .filter-select-guests {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 6px 10px;
  border-radius: var(--radius-sm);
  font-size: 12px;
  color: var(--text-main);
  outline: none;
}

/* Rooms vertical cards */
.rooms-vertical-cards-list {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.room-full-card {
  display: grid;
  grid-template-columns: 240px 1fr 220px;
  padding: 0;
  overflow: hidden;
  transition: all 0.2s;
}

.room-full-card:hover {
  border-color: var(--primary);
  box-shadow: var(--shadow-lg);
}

.room-full-card.selected {
  border-color: var(--primary);
  box-shadow: 0 0 0 2px var(--primary-light);
}

.room-photo-left {
  position: relative;
  background: #1e293b;
}

.room-photo-left img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.room-type-tag {
  position: absolute;
  top: 10px;
  left: 10px;
  background: var(--primary);
  color: #fff;
  font-size: 10px;
  font-weight: 800;
  padding: 2px 7px;
  border-radius: 4px;
}

.room-body-center {
  padding: 20px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.room-title-floor-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
}

.room-name {
  font-size: 17px;
  font-weight: 800;
  color: var(--text-main);
}

.room-floor-badge {
  font-size: 11px;
  background: var(--bg-subtle);
  color: var(--text-muted);
  padding: 3px 8px;
  border-radius: 4px;
}

.room-features-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 8px;
  font-size: 12px;
  color: var(--text-secondary);
  margin-bottom: 10px;
}

.text-green {
  color: var(--success);
  font-weight: 700;
}

.room-mini-desc {
  font-size: 11.5px;
  color: var(--text-muted);
}

.room-action-right {
  padding: 20px;
  background: var(--bg-subtle);
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  border-left: 1px solid var(--border-light);
  text-align: center;
}

.from-price-lbl {
  font-size: 10.5px;
  color: var(--text-muted);
}

.exact-room-price {
  font-size: 18px;
  font-weight: 800;
  color: var(--primary);
  margin: 2px 0;
}

.per-night-note {
  font-size: 10.5px;
  color: var(--text-muted);
  margin-bottom: 16px;
}

.select-book-btn {
  width: 100%;
  padding: 10px;
  font-size: 12px;
}

@media (max-width: 900px) {
  .room-full-card {
    grid-template-columns: 1fr;
  }
  .room-photo-left {
    height: 180px;
  }
  .room-action-right {
    border-left: none;
    border-top: 1px solid var(--border-light);
  }
}
</style>
