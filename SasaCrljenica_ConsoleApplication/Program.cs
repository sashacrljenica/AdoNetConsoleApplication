using SasaCrljenica_ConsoleApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetConsoleApplication
{
    class Program
    {
        private static string opcija;

        static void Main(string[] args)
        {
            #region ConnectionString

            string connString = SasaCrljenica_ConsoleApplication.Properties.Settings.Default.TriTabeleConnectionString;
            SqlConnection sqlConn = new SqlConnection(connString);

            #endregion

            do
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("-----------------Odaberite opciju----------------------------------");
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("1 - Ucitaj podatke o studentima");
                Console.WriteLine("2 - Ucitaj podatke o predmetima");
                Console.WriteLine("3 - Ucitaj podatke koje ocene ima pojedini student");
                Console.WriteLine("4 - Dodaj ocenu iz odredjenog predmeta, za pojedinog studenta");
                Console.WriteLine("5 - Dodaj novog studenta");
                Console.WriteLine("6 - Dodaj novi predmet");
                Console.WriteLine("7 - Koriguj podatke o studentu");
                Console.WriteLine("8 - Koriguj podatke o predmetu");
                Console.WriteLine("9 - Obrisi studenta");
                Console.WriteLine("10 - Obrisi predmet");
                Console.WriteLine("0 - Izadji iz programa");
                Console.WriteLine("-------------------------------------------------------------------");

                opcija = Console.ReadLine();

                Student student = new Student();

                switch (opcija)
                {
                    case "1": break;
                    case "2": break;
                    case "3": break;
                    case "4": break;
                    case "5":
                        Console.WriteLine("Unesite ime studenta:");
                        student.Name = Console.ReadLine();
                        Console.WriteLine("Unesite prezime studenta");
                        student.Surname = Console.ReadLine();

                        string query = string.Format("Insert into tblStudent Values('{0}','{1}');", student.Name, student.Surname);
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConn);

                        try
                        {
                            sqlConn.Open();
                            sqlCommand.ExecuteNonQuery();
                            Console.WriteLine("Student je uspesno dodat!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        finally
                        {
                            sqlConn.Close();
                        }

                        break;
                    case "6": break;
                    case "7": break;
                    case "8": break;
                    case "9": break;
                    case "10": break;
                    case "0": break;

                    default:
                        Console.WriteLine("Pogresna opcija!");
                        break;
                }


            } while (opcija != "0");

            Console.ReadLine();
        }
    }
}
