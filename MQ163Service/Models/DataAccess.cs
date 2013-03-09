using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MQ163Service.Models
{
    public class DataAccess
    {
        public static List<Prospect> GetLeads()
        {
            var db = new MQ163DataContext();

            var prospects = from x in db.Prospects
                                where x.IsCustomer == false
                                select x;
            return prospects.ToList();
        }

        public static List<Prospect> GetCustomers()
        {
            var db = new MQ163DataContext();

            var prospects = from x in db.Prospects
                            where x.IsCustomer == true
                            select x;
            return prospects.ToList();
        }

        public static List<MerchandizeOffer> GetOffers()
        {
            var db = new MQ163DataContext();
            return db.MerchandizeOffers.ToList();
        }

        public static bool CreateLead(string fullName)
        {
            var db = new MQ163DataContext();
            var prospects = from x in db.Prospects
                            orderby x.Id descending
                            select x;
            int newId = (prospects != null && prospects.Count() > 0)? prospects.First().Id : 0;              
                            
            var newLead = new Prospect{
                Id = newId + 1,
                FullName = fullName,
                email = @" ",
                IsCustomer = false,
                Features = " ",
                Car=" "
            };

            db.Prospects.InsertOnSubmit(newLead);
            db.SubmitChanges();

            return true;
        }

    }
}