import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatBadgeModule,
  MatButtonModule, MatChipsModule, MatExpansionModule,
  MatIconModule, MatInputModule,
  MatListModule,
  MatMenuModule, MatProgressSpinnerModule,
  MatSidenavModule,
  MatTabsModule,
  MatToolbarModule, MatTooltipModule
} from '@angular/material';
import { JobsComponent } from './jobs/jobs.component';
import {HttpClientModule} from '@angular/common/http';
import { CandidateComponent } from './candidate/candidate.component';

@NgModule({
  declarations: [
    AppComponent,
    JobsComponent,
    CandidateComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
    MatTabsModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatExpansionModule,
    MatTooltipModule,
    MatButtonModule,
    MatChipsModule,
    MatBadgeModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
