<template>
  <div class="blueprint-view-page">
    <div class="content-container">
      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>16 Ekranli Tizim Master Blueprint (Poster Ko'rinishi)</h2>
          <p>Mehmonxona bron qilish tizimining barcha modullari bir umumiy arxitekturaviy kartada</p>
        </div>
        <div class="page-actions-box">
          <router-link to="/" class="btn-primary">
            ← To'liq Ilovaga Qaytish
          </router-link>
        </div>
      </div>

      <!-- Bento Grid (Row 1 to 5) -->
      <div class="bento-container">
        <!-- ROW 1: USER DISCOVERY (01, 02, 03) -->
        <div class="bento-row row-user">
          <Screen01BoshSahifa />
          <Screen02MehmonxonaDetali />
          <Screen03XonalarniTanlash />
        </div>

        <!-- ROW 2: BOOKING FLOW & TIMELINE (04, 05, 06) -->
        <div class="bento-row row-booking">
          <Screen04BronQilish />
          <Screen05Bronlarim />
          <Screen06XonalarTaqvimi />
        </div>

        <!-- ROW 3: PARTNER MANAGEMENT & ANALYTICS (07, 08, 09) -->
        <div class="bento-row row-partner">
          <Screen07BronlarBoshqaruv />
          <Screen08Sharhlar />
          <Screen09StatistikaEgasi />
        </div>

        <!-- ROW 4: ADMIN MODULES & AUTH & PAYMENT (10, 11, 12, 14) -->
        <div class="bento-row row-admin">
          <Screen10AdminDashboard />
          <Screen11XonalarJadvali />
          <Screen12Foydalanuvchilar />
          <Screen14TolovSahifasi />
        </div>

        <!-- ROW 5: EXTRAS (13 Login/Register & 16 Sevimlilar/Xabarlar) -->
        <div class="bento-row row-extras">
          <Screen13LoginRegister />
          <Screen16Xabarlar />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import Screen01BoshSahifa from '../components/screens/Screen01BoshSahifa.vue'
import Screen02MehmonxonaDetali from '../components/screens/Screen02MehmonxonaDetali.vue'
import Screen03XonalarniTanlash from '../components/screens/Screen03XonalarniTanlash.vue'
import Screen04BronQilish from '../components/screens/Screen04BronQilish.vue'
import Screen05Bronlarim from '../components/screens/Screen05Bronlarim.vue'
import Screen06XonalarTaqvimi from '../components/screens/Screen06XonalarTaqvimi.vue'
import Screen07BronlarBoshqaruv from '../components/screens/Screen07BronlarBoshqaruv.vue'
import Screen08Sharhlar from '../components/screens/Screen08Sharhlar.vue'
import Screen09StatistikaEgasi from '../components/screens/Screen09StatistikaEgasi.vue'
import Screen10AdminDashboard from '../components/screens/Screen10AdminDashboard.vue'
import Screen11XonalarJadvali from '../components/screens/Screen11XonalarJadvali.vue'
import Screen12Foydalanuvchilar from '../components/screens/Screen12Foydalanuvchilar.vue'
import Screen13LoginRegister from '../components/screens/Screen13LoginRegister.vue'
import Screen14TolovSahifasi from '../components/screens/Screen14TolovSahifasi.vue'
import Screen16Xabarlar from '../components/screens/Screen16Xabarlar.vue'
</script>

<style scoped>
.blueprint-view-page {
  padding-bottom: 50px;
}
</style>
