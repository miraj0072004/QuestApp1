import { Component, OnInit } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { QuestionService } from 'src/app/_services/question.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-question-edit',
  templateUrl: './question-edit.component.html',
  styleUrls: ['./question-edit.component.css']
})
export class QuestionEditComponent implements OnInit {

  question: Question;
  savedQuestion: Question;

  checkOne:boolean = false;
  checkTwo:boolean = false;
  checkThree:boolean = false;
  constructor(private questionService: QuestionService, private alertify: AlertifyService,
              private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {

    this.question = {
      id: -1,
      questionCategory : '',
      questionText : '',
      answerOne : '',
      answerTwo : '',
      answerThree : '',
      answerExplanation : '',
      correctAnswerIndex : 2
    }  ;

    const id = this.route.snapshot.params['id'];
    if (id != null) {
      this.route.data.subscribe(data=>{
        this.question = data['question'];

        switch (this.question.correctAnswerIndex) {
          case 1:
            this.checkOne = true;
            break;
          case 2:
            this.checkTwo = true;
            break;
          case 3:
            this.checkThree = true;
            break;
          default:
            break;
        }
      });
    }
    
  }

  onCheck(e: any)
  {
    //console.log('The check box you clicked is '+ e.currentTarget.name + ' and is in state ' + e.currentTarget.checked);
    if (e.currentTarget.name == 'answerOneCheck') {
      if (e.currentTarget.checked == true) {
        this.question.correctAnswerIndex = 1;
        this.checkOne = true;
        this.checkTwo = this.checkThree = false;
      }
    }

    if (e.currentTarget.name == 'answerTwoCheck') {
      if (e.currentTarget.checked == true) {
        this.question.correctAnswerIndex = 2;
        this.checkTwo = true;
        this.checkOne = this.checkThree = false;
      }
    }

    if (e.currentTarget.name == 'answerThreeCheck') {
      if (e.currentTarget.checked == true) {
        this.question.correctAnswerIndex = 3;
        this.checkThree = true;
        this.checkTwo = this.checkOne = false;
      }
    }
  }

  updateQuestion()
  {
    this.questionService.saveQuestion(this.question).subscribe(
      next =>
      {
        this.savedQuestion = <Question>next;
        this.alertify.success('question updated successfully');
        this.router.navigate(['/questions', this.savedQuestion.id]);
      },
      error =>
      {
        this.alertify.error(error);
        
      });
  }
}
