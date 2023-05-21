using CsvHelper.Configuration;

namespace Project.Entities;

public class JobSeekerMap : ClassMap<JobSeeker>
{
    public JobSeekerMap() {
        Map(p => p.Id).Index(0);
        Map(p => p.Name).Index(1);
        Map(p => p.Skills).Convert(s => s.Row[2]?.Split(",").ToList() ?? new List<string>());
    }
}
