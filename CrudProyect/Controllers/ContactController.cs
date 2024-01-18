using CrudProyect.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudProyect.Controllers
{
    public class ContactController : Controller
    {
        private static string connection = ConfigurationManager.ConnectionStrings["string"].ToString();

        private static List<Contact> olist = new List<Contact>();

        // GET: Contact
        public ActionResult Index()
        {
            olist = new List<Contact>();

            using (SqlConnection oconnection = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACT", oconnection);
                cmd.CommandType = CommandType.Text;
                oconnection.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        Contact newContact = new Contact();

                        newContact.IdContact = Convert.ToInt32(dr["IdContact"]);
                        newContact.Firstname = dr["Firstname"].ToString();
                        newContact.Lastname = dr["Lastname"].ToString();
                        newContact.Phone = dr["Phone"].ToString();
                        newContact.Email = dr["Email"].ToString();

                        olist.Add(newContact);

                    }
                }
            }
            return View(olist);
        }
    }
}