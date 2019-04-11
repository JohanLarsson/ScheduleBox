import { Component, Input } from '@angular/core';
import { SchedulesResponse, Activity, Person } from '../shared/SchedulesResponse';

function* range(start: number, end: number) {
  for (let i = start; i <= end; i++) {
    yield i;
  }
}

function clamp(value: number, min: number, max: number): number {
  return Math.max(min, Math.min(max, value));
}

export class Range<T> {
  constructor(
    public start: number,
    public end: number,
    public value: T) {
  }
}

export class DaySchedule {
  constructor(
    public person: Person,
    public activities: Range<Activity>[]) { }
}

@Component({
  selector: "app-chart",
  templateUrl: "./chart.component.html",
  styleUrls: ["./chart.component.scss"]
})
export class ChartComponent {
  headers: Range<string>[];
  schedules: DaySchedule[] = [];
  private _response: SchedulesResponse;
  private startHour = 8;
  private endHour = 17;

  constructor() {
    this.headers = Array.from(
      range(this.startHour, this.endHour),
      x => {
        // One cell per fifteen minutes and first column for labels.
        const time = new Date();
        time.setHours(x);
        const start = this.getCellIndex(time);
        return new Range<string>(start, start + 3, `${x}:00`);
      }
    );
  }

  public get response(): SchedulesResponse {
    return this._response;
  }

  @Input()
  public set response(v: SchedulesResponse) {
    this._response = v;
    const endColumn = this.getCellIndex(new Date(0, 0, 0, this.endHour));
    this.schedules = Array.from(
      v.schedules,
      x => new DaySchedule(
        x.person,
        Array.from(
          x.activities.filter(a => new Date(a.start).getHours() <= this.endHour &&
                                   new Date(a.end).getHours() >= this.startHour),
          a => new Range<Activity>(
            clamp(this.getCellIndex(new Date(a.start)), 2, endColumn),
            clamp(this.getCellIndex(new Date(a.end)), 2, endColumn),
            a))));
  }

  private getCellIndex(time: Date): number {
    return ((time.getHours() - this.startHour) * 4) + 2 + Math.ceil(time.getMinutes() / 15);
  }
}
