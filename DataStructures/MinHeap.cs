using Project.Entities;

namespace Project.DataStructures;

public class MinHeap
{
    private readonly Job[] _elements;
    private int _size;
    private JobSeeker _seeker;

    public MinHeap(JobSeeker seeker, int size)
    {
        _elements = new Job[size];
        _seeker = seeker;
    }

    private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
    private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
    private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

    private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
    private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
    private bool IsRoot(int elementIndex) => elementIndex == 0;

    private Job GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
    private Job GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
    private Job GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

    private void Swap(int firstIndex, int secondIndex)
    {
        var temp = _elements[firstIndex];
        _elements[firstIndex] = _elements[secondIndex];
        _elements[secondIndex] = temp;
    }

    public bool IsEmpty()
    {
        return _size == 0;
    }

    public Job Peek()
    {
        if (_size == 0)
            throw new IndexOutOfRangeException();

        return _elements[0];
    }

    public Job Pop()
    {
        if (_size == 0)
            throw new IndexOutOfRangeException();

        var result = _elements[0];
        _elements[0] = _elements[_size - 1];
        _size--;

        ReCalculateDown();

        return result;
    }

    public void Add(Job element)
    {
        if (_size == _elements.Length)
            throw new IndexOutOfRangeException();

        _elements[_size] = element;
        _size++;

        ReCalculateUp();
    }

    private void ReCalculateDown()
    {
        int index = 0;
        while (HasLeftChild(index))
        {
            var smallerIndex = GetLeftChildIndex(index);
            if (HasRightChild(index) && Compare(GetLeftChild(index), GetRightChild(index)))
            {
                smallerIndex = GetRightChildIndex(index);
            }

            if (Compare(_elements[smallerIndex], _elements[index]))
            {
                break;
            }

            Swap(smallerIndex, index);
            index = smallerIndex;
        }
    }

    private void ReCalculateUp()
    {
        var index = _size - 1;
        while (!IsRoot(index) && Compare(GetParent(index), _elements[index]))
        {
            var parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = parentIndex;
        }
    }

    private bool Compare(Job job1, Job job2)
    {
        int skillNum1 = Compatibility(job1);
        int skillNum2 = Compatibility(job2);
        if (skillNum1 > skillNum2)
        {
            return true;
        }
        else if(skillNum1 < skillNum2) {
            return false;
        }
        else
        {
            return job1.Id < job2.Id;
        }
    }

    private int Compatibility(Job job)
    {
        return job.RequiredSkills.Intersect(_seeker.Skills).Count();
    }
}
