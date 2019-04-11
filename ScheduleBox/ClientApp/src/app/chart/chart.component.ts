import { Component, Input } from '@angular/core';
import { SchedulesResponse } from '../shared/SchedulesResponse';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent {
  @Input()
  response: SchedulesResponse;
}
