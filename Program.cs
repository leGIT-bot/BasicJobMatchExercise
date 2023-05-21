using System.Globalization;
using CsvHelper;
using Project.Entities;

int size = 5;

IEnumerable<Job> jobs;
using (var reader = new StreamReader("jobs.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    csv.Context.RegisterClassMap<JobMap>();
    jobs = csv.GetRecords<Job>();
}

IEnumerable<JobSeeker> jobSeekers;
using (var reader = new StreamReader("jobseekers.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    csv.Context.RegisterClassMap<JobSeekerMap>();
    jobSeekers = csv.GetRecords<JobSeeker>();
}
