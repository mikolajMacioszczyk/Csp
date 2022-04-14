﻿namespace Lista2.Interface
{
    public interface IValueHeuristic<T>
    {
        IEnumerable<T> OrderValues(IEnumerable<T> values);
        void Use(T value);
        void Remove(T value);
    }
}
