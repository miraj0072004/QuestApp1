import { Component, OnInit } from '@angular/core';
import { QuestionService } from '../../_services/question.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Question } from '../../_models/question';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  questions: Question[];
  pagination: Pagination;
  searchTerm: any = null;

  constructor(private questionService: QuestionService, private alertifyService: AlertifyService,private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.questions = data['questions'].result;
      this.pagination = data['questions'].pagination;
    });

    this.questionService.questionsChanged.subscribe(
      (questionsAfterChange: PaginatedResult<Question[]>)=>
      {
        this.questions = questionsAfterChange.result;
        this.pagination = questionsAfterChange.pagination;
        //this.loadQuestions();
      }
    );
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadQuestions();
  }

  clearSearchForm()
  {
    this.searchTerm = null;
    this.loadQuestions();
  }


  loadQuestions()
  {
    this.questionService.getQuestions(this.pagination.currentPage, this.pagination.itemsPerPage, this.searchTerm)
    .subscribe((res: PaginatedResult<Question[]>)=>
    {
      this.questions = res.result;
      this.pagination = res.pagination;
    });
  }

}
