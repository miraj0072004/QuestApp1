import { Component, OnInit } from '@angular/core';
import { QuestionService } from '../../_services/question.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Question } from '../../_models/question';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  questions: Question[];

  constructor(private questionService: QuestionService, private alertifyService: AlertifyService,private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.questions = data['questions'];
    });
  }

}
