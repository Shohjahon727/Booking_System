<template>
  <footer class="master-footer-component">
    <!-- Trust Features -->
    <div class="trust-features-block">
      <div class="trust-box">
        <span class="trust-emoji">🏷️</span>
        <div class="trust-info">
          <h5>Eng yaxshi narxlar</h5>
          <p>Kafolatlangan qulay narx</p>
        </div>
      </div>
      <div class="trust-box">
        <span class="trust-emoji">🔒</span>
        <div class="trust-info">
          <h5>Xavfsiz to'lov</h5>
          <p>100% himoyalangan tizim</p>
        </div>
      </div>
      <div class="trust-box">
        <span class="trust-emoji">📞</span>
        <div class="trust-info">
          <h5>24/7 Qo'llab-quvvatlash</h5>
          <p>Doimiy yordam markazi</p>
        </div>
      </div>
      <div class="trust-box">
        <span class="trust-emoji">⚡</span>
        <div class="trust-info">
          <h5>Oson bron qilish</h5>
          <p>Tezkor va oson tizim</p>
        </div>
      </div>
    </div>

    <!-- Tech Stack -->
    <div class="tech-stack-block">
      <span class="tech-title">TEXNOLOGIYA:</span>
      <div class="tech-badges-list">
        <span class="tech-tag vue-tag">Vue.js 3 Frontend</span>
        <span class="tech-tag dotnet-tag">.NET 10 Web API</span>
        <span class="tech-tag">PostgreSQL</span>
        <span class="tech-tag">Cloudinary</span>
        <span class="tech-tag">Vite & Pinia</span>
      </div>
    </div>

    <!-- Payment Providers -->
    <div class="payment-providers-block">
      <span class="payment-title">TO'LOV TIZIMLARI:</span>
      <div class="payment-badges-list">
        <span class="pay-tag payme-tag">payme</span>
        <span class="pay-tag click-tag">CLICK</span>
        <span class="pay-tag uzcard-tag">uzcard</span>
        <span class="pay-tag humo-tag">HUMO</span>
        <span class="pay-tag visa-tag">VISA</span>
        <span class="pay-tag mc-tag">mastercard</span>
      </div>
    </div>
  </footer>
</template>

<script setup>
</script>

<style scoped>
.master-footer-component {
  background: var(--header-bg);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-md);
  margin: 0 16px 16px 16px;
  padding: 14px 22px;
  display: grid;
  grid-template-columns: 1.4fr 1.3fr 1fr;
  gap: 20px;
  align-items: center;
  box-shadow: var(--shadow-sm);
}

.trust-features-block {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 12px;
}

.trust-box {
  display: flex;
  align-items: center;
  gap: 8px;
}

.trust-emoji {
  font-size: 18px;
}

.trust-info h5 {
  font-size: 11px;
  font-weight: 700;
  color: var(--text-main);
  line-height: 1.2;
}

.trust-info p {
  font-size: 9.5px;
  color: var(--text-muted);
}

.tech-stack-block {
  display: flex;
  align-items: center;
  gap: 8px;
  flex-wrap: wrap;
}

.tech-title, .payment-title {
  font-size: 10px;
  font-weight: 800;
  color: var(--text-muted);
  letter-spacing: 0.5px;
}

.tech-badges-list {
  display: flex;
  align-items: center;
  gap: 6px;
  flex-wrap: wrap;
}

.tech-tag {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-secondary);
  font-size: 9.5px;
  font-weight: 600;
  padding: 3px 7px;
  border-radius: 4px;
}

.vue-tag {
  color: #10b981;
  border-color: rgba(16, 185, 129, 0.4);
  font-weight: 700;
}

.dotnet-tag {
  color: #38bdf8;
  border-color: rgba(56, 189, 248, 0.4);
  font-weight: 700;
}

.payment-providers-block {
  display: flex;
  align-items: center;
  gap: 8px;
  justify-content: flex-end;
  flex-wrap: wrap;
}

.payment-badges-list {
  display: flex;
  align-items: center;
  gap: 6px;
  flex-wrap: wrap;
}

.pay-tag {
  font-size: 9px;
  font-weight: 800;
  padding: 3px 7px;
  border-radius: 4px;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.payme-tag {
  background: #00c8b3;
  color: #ffffff;
}

.click-tag {
  background: #007aff;
  color: #ffffff;
}

.uzcard-tag {
  background: #1e3a8a;
  color: #ffffff;
}

.humo-tag {
  background: #d97706;
  color: #ffffff;
}

.visa-tag {
  background: #1d4ed8;
  color: #ffffff;
}

.mc-tag {
  background: #ef4444;
  color: #ffffff;
}

@media (max-width: 1200px) {
  .master-footer-component {
    grid-template-columns: 1fr;
    gap: 14px;
  }
  .payment-providers-block {
    justify-content: flex-start;
  }
}
</style>
