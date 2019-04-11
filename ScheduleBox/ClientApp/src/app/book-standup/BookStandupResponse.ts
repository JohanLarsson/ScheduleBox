export interface BookStandupResponse {
  timeSlots: TimeSlot[];
  schedules: Schedule[];
}

export interface TimeSlot {
  start: string;
  end: string;
  attendees: Person[];
}

export interface Person {
  name: string;
  id: string;
}

export interface Activity {
  description: string;
  color: string;
  start: string;
  end: string;
}

export interface Schedule {
  person: Person;
  activities: Activity[];
}
