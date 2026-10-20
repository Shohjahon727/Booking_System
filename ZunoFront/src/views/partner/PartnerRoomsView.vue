<template>
  <div class="dashboard-layout">
    <div class="dashboard-sidebar-wrap">
      <PartnerSidebar />
    </div>

    <div class="dashboard-main-area">
      <div class="page-top-bar">
        <div class="page-title-box">
          <h2>Xonalar Fondini Boshqarish</h2>
          <p>Mehmonxonangizdagi barcha xonalar ro'yxati, toifalari va narx siyosati</p>
        </div>
        <div class="page-actions-box">
          <button class="btn-primary" @click="openAddRoomModal">+ Yangi xona qo'shish</button>
        </div>
      </div>

      <!-- Rooms Grid Cards -->
      <div class="rooms-management-grid">
        <div v-for="room in state.rooms" :key="room.id" class="card room-manage-card">
          <div class="room-img-top">
            <img :src="room.image" :alt="room.name" />
            <span class="room-type-pill">{{ room.type }}</span>
          </div>

          <div class="room-manage-body">
            <div class="room-title-floor">
              <h4>{{ room.name }}</h4>
              <span class="floor-badge">{{ room.floor }}</span>
            </div>

            <div class="room-specs-list">
              <span>👥 {{ room.capacity }} kishilik</span>
              <span>📐 {{ room.size }}</span>
              <span>🛏️ {{ room.beds }}</span>
              <span v-if="room.breakfastIncluded" class="breakfast-tag">🍳 Nonushta kiritilgan</span>
            </div>

            <div class="room-price-row">
              <span class="price-lbl">1 kecha narxi:</span>
              <span class="price-num">{{ formatSum(room.pricePerNight) }} so'm</span>
            </div>

            <div class="room-actions-bar">
              <button class="btn-secondary btn-sm" @click="editRoom(room)">✏️ Tahrirlash</button>
              <button class="btn-outline btn-sm btn-delete" @click="deleteRoom(room.id)">🗑️ O'chirish</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { state } from '../../store'
import PartnerSidebar from '../../components/layout/PartnerSidebar.vue'

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function openAddRoomModal() {
  state.modals.addRoom = true
}

function editRoom(room) {
  alert(`${room.name} parametrlarini o'zgartirish oynasi.`)
}

function deleteRoom(id) {
  if (confirm("Haqiqatan ham ushbu xonani o'chirishni xohlaysizmi?")) {
    state.rooms = state.rooms.filter(r => r.id !== id)
  }
}
</script>

<style scoped>
.rooms-management-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 18px;
}

.room-manage-card {
  padding: 0;
  overflow: hidden;
}

.room-img-top {
  height: 160px;
  position: relative;
  background: #1e293b;
}

.room-img-top img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.room-type-pill {
  position: absolute;
  top: 10px;
  left: 10px;
  background: var(--primary);
  color: #fff;
  font-size: 10px;
  font-weight: 800;
  padding: 3px 8px;
  border-radius: 4px;
}

.room-manage-body {
  padding: 16px;
  display: flex;
  flex-direction: column;
}

.room-title-floor {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
}

.room-title-floor h4 {
  font-size: 15px;
  font-weight: 800;
  color: var(--text-main);
}

.floor-badge {
  font-size: 10px;
  background: var(--bg-subtle);
  color: var(--text-muted);
  padding: 2px 7px;
  border-radius: 4px;
}

.room-specs-list {
  display: flex;
  flex-direction: column;
  gap: 4px;
  font-size: 11.5px;
  color: var(--text-secondary);
  margin-bottom: 12px;
}

.breakfast-tag {
  color: var(--success);
  font-weight: 700;
}

.room-price-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-top: 1px solid var(--border-light);
  padding-top: 10px;
  margin-bottom: 12px;
}

.price-lbl {
  font-size: 11px;
  color: var(--text-muted);
}

.price-num {
  font-size: 15px;
  font-weight: 800;
  color: var(--primary);
}

.room-actions-bar {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 8px;
}

.btn-sm {
  padding: 6px;
  font-size: 11.5px;
  text-align: center;
}

.btn-delete {
  border-color: var(--danger);
  color: var(--danger);
}

.btn-delete:hover {
  background: var(--danger);
  color: #fff;
}

@media (max-width: 900px) {
  .rooms-management-grid {
    grid-template-columns: 1fr;
  }
}
</style>
