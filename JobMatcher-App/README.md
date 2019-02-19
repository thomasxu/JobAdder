
## JobMatcher App and API

This repository contains the code of the code challenge from JobAdder.
The application will find the top and matched skills candidates for all jobs listed on 
http://private-76432-jobadder1.apiary-mock.com
 

# Installation pre-requisites
Please install the latest version of Angular CLI and NodeJs

# Source code structure
It contains 2 applications
 1. JobMatcher.Api which is the frontend angular7 app 
 2. JobMatcher which is backend using Dotnet Core 2.2 

 
# How to run backend server, 
Make sure you are in Development Mode

use Rider or Visual Studio F5 
or cd into JobAdder.Api and type:
    dotnet run     
It will bring up the development server on [http://localhost:5050](http://localhost:5050)

#How to run frontend server
open command prompt and cd to directory JobAdder/JobMatcher.App
npm install
ng run
The application is visible at port 4200: [http://localhost:4200](http://localhost:4200)

I
#Algorithm
The application is trying to find the best and matched candidates for the job.
A match means there are common skills from job and candidate
Skills comparison is case insensitive, will ignore leading and trailing white space
and if duplicate skill it finds will use the first one and ignore the others.
The order of the skill is considered and using the score to compare mark,
both candidate and job the 1st skill in the skill list will have score 100, 2nd
99, 3rd 98 ... 100th 0, e.g.  Candidate has skills ['dotnet', 'angular', 'aws']
and Company has skills ['aws', 'sql', 'angular']. Two matches will be found between candidate and company skills and the total score of the match is (99 + 98) + (98 + 100) = 395, the higher the score the better the match. And the highest
score match will be put as the featured candidate for the company.

#Assumption:
There will not be more than 100 skills for jobs or candidates
