<template>
  <div class="dashboard-layout">
    <div class="dashboard-sidebar-wrap">
      <PartnerSidebar />
    </div>

    <div class="dashboard-main-area">
      <div class="page-top-bar">
        <div class="page-title-box">
          <h2>Xonalar Bandlik Taqvimi (May 2025)</h2>
          <p>Xonalarning kunlik bandligi, kutilayotgan va tasdiqlangan bronlar jadvali</p>
        </div>
        <div class="page-actions-box">
          <button class="btn-primary" @click="openAddBookingModal">+ Bron kiritish</button>
        </div>
      </div>

      <!-- Calendar Controls -->
      <div class="calendar-controls-card card">
        <div class="month-selector">
          <button class="month-nav-btn">&lt;</button>
          <span class="active-month-text">May 2025</span>
          <button class="month-nav-btn">&gt;</button>
        </div>

        <div class="view-mode-buttons">
          <button class="mode-btn active">Oylik</button>
          <button class="mode-btn">Haftalik</button>
          <button class="mode-btn">Bugun</button>
        </div>
      </div>

      <!-- Gantt Timeline Card -->
      <div class="card timeline-card">
        <div class="timeline-table-wrapper">
          <table class="gantt-table">
            <thead>
              <tr>
                <th class="room-col">Xona nomi</th>
                <th v-for="d in calendarDays" :key="d.num" class="day-header-cell">
                  <span class="d-week">{{ d.dayName }}</span>
                  <span class="d-num">{{ d.num }}</span>
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="r in timelineRooms" :key="r.id">
                <td class="room-name-cell">
                  <b>{{ r.name }}</b>
                  <small>{{ r.floor }}</small>
                </td>
                <td v-for="d in calendarDays" :key="d.num" class="day-slot">
                  <div
                    v-if="hasBooking(r, d.num)"
                    :class="['reservation-bar', getBookingStatus(r, d.num)]"
                    :title="getGuestName(r, d.num)"
                  >
                    {{ getGuestName(r, d.num) }}
                  </div>
                  <span v-else class="empty-slot">•</span>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="gantt-legend-footer">
          <div class="legend-item"><span class="legend-color-box bg-blue"></span> Band (Booked)</div>
          <div class="legend-item"><span class="legend-color-box bg-amber"></span> Kutilmoqda (Pending)</div>
          <div class="legend-item"><span class="legend-color-box bg-green"></span> Bo'sh (Available)</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { state } from '../../store'
import PartnerSidebar from '../../components/layout/PartnerSidebar.vue'

const calendarDays = [
  { dayName: 'Du', num: 25 },
  { dayName: 'Se', num: 26 },
  { dayName: 'Ch', num: 27 },
  { dayName: 'Pa', num: 28 },
  { dayName: 'Ju', num: 29 },
  { dayName: 'Sh', num: 30 },
  { dayName: 'Ya', num: 31 }
]

const timelineRooms = [
  {
    id: 1,
    name: 'Deluxe Room 101',
    floor: '1-qavat',
    bookings: [
      { start: 26, end: 28, guest: 'Alisher Normatov', status: 'booked' }
    ]
  },
  {
    id: 2,
    name: 'Suite Room 201',
    floor: '2-qavat',
    bookings: [
      { start: 25, end: 26, guest: 'Diyorbek Sodiqov', status: 'booked' },
      { start: 29, end: 30, guest: 'John Smith', status: 'booked' }
    ]
  },
  {
    id: 3,
    name: 'Standard Room 301',
    floor: '3-qavat',
    bookings: [
      { start: 27, end: 28, guest: 'Bobo Mirzo', status: 'pending' }
    ]
  },
  {
    id: 4,
    name: 'Deluxe Twin 401',
    floor: '4-qavat',
    bookings: [
      { start: 28, end: 30, guest: 'Arthur Vane', status: 'booked' }
    ]
  }
]

function hasBooking(room, dayNum) {
  return room.bookings.some(b => dayNum >= b.start && dayNum <= b.end)
}

function getGuestName(room, dayNum) {
  const b = room.bookings.find(b => dayNum >= b.start && dayNum <= b.end)
  return b ? b.guest : ''
}

function getBookingStatus(room, dayNum) {
  const b = room.bookings.find(b => dayNum >= b.start && dayNum <= b.end)
  return b ? b.status : ''
}

function openAddBookingModal() {
  state.modals.addBooking = true
}
</script>

<style scoped>
.calendar-controls-card {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 18px;
  flex-direction: row;
}

.month-selector {
  display: flex;
  align-items: center;
  gap: 12px;
}

.month-nav-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 28px;
  height: 28px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  font-weight: 800;
  color: var(--text-main);
}

.active-month-text {
  font-size: 15px;
  font-weight: 800;
  color: var(--text-main);
}

.view-mode-buttons {
  display: flex;
  gap: 6px;
}

.mode-btn {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 6px 12px;
  border-radius: var(--radius-sm);
  font-size: 11.5px;
  font-weight: 600;
  cursor: pointer;
  color: var(--text-muted);
}

.mode-btn.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

/* Gantt table */
.timeline-table-wrapper {
  overflow-x: auto;
}

.gantt-table {
  width: 100%;
  border-collapse: collapse;
}

.gantt-table th, .gantt-table td {
  border: 1px solid var(--border-light);
  padding: 10px 8px;
  text-align: center;
}

.room-col {
  width: 180px;
  text-align: left;
  background: var(--bg-subtle);
  color: var(--text-muted);
  font-size: 11px;
}

.day-header-cell {
  background: var(--bg-subtle);
}

.d-week {
  display: block;
  font-size: 10px;
  color: var(--text-muted);
}

.d-num {
  font-size: 14px;
  font-weight: 800;
  color: var(--text-main);
}

.room-name-cell {
  text-align: left;
}

.room-name-cell b {
  display: block;
  font-size: 13px;
  color: var(--text-main);
}

.room-name-cell small {
  font-size: 11px;
  color: var(--text-muted);
}

.day-slot {
  height: 48px;
  position: relative;
}

.reservation-bar {
  padding: 6px 8px;
  border-radius: var(--radius-sm);
  color: #fff;
  font-size: 11px;
  font-weight: 700;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
}

.reservation-bar.booked { background: #2563eb; }
.reservation-bar.pending { background: #f59e0b; }

.empty-slot {
  color: var(--success);
  font-size: 18px;
  opacity: 0.5;
}

.gantt-legend-footer {
  display: flex;
  justify-content: center;
  gap: 24px;
  padding-top: 16px;
  margin-top: 14px;
  border-top: 1px solid var(--border-light);
  font-size: 12px;
  color: var(--text-secondary);
}

.legend-item {
  display: flex;
  align-items: center;
  gap: 6px;
}

.legend-color-box {
  width: 12px;
  height: 12px;
  border-radius: 3px;
}

.bg-blue { background: #2563eb; }
.bg-amber { background: #f59e0b; }
.bg-green { background: #10b981; }
</style>
