using CsvHelper.Configuration.Attributes;

namespace Project.Entities;

public class JobSeeker
{
    [Index(0)]
    public int Id { get; set; }
    [Index(1)]
    public string Name { get; set; }
    [Index(2)]
    public List<string> Skills { get; set; }
}
