<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">04</span> BRON QILISH & KALKULYATOR
      </span>
      <span class="card-badge badge-active">3 kecha • 2 mehmon</span>
    </div>

    <div class="checkout-split-layout">
      <!-- Left: Guest Details & Addons -->
      <div class="checkout-left-form">
        <!-- Room and Dates mini card -->
        <div class="selected-room-strip">
          <div class="room-strip-thumb">
            <img :src="selectedRoom.image" :alt="selectedRoom.name" />
          </div>
          <div class="room-strip-info">
            <div class="room-strip-name">{{ selectedRoom.name }}</div>
            <div class="room-strip-dates">
              📅 {{ state.checkout.checkIn }} - {{ state.checkout.checkOut }}
            </div>
            <div class="room-strip-nights">
              3 kecha • 2 mehmon
            </div>
          </div>
        </div>

        <!-- Guest inputs -->
        <div class="guest-form-fields">
          <div class="form-group-compact">
            <label>Mehmon to'liq ismi</label>
            <input
              type="text"
              v-model="state.checkout.guestName"
              placeholder="Alisher Normatov"
              class="compact-input"
            />
          </div>
          <div class="form-group-compact">
            <label>Telefon raqami</label>
            <input
              type="text"
              v-model="state.checkout.guestPhone"
              placeholder="+998 90 123 45 67"
              class="compact-input"
            />
          </div>
          <div class="form-group-compact">
            <label>Email pochta</label>
            <input
              type="email"
              v-model="state.checkout.guestEmail"
              placeholder="alisher@mail.com"
              class="compact-input"
            />
          </div>
        </div>

        <!-- Addons checklist -->
        <div class="addons-container">
          <div class="addons-heading">Qo'shimcha xizmatlar:</div>
          <label class="addon-checkbox-row">
            <input type="checkbox" v-model="state.checkout.addons.breakfast" />
            <span class="addon-title">Nonushta shved stoli</span>
            <span class="addon-price">+150 000 so'm</span>
          </label>
          <label class="addon-checkbox-row">
            <input type="checkbox" v-model="state.checkout.addons.transfer" />
            <span class="addon-title">Aeroportdan transfer</span>
            <span class="addon-price">+100 000 so'm</span>
          </label>
        </div>
      </div>

      <!-- Right: Calculator & Confirm Button -->
      <div class="checkout-right-calc">
        <div class="calc-receipt-box">
          <div class="receipt-header">To'lov ma'lumotlari:</div>

          <div class="receipt-row">
            <span>Xona narxi (3 kecha):</span>
            <span class="receipt-val">600 000 so'm</span>
          </div>

          <div class="receipt-row">
            <span>Xizmat haqi:</span>
            <span class="receipt-val">120 000 so'm</span>
          </div>

          <div class="receipt-row">
            <span>Soliq va servis:</span>
            <span class="receipt-val">180 000 so'm</span>
          </div>

          <div class="receipt-row receipt-total">
            <span>Jami to'lov:</span>
            <span class="total-sum">1 200 000 so'm</span>
          </div>
        </div>

        <button class="btn-confirm-booking" @click="handleConfirm">
          ✓ Bronni tasdiqlash
        </button>

        <div class="moneyback-guarantee">
          <span>🛡️ To'lov qaytarilishi kafolatlangan</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { state, selectedRoom, addBooking, setActiveView } from '../../store'

function handleConfirm() {
  addBooking({
    guestName: state.checkout.guestName,
    guestPhone: state.checkout.guestPhone,
    guestEmail: state.checkout.guestEmail
  })
  alert("Tabriklaymiz! Sizning broningiz muvaffaqiyatli qabul qilindi.")
  setActiveView('05')
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

.checkout-split-layout {
  display: grid;
  grid-template-columns: 1.1fr 1fr;
  gap: 10px;
}

/* Left */
.checkout-left-form {
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.selected-room-strip {
  display: flex;
  align-items: center;
  gap: 8px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
}

.room-strip-thumb {
  width: 48px;
  height: 38px;
  border-radius: 4px;
  overflow: hidden;
  background: #1e293b;
}

.room-strip-thumb img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.room-strip-name {
  font-size: 10.5px;
  font-weight: 700;
  color: var(--text-main);
}

.room-strip-dates, .room-strip-nights {
  font-size: 8.5px;
  color: var(--text-muted);
}

.guest-form-fields {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.form-group-compact label {
  display: block;
  font-size: 8.5px;
  font-weight: 600;
  color: var(--text-muted);
  margin-bottom: 2px;
}

.compact-input {
  width: 100%;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-main);
  padding: 4px 6px;
  border-radius: 4px;
  font-size: 9.5px;
  outline: none;
}

.compact-input:focus {
  border-color: var(--primary);
}

/* Addons */
.addons-container {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px 8px;
}

.addons-heading {
  font-size: 8.5px;
  font-weight: 700;
  color: var(--text-muted);
  text-transform: uppercase;
  margin-bottom: 4px;
}

.addon-checkbox-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 9.5px;
  color: var(--text-main);
  margin-bottom: 3px;
  cursor: pointer;
}

.addon-title {
  flex: 1;
  margin-left: 6px;
}

.addon-price {
  font-weight: 700;
  color: var(--primary);
  font-size: 9px;
}

/* Right */
.checkout-right-calc {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.calc-receipt-box {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 8px 10px;
}

.receipt-header {
  font-size: 9.5px;
  font-weight: 700;
  color: var(--text-muted);
  text-transform: uppercase;
  border-bottom: 1px solid var(--border-light);
  padding-bottom: 4px;
  margin-bottom: 6px;
}

.receipt-row {
  display: flex;
  justify-content: space-between;
  font-size: 9.5px;
  color: var(--text-secondary);
  margin-bottom: 4px;
}

.receipt-val {
  font-weight: 600;
  color: var(--text-main);
}

.receipt-total {
  border-top: 1px solid var(--border-subtle);
  padding-top: 6px;
  margin-top: 6px;
  font-size: 11px;
  font-weight: 800;
  color: var(--text-main);
}

.total-sum {
  color: var(--success);
  font-size: 12px;
  font-weight: 800;
}

.btn-confirm-booking {
  background: var(--success);
  color: #fff;
  border: none;
  padding: 8px 12px;
  border-radius: var(--radius-sm);
  font-weight: 800;
  font-size: 10.5px;
  cursor: pointer;
  margin-top: 8px;
  width: 100%;
  transition: opacity 0.15s;
}

.btn-confirm-booking:hover {
  opacity: 0.92;
}

.moneyback-guarantee {
  text-align: center;
  font-size: 8.5px;
  color: var(--text-muted);
  margin-top: 4px;
}

@media (max-width: 900px) {
  .checkout-split-layout {
    grid-template-columns: 1fr;
  }
}
</style>
