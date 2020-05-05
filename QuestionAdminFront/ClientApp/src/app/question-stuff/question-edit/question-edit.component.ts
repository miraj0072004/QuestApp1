import { Component, OnInit } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { QuestionService } from 'src/app/_services/question.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-question-edit',
  templateUrl: './question-edit.component.html',
  styleUrls: ['./question-edit.component.css']
})
export class QuestionEditComponent implements OnInit {

  question: Question = {
    id: -1,
    questionCategory : '',
    questionText : '',
    answerOne : '',
    answerTwo : '',
    answerThree : '',
    answerExplanation : '',
    correctAnswerIndex : 2
  }  ;

  checkOne:boolean = false;
  checkTwo:boolean = false;
  checkThree:boolean = false;
  constructor(private questionService: QuestionService, private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {

    if (this.route.params['id'] != '') {
      this.route.data.subscribe(data=>{
        this.question = data['question'];
      });
    }
    
  }

  onCheck(e: any)
  {
    //console.log('The check box you clicked is '+ e.currentTarget.name + ' and is in state ' + e.currentTarget.checked);
    if (e.currentTarget.name == 'answerOneCheck') {
      if (e.currentTarget.checked == true) {
        this.checkTwo = this.checkThree = false;
      }
    }

    if (e.currentTarget.name == 'answerTwoCheck') {
      if (e.currentTarget.checked == true) {
        this.checkOne = this.checkThree = false;
      }
    }

    if (e.currentTarget.name == 'answerThreeCheck') {
      if (e.currentTarget.checked == true) {
        this.checkTwo = this.checkOne = false;
      }
    }
  }

  updateQuestion()
  {
    this.questionService.saveQuestion(this.question).subscribe(
      next =>
      {
        this.alertify.success('question updated successfully');
      },
      error =>
      {
        this.alertify.error(error);
        
      });
  }
}
