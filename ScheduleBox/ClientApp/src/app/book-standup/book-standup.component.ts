import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BookStandupResponse } from './BookStandupResponse';

@Component({
  templateUrl: './book-standup.component.html',
  styleUrls: ['./book-standup.component.scss']
})
export class BookStandupComponent implements OnInit, OnDestroy {
  response: BookStandupResponse;
  private _attendees: number | null;
  private _isoDate: string;
  private sub: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
  ){ }

  public get date(): string {
    return this._isoDate;
  }

  public set date(v : string) {
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
    this.sub = this.route.paramMap.subscribe(map => {
      const isoDate = this.getIsoDate(map.get('date'));
      this._isoDate = isoDate;
      this.response = null;
      this.http.get<BookStandupResponse>(`${document.getElementsByTagName('base')[0].href}api/schedules/${isoDate}`)
               .subscribe(x => this.response = x);
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  getIsoDate(text: string): string {
    return new Date(text).toISOString().substring(0, 10);
  }

  private navigate() {
    this.router.navigate([`/book-standup/${this._isoDate}`], { queryParams: { attendees: this.attendees } });
  }
}
