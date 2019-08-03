import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public tasks: BPMSData[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<BPMSData[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.tasks = result;
    }, error => console.error(error));
  }
}

interface BPMSData {
  assignee: string;
  createTime: Date;
  description: string;
  dueDate: Date;
  id: number;
  name: string;
}
