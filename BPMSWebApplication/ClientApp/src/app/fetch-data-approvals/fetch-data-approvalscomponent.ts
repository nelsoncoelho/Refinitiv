import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'fetch-data-approvals-data',
  templateUrl: './fetch-data-approvals.component.html'
})
export class FetchDataApprovalsComponent {
  public tasks: BPMSData[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<BPMSData[]>(baseUrl + 'api/SampleData/GetBPMSApprovals').subscribe(result => {
      this.tasks = result;
    }, error => console.error(error));
  }
}

interface BPMSData {
  approver: string;
  approvalTime: Date;
  rejectionReason: string;
  dueDate: Date;
  approved: number;
  id: string;
}
