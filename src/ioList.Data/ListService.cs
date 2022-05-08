using System;
using System.Collections.Generic;
using ioList.Domain;
using ioList.Services;

namespace ioList.Data
{
    public class ListService : IListService
    {
        private readonly ListContext _context;

        public ListService(ListContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void RenameList(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty.");
            
            _context.List.Name = name;

            _context.SaveChanges();
        }

        public IEnumerable<Card> GetCards()
        {
            throw new System.NotImplementedException();
        }

        public Card GetCard(int id)
        {
            throw new System.NotImplementedException();
        }

        public void ExcludeCard(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Point> GetList()
        {
            throw new System.NotImplementedException();
        }

        public void AddPoint(Point point)
        {
            throw new System.NotImplementedException();
        }

        public void SetPointInclusion(int pointId, bool include)
        {
            throw new System.NotImplementedException();
        }
    }
}