<template>
  <div class="page-container page-04-booking">
    <div class="content-container">
      <!-- Screen 04 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 04</span>
        <span class="s-title">SMART BRON QILISH & KALKULYATOR</span>
        <span class="s-role">3 kecha • 2 mehmon</span>
      </div>

      <!-- Checkout 2-column layout -->
      <div class="checkout-grid-container">
        <!-- 1. Left Form -->
        <div class="checkout-left-form">
          <!-- Room Summary Card -->
          <div class="card room-summary-mini-card">
            <img :src="selectedRoom.image" :alt="selectedRoom.name" class="mini-room-pic" />
            <div class="mini-room-info">
              <span class="hotel-owner-name">{{ selectedHotel.name }}</span>
              <h3 class="room-title">{{ selectedRoom.name }}</h3>
              <div class="dates-tag">
                📅 {{ state.checkout.checkIn }} — {{ state.checkout.checkOut }} (3 kecha, {{ state.checkout.guestsCount }} mehmon)
              </div>
            </div>
            <router-link to="/rooms" class="change-room-link">O'zgartirish</router-link>
          </div>

          <!-- Guest Contact Form -->
          <div class="card guest-form-card">
            <h3 class="card-section-title">Mehmon ma'lumotlari</h3>
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
              <label>Elektron pochta (Vaucher olish uchun) *</label>
              <input
                type="email"
                v-model="state.checkout.guestEmail"
                placeholder="alisher@mail.com"
                class="form-input"
                required
              />
            </div>
          </div>

          <!-- Addons Checklist -->
          <div class="card addons-card">
            <h3 class="card-section-title">Qo'shimcha xizmatlar</h3>
            <div class="addons-list">
              <label class="addon-item-box" :class="{ selected: state.checkout.addons.breakfast }">
                <input type="checkbox" v-model="state.checkout.addons.breakfast" />
                <div class="addon-info">
                  <div class="a-title">🍳 Nonushta shved stoli (3 kecha uchun)</div>
                  <div class="a-desc">Har kuni ertalab issiq milliy va yevropacha nonushta</div>
                </div>
                <div class="a-price">+150 000 so'm</div>
              </label>

              <label class="addon-item-box" :class="{ selected: state.checkout.addons.transfer }">
                <input type="checkbox" v-model="state.checkout.addons.transfer" />
                <div class="addon-info">
                  <div class="a-title">🚗 Aeroportdan kutib olish va transfer</div>
                  <div class="a-desc">Mehmonxona xodimi tomonidan aeroportda kutib olish</div>
                </div>
                <div class="a-price">+100 000 so'm</div>
              </label>

              <label class="addon-item-box" :class="{ selected: state.checkout.addons.spa }">
                <input type="checkbox" v-model="state.checkout.addons.spa" />
                <div class="addon-info">
                  <div class="a-title">💆 Spa va sauna majmuasi xizmati</div>
                  <div class="a-desc">Fin saunasi, basseyn va dam olish hududi</div>
                </div>
                <div class="a-price">+200 000 so'm</div>
              </label>
            </div>
          </div>
        </div>

        <!-- 2. Right Calculator & Confirm -->
        <aside class="checkout-right-calc">
          <div class="card calc-receipt-card">
            <h3 class="calc-card-title">Hisob-kitob (Kalkulyator)</h3>

            <div class="receipt-rows-list">
              <div class="r-row">
                <span>Xona narxi (3 kecha):</span>
                <span>600 000 so'm</span>
              </div>

              <div v-if="state.checkout.addons.breakfast" class="r-row">
                <span>Nonushta (3 kecha):</span>
                <span>+150 000 so'm</span>
              </div>

              <div v-if="state.checkout.addons.transfer" class="r-row">
                <span>Transfer (Aeroport):</span>
                <span>+100 000 so'm</span>
              </div>

              <div v-if="state.checkout.addons.spa" class="r-row">
                <span>Spa kompleks xizmati:</span>
                <span>+200 000 so'm</span>
              </div>

              <div class="r-row">
                <span>Xizmat haqi:</span>
                <span>120 000 so'm</span>
              </div>

              <div class="r-row">
                <span>Soliq va servis xizmati:</span>
                <span>180 000 so'm</span>
              </div>

              <div class="r-row total-receipt-row">
                <span>Jami to'lov:</span>
                <span class="total-big-green">{{ formatSum(calculatedSum) }} so'm</span>
              </div>
            </div>

            <button class="btn-primary full-confirm-btn" @click="proceedToPayment">
              ✓ Bronni tasdiqlash va to'lovga o'tish &rarr;
            </button>

            <div class="moneyback-guarantee-note">
              🛡️ To'lov qaytarilishi kafolatlangan • 24/7 Yordam
            </div>
          </div>
        </aside>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { state, selectedHotel, selectedRoom } from '../../store'

const router = useRouter()

const calculatedSum = computed(() => {
  let sum = 600000 + 120000 + 180000
  if (state.checkout.addons.breakfast) sum += 150000
  if (state.checkout.addons.transfer) sum += 100000
  if (state.checkout.addons.spa) sum += 200000
  return sum
})

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function proceedToPayment() {
  if (!state.checkout.guestName || !state.checkout.guestPhone) {
    alert("Iltimos, to'liq ism va telefon raqamingizni kiriting")
    return
  }
  state.payment.amount = calculatedSum.value
  router.push('/payment')
}
</script>

<style scoped>
.page-04-booking {
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
  color: var(--success);
  font-weight: 600;
}

.checkout-grid-container {
  display: grid;
  grid-template-columns: 1fr 380px;
  gap: 24px;
}

.room-summary-mini-card {
  display: flex;
  align-items: center;
  gap: 16px;
  padding: 14px 18px;
  margin-bottom: 20px;
}

.mini-room-pic {
  width: 70px;
  height: 55px;
  border-radius: 6px;
  object-fit: cover;
}

.mini-room-info {
  flex: 1;
}

.hotel-owner-name {
  font-size: 11px;
  font-weight: 700;
  color: var(--primary);
}

.room-title {
  font-size: 15px;
  font-weight: 800;
  color: var(--text-main);
}

.dates-tag {
  font-size: 11.5px;
  color: var(--text-muted);
}

.change-room-link {
  font-size: 11.5px;
  color: var(--primary);
  font-weight: 700;
  text-decoration: none;
}

.guest-form-card, .addons-card {
  margin-bottom: 20px;
  padding: 20px;
}

.card-section-title {
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

.addons-list {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.addon-item-box {
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

.addon-item-box:hover {
  border-color: var(--primary);
}

.addon-item-box.selected {
  border-color: var(--primary);
  background: var(--primary-light);
}

.addon-info {
  flex: 1;
}

.a-title {
  font-size: 13px;
  font-weight: 700;
  color: var(--text-main);
}

.a-desc {
  font-size: 11px;
  color: var(--text-muted);
}

.a-price {
  font-size: 13px;
  font-weight: 800;
  color: var(--primary);
}

/* Right Calculator */
.checkout-right-calc {
  position: sticky;
  top: 90px;
  height: fit-content;
}

.calc-receipt-card {
  padding: 20px;
}

.calc-card-title {
  font-size: 15px;
  font-weight: 800;
  color: var(--text-main);
  border-bottom: 1px solid var(--border-light);
  padding-bottom: 10px;
  margin-bottom: 16px;
}

.receipt-rows-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-bottom: 18px;
}

.r-row {
  display: flex;
  justify-content: space-between;
  font-size: 12.5px;
  color: var(--text-secondary);
}

.total-receipt-row {
  border-top: 2px solid var(--border-subtle);
  padding-top: 10px;
  font-size: 14px;
  font-weight: 800;
  color: var(--text-main);
}

.total-big-green {
  font-size: 18px;
  color: var(--success);
}

.full-confirm-btn {
  width: 100%;
  padding: 12px;
  font-size: 13.5px;
  font-weight: 800;
}

.moneyback-guarantee-note {
  text-align: center;
  font-size: 10.5px;
  color: var(--text-muted);
  margin-top: 10px;
}

@media (max-width: 900px) {
  .checkout-grid-container {
    grid-template-columns: 1fr;
  }
}
</style>
