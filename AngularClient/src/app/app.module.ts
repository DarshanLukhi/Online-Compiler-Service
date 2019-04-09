import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule} from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {NgxPaginationModule} from 'ngx-pagination';


import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HistoryComponent } from './history/history.component';
import { IdeComponent } from './ide/ide.component';
import { FilterPipe } from './filter.pipe';
import { ViewComponent } from './view/view.component';
import { UpdateComponent } from './update/update.component';



const routes: Routes = [
   { path: 'history', component: HistoryComponent},
   { path: 'view/:CodeID', component: ViewComponent },
   { path: 'edit/:CodeID', component: UpdateComponent },
   { path: '**', component: IdeComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HistoryComponent,
    IdeComponent,
    FilterPipe,
    ViewComponent,
    UpdateComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgxPaginationModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
