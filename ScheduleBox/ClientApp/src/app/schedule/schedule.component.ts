import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit, OnDestroy {
  json = 'Empty';
  private _date: string;
  private dateSub: any;

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
    this.json = '';
    this.http.get<string>(`${document.getElementsByTagName('base')[0].href}api/schedules/${isoDate}`)
             .subscribe(x => this.json = JSON.stringify(x));
  }

  ngOnInit(): void {
    this.dateSub = this.route.paramMap.subscribe(x => this.date = this.getIsoDate(x.get('date')));
  }

  ngOnDestroy() {
    this.dateSub.unsubscribe();
  }

  getIsoDate(text: string): string {
    return new Date(text).toISOString().substring(0, 10);
  }
}
