<template>
  <div class="page-container page-06-calendar">
    <div class="content-container">
      <!-- Screen 06 Header Tag -->
      <div class="screen-identifier-pill">
        <span class="s-badge">EKRAN 06</span>
        <span class="s-title">XONALAR TAQVIMI (Hotel Egasi)</span>
        <span class="s-role">Gantt Timeline</span>
      </div>

      <div class="page-top-bar" style="margin-bottom: 20px;">
        <div class="page-title-box">
          <h2>Xonalar Taqvimi va Bandlik Jadvali</h2>
          <p>Xonalarning kunlar bo'yicha bandligi (May 2025) va bron muddatlari</p>
        </div>
        <div class="page-actions-box">
          <button class="btn-primary" @click="openAddBookingModal">+ Bron kiritish</button>
        </div>
      </div>

      <!-- Calendar Controls -->
      <div class="card calendar-controls-strip">
        <div class="month-box">
          <button class="nav-arrow">&lt;</button>
          <span class="month-label">May 2025</span>
          <button class="nav-arrow">&gt;</button>
        </div>

        <div class="time-filters">
          <button class="mode-pill active">Oylik</button>
          <button class="mode-pill">Haftalik</button>
          <button class="mode-pill">Bugun</button>
        </div>
      </div>

      <!-- Gantt Timeline Table Card -->
      <div class="card timeline-card">
        <div class="timeline-scroll-wrap">
          <table class="gantt-schedule-table">
            <thead>
              <tr>
                <th class="room-col-header">Xona nomi</th>
                <th v-for="d in calendarDays" :key="d.num" class="day-col-head">
                  <div class="week-name">{{ d.dayName }}</div>
                  <div class="num-day">{{ d.num }}</div>
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="r in timelineRooms" :key="r.id">
                <td class="room-title-td">
                  <b>{{ r.name }}</b>
                  <small>{{ r.floor }}</small>
                </td>
                <td v-for="d in calendarDays" :key="d.num" class="slot-td">
                  <div
                    v-if="hasBooking(r, d.num)"
                    :class="['reservation-pill-bar', getBookingStatus(r, d.num)]"
                    :title="getGuestName(r, d.num)"
                  >
                    {{ getGuestName(r, d.num) }}
                  </div>
                  <span v-else class="free-slot-dot">•</span>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="timeline-legend-row">
          <div class="legend-unit"><span class="legend-color-dot booked-dot">■</span> Band (Booked)</div>
          <div class="legend-unit"><span class="legend-color-dot pending-dot">■</span> Kutilmoqda (Pending)</div>
          <div class="legend-unit"><span class="legend-color-dot available-dot">■</span> Mavjud / Bo'sh (Available)</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { state } from '../../store'

const calendarDays = [
  { dayName: 'Dushanba', num: 25 },
  { dayName: 'Seshanba', num: 26 },
  { dayName: 'Chorshanba', num: 27 },
  { dayName: 'Payshanba', num: 28 },
  { dayName: 'Juma', num: 29 },
  { dayName: 'Shanba', num: 30 },
  { dayName: 'Yakshanba', num: 31 }
]

const timelineRooms = [
  {
    id: 1,
    name: '1-Xona (Deluxe 101)',
    floor: '1-qavat',
    bookings: [
      { start: 26, end: 28, guest: 'Alisher N. (Band)', status: 'booked' }
    ]
  },
  {
    id: 2,
    name: '2-Xona (Suite 201)',
    floor: '2-qavat',
    bookings: [
      { start: 25, end: 26, guest: 'Diyorbek S.', status: 'booked' },
      { start: 29, end: 30, guest: 'John Smith', status: 'booked' }
    ]
  },
  {
    id: 3,
    name: '3-Xona (Standard 301)',
    floor: '3-qavat',
    bookings: [
      { start: 27, end: 28, guest: 'Bobo Mirzo', status: 'pending' }
    ]
  },
  {
    id: 4,
    name: '4-Xona (Deluxe Twin 401)',
    floor: '4-qavat',
    bookings: [
      { start: 28, end: 30, guest: 'Arthur Vane (Band)', status: 'booked' }
    ]
  },
  {
    id: 5,
    name: '5-Xona (Suite 501)',
    floor: '5-qavat',
    bookings: [
      { start: 29, end: 31, guest: 'Rustam R.', status: 'booked' }
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
.page-06-calendar {
  padding-bottom: 50px;
}

.screen-identifier-pill {
  display: inline-flex;
  align-items: center;
  gap: 8px;
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 4px 12px;
  border-radius: var(--radius-full);
  margin-bottom: 16px;
  font-size: 11px;
}

.s-badge {
  background: var(--primary);
  color: #fff;
  font-weight: 800;
  padding: 2px 6px;
  border-radius: 4px;
}

.s-title {
  font-weight: 700;
  color: var(--text-main);
}

.s-role {
  color: #d97706;
  font-weight: 600;
}

.calendar-controls-strip {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 18px;
  margin-bottom: 20px;
  flex-direction: row;
}

.month-box {
  display: flex;
  align-items: center;
  gap: 12px;
}

.nav-arrow {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  width: 28px;
  height: 28px;
  border-radius: var(--radius-sm);
  cursor: pointer;
  font-weight: 800;
}

.month-label {
  font-size: 15px;
  font-weight: 800;
  color: var(--text-main);
}

.time-filters {
  display: flex;
  gap: 6px;
}

.mode-pill {
  background: var(--bg-subtle);
  border: 1px solid var(--border-subtle);
  padding: 6px 12px;
  border-radius: var(--radius-sm);
  font-size: 11.5px;
  font-weight: 600;
  color: var(--text-muted);
  cursor: pointer;
}

.mode-pill.active {
  background: var(--primary);
  color: #fff;
  border-color: var(--primary);
}

/* Timeline Table */
.timeline-scroll-wrap {
  overflow-x: auto;
}

.gantt-schedule-table {
  width: 100%;
  border-collapse: collapse;
}

.gantt-schedule-table th, .gantt-schedule-table td {
  border: 1px solid var(--border-light);
  padding: 12px 8px;
  text-align: center;
}

.room-col-header {
  width: 200px;
  text-align: left;
  background: var(--bg-subtle);
  color: var(--text-muted);
  font-size: 11px;
}

.day-col-head {
  background: var(--bg-subtle);
}

.week-name {
  font-size: 10px;
  color: var(--text-muted);
}

.num-day {
  font-size: 14px;
  font-weight: 800;
  color: var(--text-main);
}

.room-title-td {
  text-align: left;
}

.room-title-td b {
  display: block;
  font-size: 13px;
  color: var(--text-main);
}

.room-title-td small {
  font-size: 11px;
  color: var(--text-muted);
}

.slot-td {
  height: 52px;
  position: relative;
}

.reservation-pill-bar {
  padding: 8px 10px;
  border-radius: var(--radius-sm);
  color: #fff;
  font-size: 11.5px;
  font-weight: 700;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
}

.reservation-pill-bar.booked { background: #2563eb; }
.reservation-pill-bar.pending { background: #f59e0b; }

.free-slot-dot {
  color: var(--success);
  font-size: 20px;
  opacity: 0.4;
}

.timeline-legend-row {
  display: flex;
  justify-content: center;
  gap: 24px;
  padding-top: 16px;
  margin-top: 16px;
  border-top: 1px solid var(--border-light);
  font-size: 12px;
  color: var(--text-secondary);
}

.legend-unit {
  display: flex;
  align-items: center;
  gap: 6px;
}

.legend-color-dot {
  font-size: 14px;
}

.booked-dot { color: #2563eb; }
.pending-dot { color: #f59e0b; }
.available-dot { color: #10b981; }
</style>
