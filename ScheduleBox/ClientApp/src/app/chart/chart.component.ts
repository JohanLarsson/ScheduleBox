import { Component, Input } from '@angular/core';
import { SchedulesResponse, Activity, Person } from '../shared/SchedulesResponse';

function* range(start: number, end: number) {
  for (let i = start; i <= end; i++) {
    yield i;
  }
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
        const start = (x - this.startHour) * 4 + 2;
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
  }
}
