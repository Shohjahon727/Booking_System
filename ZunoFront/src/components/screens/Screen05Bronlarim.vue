<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">05</span> BRON DILARI
      </span>
      <span class="card-badge">Jami {{ state.bookings.length }} ta</span>
    </div>

    <!-- Status Filter Tabs -->
    <div class="booking-status-tabs">
      <button
        v-for="filter in filters"
        :key="filter"
        class="filter-tab-pill"
        :class="{ active: activeFilter === filter }"
        @click="activeFilter = filter"
      >
        {{ filter }}
      </button>
    </div>

    <!-- Bookings Cards List -->
    <div class="bookings-items-list">
      <div
        v-for="item in filteredBookings"
        :key="item.id"
        class="booking-card-row"
      >
        <div class="booking-thumb-box">
          <img :src="item.image" :alt="item.hotelName" />
        </div>

        <div class="booking-body-col">
          <div class="booking-hotel-name">{{ item.hotelName }}</div>
          <div class="booking-room-label">{{ item.roomName }}</div>
          <div class="booking-dates-text">
            📅 {{ item.checkIn }} - {{ item.checkOut }} ({{ item.nights }} kecha, {{ item.guests }} mehmon)
          </div>
        </div>

        <div class="booking-status-col">
          <span :class="['status-badge', item.status]">
            {{ item.status }}
          </span>
          <div class="booking-price-text">
            {{ formatSum(item.totalPrice) }} so'm
          </div>
        </div>
      </div>
    </div>

    <div class="load-more-row">
      <button class="load-more-btn" @click="handleLoadMore">Yana yuklash</button>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { state } from '../../store'

const filters = ['Barchasi', 'Pending', 'Confirmed', 'Cancelled', 'Completed']
const activeFilter = ref('Barchasi')

const filteredBookings = computed(() => {
  if (activeFilter.value === 'Barchasi') return state.bookings
  return state.bookings.filter(b => b.status === activeFilter.value)
})

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function handleLoadMore() {
  alert("Barcha bronlar ro'yxati to'liq ko'rsatilgan.")
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

.booking-status-tabs {
  display: flex;
  gap: 4px;
  margin-bottom: 8px;
  overflow-x: auto;
  padding-bottom: 2px;
}

.filter-tab-pill {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-muted);
  font-size: 9px;
  font-weight: 700;
  padding: 3px 8px;
  border-radius: var(--radius-full);
  cursor: pointer;
  white-space: nowrap;
  transition: all 0.12s;
}

.filter-tab-pill.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.bookings-items-list {
  display: flex;
  flex-direction: column;
  gap: 6px;
  flex: 1;
}

.booking-card-row {
  display: flex;
  align-items: center;
  gap: 8px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
  transition: border-color 0.15s;
}

.booking-card-row:hover {
  border-color: var(--primary);
}

.booking-thumb-box {
  width: 44px;
  height: 40px;
  border-radius: 4px;
  overflow: hidden;
  background: #1e293b;
  flex-shrink: 0;
}

.booking-thumb-box img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.booking-body-col {
  flex: 1;
  min-width: 0;
}

.booking-hotel-name {
  font-size: 10.5px;
  font-weight: 700;
  color: var(--text-main);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.booking-room-label {
  font-size: 9.5px;
  color: var(--primary);
  font-weight: 600;
}

.booking-dates-text {
  font-size: 8.5px;
  color: var(--text-muted);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.booking-status-col {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  gap: 3px;
  flex-shrink: 0;
}

.booking-price-text {
  font-size: 9.5px;
  font-weight: 700;
  color: var(--text-main);
}

.load-more-row {
  text-align: center;
  margin-top: 8px;
}

.load-more-btn {
  background: transparent;
  border: none;
  color: var(--primary);
  font-size: 9.5px;
  font-weight: 700;
  cursor: pointer;
}
</style>
