import { reactive, computed } from 'vue'
import {
  initialHotels,
  initialRooms,
  initialBookings,
  initialCalendar,
  initialReviews,
  initialRatingsBreakdown,
  initialUsers,
  initialMessages
} from '../data/mockData'

export const state = reactive({
  // Global & Theme
  darkMode: false,
  language: 'UZ',
  searchGlobal: '',

  // Authentication & Role State
  // Default: unauthenticated guest viewing the public website
  isAuthenticated: false,
  currentUser: null, // { name, email, role: 'USER' | 'HOTEL' | 'ADMIN', avatar }

  // Notifications
  unreadNotificationsCount: 2,
  notifications: [
    { id: 1, title: 'Yangi bron', text: 'Deluxe Room uchun yangi bron qabul qilindi', time: '5 daqiqa oldin' },
    { id: 2, title: 'To\'lov tasdiqlandi', text: '1 200 000 so\'m Payme orqali qabul qilindi', time: '1 soat oldin' }
  ],

  // Entities
  hotels: [...initialHotels],
  selectedHotelId: 1,
  rooms: [...initialRooms],
  selectedRoomId: 101,
  bookings: [...initialBookings],
  calendar: initialCalendar,
  reviews: [...initialReviews],
  ratingsBreakdown: [...initialRatingsBreakdown],
  users: [...initialUsers],
  messages: [...initialMessages],

  // Booking Checkout State
  checkout: {
    hotelId: 1,
    roomId: 101,
    checkIn: '2025-05-15',
    checkOut: '2025-05-18',
    nights: 3,
    guestsCount: 2,
    guestName: '',
    guestPhone: '',
    guestEmail: '',
    addons: {
      breakfast: true, // +150 000 so'm
      transfer: true,  // +100 000 so'm
      spa: false       // +200 000 so'm
    }
  },

  // Payment Form State
  payment: {
    method: 'payme', // 'payme' | 'click' | 'uzcard' | 'visa' | 'mastercard'
    cardNumber: '8600 4912 3456 7890',
    cardExpiry: '12/28',
    cardCvv: '742',
    cardHolder: 'ALISHER NORMATOV',
    amount: 1200000,
    status: 'paid' // 'idle' | 'processing' | 'paid' | 'failed'
  },

  // Modals
  modals: {
    addBooking: false,
    addRoom: false,
    addHotel: false,
    support: false
  }
})

// Getters
export const selectedHotel = computed(() => {
  return state.hotels.find(h => h.id === state.selectedHotelId) || state.hotels[0]
})

export const selectedRoom = computed(() => {
  return state.rooms.find(r => r.id === state.selectedRoomId) || state.rooms[0]
})

export const favoriteHotels = computed(() => {
  return state.hotels.filter(h => h.isFavorite)
})

export const currentRole = computed(() => {
  return state.currentUser?.role || 'GUEST'
})

// Actions
export function toggleDarkMode() {
  state.darkMode = !state.darkMode
  if (state.darkMode) {
    document.documentElement.classList.add('dark-theme')
  } else {
    document.documentElement.classList.remove('dark-theme')
  }
}

export function login(role, customData = {}) {
  state.isAuthenticated = true
  if (role === 'ADMIN') {
    state.currentUser = {
      name: customData.name || 'Alisher Normatov',
      email: customData.email || 'alisher@admin.uz',
      role: 'ADMIN',
      avatar: 'AN'
    }
  } else if (role === 'HOTEL') {
    state.currentUser = {
      name: customData.name || 'Rustam Rahimov',
      email: customData.email || 'owner@grandplaza.uz',
      role: 'HOTEL',
      avatar: 'GP'
    }
  } else {
    state.currentUser = {
      name: customData.name || 'Diyorbek Sodiqov',
      email: customData.email || 'diyorbek@mail.uz',
      role: 'USER',
      avatar: 'DS'
    }
  }
  // Populate checkout defaults
  state.checkout.guestName = state.currentUser.name
  state.checkout.guestEmail = state.currentUser.email
}

export function logout() {
  state.isAuthenticated = false
  state.currentUser = null
}

export function selectHotel(hotelId) {
  state.selectedHotelId = hotelId
}

export function selectRoom(roomId) {
  state.selectedRoomId = roomId
  state.checkout.roomId = roomId
}

export function toggleFavorite(hotelId) {
  const hotel = state.hotels.find(h => h.id === hotelId)
  if (hotel) hotel.isFavorite = !hotel.isFavorite
}

export function addBooking(bookingData) {
  const newBooking = {
    id: `BRON-${Math.floor(1000 + Math.random() * 9000)}`,
    hotelName: selectedHotel.value.name,
    guestName: bookingData.guestName || state.checkout.guestName || 'Mijoz',
    guestPhone: bookingData.guestPhone || state.checkout.guestPhone || '+998 90 000 00 00',
    guestEmail: bookingData.guestEmail || state.checkout.guestEmail || 'guest@mail.uz',
    roomName: selectedRoom.value.name,
    roomType: selectedRoom.value.type,
    checkIn: bookingData.checkIn || state.checkout.checkIn,
    checkOut: bookingData.checkOut || state.checkout.checkOut,
    nights: bookingData.nights || 3,
    guests: bookingData.guests || 2,
    totalPrice: bookingData.totalPrice || 1200000,
    status: 'Confirmed',
    services: ['Nonushta', 'Transfer'],
    image: selectedHotel.value.image
  }
  state.bookings.unshift(newBooking)
  state.unreadNotificationsCount++
  state.notifications.unshift({
    id: Date.now(),
    title: 'Yangi bron tasdiqlandi!',
    text: `${newBooking.hotelName} — ${newBooking.roomName} (${newBooking.guestName})`,
    time: 'Hozirgina'
  })
  return newBooking.id
}

export function updateBookingStatus(bookingId, newStatus) {
  const booking = state.bookings.find(b => b.id === bookingId)
  if (booking) booking.status = newStatus
}

export function addReview(newRev) {
  state.reviews.unshift({
    id: Date.now(),
    author: state.currentUser?.name || 'Mijoz',
    avatarText: state.currentUser?.avatar || 'M',
    date: 'Bugun',
    rating: newRev.rating || 5,
    comment: newRev.comment || 'Ajoyib servis!'
  })
}

export function addRoom(room) {
  state.rooms.push({
    id: Date.now(),
    hotelId: state.selectedHotelId,
    name: room.name,
    type: room.type,
    capacity: room.capacity || 2,
    size: room.size || '30 m²',
    breakfastIncluded: true,
    pricePerNight: Number(room.price) || 800000,
    beds: room.beds || '1 Katta karavot',
    floor: room.floor || '2-Qavat / 205',
    status: 'Active',
    image: 'https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=500&q=80'
  })
}
