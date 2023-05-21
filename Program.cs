using System.Globalization;
using System.Text;
using CsvHelper;
using Project.DataStructures;
using Project.Entities;
using Project.Services;

int size = 5;

IEnumerable<Job> jobs;
using (var reader = new StreamReader("jobs.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    csv.Context.RegisterClassMap<JobMap>();
    jobs = csv.GetRecords<Job>().ToList();
}

IEnumerable<JobSeeker> jobSeekers;
using (var reader = new StreamReader("jobseekers.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    csv.Context.RegisterClassMap<JobSeekerMap>();
    jobSeekers = csv.GetRecords<JobSeeker>().ToList();
}

string separator = ",";
StringBuilder output = new StringBuilder();
string[] headings = { "jobseeker_id", "jobseeker_name", "job_id", "job_title", "matching_skill_count" };
output.AppendLine(string.Join(separator, headings));

foreach (var seeker in jobSeekers)
{
    JobService jobService = new JobService(seeker);
    MinHeap heap = new MinHeap(jobService, size);
    heap.AddMany(jobs);
    foreach (Job job in heap.Export())
    {
        output.AppendLine(jobService.ToString(job));
    }
}

try
{
    File.AppendAllText("./output.csv", output.ToString());
}
catch (Exception ex)
{
    Console.WriteLine("Data could not be written to the CSV file.");
    return;
}