<template>
  <div class="messages-page">
    <div class="content-container">
      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Xabarlar va Muloqot Markazi</h2>
          <p>Mehmonxona ma'muriyati va 24/7 qo'llab-quvvatlash xizmati bilan to'g'ridan-to'g'ri aloqa</p>
        </div>
        <div class="page-actions-box">
          <span class="status-badge status-confirmed">● Jonli aloqa (Online)</span>
        </div>
      </div>

      <div class="chat-messenger-layout card">
        <!-- 1. Left Dialogs Sidebar -->
        <aside class="chat-conversations-aside">
          <div class="chat-search-input-wrap">
            <input type="text" placeholder="Suhbatlarni qidirish..." class="form-input" />
          </div>

          <div class="chat-threads-list">
            <div
              v-for="msg in state.messages"
              :key="msg.id"
              class="chat-thread-item"
              :class="{ active: activeMsgId === msg.id, unread: msg.unread }"
              @click="selectThread(msg)"
            >
              <div class="thread-avatar">{{ msg.avatar }}</div>
              <div class="thread-meta">
                <div class="thread-header">
                  <span class="thread-sender">{{ msg.from }}</span>
                  <span class="thread-time">{{ msg.time }}</span>
                </div>
                <div class="thread-snippet">{{ msg.text }}</div>
              </div>
            </div>
          </div>
        </aside>

        <!-- 2. Right Active Conversation -->
        <main class="chat-active-window">
          <div class="chat-active-header">
            <div class="chat-peer-avatar">{{ activeThread.avatar }}</div>
            <div>
              <h4 class="chat-peer-name">{{ activeThread.from }}</h4>
              <span class="chat-peer-status">● 24/7 Faol qo'llab-quvvatlash</span>
            </div>
          </div>

          <!-- Message bubbles -->
          <div class="chat-bubbles-scroll">
            <div class="chat-bubble incoming">
              <div class="bubble-sender">{{ activeThread.from }}</div>
              <div class="bubble-text">{{ activeThread.text }}</div>
              <div class="bubble-time">{{ activeThread.time }}</div>
            </div>

            <div v-for="(m, i) in customReplies" :key="i" class="chat-bubble outgoing">
              <div class="bubble-text">{{ m.text }}</div>
              <div class="bubble-time">{{ m.time }}</div>
            </div>
          </div>

          <!-- Input bar -->
          <div class="chat-input-bar">
            <input
              type="text"
              v-model="replyText"
              placeholder="Xabaringizni yozing..."
              class="form-input chat-text-field"
              @keyup.enter="sendMessage"
            />
            <button class="btn-primary send-msg-btn" @click="sendMessage">
              Yuborish &rarr;
            </button>
          </div>
        </main>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { state } from '../store'

const activeMsgId = ref(1)
const activeThread = ref(state.messages[0])
const replyText = ref('')
const customReplies = ref([
  { text: "Assalomu alaykum! Ha, aeroportdan transfer kerak bo'ladi.", time: "10:28" }
])

function selectThread(msg) {
  activeMsgId.value = msg.id
  activeThread.value = msg
  msg.unread = false
}

function sendMessage() {
  if (!replyText.value.trim()) return
  customReplies.value.push({
    text: replyText.value.trim(),
    time: "Hozirgina"
  })
  replyText.value = ''
}
</script>

<style scoped>
.messages-page {
  padding-bottom: 50px;
}

.chat-messenger-layout {
  display: grid;
  grid-template-columns: 320px 1fr;
  height: 580px;
  padding: 0;
  overflow: hidden;
}

/* Sidebar */
.chat-conversations-aside {
  border-right: 1px solid var(--border-light);
  display: flex;
  flex-direction: column;
}

.chat-search-input-wrap {
  padding: 14px;
  border-bottom: 1px solid var(--border-light);
}

.chat-threads-list {
  flex: 1;
  overflow-y: auto;
}

.chat-thread-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 12px 14px;
  border-bottom: 1px solid var(--border-light);
  cursor: pointer;
  transition: all 0.15s;
}

.chat-thread-item:hover {
  background: var(--bg-subtle);
}

.chat-thread-item.active {
  background: var(--primary-light);
  border-color: transparent;
}

.chat-thread-item.unread .thread-sender {
  font-weight: 800;
  color: var(--primary);
}

.thread-avatar {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  background: var(--primary);
  color: #fff;
  font-size: 12px;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}

.thread-meta {
  flex: 1;
  min-width: 0;
}

.thread-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 2px;
}

.thread-sender {
  font-size: 13px;
  font-weight: 700;
  color: var(--text-main);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.thread-time {
  font-size: 10px;
  color: var(--text-muted);
}

.thread-snippet {
  font-size: 11px;
  color: var(--text-secondary);
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

/* Active window */
.chat-active-window {
  display: flex;
  flex-direction: column;
  background: var(--bg);
}

.chat-active-header {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 14px 20px;
  background: var(--card-bg);
  border-bottom: 1px solid var(--border-light);
}

.chat-peer-avatar {
  width: 38px;
  height: 38px;
  border-radius: 50%;
  background: var(--primary);
  color: #fff;
  font-size: 13px;
  font-weight: 800;
  display: flex;
  align-items: center;
  justify-content: center;
}

.chat-peer-name {
  font-size: 14px;
  font-weight: 800;
  color: var(--text-main);
}

.chat-peer-status {
  font-size: 10.5px;
  color: var(--success);
}

.chat-bubbles-scroll {
  flex: 1;
  padding: 20px;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 14px;
}

.chat-bubble {
  max-width: 70%;
  padding: 12px 16px;
  border-radius: var(--radius-md);
  position: relative;
}

.chat-bubble.incoming {
  align-self: flex-start;
  background: var(--card-bg);
  border: 1px solid var(--card-border);
  color: var(--text-main);
  border-bottom-left-radius: 2px;
}

.chat-bubble.outgoing {
  align-self: flex-end;
  background: var(--primary);
  color: #ffffff;
  border-bottom-right-radius: 2px;
}

.bubble-sender {
  font-size: 10.5px;
  font-weight: 700;
  color: var(--primary);
  margin-bottom: 4px;
}

.bubble-text {
  font-size: 12.5px;
  line-height: 1.4;
}

.bubble-time {
  font-size: 9px;
  opacity: 0.7;
  text-align: right;
  margin-top: 4px;
}

.chat-input-bar {
  display: flex;
  gap: 10px;
  padding: 14px 20px;
  background: var(--card-bg);
  border-top: 1px solid var(--border-light);
}

.chat-text-field {
  flex: 1;
}

.send-msg-btn {
  padding: 0 20px;
  font-size: 12.5px;
}

@media (max-width: 768px) {
  .chat-messenger-layout {
    grid-template-columns: 1fr;
  }
  .chat-conversations-aside {
    display: none;
  }
}
</style>
