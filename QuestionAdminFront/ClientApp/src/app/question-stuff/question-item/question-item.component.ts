import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { QuestionService } from 'src/app/_services/question.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-question-item',
  templateUrl: './question-item.component.html',
  styleUrls: ['./question-item.component.css']
})
export class QuestionItemComponent implements OnInit {

  @Input() question: Question;
  modalRef: BsModalRef;
  message: string;

  constructor(private modalService: BsModalService,
              private questionService: QuestionService,
              private alertify: AlertifyService,
              private router: Router) { }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  ngOnInit() {
  }

  sendDelete(questionId: any)
  {
    console.log("Deletion Successful!");
    this.modalRef.hide();
    this.questionService.deleteQuestion(questionId).subscribe(
      next =>
      {
        
        this.alertify.success('question deleted successfully');
        this.router.navigate(['/questions']);
      },
      error =>
      {
        this.alertify.error(error);
        
      });
  }

  sendCancel(event: any)
  {
    console.log("Deletion Canceled!");
    this.modalRef.hide();
  }

}
