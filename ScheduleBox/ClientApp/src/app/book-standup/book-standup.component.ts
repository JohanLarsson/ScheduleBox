import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { SchedulesResponse } from "../schedule/SchedulesResponse";
import { map, distinctUntilChanged } from "rxjs/operators";
import { ScheduleService } from '../schedule/schedule.service';

@Component({
  templateUrl: "./book-standup.component.html",
  styleUrls: ["./book-standup.component.scss"]
})
export class BookStandupComponent implements OnInit, OnDestroy {
  private dateSubcription: any;
  private attendeesSubcription: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public scheduleService: ScheduleService
  ) { }

  public get date(): string {
    return this.scheduleService.isoDate;
  }

  public set date(v: string) {
    this.scheduleService.isoDate = this.getIsoDate(v);
    this.navigate();
  }

  public get attendees(): number | null {
    return this.scheduleService.attendees;
  }

  public set attendees(v: number | null) {
    this.scheduleService.attendees = v;
    this.navigate();
  }

  ngOnInit(): void {
    this.dateSubcription = this.route.paramMap
      .pipe(
        map(paramMap => this.getIsoDate(paramMap.get("date"))),
        distinctUntilChanged())
      .subscribe(isoDate => {
        this.scheduleService.isoDate = isoDate;
      });

    this.attendeesSubcription = this.route.queryParamMap
      .pipe(
        map(paramMap => paramMap.get("attendees")),
        distinctUntilChanged())
      .subscribe(x => this.scheduleService.attendees = x ? +x : null);
  }

  ngOnDestroy() {
    this.dateSubcription.unsubscribe();
    this.attendeesSubcription.unsubscribe();
  }

  getIsoDate(text: string): string {
    return new Date(text).toISOString().substring(0, 10);
  }

  private navigate() {
    this.router.navigate([`/book-standup/${this.scheduleService.isoDate}`], {
      queryParams: { attendees: this.attendees }
    });
  }
}
