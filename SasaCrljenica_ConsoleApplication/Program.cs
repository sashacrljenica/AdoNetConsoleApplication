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
                Console.WriteLine("-----------------Select an option----------------------------------");
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("1 - Load student information");
                Console.WriteLine("2 - Load subject data");
                Console.WriteLine("3 - Load the data that the ratings have to a particular student");
                Console.WriteLine("4 - Add a grade from a particular subject to a particular student");
                Console.WriteLine("5 - Add a new student");
                Console.WriteLine("6 - Add new subject");
                Console.WriteLine("7 - Update student information");
                Console.WriteLine("8 - Update subject information");
                Console.WriteLine("9 - Delete student");
                Console.WriteLine("10 - Delete subject");
                Console.WriteLine("0 - Exit from application");
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
                        Console.WriteLine("Enter the name of the student:");
                        student.Name = Console.ReadLine();
                        Console.WriteLine("Enter the student's last name:");
                        student.Surname = Console.ReadLine();

                        string query = string.Format("Insert into tblStudent Values('{0}','{1}');", student.Name, student.Surname);
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConn);

                        try
                        {
                            sqlConn.Open();
                            sqlCommand.ExecuteNonQuery();
                            Console.WriteLine("The student has successfully added!");
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
                        Console.WriteLine("Wrong option!");
                        break;
                }


            } while (opcija != "0");

            Console.ReadLine();
        }
    }
}
