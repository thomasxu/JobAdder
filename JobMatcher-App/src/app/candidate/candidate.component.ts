import {Component, Input, OnInit} from '@angular/core';
import {MatchedCandidate} from '../models/job.model';

@Component({
  selector: 'app-candidate',
  templateUrl: './candidate.component.html',
  styleUrls: ['./candidate.component.less']
})
export class CandidateComponent implements OnInit {
  @Input()
  title: string;
  @Input()
  matchedCandidate: MatchedCandidate;

  constructor() { }

  ngOnInit() {
  }

}
