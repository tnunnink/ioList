using System;

namespace ioList.Domain
{
    public class CardType
    {
        private CardType()
        {
        }

        public CardType(int id, int productId, string productName)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string LocalName { get; private set; }

        public void SetLocalName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name can not be null or empty");

            LocalName = name;
        }
    }
}