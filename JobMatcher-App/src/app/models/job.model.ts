export class MatchedCandidate {
  candidateId: number;
  name: string;
  matchedScore: number;
  matchedSkills: string[];
}

export class MatchedJob {
  jobId: number;
  name: string;
  company: string;
  matchedCandidates: MatchedCandidate[];
}
