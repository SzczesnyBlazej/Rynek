using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek
{
    public class Seller : IObserwator, IVisitor
    {
        private string name;
        private Bank bank;
        private double aktualnainflacja;
        private double profit;
        private List<Product> productsOfSeller = new List<Product>();
        

        public Seller(string name,Bank b)
        {
            this.Name = name;
            this.Bank = b;
        }

        public string Name { get => name; set => name = value; }
        public List<Product> ProductsOfSeller { get => productsOfSeller; set => productsOfSeller = value; }
        public double Aktualnainflacja { get => Bank.Inflation; }
        public double Profit { get => profit; set => profit = value; }
        public Bank Bank { get => bank; set => bank = value; }

        public void addProductToList(Product product)
        {
            productsOfSeller.Add(product);
        }

        public void aktualizacjaDanych()
        {
            Console.WriteLine(Name + " otrzymał informację o zmianie inflacji z " + Math.Round(Bank.PierwotnaInflacja * 100, 2) + "% na " + Math.Round(Bank.Inflation * 100, 2) + "% w " + Bank.Name);

        }

        public Product getProductFromList(Object product)
        {
            foreach (var item in productsOfSeller)
            {
                if (item == product)
                {
                    return (Product)product;
                }
            }
            return null;
        }


        public void PerformOperation(IVisitor visitor)
        {
            foreach (Product prod in productsOfSeller)
            {
                prod.Accept(visitor);
                
            }
        }

        public void Visit(IElement element)
        {
            Product product = element as Product;
            Console.WriteLine(Name + " Zmianił cenę dla " + product.Nazwa_produktu);
        }
    }
}
