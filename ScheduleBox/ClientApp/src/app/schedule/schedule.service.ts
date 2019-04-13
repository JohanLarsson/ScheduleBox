import { Injectable } from '@angular/core';
import { SchedulesResponse } from './SchedulesResponse';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  error: string | null;
  private readonly _response = new BehaviorSubject<SchedulesResponse>(null);
  private _date: Date;
  private _isoDate = new BehaviorSubject<string>('');
  private _attendees = new BehaviorSubject<number | null>(null);

  constructor(http: HttpClient
  ) { 
    this._isoDate.pipe(
      distinctUntilChanged())
      .subscribe(isoDate =>{
        this._response.next(null);
        this.error = null;
        http
          .get<SchedulesResponse>(`${document.getElementsByTagName("base")[0].href}api/schedules/${isoDate}`)
          .subscribe(
            response => this._response.next(response),
            error => this.error = error.error);
      })
  }

  public get date(): Date {
    return this._date;
  }

  public set date(v: Date) {
    this._date = v;
    this._isoDate.next(v.toISOString().substring(0, 10))
  }

  public get isoDate(): Observable<string> {
    return this._isoDate.pipe(distinctUntilChanged());
  }

  public get attendees() : number | null {
    return this._attendees.value;
  }
  
  public get attendeesObservable() : Observable<number | null> {
    return this._attendees.pipe(distinctUntilChanged());
  }

  public set attendees(v : number | null) {
    this._attendees.next(v);
  }

  public get response(): Observable<SchedulesResponse> {
    return this._response.asObservable();
  }
}
