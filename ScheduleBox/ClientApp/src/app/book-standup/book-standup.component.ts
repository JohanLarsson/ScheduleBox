import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { StandupOpportunities } from "../shared/StandupOpportunitiesResponse";
import { map, distinctUntilChanged } from "rxjs/operators";

@Component({
  templateUrl: "./book-standup.component.html",
  styleUrls: ["./book-standup.component.scss"]
})
export class BookStandupComponent implements OnInit, OnDestroy {
  response: StandupOpportunities | null;
  error: string | null;
  private _attendees: number | null;
  private _isoDate: string | null;
  private sub: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient
  ) {}

  public get date(): string {
    return this._isoDate;
  }

  public set date(v: string) {
    const isoDate = this.getIsoDate(v);
    this._isoDate = isoDate;
    this.navigate();
  }

  public get attendees(): number | null {
    return this._attendees;
  }

  public set attendees(v: number | null) {
    this._attendees = v;
    this.navigate();
  }

  ngOnInit(): void {
    this.sub = this.route.paramMap
      .pipe(
        map(paramMap => this.getIsoDate(paramMap.get("date"))),
        distinctUntilChanged()
      )
      .subscribe(isoDate => {
        this._isoDate = isoDate;
        this.response = null;
        this.error = null;
        this.http
          .get<StandupOpportunities>(`${document.getElementsByTagName("base")[0].href}api/schedules/${isoDate}`)
          .subscribe(
            response => this.response = response,
            error => this.error = error.error);
      });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  getIsoDate(text: string): string {
    return new Date(text).toISOString().substring(0, 10);
  }

  private navigate() {
    this.router.navigate([`/book-standup/${this._isoDate}`], {
      queryParams: { attendees: this.attendees }
    });
  }
}
