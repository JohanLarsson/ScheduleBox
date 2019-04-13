import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, distinctUntilChanged, filter } from 'rxjs/operators';
import { ScheduleService } from '../schedule/schedule.service';
import { combineLatest } from 'rxjs';

@Component({
  templateUrl: './book-standup.component.html',
  styleUrls: ['./book-standup.component.scss']
})
export class BookStandupComponent implements OnInit, OnDestroy {
  private dateParameterSubcription: any;
  private attendeesQueryParameterSubcription: any;
  private navigateSubscription: any;

  constructor(
    public scheduleService: ScheduleService,
    private route: ActivatedRoute,
    private router: Router
  ) { }


  public get date(): Date {
    return this.scheduleService.date;
  }

  public set date(v: Date) {
    console.log(`datepicker: ${v}`);
    this.scheduleService.date = v;
  }

  ngOnInit(): void {
    this.dateParameterSubcription = this.route.paramMap
      .pipe(
        map(paramMap => new Date(paramMap.get('date'))),
        distinctUntilChanged())
      .subscribe(date => {
        console.log(`url: ${date}`);
        this.scheduleService.date = date;
      });

    this.attendeesQueryParameterSubcription = this.route.queryParamMap
      .pipe(
        map(paramMap => paramMap.get('attendees')),
        distinctUntilChanged())
      .subscribe(x => this.scheduleService.attendees = x ? +x : null);

    this.navigateSubscription = combineLatest(
      this.scheduleService.dateObservable.pipe(filter(x => x !== null)),
      this.scheduleService.attendeesObservable)
      .subscribe(x => this.router.navigate(
        [`/book-standup/${x[0].getFullYear()}-${x[0].getMonth() + 1}-${x[0].getDate()}`],
        { queryParams: { attendees: x[1] } }));
  }

  ngOnDestroy() {
    this.dateParameterSubcription.unsubscribe();
    this.attendeesQueryParameterSubcription.unsubscribe();
    this.navigateSubscription.unsubscribe();
  }
}
