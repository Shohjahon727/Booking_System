<template>
  <div class="page-container page-13-auth">
    <div class="content-container">
      <!-- Screen 13 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 13</span>
        <span class="s-title">LOGIN / RO'YXATDAN O'TISH</span>
        <span class="s-role">Avtorizatsiya</span>
      </div>

      <div class="auth-two-boxes-grid">
        <!-- 1. Left Box: Kirish (Login) -->
        <div class="card auth-panel-box">
          <div class="panel-head">
            <span class="panel-icon">🔐</span>
            <h3>Tizimga Kirish (Login)</h3>
            <p>Hisobingizga kirish uchun ma'lumotlaringizni kiriting</p>
          </div>

          <form @submit.prevent="handleLogin" class="auth-inner-form">
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
              <label>Maxfiy parol</label>
              <input
                type="password"
                v-model="loginPass"
                placeholder="••••••••"
                class="form-input"
                required
              />
            </div>

            <div class="aux-row">
              <label class="remember-check">
                <input type="checkbox" v-model="remember" /> Eslab qolish
              </label>
              <a href="#" class="forgot-link" @click.prevent="alert('Parolni tiklash emailingizga yuborildi')">
                Parolni unutdingizmi?
              </a>
            </div>

            <button type="submit" class="btn-primary auth-submit-btn">
              Kirish &rarr;
            </button>
          </form>

          <!-- Demo Quick Login Buttons -->
          <div class="quick-demo-logins-box">
            <span class="demo-title">Tezkor Sinov (Rolni tanlang):</span>
            <div class="demo-pills-row">
              <button class="demo-role-btn" @click="quickLogin('USER')">👤 Mijoz (Mehmon)</button>
              <button class="demo-role-btn" @click="quickLogin('HOTEL')">🏨 Hotel Egasi</button>
              <button class="demo-role-btn" @click="quickLogin('ADMIN')">⚡ Admin</button>
            </div>
          </div>
        </div>

        <!-- 2. Right Box: Ro'yxatdan O'tish (Register) -->
        <div class="card auth-panel-box">
          <div class="panel-head">
            <span class="panel-icon">📝</span>
            <h3>Ro'yxatdan O'tish</h3>
            <p>ZunoBooking tizimidan to'liq foydalanish uchun yangi hisob oching</p>
          </div>

          <form @submit.prevent="handleRegister" class="auth-inner-form">
            <div class="form-group">
              <label>To'liq ism va familiya</label>
              <input
                type="text"
                v-model="regName"
                placeholder="Alisher Normatov"
                class="form-input"
                required
              />
            </div>

            <div class="form-group">
              <label>Elektron pochta</label>
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
                <option value="USER">Mijoz (Xona qidirish va bron qilish)</option>
                <option value="HOTEL">Hotel Egasi (O'z mehmonxonasini boshqarish)</option>
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

            <button type="submit" class="btn-primary auth-submit-btn">
              Ro'yxatdan o'tish &rarr;
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { login } from '../../store'

const router = useRouter()
const loginEmail = ref('alisher@mail.com')
const loginPass = ref('••••••••')
const remember = ref(true)

const regName = ref('')
const regEmail = ref('')
const regRole = ref('USER')
const regPass = ref('')

function handleLogin() {
  login('USER', { email: loginEmail.value })
  alert("Tizimga muvaffaqiyatli kirdingiz!")
  router.push('/my-bookings')
}

function handleRegister() {
  login(regRole.value, { name: regName.value, email: regEmail.value })
  alert("Ro'yxatdan o'tish muvaffaqiyatli yakunlandi!")
  if (regRole.value === 'HOTEL') {
    router.push('/partner/stats')
  } else {
    router.push('/my-bookings')
  }
}

function quickLogin(role) {
  login(role)
  if (role === 'ADMIN') router.push('/admin/dashboard')
  else if (role === 'HOTEL') router.push('/partner/stats')
  else router.push('/my-bookings')
}
</script>

<style scoped>
.page-13-auth {
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
  color: var(--primary);
  font-weight: 600;
}

.auth-two-boxes-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 24px;
  max-width: 900px;
  margin: 0 auto;
}

.auth-panel-box {
  padding: 28px;
  display: flex;
  flex-direction: column;
}

.panel-head {
  margin-bottom: 20px;
}

.panel-icon {
  font-size: 26px;
  margin-bottom: 8px;
  display: block;
}

.panel-head h3 {
  font-size: 18px;
  font-weight: 800;
  color: var(--text-main);
}

.panel-head p {
  font-size: 12px;
  color: var(--text-muted);
  margin-top: 2px;
}

.auth-inner-form {
  display: flex;
  flex-direction: column;
  gap: 14px;
}

.aux-row {
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

.auth-submit-btn {
  width: 100%;
  padding: 10px;
  font-size: 13px;
  font-weight: 700;
}

.quick-demo-logins-box {
  margin-top: auto;
  padding-top: 20px;
  border-top: 1px solid var(--border-light);
  text-align: center;
}

.demo-title {
  font-size: 11px;
  font-weight: 700;
  color: var(--text-muted);
  text-transform: uppercase;
  display: block;
  margin-bottom: 8px;
}

.demo-pills-row {
  display: flex;
  justify-content: center;
  gap: 6px;
  flex-wrap: wrap;
}

.demo-role-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-secondary);
  font-size: 11px;
  font-weight: 700;
  padding: 5px 10px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  transition: all 0.15s;
}

.demo-role-btn:hover {
  background: var(--primary-light);
  color: var(--primary);
  border-color: var(--primary);
}

@media (max-width: 768px) {
  .auth-two-boxes-grid {
    grid-template-columns: 1fr;
  }
}
</style>
