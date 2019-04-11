import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BookStandupComponent } from './book-standup/book-standup.component';

const routes: Routes = [
  { path: 'book-standup/:date', component: BookStandupComponent },
  { path: '', redirectTo: 'book-standup/2015-12-14',   pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
