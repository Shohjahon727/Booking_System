<template>
  <div v-if="state.modals.addBooking" class="modal-overlay" @click.self="close">
    <div class="modal-box">
      <div class="modal-header">
        <h3 class="modal-title">Yangi bron qo'shish</h3>
        <button class="modal-close-btn" @click="close">&times;</button>
      </div>

      <div class="modal-body">
        <div class="form-group">
          <label>Mehmon ismi</label>
          <input type="text" v-model="form.guestName" class="form-input" placeholder="Ism Familiya" />
        </div>

        <div class="form-group">
          <label>Telefon raqam</label>
          <input type="text" v-model="form.guestPhone" class="form-input" placeholder="+998 90 123 45 67" />
        </div>

        <div class="form-row-2">
          <div class="form-group">
            <label>Kirish sanasi</label>
            <input type="date" v-model="form.checkIn" class="form-input" />
          </div>
          <div class="form-group">
            <label>Chiqish sanasi</label>
            <input type="date" v-model="form.checkOut" class="form-input" />
          </div>
        </div>

        <div class="form-group">
          <label>Xona toifasi</label>
          <select v-model="form.roomName" class="form-select">
            <option v-for="r in state.rooms" :key="r.id" :value="r.name">{{ r.name }}</option>
          </select>
        </div>

        <div class="form-group">
          <label>Jami to'lov (so'm)</label>
          <input type="number" v-model="form.totalPrice" class="form-input" />
        </div>
      </div>

      <div class="modal-footer">
        <button class="btn-secondary" @click="close">Bekor qilish</button>
        <button class="btn-primary" @click="submit">Saqlash</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive } from 'vue'
import { state, addBooking } from '../../store'

const form = reactive({
  guestName: '',
  guestPhone: '',
  checkIn: '2025-05-20',
  checkOut: '2025-05-23',
  roomName: 'Deluxe Room',
  totalPrice: 1200000
})

function close() {
  state.modals.addBooking = false
}

function submit() {
  if (!form.guestName) {
    alert("Iltimos, mehmon ismini kiriting")
    return
  }
  addBooking({
    guestName: form.guestName,
    guestPhone: form.guestPhone,
    checkIn: form.checkIn,
    checkOut: form.checkOut,
    roomName: form.roomName,
    totalPrice: form.totalPrice
  })
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

.form-row-2 {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 10px;
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
