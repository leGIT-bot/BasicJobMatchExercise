using CsvHelper.Configuration;

namespace Project.Entities;

public class JobMap : ClassMap<Job>
{
    public JobMap() {
        Map(p => p.Id).Index(0);
        Map(p => p.Name).Index(1);
        Map(p => p.RequiredSkills).Convert(s => s.Row[2]?.Split(",").ToList() ?? new List<string>());
    }
}
