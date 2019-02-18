import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {JobsComponent} from './jobs/jobs.component';

const routes: Routes = [
  {
    path      : '',
    redirectTo: 'jobs',
    pathMatch : 'full'
  },
  {
    path: 'jobs',
    component: JobsComponent
  },
  {
    path: '**',
    redirectTo: '/'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
