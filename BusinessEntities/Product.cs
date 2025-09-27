using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private decimal? _price;
        private CategoryTypes? _category;
        private int? _stockQuantity;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public decimal Price
        {
            get => _price.Value;
            private set => _price = value;
        }

        public CategoryTypes Category
        {
            get => _category.Value;
            private set => _category = value;
        }

        public int StockQuantity
        {
            get => _stockQuantity.Value;
            private set => _stockQuantity = value;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetPrice(decimal price)
        {
            _price = price;
        }

        public void SetCategory(CategoryTypes category)
        {
            _category = category;
        }

        public void SetStockQuantity(int stockQuantity)
        {
            _stockQuantity = stockQuantity;
        }
    }
}
