<template>
  <div class="auth-page">
    <div class="auth-card-wrapper card">
      <div class="auth-brand-logo">
        <div class="logo-icon-circle">🏨</div>
        <h2>ZunoBookingSystem</h2>
        <p>Mehmonxona bron qilish tizimiga xush kelibsiz</p>
      </div>

      <!-- Mode Toggle Tabs -->
      <div class="auth-tabs">
        <button
          class="auth-tab-btn"
          :class="{ active: mode === 'login' }"
          @click="mode = 'login'"
        >
          Tizimga Kirish
        </button>
        <button
          class="auth-tab-btn"
          :class="{ active: mode === 'register' }"
          @click="mode = 'register'"
        >
          Ro'yxatdan O'tish
        </button>
      </div>

      <!-- Form Body -->
      <form v-if="mode === 'login'" @submit.prevent="handleLogin" class="auth-form">
        <div class="form-group">
          <label>Email pochta</label>
          <input
            type="email"
            v-model="loginEmail"
            placeholder="alisher@mail.com"
            class="form-input"
            required
          />
        </div>

        <div class="form-group">
          <label>Parol</label>
          <input
            type="password"
            v-model="loginPass"
            placeholder="••••••••"
            class="form-input"
            required
          />
        </div>

        <div class="form-aux-row">
          <label class="remember-check">
            <input type="checkbox" v-model="remember" /> Eslab qolish
          </label>
          <a href="#" class="forgot-link" @click.prevent="alert('Parolni tiklash emailingizga yuborildi')">Parolni unutdingizmi?</a>
        </div>

        <button type="submit" class="btn-primary auth-btn">
          Kirish &rarr;
        </button>

        <div class="demo-logins-box">
          <span class="demo-lbl">Tezkor Demo Kirish:</span>
          <div class="demo-buttons">
            <button type="button" class="demo-btn" @click="quickLogin('ADMIN')">Admin</button>
            <button type="button" class="demo-btn" @click="quickLogin('HOTEL')">Hotel Egasi</button>
            <button type="button" class="demo-btn" @click="quickLogin('USER')">Mehmon</button>
          </div>
        </div>
      </form>

      <!-- Register Form -->
      <form v-else @submit.prevent="handleRegister" class="auth-form">
        <div class="form-group">
          <label>To'liq ismingiz</label>
          <input
            type="text"
            v-model="regName"
            placeholder="Alisher Normatov"
            class="form-input"
            required
          />
        </div>

        <div class="form-group">
          <label>Email pochta</label>
          <input
            type="email"
            v-model="regEmail"
            placeholder="alisher@mail.com"
            class="form-input"
            required
          />
        </div>

        <div class="form-group">
          <label>Tizimdagi rolingiz</label>
          <select v-model="regRole" class="form-select">
            <option value="USER">Mehmon (Xona qidirish va bron qilish)</option>
            <option value="HOTEL">Hotel Egasi (Mehmonxona boshqaruvi)</option>
          </select>
        </div>

        <div class="form-group">
          <label>Parol yarating</label>
          <input
            type="password"
            v-model="regPass"
            placeholder="Kamida 6 ta belgi"
            class="form-input"
            required
          />
        </div>

        <button type="submit" class="btn-primary auth-btn">
          Ro'yxatdan o'tish &rarr;
        </button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { setRole } from '../store'

const router = useRouter()
const mode = ref('login')
const loginEmail = ref('alisher@mail.com')
const loginPass = ref('••••••••')
const remember = ref(true)

const regName = ref('')
const regEmail = ref('')
const regRole = ref('USER')
const regPass = ref('')

function handleLogin() {
  alert("Tizimga muvaffaqiyatli kirdingiz!")
  router.push('/')
}

function handleRegister() {
  setRole(regRole.value)
  alert("Ro'yxatdan o'tish muvaffaqiyatli yakunlandi!")
  if (regRole.value === 'HOTEL') {
    router.push('/partner')
  } else {
    router.push('/')
  }
}

function quickLogin(role) {
  setRole(role)
  if (role === 'ADMIN') router.push('/admin')
  else if (role === 'HOTEL') router.push('/partner')
  else router.push('/')
}
</script>

<style scoped>
.auth-page {
  padding: 40px 20px 60px 20px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.auth-card-wrapper {
  width: 100%;
  max-width: 440px;
  padding: 30px;
}

.auth-brand-logo {
  text-align: center;
  margin-bottom: 24px;
}

.logo-icon-circle {
  width: 50px;
  height: 50px;
  background: var(--primary-light);
  color: var(--primary);
  border-radius: 50%;
  font-size: 24px;
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 auto 10px auto;
}

.auth-brand-logo h2 {
  font-size: 20px;
  font-weight: 800;
  color: var(--text-main);
}

.auth-brand-logo p {
  font-size: 12px;
  color: var(--text-muted);
  margin-top: 2px;
}

.auth-tabs {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 6px;
  background: var(--bg-subtle);
  border-radius: var(--radius-sm);
  padding: 4px;
  margin-bottom: 20px;
}

.auth-tab-btn {
  background: transparent;
  border: none;
  font-size: 12px;
  font-weight: 700;
  color: var(--text-muted);
  padding: 8px;
  border-radius: 4px;
  cursor: pointer;
  transition: all 0.15s;
}

.auth-tab-btn.active {
  background: var(--card-bg);
  color: var(--primary);
  box-shadow: var(--shadow-sm);
}

.auth-form {
  display: flex;
  flex-direction: column;
  gap: 14px;
}

.form-aux-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-size: 11.5px;
}

.remember-check {
  display: flex;
  align-items: center;
  gap: 6px;
  color: var(--text-muted);
  cursor: pointer;
}

.forgot-link {
  color: var(--primary);
  text-decoration: none;
}

.auth-btn {
  width: 100%;
  padding: 10px;
  font-size: 13.5px;
  margin-top: 6px;
}

.demo-logins-box {
  margin-top: 14px;
  border-top: 1px solid var(--border-light);
  padding-top: 14px;
  text-align: center;
}

.demo-lbl {
  font-size: 10.5px;
  color: var(--text-muted);
  font-weight: 700;
  text-transform: uppercase;
}

.demo-buttons {
  display: flex;
  justify-content: center;
  gap: 8px;
  margin-top: 8px;
}

.demo-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-secondary);
  font-size: 11px;
  font-weight: 700;
  padding: 5px 12px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  transition: all 0.15s;
}

.demo-btn:hover {
  background: var(--primary-light);
  color: var(--primary);
  border-color: var(--primary);
}
</style>
