using System.Collections.Generic;
using ioList.Domain;

namespace ioList.Services
{
    public interface IListService
    {
        IEnumerable<Card> GetCards();
        Card GetCard(int id);
        Card GetCard(string name);
        void AddCard(Card card);
        void RemoveCard(Card card);
    }
}