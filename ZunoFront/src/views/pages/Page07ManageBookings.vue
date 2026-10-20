<template>
  <div class="page-container page-07-manage-bookings">
    <div class="content-container">
      <!-- Screen 07 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 07</span>
        <span class="s-title">BRONLARNI BOSHQARISH</span>
        <span class="s-role">Hotel Egasi paneli</span>
      </div>

      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Bronlarni Boshqarish va Tasdiqlash</h2>
          <p>Mijozlardan kelib tushgan buyurtmalar, ularning muddatlari va to'lov hisoblari</p>
        </div>
        <div class="page-actions-box">
          <button class="btn-primary" @click="openAddBookingModal">+ Yangi bron qo'shish</button>
        </div>
      </div>

      <!-- Filter Tabs Card -->
      <div class="card toolbar-strip">
        <div class="tabs-pills-row">
          <button
            v-for="t in ['Barchasi', 'Pending', 'Confirmed', 'Cancelled', 'Completed']"
            :key="t"
            class="t-tab"
            :class="{ active: currentTab === t }"
            @click="currentTab = t"
          >
            {{ t }}
          </button>
        </div>

        <input
          type="text"
          v-model="search"
          placeholder="Mijoz ismi yoki ID..."
          class="form-input search-box-field"
        />
      </div>

      <!-- Bookings Management Table Card -->
      <div class="card">
        <div class="table-responsive">
          <table class="data-table">
            <thead>
              <tr>
                <th>Mijoz</th>
                <th>Xona turi</th>
                <th>Kirish sanasi</th>
                <th>Chiqish sanasi</th>
                <th>Jami to'lov</th>
                <th>Status</th>
                <th>Amallar</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="b in filteredBookings" :key="b.id">
                <td>
                  <div class="client-meta-box">
                    <span class="c-name">{{ b.guestName }}</span>
                    <span class="c-phone">{{ b.guestPhone }}</span>
                  </div>
                </td>
                <td><b>{{ b.roomName }}</b> ({{ b.roomType }})</td>
                <td>📅 {{ b.checkIn }}</td>
                <td>📅 {{ b.checkOut }}</td>
                <td class="sum-highlight">{{ formatSum(b.totalPrice) }} so'm</td>
                <td>
                  <span :class="['status-badge', b.status]">{{ b.status }}</span>
                </td>
                <td>
                  <div class="actions-buttons-cell">
                    <button
                      v-if="b.status === 'Pending'"
                      class="btn-icon-action btn-confirm"
                      @click="updateStatus(b.id, 'Confirmed')"
                      title="Tasdiqlash"
                    >
                      ✓
                    </button>
                    <button
                      v-if="b.status === 'Confirmed' || b.status === 'Pending'"
                      class="btn-icon-action btn-reject"
                      @click="updateStatus(b.id, 'Cancelled')"
                      title="Bekor qilish"
                    >
                      ✕
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

const currentTab = ref('Barchasi')
const search = ref('')

const filteredBookings = computed(() => {
  return state.bookings.filter(b => {
    const matchTab = currentTab.value === 'Barchasi' || b.status === currentTab.value
    const matchSearch = !search.value ||
      b.guestName.toLowerCase().includes(search.value.toLowerCase()) ||
      b.roomName.toLowerCase().includes(search.value.toLowerCase())
    return matchTab && matchSearch
  })
})

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function updateStatus(id, newStatus) {
  updateBookingStatus(id, newStatus)
}

function openAddBookingModal() {
  state.modals.addBooking = true
}
</script>

<style scoped>
.page-07-manage-bookings {
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
  color: #d97706;
  font-weight: 600;
}

.toolbar-strip {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 18px;
  margin-bottom: 20px;
  flex-direction: row;
}

.tabs-pills-row {
  display: flex;
  gap: 6px;
}

.t-tab {
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

.t-tab.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.search-box-field {
  width: 240px;
}

.client-meta-box {
  display: flex;
  flex-direction: column;
}

.c-name {
  font-weight: 700;
  color: var(--text-main);
}

.c-phone {
  font-size: 11px;
  color: var(--text-muted);
}

.sum-highlight {
  font-weight: 800;
  color: var(--primary);
}

.actions-buttons-cell {
  display: flex;
  gap: 6px;
}

.btn-icon-action {
  width: 28px;
  height: 28px;
  border-radius: 4px;
  border: 1px solid var(--border-subtle);
  background: var(--bg-subtle);
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  font-size: 12px;
  font-weight: 800;
}

.btn-confirm:hover { background: var(--success-bg); color: var(--success); border-color: var(--success); }
.btn-reject:hover { background: var(--danger-bg); color: var(--danger); border-color: var(--danger); }

@media (max-width: 800px) {
  .toolbar-strip {
    flex-direction: column;
    align-items: stretch;
    gap: 12px;
  }
}
</style>
