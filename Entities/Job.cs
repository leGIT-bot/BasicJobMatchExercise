using CsvHelper.Configuration.Attributes;

namespace Project.Entities;

public class Job
{
    [Index(0)]
    public int Id { get; set; }
    [Index(1)]
    public string Name { get; set; }
    [Index(2)]
    public List<string> RequiredSkills { get; set; }
}
