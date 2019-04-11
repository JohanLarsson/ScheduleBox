export interface SchedulesResponse {
  date: string;
  schedules: Schedule[];
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
