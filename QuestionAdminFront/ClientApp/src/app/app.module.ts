import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';



import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { QuestionsComponent } from './question-stuff/questions/questions.component';
import { QuestionService } from './_services/question.service';
import { QuestionsResolver } from './_resolvers/questions.resolver';
import { QuestionItemComponent } from './question-stuff/question-item/question-item.component';
import { QuestionDetailComponent } from './question-stuff/question-detail/question-detail.component';
import { QuestionDetailResolver } from './_resolvers/question-detail.resolver';
import { QuestionEditComponent } from './question-stuff/question-edit/question-edit.component';
import { QuestionEditResolver } from './_resolvers/question-edit.resolver';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AuthGuard } from './_guards/auth.guard';


export function tokenGetter(){
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    QuestionsComponent,
    QuestionItemComponent,
    QuestionDetailComponent,
    QuestionEditComponent  
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent},

      {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children:
            [ { path: 'counter', component: CounterComponent },
              { path: 'fetch-data', component: FetchDataComponent },
              { path: 'questions/new', component: QuestionEditComponent },
              { path: 'questions/:id', component: QuestionDetailComponent, resolve : {question: QuestionDetailResolver} },
              { path: 'questions/edit/:id', component: QuestionEditComponent, resolve : {question: QuestionEditResolver} },
              { path: 'questions', component: QuestionsComponent, resolve : {questions: QuestionsResolver} }
            ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
    ]),
    JwtModule.forRoot(
      {
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth'],
            
         }
      }
   ),
    ModalModule.forRoot(),
    PaginationModule.forRoot()
  ],
  providers: [
    AuthService,
    AlertifyService,
    QuestionService,
    QuestionsResolver,
    QuestionDetailResolver,
    QuestionEditResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
