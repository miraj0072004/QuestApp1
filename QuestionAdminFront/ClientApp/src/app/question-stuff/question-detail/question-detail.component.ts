import { Component, OnInit } from '@angular/core';
import { Question } from 'src/app/_models/question';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-question-detail',
  templateUrl: './question-detail.component.html',
  styleUrls: ['./question-detail.component.css']
})
export class QuestionDetailComponent implements OnInit {

  question: Question;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {

    this.route.data.subscribe(data=>{
      this.question = data['question'];
    });
  }

}
