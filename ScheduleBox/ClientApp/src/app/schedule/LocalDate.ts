export class LocalDate {
  constructor(
    public readonly year: number,
    public readonly month: number,
    public readonly date: number) { }

  public static format(date: Date): string {
    return `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}`;
  }

  public static parse(s: string): LocalDate {
    const regex = /^(\d\d\d\d)-(\d\d)-(\d\d)$/;
    if (regex.test(s)) {
      const matches = regex.exec(s);
      return new LocalDate(+matches[1], +matches[2] - 1, +matches[3]);
    }

    return null;
  }

  public static ofDate(date: Date): LocalDate {
    return new LocalDate(date.getFullYear(), date.getMonth(), date.getDate());
  }

  public asDate(): Date {
    return new Date(this.year, this.month, this.date);
  }

  public toString(): string {
    return `${this.year}-${(this.month + 1).toString().padStart(2, '0')}-${(this.date).toString().padStart(2, '0')}`;
  }
}