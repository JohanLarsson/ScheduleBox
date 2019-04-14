import { DateAdapter, NativeDateAdapter, MAT_DATE_LOCALE } from '@angular/material';
import { Day as Day } from '../schedule/Day';
import { Injectable, Inject, Optional } from '@angular/core';
@Injectable()
export class DayAdapter extends DateAdapter<Day> {
  constructor(
    @Optional() @Inject(MAT_DATE_LOCALE) matDateLocale: string,
    private native: NativeDateAdapter) {
    super();
    super.setLocale(matDateLocale);
  }

  getYear(date: Day): number {
    return date.year;
  }

  getMonth(date: Day): number {
    return date.month;
  }

  getDate(date: Day): number {
    return date.date;
  }

  getDayOfWeek(date: Day): number {
    return this.native.getDayOfWeek(date.asDate());
  }

  getMonthNames(style: 'long' | 'short' | 'narrow'): string[] {
    return this.native.getMonthNames(style);
  }

  getDateNames(): string[] {
    return this.native.getDateNames();
  }

  getDayOfWeekNames(style: 'long' | 'short' | 'narrow'): string[] {
    return this.native.getDayOfWeekNames(style);
  }

  getYearName(date: Day): string {
    return date.year.toString();
  }

  getFirstDayOfWeek(): number {
    return this.native.getFirstDayOfWeek();
  }

  getNumDaysInMonth(date: Day): number {
    return this.native.getNumDaysInMonth(date.asDate());
  }

  clone(date: Day): Day {
    return new Day(date.year, date.month, date.date);
  }

  createDate(year: number, month: number, date: number): Day {
    return new Day(year, month, date);
  }

  today(): Day {
    return Day.ofDate(new Date());
  }

  parse(value: any, parseFormat: string): Day | null {
    if (value && typeof value === 'string') {
      return Day.parse(value);
    }
    return Day.ofDate(this.native.parse(value));
  }

  format(date: Day, displayFormat: string): string {
    return date.toString();
  }

  addCalendarYears(date: Day, years: number): Day {
    return new Day(date.year + years, date.month, date.date);
  }

  addCalendarMonths(date: Day, months: number): Day {
    return Day.ofDate(this.native.addCalendarMonths(date.asDate(), months));
  }

  addCalendarDays(date: Day, days: number): Day {
    return Day.ofDate(this.native.addCalendarDays(date.asDate(), days));
  }

  toIso8601(date: Day): string {
    return date.toString();
  }

  isDateInstance(obj: any): boolean {
    return obj instanceof Day;
  }

  isValid(date: Day): boolean {
    return !isNaN(date.year) &&
      !isNaN(date.month) &&
      !isNaN(date.date);
  }

  invalid(): Day {
    return new Day(NaN, NaN, NaN);
  }
}
