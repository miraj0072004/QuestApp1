import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Question } from '../_models/question';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

baseUrl = environment.apiUrl+'questions/';  

constructor(private http: HttpClient) { }

getQuestions()
{
  return this.http.get(this.baseUrl);
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

}
