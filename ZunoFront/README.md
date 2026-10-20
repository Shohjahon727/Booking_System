# ZunoBookingSystem — Zamonaviy Mehmonxona Bron Qilish Tizimi (Frontend)

Ushbu loyiha taqdim etilgan dizayn konsepsiyasi asosida noldan to'liq, professional va haqiqiy ko'p sahifali **Vue.js 3** va **Vue Router 4** veb-ilovasi sifatida ishlab chiqildi.

---

## 🌟 Tizim Arxitekturasi va Sahifalar (Pages & Portals)

Ilova 3 ta asosiy foydalanuvchi qatlamiga va to'liq sahifalarga ega:

### 1. 👤 Mijoz (Mehmon) Qismi:
- **`/` — Bosh Sahifa (Landing Page):**
  - Zamonaviy Hero qidiruv bloki (shahar/hudud, kirish-chiqish sanalari, mehmonlar soni).
  - Ommabop shaharlar (Toshkent, Samarqand, Buxoro, Xiva) filtrlari.
  - Tavsiya etilgan mehmonxonalar vitrinasi, nishonlar, narxlar va qulayliklar.
  - "Nima uchun ZunoBooking?" ishonch kafolatlari va hamkorlik taklifi.
- **`/hotels` — Mehmonxonalar Katalogi:**
  - Yon tomondagi filtrlar: narxlar, shahar, yulduzlar darajasi (5★, 4★, 3★), qulayliklar (Wi-Fi, Basseyn, Parking, Restoran).
  - Saralash: arzonidan qimmatiga, reyting bo'yicha, tavsiya etilgan.
- **`/hotels/:id` — Mehmonxona Detali Sahifasi:**
  - 5 ta fotosuratdan iborat yirik foto-galereya.
  - Tablar: Xonalar, Tavsif, Qulayliklar, Sharhlar va Joylashuv.
  - Har bir xona toifasi (Deluxe, Suite, Standard, Twin) bo'yicha parametrlar va to'g'ridan-to'g'ri bron qilish.
  - O'ng tomonda biriktirilgan yopishqoq (sticky) narx kalkulyatori.
- **`/checkout` — Bron Qilish va To'lov Sahifasi:**
  - 3 bosqichli checkout jarayoni: Mehmon ma'lumotlari &rarr; Xizmatlar & To'lov &rarr; Tasdiqlash.
  - Qo'shimcha xizmatlar (Nonushta, Aeroport transferi, Spa) bilan real vaqtda avtomatik kvitansiya hisobi.
  - To'lov tizimlari: Payme, Click, Uzcard, Humo, Visa va Mastercard.
  - Muvaffaqiyatli to'lovdan so'ng rasmiy elektron vaucher berish va chop etish imkoniyati.
- **`/my-bookings` — Mening Bronlarim:**
  - Bronlar tarixi (Barchasi, Tasdiqlangan, Kutilmoqda, Bekor qilingan, Yakunlangan).
  - Har bir bron bo'yicha elektron vaucherni ko'rish, bekor qilish va sharh qoldirish.
- **`/favorites` — Sevimlilar (Wishlist):**
  - Saqlangan mehmonxonalarni boshqarish va tezkor bron qilish.
- **`/messages` — Xabarlar va Qo'llab-quvvatlash Markazi:**
  - Mehmonxona ma'muriyati va 24/7 operator bilan jonli muloqot chat interfeysi.
- **`/auth` — Kirish va Ro'yxatdan O'tish:**
  - Rol tanlash imkoniyati bilan to'liq avtorizatsiya va demo tezkor kirish tugmalari.

---

### 2. 🏨 Hotel Egasi (Partner Portali):
- **`/partner` — Dashboard:**
  - KPI ko'rsatkichlari: O'rinlar (128), Mehmonlar (256), Bandlik (68%), Oylik Tushum (24.5M).
  - SVG chiziqli oylik daromad grafigi va bronlar holati taqsimoti (Donut Chart).
  - So'nggi kelib tushgan buyurtmalar jadvali.
- **`/partner/calendar` — Xonalar Bandlik Taqvimi (Gantt Timeline):**
  - Xonalar (Deluxe, Suite, Standard, Twin) bo'yicha kunlik bandlik jadvali.
  - Rangli buyurtmalar bloklari (Band, Kutilmoqda, Bo'sh).
- **`/partner/bookings` — Bronlarni Boshqarish:**
  - Barcha bronlar ro'yxati, filtrlash, tasdiqlash yoki bekor qilish amallari.
- **`/partner/rooms` — Xonalar Fondini Boshqarish:**
  - Xonalar toifalari, qavatlari, sig'imi, narxlari va yangi xona qo'shish modali.
- **`/partner/reviews` — Sharhlar va Reyting:**
  - 5 yulduzli baholash tahlili, foizli progress barlar va mijozga javob yozish.

---

### 3. ⚡ Super Admin (Boshqaruv Paneli):
- **`/admin` — Global Tizim Dashboardi:**
  - Platforma statistikasi: 156 ta mehmonxona, 1856 ta foydalanuvchi, 125.4M so'm aylanma.
  - 12 oylik umumiy tushum ustunli diagrammasi (Bar Chart).
- **`/admin/hotels` — Mehmonxonalar Nazorati:**
  - Tizimdagi mehmonxonalarni tasdiqlash, tahrirlash va o'chirish.
- **`/admin/users` — Foydalanuvchilar va Rollar:**
  - Admin, Hotel, User rollari bo'yicha saralash, qidiruv va rollarni o'zgartirish.

---

### 4. 📐 16 Ekranli Master Blueprint:
- **`/blueprint` — Arxitekturaviy Poster:**
  - Dastlabki 16 ta modulni bir umumiy bento grid posterida ko'rish imkoniyati.

---

## 🎨 Dizayn va Texnik Afzalliklar:

- **Vue 3 (Composition API) + Vite + Vue Router 4**
- **To'liq Reaktiv Markaziy Holat (`src/store/index.js`):**
  - Mehmon tomonidan yangi bron amalga oshirilganda, u darhol Hotel Egasining bronlar jadvalida, taqvimida va Admin statistikasida aks etadi.
- **Yorug' (Light) & Qorong'i (Dark Blueprint) Mavzular:**
  - Navbar orqali istalgan paytda mavzuni o'zgartirish mumkin.
- **To'lov Tizimlari Simulyatsiyasi:**
  - Payme, Click, Uzcard va xalqaro kartalar orqali haqiqiy to'lov ssenariysi.

---

## 🚀 Ishga Tushirish

```powershell
# ZunoFront papkasiga o'ting
cd F:\APIS\Booking_System\ZunoFront

# Loyihani ishga tushiring
npm run dev
```

Brauzerda ochish manzili: **`http://localhost:3000`**
Build qilish uchun: **`npm run build`**
