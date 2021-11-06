import { Component, OnInit } from '@angular/core';
import { ManagerFeedbackService } from '../manager-feedback.service';

@Component({
  selector: 'app-manager-feedback-view',
  templateUrl: './manager-feedback-view.component.html',
  styleUrls: ['./manager-feedback-view.component.css']
})
export class ManagerFeedbackViewComponent implements OnInit {

  public feedbacks = [] as any
  public errorMsg = ""

  constructor(private managerService : ManagerFeedbackService) { }

  ngOnInit(): void {
    this.managerService.getFeedback().subscribe(data => this.feedbacks = data,
                                                error => this.errorMsg = "Couldn't load user feedback");
  }

  publishFeedback(i) {
    console.log(i);
    let feedback = this.feedbacks[i];
    this.managerService.publishFeedback(feedback).subscribe();
  }

}