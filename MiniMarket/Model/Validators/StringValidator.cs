using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Model.Validators
{
    public  class StringValidator : Validator
    {
        public static string CzyPuste(string wartosc)
        {
            try
            {
                if (wartosc == null)
                {
                   return "Uzupełnij pola wymagane ";
                   
                }
            }
            catch (Exception)
            {


            };
            return null;

        }
        public static string CzyPusteCombo(string wartosc)
        {
            try
            {
                if (wartosc == string.Empty)
                {
                    return "Uzupełnij pola wymagane ";

                }
            }
            catch (Exception)
            {


            };
            return null;

        }
        public static string CzyDuzaLitera(string wartosc)
        {
                      
            try
            {
                if (!char.IsUpper(wartosc, 0))
                {
                    return "Zacznij od dużej litery ";

                }
            }
            catch (Exception)
            {


            };
            return null;

        }
        public static string CzyEmail(string wartosc)
        {

            try
            {
                if (wartosc!= null && !wartosc.Contains("@"))
                {
                    return "Nieprawidłowy adres email ";

                }
            }
            catch (Exception)
            {


            };
            return null;

        }
        public static string CzyNumer(string wartosc)
        {

            try
            {
                if (wartosc !=null)
                {
                    for (int i = 0; i < wartosc.Length; i++)
                    {
                        if (!char.IsDigit(wartosc, i))
                        {
                            return "Nieprawidłowy numer ";

                        }
                    }
                }
                
                                  
                
            }
            catch (Exception)
            {


            };
            return null;

        }
        public static string CzyLiczbaDodatnia(decimal? wartosc)
        {
            try
            {
                if (wartosc < 0)
                {
                    return "Wartość musi być dodatnia ";

                }
            }
            catch (Exception)
            {


            };
            return null;

        }
        

    }
}
