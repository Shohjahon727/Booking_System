<template>
  <div v-if="state.modals.addRoom" class="modal-overlay" @click.self="close">
    <div class="modal-box">
      <div class="modal-header">
        <h3 class="modal-title">Yangi xona qo'shish</h3>
        <button class="modal-close-btn" @click="close">&times;</button>
      </div>

      <div class="modal-body">
        <div class="form-group">
          <label>Xona nomi</label>
          <input type="text" v-model="form.name" class="form-input" placeholder="Executive King Room" />
        </div>

        <div class="form-row-2">
          <div class="form-group">
            <label>Xona toifasi</label>
            <select v-model="form.type" class="form-select">
              <option value="Deluxe">Deluxe</option>
              <option value="Suite">Suite</option>
              <option value="Standard">Standard</option>
              <option value="Twin">Twin</option>
            </select>
          </div>
          <div class="form-group">
            <label>Qavat / Raqam</label>
            <input type="text" v-model="form.floor" class="form-input" placeholder="3-Qavat / 305" />
          </div>
        </div>

        <div class="form-row-2">
          <div class="form-group">
            <label>Sig'imi (kishi)</label>
            <input type="number" v-model="form.capacity" class="form-input" />
          </div>
          <div class="form-group">
            <label>Maydoni (m²)</label>
            <input type="text" v-model="form.size" class="form-input" placeholder="35 m²" />
          </div>
        </div>

        <div class="form-group">
          <label>1 kecha narxi (so'm)</label>
          <input type="number" v-model="form.price" class="form-input" placeholder="900000" />
        </div>
      </div>

      <div class="modal-footer">
        <button class="btn-secondary" @click="close">Bekor qilish</button>
        <button class="btn-primary" @click="submit">Qo'shish</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive } from 'vue'
import { state, addRoom } from '../../store'

const form = reactive({
  name: '',
  type: 'Deluxe',
  floor: '3-Qavat / 305',
  capacity: 2,
  size: '35 m²',
  price: 950000
})

function close() {
  state.modals.addRoom = false
}

function submit() {
  if (!form.name) {
    alert("Iltimos, xona nomini kiriting")
    return
  }
  addRoom({
    name: form.name,
    type: form.type,
    floor: form.floor,
    capacity: form.capacity,
    size: form.size,
    price: form.price
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
