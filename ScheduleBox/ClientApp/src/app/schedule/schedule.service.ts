import { Injectable } from '@angular/core';
import { SchedulesResponse, Person, Activity, Schedule } from './SchedulesResponse';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { distinctUntilChanged, map } from 'rxjs/operators';
import { Day } from './Day';
import { _MatTabHeaderMixinBase } from '@angular/material/tabs/typings/tab-header';

export class PersonAndActivity {
  constructor(
    public readonly person: Person,
    public readonly activities: Activity[]) { }
}

export class Slot {
  constructor(
    public readonly start: Date,
    public readonly end: Date,
    public readonly attendees: PersonAndActivity[],
    public readonly occupied: PersonAndActivity[]) { }

  public static create(start: Date, end: Date, schedules: Schedule[]) {
    const personactivities = Array.from(
      schedules,
      x => new PersonAndActivity(x.person, x.activities.filter(a => new Date(a.start) <= start && new Date(a.end) >= end)));
    return new Slot(
      start,
      end,
      personactivities.filter(x => x.activities.length > 0 && x.activities.every(a => isAvailable(a))),
      personactivities.filter(x => x.activities.length === 0 || x.activities.every(a => !isAvailable(a))));

    function isAvailable(activity: Activity): boolean {
      return activity.description !== 'Lunch' &&
        activity.description !== 'Short break';
    }
  }
}

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  error: string | null;
  slots: Slot[];
  selectedSlot: Slot | null;
  private readonly _response = new BehaviorSubject<SchedulesResponse>(null);
  private readonly _date = new BehaviorSubject<Day | null>(null);
  private readonly _minAttendees = new BehaviorSubject<number | null>(null);

  constructor(http: HttpClient) {
    this._date.pipe(
      map(x => x === null ? null : x.toString()),
      distinctUntilChanged())
      .subscribe(date => {
        this._response.next(null);
        this.slots = [];
        this.selectedSlot = null;
        if (date === null) {
          this.error = 'Invalid date.';
        } else {
          this.error = null;
          http
            .get<SchedulesResponse>(`${document.getElementsByTagName('base')[0].href}api/schedules/${date}`)
            .subscribe(
              response => {
                this.slots = Array.from(
                  this.slotTimes(response),
                  x => Slot.create(x[0], x[1], response.schedules));
                this.selectedSlot = this.findSlot(this.slots);
                this._response.next(response);
              },
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

  public get minAttendees(): number | null {
    return this._minAttendees.value;
  }

  public set minAttendees(v: number | null) {
    if (v !== null &&
      this.selectedSlot !== null &&
      this.selectedSlot.attendees.length < v) {
      this.selectedSlot = null;
    }

    if (this.selectedSlot === null &&
      this.slots !== null) {
      this.selectedSlot = this.findSlot(this.slots);
    }

    this._minAttendees.next(v);
  }

  public get minAttendeesObservable(): Observable<number | null> {
    return this._minAttendees.pipe(distinctUntilChanged());
  }

  public get response(): Observable<SchedulesResponse> {
    return this._response.asObservable();
  }

  public select(slot: Slot): void {
    this.selectedSlot = slot;
  }

  private *slotTimes(response: SchedulesResponse) {
    const end = new Date(response.end);
    let start = new Date(response.start);
    while (start < end) {
      const next = new Date(start.getTime() + 15 * 60 * 1000);
      yield [start, next];
      start = next;
    }
  }

  private findSlot(slots: Slot[]): Slot {
    let max = null;
    for (const slot of slots) {
      if (max == null) {
        max = slot;
      }
      else if (slot.attendees.length > max.attendees.length) {
        max = slot;
      }
    }

    if (this._minAttendees.value !== null &&
      max.attendees.length < this._minAttendees.value) {
      return null;
    }

    return max;
  }
}
