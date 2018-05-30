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
                Console.WriteLine("4 - Add a mark grade from a particular student and subject");
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
                Subject subject = new Subject();
                Mark mark = new Mark();

                switch (opcija)
                {
                    #region 1 list of Student
                    case "1":
                        try
                        {
                            int num = 1;

                            string query = string.Format(" select * from tblStudent ");

                            sqlConn.Open();

                            SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
                            SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                            Console.WriteLine("-------------------------------------------------------");
                            Console.WriteLine("First and last name of students:");
                            Console.WriteLine("-------------------------------------------------------");
                            while (sqlReader.Read())
                            {
                                Console.WriteLine(num + ": " + sqlReader["StudentName"] + " " + sqlReader["SurName"]);
                                num++;
                            }
                            Console.WriteLine("-------------------------------------------------------");

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
                    #endregion

                    #region 2 list of Subject
                    case "2":
                        try
                        {
                            int num = 1;

                            string query = string.Format(" select * from tblSubject ");

                            sqlConn.Open();

                            SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
                            SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                            Console.WriteLine("-------------------------------------------------------");
                            Console.WriteLine("Name of subjects:");
                            Console.WriteLine("-------------------------------------------------------");
                            while (sqlReader.Read())
                            {
                                Console.WriteLine(num + ": " + sqlReader["SubjectName"]);
                                num++;
                            }
                            Console.WriteLine("-------------------------------------------------------");

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
                    #endregion

                    #region 3 Student name,surname, Subject name, Mark from Student name and surname
                    case "3":

                        try
                        {
                            int num = 1;

                            Console.WriteLine("Please enter the name for search:");
                            string nameForSearch = Console.ReadLine();

                            Console.WriteLine("Please enter the surname for search:");
                            string surnameForSearch = Console.ReadLine();

                            string query = string.Format("select tblStudent.StudentName,tblStudent.SurName,tblSubject.SubjectName,tblMark.Mark FROM((tblMark INNER JOIN tblStudent ON tblMark.StudentID=tblStudent.StudentID) INNER JOIN tblSubject ON tblMark.SubjectID=tblSubject.SubjectID) where StudentName='{0}' and SurName='{1}';", nameForSearch, surnameForSearch);

                            sqlConn.Open();

                            SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
                            SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                            if (!sqlReader.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular student!");
                            }
                            else
                            {

                                Console.WriteLine("-------------------------------------------------------");
                                Console.WriteLine("Student name, surname, subject and mark:");
                                Console.WriteLine("-------------------------------------------------------");
                                while (sqlReader.Read())
                                {
                                    Console.WriteLine(num + ": " + sqlReader["StudentName"] + "  " + sqlReader["SurName"] + ":  " + sqlReader["SubjectName"] + " - " + sqlReader["Mark"]);
                                    num++;
                                }
                                Console.WriteLine("-------------------------------------------------------");
                            }

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
                    #endregion

                    #region 4 Add mark grade from particular student and subject
                    case "4":
                        try
                        {
                            int studentID = 0;
                            int subjectID = 0;

                            Console.WriteLine("Enter the name of the student:");
                            student.Name = Console.ReadLine();
                            Console.WriteLine("Enter the student's last name:");
                            student.Surname = Console.ReadLine();
                            Console.WriteLine("Enter the name of subject");
                            subject.NameOfSubject = Console.ReadLine();

                            string query1 = string.Format("select tblStudent.StudentID from tblStudent where StudentName='{0}' and SurName='{1}';", student.Name, student.Surname);
                            string query2 = string.Format("select tblSubject.SubjectID from tblSubject where SubjectName='{0}';", subject.NameOfSubject);

                            sqlConn.Open();

                            SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConn);
                            SqlDataReader sqlReader1 = sqlCommand1.ExecuteReader();
                            //sqlCommand1.ExecuteNonQuery();


                            if (!sqlReader1.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular student!");
                                break;
                            }
                            else
                            {
                                // Call Read before accessing data.
                                while (sqlReader1.Read())
                                {
                                    studentID = Convert.ToInt32(sqlReader1["StudentID"]);
                                }

                                // Call Close when done reading.
                                sqlReader1.Close();
                            }

                            SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConn);
                            SqlDataReader sqlReader2 = sqlCommand2.ExecuteReader();
                            //sqlCommand2.ExecuteNonQuery();

                            if (!sqlReader2.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular subject!");
                                break;
                            }
                            else
                            {
                                // Call Read before accessing data.
                                while (sqlReader2.Read())
                                {
                                    subjectID = Convert.ToInt32(sqlReader2["SubjectID"]);
                                }

                                // Call Close when done reading.
                                sqlReader2.Close();
                            }

                            Console.WriteLine("Enter the mark number of subject for particular student:");
                            mark.Evaluation = Convert.ToInt32(Console.ReadLine());
                            if (mark.Evaluation > 10 && mark.Evaluation < 6)
                            {
                                Console.WriteLine("Mark number must be beetwen 6 and 10! Please repeat!");
                                return;
                            }
                            else
                            {
                                string query3 = string.Format("Insert into tblMark Values('{0}','{1}','{2}');", mark.Evaluation, studentID, subjectID);
                                SqlCommand sqlCommand3 = new SqlCommand(query3, sqlConn);
                                sqlCommand3.ExecuteNonQuery();

                                Console.WriteLine("The mark grade of student has successfully added!");
                            }

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
                    #endregion

                    #region 5 add Student
                    case "5":

                        try
                        {
                            Console.WriteLine("Enter the name of the student:");
                            student.Name = Console.ReadLine();
                            Console.WriteLine("Enter the student's last name:");
                            student.Surname = Console.ReadLine();

                            string query = string.Format("Insert into tblStudent Values('{0}','{1}');", student.Name, student.Surname);

                            sqlConn.Open();

                            SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
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
                    #endregion

                    #region 6 add Subject
                    case "6":

                        try
                        {
                            Console.WriteLine("Enter the name of the subject:");
                            subject.NameOfSubject = Console.ReadLine();

                            string query = string.Format("Insert into tblSubject Values('{0}');", subject.NameOfSubject);
                            sqlConn.Open();

                            SqlCommand sqlCommand = new SqlCommand(query, sqlConn);
                            sqlCommand.ExecuteNonQuery();

                            Console.WriteLine("The subject has successfully added!");
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
                    #endregion

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

            //Console.ReadLine();
        }
    }
}
