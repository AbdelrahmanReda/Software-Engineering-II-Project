using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;


namespace softwareIIProject.Controllers
{
    public class UserController : ApiController
    {

        [HttpPost]
        // api/user/Register/storeOwner or normalUser 
        public bool Register(String userType, String adminMail , JObject userInformation)
        {
            if(userType.Equals(UserContract.TABLE_ADMIN))
            {
                if (!DummyLogin(adminMail)) return false;
            }

            return AddEntry(userType, userInformation);
          

            
        }


        [HttpGet]
        // api/user/ListAllRegisteredUser
        public Dictionary<String,object> ListAllRegisteredUser(String mail)
        {   if (!DummyLogin(mail)) return null;
            var store = new onlineStorePlatformEntities1();
            var normalUsersList = from normalUser in store.normalUsers select normalUser.Email;
            var storeOwnerList = from storeOwner in store.storeOwners select storeOwner.Email;
            var adminsList = from Admin in store.Admins select Admin.Email;
            Dictionary<String, object> resultList = new Dictionary<string, object>();
            resultList.Add("Normal Users", normalUsersList);
            resultList.Add("Store Owners", storeOwnerList);
            resultList.Add("Admins", adminsList);
             return resultList;
        }
        private Boolean DummyLogin(String mail)
        {
            if(mail.Equals("mohamed606tanna@gmail.com"))
            {
                return true;
            }
            return false;
        }
        private Object GetEntity  (String entityName,JObject userInformation)
        {
            if(entityName.Equals(UserContract.TABLE_STORE_OWNER))
            {
                return JsonConvert.DeserializeObject<storeOwner>(userInformation.ToString()); 
            }
            else if(entityName.Equals(UserContract.TABLE_NORMAL_USER))
            {
                return  JsonConvert.DeserializeObject<normalUser>(userInformation.ToString());
            }
            else if(entityName.Equals(UserContract.TABLE_ADMIN))
            {
                return JsonConvert.DeserializeObject<Admin>(userInformation.ToString());
            }
            return null;
        }
        private Boolean AddEntry(String userType,JObject userInformation)
        {
            
            var entity = GetEntity(userType, userInformation);
            using (var store = new onlineStorePlatformEntities1()) {
                if (userType.Equals(UserContract.TABLE_NORMAL_USER))
                {
                   store.normalUsers.Add((normalUser)entity) ;
                   
                }
                else if(userType.Equals(UserContract.TABLE_STORE_OWNER))
                 {
                    store.storeOwners.Add((storeOwner)entity);
                }
                else if(userType.Equals(UserContract.TABLE_ADMIN))
                {
                    store.Admins.Add((Admin)entity);
                }
                try
                {
                    store.SaveChanges();
                    return true;

                }catch(DbUpdateException)
                {
                    return false;
                }
                
             } 
            
            
        }
       
    }
}
