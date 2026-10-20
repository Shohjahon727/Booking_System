<template>
  <div class="checkout-page">
    <div class="content-container">
      <!-- Top Step indicator -->
      <div class="checkout-steps-bar">
        <div class="step-item active">
          <span class="step-num">1</span>
          <span>Mehmon Ma'lumotlari</span>
        </div>
        <div class="step-connector"></div>
        <div class="step-item active">
          <span class="step-num">2</span>
          <span>Xizmatlar & To'lov</span>
        </div>
        <div class="step-connector"></div>
        <div class="step-item" :class="{ active: isPaid }">
          <span class="step-num">3</span>
          <span>Tasdiqlash & Vaucher</span>
        </div>
      </div>

      <!-- Main Layout -->
      <div v-if="!isPaid" class="checkout-layout">
        <!-- Left: Form Sections -->
        <div class="checkout-form-column">
          <!-- 1. Guest Information Card -->
          <div class="card checkout-section-card">
            <h3 class="section-card-title">1. Mehmon ma'lumotlari</h3>
            <div class="form-grid-2">
              <div class="form-group">
                <label>To'liq ism va familiya *</label>
                <input
                  type="text"
                  v-model="state.checkout.guestName"
                  placeholder="Alisher Normatov"
                  class="form-input"
                  required
                />
              </div>

              <div class="form-group">
                <label>Telefon raqam *</label>
                <input
                  type="text"
                  v-model="state.checkout.guestPhone"
                  placeholder="+998 90 123 45 67"
                  class="form-input"
                  required
                />
              </div>
            </div>

            <div class="form-group">
              <label>Elektron pochta (vaucher yuborish uchun) *</label>
              <input
                type="email"
                v-model="state.checkout.guestEmail"
                placeholder="alisher@mail.com"
                class="form-input"
                required
              />
            </div>
          </div>

          <!-- 2. Add-on Services -->
          <div class="card checkout-section-card">
            <h3 class="section-card-title">2. Qo'shimcha qulayliklar va xizmatlar</h3>
            <div class="addons-list-items">
              <label class="addon-row-box" :class="{ selected: state.checkout.addons.breakfast }">
                <input type="checkbox" v-model="state.checkout.addons.breakfast" />
                <div class="addon-txt">
                  <div class="addon-name">🍳 Shved stoli nonushta (barcha kunlar uchun)</div>
                  <div class="addon-desc">Har kuni soat 07:00 dan 11:00 gacha xilma-xil issiq nonushta</div>
                </div>
                <div class="addon-cost">+150 000 so'm</div>
              </label>

              <label class="addon-row-box" :class="{ selected: state.checkout.addons.transfer }">
                <input type="checkbox" v-model="state.checkout.addons.transfer" />
                <div class="addon-txt">
                  <div class="addon-name">🚗 Aeroportdan kutib olish va transfer</div>
                  <div class="addon-desc">Qulay biznes klass avtomobilida mehmonxonagacha yetkazish</div>
                </div>
                <div class="addon-cost">+100 000 so'm</div>
              </label>

              <label class="addon-row-box" :class="{ selected: state.checkout.addons.spa }">
                <input type="checkbox" v-model="state.checkout.addons.spa" />
                <div class="addon-txt">
                  <div class="addon-name">💆 Spa va sauna markazidan cheksiz foydalanish</div>
                  <div class="addon-desc">Fin saunasi, turk hamomi va dam olish hududi</div>
                </div>
                <div class="addon-cost">+200 000 so'm</div>
              </label>
            </div>
          </div>

          <!-- 3. Payment Method -->
          <div class="card checkout-section-card">
            <h3 class="section-card-title">3. To'lov tizimini tanlang</h3>
            <div class="payment-systems-tabs">
              <button
                class="pay-system-btn"
                :class="{ active: state.payment.method === 'payme' }"
                @click="state.payment.method = 'payme'"
              >
                <span class="pay-logo payme-logo">payme</span>
              </button>

              <button
                class="pay-system-btn"
                :class="{ active: state.payment.method === 'click' }"
                @click="state.payment.method = 'click'"
              >
                <span class="pay-logo click-logo">CLICK</span>
              </button>

              <button
                class="pay-system-btn"
                :class="{ active: state.payment.method === 'uzcard' }"
                @click="state.payment.method = 'uzcard'"
              >
                <span class="pay-logo uzcard-logo">uzcard</span>
              </button>

              <button
                class="pay-system-btn"
                :class="{ active: state.payment.method === 'visa' }"
                @click="state.payment.method = 'visa'"
              >
                <span class="pay-logo visa-logo">VISA / MC</span>
              </button>
            </div>

            <!-- Card Inputs Form -->
            <div class="card-details-fields">
              <div class="form-group">
                <label>Karta raqami (16 xonali)</label>
                <input
                  type="text"
                  v-model="state.payment.cardNumber"
                  placeholder="8600 •••• •••• ••••"
                  class="form-input"
                />
              </div>

              <div class="form-grid-2">
                <div class="form-group">
                  <label>Amal qilish muddati</label>
                  <input
                    type="text"
                    v-model="state.payment.cardExpiry"
                    placeholder="MM/YY"
                    class="form-input"
                  />
                </div>
                <div class="form-group">
                  <label>CVV / CVC (yoki SMS kod)</label>
                  <input
                    type="password"
                    v-model="state.payment.cardCvv"
                    placeholder="•••"
                    class="form-input"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Right: Order Summary Sticky Card -->
        <aside class="checkout-summary-column">
          <div class="card summary-card">
            <div class="summary-hotel-strip">
              <img :src="selectedHotel.image" :alt="selectedHotel.name" class="s-thumb" />
              <div>
                <h4 class="s-hotel-name">{{ selectedHotel.name }}</h4>
                <div class="s-room-name">{{ selectedRoom.name }}</div>
                <div class="s-dates">📅 {{ state.checkout.checkIn }} - {{ state.checkout.checkOut }}</div>
              </div>
            </div>

            <div class="summary-line-items">
              <div class="s-row">
                <span>Xona narxi (3 kecha):</span>
                <span>600 000 so'm</span>
              </div>

              <div v-if="state.checkout.addons.breakfast" class="s-row">
                <span>Nonushta xizmati:</span>
                <span>+150 000 so'm</span>
              </div>

              <div v-if="state.checkout.addons.transfer" class="s-row">
                <span>Aeroport transferi:</span>
                <span>+100 000 so'm</span>
              </div>

              <div v-if="state.checkout.addons.spa" class="s-row">
                <span>Spa markazi:</span>
                <span>+200 000 so'm</span>
              </div>

              <div class="s-row">
                <span>Xizmat haqi va QQS (12%):</span>
                <span>120 000 so'm</span>
              </div>

              <div class="s-row s-total-row">
                <span>Jami to'lov:</span>
                <span class="total-big-val">{{ formatSum(calculatedTotal) }} so'm</span>
              </div>
            </div>

            <button
              class="btn-primary confirm-pay-btn"
              :disabled="isProcessing"
              @click="submitCheckout"
            >
              <span v-if="isProcessing">To'lov tekshirilmoqda... ⏳</span>
              <span v-else>💳 To'lash va Bronni Tasdiqlash</span>
            </button>

            <div class="safe-payment-guarantee">
              <span>🛡️ 100% Xavfsiz to'lov • Bepul bekor qilish kafolatlangan</span>
            </div>
          </div>
        </aside>
      </div>

      <!-- Success Voucher Card (After payment) -->
      <div v-else class="voucher-success-card card">
        <div class="voucher-check-icon">✓</div>
        <h2 class="voucher-heading">Broningiz muvaffaqiyatli tasdiqlandi!</h2>
        <p class="voucher-sub">
          Vaucher elektron pochtangizga yuborildi. Mehmonxonaga yetib kelgach ushbu vaucher raqamini ko'rsatishingiz yetarli.
        </p>

        <div class="voucher-details-box">
          <div class="v-detail-item">
            <span class="v-label">Bron raqami:</span>
            <span class="v-val v-code">{{ createdBookingId }}</span>
          </div>
          <div class="v-detail-item">
            <span class="v-label">Mehmonxona:</span>
            <span class="v-val">{{ selectedHotel.name }}</span>
          </div>
          <div class="v-detail-item">
            <span class="v-label">Xona toifasi:</span>
            <span class="v-val">{{ selectedRoom.name }}</span>
          </div>
          <div class="v-detail-item">
            <span class="v-label">Sanalar:</span>
            <span class="v-val">{{ state.checkout.checkIn }} — {{ state.checkout.checkOut }} (3 kecha)</span>
          </div>
          <div class="v-detail-item">
            <span class="v-label">To'lov holati:</span>
            <span class="v-val status-confirmed">✓ To'langan (Payme)</span>
          </div>
        </div>

        <div class="voucher-action-buttons">
          <router-link to="/my-bookings" class="btn-primary">
            📋 Mening bronlarimga o'tish
          </router-link>
          <button class="btn-secondary" @click="handlePrint">
            🖨️ Vaucherni chop etish / Saqlash
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { state, selectedHotel, selectedRoom, addBooking } from '../store'

const isProcessing = ref(false)
const isPaid = ref(false)
const createdBookingId = ref('')

const calculatedTotal = computed(() => {
  let sum = 600000 + 120000 + 180000 // base
  if (state.checkout.addons.breakfast) sum += 150000
  if (state.checkout.addons.transfer) sum += 100000
  if (state.checkout.addons.spa) sum += 200000
  return sum
})

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function submitCheckout() {
  if (!state.checkout.guestName || !state.checkout.guestPhone) {
    alert("Iltimos, ism va telefon raqamingizni kiriting")
    return
  }

  isProcessing.value = true
  setTimeout(() => {
    isProcessing.value = false
    isPaid.value = true
    createdBookingId.value = `BRON-${Math.floor(1000 + Math.random() * 9000)}`

    addBooking({
      guestName: state.checkout.guestName,
      guestPhone: state.checkout.guestPhone,
      guestEmail: state.checkout.guestEmail,
      checkIn: state.checkout.checkIn,
      checkOut: state.checkout.checkOut,
      totalPrice: calculatedTotal.value
    })
  }, 1200)
}

function handlePrint() {
  window.print()
}
</script>

<style scoped>
.checkout-page {
  padding-bottom: 50px;
}

/* Steps */
.checkout-steps-bar {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 16px;
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  border-radius: var(--radius-md);
  padding: 14px 20px;
  margin-bottom: 24px;
}

.step-item {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 13px;
  font-weight: 700;
  color: var(--text-muted);
}

.step-item.active {
  color: var(--primary);
}

.step-num {
  width: 24px;
  height: 24px;
  border-radius: 50%;
  background: var(--bg-subtle);
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 11px;
}

.step-item.active .step-num {
  background: var(--primary);
  color: #fff;
}

.step-connector {
  width: 40px;
  height: 2px;
  background: var(--border-light);
}

/* Layout */
.checkout-layout {
  display: grid;
  grid-template-columns: 1fr 380px;
  gap: 24px;
}

.checkout-section-card {
  margin-bottom: 20px;
}

.section-card-title {
  font-size: 15px;
  font-weight: 800;
  color: var(--text-main);
  border-bottom: 1px solid var(--border-light);
  padding-bottom: 10px;
  margin-bottom: 16px;
}

.form-grid-2 {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 14px;
}

/* Addons */
.addons-list-items {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.addon-row-box {
  display: flex;
  align-items: center;
  gap: 14px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 12px 14px;
  cursor: pointer;
  transition: all 0.15s;
}

.addon-row-box:hover {
  border-color: var(--primary);
}

.addon-row-box.selected {
  border-color: var(--primary);
  background: var(--primary-light);
}

.addon-txt {
  flex: 1;
}

.addon-name {
  font-size: 13px;
  font-weight: 700;
  color: var(--text-main);
}

.addon-desc {
  font-size: 11px;
  color: var(--text-muted);
  margin-top: 2px;
}

.addon-cost {
  font-size: 13px;
  font-weight: 800;
  color: var(--primary);
}

/* Payment tabs */
.payment-systems-tabs {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 10px;
  margin-bottom: 18px;
}

.pay-system-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 10px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.15s;
}

.pay-system-btn.active {
  border-color: var(--primary);
  background: var(--card-bg);
  box-shadow: 0 0 0 2px var(--primary-light);
}

.pay-logo {
  font-weight: 800;
  font-size: 11px;
}

.payme-logo { color: #00c8b3; }
.click-logo { color: #007aff; }
.uzcard-logo { color: #1e3a8a; }
.visa-logo { color: #1d4ed8; }

/* Summary */
.checkout-summary-column {
  position: sticky;
  top: 90px;
  height: fit-content;
}

.summary-card {
  padding: 20px;
}

.summary-hotel-strip {
  display: flex;
  align-items: center;
  gap: 12px;
  border-bottom: 1px solid var(--border-light);
  padding-bottom: 14px;
  margin-bottom: 14px;
}

.s-thumb {
  width: 60px;
  height: 50px;
  border-radius: 4px;
  object-fit: cover;
}

.s-hotel-name {
  font-size: 13.5px;
  font-weight: 800;
  color: var(--text-main);
}

.s-room-name {
  font-size: 11.5px;
  color: var(--primary);
  font-weight: 600;
}

.s-dates {
  font-size: 10.5px;
  color: var(--text-muted);
}

.summary-line-items {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-bottom: 18px;
}

.s-row {
  display: flex;
  justify-content: space-between;
  font-size: 12px;
  color: var(--text-secondary);
}

.s-total-row {
  border-top: 2px solid var(--border-subtle);
  padding-top: 10px;
  font-size: 14px;
  font-weight: 800;
  color: var(--text-main);
}

.total-big-val {
  font-size: 17px;
  color: var(--success);
}

.confirm-pay-btn {
  width: 100%;
  padding: 12px;
  font-size: 13.5px;
}

.safe-payment-guarantee {
  text-align: center;
  font-size: 10px;
  color: var(--text-muted);
  margin-top: 10px;
}

/* Voucher success */
.voucher-success-card {
  max-width: 650px;
  margin: 30px auto;
  text-align: center;
  padding: 36px 30px;
}

.voucher-check-icon {
  width: 60px;
  height: 60px;
  background: var(--success-bg);
  color: var(--success);
  border-radius: 50%;
  font-size: 28px;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 auto 16px auto;
}

.voucher-heading {
  font-size: 22px;
  font-weight: 800;
  color: var(--text-main);
  margin-bottom: 8px;
}

.voucher-sub {
  font-size: 13px;
  color: var(--text-muted);
  margin-bottom: 24px;
}

.voucher-details-box {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-md);
  padding: 16px 20px;
  display: flex;
  flex-direction: column;
  gap: 10px;
  text-align: left;
  margin-bottom: 24px;
}

.v-detail-item {
  display: flex;
  justify-content: space-between;
  font-size: 12.5px;
}

.v-label {
  color: var(--text-muted);
}

.v-val {
  font-weight: 700;
  color: var(--text-main);
}

.v-code {
  color: var(--primary);
  font-size: 14px;
}

.voucher-action-buttons {
  display: flex;
  justify-content: center;
  gap: 12px;
}

@media (max-width: 900px) {
  .checkout-layout {
    grid-template-columns: 1fr;
  }
}
</style>
