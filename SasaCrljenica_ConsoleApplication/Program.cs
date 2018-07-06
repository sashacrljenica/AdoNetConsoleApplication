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
        private static string option;

        static void Main(string[] args)
        {
            #region Connection String

            string connString = SasaCrljenica_ConsoleApplication.Properties.Settings.Default.TriTabeleConnectionString;
            SqlConnection sqlConn = new SqlConnection(connString);

            #endregion

            do
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("-----------------Select an option----------------------------------");
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine(" 1 - Load Student information");
                Console.WriteLine(" 2 - Load name of Subject");
                Console.WriteLine(" 3 - Load the data of Student that have Mark of ratings\n for a particular Subject");
                Console.WriteLine(" 4 - Add a Mark grade for a particular student and subject");
                Console.WriteLine(" 5 - Add a new Student");
                Console.WriteLine(" 6 - Add new Subject");
                Console.WriteLine(" 7 - Update Student information");
                Console.WriteLine(" 8 - Update Subject information");
                Console.WriteLine(" 9 - Delete Student");
                Console.WriteLine("10 - Delete Subject");
                Console.WriteLine(" 0 - Exit from application");
                Console.WriteLine("-------------------------------------------------------------------");

                option = Console.ReadLine();

                Student student = new Student();
                Subject subject = new Subject();
                Mark mark = new Mark();

                switch (option)
                {
                    #region 1 List of Students
                    case "1":
                        try
                        {
                            sqlConn.Open();

                            int num = 1;

                            string query11 = string.Format("select * from tblStudent;");

                            SqlCommand sqlCommand11 = new SqlCommand(query11, sqlConn);
                            SqlDataReader sqlReader11 = sqlCommand11.ExecuteReader();

                            if (sqlReader11.HasRows)
                            {
                                Console.WriteLine("-------------------------------------------------------");
                                Console.WriteLine("First and last name of students:");
                                Console.WriteLine("-------------------------------------------------------");
                                while (sqlReader11.Read())
                                {
                                    Console.WriteLine(num + ": " + sqlReader11["StudentName"] + " " + sqlReader11["SurName"]);
                                    num++;
                                }
                                Console.WriteLine("-------------------------------------------------------");
                                sqlReader11.Close();
                            }
                            else
                            {
                                Console.WriteLine("Data for Students is empty!");
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

                    #region 2 List of Subjects
                    case "2":
                        try
                        {
                            sqlConn.Open();

                            int num = 1;

                            string query21 = string.Format("select * from tblSubject;");

                            SqlCommand sqlCommand21 = new SqlCommand(query21, sqlConn);
                            SqlDataReader sqlReader21 = sqlCommand21.ExecuteReader();

                            if (sqlReader21.HasRows)
                            {
                                Console.WriteLine("-------------------------------------------------------");
                                Console.WriteLine("Name of subjects:");
                                Console.WriteLine("-------------------------------------------------------");
                                while (sqlReader21.Read())
                                {
                                    Console.WriteLine(num + ": " + sqlReader21["SubjectName"]);
                                    num++;
                                }
                                Console.WriteLine("------------------------------------------------------");
                                sqlReader21.Close();
                            }
                            else
                            {
                                Console.WriteLine("Empty data for table Subject!");
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

                    #region 3 View Student name,surname,subject name and Mark from Student name and surname
                    case "3":

                        Console.WriteLine("Please enter the name for search:");
                        string nameForSearch = Console.ReadLine();

                        Console.WriteLine("Please enter the surname for search:");
                        string surnameForSearch = Console.ReadLine();

                        try
                        {
                            sqlConn.Open();

                            int num = 1;

                            string query31 = string.Format("select tblStudent.StudentName,tblStudent.SurName,tblSubject.SubjectName,tblMark.Mark FROM((tblMark INNER JOIN tblStudent ON tblMark.StudentID=tblStudent.StudentID) INNER JOIN tblSubject ON tblMark.SubjectID=tblSubject.SubjectID) where StudentName='{0}' and SurName='{1}';", nameForSearch, surnameForSearch);

                            SqlCommand sqlCommand31 = new SqlCommand(query31, sqlConn);
                            SqlDataReader sqlReader31 = sqlCommand31.ExecuteReader();

                            if (!sqlReader31.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular student!");
                                Console.WriteLine("---------------------------------------------------");
                            }
                            else
                            {
                                Console.WriteLine("-------------------------------------------------------");
                                Console.WriteLine("Student name, surname, subject and mark:");
                                Console.WriteLine("-------------------------------------------------------");
                                while (sqlReader31.Read())
                                {
                                    Console.WriteLine(num + ": " + sqlReader31["StudentName"] + "  " + sqlReader31["SurName"] + ":  " + sqlReader31["SubjectName"] + " - " + sqlReader31["Mark"]);
                                    num++;
                                }
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
                        Console.WriteLine("Enter the name of the student:");
                        student.Name = Console.ReadLine();
                        Console.WriteLine("Enter the student's last name:");
                        student.Surname = Console.ReadLine();
                        Console.WriteLine("Enter the name of subject");
                        subject.NameOfSubject = Console.ReadLine();

                        try
                        {
                            string query41 = string.Format("select tblStudent.StudentID from tblStudent where StudentName='{0}' and SurName='{1}';", student.Name, student.Surname);
                            string query42 = string.Format("select tblSubject.SubjectID from tblSubject where SubjectName='{0}';", subject.NameOfSubject);

                            sqlConn.Open();

                            SqlCommand sqlCommand41 = new SqlCommand(query41, sqlConn);
                            SqlDataReader sqlReader41 = sqlCommand41.ExecuteReader();

                            if (!sqlReader41.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular student!");
                                Console.WriteLine("---------------------------------------------------");
                                break;
                            }
                            else
                            {
                                // Call Read before accessing data.
                                if (sqlReader41.Read())
                                {
                                    student.StudentID = Convert.ToInt32(sqlReader41["StudentID"]);
                                }

                                // Call Close when done reading.
                                sqlReader41.Close();
                            }

                            SqlCommand sqlCommand42 = new SqlCommand(query42, sqlConn);
                            SqlDataReader sqlReader42 = sqlCommand42.ExecuteReader();

                            if (!sqlReader42.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular subject!");
                                Console.WriteLine("---------------------------------------------------");
                                break;
                            }
                            else
                            {
                                // Call Read before accessing data.
                                if (sqlReader42.Read())
                                {
                                    subject.SubjectID = Convert.ToInt32(sqlReader42["SubjectID"]);
                                }

                                // Call Close when done reading.
                                sqlReader42.Close();
                            }

                            Console.WriteLine("Enter the mark number of subject for particular student:");
                            mark.Evaluation = Convert.ToInt32(Console.ReadLine());

                            if (mark.Evaluation > 10 || mark.Evaluation < 6)
                            {
                                Console.WriteLine("Mark number must be beetwen 6 and 10! Please repeat again!");
                            }
                            else
                            {
                                string query43 = string.Format("Insert into tblMark Values('{0}','{1}','{2}');", mark.Evaluation, student.StudentID, subject.SubjectID);
                                SqlCommand sqlCommand43 = new SqlCommand(query43, sqlConn);
                                sqlCommand43.ExecuteNonQuery();

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

                    #region 5 Add Student
                    case "5":
                        Console.WriteLine("Enter the name of the student:");
                        student.Name = Console.ReadLine();
                        Console.WriteLine("Enter the student's last name:");
                        student.Surname = Console.ReadLine();

                        try
                        {
                            sqlConn.Open();

                            string query51 = string.Format("select * from tblStudent where StudentName='{0}' and SurName='{1}';", student.Name, student.Surname);

                            SqlCommand sqlCommand51 = new SqlCommand(query51, sqlConn);
                            SqlDataReader sqlReader51 = sqlCommand51.ExecuteReader();

                            if (!sqlReader51.HasRows)
                            {
                                sqlReader51.Close();

                                string query52 = string.Format("Insert into tblStudent Values('{0}','{1}');", student.Name, student.Surname);

                                SqlCommand sqlCommand52 = new SqlCommand(query52, sqlConn);
                                sqlCommand52.ExecuteNonQuery();

                                Console.WriteLine("The student has successfully added!");
                            }
                            else
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("particular student {0} {1} already exist!", student.Name, student.Surname);
                                Console.WriteLine("Please repeat procedure!");
                                Console.WriteLine("---------------------------------------------------");
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

                    #region 6 Add Subject
                    case "6":

                        Console.WriteLine("Enter the name of the subject:");
                        subject.NameOfSubject = Console.ReadLine();

                        try
                        {
                            sqlConn.Open();

                            string query61 = string.Format("select * from tblSubject where SubjectName='{0}';", subject.NameOfSubject);

                            SqlCommand sqlCommand61 = new SqlCommand(query61, sqlConn);
                            SqlDataReader sqlReader61 = sqlCommand61.ExecuteReader();

                            if (!sqlReader61.HasRows)
                            {

                                string query62 = string.Format("Insert into tblSubject Values('{0}');", subject.NameOfSubject);
                                sqlConn.Open();

                                SqlCommand sqlCommand62 = new SqlCommand(query62, sqlConn);
                                sqlCommand62.ExecuteNonQuery();

                                Console.WriteLine("The subject has successfully added!");
                                sqlReader61.Close();
                            }
                            else
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("particular subject {0} already exist!", subject.NameOfSubject);
                                Console.WriteLine("Please repeat procedure!");
                                Console.WriteLine("---------------------------------------------------");
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

                    #region 7 Update student information
                    case "7":

                        Console.WriteLine("Update student information");
                        Console.WriteLine("Please enter old student name");
                        string oldName = Console.ReadLine();
                        Console.WriteLine("Please enter old student surname:");
                        string oldSurname = Console.ReadLine();

                        try
                        {
                            string query71 = string.Format(" select * from tblStudent where StudentName='{0}' and SurName='{1}';", oldName, oldSurname);

                            sqlConn.Open();

                            SqlCommand sqlCommand71 = new SqlCommand(query71, sqlConn);
                            SqlDataReader sqlReader71 = sqlCommand71.ExecuteReader();

                            if (!sqlReader71.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular student!");
                                Console.WriteLine("---------------------------------------------------");
                            }
                            else
                            {
                                sqlReader71.Close();

                                Console.WriteLine("Please enter new student name:");
                                string newName = Console.ReadLine();
                                Console.WriteLine("Please enter new student surname:");
                                string newSurname = Console.ReadLine();

                                string query72 = string.Format("select tblStudent.StudentID from tblStudent where StudentName='{0}' and SurName='{1}';", oldName, oldSurname);

                                SqlCommand sqlCommand72 = new SqlCommand(query72, sqlConn);
                                SqlDataReader sqlReader72 = sqlCommand72.ExecuteReader();

                                while (sqlReader72.Read())
                                {
                                    student.StudentID = Convert.ToInt32(sqlReader72["StudentID"]);
                                }

                                sqlReader72.Close();

                                string query73 = string.Format("Update tblStudent set StudentName='{0}', SurName='{1}' where StudentID='{2}';", newName, newSurname, student.StudentID);
                                SqlCommand sqlCommand73 = new SqlCommand(query73, sqlConn);
                                sqlCommand73.ExecuteNonQuery();

                                Console.WriteLine();
                                Console.WriteLine("You are successfully updated student name {0} and surname {1} with {2} and {3}!", oldName, oldSurname, newName, newSurname);
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

                    #region 8 update Subject information
                    case "8":
                        Console.WriteLine("Update subject information");
                        Console.WriteLine("Please enter old subject name");
                        string oldNameSubject = Console.ReadLine();

                        try
                        {
                            string query81 = string.Format(" select * from tblSubject where SubjectName='{0}';", oldNameSubject);

                            sqlConn.Open();

                            SqlCommand sqlCommand81 = new SqlCommand(query81, sqlConn);
                            SqlDataReader sqlReader81 = sqlCommand81.ExecuteReader();

                            if (!sqlReader81.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular subject!");
                                Console.WriteLine("---------------------------------------------------");
                            }
                            else
                            {
                                sqlReader81.Close();

                                Console.WriteLine("Please enter new subject name:");
                                string newNameSubject = Console.ReadLine();

                                string query82 = string.Format("select tblSubject.SubjectID from tblSubject where SubjectName='{0}';", oldNameSubject);

                                SqlCommand sqlCommand82 = new SqlCommand(query82, sqlConn);
                                SqlDataReader sqlReader82 = sqlCommand82.ExecuteReader();

                                while (sqlReader82.Read())
                                {
                                    subject.SubjectID = Convert.ToInt32(sqlReader82["SubjectID"]);
                                }

                                sqlReader82.Close();

                                string query83 = string.Format("Update tblSubject set SubjectName='{0}' where SubjectID='{1}';", newNameSubject, subject.SubjectID);
                                SqlCommand sqlCommand83 = new SqlCommand(query83, sqlConn);
                                sqlCommand83.ExecuteNonQuery();

                                Console.WriteLine();
                                Console.WriteLine("You are successfully updated subject name {0} with {1}!", oldNameSubject, newNameSubject);
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

                    #region 9 Delete Student
                    case "9":
                        Console.WriteLine("Enter the name of Student which want to delete:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter the surname of Student which want to delete:");
                        string surname = Console.ReadLine();

                        try
                        {
                            sqlConn.Open();

                            string query90 = string.Format("select * from tblStudent where StudentName='{0}' and SurName='{1}';", name, surname);
                            SqlCommand sqlCommand90 = new SqlCommand(query90, sqlConn);

                            SqlDataReader sqlReader90 = sqlCommand90.ExecuteReader();

                            if (!sqlReader90.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular student!");
                                Console.WriteLine("Please repeat procedure!");
                                Console.WriteLine("---------------------------------------------------");
                                break;
                            }
                            else
                            {
                                sqlReader90.Close();

                                //string query91 = string.Format("select tblStudent.StudentID from tblStudent where StudentName='{0}' and SurName='{1}';", name, surname);
                                //SqlCommand sqlCommand91 = new SqlCommand(query91, sqlConn);

                                //SqlDataReader sqlReader91 = sqlCommand91.ExecuteReader();
                                //while (sqlReader91.Read())
                                //{
                                //    studentID = Convert.ToInt32(sqlReader91["StudentID"]);
                                //}
                                //sqlReader91.Close();

                                //string query92 = string.Format("delete from tblStudent where StudentID='{0}';", studentID);

                                string query92 = string.Format("delete from tblStudent where StudentName='{0}' and SurName='{1}';", name, surname);
                                SqlCommand sqlCommand92 = new SqlCommand(query92, sqlConn);

                                sqlCommand92.ExecuteNonQuery();

                                Console.WriteLine();
                                Console.WriteLine("Student {0} {1}, deleted successfully!", name, surname);
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

                    #region 10 Delete Subject
                    case "10":
                        Console.WriteLine("Enter the name of Subject which want to delete:");
                        string nameOfSubject = Console.ReadLine();

                        try
                        {
                            sqlConn.Open();

                            string query101 = string.Format("select * from tblSubject where SubjectName='{0}';", nameOfSubject);
                            SqlCommand sqlCommand101 = new SqlCommand(query101, sqlConn);

                            SqlDataReader sqlReader101 = sqlCommand101.ExecuteReader();

                            if (!sqlReader101.HasRows)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("No data for particular subject!");
                                Console.WriteLine("Please repeat procedure!");
                                Console.WriteLine("---------------------------------------------------");
                                break;
                            }
                            else
                            {
                                string query102 = string.Format("delete from tblSubject where SubjectName='{0}';", nameOfSubject);
                                SqlCommand sqlCommand102 = new SqlCommand(query102, sqlConn);

                                sqlCommand102.ExecuteNonQuery();

                                Console.WriteLine();
                                Console.WriteLine("Subject {0}, deleted successfully!", nameOfSubject);
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

                    case "0": break;

                    default:
                        Console.WriteLine("Wrong option!");
                        break;
                }

            } while (option != "0");

            //Console.ReadLine();
        }
    }
}
