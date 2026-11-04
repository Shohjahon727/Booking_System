<template>
  <div class="messages-page page-container">
    <div class="content-container">
      <!-- Breadcrumb Navigation -->
      <nav class="breadcrumb-bar">
        <router-link to="/" class="b-link">Bosh sahifa</router-link>
        <span class="b-sep">/</span>
        <span class="b-current">16. Xabarlar & Qo'llab-quvvatlash</span>
      </nav>

      <!-- Page Header -->
      <div class="page-header-flex">
        <div class="header-titles">
          <div class="badge-tag">💬 ONLAYN MULOQOT</div>
          <h1 class="page-title">Xabarlar va Yordam Markazi</h1>
          <p class="page-subtitle">
            Mehmonxona xodimlari va 24/7 texnik yordam bilan to'g'ridan-to'g'ri tezkor muloqot qiling.
          </p>
        </div>

        <div class="header-action-badge">
          <span class="online-indicator"></span>
          <span class="online-text">24/7 Operatorlar liniyada faol</span>
        </div>
      </div>

      <!-- Main Chat Messenger Layout (2 Columns) -->
      <div class="chat-messenger-card card-surface">
        <!-- LEFT COLUMN: THREADS LIST -->
        <aside class="threads-sidebar">
          <div class="sidebar-header">
            <h2 class="sidebar-title">Muloqotlar ({{ threads.length }})</h2>
            <div class="search-box">
              <input
                v-model="searchQuery"
                type="text"
                placeholder="Xabarlardan qidirish..."
                class="search-input"
              />
            </div>
          </div>

          <!-- Filter Tabs -->
          <div class="chat-filter-tabs">
            <button
              class="tab-btn"
              :class="{ active: activeFilter === 'all' }"
              @click="activeFilter = 'all'"
            >
              Barchasi
            </button>
            <button
              class="tab-btn"
              :class="{ active: activeFilter === 'unread' }"
              @click="activeFilter = 'unread'"
            >
              O'qilmagan
            </button>
            <button
              class="tab-btn"
              :class="{ active: activeFilter === 'hotel' }"
              @click="activeFilter = 'hotel'"
            >
              Mehmonxonalar
            </button>
          </div>

          <!-- Threads List -->
          <div class="threads-list">
            <div
              v-for="thread in filteredThreads"
              :key="thread.id"
              class="thread-item"
              :class="{ active: selectedThreadId === thread.id, unread: thread.unread }"
              @click="selectThread(thread.id)"
            >
              <div class="thread-avatar-wrap">
                <div class="thread-avatar" :class="thread.avatarClass || 'default-avatar'">
                  {{ thread.avatar }}
                </div>
                <span v-if="thread.isOnline" class="status-dot"></span>
              </div>

              <div class="thread-info">
                <div class="thread-top">
                  <h4 class="sender-name">{{ thread.from }}</h4>
                  <span class="thread-time">{{ thread.time }}</span>
                </div>
                <p class="thread-snippet">{{ thread.text }}</p>
              </div>

              <span v-if="thread.unread" class="unread-pill">1</span>
            </div>

            <div v-if="filteredThreads.length === 0" class="empty-threads">
              Xabarlar topilmadi
            </div>
          </div>
        </aside>

        <!-- RIGHT COLUMN: ACTIVE CHAT CONVERSATION -->
        <main class="chat-viewport">
          <!-- Active Chat Top Bar -->
          <header class="chat-header">
            <div class="chat-partner-info">
              <div class="partner-avatar" :class="activeThread.avatarClass || 'default-avatar'">
                {{ activeThread.avatar }}
              </div>
              <div class="partner-meta">
                <h3 class="partner-name">{{ activeThread.from }}</h3>
                <span class="partner-status">
                  <span class="green-dot"></span>
                  {{ activeThread.isOnline ? 'Hozir tarmoqda (Tezkor javob beradi)' : 'Yaqinda faol edi' }}
                </span>
              </div>
            </div>

            <div class="header-actions">
              <button class="icon-btn" title="Telefon qo'ng'irog'i" @click="handleCall">
                📞 +998 71 200 80 80
              </button>
              <router-link to="/hotel/1" class="icon-btn outline">
                🏨 Mehmonxona sahifasi
              </router-link>
            </div>
          </header>

          <!-- Chat Conversation Log -->
          <div class="chat-messages-container" ref="messagesContainer">
            <div class="date-divider">
              <span>Bugun, 20-may</span>
            </div>

            <div
              v-for="msg in currentConversation"
              :key="msg.id"
              class="message-row"
              :class="{ 'outgoing': msg.isMine, 'incoming': !msg.isMine }"
            >
              <div v-if="!msg.isMine" class="msg-avatar">
                {{ activeThread.avatar }}
              </div>

              <div class="message-bubble">
                <p class="bubble-text">{{ msg.text }}</p>
                <div class="bubble-meta">
                  <span class="bubble-time">{{ msg.time }}</span>
                  <span v-if="msg.isMine" class="check-icon">✓✓</span>
                </div>
              </div>
            </div>

            <div v-if="isTyping" class="typing-indicator">
              <span class="dot"></span>
              <span class="dot"></span>
              <span class="dot"></span>
              <span class="typing-text">{{ activeThread.from }} yozmoqda...</span>
            </div>
          </div>

          <!-- Quick Template Replies -->
          <div class="quick-replies-strip">
            <span class="quick-title">Tezkor savollar:</span>
            <button
              v-for="reply in quickReplies"
              :key="reply"
              class="quick-pill"
              @click="sendQuickReply(reply)"
            >
              {{ reply }}
            </button>
          </div>

          <!-- Chat Input Composer -->
          <footer class="chat-composer">
            <input
              v-model="newMessageText"
              type="text"
              placeholder="Xabaringizni yozing..."
              class="composer-input"
              @keyup.enter="handleSendMessage"
            />
            <button
              class="send-btn"
              :disabled="!newMessageText.trim()"
              @click="handleSendMessage"
            >
              <span>Yuborish</span>
              <svg viewBox="0 0 24 24" width="16" height="16" fill="currentColor">
                <path d="M2.01 21L23 12 2.01 3 2 10l15 2-15 2z"/>
              </svg>
            </button>
          </footer>
        </main>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, nextTick } from 'vue'
import { state } from '../../store'

const searchQuery = ref('')
const activeFilter = ref('all')
const selectedThreadId = ref(1)
const newMessageText = ref('')
const isTyping = ref(false)
const messagesContainer = ref(null)

const threads = ref([
  {
    id: 1,
    from: "Grand Plaza Hotel (Qabulxona)",
    type: "hotel",
    time: "10:24",
    text: "Hurmatli mijoz, broningiz tasdiqlandi. Aeroportdan transfer kerakmi?",
    unread: false,
    avatar: "GP",
    avatarClass: "avatar-blue",
    isOnline: true
  },
  {
    id: 2,
    from: "Zuno 24/7 Qo'llab-quvvatlash",
    type: "support",
    time: "Kecha",
    text: "To'lov bilan bog'liq har qanday savolingiz bo'lsa, bajonidil yordam beramiz.",
    unread: false,
    avatar: "ZN",
    avatarClass: "avatar-purple",
    isOnline: true
  },
  {
    id: 3,
    from: "City Palace Hotel Menejeri",
    type: "hotel",
    time: "14 May",
    text: "Sizga maxsus 15% chegirmali vaucher taqdim etildi! Foydalanish kodi: CITY2025",
    unread: false,
    avatar: "CP",
    avatarClass: "avatar-green",
    isOnline: false
  },
  {
    id: 4,
    from: "Xavfsizlik va Administrator",
    type: "support",
    time: "12 May",
    text: "Hisobingizga muvaffaqiyatli kirildi. Profil ma'lumotlaringiz yangilandi.",
    unread: false,
    avatar: "XA",
    avatarClass: "avatar-orange",
    isOnline: true
  }
])

// Separate chat logs for each thread
const conversations = ref({
  1: [
    { id: 1, text: "Assalomu alaykum! Grand Plaza Hotel'ga xush kelibsiz.", isMine: false, time: "10:20" },
    { id: 2, text: "Hurmatli mijoz, Deluxe Room uchun broningiz qabul qilindi.", isMine: false, time: "10:21" },
    { id: 3, text: "Assalomu alaykum. Rahmat! Nonushta narx ichiga kiritilganmi?", isMine: true, time: "10:22" },
    { id: 4, text: "Ha, albatta. Har kuni 07:00 dan 10:30 gacha shved stoli nonushtasi taqdim etiladi.", isMine: false, time: "10:23" },
    { id: 5, text: "Shuningdek, aeroportdan bepul transfer xizmatimiz ham mavjud. Reys raqamingizni yuborsangiz haydovchi kutib oladi.", isMine: false, time: "10:24" }
  ],
  2: [
    { id: 1, text: "Zuno qo'llab-quvvatlash markaziga xush kelibsiz! Qanday yordam bera olamiz?", isMine: false, time: "09:00" },
    { id: 2, text: "To'lov qilingandan keyin kvitansiyani qayerdan yuklab olsam bo'ladi?", isMine: true, time: "09:05" },
    { id: 3, text: "Mening Bronlarim (Screen 05) sahifasida har bir bron qarshisida 'PDF Vaucher' tugmasi mavjud.", isMine: false, time: "09:06" }
  ],
  3: [
    { id: 1, text: "City Palace Hotel sizga 15% chegirmali promokod sovg'a qiladi: CITY2025", isMine: false, time: "14 May" }
  ],
  4: [
    { id: 1, text: "Tizim xabarnomasi: Yangi brauzerdan login qilindi.", isMine: false, time: "12 May" }
  ]
})

const quickReplies = [
  "Nonushta vaqti qachon?",
  "Reys HY-054, transfer bormi?",
  "Erta kirish (Early check-in) mumkinmi?",
  "Kvitansiyani yubora olasizmi?"
]

const filteredThreads = computed(() => {
  return threads.value.filter(t => {
    const matchesSearch = t.from.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
                          t.text.toLowerCase().includes(searchQuery.value.toLowerCase())
    if (activeFilter.value === 'unread') return matchesSearch && t.unread
    if (activeFilter.value === 'hotel') return matchesSearch && t.type === 'hotel'
    return matchesSearch
  })
})

const activeThread = computed(() => {
  return threads.value.find(t => t.id === selectedThreadId.value) || threads.value[0]
})

const currentConversation = computed(() => {
  return conversations.value[selectedThreadId.value] || []
})

function selectThread(id) {
  selectedThreadId.value = id
  const th = threads.value.find(t => t.id === id)
  if (th) th.unread = false
  scrollToBottom()
}

function scrollToBottom() {
  nextTick(() => {
    if (messagesContainer.value) {
      messagesContainer.value.scrollTop = messagesContainer.value.scrollHeight
    }
  })
}

function handleSendMessage() {
  const text = newMessageText.value.trim()
  if (!text) return

  if (!conversations.value[selectedThreadId.value]) {
    conversations.value[selectedThreadId.value] = []
  }

  const now = new Date()
  const timeStr = `${String(now.getHours()).padStart(2, '0')}:${String(now.getMinutes()).padStart(2, '0')}`

  // Append user message
  conversations.value[selectedThreadId.value].push({
    id: Date.now(),
    text,
    isMine: true,
    time: timeStr
  })

  // Update thread snippet
  const th = threads.value.find(t => t.id === selectedThreadId.value)
  if (th) {
    th.text = text
    th.time = timeStr
  }

  newMessageText.value = ''
  scrollToBottom()

  // Simulate auto reply from hotel/support
  isTyping.value = true
  setTimeout(() => {
    isTyping.value = false
    const replies = [
      "Qabul qilindi! Menejerimiz tez orada sizga batafsil ma'lumot beradi.",
      "Xabaringiz uchun tashakkur! Broningizga tegishli barcha ma'lumotlar tizimda saqlandi.",
      "Albatta, ushbu xizmatni siz uchun tashkillashtiramiz. Administrator siz bilan aloqada bo'ladi."
    ]
    const randomReply = replies[Math.floor(Math.random() * replies.length)]

    conversations.value[selectedThreadId.value].push({
      id: Date.now() + 1,
      text: randomReply,
      isMine: false,
      time: timeStr
    })
    if (th) {
      th.text = randomReply
    }
    scrollToBottom()
  }, 1200)
}

function sendQuickReply(replyText) {
  newMessageText.value = replyText
  handleSendMessage()
}

function handleCall() {
  alert("Tezkor qo'ng'iroq: +998 71 200 80 80 raqami orqali operatorlarimiz bilan bog'lanishingiz mumkin.")
}
</script>

<style scoped>
.messages-page {
  padding-bottom: 50px;
}

.breadcrumb-bar {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 13px;
  color: #64748b;
  margin-bottom: 20px;
}

.b-link {
  color: #3b82f6;
  text-decoration: none;
}

.b-link:hover {
  text-decoration: underline;
}

.b-sep {
  color: #cbd5e1;
}

.b-current {
  color: #1e293b;
  font-weight: 600;
}

/* Header */
.page-header-flex {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  margin-bottom: 24px;
  gap: 20px;
}

.badge-tag {
  display: inline-block;
  background: #e0e7ff;
  color: #4338ca;
  font-size: 11px;
  font-weight: 700;
  padding: 4px 10px;
  border-radius: 6px;
  margin-bottom: 8px;
  letter-spacing: 0.5px;
}

.page-title {
  font-size: 26px;
  font-weight: 800;
  color: #0f172a;
  margin-bottom: 6px;
}

.page-subtitle {
  font-size: 14px;
  color: #64748b;
}

.header-action-badge {
  display: flex;
  align-items: center;
  gap: 8px;
  background: #f0fdf4;
  border: 1px solid #bbf7d0;
  padding: 8px 16px;
  border-radius: 20px;
}

.online-indicator {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background: #22c55e;
  box-shadow: 0 0 0 3px rgba(34, 197, 94, 0.2);
}

.online-text {
  font-size: 12px;
  font-weight: 600;
  color: #166534;
}

/* 2-Column Messenger Layout */
.chat-messenger-card {
  display: grid;
  grid-template-columns: 360px 1fr;
  height: 650px;
  border-radius: 16px;
  overflow: hidden;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.05);
}

/* LEFT SIDEBAR */
.threads-sidebar {
  border-right: 1px solid #e2e8f0;
  display: flex;
  flex-direction: column;
  background: #ffffff;
}

.sidebar-header {
  padding: 18px;
  border-bottom: 1px solid #f1f5f9;
}

.sidebar-title {
  font-size: 16px;
  font-weight: 700;
  color: #0f172a;
  margin-bottom: 12px;
}

.search-input {
  width: 100%;
  padding: 8px 14px;
  border: 1px solid #cbd5e1;
  border-radius: 8px;
  font-size: 13px;
  outline: none;
  background: #f8fafc;
}

.search-input:focus {
  border-color: #3b82f6;
  background: #ffffff;
}

.chat-filter-tabs {
  display: flex;
  padding: 8px 14px;
  gap: 6px;
  border-bottom: 1px solid #f1f5f9;
  background: #f8fafc;
}

.tab-btn {
  background: transparent;
  border: none;
  padding: 6px 12px;
  font-size: 12px;
  font-weight: 600;
  color: #64748b;
  border-radius: 6px;
  cursor: pointer;
  transition: all 0.2s;
}

.tab-btn.active {
  background: #ffffff;
  color: #2563eb;
  box-shadow: 0 1px 3px rgba(0,0,0,0.08);
}

.threads-list {
  flex: 1;
  overflow-y: auto;
}

.thread-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 14px 18px;
  border-bottom: 1px solid #f8fafc;
  cursor: pointer;
  transition: background 0.15s;
  position: relative;
}

.thread-item:hover {
  background: #f8fafc;
}

.thread-item.active {
  background: #eff6ff;
  border-left: 4px solid #2563eb;
}

.thread-avatar-wrap {
  position: relative;
}

.thread-avatar {
  width: 44px;
  height: 44px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 700;
  font-size: 15px;
  color: #ffffff;
}

.avatar-blue { background: #3b82f6; }
.avatar-purple { background: #8b5cf6; }
.avatar-green { background: #10b981; }
.avatar-orange { background: #f97316; }
.default-avatar { background: #64748b; }

.status-dot {
  position: absolute;
  bottom: 0;
  right: 0;
  width: 11px;
  height: 11px;
  background: #22c55e;
  border: 2px solid #ffffff;
  border-radius: 50%;
}

.thread-info {
  flex: 1;
  min-width: 0;
}

.thread-top {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 4px;
}

.sender-name {
  font-size: 14px;
  font-weight: 600;
  color: #1e293b;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.thread-time {
  font-size: 11px;
  color: #94a3b8;
}

.thread-snippet {
  font-size: 12px;
  color: #64748b;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.unread-pill {
  background: #ef4444;
  color: #ffffff;
  font-size: 11px;
  font-weight: 700;
  border-radius: 10px;
  padding: 2px 7px;
}

.empty-threads {
  text-align: center;
  padding: 40px 20px;
  color: #94a3b8;
  font-size: 13px;
}

/* RIGHT VIEWPORT */
.chat-viewport {
  display: flex;
  flex-direction: column;
  background: #ffffff;
}

.chat-header {
  padding: 14px 20px;
  border-bottom: 1px solid #e2e8f0;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #ffffff;
}

.chat-partner-info {
  display: flex;
  align-items: center;
  gap: 12px;
}

.partner-avatar {
  width: 42px;
  height: 42px;
  border-radius: 50%;
  color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 700;
  font-size: 14px;
}

.partner-name {
  font-size: 15px;
  font-weight: 700;
  color: #0f172a;
}

.partner-status {
  font-size: 12px;
  color: #64748b;
  display: flex;
  align-items: center;
  gap: 6px;
}

.green-dot {
  width: 7px;
  height: 7px;
  background: #22c55e;
  border-radius: 50%;
}

.header-actions {
  display: flex;
  gap: 8px;
}

.icon-btn {
  padding: 7px 12px;
  border: 1px solid #e2e8f0;
  border-radius: 8px;
  background: #ffffff;
  font-size: 12px;
  font-weight: 600;
  color: #334155;
  cursor: pointer;
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  gap: 6px;
}

.icon-btn:hover {
  background: #f8fafc;
}

.icon-btn.outline {
  border-color: #3b82f6;
  color: #2563eb;
}

/* Messages Container */
.chat-messages-container {
  flex: 1;
  padding: 20px;
  overflow-y: auto;
  background: #f8fafc;
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.date-divider {
  text-align: center;
  position: relative;
  margin: 10px 0;
}

.date-divider span {
  background: #e2e8f0;
  color: #475569;
  font-size: 11px;
  font-weight: 600;
  padding: 3px 12px;
  border-radius: 12px;
}

.message-row {
  display: flex;
  gap: 8px;
  max-width: 75%;
}

.message-row.incoming {
  align-self: flex-start;
}

.message-row.outgoing {
  align-self: flex-end;
  flex-direction: row-reverse;
}

.msg-avatar {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  background: #3b82f6;
  color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 11px;
  font-weight: 700;
  flex-shrink: 0;
}

.message-bubble {
  padding: 10px 14px;
  border-radius: 14px;
  position: relative;
}

.incoming .message-bubble {
  background: #ffffff;
  border: 1px solid #e2e8f0;
  border-bottom-left-radius: 2px;
  color: #1e293b;
}

.outgoing .message-bubble {
  background: #2563eb;
  color: #ffffff;
  border-bottom-right-radius: 2px;
}

.bubble-text {
  font-size: 13px;
  line-height: 1.5;
  margin-bottom: 4px;
  word-break: break-word;
}

.bubble-meta {
  display: flex;
  justify-content: flex-end;
  align-items: center;
  gap: 4px;
}

.bubble-time {
  font-size: 10px;
  opacity: 0.75;
}

.check-icon {
  font-size: 10px;
}

/* Typing Indicator */
.typing-indicator {
  display: inline-flex;
  align-items: center;
  gap: 4px;
  background: #ffffff;
  border: 1px solid #e2e8f0;
  padding: 8px 12px;
  border-radius: 16px;
  align-self: flex-start;
}

.typing-indicator .dot {
  width: 6px;
  height: 6px;
  background: #94a3b8;
  border-radius: 50%;
  animation: bounce 1.4s infinite ease-in-out both;
}

.typing-indicator .dot:nth-child(1) { animation-delay: -0.32s; }
.typing-indicator .dot:nth-child(2) { animation-delay: -0.16s; }

@keyframes bounce {
  0%, 80%, 100% { transform: scale(0); }
  40% { transform: scale(1); }
}

.typing-text {
  font-size: 11px;
  color: #64748b;
  margin-left: 4px;
}

/* Quick Replies Strip */
.quick-replies-strip {
  padding: 8px 16px;
  background: #f1f5f9;
  border-top: 1px solid #e2e8f0;
  display: flex;
  align-items: center;
  gap: 8px;
  overflow-x: auto;
}

.quick-title {
  font-size: 11px;
  font-weight: 700;
  color: #64748b;
  white-space: nowrap;
}

.quick-pill {
  background: #ffffff;
  border: 1px solid #cbd5e1;
  border-radius: 14px;
  padding: 4px 10px;
  font-size: 12px;
  color: #334155;
  cursor: pointer;
  white-space: nowrap;
  transition: all 0.15s;
}

.quick-pill:hover {
  background: #2563eb;
  color: #ffffff;
  border-color: #2563eb;
}

/* Composer */
.chat-composer {
  padding: 12px 18px;
  border-top: 1px solid #e2e8f0;
  display: flex;
  gap: 10px;
  background: #ffffff;
}

.composer-input {
  flex: 1;
  padding: 10px 14px;
  border: 1px solid #cbd5e1;
  border-radius: 8px;
  font-size: 13px;
  outline: none;
}

.composer-input:focus {
  border-color: #2563eb;
}

.send-btn {
  background: #2563eb;
  color: #ffffff;
  border: none;
  padding: 8px 16px;
  border-radius: 8px;
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  display: flex;
  align-items: center;
  gap: 6px;
  transition: background 0.15s;
}

.send-btn:hover:not(:disabled) {
  background: #1d4ed8;
}

.send-btn:disabled {
  background: #cbd5e1;
  cursor: not-allowed;
}

@media (max-width: 860px) {
  .chat-messenger-card {
    grid-template-columns: 1fr;
    height: auto;
  }
  .threads-sidebar {
    height: 300px;
  }
  .chat-viewport {
    height: 500px;
  }
}
</style>
