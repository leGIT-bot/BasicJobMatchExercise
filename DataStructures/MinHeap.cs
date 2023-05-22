﻿using Project.Entities;
using Project.Services;

namespace Project.DataStructures;

public class MinHeap
{
    private readonly Job[] _elements;
    private int _size;
    private JobService _seekerService;

    public MinHeap(JobService seekerService, int size)
    {
        _elements = new Job[size];
        _seekerService = seekerService;
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

    public void AddMany(IEnumerable<Job> jobs)
    {
        foreach (var job in jobs)
        {
            Add(job);
        }
    }

    public void Add(Job element)
    {
        if (_size == _elements.Length)
        {
            if (_seekerService.Compare(element, Peek()))
            {
                _elements[0] = element;
                ReCalculateDown();
            }
        }
        else
        {
            _elements[_size] = element;
            _size++;

            ReCalculateUp();
        }
    }

    public List<Job> Export()
    {
        List<Job> jobs = new List<Job>();
        int size = _size;

        for (int i = 0; i < size; i++)
        {
            jobs.Add(Pop());
        }
        jobs.Reverse();
        return jobs;
    }

    private void ReCalculateDown()
    {
        int index = 0;
        while (HasLeftChild(index))
        {
            var smallerIndex = GetLeftChildIndex(index);
            if (HasRightChild(index) && _seekerService.Compare(GetLeftChild(index), GetRightChild(index)))
            {
                smallerIndex = GetRightChildIndex(index);
            }

            if (_seekerService.Compare(_elements[smallerIndex], _elements[index]))
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
        while (!IsRoot(index) && _seekerService.Compare(GetParent(index), _elements[index]))
        {
            var parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = parentIndex;
        }
    }
}
