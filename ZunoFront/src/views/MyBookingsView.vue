<template>
  <div class="my-bookings-page">
    <div class="content-container">
      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Mening Bronlarim</h2>
          <p>Barcha faol, yakunlangan va rejalashtirilgan mehmonxona bronlaringiz tarixi</p>
        </div>
        <div class="page-actions-box">
          <router-link to="/hotels" class="btn-primary">
            + Yangi mehmonxona bron qilish
          </router-link>
        </div>
      </div>

      <!-- Status Filter Tabs -->
      <div class="status-tabs-row">
        <button
          v-for="f in ['Barchasi', 'Confirmed', 'Pending', 'Cancelled', 'Completed']"
          :key="f"
          class="tab-pill"
          :class="{ active: activeFilter === f }"
          @click="activeFilter = f"
        >
          {{ getFilterLabel(f) }}
          <span class="count-tag">{{ getCount(f) }}</span>
        </button>
      </div>

      <!-- Bookings Cards List -->
      <div v-if="filteredBookings.length === 0" class="empty-bookings-box card">
        <span class="empty-icon">📋</span>
        <h3>Ushbu toifada bronlar topilmadi</h3>
        <p>Boshqa toifani tanlang yoki yangi qulay mehmonxonani bron qiling.</p>
        <router-link to="/hotels" class="btn-primary" style="margin-top: 14px;">
          Mehmonxonalarni ko'rish
        </router-link>
      </div>

      <div v-else class="bookings-grid-list">
        <div
          v-for="b in filteredBookings"
          :key="b.id"
          class="booking-full-card card"
        >
          <div class="booking-card-image">
            <img :src="b.image" :alt="b.hotelName" />
            <span :class="['card-status-badge', b.status]">{{ b.status }}</span>
          </div>

          <div class="booking-card-content">
            <div class="booking-top-line">
              <div>
                <span class="booking-id-txt">Bron ID: {{ b.id }}</span>
                <h3 class="booking-hotel-title">{{ b.hotelName }}</h3>
                <div class="booking-room-type">🛏️ {{ b.roomName }} ({{ b.roomType }})</div>
              </div>

              <div class="booking-price-side">
                <span class="p-sub-txt">Jami to'lov:</span>
                <div class="p-total-val">{{ formatSum(b.totalPrice) }} so'm</div>
              </div>
            </div>

            <div class="booking-details-bar">
              <div class="detail-col">
                <span class="d-label">Kirish sanasi:</span>
                <span class="d-val">📅 {{ b.checkIn }}</span>
              </div>
              <div class="detail-col">
                <span class="d-label">Chiqish sanasi:</span>
                <span class="d-val">📅 {{ b.checkOut }}</span>
              </div>
              <div class="detail-col">
                <span class="d-label">Muddat:</span>
                <span class="d-val">{{ b.nights }} kecha • {{ b.guests }} mehmon</span>
              </div>
              <div class="detail-col">
                <span class="d-label">Mehmon:</span>
                <span class="d-val">👤 {{ b.guestName }}</span>
              </div>
            </div>

            <div class="booking-actions-footer">
              <button class="btn-secondary" @click="openVoucher(b)">
                📄 Vaucherni ko'rish
              </button>

              <button
                v-if="b.status === 'Confirmed' || b.status === 'Pending'"
                class="btn-outline btn-cancel"
                @click="cancelBooking(b.id)"
              >
                Bronni bekor qilish
              </button>

              <button
                v-if="b.status === 'Completed'"
                class="btn-secondary"
                @click="leaveReview(b)"
              >
                ⭐ Sharh qoldirish
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Voucher Modal -->
    <div v-if="voucherModalOpen" class="modal-overlay" @click.self="voucherModalOpen = false">
      <div class="modal-box voucher-modal-box">
        <div class="modal-header">
          <h3 class="modal-title">Rasmiy Bron Vaucheri</h3>
          <button class="modal-close-btn" @click="voucherModalOpen = false">&times;</button>
        </div>
        <div class="modal-body voucher-modal-body">
          <div class="voucher-top-brand">
            <h2>ZunoBookingSystem</h2>
            <span>Elektron Bron Voucheri</span>
          </div>

          <div class="voucher-info-grid">
            <div>
              <label>Vaucher ID</label>
              <div class="v-bold">{{ selectedVoucher.id }}</div>
            </div>
            <div>
              <label>Status</label>
              <div class="v-bold status-confirmed">{{ selectedVoucher.status }}</div>
            </div>
            <div>
              <label>Mehmonxona</label>
              <div class="v-bold">{{ selectedVoucher.hotelName }}</div>
            </div>
            <div>
              <label>Xona toifasi</label>
              <div class="v-bold">{{ selectedVoucher.roomName }}</div>
            </div>
            <div>
              <label>Kirish - Chiqish</label>
              <div class="v-bold">{{ selectedVoucher.checkIn }} — {{ selectedVoucher.checkOut }}</div>
            </div>
            <div>
              <label>Mehmon ismi</label>
              <div class="v-bold">{{ selectedVoucher.guestName }}</div>
            </div>
            <div>
              <label>Jami to'lov</label>
              <div class="v-bold">{{ formatSum(selectedVoucher.totalPrice) }} so'm</div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button class="btn-secondary" @click="voucherModalOpen = false">Yopish</button>
          <button class="btn-primary" @click="handlePrintVoucher">🖨️ Chop etish</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { state, updateBookingStatus } from '../store'

const activeFilter = ref('Barchasi')
const voucherModalOpen = ref(false)
const selectedVoucher = ref({})

const filteredBookings = computed(() => {
  if (activeFilter.value === 'Barchasi') return state.bookings
  return state.bookings.filter(b => b.status === activeFilter.value)
})

function getCount(f) {
  if (f === 'Barchasi') return state.bookings.length
  return state.bookings.filter(b => b.status === f).length
}

function getFilterLabel(f) {
  const map = {
    'Barchasi': 'Barchasi',
    'Confirmed': 'Tasdiqlangan',
    'Pending': 'Kutilmoqda',
    'Cancelled': 'Bekor qilingan',
    'Completed': 'Yakunlangan'
  }
  return map[f] || f
}

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function openVoucher(b) {
  selectedVoucher.value = b
  voucherModalOpen.value = true
}

function handlePrintVoucher() {
  window.print()
}

function cancelBooking(id) {
  if (confirm("Ushbu bronni bekor qilishni tasdiqlaysizmi?")) {
    updateBookingStatus(id, 'Cancelled')
  }
}

function leaveReview(b) {
  alert(`${b.hotelName} haqida o'z sharhingizni qoldiring.`)
}
</script>

<style scoped>
.my-bookings-page {
  padding-bottom: 50px;
}

.status-tabs-row {
  display: flex;
  gap: 8px;
  margin-bottom: 20px;
  overflow-x: auto;
  padding-bottom: 4px;
}

.tab-pill {
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  color: var(--text-secondary);
  font-size: 12px;
  font-weight: 700;
  padding: 6px 14px;
  border-radius: var(--radius-full);
  cursor: pointer;
  display: flex;
  align-items: center;
  gap: 6px;
  transition: all 0.15s;
}

.tab-pill:hover {
  border-color: var(--primary);
  color: var(--primary);
}

.tab-pill.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.count-tag {
  background: rgba(0, 0, 0, 0.1);
  padding: 1px 6px;
  border-radius: 10px;
  font-size: 10px;
}

.tab-pill.active .count-tag {
  background: rgba(255, 255, 255, 0.25);
  color: #fff;
}

.bookings-grid-list {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.booking-full-card {
  display: grid;
  grid-template-columns: 200px 1fr;
  padding: 0;
  overflow: hidden;
}

.booking-card-image {
  position: relative;
  background: #1e293b;
}

.booking-card-image img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.card-status-badge {
  position: absolute;
  top: 10px;
  left: 10px;
  font-size: 10px;
  font-weight: 800;
  padding: 3px 8px;
  border-radius: 4px;
  text-transform: uppercase;
}

.card-status-badge.Confirmed { background: #10b981; color: #fff; }
.card-status-badge.Pending { background: #f59e0b; color: #fff; }
.card-status-badge.Completed { background: #3b82f6; color: #fff; }
.card-status-badge.Cancelled { background: #ef4444; color: #fff; }

.booking-card-content {
  padding: 18px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.booking-top-line {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 12px;
}

.booking-id-txt {
  font-size: 10.5px;
  font-weight: 700;
  color: var(--primary);
}

.booking-hotel-title {
  font-size: 16px;
  font-weight: 800;
  color: var(--text-main);
  margin-top: 2px;
}

.booking-room-type {
  font-size: 12px;
  color: var(--text-secondary);
  font-weight: 600;
  margin-top: 2px;
}

.booking-price-side {
  text-align: right;
}

.p-sub-txt {
  font-size: 10.5px;
  color: var(--text-muted);
}

.p-total-val {
  font-size: 17px;
  font-weight: 800;
  color: var(--primary);
}

.booking-details-bar {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 12px;
  background: var(--bg-subtle);
  border-radius: var(--radius-sm);
  padding: 10px 14px;
  margin-bottom: 16px;
}

.detail-col {
  display: flex;
  flex-direction: column;
}

.d-label {
  font-size: 10px;
  color: var(--text-muted);
}

.d-val {
  font-size: 12px;
  font-weight: 700;
  color: var(--text-main);
  margin-top: 2px;
}

.booking-actions-footer {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  border-top: 1px solid var(--border-light);
  padding-top: 12px;
}

.btn-cancel {
  border-color: var(--danger);
  color: var(--danger);
}

.btn-cancel:hover {
  background: var(--danger);
  color: #fff;
}

.empty-bookings-box {
  text-align: center;
  padding: 40px;
}

.empty-icon {
  font-size: 40px;
  display: block;
  margin-bottom: 10px;
}

/* Voucher modal */
.voucher-modal-box {
  max-width: 500px;
}

.voucher-top-brand {
  text-align: center;
  border-bottom: 2px dashed var(--border-subtle);
  padding-bottom: 14px;
  margin-bottom: 16px;
}

.voucher-top-brand h2 {
  font-size: 18px;
  font-weight: 800;
  color: var(--primary);
}

.voucher-info-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
}

.voucher-info-grid label {
  font-size: 10px;
  color: var(--text-muted);
  text-transform: uppercase;
}

.v-bold {
  font-size: 13px;
  font-weight: 700;
  color: var(--text-main);
}

@media (max-width: 768px) {
  .booking-full-card {
    grid-template-columns: 1fr;
  }
  .booking-card-image {
    height: 160px;
  }
  .booking-details-bar {
    grid-template-columns: 1fr 1fr;
  }
}
</style>
