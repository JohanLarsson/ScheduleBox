import { Component, Input } from '@angular/core';
import { SchedulesResponse } from '../shared/SchedulesResponse';

export class Range<T> {
  constructor(
    public start: number,
    public end: number,
    public value: T) {}
}

function* range(start: number, end: number) {
  for (let i = start; i <= end; i++) {
    yield i;
  }
}

@Component({
  selector: "app-chart",
  templateUrl: "./chart.component.html",
  styleUrls: ["./chart.component.scss"]
})
export class ChartComponent {
  headers: Range<string>[] = [];
  private _response: SchedulesResponse;
  private startHour = 8;
  private endHour = 17;

  public get response(): SchedulesResponse {
    return this._response;
  }

  @Input()
  public set response(v: SchedulesResponse) {
    this._response = v;
    this.headers = Array.from(range(this.startHour, this.endHour), x => this.createHeader(x));
  }

  private createHeader(hour: number): Range<string> {
    // One cell per fifteen minutes and first column for labels.
    const start = ((hour - this.startHour) * 4) + 2;
    return new Range<string>(start, start + 3, `${hour}:00`);
  }
}
