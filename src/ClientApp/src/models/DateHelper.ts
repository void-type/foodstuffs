import { format, formatISO } from 'date-fns';
import { isNil } from '@/models/FormatHelper';

export default class DateHelper {
  static dateForApi(value: Date) {
    return formatISO(value as Date, { representation: 'date' });
  }

  static dateTimeForApi(value: Date) {
    return formatISO(value);
  }

  static dateForView(value: Date) {
    return format(value as Date, 'P');
  }

  static dateTimeForView(value: Date | string | null | undefined): string {
    if (typeof value === 'string') {
      if (isNil(value)) {
        return '';
      }

      return this.dateTimeForView(new Date(value));
    }

    return format(value as Date, 'P p');
  }

  static getThisOrNextDayOfWeek(day: number) {
    const d = new Date();

    while (d.getDay() !== day) {
      d.setDate(d.getDate() + 1);
    }

    return d;
  }
}
