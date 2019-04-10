import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit, OnDestroy {
  json = 'Empty';
  date = new Date();
  private dateSub: any;
  private jsonSub: any;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
  ) { }

  ngOnInit(): void {
    let dateString;
    this.dateSub = this.route.paramMap.subscribe(x => {
          this.date = new Date(x.get('date'));
          dateString = x.get('date');
         }
       );
    this.jsonSub = this.http.get<string>(document.getElementsByTagName('base')[0].href + 'api/schedules/' + dateString)
                        .subscribe(x => this.json = JSON.stringify(x));
  }

  ngOnDestroy() {
    this.dateSub.unsubscribe();
    this.jsonSub.unsubscribe();
  }
}
