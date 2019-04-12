import { Injectable } from '@angular/core';
import { SchedulesResponse } from './SchedulesResponse';
import { HttpClient } from '@angular/common/http';
import { Subject, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  readonly response = new BehaviorSubject<SchedulesResponse>(null);
  error: string | null;
  attendees: number | null;
  private _isoDate: string | null;

  constructor(
    private http: HttpClient
  ) { }

  public get isoDate(): string {
    return this._isoDate;
  }

  public set isoDate(v: string) {
    this._isoDate = v;
    this.response.next(null);
    this.error = null;
    this.http
      .get<SchedulesResponse>(`${document.getElementsByTagName("base")[0].href}api/schedules/${v}`)
      .subscribe(
        response => this.response.next(response),
        error => this.error = error.error);
  }
}
