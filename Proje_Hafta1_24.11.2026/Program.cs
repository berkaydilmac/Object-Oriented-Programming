
using System;
using System.Collections.Generic;
enum CihazTuru
{//okunabilirliği,kod düzenini sağlamak ve belirli bir veri tipi elde etmek için enum yapısını kuruyoruz.
    GuvenlikSistemi,
    Lamba,
    Klima,
}
//BURADA AÇMA VE KAPAMA FONKSİYONLARI NEREDEYSE AYNI. ASLINDA BİR YARDIMCI FONKSİYON İLE İKİSİNİ DAHA TEMİZ BİR ŞEKİLDE YAZABİLİRDİK.
//AKLIMIZIN BİR KENARINDA DURUYOR.
class Cihaz
{
    public int Kimlik { get; private set; }
    public string Ad { get; }//ad değiştirme işlemi tanımalmıyorum.Ad sabit kalacak.
    public CihazTuru Tur { get; private set; }//Diğer değerler dışarıdan değişmemeli.Bu nedenle private set olarak alıyorum.
    public bool AcikMi { get; private set; }

    public Cihaz(int kimlik, string ad, CihazTuru tur)//Tür yapısını enum olarak aldığım için veri tipine enumu yazıyorum ve constructor yapısını oluşturuyorum.
    //bool acikmi parametresini sonradan çıkarttım.Çünkü zaten kapalı olması isteniyor bizden başlangıç için.
    {
        if (string.IsNullOrWhiteSpace(ad))//Burada isim için gerekli try exception yapısını kurup kontrol sağlıyorum.
        {
            throw new ArgumentException("HATA!!! Cihaz adi bos olamaz!!!");//parametre geçersiz olabileceği için argument exception fırlatıyorum.
        }

        Kimlik = kimlik;
        Ad = ad;
        Tur = tur;
        AcikMi = false;
    }
    public void Ac() //Açma fonksiyonunu ek bir else yapısı kullanmadan oluşturuyorum.
    {
        if (AcikMi)
        {
            Console.WriteLine("Cihaz acik durumda!");
            return;//son derste üstünde durduğumuz clean code yapısı ile yazıldı.Aksi takdirde else yapısı da koda dahil edilecekti.
        }
        AcikMi = true;
        Console.WriteLine("Cihaz aciliyor.");
    }

    public void Kapat()//Kapatma fonksiyonunu ek bir else yapısı kullanmadan oluşturuyorum.
    {
        if (!AcikMi)
        {
            Console.WriteLine("Cihaz zaten kapali durumda!");
            return;//son derste üstünde durduğumuz clean code yapısı ile yazıldı.Aksi takdirde else yapısı da koda dahil edilecekti.
        }
        AcikMi = false;
        Console.WriteLine("Cihaz kapatiliyor!!");
    }

    public void BilgileriGoster()//bilgileri gösterme fonksiyonunu tanımlıyorum.Aynı zamanda menü görüntüsünü de oluşturuyorum.
    {
        Console.WriteLine("-------------");
        Console.WriteLine("\n");
        Console.WriteLine("Kimlik:" + Kimlik);
        Console.WriteLine("Ad:" +  Ad);
        Console.WriteLine("Tur:" +  Tur);
        Console.WriteLine("Cihaz durumu:" + (AcikMi ? "Acik" : "Kapali"));//son derste üstünde durduğumuz clean code ile yazıldı.
        //aksi takdirde if else yapısı ile de yazabilirdik.

    }
}

//bu kod özelinde switch-case yapısı içerisinde kodları yığmak istemedim.Çünkü o şekilde olunca main fonksiyonu çok uzun oluyor.
//Bu da istediğimiz bir durum değil.O sebepten sadece gerekli fonksiyonları yazıp tanımlamaları main dışarısında program içerisinde yapıyorum.
class Program
{
    static void Main(string[] args)
    {

        //Cihazlarımızı saklamak için list tutmamız isteniyor.O sebepten ötürü lst ile tutuyoruz.
        List<Cihaz> cihazlar = new List<Cihaz>();

        //While döngüsü kullanmayı seçtiğim için kullanıcının seçim değerini ilk olarak boş bir değere atıyorum.
        //ilk kez çalışmasını sağlamak için.

        int secim = 0;

        // 5 cikis oldugu icin secim 5 olmadigi surece dongu devam etmeli
        while (secim != 5)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("");
            Console.WriteLine("1- Cihazları Listeleme: ");
            Console.WriteLine("2- Yeni Cihaz Ekleme");
            Console.WriteLine("3- Cihaz Acma: ");
            Console.WriteLine("4- Cihaz Kapama: ");
            Console.WriteLine("5- Programdan Cikis: \n");
            Console.Write("Seciminiz: ");//alt satıra geçip görüntü bozulmasın diye mevcut satırdan devam ediyorum.

            //bizden istenen try-catch yapısını oluşturuyoruz.Gerekli kontrolleri sağlayıp programın çökmesini engelleyip anlamlı çıktılar için.

            try
            {
                secim = int.Parse(Console.ReadLine());//kullanıcıdan seçim değerini alıp bir değişkene atıyorum.
                switch (secim)//biraz sonra tanımlayacağımız fonksiyonları burada çağırıyoruz.
                {
                    case 1:
                        CihazlariListeleme(cihazlar);
                        break;

                    case 2:
                        CihazEkleme(cihazlar);
                        break;

                    case 3:
                        CihazAcma(cihazlar);
                        break;

                    case 4:
                        CihazKapatma(cihazlar);
                        break;

                    case 5:
                        Console.WriteLine("Programdan cikiliyor...");
                        break;

                    default:
                        Console.WriteLine("Gecersiz secim! 1-5 arasi bir deger giriniz.");//menü içerisindeki sayılar dışındaki değerler için gerekli çıktı.
                        break;
                        //Yukarıda da göründüğü gibi switch case yapısı basit olması istendiğinden hem çok temiz görünüyor.
                        //Hem de clean code prensiplerine uygun olmuş oluyor.
                        //Aksi durumlarda kod çok karışık olacaktı ve main çok uzun olacaktı.
                        //Şu anki durumunda her şey olması gerektiği gibi.
                }
            }
            catch (FormatException)//Eğer sayı dışında bir değer girilirse try-catch mekanizması programı kapatmak yerine çıktı verecek ve program hata vermeyecek.
            {
                Console.WriteLine("Gecersiz giris! Lutfen sayi giriniz.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
    }

    static void CihazlariListeleme(List<Cihaz> cihazlar)//bizden istenen şekilde cihazları liste olarak tutuyorum.
    {
        if (cihazlar.Count == 0) //eğer cihaz sayısı 0 ise gerekli çıktıyı veriyorum.
        {
            Console.WriteLine("Kayitli cihaz bulunmamaktadir.");
            return;
        }

        foreach (Cihaz cihaz in cihazlar)
        {
            cihaz.BilgileriGoster();//cihazları loop ile dolanarak bilgileri ekrana yazdırıyoruz.
        }
    }

    static void CihazEkleme(List<Cihaz> cihazlar)//yeni cihaz eklemek için fonksiyon tanımlıyorum.Listeye ekleme yapabilmek için cihaz listesini parametre olarak alıyorum.
    {
        Console.Write("Kimlik giriniz: ");//arayüz düzgün olsun diye alt satıra geçmiyorum write olarak bıraktım.
        int kimlik = int.Parse(Console.ReadLine());//yeni öğrendiğim kullanıcını girdisinin tür dönüşüm işlemini gerçekleştiriyorum.
        if (kimlik <= 0) //kimlik değişkeninin negatif olma durumunun kontrolünü sağlıyorum.
        {
            Console.WriteLine("Kimlik pozitif bir sayi olmalidir!");
            return;
        }
        //Eğer girilen değer sayı değilse FormatException oluşur ve Main'deki catch yakalar.
        // Ayni kimlige sahip ikinci bir cihaz eklenmesini engelliyoruz
        foreach (Cihaz c in cihazlar)
        {
            if (c.Kimlik == kimlik)//bizden istenen durumun kontrolünü sağlıyorum.
            {//eğer girilen kimliğe sahip bir cihaz varsa ikinciyi ekleme  işlemini engelliyorum.
                Console.WriteLine("Bu kimlige sahip bir cihaz zaten mevcut!");
                return;
            }
        }

        Console.Write("Ad giriniz: ");
        string ad = Console.ReadLine();//ad çıktısı için kullanıcıdan bir ad alıyorum.

        Console.WriteLine("Tur seciniz\n 0-> GuvenlikSistemi");//eklenen cihazın türü kullanıcıya sunuuluyor.
        Console.WriteLine("1-> Lamba ");
        Console.WriteLine("2-> Klima");
        Console.Write("Seciminiz: ");
        int turSecim = int.Parse(Console.ReadLine());//girilen değeri int olarak yukarıda yaptığım gibi çeviriyorum.

        if (!Enum.IsDefined(typeof(CihazTuru), turSecim))
        {//girilen sayı CihazTuru enum'ında tanımlı değilse (örneğin 7) koşul doğru olur.
            Console.WriteLine("Gecersiz cihaz turu!");//çıktımızı verdik.
            return;
        }

        CihazTuru tur = (CihazTuru)turSecim;
        //Casting işlemi ile int olan tür numarasını CihazTuru enum tipine çeviriyorum.
        Cihaz yeniCihaz = new Cihaz(kimlik, ad, tur);
        //Aldığım bilgilerle yeni bir Cihaz nesnesi oluşturuyorum.Cihaz başlangıçta kapalı olur.Ad boşsa constructor hata fırlatır.
        cihazlar.Add(yeniCihaz);//listeye ekliyorum.
        Console.WriteLine("Cihaz basariyla eklendi.");
    }

    static void CihazAcma(List<Cihaz> cihazlar)//listeyi parametre verek açma fonksiyonunu hazırlıyorum.
    {
        Console.Write("Acilacak cihaz kimligini giriniz: ");
        int kimlik = int.Parse(Console.ReadLine());//Tekrardan gerekli tür dönüşümünün sağlıyorum.

        foreach (Cihaz cihaz in cihazlar)//loop ile cihazları dönüp  idsi girilen cihazı bulup önceden tanımladığım cihaz açma fonksiyonunu çağırıyorum.
        {
            if (cihaz.Kimlik == kimlik)
            {
                cihaz.Ac();
                return;
            }
        }

        // Olmayan cihaz icin hata mesajı veriyorum.
        Console.WriteLine("Girilen kimlige sahip bir cihaz bulunamadi!");
    }

    static void CihazKapatma(List<Cihaz> cihazlar)//açma'da kullandığımız mantığın aynısını kapatmak için de kuruyoruz.
    {
        Console.Write("Kapatilacak cihaz kimligini giriniz: ");
        int kimlik = int.Parse(Console.ReadLine());//Tekrar tür dönüşümü.

        foreach (Cihaz cihaz in cihazlar)
        {
            if (cihaz.Kimlik == kimlik)
            {
                cihaz.Kapat();
                return;
            }
        }

        // Olmayan cihaz icin hata mesajı veriyoruz ve kodu bitiriyoruz.
        Console.WriteLine("Girilen kimlige sahip bir cihaz bulunamadi!");
    }
}
