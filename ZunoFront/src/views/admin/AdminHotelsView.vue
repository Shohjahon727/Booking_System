<template>
  <div class="dashboard-layout">
    <div class="dashboard-sidebar-wrap">
      <AdminSidebar />
    </div>

    <div class="dashboard-main-area">
      <div class="page-top-bar">
        <div class="page-title-box">
          <h2>Mehmonxonalar Boshqaruvi va Nazorati</h2>
          <p>Tizimga ulangan barcha mehmonxonalar, ularning holati, xonalar soni va reytingi</p>
        </div>
        <div class="page-actions-box">
          <button class="btn-primary" @click="addNewHotel">+ Yangi mehmonxona qo'shish</button>
        </div>
      </div>

      <!-- Hotels Table -->
      <div class="card">
        <div class="table-responsive">
          <table class="data-table">
            <thead>
              <tr>
                <th>Rasm</th>
                <th>Mehmonxona nomi</th>
                <th>Shahar & Manzil</th>
                <th>Xonalar soni</th>
                <th>Reyting</th>
                <th>Status</th>
                <th>Amallar</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="h in state.hotels" :key="h.id">
                <td>
                  <img :src="h.image" :alt="h.name" class="table-hotel-thumb" />
                </td>
                <td><b>{{ h.name }}</b></td>
                <td>{{ h.address }}</td>
                <td>{{ h.roomsCount }} ta xona</td>
                <td><span style="color:#f59e0b; font-weight:bold;">★ {{ h.rating }}</span></td>
                <td><span class="status-badge status-confirmed">{{ h.status }}</span></td>
                <td>
                  <div class="admin-action-btns">
                    <button class="btn-action" @click="editHotel(h)" title="Tahrirlash">✏️</button>
                    <button class="btn-action btn-del" @click="deleteHotel(h.id)" title="O'chirish">🗑️</button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { state } from '../../store'
import AdminSidebar from '../../components/layout/AdminSidebar.vue'

function addNewHotel() {
  const name = prompt("Yangi mehmonxona nomini kiriting:")
  if (name) {
    state.hotels.push({
      id: Date.now(),
      name,
      city: "Toshkent",
      address: "Toshkent shahri, Markaz",
      rating: 5.0,
      reviewsCount: 1,
      pricePerNight: 800000,
      featuredPriceText: "dan 400 000 so'm/kun",
      starCount: 5,
      verified: true,
      image: "https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=800&q=80",
      gallery: ["https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=800&q=80"],
      amenities: [{ name: "Wi-Fi", icon: "wifi" }],
      description: "Yangi ro'yxatga olingan mehmonxona",
      roomsCount: 20,
      status: "Active",
      isFavorite: false
    })
    alert("Mehmonxona tizimga muvaffaqiyatli qo'shildi!")
  }
}

function editHotel(h) {
  alert(`${h.name} parametrlarini tahrirlash oynasi.`)
}

function deleteHotel(id) {
  if (confirm("Ushbu mehmonxonani tizimdan o'chirishni xohlaysizmi?")) {
    state.hotels = state.hotels.filter(h => h.id !== id)
  }
}
</script>

<style scoped>
.table-hotel-thumb {
  width: 48px;
  height: 40px;
  border-radius: 4px;
  object-fit: cover;
}

.admin-action-btns {
  display: flex;
  gap: 4px;
}

.btn-action {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 28px;
  height: 28px;
  border-radius: 4px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
}

.btn-del:hover {
  background: var(--danger-bg);
  color: var(--danger);
  border-color: var(--danger);
}
</style>
