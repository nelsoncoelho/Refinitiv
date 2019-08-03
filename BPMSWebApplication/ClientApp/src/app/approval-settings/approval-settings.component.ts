import { Component, Inject } from '@angular/core';
import { Params, ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { request } from 'https';

@Component({
  selector: 'app-approval-settings',
  templateUrl: './approval-settings.component.html',
})
export class ApprovalSettingsComponent {
  public forecasts: BPMSData[];
  public id: string;
  public request: ApprovalQuestions = {
    approved: true,
    approver: null,
    rejectionReason: null,
    id: null
  };

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, public activatedRoute: ActivatedRoute, public router: Router) {
    activatedRoute.params.subscribe((params: Params) => {
      this.id = params['id'];
    });
  }

  display() {
    if (this.request.approver == null || (this.request.approved == false && this.request.rejectionReason == null)) {
      window.alert("Please fill all the mandatory fields.");
    }
    else {
      this.request.id = this.id;
      this.http.put<BPMSData[]>(this.baseUrl + 'api/SampleData/UpdateBPMSTask', this.request).subscribe(result => {
        this.forecasts = result;
        window.alert("Approval settings completed.");
        this.router.navigateByUrl("fetch-data");
      }, error => console.error(error));
    }
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

interface ApprovalQuestions {
  approved: boolean;
  rejectionReason: string;
  approver: string;
  id: string;
}
