<template>
  <div class="page-container page-11-rooms-table">
    <div class="content-container">
      <!-- Screen 11 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 11</span>
        <span class="s-title">XONALAR JADVALI</span>
        <span class="s-role">Xonalar Fondi va Narxlar</span>
      </div>

      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Xonalar Fondi va Boshqaruv Jadvali</h2>
          <p>Barcha xonalar ro'yxati, sig'imi, narxlari va faol statusi</p>
        </div>
        <div class="page-actions-box">
          <button class="btn-primary" @click="openAddRoomModal">+ Xona qo'shish</button>
        </div>
      </div>

      <!-- Rooms Table (From Mockup 11) -->
      <div class="card">
        <div class="table-responsive">
          <table class="data-table">
            <thead>
              <tr>
                <th>Qavat / Xona</th>
                <th>Xona nomi</th>
                <th>Turi</th>
                <th>Narxi</th>
                <th>Status</th>
                <th>Amallar</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="r in state.rooms" :key="r.id">
                <td><span class="floor-code">{{ r.floor }}</span></td>
                <td><b>{{ r.name }}</b></td>
                <td>{{ r.type }}</td>
                <td class="price-val-highlight">{{ formatSum(r.pricePerNight) }} so'm</td>
                <td>
                  <span class="status-badge status-confirmed">{{ r.status }}</span>
                </td>
                <td>
                  <div class="actions-buttons-row">
                    <button class="action-btn" @click="editRoom(r)" title="Tahrirlash">✏️</button>
                    <button class="action-btn btn-del" @click="deleteRoom(r.id)" title="O'chirish">🗑️</button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
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

function editRoom(room) {
  alert(`${room.name} ma'lumotlarini tahrirlash oynasi.`)
}

function deleteRoom(id) {
  if (confirm("Ushbu xonani ro'yxatdan o'chirishni xohlaysizmi?")) {
    state.rooms = state.rooms.filter(r => r.id !== id)
  }
}
</script>

<style scoped>
.page-11-rooms-table {
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

.floor-code {
  font-size: 11px;
  color: var(--text-muted);
  background: var(--bg-subtle);
  padding: 2px 6px;
  border-radius: 3px;
}

.price-val-highlight {
  font-weight: 800;
  color: var(--primary);
}

.actions-buttons-row {
  display: flex;
  gap: 4px;
}

.action-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 28px;
  height: 28px;
  border-radius: 4px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
}

.btn-del:hover {
  background: var(--danger-bg);
  color: var(--danger);
  border-color: var(--danger);
}
</style>
