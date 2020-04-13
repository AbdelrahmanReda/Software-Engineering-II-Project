using softwareIIProject.Providers;
using System;
using System.Collections.Generic;
using System.Web.Http;


namespace softwareIIProject.Controllers
{
    public class UserController : ApiController
    {

        [HttpPost]
        // api/user/Register/storeOwner or normalUser 
        public bool Register(String userType, String adminMail, String mail,String password)
        {
            if(userType.Equals(UserContract.TABLE_ADMIN))
            {
                if (!DummyLogin(adminMail)) return false;
            }
            IDatabaseProvider provider = GetDatabaseProvider(userType);
            Dictionary<String, String> credentials = new Dictionary<String, String>();
            credentials.Add("mail", mail);
            credentials.Add("password", password);

            if (provider.Insert(credentials)) return true;

            return false;
        }


        [HttpGet]
        // api/user/ListAllRegisteredUser
        public Dictionary<string, List<Dictionary<string, object>>> ListAllRegisteredUser(String mail)
        {
            if (!DummyLogin(mail)) return null;
            List<Dictionary<String, Object>> storeOwnerReader = new List<Dictionary<string, object>>();
            IDatabaseProvider storeOwnerProvider = new StoreOwnerDatabaseProvider();
            storeOwnerReader = storeOwnerProvider.Select();

            List<Dictionary<String, Object>> normalUserReader = new List<Dictionary<string, object>>();
            IDatabaseProvider normalUserProvider = new NormalUserDatabaseProvider();
            normalUserReader = normalUserProvider.Select();
            List<Dictionary<String, Object>> adminReader = new List<Dictionary<string, object>>();
            IDatabaseProvider adminProvider = new AdminDatabaseProvider();
            adminReader = adminProvider.Select();
            Dictionary<String, List<Dictionary<String, Object>>> data=new Dictionary<string, List<Dictionary<string, object>>>();
            data.Add("Normal Users", normalUserReader);
            data.Add("Store Owners", storeOwnerReader);
            data.Add("Admins", adminReader);

            return data;
        }
        private Boolean DummyLogin(String mail)
        {
            if(mail.Equals("mohamed606tanna@gmail.com"))
            {
                return true;
            }
            return false;
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
            else if(entityName.Equals("Admin"))
            {
                return new AdminDatabaseProvider();
            }
            return null;
        }
        
    }
}
