import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit, OnDestroy {
  response: ScheduleResponse;
  private _date: string;
  private sub: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
  ) { }


  public get date() : string {
    return this._date;
  }

  public set date(v : string) {
    const isoDate = this.getIsoDate(v);
    this._date = isoDate;
    this.router.navigate([`/${isoDate}`]);
  }

  ngOnInit(): void {
    this.sub = this.route.paramMap.subscribe(x => {
      const isoDate = this.getIsoDate(x.get('date'));
      this._date = isoDate;
      this.resonse = null;
      this.http.get<ScheduleResponse>(`${document.getElementsByTagName('base')[0].href}api/schedules/${isoDate}`)
               .subscribe(x => this.response = x);
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  getIsoDate(text: string): string {
    return new Date(text).toISOString().substring(0, 10);
  }
}

export interface ScheduleResponse {
  timeSlots: TimeSlot[];
  schedules: Schedule[];
}

export interface TimeSlot {
  start: string;
  end: string;
  attendees: Person[];
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
