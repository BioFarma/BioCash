using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BioTemplate.Controller.Function
{
    public class NominalCurrencyReference
    {
    }

    public class CurrencyConverter
    {
        public static String SetNominalToRupiahFormat(string cvalue)
        {
            string result = null;
            int counter = 0;
            bool flag = false;
            bool negatif = false;

            if (cvalue.Contains('-')) {
                cvalue = cvalue.Remove(0, 1);
                negatif = true;
            }

            foreach (char c in cvalue) {
                if (flag == false)
                {
                    if (counter < cvalue.Length % 3)
                    {
                        result += c;
                        counter++;
                    }
                    else
                    {
                        if (cvalue.Length % 3 != 0)
                        {
                            result += ".";
                        }
                        flag = true;
                        result += c;
                        counter = 1;
                    }
                }
                else
                {
                    if (counter < 3)
                    {
                        result += c;
                        counter++;
                    }
                    else
                    {

                        result += ".";
                        result += c;
                        counter = 1;
                    }
                }
            }

            if (negatif) return ("Rp -" + result + ",00");
            return ("Rp " + result + ",00");
        }

    }

    public class NominalConverter
    {
        private static String[] huruf = { "", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh", "Sebelas" };
        public static String BilanganToTerbilang(int satuan)
        {
            return Angka(satuan) + " Rupiah.";
        }

        private static String Angka(int satuan)
        {
            String result = null;

            if (satuan < 12)
                result += huruf[satuan];
            else if (satuan == 0)
                result += "Nol";
            else if (satuan < 20)
                result += Angka(satuan - 10) + " Belas";
            else if (satuan < 100)
                result += Angka(satuan / 10) + " Puluh " + Angka(satuan % 10);
            else if (satuan < 200)
                result += "Seratus " + Angka(satuan - 100);
            else if (satuan < 1000)
                result += Angka(satuan / 100) + " Ratus " + Angka(satuan % 100);
            else if (satuan < 2000)
                result += "Seribu " + Angka(satuan - 1000);
            else if (satuan < 1000000)
                result += Angka(satuan / 1000) + " Ribu " + Angka(satuan % 1000);
            else if (satuan < 1000000000)
                result += Angka(satuan / 1000000) + " Juta " + Angka(satuan % 1000000);
            else if (satuan >= 1000000000)
                result = "Angka terlalu besar, harus kurang dari 1 milyar!";

            return result;
        }
    }

}