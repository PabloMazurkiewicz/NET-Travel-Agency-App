﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Models.DataManager
{
    public class CustomerProfileManager
    {
        // TODO: Louise - get a customer's all previous bookings using id
        //public static List<> GetAllBookingsByCustomer(int custId) { }

        // Below is an example Louise can refer to ↓

        //public static List<Asset> GetAllByAssetType(int assetTypeId)
        //{
        //    var context = new AssetsContext();
        //    var assets = context.Assets.
        //        Include(asset => asset.AssetType).
        //        Where(ast => ast.Id == assetTypeId).ToList();

        //    return assets;
        //}

        /// <summary>
        /// Find a certain customer using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customers object.</returns>
        public static Customers Find(int id)
        {
            var context = new TravelExpertsContext();

            // find the domain entity with this context that has the same id as 
            // the entity passed
            var customer = context.Customers.
                Include(agt => agt.Agent).
                SingleOrDefault(ast => ast.CustomerId == id);

            return customer;

        }

        /// <summary>
        /// Add a new customer to database.
        /// </summary>
        /// <param name="newCust">Customers object need to be added.</param>
        /// <returns>A bool indicate if added successfully.</returns>
        public static async Task<bool> Add(Customers newCust)
        {
            bool isSucceed = false;
            var context = new TravelExpertsContext();
            context.Customers.Add(newCust);
            try
            {
                int i = await context.SaveChangesAsync();
                if (i > 0)
                    isSucceed = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return isSucceed;
        }


        public static async Task<Customers> CompareLogin(string username, string password)
        {

            var context = new TravelExpertsContext();
            // use username to find a customer, if cannot find one, return null
            var cust = context.Customers.SingleOrDefault(c => c.Username == username);
            if (cust == null)
                return null;
            // if find a customer, compare password
            if (cust.Password == password)
                return cust;
            else
                return null;
        }

    }
}
