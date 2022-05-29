using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek
{
    public class Bank :IObserwowany
    {
        private double inflation;
        private string nameOfBank="Bank Centralny";
        private double pierwotnaInflacja;
        private double profit;
        private List<IObserwator> listaObserwatorow = new List<IObserwator>();
        public double Inflation { get => inflation; set => inflation = value; }
        public double Profit { get => profit; set => profit = profit+value; }
        public double PierwotnaInflacja { get => pierwotnaInflacja; set => pierwotnaInflacja = value; }


        public string Name { get => nameOfBank; set => nameOfBank = value; }

        public Bank(double inflation)
        {
            Inflation = inflation;
        }

        public void dodajObserwatora(IObserwator o)
        {
            listaObserwatorow.Add(o);
        }

        public void usunObserwator(IObserwator o)
        {
            listaObserwatorow.Remove(o);
        }

        public void powiadomObserwatorow()
        {
            foreach (var item in listaObserwatorow)
            {
                item.aktualizacjaDanych();
            }
        }

        public double zmianaInflacji(double zmiana)
        {
            Console.WriteLine(Name+ " Ogłasza zmianę infalcji");
            pierwotnaInflacja = inflation;
            inflation = inflation + zmiana;

            return inflation;
        }
    }
}
