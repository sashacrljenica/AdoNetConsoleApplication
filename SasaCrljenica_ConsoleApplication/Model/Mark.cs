﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SasaCrljenica_ConsoleApplication.Model
{
    class Mark
    {
        private int evaluation;
        private Student student;
        private Subject subject;

        public int Evaluation
        {
            get
            {
                return evaluation;
            }

            set
            {
                evaluation = value;
            }
        }

        internal Student Student
        {
            get
            {
                return student;
            }

            set
            {
                student = value;
            }
        }

        internal Subject Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }
    }
}
