using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek
{
    public class Product : IObserwowany, IElement
    {
        private string nazwa_produktu;
        private double kosztWytworzeniaProduktu;
        private double marza;
        private double cena;
        private double stara_cena;
        private bool czyDostępny=true;
        private Seller seller;
        private Bank bank;

        private List<IObserwator> listaObserwatorow = new List<IObserwator>();

        private List<Product> products = new List<Product>();

        public Product(string nazwa_produktu, double kosztWytworzeniaProduktu, double marza,Seller s, Bank b)
        {
            Nazwa_produktu = nazwa_produktu;
            KosztWytworzeniaProduktu = kosztWytworzeniaProduktu;
            Marza = marza;
            Seller = s;
            Bank = b;
            Cena = kosztWytworzeniaProduktu+kosztWytworzeniaProduktu* marza + kosztWytworzeniaProduktu * Bank.Inflation;
            
        }
        public string Nazwa_produktu { get => nazwa_produktu; set => nazwa_produktu = value; }
        public double KosztWytworzeniaProduktu { get => kosztWytworzeniaProduktu; set => kosztWytworzeniaProduktu = value; }
        public double Marza { get => marza; set => marza = value; }
        public List<Product> Products { get => products; set => products = value; }
        public Seller Seller { get => seller; set => seller = value; }
        public Bank Bank { get; }
        public double Cena { get => cena; set => cena = value; }
        public double Stara_cena { get => stara_cena; set => stara_cena = value; }
        
        public bool CzyDostępny { get => czyDostępny; set => czyDostępny = value; }
        public List<IObserwator> ListaObserwatorow { get => listaObserwatorow; set => listaObserwatorow = value; }

        public void addProductToList(Product product)
        {
            Products.Add(product);
        }

        public void deleteProductFromList(Product produkt)
        {
            Products.Remove(produkt);
        }

        public bool countProducts()
        {
            return Products.Count > 0;
        }

        public void dodajObserwatora(IObserwator o)
        {
            ListaObserwatorow.Add(o);
            Buyer buyer = (Buyer)o;
            Console.WriteLine(nazwa_produktu + " Jest obserwowany przez "+buyer.Name);
        }

        public void usunObserwator(IObserwator o)
        {
            ListaObserwatorow.Remove(o);
            Buyer buyer = (Buyer)o;
            Console.WriteLine(buyer.Name + " przestał obserwować " + nazwa_produktu);
        }

        public void powiadomObserwatorow()
        {
            if(listaObserwatorow.Count > 0) { 
                foreach (var item in ListaObserwatorow)
                {
                    Buyer buyer=(Buyer)item;   
                    Console.WriteLine(buyer.Name+ " Otrzymał informację o zmianie ceny\r\n");
                    buyer.zakup();
                }
            }
            else
            {
                Console.WriteLine("Nikt nie obserwuje już tych produktów");
            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            if (seller.Aktualnainflacja >= 0.06)
            {
                
                Console.WriteLine("Cena została zwiększona z " + stara_cena + "zł na : " + Math.Round(cena + cena * (seller.Aktualnainflacja - seller.Bank.PierwotnaInflacja), 2) + "zł");
                cena = Math.Round(cena + cena * (seller.Aktualnainflacja - seller.Bank.PierwotnaInflacja), 2);
            }
            else
            {
                
                Console.WriteLine("Cena została zmniejszona z " + cena + "zł na : " + Math.Round(cena - (cena * (seller.Bank.PierwotnaInflacja - seller.Aktualnainflacja)), 2) + "zł");
                cena = Math.Round(cena - (cena * (seller.Bank.PierwotnaInflacja - seller.Aktualnainflacja)), 2);
            }
        }
    }
}
