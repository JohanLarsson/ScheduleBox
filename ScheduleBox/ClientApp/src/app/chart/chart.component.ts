import { Component, Input } from '@angular/core';
import { StandupOpportunities } from '../shared/StandupOpportunitiesResponse';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent {
  @Input()
  response: StandupOpportunities;
}
