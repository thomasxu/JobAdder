import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {MatchedJob} from '../models/job.model';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  constructor(private http: HttpClient) { }

  GetJobsWithMatchedCandidates() {
    return this.http.get<MatchedJob[]>(`${environment.apiBase}/jobs`);
  }
}
