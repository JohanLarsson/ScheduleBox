import { Injectable } from '@angular/core';
import { SchedulesResponse } from './SchedulesResponse';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { distinctUntilChanged, map } from 'rxjs/operators';
import { Day } from './Day';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  error: string | null;
  private readonly _response = new BehaviorSubject<SchedulesResponse>(null);
  private readonly _date = new BehaviorSubject<Day | null>(null);
  private readonly _attendees = new BehaviorSubject<number | null>(null);

  constructor(http: HttpClient
  ) {
    this._date.pipe(
      map(x => x === null ? null : x.toString()),
      distinctUntilChanged())
      .subscribe(date => {
        this._response.next(null);
        if (date === null) {
          this.error = 'Invalid date.';
        } else {
          this.error = null;
          http
            .get<SchedulesResponse>(`${document.getElementsByTagName('base')[0].href}api/schedules/${date}`)
            .subscribe(
              response => this._response.next(response),
              error => this.error = error.error);
        }
      });
  }

  public get date(): Day | null {
    return this._date.value;
  }

  public set date(v: Day) {
    this._date.next(v);
  }

  public get dateObservable(): Observable<Day | null> {
    return this._date.pipe(distinctUntilChanged());
  }

  public get attendees(): number | null {
    return this._attendees.value;
  }

  public set attendees(v: number | null) {
    this._attendees.next(v);
  }

  public get attendeesObservable(): Observable<number | null> {
    return this._attendees.pipe(distinctUntilChanged());
  }

  public get response(): Observable<SchedulesResponse> {
    return this._response.asObservable();
  }
}
