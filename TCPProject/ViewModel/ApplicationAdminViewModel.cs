using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCPProject.Model;

namespace TCPProject.ViewModel
{
    public class ApplicationAdminViewModel
    {
        public Applications applications { get; set; }
        public int CountElementPending { get; set; }
    }
}
