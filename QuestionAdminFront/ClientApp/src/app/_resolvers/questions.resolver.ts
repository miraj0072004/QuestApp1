import { Resolve, ActivatedRouteSnapshot, Router } from "@angular/router";
import { Question } from "../_models/question";
import { Observable, of } from "rxjs";
import { Injectable } from '@angular/core';
import { QuestionService } from "../_services/question.service";
import { AlertifyService } from "../_services/alertify.service";
import { catchError } from "rxjs/operators";

@Injectable()
export class QuestionsResolver implements Resolve<Question[]>
{
    /**
     *
     */
    constructor(private questionService: QuestionService, private router: Router, private alertify: AlertifyService) {

    }

    resolve(route: ActivatedRouteSnapshot): Observable<Question[]> {
        return this.questionService.getQuestions().pipe(
            catchError(error => {
                    this.alertify.error("Problem retrieving data");
                    this.router.navigate(['/']);
                    return of(null);
                })
        );
    }
}