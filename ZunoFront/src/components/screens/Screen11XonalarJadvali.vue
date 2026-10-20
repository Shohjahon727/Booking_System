<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">11</span> XONALAR JADVALI
      </span>
      <button class="btn-primary add-room-btn" @click="openAddRoomModal">
        + Xona qo'shish
      </button>
    </div>

    <!-- Rooms Table -->
    <div class="table-responsive-box">
      <table class="data-table">
        <thead>
          <tr>
            <th>Qavat/Xona</th>
            <th>Xona nomi</th>
            <th>Turi</th>
            <th>Narxi</th>
            <th>Status</th>
            <th>Amallar</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="r in state.rooms" :key="r.id">
            <td class="floor-cell">{{ r.floor }}</td>
            <td class="room-name-cell"><b>{{ r.name }}</b></td>
            <td>{{ r.type }}</td>
            <td class="price-cell">{{ formatSum(r.pricePerNight) }}</td>
            <td>
              <span class="status-badge status-confirmed">{{ r.status }}</span>
            </td>
            <td class="actions-cell">
              <button class="action-btn-mini" @click="handleEdit(r)">✏️</button>
              <button class="action-btn-mini del" @click="handleDelete(r.id)">🗑️</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { state } from '../../store'

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function openAddRoomModal() {
  state.modals.addRoom = true
}

function handleEdit(room) {
  alert(`${room.name} ma'lumotlarini tahrirlash oynasi ochildi.`)
}

function handleDelete(id) {
  if (confirm("Ushbu xonani o'chirishni tasdiqlaysizmi?")) {
    state.rooms = state.rooms.filter(r => r.id !== id)
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

.add-room-btn {
  padding: 4px 10px;
  font-size: 9.5px;
}

.table-responsive-box {
  overflow-x: auto;
  flex: 1;
}

.floor-cell {
  color: var(--text-muted);
  font-size: 9px;
  white-space: nowrap;
}

.room-name-cell {
  color: var(--text-main);
  white-space: nowrap;
}

.price-cell {
  font-weight: 700;
  color: var(--primary);
  white-space: nowrap;
}

.actions-cell {
  white-space: nowrap;
}

.action-btn-mini {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 2px 5px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 9px;
  margin-right: 3px;
}

.action-btn-mini.del:hover {
  background: var(--danger-bg);
  border-color: var(--danger);
}
</style>
