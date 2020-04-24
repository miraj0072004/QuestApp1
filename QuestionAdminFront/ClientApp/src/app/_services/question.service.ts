import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

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


}
