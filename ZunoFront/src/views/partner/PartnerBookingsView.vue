<template>
  <div class="dashboard-layout">
    <div class="dashboard-sidebar-wrap">
      <PartnerSidebar />
    </div>

    <div class="dashboard-main-area">
      <div class="page-top-bar">
        <div class="page-title-box">
          <h2>Bronlarni Boshqarish</h2>
          <p>Mehmonxonangizga kelib tushgan barcha buyurtmalar va ularning holati</p>
        </div>
        <div class="page-actions-box">
          <button class="btn-primary" @click="openAddBookingModal">+ Yangi bron qo'shish</button>
        </div>
      </div>

      <!-- Filters & Search Toolbar -->
      <div class="card toolbar-card">
        <div class="toolbar-left">
          <div class="filter-tabs-pills">
            <button
              v-for="t in ['Barchasi', 'Confirmed', 'Pending', 'Cancelled', 'Completed']"
              :key="t"
              class="t-pill"
              :class="{ active: currentTab === t }"
              @click="currentTab = t"
            >
              {{ t }}
            </button>
          </div>
        </div>

        <div class="toolbar-right">
          <input
            type="text"
            v-model="searchQuery"
            placeholder="Mijoz ismi yoki ID..."
            class="form-input search-box"
          />
        </div>
      </div>

      <!-- Bookings Table -->
      <div class="card">
        <div class="table-responsive">
          <table class="data-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Mijoz</th>
                <th>Xona nomi</th>
                <th>Kirish — Chiqish</th>
                <th>Mehmonlar</th>
                <th>Jami</th>
                <th>Status</th>
                <th>Amallar</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="b in filteredBookings" :key="b.id">
                <td><b>{{ b.id }}</b></td>
                <td>
                  <div class="guest-cell">
                    <span class="g-name">{{ b.guestName }}</span>
                    <span class="g-phone">{{ b.guestPhone }}</span>
                  </div>
                </td>
                <td>{{ b.roomName }}</td>
                <td>{{ b.checkIn }} — {{ b.checkOut }} ({{ b.nights }} kecha)</td>
                <td>{{ b.guests }} kishi</td>
                <td class="sum-col">{{ formatSum(b.totalPrice) }} so'm</td>
                <td>
                  <span :class="['status-badge', b.status]">{{ b.status }}</span>
                </td>
                <td>
                  <div class="row-action-btns">
                    <button
                      v-if="b.status === 'Pending'"
                      class="btn-action confirm-btn"
                      @click="updateStatus(b.id, 'Confirmed')"
                      title="Tasdiqlash"
                    >
                      ✓
                    </button>
                    <button
                      v-if="b.status === 'Confirmed' || b.status === 'Pending'"
                      class="btn-action cancel-btn"
                      @click="updateStatus(b.id, 'Cancelled')"
                      title="Bekor qilish"
                    >
                      ✕
                    </button>
                    <button class="btn-action view-btn" @click="viewDetails(b)" title="Batafsil">
                      👁️
                    </button>
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
import { ref, computed } from 'vue'
import { state, updateBookingStatus } from '../../store'
import PartnerSidebar from '../../components/layout/PartnerSidebar.vue'

const currentTab = ref('Barchasi')
const searchQuery = ref('')

const filteredBookings = computed(() => {
  return state.bookings.filter(b => {
    const matchTab = currentTab.value === 'Barchasi' || b.status === currentTab.value
    const matchSearch = !searchQuery.value ||
      b.guestName.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
      b.id.toLowerCase().includes(searchQuery.value.toLowerCase())
    return matchTab && matchSearch
  })
})

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function updateStatus(id, newStatus) {
  updateBookingStatus(id, newStatus)
}

function viewDetails(b) {
  alert(`Bron ma'lumotlari: ${b.guestName} (${b.guestPhone})\nXona: ${b.roomName}\nJami to'lov: ${formatSum(b.totalPrice)} so'm`)
}

function openAddBookingModal() {
  state.modals.addBooking = true
}
</script>

<style scoped>
.toolbar-card {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 18px;
  flex-direction: row;
}

.filter-tabs-pills {
  display: flex;
  gap: 6px;
}

.t-pill {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-muted);
  font-size: 11px;
  font-weight: 700;
  padding: 6px 12px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  transition: all 0.15s;
}

.t-pill.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.search-box {
  width: 240px;
}

.guest-cell {
  display: flex;
  flex-direction: column;
}

.g-name {
  font-weight: 700;
  color: var(--text-main);
}

.g-phone {
  font-size: 11px;
  color: var(--text-muted);
}

.sum-col {
  font-weight: 800;
  color: var(--primary);
}

.row-action-btns {
  display: flex;
  gap: 4px;
}

.btn-action {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 28px;
  height: 28px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.12s;
}

.confirm-btn:hover { background: var(--success-bg); color: var(--success); border-color: var(--success); }
.cancel-btn:hover { background: var(--danger-bg); color: var(--danger); border-color: var(--danger); }
.view-btn:hover { background: var(--primary-light); color: var(--primary); border-color: var(--primary); }

@media (max-width: 900px) {
  .toolbar-card {
    flex-direction: column;
    align-items: stretch;
    gap: 12px;
  }
}
</style>
