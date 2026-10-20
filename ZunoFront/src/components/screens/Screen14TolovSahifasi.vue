<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">14</span> TO'LOV SAHIFASI
      </span>
      <span class="card-badge badge-active">100% Xavfsiz</span>
    </div>

    <!-- Payment Providers Tabs -->
    <div class="payment-tabs-row">
      <button
        class="pay-method-tab payme-btn"
        :class="{ active: state.payment.method === 'payme' }"
        @click="state.payment.method = 'payme'"
      >
        payme
      </button>
      <button
        class="pay-method-tab click-btn"
        :class="{ active: state.payment.method === 'click' }"
        @click="state.payment.method = 'click'"
      >
        CLICK
      </button>
      <button
        class="pay-method-tab uzcard-btn"
        :class="{ active: state.payment.method === 'uzcard' }"
        @click="state.payment.method = 'uzcard'"
      >
        uzcard
      </button>
      <button
        class="pay-method-tab visa-btn"
        :class="{ active: state.payment.method === 'visa' }"
        @click="state.payment.method = 'visa'"
      >
        VISA
      </button>
      <button
        class="pay-method-tab mc-btn"
        :class="{ active: state.payment.method === 'mastercard' }"
        @click="state.payment.method = 'mastercard'"
      >
        MC
      </button>
    </div>

    <!-- Virtual Card Mock & Inputs -->
    <div class="card-inputs-grid">
      <div class="form-group-compact">
        <label>Karta raqami</label>
        <input
          type="text"
          v-model="state.payment.cardNumber"
          placeholder="8600 4912 3456 7890"
          class="compact-input"
        />
      </div>

      <div class="card-row-two">
        <div class="form-group-compact">
          <label>Amal qilish muddati</label>
          <input
            type="text"
            v-model="state.payment.cardExpiry"
            placeholder="12/28"
            class="compact-input"
          />
        </div>
        <div class="form-group-compact">
          <label>CVV / CVC</label>
          <input
            type="password"
            v-model="state.payment.cardCvv"
            placeholder="•••"
            class="compact-input"
          />
        </div>
      </div>
    </div>

    <!-- Big Pay Button -->
    <button
      class="btn-primary pay-action-big-btn"
      :disabled="state.payment.status === 'processing'"
      @click="handlePay"
    >
      <span v-if="state.payment.status === 'processing'">To'lov amalga oshirilmoqda...</span>
      <span v-else>Pay 1 200 000 so'm</span>
    </button>

    <!-- Status Indicators: Paid, Pending, Failed -->
    <div class="payment-status-footer">
      <div
        class="status-dot-item"
        :class="{ active: state.payment.status === 'paid' }"
        @click="state.payment.status = 'paid'"
      >
        <span class="status-dot dot-paid">●</span> Paid
      </div>
      <div
        class="status-dot-item"
        :class="{ active: state.payment.status === 'processing' }"
        @click="state.payment.status = 'processing'"
      >
        <span class="status-dot dot-pending">●</span> Pending
      </div>
      <div
        class="status-dot-item"
        :class="{ active: state.payment.status === 'failed' }"
        @click="state.payment.status = 'failed'"
      >
        <span class="status-dot dot-failed">●</span> Failed
      </div>
    </div>
  </div>
</template>

<script setup>
import { state, processPayment } from '../../store'

function handlePay() {
  processPayment()
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

.payment-tabs-row {
  display: flex;
  gap: 4px;
  margin-bottom: 8px;
  overflow-x: auto;
}

.pay-method-tab {
  font-size: 8.5px;
  font-weight: 800;
  padding: 4px 8px;
  border-radius: 4px;
  border: 1px solid transparent;
  cursor: pointer;
  text-transform: uppercase;
}

.payme-btn { background: #00c8b3; color: #fff; }
.click-btn { background: #007aff; color: #fff; }
.uzcard-btn { background: #1e3a8a; color: #fff; }
.visa-btn { background: #1d4ed8; color: #fff; }
.mc-btn { background: #ef4444; color: #fff; }

.pay-method-tab.active {
  outline: 2px solid var(--text-main);
}

.card-inputs-grid {
  display: flex;
  flex-direction: column;
  gap: 4px;
  margin-bottom: 8px;
}

.card-row-two {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 6px;
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
  padding: 5px 8px;
  border-radius: 4px;
  font-size: 9.5px;
  outline: none;
}

.compact-input:focus {
  border-color: var(--primary);
}

.pay-action-big-btn {
  width: 100%;
  padding: 8px;
  font-size: 11px;
  font-weight: 800;
  background: #2563eb;
  border-radius: var(--radius-sm);
  margin-bottom: 8px;
}

.pay-action-big-btn:hover {
  background: #1d4ed8;
}

.payment-status-footer {
  display: flex;
  justify-content: center;
  gap: 16px;
  font-size: 8.5px;
  color: var(--text-muted);
  border-top: 1px solid var(--border-light);
  padding-top: 6px;
  margin-top: auto;
}

.status-dot-item {
  display: flex;
  align-items: center;
  gap: 4px;
  cursor: pointer;
  padding: 2px 4px;
  border-radius: 3px;
}

.status-dot-item.active {
  font-weight: 800;
  color: var(--text-main);
  background: var(--bg-subtle);
}

.dot-paid { color: #10b981; }
.dot-pending { color: #f59e0b; }
.dot-failed { color: #ef4444; }
</style>
