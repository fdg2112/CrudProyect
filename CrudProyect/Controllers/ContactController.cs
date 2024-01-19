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
using System.Collections;

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

        [HttpGet]
        public ActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(Contact ocontact)
        {
            using (SqlConnection oconnection = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_Register", oconnection);
                cmd.Parameters.AddWithValue("Firstname", ocontact.Firstname);
                cmd.Parameters.AddWithValue("Lastname", ocontact.Lastname);
                cmd.Parameters.AddWithValue("Phone", ocontact.Phone);
                cmd.Parameters.AddWithValue("Email", ocontact.Email);
                cmd.CommandType = CommandType.StoredProcedure;
                oconnection.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Contact");
        }

        [HttpGet]
        public ActionResult Edit(int? idContact)
        {
            if (idContact == null)
                return RedirectToAction("Index", "Contact");
            Contact ocontact = olist.Where(c => c.IdContact == idContact).FirstOrDefault();
            return View(ocontact);
        }

        [HttpPost]
        public ActionResult Edit(Contact ocontact)
        {
            using (SqlConnection oconnection = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_Edit", oconnection);
                cmd.Parameters.AddWithValue("IdContact", ocontact.IdContact);
                cmd.Parameters.AddWithValue("Firstname", ocontact.Firstname);
                cmd.Parameters.AddWithValue("Lastname", ocontact.Lastname);
                cmd.Parameters.AddWithValue("Phone", ocontact.Phone);
                cmd.Parameters.AddWithValue("Email", ocontact.Email);
                cmd.CommandType = CommandType.StoredProcedure;
                oconnection.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Contact");
        }

        [HttpGet]
        public ActionResult Delete(int? idContact)
        {
            if (idContact == null)
                return RedirectToAction("Index", "Contact");
            Contact oContact = olist.Where(c => c.IdContact == idContact).FirstOrDefault();
            return View(oContact);
        }

        [HttpPost]
        public ActionResult Delete(string IdContact)
        {
            using (SqlConnection oconnection = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_Delete", oconnection);
                cmd.Parameters.AddWithValue("IdContact", IdContact);
                cmd.CommandType = CommandType.StoredProcedure;
                oconnection.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Contact");
        }
    }
}