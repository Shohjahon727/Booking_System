<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">13</span> LOGIN / RO'YXATDAN O'TISH
      </span>
      <div class="auth-mode-toggle">
        <button
          class="auth-toggle-tab"
          :class="{ active: authMode === 'login' }"
          @click="authMode = 'login'"
        >
          Kirish
        </button>
        <button
          class="auth-toggle-tab"
          :class="{ active: authMode === 'register' }"
          @click="authMode = 'register'"
        >
          Ro'yxatdan o'tish
        </button>
      </div>
    </div>

    <!-- Login Form -->
    <div v-if="authMode === 'login'" class="auth-form-body">
      <div class="form-group-compact">
        <label>Email pochta</label>
        <input
          type="email"
          v-model="loginEmail"
          placeholder="alisher@mail.com"
          class="compact-input"
        />
      </div>

      <div class="form-group-compact">
        <label>Maxfiy parol</label>
        <input
          type="password"
          v-model="loginPassword"
          placeholder="••••••••"
          class="compact-input"
        />
      </div>

      <div class="auth-aux-row">
        <label class="remember-label">
          <input type="checkbox" v-model="rememberMe" /> Eslab qolish
        </label>
        <a href="#" class="forgot-pass" @click.prevent="handleForgot">Parolni unutdingizmi?</a>
      </div>

      <button class="btn-primary auth-submit-btn" @click="handleLogin">
        Kirish
      </button>

      <!-- Quick Role Login Shortcuts -->
      <div class="quick-demo-logins">
        <span class="quick-lbl">Demo rejim:</span>
        <button class="quick-role-pill" @click="demoLogin('ADMIN')">Admin</button>
        <button class="quick-role-pill" @click="demoLogin('HOTEL')">Hotel Egasi</button>
        <button class="quick-role-pill" @click="demoLogin('USER')">Mijoz</button>
      </div>
    </div>

    <!-- Register Form -->
    <div v-else class="auth-form-body">
      <div class="form-group-compact">
        <label>To'liq ismingiz</label>
        <input
          type="text"
          v-model="registerName"
          placeholder="Alisher Normatov"
          class="compact-input"
        />
      </div>

      <div class="form-group-compact">
        <label>Email pochta</label>
        <input
          type="email"
          v-model="registerEmail"
          placeholder="alisher@mail.com"
          class="compact-input"
        />
      </div>

      <div class="form-group-compact">
        <label>Yangi parol</label>
        <input
          type="password"
          v-model="registerPassword"
          placeholder="••••••••"
          class="compact-input"
        />
      </div>

      <button class="btn-primary auth-submit-btn" @click="handleRegister">
        Ro'yxatdan o'tish
      </button>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { state, setRole } from '../../store'

const authMode = ref('login')
const loginEmail = ref('alisher@mail.com')
const loginPassword = ref('••••••••')
const rememberMe = ref(true)

const registerName = ref('')
const registerEmail = ref('')
const registerPassword = ref('')

function handleLogin() {
  alert(`Xush kelibsiz! ${loginEmail.value} hisobingizga muvaffaqiyatli kirdingiz.`)
}

function handleRegister() {
  alert("Ro'yxatdan o'tish muvaffaqiyatli yakunlandi!")
  authMode.value = 'login'
}

function handleForgot() {
  alert("Parolni tiklash havolasi emailingizga yuborildi.")
}

function demoLogin(role) {
  setRole(role)
  alert(`${role} roli faollashtirildi!`)
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

.auth-mode-toggle {
  display: flex;
  gap: 4px;
}

.auth-toggle-tab {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-muted);
  font-size: 8.5px;
  font-weight: 700;
  padding: 3px 8px;
  border-radius: var(--radius-sm);
  cursor: pointer;
}

.auth-toggle-tab.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

.auth-form-body {
  display: flex;
  flex-direction: column;
  gap: 6px;
  flex: 1;
  justify-content: center;
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

.auth-aux-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-size: 8.5px;
  margin: 2px 0 6px 0;
}

.remember-label {
  display: flex;
  align-items: center;
  gap: 4px;
  color: var(--text-muted);
  cursor: pointer;
}

.forgot-pass {
  color: var(--primary);
  text-decoration: none;
}

.auth-submit-btn {
  width: 100%;
  padding: 7px;
  font-size: 10.5px;
}

.quick-demo-logins {
  display: flex;
  align-items: center;
  gap: 6px;
  margin-top: 6px;
  font-size: 8.5px;
}

.quick-lbl {
  color: var(--text-muted);
}

.quick-role-pill {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  color: var(--text-secondary);
  padding: 2px 6px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 8px;
  font-weight: 700;
}

.quick-role-pill:hover {
  border-color: var(--primary);
  color: var(--primary);
}
</style>
