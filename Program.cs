using System;
using System.Security.Policy;

namespace System_zarzadzania_przesylkami_miedzynarodowymi
{
    public interface IPaczka
    {
        void Spakuj();
    }

    public interface IKurier
    {
        void Dostarcz();
    }

    public class MalaPaczka : IPaczka
    {
        public void Spakuj()
        {
            Console.WriteLine("Spakowano małą paczkę.");
        }
    }

    public class DuzaPaczka : IPaczka
    {
        public void Spakuj()
        {
            Console.WriteLine("Spakowano dużą paczkę.");
        }
    }

    public class DHLKurier : IKurier
    {
        public void Dostarcz()
        {
            Console.WriteLine("Dostarczono przez kuriera DHL.");
        }
    }

    public class UPSKurier : IKurier
    {
        public void Dostarcz()
        {
            Console.WriteLine("Dostarczono przez kuriera UPS.");
        }
    }

    public interface IFabrykaLogistyki
    {
        IPaczka UtworzPaczke();
        IKurier UtworzKuriera();
    }

    public class FabrykaLogistykiPolska : IFabrykaLogistyki
    {
        public IPaczka UtworzPaczke()
        {
            return new MalaPaczka();
        }

        public IKurier UtworzKuriera()
        {
            return new DHLKurier();
        }
    }

    public class FabrykaLogistykiSzwecja : IFabrykaLogistyki
    {
        public IPaczka UtworzPaczke()
        {
            return new DuzaPaczka();
        }

        public IKurier UtworzKuriera()
        {
            return new UPSKurier();
        }
    }

    class ZarzadzaniePrzesylkami
    {
        private IFabrykaLogistyki fabrykaLogistyki;
        private static ZarzadzaniePrzesylkami _instancja;
        private ZarzadzaniePrzesylkami() { }
        public static ZarzadzaniePrzesylkami Instancja
        {
            get
            {
                if (_instancja == null)
                {
                    _instancja = new ZarzadzaniePrzesylkami();
                }
                return _instancja;
            }
        }

        public enum Lokalizacja
        {
            Polska,
            Szwecja
        }
        
        public void PrzyjmijZamowienie(Lokalizacja lokalizacja)
        {
            switch (lokalizacja)
            {
                case Lokalizacja.Polska:
                    fabrykaLogistyki = new FabrykaLogistykiPolska();
                    break;
                case Lokalizacja.Szwecja:
                    fabrykaLogistyki = new FabrykaLogistykiSzwecja();
                    break;
                default:
                    throw new ArgumentException("Nieobsługiwana lokalizacja.");
            }

            var paczka = fabrykaLogistyki.UtworzPaczke();
            var kurier = fabrykaLogistyki.UtworzKuriera();
            paczka.Spakuj();
            kurier.Dostarcz();
        }
    }
    class Program
    {
        static void Main()
        {
            var system = ZarzadzaniePrzesylkami.Instancja;

            system.PrzyjmijZamowienie(ZarzadzaniePrzesylkami.Lokalizacja.Polska);
            system.PrzyjmijZamowienie(ZarzadzaniePrzesylkami.Lokalizacja.Szwecja);

            Console.ReadLine(); 
        }
    }


}