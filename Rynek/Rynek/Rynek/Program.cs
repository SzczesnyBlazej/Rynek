using Rynek;

class Program
{
    private static Product obserwowanyProdukt;
    private static Product obserwowanyProdukt2;

    static void Main(string[] args)
    {
        Bank bank = new Bank(0.06);
        Seller seller = new Seller("NetVision", bank);
        Product product = new Product("TV", 1500, 0.05, seller,bank);
        Product product2 = new Product("TV Samsung 8k", 500, 0.05, seller,bank);
        seller.addProductToList(product);
        seller.addProductToList(product2);
        Buyer buyer = new Buyer("Marian", 1700, product,bank);
        Buyer buyer2 = new Buyer("Błażej", 1500, product2,bank);
        bank.dodajObserwatora(seller);
        bank.dodajObserwatora(buyer);

        bank.PierwotnaInflacja = bank.Inflation;
        

        

        obserwowanyProdukt =seller.getProductFromList(product);
        obserwowanyProdukt2=seller.getProductFromList(product2);
        
        obserwowanyProdukt.Stara_cena = obserwowanyProdukt.Cena;
        obserwowanyProdukt2.Stara_cena = obserwowanyProdukt2.Cena;

        obserwowanyProdukt.dodajObserwatora(buyer);
        obserwowanyProdukt2.dodajObserwatora(buyer2);


        bank.zmianaInflacji(-0.03);
        bank.powiadomObserwatorow();
        seller.PerformOperation(seller);
        obserwowanyProdukt.powiadomObserwatorow();
        obserwowanyProdukt2.powiadomObserwatorow();

        obserwowanyProdukt2.usunObserwator(buyer2);

        bank.zmianaInflacji(-0.02);
        bank.powiadomObserwatorow();
        seller.PerformOperation(seller);
        obserwowanyProdukt.powiadomObserwatorow();
        buyer.PerformOperation(buyer);

        bank.zmianaInflacji(0.03);
        bank.powiadomObserwatorow();
        seller.PerformOperation(seller);
        obserwowanyProdukt.powiadomObserwatorow();

    }
}