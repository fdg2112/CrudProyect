using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudProyect.Models
{
    public class Contact
    {
        public int IdContact { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}