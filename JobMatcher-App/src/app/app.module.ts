import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatIconModule, MatInputModule,
  MatMenuModule, MatSidenavModule,
  MatToolbarModule
} from '@angular/material';
import { JobsComponent } from './jobs/jobs.component';

@NgModule({
  declarations: [
    AppComponent,
    JobsComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    MatMenuModule,
    MatIconModule,
    MatSidenavModule,
    MatToolbarModule,
    MatInputModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
