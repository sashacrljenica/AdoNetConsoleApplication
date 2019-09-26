using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SasaCrljenica_ConsoleApplication.Connection
{
    class ConnectionString
    {
        public static string computer = Environment.MachineName;
        public static string connString = @"Data Source=" + computer + @";Initial Catalog=TriTabele;Integrated Security=True";
    }
}
