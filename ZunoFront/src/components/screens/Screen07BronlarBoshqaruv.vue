<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">07</span> BRONLARNI BOSHQARISH
      </span>
      <span class="card-badge badge-active">Egasi paneli</span>
    </div>

    <!-- Filter tabs -->
    <div class="booking-filter-tabs-row">
      <button
        v-for="t in ['Barchasi', 'Pending', 'Confirmed', 'Cancelled', 'Completed']"
        :key="t"
        class="tab-btn-mini"
        :class="{ active: currentTab === t }"
        @click="currentTab = t"
      >
        {{ t }}
      </button>
    </div>

    <!-- Bookings Table -->
    <div class="table-responsive-box">
      <table class="data-table">
        <thead>
          <tr>
            <th>Mijoz</th>
            <th>Xona turi</th>
            <th>Kirish sanasi</th>
            <th>Chiqish sanasi</th>
            <th>Jami</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="b in filteredBookings" :key="b.id">
            <td class="client-name-cell">
              <b>{{ b.guestName }}</b>
            </td>
            <td>{{ b.roomName }}</td>
            <td>{{ b.checkIn }}</td>
            <td>{{ b.checkOut }}</td>
            <td class="sum-cell">{{ formatSum(b.totalPrice) }}</td>
            <td>
              <span :class="['status-badge', b.status]">
                {{ b.status }}
              </span>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Bottom Add Booking Button -->
    <div class="action-footer-row">
      <button class="btn-primary add-bron-btn" @click="openAddBookingModal">
        + Bron qo'shish
      </button>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { state } from '../../store'

const currentTab = ref('Barchasi')

const filteredBookings = computed(() => {
  if (currentTab.value === 'Barchasi') return state.bookings
  return state.bookings.filter(b => b.status === currentTab.value)
})

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function openAddBookingModal() {
  state.modals.addBooking = true
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

.booking-filter-tabs-row {
  display: flex;
  gap: 4px;
  margin-bottom: 8px;
  overflow-x: auto;
}

.tab-btn-mini {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-muted);
  font-size: 8.5px;
  font-weight: 700;
  padding: 3px 7px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  white-space: nowrap;
}

.tab-btn-mini.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.table-responsive-box {
  overflow-x: auto;
  flex: 1;
}

.client-name-cell {
  color: var(--text-main);
  white-space: nowrap;
}

.sum-cell {
  font-weight: 700;
  color: var(--primary);
}

.action-footer-row {
  margin-top: 8px;
  display: flex;
  justify-content: flex-end;
}

.add-bron-btn {
  background: var(--primary);
  padding: 6px 14px;
  font-size: 10px;
}
</style>
