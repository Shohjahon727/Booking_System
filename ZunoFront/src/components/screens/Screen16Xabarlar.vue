<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">16</span> SEVIMLILAR & XABARLAR
      </span>
      <span class="card-badge">2 Yangi xabar</span>
    </div>

    <div class="favs-and-messages-grid">
      <!-- Left: Favorites List -->
      <div class="favs-col">
        <div class="sub-column-title">❤️ Sevimlilar</div>
        <div class="sub-items-scroll">
          <div
            v-for="hotel in favoriteHotels"
            :key="hotel.id"
            class="fav-mini-item"
            @click="selectHotel(hotel.id)"
          >
            <div class="fav-mini-thumb">
              <img :src="hotel.image" :alt="hotel.name" />
            </div>
            <div class="fav-mini-info">
              <div class="fav-mini-name">{{ hotel.name }}</div>
              <div class="fav-mini-price">{{ hotel.featuredPriceText }}</div>
            </div>
            <button
              class="fav-heart-btn"
              @click.stop="toggleFavorite(hotel.id)"
            >
              ❤️
            </button>
          </div>
        </div>
      </div>

      <!-- Right: Messages List -->
      <div class="messages-col">
        <div class="sub-column-title">💬 Xabarlar & Chat</div>
        <div class="sub-items-scroll">
          <div
            v-for="msg in state.messages"
            :key="msg.id"
            class="msg-bubble-item"
            :class="{ unread: msg.unread }"
            @click="msg.unread = false"
          >
            <div class="msg-avatar-circle">{{ msg.avatar }}</div>
            <div class="msg-content-box">
              <div class="msg-meta-row">
                <span class="msg-sender">{{ msg.from }}</span>
                <span class="msg-time">{{ msg.time }}</span>
              </div>
              <div class="msg-text-snippet">{{ msg.text }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { state, favoriteHotels, toggleFavorite, selectHotel } from '../../store'
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

.favs-and-messages-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 8px;
  flex: 1;
}

.favs-col, .messages-col {
  display: flex;
  flex-direction: column;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  padding: 6px;
}

.sub-column-title {
  font-size: 9px;
  font-weight: 800;
  color: var(--text-muted);
  text-transform: uppercase;
  margin-bottom: 6px;
  padding-bottom: 3px;
  border-bottom: 1px solid var(--border-light);
}

.sub-items-scroll {
  display: flex;
  flex-direction: column;
  gap: 4px;
  overflow-y: auto;
}

.fav-mini-item {
  display: flex;
  align-items: center;
  gap: 6px;
  background: var(--card-bg);
  border: 1px solid var(--border-light);
  border-radius: 4px;
  padding: 4px;
  cursor: pointer;
}

.fav-mini-thumb {
  width: 28px;
  height: 24px;
  border-radius: 3px;
  overflow: hidden;
  background: #1e293b;
  flex-shrink: 0;
}

.fav-mini-thumb img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.fav-mini-info {
  flex: 1;
  min-width: 0;
}

.fav-mini-name {
  font-size: 9.5px;
  font-weight: 700;
  color: var(--text-main);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.fav-mini-price {
  font-size: 8px;
  color: var(--primary);
  font-weight: 600;
}

.fav-heart-btn {
  background: transparent;
  border: none;
  cursor: pointer;
  font-size: 10px;
}

/* Messages */
.msg-bubble-item {
  display: flex;
  align-items: flex-start;
  gap: 6px;
  background: var(--card-bg);
  border: 1px solid var(--border-light);
  border-radius: 4px;
  padding: 4px 6px;
  cursor: pointer;
  transition: border-color 0.15s;
}

.msg-bubble-item.unread {
  border-color: var(--primary);
  background: var(--primary-light);
}

.msg-avatar-circle {
  width: 20px;
  height: 20px;
  border-radius: 50%;
  background: var(--primary);
  color: #fff;
  font-size: 8px;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}

.msg-content-box {
  flex: 1;
  min-width: 0;
}

.msg-meta-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1px;
}

.msg-sender {
  font-size: 9px;
  font-weight: 700;
  color: var(--text-main);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.msg-time {
  font-size: 7.5px;
  color: var(--text-muted);
}

.msg-text-snippet {
  font-size: 8px;
  color: var(--text-secondary);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

@media (max-width: 600px) {
  .favs-and-messages-grid {
    grid-template-columns: 1fr;
  }
}
</style>
