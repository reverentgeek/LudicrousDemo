using System.Collections.Generic;
using LudicrousDemo.Common;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;

namespace LudicrousDemo.Services
{
    public class CustomerService : Service
    {
        public Customer Post(Customer customer)
        {
            var redis = Redis.As<Customer>();

            if (customer.Id == default(long))
                customer.Id = redis.GetNextSequence();

            redis.Store(customer);

            return customer;
        }

        public Customer Put(Customer customer)
        {
            return Post(customer);
        }

        public void Delete(Customer customer)
        {
            if (customer.Id == default (long)) return;
            Redis.As<Customer>().DeleteById(customer.Id);
        }

        public IList<Customer> Any(CustomerSearch search)
        {
            // Example retrieving using OrmLite
            //if (search.Ids != null && search.Ids.Length > 0)
            //    return Db.GetByIds<Customer>(search.Ids);

            // Example retrieving using Redis
            if (search.Ids != null && search.Ids.Length > 0)
                return Redis.As<Customer>().GetByIds(search.Ids);

            return Redis.As<Customer>().GetAll();

            //var response = new List<Customer>();
            //response.Add(new Customer
            //{
            //    Id = 1,
            //    FirstName = "David",
            //    LastName = "Neal",
            //    Email = "david@reverentgeek.com",
            //    UserName = "david.neal"
            //});
            //response.Add(new Customer
            //{
            //    Id = 2,
            //    FirstName = "Daniel",
            //    LastName = "Norton",
            //    Email = "daniel.norton@leankit.com",
            //    UserName = "daniel.norton"
            //});
            //response.Add(new Customer
            //{
            //    Id = 3,
            //    FirstName = "Mike",
            //    LastName = "Hostetler",
            //    Email = "mike@appendto.com",
            //    UserName = "mike.hostetler"
            //});

            //return response;
        }
    }
}