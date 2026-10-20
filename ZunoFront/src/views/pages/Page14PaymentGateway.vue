<template>
  <div class="page-container page-14-payment">
    <div class="content-container">
      <!-- Screen 14 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 14</span>
        <span class="s-title">TO'LOV SAHIFASI</span>
        <span class="s-role">Xavfsiz To'lov Gateway</span>
      </div>

      <div class="payment-gateway-wrapper card">
        <div class="gateway-header">
          <h2>Xavfsiz Onlayn To'lov Tizimi</h2>
          <p>Tanlangan to'lov tizimi orqali to'lovni 100% himoyalangan holda amalga oshiring</p>
        </div>

        <!-- Payment Method Tabs (From Mockup 14) -->
        <div class="payment-tabs-grid">
          <button
            class="pay-tab-btn payme-tab"
            :class="{ active: state.payment.method === 'payme' }"
            @click="state.payment.method = 'payme'"
          >
            payme
          </button>

          <button
            class="pay-tab-btn click-tab"
            :class="{ active: state.payment.method === 'click' }"
            @click="state.payment.method = 'click'"
          >
            CLICK
          </button>

          <button
            class="pay-tab-btn uzcard-tab"
            :class="{ active: state.payment.method === 'uzcard' }"
            @click="state.payment.method = 'uzcard'"
          >
            uzcard
          </button>

          <button
            class="pay-tab-btn visa-tab"
            :class="{ active: state.payment.method === 'visa' }"
            @click="state.payment.method = 'visa'"
          >
            VISA
          </button>

          <button
            class="pay-tab-btn mc-tab"
            :class="{ active: state.payment.method === 'mastercard' }"
            @click="state.payment.method = 'mastercard'"
          >
            Mastercard
          </button>
        </div>

        <!-- Card Mock & Inputs -->
        <div class="card-inputs-container">
          <div class="form-group">
            <label>Karta raqami</label>
            <input
              type="text"
              v-model="state.payment.cardNumber"
              placeholder="8600 4912 3456 7890"
              class="form-input"
            />
          </div>

          <div class="card-grid-two">
            <div class="form-group">
              <label>Amal qilish muddati</label>
              <input
                type="text"
                v-model="state.payment.cardExpiry"
                placeholder="12/28"
                class="form-input"
              />
            </div>
            <div class="form-group">
              <label>CVV / CVC kodi</label>
              <input
                type="password"
                v-model="state.payment.cardCvv"
                placeholder="742"
                class="form-input"
              />
            </div>
          </div>

          <div class="form-group">
            <label>Karta egasining ismi</label>
            <input
              type="text"
              v-model="state.payment.cardHolder"
              placeholder="ALISHER NORMATOV"
              class="form-input"
            />
          </div>
        </div>

        <!-- Big Pay Button -->
        <button
          class="btn-primary big-pay-btn"
          :disabled="isPaying"
          @click="handleProcessPay"
        >
          <span v-if="isPaying">To'lov tekshirilmoqda... ⏳</span>
          <span v-else>To'lash: {{ formatSum(state.payment.amount || 1200000) }} so'm</span>
        </button>

        <!-- Status Indicators (From Mockup 14: Paid, Pending, Failed) -->
        <div class="payment-status-indicators-bar">
          <span class="status-lbl">Tranzaksiya holati:</span>
          <div class="indicators-row">
            <span
              class="ind-dot"
              :class="{ active: currentStatus === 'paid' }"
              @click="currentStatus = 'paid'"
            >
              <b class="dot-green">●</b> Paid (Muvaffaqiyatli)
            </span>
            <span
              class="ind-dot"
              :class="{ active: currentStatus === 'pending' }"
              @click="currentStatus = 'pending'"
            >
              <b class="dot-amber">●</b> Pending (Kutilmoqda)
            </span>
            <span
              class="ind-dot"
              :class="{ active: currentStatus === 'failed' }"
              @click="currentStatus = 'failed'"
            >
              <b class="dot-red">●</b> Failed (Xatolik)
            </span>
          </div>
        </div>

        <div v-if="paymentSuccess" class="payment-success-alert">
          🎉 To'lov muvaffaqiyatli qabul qilindi! Bron vaucheri rasmiylashtirildi.
          <router-link to="/my-bookings" class="alert-link">Mening bronlarimda ko'rish &rarr;</router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { state, addBooking } from '../../store'

const isPaying = ref(false)
const currentStatus = ref('paid')
const paymentSuccess = ref(false)

function formatSum(num) {
  return Number(num).toLocaleString('uz-UZ')
}

function handleProcessPay() {
  isPaying.value = true
  setTimeout(() => {
    isPaying.value = false
    currentStatus.value = 'paid'
    paymentSuccess.value = true
    addBooking({
      totalPrice: state.payment.amount || 1200000
    })
  }, 1000)
}
</script>

<style scoped>
.page-14-payment {
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
  margin-bottom: 20px;
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

.payment-gateway-wrapper {
  max-width: 600px;
  margin: 0 auto;
  padding: 32px;
}

.gateway-header {
  text-align: center;
  margin-bottom: 24px;
}

.gateway-header h2 {
  font-size: 20px;
  font-weight: 800;
  color: var(--text-main);
}

.gateway-header p {
  font-size: 12.5px;
  color: var(--text-muted);
  margin-top: 4px;
}

.payment-tabs-grid {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  gap: 8px;
  margin-bottom: 24px;
}

.pay-tab-btn {
  padding: 10px 4px;
  border-radius: var(--radius-sm);
  font-weight: 800;
  font-size: 11px;
  border: 1px solid transparent;
  cursor: pointer;
  text-align: center;
  text-transform: uppercase;
}

.payme-tab { background: #00c8b3; color: #fff; }
.click-tab { background: #007aff; color: #fff; }
.uzcard-tab { background: #1e3a8a; color: #fff; }
.visa-tab { background: #1d4ed8; color: #fff; }
.mc-tab { background: #ef4444; color: #fff; }

.pay-tab-btn.active {
  box-shadow: 0 0 0 3px var(--text-main);
}

.card-inputs-container {
  display: flex;
  flex-direction: column;
  gap: 12px;
  margin-bottom: 24px;
}

.card-grid-two {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 14px;
}

.big-pay-btn {
  width: 100%;
  padding: 12px;
  font-size: 15px;
  font-weight: 800;
  margin-bottom: 20px;
}

.payment-status-indicators-bar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-top: 1px solid var(--border-light);
  padding-top: 14px;
  font-size: 12px;
}

.status-lbl {
  color: var(--text-muted);
  font-weight: 700;
}

.indicators-row {
  display: flex;
  gap: 14px;
}

.ind-dot {
  cursor: pointer;
  padding: 2px 6px;
  border-radius: 4px;
  color: var(--text-muted);
}

.ind-dot.active {
  font-weight: 800;
  color: var(--text-main);
  background: var(--bg-subtle);
}

.dot-green { color: #10b981; }
.dot-amber { color: #f59e0b; }
.dot-red { color: #ef4444; }

.payment-success-alert {
  background: var(--success-bg);
  border: 1px solid rgba(16, 185, 129, 0.3);
  color: var(--success);
  padding: 12px;
  border-radius: var(--radius-sm);
  margin-top: 18px;
  font-size: 12.5px;
  font-weight: 700;
  text-align: center;
}

.alert-link {
  display: block;
  margin-top: 4px;
  color: var(--primary);
  text-decoration: underline;
}
</style>
