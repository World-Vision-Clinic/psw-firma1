using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Integration.Pharmacy.Service
{
   public class ObjectionsService
    {
        ObjectionsRepository objectionsRepository = new ObjectionsRepository();

        public List<Objection> GetAll()
        {
            return objectionsRepository.GetAll();
        }

        public void saveEntity(Objection newObjection)
        {
            objectionsRepository.saveEntity(newObjection);
        }

        public bool sendObjection(Objection objection)
        {
            var client = new RestSharp.RestClient(objection.PharmacyId);
            var request = new RestRequest("/objections/add");

            

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(
            new
            {
                content = objection.Content,
                idEncoded = objection.Id
            });
            request.AddHeader("ApiKey", "ABCD1234EFGH");

            IRestResponse response = client.Post(request);  
            System.Diagnostics.Debug.WriteLine(response.StatusCode);

            if(response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
