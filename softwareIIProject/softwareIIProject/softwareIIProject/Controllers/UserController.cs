using softwareIIProject.Providers;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace softwareIIProject.Controllers
{
    public class UserController : ApiController
    {   [HttpGet]
        // api/user/Register/storeOwner or normalUser 
        public bool Register(String userType,String mail,String password)
        {
            IDatabaseProvider provider = GetDatabaseProvider(userType);
            Dictionary<String, String> credentials = new Dictionary<String, String>();
            credentials.Add("mail", mail);
            credentials.Add("password", password);

            if (provider.Insert(credentials)) return true;

            return false;
        }


        [HttpGet]
        // api/user/ListAllRegisteredUser
        private String ListAllRegisteredUser()
        {
            IDatabaseProvider storeOwnerProvider = new StoreOwnerDatabaseProvider();
            
            IDatabaseProvider normalUserProvider = new NormalUserDatabaseProvider();

            return normalUserProvider.Select() +storeOwnerProvider.Select();
        }
        private IDatabaseProvider GetDatabaseProvider(String entityName)
        {
            if(entityName.Equals("storeOwner"))
            {
                return new StoreOwnerDatabaseProvider();
            }
            else if(entityName.Equals("normalUser"))
            {
                return new NormalUserDatabaseProvider();
            }
            return null;
        }
    }
}
