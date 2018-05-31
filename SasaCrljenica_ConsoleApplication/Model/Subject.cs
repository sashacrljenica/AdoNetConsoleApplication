using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SasaCrljenica_ConsoleApplication.Model
{
    class Subject
    {
        private int subjectID;
        private string nameOfSubject;

        public string NameOfSubject
        {
            get
            {
                return nameOfSubject;
            }

            set
            {
                nameOfSubject = value;
            }
        }

        public int SubjectID
        {
            get
            {
                return subjectID;
            }

            set
            {
                subjectID = value;
            }
        }
    }
}
