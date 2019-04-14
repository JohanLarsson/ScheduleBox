import { DateAdapter, NativeDateAdapter, MAT_DATE_LOCALE } from '@angular/material';
import { LocalDate } from '../schedule/LocalDate';
import { Injectable, Inject, Optional } from '@angular/core';

@Injectable()
export class LocalDateAdapter extends DateAdapter<LocalDate> {
  constructor(
    @Optional() @Inject(MAT_DATE_LOCALE) matDateLocale: string,
    private native: NativeDateAdapter) {
    super();
    super.setLocale(matDateLocale);
  }

  getYear(date: LocalDate): number {
    return date.year;
  }

  getMonth(date: LocalDate): number {
    return date.month;
  }

  getDate(date: LocalDate): number {
    return date.date;
  }

  getDayOfWeek(date: LocalDate): number {
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

  getYearName(date: LocalDate): string {
    return date.year.toString();
  }

  getFirstDayOfWeek(): number {
    return this.native.getFirstDayOfWeek();
  }

  getNumDaysInMonth(date: LocalDate): number {
    return this.native.getNumDaysInMonth(date.asDate());
  }

  clone(date: LocalDate): LocalDate {
    return new LocalDate(date.year, date.month, date.date);
  }

  createDate(year: number, month: number, date: number): LocalDate {
    return new LocalDate(year, month, date);
  }

  today(): LocalDate {
    return LocalDate.ofDate(new Date());
  }

  parse(value: any, parseFormat: string): LocalDate | null{
    if (value && typeof value === 'string') {
      return LocalDate.parse(value);
    }

    return LocalDate.ofDate(this.native.parse(value));
  }

  format(date: LocalDate, displayFormat: string): string {
    return date.toString();
  }

  addCalendarYears(date: LocalDate, years: number): LocalDate {
    return new LocalDate(date.year + years, date.month, date.date);
  }

  addCalendarMonths(date: LocalDate, months: number): LocalDate {
    return LocalDate.ofDate(this.native.addCalendarMonths(date.asDate(), months));
  }

  addCalendarDays(date: LocalDate, days: number): LocalDate {
    return LocalDate.ofDate(this.native.addCalendarDays(date.asDate(), days));
  }

  toIso8601(date: LocalDate): string {
    return date.toString();
  }

  isDateInstance(obj: any): boolean {
    return obj instanceof LocalDate;
  }

  isValid(date: LocalDate): boolean {
    return !isNaN(date.year) &&
           !isNaN(date.month) &&
           !isNaN(date.date);
  }

  invalid(): LocalDate {
    return new LocalDate(NaN, NaN, NaN);
  }
}
