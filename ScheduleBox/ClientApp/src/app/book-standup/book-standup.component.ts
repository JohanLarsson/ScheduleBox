import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, distinctUntilChanged } from 'rxjs/operators';
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
    private route: ActivatedRoute,
    private router: Router,
    public scheduleService: ScheduleService
  ) { }

  ngOnInit(): void {
    this.dateParameterSubcription = this.route.paramMap
      .pipe(
        map(paramMap => new Date(paramMap.get('date'))),
        distinctUntilChanged())
      .subscribe(date => this.scheduleService.date = date);

    this.attendeesQueryParameterSubcription = this.route.queryParamMap
      .pipe(
        map(paramMap => paramMap.get('attendees')),
        distinctUntilChanged())
      .subscribe(x => this.scheduleService.attendees = x ? +x : null);

    this.navigateSubscription = combineLatest(
      this.scheduleService.isoDate,
      this.scheduleService.attendeesObservable)
      .subscribe(x => this.router.navigate([`/book-standup/${x[0]}`], { queryParams: { attendees: x[1] } }));
  }

  ngOnDestroy() {
    this.dateParameterSubcription.unsubscribe();
    this.attendeesQueryParameterSubcription.unsubscribe();
    this.navigateSubscription.unsubscribe();
  }
}
