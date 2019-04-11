import { Component, Input } from '@angular/core';
import { SchedulesResponse, Activity, Person } from '../shared/SchedulesResponse';

function* range(start: number, end: number) {
  for (let i = start; i <= end; i++) {
    yield i;
  }
}

export class Range<T> {
  constructor(
    public startColumn: number,
    public endColumn: number,
    public value: T) {
  }
}

export class Bounds {
  // first column is for labels.
  readonly startColumn = 2;
  readonly endColumn: number;
  constructor(
    public readonly start: Date,
    public readonly end: Date) {
    this.endColumn = this.getColumnFromHours(end.getHours()) + Math.floor((end.getMinutes() - start.getMinutes()) / 15);
  }

  getStartColumn(time: Date): number {
    return this.clamp(
      this.getColumnFromHours(time.getHours()) + Math.floor(time.getMinutes() / 15),
      this.startColumn,
      this.endColumn);
  }

  getEndColumn(time: Date): number {
    return this.clamp(
      this.getColumnFromHours(time.getHours()) + Math.ceil(time.getMinutes() / 15),
      this.startColumn,
      this.endColumn);
  }

  getColumnFromHours(hours: number): number {
    return ((hours - this.start.getHours()) * 4) + this.startColumn;
  }

  private clamp(value: number, min: number, max: number): number {
    return Math.max(min, Math.min(max, value));
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
  slots: Range<Person[]>[] = [];
  bounds: Bounds;
  private _response: SchedulesResponse;

  public get response(): SchedulesResponse {
    return this._response;
  }

  @Input()
  public set response(v: SchedulesResponse) {
    this._response = v;
    if (v == null) {
      this.bounds = null;
      this.headers = [];
      this.schedules = [];
      this.slots = [];
      return;
    }
    this.bounds = new Bounds(new Date(v.start), new Date(v.end));

    this.headers = Array.from(
      range(this.bounds.start.getHours(), this.bounds.end.getHours()),
      x => {
        const start = this.bounds.getColumnFromHours(x);
        return new Range<string>(start, start + 3, `${x}:00`);
      }
    );

    this.schedules = Array.from(
      v.schedules,
      x => new DaySchedule(
        x.person,
        Array.from(
          x.activities,
          a => new Range<Activity>(
            this.bounds.getStartColumn(new Date(a.start)),
            this.bounds.getEndColumn(new Date(a.end)),
            a))));

    this.slots = Array.from(
      range(this.bounds.startColumn, this.bounds.endColumn),
      x => new Range<Person[]>(
        x,
        x,
        this.schedules.filter(s => s.activities.some(a => a.startColumn <= x && a.endColumn >= x))
          .map(s => s.person)));
  }
}
