using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek
{
    public class Buyer : IObserwator, IElement, IVisitor
    {
        private string name;
        private double money;
        private bool czyKupic;
        private Product product;
        private Bank bank;

        private List<Product> productsToBuy = new List<Product>();

        public Buyer(string name, double money, Product p, Bank b)
        {
            Name = name;
            Money = money;
            product = p;
            bank = b;
        }

        public string Name { get => name; set => name = value; }
        public double Money { get => money; set => money = value; }
        public List<Product> ProductsToBuy { get => productsToBuy; set => productsToBuy = value; }
        public bool CzyKupic { get => czyKupic; set => czyKupic = value; }

        public void PerformOperation(IVisitor visitor)
        {
            Buyer buyer=visitor as Buyer;
            buyer.Accept(visitor);
        }

        public void Visit(IElement element)
        {
            Buyer buyer = element as Buyer;
            money = Math.Round((money + 100),2);
            Console.WriteLine(Name + " otrzymał premię 100zł, jego saldo wynosi " + money+"zł");
        }

        public void aktualizacjaDanych()
        {
            if (product.CzyDostępny) 
            {
                Console.WriteLine(Name + " otrzymał informację o zmianie inflacji z " + Math.Round(bank.PierwotnaInflacja * 100, 2) + "% na " + Math.Round(bank.Inflation * 100, 2) + "% w " + bank.Name);         
            }
            else
            {
                Console.WriteLine(Name+": Produkt " + product.Nazwa_produktu + " Jest aktualnie niedostępny");
            }
        }

        public void zakup()
        {
            Random rnd = new Random();
            int poczatek = 0;
            int koniec = 101;
            double wylosowana = rnd.Next(poczatek, koniec)*1.3;
            double obnizka = Math.Round(product.Cena / product.Stara_cena*100,0);

            if (wylosowana>=obnizka)
            {
                if (money >= product.Cena)
                {
                    czyKupic = true;
                    product.CzyDostępny = false;
                    Console.WriteLine(name + ": zakupił przedmiot " + product.Nazwa_produktu + " po obniżce o " + Math.Round((1.0 - (product.Cena / product.Stara_cena)) * 100, 2) + "% od ceny początkowej w sklepie " + product.Seller.Name+ "\r\n");
                    
                    bank.Profit = Math.Round(product.Cena * bank.Inflation,2);
                    productsToBuy.Add(product);
                    money = money - product.Cena;
                    product.Seller.Profit +=Math.Round(product.Cena*product.Marza,2);
                    bank.Profit=Math.Round(product.Cena*bank.Inflation,2);
                }
                else
                {
                    Console.WriteLine(Name + "(" + money + "zł): Nie ma wystarczająco pieniędzy na zakup:" + product.Nazwa_produktu + "(" + product.Cena + "zł)\r\n");
                }
            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
