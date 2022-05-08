using System;
using System.Collections.Generic;
using ioList.Domain;

namespace ioList.Services
{
    public interface IListService : IDisposable
    {
        void RenameList(string name);
        IEnumerable<Card> GetCards();
        Card GetCard(int id);
        void ExcludeCard(int id);
        IEnumerable<Point> GetList();
        void AddPoint(Point point);
        void SetPointInclusion(int pointId, bool include);
    }
}