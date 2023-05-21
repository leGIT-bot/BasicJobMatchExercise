using Project.Entities;

namespace Project.Services;

public class JobService
{
    private JobSeeker _seeker;
    public JobService(JobSeeker seeker)
    {
        _seeker = seeker;
    }

    public bool Compare(Job job1, Job job2)
    {
        int skillNum1 = Compatibility(job1);
        int skillNum2 = Compatibility(job2);
        if (skillNum1 > skillNum2)
        {
            return true;
        }
        else if (skillNum1 < skillNum2)
        {
            return false;
        }
        else
        {
            return job1.Id < job2.Id;
        }
    }

    public int Compatibility(Job job)
    {
        return job.RequiredSkills.Intersect(_seeker.Skills).Count();
    }

    public string ToString(Job job)
    {
        return $"{_seeker.Id},{_seeker.Name},{job.Id},{job.Name},{Compatibility(job)}";
    }
}
