<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">03</span> XONALARNI TANLASH
      </span>
      <div class="header-action-icons">
        <button class="icon-tiny-btn" title="Sevimlilar">🤍</button>
        <button class="icon-tiny-btn" title="Ulashish">🔗</button>
      </div>
    </div>

    <!-- Mini Filter Bar -->
    <div class="rooms-mini-filter-bar">
      <div class="filter-item">
        <span class="filter-lbl">Kirish:</span>
        <span class="filter-val">{{ state.checkout.checkIn }}</span>
      </div>
      <div class="filter-item">
        <span class="filter-lbl">Chiqish:</span>
        <span class="filter-val">{{ state.checkout.checkOut }}</span>
      </div>
      <div class="filter-item">
        <span class="filter-lbl">Mehmonlar:</span>
        <span class="filter-val">{{ state.checkout.guestsCount }} mehmon</span>
      </div>
    </div>

    <!-- Rooms List -->
    <div class="rooms-vertical-list">
      <div
        v-for="room in state.rooms"
        :key="room.id"
        class="room-card-row"
        :class="{ selected: state.selectedRoomId === room.id }"
      >
        <div class="room-thumbnail-box">
          <img :src="room.image" :alt="room.name" class="room-img" />
        </div>

        <div class="room-details-col">
          <h5 class="room-card-title">{{ room.name }}</h5>
          <div class="room-specs-line">
            <span>👥 {{ room.capacity }} kishi</span>
            <span>• 📐 {{ room.size }}</span>
            <span v-if="room.breakfastIncluded">• 🍳 Nonushta</span>
          </div>
        </div>

        <div class="room-price-col">
          <div class="price-dan-text">dan</div>
          <div class="room-exact-price">{{ formatSum(room.pricePerNight) }} so'm</div>
          <div class="per-kecha">/ 1 kecha</div>
        </div>

        <button
          class="btn-select-room"
          :class="{ active: state.selectedRoomId === room.id }"
          @click="chooseRoom(room.id)"
        >
          {{ state.selectedRoomId === room.id ? 'Tanlangan' : 'Tanlash' }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { state, selectRoom, setActiveView } from '../../store'

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function chooseRoom(roomId) {
  selectRoom(roomId)
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

.header-action-icons {
  display: flex;
  gap: 6px;
}

.icon-tiny-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 24px;
  height: 24px;
  border-radius: 4px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  font-size: 11px;
}

/* Filter bar */
.rooms-mini-filter-bar {
  display: flex;
  justify-content: space-between;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 5px 10px;
  margin-bottom: 8px;
  font-size: 9.5px;
}

.filter-lbl {
  color: var(--text-muted);
  margin-right: 3px;
}

.filter-val {
  font-weight: 700;
  color: var(--text-main);
}

/* Rooms list */
.rooms-vertical-list {
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.room-card-row {
  display: grid;
  grid-template-columns: 55px 1fr auto auto;
  gap: 8px;
  align-items: center;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
  transition: all 0.12s ease;
}

.room-card-row:hover {
  border-color: var(--primary);
}

.room-card-row.selected {
  border-color: var(--primary);
  background: var(--primary-light);
}

.room-thumbnail-box {
  width: 55px;
  height: 44px;
  border-radius: 4px;
  overflow: hidden;
  background: #1e293b;
}

.room-img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.room-card-title {
  font-size: 11px;
  font-weight: 700;
  color: var(--text-main);
  margin-bottom: 2px;
}

.room-specs-line {
  font-size: 9px;
  color: var(--text-muted);
  display: flex;
  flex-wrap: wrap;
  gap: 4px;
}

.room-price-col {
  text-align: right;
  line-height: 1.1;
}

.price-dan-text {
  font-size: 8px;
  color: var(--text-muted);
}

.room-exact-price {
  font-size: 10.5px;
  font-weight: 800;
  color: var(--primary);
}

.per-kecha {
  font-size: 8px;
  color: var(--text-muted);
}

.btn-select-room {
  background: var(--primary);
  color: #fff;
  border: none;
  padding: 5px 10px;
  border-radius: 4px;
  font-size: 9.5px;
  font-weight: 700;
  cursor: pointer;
  transition: all 0.15s;
}

.btn-select-room:hover {
  background: var(--primary-hover);
}

.btn-select-room.active {
  background: var(--success);
}
</style>
