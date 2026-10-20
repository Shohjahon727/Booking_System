import { createRouter, createWebHistory } from 'vue-router'
import { state } from '../store'

// 16 Real Dedicated Pages
import Page01Home from '../views/pages/Page01Home.vue'
import Page02HotelDetail from '../views/pages/Page02HotelDetail.vue'
import Page03RoomsList from '../views/pages/Page03RoomsList.vue'
import Page04BookingCheckout from '../views/pages/Page04BookingCheckout.vue'
import Page05MyBookings from '../views/pages/Page05MyBookings.vue'
import Page06RoomCalendar from '../views/pages/Page06RoomCalendar.vue'
import Page07ManageBookings from '../views/pages/Page07ManageBookings.vue'
import Page08HotelReviews from '../views/pages/Page08HotelReviews.vue'
import Page09OwnerStats from '../views/pages/Page09OwnerStats.vue'
import Page10AdminDashboard from '../views/pages/Page10AdminDashboard.vue'
import Page11RoomsTable from '../views/pages/Page11RoomsTable.vue'
import Page12UsersTable from '../views/pages/Page12UsersTable.vue'
import Page13Auth from '../views/pages/Page13Auth.vue'
import Page14PaymentGateway from '../views/pages/Page14PaymentGateway.vue'
import Page15Favorites from '../views/pages/Page15Favorites.vue'
import Page16Messages from '../views/pages/Page16Messages.vue'

const routes = [
  // ==========================================
  // 1. PUBLIC PAGES (Barcha tashrif buyuruvchilar uchun)
  // ==========================================
  // Screen 01: Bosh Sahifa (Landing & Qidiruv)
  { path: '/', name: 'home', component: Page01Home },

  // Screen 02: Mehmonxona Detali
  { path: '/hotel/:id', name: 'hotel-detail', component: Page02HotelDetail },

  // Screen 03: Xonalarni Tanlash
  { path: '/rooms', name: 'rooms', component: Page03RoomsList },

  // Screen 04: Bron Qilish & Hisob-kitob
  { path: '/booking', name: 'booking', component: Page04BookingCheckout },

  // Screen 13: Kirish / Ro'yxatdan o'tish (Auth)
  { path: '/login', name: 'login', component: Page13Auth },

  // Screen 14: To'lov Sahifasi (Payment Gateway)
  { path: '/payment', name: 'payment', component: Page14PaymentGateway },

  // ==========================================
  // 2. USER (Mehmon login qilganda ochiladigan sahifalar)
  // ==========================================
  // Screen 05: Mening Bronlarim
  {
    path: '/my-bookings',
    name: 'my-bookings',
    component: Page05MyBookings,
    meta: { requiresAuth: true, roles: ['USER', 'ADMIN'] }
  },

  // Screen 15: Sevimlilar (Wishlist)
  {
    path: '/favorites',
    name: 'favorites',
    component: Page15Favorites,
    meta: { requiresAuth: true, roles: ['USER', 'ADMIN'] }
  },

  // Screen 16: Xabarlar & Qo'llab-quvvatlash
  {
    path: '/messages',
    name: 'messages',
    component: Page16Messages,
    meta: { requiresAuth: true, roles: ['USER', 'HOTEL', 'ADMIN'] }
  },

  // ==========================================
  // 3. HOTEL EGASI (Partner login qilganda ochiladigan sahifalar)
  // ==========================================
  // Screen 09: Hotel Egasi Statistikasi
  {
    path: '/partner/stats',
    name: 'partner-stats',
    component: Page09OwnerStats,
    meta: { requiresAuth: true, roles: ['HOTEL', 'ADMIN'] }
  },

  // Screen 06: Xonalar Taqvimi (Gantt timeline)
  {
    path: '/partner/calendar',
    name: 'partner-calendar',
    component: Page06RoomCalendar,
    meta: { requiresAuth: true, roles: ['HOTEL', 'ADMIN'] }
  },

  // Screen 07: Bronlarni Boshqarish
  {
    path: '/partner/bookings',
    name: 'partner-bookings',
    component: Page07ManageBookings,
    meta: { requiresAuth: true, roles: ['HOTEL', 'ADMIN'] }
  },

  // Screen 11: Xonalar Jadvali & Fondi
  {
    path: '/partner/rooms',
    name: 'partner-rooms',
    component: Page11RoomsTable,
    meta: { requiresAuth: true, roles: ['HOTEL', 'ADMIN'] }
  },

  // Screen 08: Sharhlar & Reyting
  {
    path: '/partner/reviews',
    name: 'partner-reviews',
    component: Page08HotelReviews,
    meta: { requiresAuth: true, roles: ['HOTEL', 'ADMIN'] }
  },

  // ==========================================
  // 4. ADMIN (Super Admin login qilganda ochiladigan sahifalar)
  // ==========================================
  // Screen 10: Admin Dashboard
  {
    path: '/admin/dashboard',
    name: 'admin-dashboard',
    component: Page10AdminDashboard,
    meta: { requiresAuth: true, roles: ['ADMIN'] }
  },

  // Screen 12: Foydalanuvchilar va Rollar
  {
    path: '/admin/users',
    name: 'admin-users',
    component: Page12UsersTable,
    meta: { requiresAuth: true, roles: ['ADMIN'] }
  },

  // Fallback
  { path: '/:pathMatch(.*)*', redirect: '/' }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior() {
    return { top: 0, behavior: 'smooth' }
  }
})

// Navigation Guard for Role-based Access Control (RBAC)
router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth) {
    if (!state.isAuthenticated) {
      alert("Ushbu sahifani ko'rish uchun tizimga kiring!")
      return next({ path: '/login', query: { redirect: to.fullPath } })
    }
    const userRole = state.currentUser?.role
    if (to.meta.roles && !to.meta.roles.includes(userRole)) {
      alert(`Sizning rolingiz (${userRole}) ushbu sahifaga kirishga ruxsat bermaydi.`)
      return next('/')
    }
  }
  next()
})

export default router
