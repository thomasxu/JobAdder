import {Component, OnInit} from '@angular/core';
import {JobService} from '../services/job.service';
import {MatchedJob} from '../models/job.model';
import {Observable, of} from 'rxjs';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.less']
})
export class JobsComponent implements OnInit {

  matchedJobs$: Observable<MatchedJob[]>;

  constructor(private jobService: JobService) {}

  ngOnInit() {
    this.matchedJobs$ = this.jobService.GetJobsWithMatchedCandidates();
  }
}
