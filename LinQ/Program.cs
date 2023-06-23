using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ
{
    internal class Program
    {

        static void musteriListele(Musteri m)
        {

            Console.WriteLine(m.isim+ " " +m.soyisim );
        }
        static bool funcDelegateKullanimi1(Musteri m) 
        {
            if (m.isim[0]=='a') 
                return true;
            else            
                return false;
            
        }

        static bool PpedicateDelegateMetot(Musteri m)
        {
            if(m.dogumTarihi.Year>1990)
                return true;
            else return false;
        }
        static void Main(string[] args)
        {
            int bulunantoplam = 0;
            DataSources ds = new DataSources();
            List<Musteri> musteriliste = ds.musteriListesi();

            //Delegate kullanimi
            var DelegateKullanimi1 = musteriliste.Where(I => I.isim.StartsWith("A"));
            Func<Musteri, bool> funcDelege1 = new Func<Musteri, bool>(funcDelegateKullanimi1);
            var DelegateKullanimi2 = musteriliste.Where(funcDelege1);

            var DelegateKullanimi3 = musteriliste.Where(new Func<Musteri, bool>(funcDelegateKullanimi1));
            var DelegateKullanimi4 = musteriliste.Where(delegate (Musteri m) { return m.isim[0] == 'A' ? true : false; });
            var DelegateKullanimi5 = musteriliste.Where((Musteri m) => { return m.isim[0] == 'A' ? true : false; });
            var DelegateKullanimi6 = musteriliste.Where((m) => {return m.isim[0]=='A'? true: false; });
            var DelegateKullanimi7 = musteriliste.Where(m => m.isim[0] == 'A');





            //Predicate Kullanimi
            Predicate<Musteri> predicate = new Predicate<Musteri>(PpedicateDelegateMetot);
            var delegateKullanimiPredicate1 = musteriliste.FindAll(predicate);
            var delegateKullanimiPredicate2 = musteriliste.FindAll(new Predicate<Musteri>(PpedicateDelegateMetot));
            var delegateKullanimiPredicate3 = musteriliste.FindAll(delegate (Musteri m) { return m.dogumTarihi.Year > 1990; });
            var delegateKullanimiPredicate4 = musteriliste.FindAll((Musteri m) => { return m.dogumTarihi.Year > 1990;} );
            var delegateKullanimiPredicate5 = musteriliste.FindAll((m) => { return m.dogumTarihi.Year > 1990; });
            var delegateKullanimiPredicate6 = musteriliste.FindAll(m =>  m.dogumTarihi.Year > 1990);




            //Action Delegate
            Action<Musteri> actionMusteri = new Action<Musteri>(musteriListele);
            musteriliste.ForEach(actionMusteri);

            musteriliste.ForEach(new Action<Musteri>(musteriListele));

            musteriliste.ForEach(delegate (Musteri m) { Console.WriteLine(m.isim + " " + m.soyisim); });

            musteriliste.ForEach((Musteri m) => { Console.WriteLine(m.isim + " " + m.soyisim); });

            musteriliste.ForEach((m) => { Console.WriteLine(m.isim + " " + m.soyisim); });

            















            // LinQ sorgulama Cesitleri
            //1. yol            
            int toplamMusteriAdet=musteriliste.Where(I => I.isim.StartsWith("A")).Count();
            //2. yol
            var toplamMusteriAdet2 =(from I in musteriliste
                                   where I.isim.StartsWith("A")
                                   select I).Count();
            //Alistirmalar
            // Musteriler icerisinde ulke degeri a ile baslayan musterilerin linq metodu ile kullanarak bulalim
            //1
            IEnumerable<Musteri> musteriListeAlistirma1 = musteriliste.Where(I => I.ulke.StartsWith("A"));
            Console.WriteLine(musteriListeAlistirma1.Count());

            //2 musteriler listesi icerisindeki kayitlardan isminin icinde b harfi bulunan ve ulke degeri icinde A harfi bulunan musterilerin listesi
            var musterilistealistirma21=musteriliste.Where(I=> I.isim.Contains("a")&& I.ulke.Contains("a")).ToList();

            IEnumerable<Musteri> musteriListeAlistirma2 = from x in musteriliste 
                                                          where x.isim.Contains("b")
                                                          where x.ulke.Contains("a")
                                                          select x;
            IEnumerable<Musteri> musteriListeAlistirma21 = from x in musteriliste
                                                          where x.isim.Contains("b") && x.ulke.Contains("a")
                                                          select x;

            //3 musteriler listesi icerisinde dogumyili 1990 dan buyuk olan ve isminin icerisinde a harfi gecen musteriler listesi

            var musterilistesiAlistirma3= from x in musteriliste
                                          where x.dogumTarihi.Year>1986 &&  x.isim.Contains("a")
                                          select x;

            //2.Yol
            var AIleBaslayanUlkeler2 = from I in musteriliste
                                       where I.ulke.StartsWith("A")
                                       select I;
            int AIleBaslayanUlkeler2sayisi=AIleBaslayanUlkeler2.Count();



            /*
            
            for (int i = 0; i < musteriliste.Count; i++)
            {
                if (musteriliste[i].isim[0]== 'A')
                {
                    bulunantoplam++;
                }
            }

            Console.WriteLine("Liste icerisinde bulunan a ile baslayan isim sayisi toplam {0}",bulunantoplam);
            Console.WriteLine(musteriliste.Count);
            */

            //LINQ
            bulunantoplam = 0;
            bulunantoplam=musteriliste.Where(i=>i.isim.StartsWith("A")).Count();
            Console.ReadLine();


            //Linq Odev

            //Musteri listesi icerisindeki kayitlardan ismi a ile baslayan soyismi degeri icinde e olan ve dogum yili 1985 den buyuk olan kayitlari getirin

            
            var musterilisteOdev1 = musteriliste.Where(m => (m.isim.StartsWith("A") || m.isim.StartsWith("a")) && (m.soyisim.Contains("e") || m.soyisim.Contains("E")) && m.dogumTarihi.Year > 1985);

        }
    }
}
