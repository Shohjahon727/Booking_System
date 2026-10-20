<template>
  <div v-if="state.modals.support" class="modal-overlay" @click.self="close">
    <div class="modal-box">
      <div class="modal-header">
        <h3 class="modal-title">24/7 Qo'llab-quvvatlash xizmati</h3>
        <button class="modal-close-btn" @click="close">&times;</button>
      </div>

      <div class="modal-body">
        <div class="support-intro-card">
          <div class="agent-avatar">🎧</div>
          <div>
            <div class="agent-name">Operator: Nodirbek</div>
            <div class="agent-status">● Hozir tarmoqda (Tezkor javob)</div>
          </div>
        </div>

        <div class="form-group">
          <label>Mavzu</label>
          <input type="text" v-model="topic" class="form-input" placeholder="Bronni o'zgartirish yoki bekor qilish" />
        </div>

        <div class="form-group">
          <label>Xabaringiz</label>
          <textarea v-model="message" class="form-input text-area" rows="4" placeholder="Savolingizni batafsil yozing..."></textarea>
        </div>

        <div class="hotline-numbers">
          <span>📞 Call-markaz: <b>+998 71 200 00 24</b></span>
          <span>✉️ Email: <b>support@zunobooking.uz</b></span>
        </div>
      </div>

      <div class="modal-footer">
        <button class="btn-secondary" @click="close">Yopish</button>
        <button class="btn-primary" @click="sendMessage">Yuborish</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { state } from '../../store'

const topic = ref('')
const message = ref('')

function close() {
  state.modals.support = false
}

function sendMessage() {
  if (!message.value.trim()) {
    alert("Iltimos, xabaringizni yozing")
    return
  }
  alert("Xabaringiz qabul qilindi. Tez orada operatorimiz siz bilan bog'lanadi!")
  topic.value = ''
  message.value = ''
  close()
}
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  inset: 0;
  background: rgba(15, 23, 42, 0.7);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  backdrop-filter: blur(4px);
}

.modal-box {
  background: var(--card-bg);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-md);
  width: 90%;
  max-width: 440px;
  box-shadow: var(--shadow-dropdown);
  overflow: hidden;
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 16px;
  border-bottom: 1px solid var(--border-light);
}

.modal-title {
  font-size: 13px;
  font-weight: 700;
  color: var(--text-main);
}

.modal-close-btn {
  background: transparent;
  border: none;
  font-size: 20px;
  color: var(--text-muted);
  cursor: pointer;
}

.modal-body {
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.support-intro-card {
  display: flex;
  align-items: center;
  gap: 10px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 8px 12px;
  border-radius: var(--radius-sm);
}

.agent-avatar {
  font-size: 22px;
}

.agent-name {
  font-size: 11px;
  font-weight: 700;
  color: var(--text-main);
}

.agent-status {
  font-size: 9.5px;
  color: var(--success);
}

.text-area {
  resize: vertical;
}

.hotline-numbers {
  display: flex;
  flex-direction: column;
  gap: 4px;
  font-size: 9.5px;
  color: var(--text-muted);
  background: var(--bg-subtle);
  padding: 8px;
  border-radius: 4px;
}

.modal-footer {
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  padding: 12px 16px;
  border-top: 1px solid var(--border-light);
  background: var(--bg-subtle);
}
</style>
