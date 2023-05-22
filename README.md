# About
Basic job recommendation software which suggests a number of jobs per candidate based on overlap between job required skills and candidate skills.

# Prereqs
1. Have git and ssh setup
2. Install dotnetcore 7.0

# Instructions
1. Clone https://github.com/leGIT-bot/BasicJobMatchExercise (ideally using ssh).
2. Rebuild Project.sln
3. Open .\bin\Debug\net7.0
4. Run execute batfile, potentially modefying the first arg for a different number of suggestions.

# Performance
Let S be number of seekers
Let J be number of jobs
Let N be number of recommendations
For simplicity, let C be skill comparison cost
1. A min heap can be used to get to get top N out of J jobs for a seeker in O(CJlog(N))
2. Hence O(CJSlog(N)) time complexity
Let A be the size of jobs.csv
Let B be the size of jobseekers.csv
Space complexity is O(A+B)