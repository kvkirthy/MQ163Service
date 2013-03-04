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

    }
}