<template>
  <div class="card screen-card">
    <div class="card-header">
      <span class="card-title">
        <span class="screen-num">06</span> XONALAR TAQVIMI
      </span>
      <div class="calendar-mode-pills">
        <span class="cal-pill active">May 2025</span>
        <span class="cal-pill">Hafta</span>
        <span class="cal-pill">Bugun</span>
      </div>
    </div>

    <!-- Timeline Gantt Grid Table -->
    <div class="calendar-scroll-wrapper">
      <table class="timeline-table">
        <thead>
          <tr>
            <th class="room-col-header">Xona</th>
            <th v-for="day in state.calendar.days" :key="day.dayNum" class="day-col-header">
              <div class="day-name">{{ day.name.slice(0, 2) }}</div>
              <div class="day-num">{{ day.dayNum }}</div>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="room in state.calendar.rooms" :key="room.id">
            <td class="room-label-cell">
              {{ room.name.split('(')[0].trim() }}
            </td>
            <td
              v-for="day in state.calendar.days"
              :key="day.dayNum"
              class="day-slot-cell"
            >
              <div
                v-if="getBookingForDay(room, day.dayNum)"
                :class="['reservation-bar', getBookingForDay(room, day.dayNum).status]"
                :title="getBookingForDay(room, day.dayNum).guest"
              >
                {{ getBookingForDay(room, day.dayNum).guest }}
              </div>
              <span v-else class="slot-available">•</span>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Legend -->
    <div class="calendar-legend-bar">
      <div class="legend-item">
        <span class="legend-dot dot-booked">■</span> Band (Booked)
      </div>
      <div class="legend-item">
        <span class="legend-dot dot-pending">■</span> Kutilmoqda (Pending)
      </div>
      <div class="legend-item">
        <span class="legend-dot dot-available">■</span> Bo'sh (Available)
      </div>
    </div>
  </div>
</template>

<script setup>
import { state } from '../../store'

function getBookingForDay(room, dayNum) {
  return room.bookings.find(b => dayNum >= b.startDay && dayNum < b.startDay + b.duration)
}
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

.calendar-mode-pills {
  display: flex;
  gap: 4px;
}

.cal-pill {
  font-size: 8.5px;
  font-weight: 700;
  padding: 2px 6px;
  border-radius: var(--radius-sm);
  background: var(--bg-subtle);
  color: var(--text-muted);
  cursor: pointer;
}

.cal-pill.active {
  background: var(--primary);
  color: #fff;
}

.calendar-scroll-wrapper {
  overflow-x: auto;
  margin-bottom: 8px;
}

.timeline-table {
  width: 100%;
  border-collapse: collapse;
  font-size: 9.5px;
  text-align: center;
}

.timeline-table th, .timeline-table td {
  border: 1px solid var(--border-light);
  padding: 4px;
}

.room-col-header {
  width: 70px;
  background: var(--bg-subtle);
  color: var(--text-muted);
  font-weight: 700;
  text-align: left;
  padding-left: 6px;
}

.day-col-header {
  background: var(--bg-subtle);
  color: var(--text-muted);
}

.day-name {
  font-size: 8px;
  text-transform: uppercase;
}

.day-num {
  font-size: 10px;
  font-weight: 800;
  color: var(--text-main);
}

.room-label-cell {
  text-align: left;
  font-weight: 700;
  color: var(--text-main);
  padding-left: 6px;
  white-space: nowrap;
}

.day-slot-cell {
  height: 28px;
  position: relative;
  background: var(--card-bg);
}

.reservation-bar {
  border-radius: 3px;
  font-size: 8px;
  font-weight: 700;
  color: #fff;
  padding: 3px 4px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
}

.reservation-bar.booked {
  background: #3b82f6;
}

.reservation-bar.pending {
  background: #f59e0b;
}

.slot-available {
  color: #10b981;
  font-size: 12px;
}

.calendar-legend-bar {
  display: flex;
  justify-content: center;
  gap: 12px;
  font-size: 8.5px;
  color: var(--text-muted);
  margin-top: auto;
  border-top: 1px solid var(--border-light);
  padding-top: 6px;
}

.legend-item {
  display: flex;
  align-items: center;
  gap: 4px;
}

.legend-dot {
  font-size: 9px;
}

.dot-booked { color: #3b82f6; }
.dot-pending { color: #f59e0b; }
.dot-available { color: #10b981; }
</style>
