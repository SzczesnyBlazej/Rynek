using NUnit.Framework;
using Rynek;
using System;
using System.Collections.Generic;

namespace TestyRynek
{
    [TestFixture]
    public class Tests
    {
        private Product product, product2;
        private Seller seller, seller2;
        private Buyer buyer, buyer2;
        private Bank bank;
        private Product obserwowanyProdukt;


        [SetUp]
        public void Setup()
        {
            bank = new Bank(0.06);


            seller = new Seller("NetMarkt", bank);
            seller2 = new Seller("Neonet", bank);

            product = new Product("Samsung 4k", 1500, 0.05, seller, bank);
            product2 = new Product("LG 8k", 2500, 0.18, seller, bank);
            buyer = new Buyer("Marcin", 3000, product, bank);
            buyer2 = new Buyer("MarekM", 3000, product2, bank);
            seller.addProductToList(product);
            seller.addProductToList(product2);
            obserwowanyProdukt = seller.getProductFromList(product);
            obserwowanyProdukt.Stara_cena = obserwowanyProdukt.Cena;
        }

        [Test]
        public void getTotalPriceOfProduct()
        {
            Assert.AreEqual(1665, product.Cena);
        }

        [Test]
        public void getCostOfProductTv()
        {
            Assert.AreEqual(1500, product.KosztWytworzeniaProduktu);
            Assert.AreEqual(1665, product.Cena);
        }

        [Test]
        public void getSellerOfProduct()
        {
            Assert.AreEqual("NetMarkt", product.Seller.Name);
        }

        [Test]
        public void countProductInList()
        {
            Assert.AreEqual(2, seller.ProductsOfSeller.Count);

        }
        [Test]
        public void changeInflation()
        {
            Assert.AreEqual(Math.Round(0.06, 2), (Math.Round(bank.Inflation, 2)));
            bank.zmianaInflacji(0.01);
            Assert.AreEqual(Math.Round(0.07, 2), (Math.Round(bank.Inflation, 2)));
        }

        [Test]
        public void changePrice()
        {

            Assert.AreEqual(1665, product.Cena);
            bank.zmianaInflacji(-0.03);
            obserwowanyProdukt.powiadomObserwatorow();
            seller.PerformOperation(seller);
            Assert.AreEqual(1615.05, product.Cena);
        }


        [Test]
        public void addObserwatorToProduct()
        {
            Assert.AreEqual(0, product.ListaObserwatorow.Count);
            product.dodajObserwatora(buyer);
            product.dodajObserwatora(buyer2);
            Assert.AreEqual(2, product.ListaObserwatorow.Count);

        }

        [Test]
        public void deleteObserwatorToProduct()
        {
            product.dodajObserwatora(buyer);
            Assert.AreEqual(1, product.ListaObserwatorow.Count);
            product.usunObserwator(buyer);
            Assert.AreEqual(0, product.ListaObserwatorow.Count);
        }

        [Test]
        public void buyProductandCheckMoney()
        {
            Assert.AreEqual(true, product.CzyDostêpny);
            product.dodajObserwatora(buyer);
            bank.zmianaInflacji(-0.03);
            bank.powiadomObserwatorow();
            double cen = buyer.Money;
            while (buyer.Money == 3000)
            {
                seller.PerformOperation(seller);
                obserwowanyProdukt.powiadomObserwatorow();
                
            }
            double profitSeller = seller.Profit;
            cen = cen - product.Cena;
            
            Assert.AreEqual(1, product.ListaObserwatorow.Count);
            Assert.AreEqual(cen, buyer.Money);
            Assert.AreEqual(false, product.CzyDostêpny);
            Assert.AreEqual(profitSeller, seller.Profit);
        }

        [Test]
        public void checkProfitOfBank()
        {

            Assert.AreEqual(0, bank.Profit);
            product.dodajObserwatora(buyer);
            bank.zmianaInflacji(-0.03);
            bank.powiadomObserwatorow();
            while (bank.Profit == 0)
            {
                seller.PerformOperation(seller);
                obserwowanyProdukt.powiadomObserwatorow();

            }
            double profitbank = bank.Profit;


            Assert.AreEqual(profitbank, bank.Profit);
        }

        [Test]
        public void checkProfitOfSeller()
        {
            Assert.AreEqual(0, seller.Profit);
            product.dodajObserwatora(buyer);
            bank.zmianaInflacji(-0.03);
            bank.powiadomObserwatorow();
            while (bank.Profit == 0)
            {
                seller.PerformOperation(seller);
                obserwowanyProdukt.powiadomObserwatorow();

            }
            double profitSeller = seller.Profit;
            double profitbank = bank.Profit;

            Assert.AreEqual(profitSeller, seller.Profit);
            Assert.AreEqual(profitbank, bank.Profit);
        }

        [Test]
        public void changeOwnBilance()
        {
            Assert.AreEqual(3000, buyer.Money);
            buyer.PerformOperation(buyer);
            Assert.AreEqual(3100, buyer.Money);
        }
    }
}