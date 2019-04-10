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
  private _date = new Date();
  private dateSub: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
  ) { }

  
  public get date() : Date {
    return this._date;
  }
    
  public set date(v : Date) {
    // if passed as string from the input
    v = new Date(v);
    this._date = v;
    const isoDate = v.toISOString().substring(0, 10);
    this.router.navigate([`/${isoDate}`]);
    this.json = '';
    this.http.get<string>(`${document.getElementsByTagName('base')[0].href}api/schedules/${isoDate}`)
             .subscribe(x => this.json = JSON.stringify(x));
  }
  
  ngOnInit(): void {
    this.dateSub = this.route.paramMap.subscribe(x => this.date = new Date(x.get('date')));
  }

  ngOnDestroy() {
    this.dateSub.unsubscribe();
  }
}
