import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map, distinctUntilChanged, filter } from 'rxjs/operators';
import { ScheduleService } from '../schedule/schedule.service';
import { combineLatest } from 'rxjs';
import { Day } from '../schedule/Day';

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

  ngOnInit(): void {
    this.dateParameterSubcription = this.route.paramMap
      .pipe(
        map(x => x.get('date')),
        distinctUntilChanged())
      .subscribe(date => this.scheduleService.date = Day.parse(date));

    this.attendeesQueryParameterSubcription = this.route.queryParamMap
      .pipe(
        map(x => x.get('attendees')),
        distinctUntilChanged())
      .subscribe(x => this.scheduleService.minAttendees = x ? +x : null);

    this.navigateSubscription = combineLatest(
      this.scheduleService.dateObservable.pipe(
        map(x => x === null ? null : x.toString()),
        filter(x => x !== null)),
      this.scheduleService.minAttendeesObservable)
      .subscribe(x => this.router.navigate([`/book-standup/${x[0]}`], { queryParams: { attendees: x[1] } }));
  }

  ngOnDestroy() {
    this.dateParameterSubcription.unsubscribe();
    this.attendeesQueryParameterSubcription.unsubscribe();
    this.navigateSubscription.unsubscribe();
  }
}
