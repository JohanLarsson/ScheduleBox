import { Component, Input } from "@angular/core";
import { SchedulesResponse } from "../shared/SchedulesResponse";

export class Range<T> {
  constructor(
    public start: number,
    public end: number,
    public value: T) {}
}

@Component({
  selector: "app-chart",
  templateUrl: "./chart.component.html",
  styleUrls: ["./chart.component.scss"]
})
export class ChartComponent {
  headers: Range<string>[] = [];
  private _response: SchedulesResponse;

  public get response(): SchedulesResponse {
    return this._response;
  }

  @Input()
  public set response(v: SchedulesResponse) {
    this._response = v;
    this.headers = [
      this.createHeader(8),
      this.createHeader(9),
      this.createHeader(10),
      this.createHeader(11),
      this.createHeader(12),
      this.createHeader(13),
      this.createHeader(14),
      this.createHeader(15),
      this.createHeader(16),
      this.createHeader(17),
    ];
  }

  private createHeader(hour: number): Range<string> {
    const start = ((hour - 8) * 4) + 2;
    return new Range<string>(start, start + 3, `${hour}:00`);
  }
}
