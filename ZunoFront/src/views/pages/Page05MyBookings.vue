<template>
  <div class="page-container page-05-my-bookings">
    <div class="content-container">
      <!-- Screen 05 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 05</span>
        <span class="s-title">BRON DILARI (Mening Bronlarim)</span>
        <span class="s-role">Mijoz kabineti</span>
      </div>

      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Mening Bronlarim Ro'yxati</h2>
          <p>Barcha buyurtmalaringiz, tasdiqlangan vaucherlar va to'lov hisobotlari</p>
        </div>
        <div class="page-actions-box">
          <router-link to="/rooms" class="btn-primary">
            + Yangi xona bron qilish
          </router-link>
        </div>
      </div>

      <!-- Filter Tabs -->
      <div class="status-tabs-row">
        <button
          v-for="t in ['Barchasi', 'Confirmed', 'Pending', 'Cancelled', 'Completed']"
          :key="t"
          class="tab-btn-pill"
          :class="{ active: currentTab === t }"
          @click="currentTab = t"
        >
          {{ getTabLabel(t) }}
          <span class="count-pill">{{ getCount(t) }}</span>
        </button>
      </div>

      <!-- Bookings Cards List -->
      <div class="bookings-cards-column">
        <div
          v-for="b in filteredBookings"
          :key="b.id"
          class="card booking-entry-card"
        >
          <div class="entry-img-left">
            <img :src="b.image" :alt="b.hotelName" />
            <span :class="['status-tag-ribbon', b.status]">{{ b.status }}</span>
          </div>

          <div class="entry-content-right">
            <div class="entry-header-row">
              <div>
                <span class="b-code">Bron raqami: {{ b.id }}</span>
                <h3 class="b-hotel">{{ b.hotelName }}</h3>
                <div class="b-room">🛏️ {{ b.roomName }} ({{ b.roomType }})</div>
              </div>

              <div class="b-price-box">
                <span class="p-sub">Jami to'lov:</span>
                <div class="p-val">{{ formatSum(b.totalPrice) }} so'm</div>
              </div>
            </div>

            <div class="entry-info-strip">
              <div class="info-cell">
                <span class="i-lbl">Kirish sanasi:</span>
                <span class="i-val">📅 {{ b.checkIn }}</span>
              </div>
              <div class="info-cell">
                <span class="i-lbl">Chiqish sanasi:</span>
                <span class="i-val">📅 {{ b.checkOut }}</span>
              </div>
              <div class="info-cell">
                <span class="i-lbl">Muddat:</span>
                <span class="i-val">{{ b.nights }} kecha • {{ b.guests }} mehmon</span>
              </div>
              <div class="info-cell">
                <span class="i-lbl">Mehmon:</span>
                <span class="i-val">👤 {{ b.guestName }}</span>
              </div>
            </div>

            <div class="entry-actions-row">
              <button class="btn-secondary btn-sm" @click="openVoucher(b)">
                📄 Vaucherni ko'rish
              </button>

              <button
                v-if="b.status === 'Confirmed' || b.status === 'Pending'"
                class="btn-outline btn-sm btn-cancel"
                @click="cancelBooking(b.id)"
              >
                Bronni bekor qilish
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Voucher Modal Dialog -->
    <div v-if="showVoucherModal" class="modal-overlay" @click.self="showVoucherModal = false">
      <div class="modal-box voucher-modal-view">
        <div class="modal-header">
          <h3 class="modal-title">Elektron Bron Voucheri</h3>
          <button class="modal-close-btn" @click="showVoucherModal = false">&times;</button>
        </div>
        <div class="modal-body voucher-inner-body">
          <div class="v-header-banner">
            <h3>ZunoBookingSystem</h3>
            <p>Rasmiy Bron Tasdiqnomasi</p>
          </div>
          <div class="v-rows-grid">
            <div><label>Bron ID:</label><b>{{ selectedBooking.id }}</b></div>
            <div><label>Holati:</label><b class="status-confirmed">{{ selectedBooking.status }}</b></div>
            <div><label>Mehmonxona:</label><b>{{ selectedBooking.hotelName }}</b></div>
            <div><label>Xona toifasi:</label><b>{{ selectedBooking.roomName }}</b></div>
            <div><label>Muddat:</label><b>{{ selectedBooking.checkIn }} — {{ selectedBooking.checkOut }}</b></div>
            <div><label>Mehmon:</label><b>{{ selectedBooking.guestName }}</b></div>
            <div><label>Jami to'lov:</label><b>{{ formatSum(selectedBooking.totalPrice) }} so'm</b></div>
          </div>
        </div>
        <div class="modal-footer">
          <button class="btn-secondary" @click="showVoucherModal = false">Yopish</button>
          <button class="btn-primary" @click="handlePrint">🖨️ Chop etish</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { state, updateBookingStatus } from '../../store'

const currentTab = ref('Barchasi')
const showVoucherModal = ref(false)
const selectedBooking = ref({})

const filteredBookings = computed(() => {
  if (currentTab.value === 'Barchasi') return state.bookings
  return state.bookings.filter(b => b.status === currentTab.value)
})

function getTabLabel(t) {
  const map = {
    'Barchasi': 'Barchasi',
    'Confirmed': 'Confirmed',
    'Pending': 'Pending',
    'Cancelled': 'Cancelled',
    'Completed': 'Completed'
  }
  return map[t] || t
}

function getCount(t) {
  if (t === 'Barchasi') return state.bookings.length
  return state.bookings.filter(b => b.status === t).length
}

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function openVoucher(b) {
  selectedBooking.value = b
  showVoucherModal.value = true
}

function cancelBooking(id) {
  if (confirm("Ushbu bronni bekor qilishni xohlaysizmi?")) {
    updateBookingStatus(id, 'Cancelled')
  }
}

function handlePrint() {
  window.print()
}
</script>

<style scoped>
.page-05-my-bookings {
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

.status-tabs-row {
  display: flex;
  gap: 8px;
  margin-bottom: 20px;
}

.tab-btn-pill {
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

.tab-btn-pill.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.count-pill {
  background: rgba(0, 0, 0, 0.08);
  padding: 1px 6px;
  border-radius: 10px;
  font-size: 10px;
}

.tab-btn-pill.active .count-pill {
  background: rgba(255, 255, 255, 0.25);
  color: #fff;
}

.bookings-cards-column {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.booking-entry-card {
  display: grid;
  grid-template-columns: 220px 1fr;
  padding: 0;
  overflow: hidden;
}

.entry-img-left {
  position: relative;
  background: #1e293b;
}

.entry-img-left img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.status-tag-ribbon {
  position: absolute;
  top: 10px;
  left: 10px;
  font-size: 10px;
  font-weight: 800;
  padding: 3px 8px;
  border-radius: 4px;
  text-transform: uppercase;
}

.status-tag-ribbon.Confirmed { background: #10b981; color: #fff; }
.status-tag-ribbon.Pending { background: #f59e0b; color: #fff; }
.status-tag-ribbon.Completed { background: #3b82f6; color: #fff; }
.status-tag-ribbon.Cancelled { background: #ef4444; color: #fff; }

.entry-content-right {
  padding: 18px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.entry-header-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 12px;
}

.b-code {
  font-size: 11px;
  font-weight: 700;
  color: var(--primary);
}

.b-hotel {
  font-size: 16px;
  font-weight: 800;
  color: var(--text-main);
}

.b-room {
  font-size: 12px;
  color: var(--text-muted);
  font-weight: 600;
}

.b-price-box {
  text-align: right;
}

.p-sub {
  font-size: 10.5px;
  color: var(--text-muted);
}

.p-val {
  font-size: 17px;
  font-weight: 800;
  color: var(--primary);
}

.entry-info-strip {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 12px;
  background: var(--bg-subtle);
  border-radius: var(--radius-sm);
  padding: 10px 14px;
  margin-bottom: 14px;
}

.i-lbl {
  font-size: 10px;
  color: var(--text-muted);
  display: block;
}

.i-val {
  font-size: 12px;
  font-weight: 700;
  color: var(--text-main);
  margin-top: 2px;
  display: block;
}

.entry-actions-row {
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  border-top: 1px solid var(--border-light);
  padding-top: 10px;
}

.btn-cancel {
  border-color: var(--danger);
  color: var(--danger);
}

.btn-cancel:hover {
  background: var(--danger);
  color: #fff;
}

/* Modal */
.voucher-modal-view {
  max-width: 480px;
}

.v-header-banner {
  text-align: center;
  border-bottom: 2px dashed var(--border-subtle);
  padding-bottom: 12px;
  margin-bottom: 14px;
}

.v-header-banner h3 {
  font-size: 18px;
  font-weight: 800;
  color: var(--primary);
}

.v-rows-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
  font-size: 12.5px;
}

.v-rows-grid label {
  font-size: 10px;
  color: var(--text-muted);
  display: block;
}

@media (max-width: 768px) {
  .booking-entry-card {
    grid-template-columns: 1fr;
  }
  .entry-img-left {
    height: 160px;
  }
  .entry-info-strip {
    grid-template-columns: 1fr 1fr;
  }
}
</style>
