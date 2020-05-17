import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Question } from '../_models/question';
import { Subject, Observable } from 'rxjs';
import { AlertifyService } from './alertify.service';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

questionsChanged = new Subject<Question[]>();
baseUrl = environment.apiUrl+'questions/';
questionsAfterChange: Question[];

constructor(private http: HttpClient, private alertify: AlertifyService) { }

getQuestions(page?, itemsPerPage?):Observable<PaginatedResult<Question[]>>
{
  const paginatedResult: PaginatedResult<Question[]> = new PaginatedResult<Question[]>();
  let params = new HttpParams();

  if(page != null && itemsPerPage != null)
  {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }
  return this.http.get<Question[]>(this.baseUrl, {observe: 'response', params}).
  pipe(
    map(
      response =>
      {
        paginatedResult.result = response.body;
          if(response.headers.get('Pagination') != null)
          {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return paginatedResult;
      }
    )
  )
}

getQuestion(questionId: number)
{
  return this.http.get(this.baseUrl+questionId);
}

saveQuestion(question: Question)
{

  if (question.id == -1)
  {
    return this.http.post(this.baseUrl, question);
  }
  else
  {
    return this.http.put(this.baseUrl+question.id, question);
  }
}

deleteQuestion(questionId: number)
{
  return this.http.delete(this.baseUrl+questionId);  
  
}

changeHandle()
{
  this.getQuestions().subscribe(
    next =>
    {
      this.questionsAfterChange = <Question[]><unknown>next.result;
      this.questionsChanged.next(this.questionsAfterChange.slice());
    }
  );
  
  //this.alertify.success('Question deleted successfully!');
}

}
