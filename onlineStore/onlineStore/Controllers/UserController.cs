using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace onlineStore.Controllers
{
    public class UserController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        // api/user/Register/storeOwner or normalUser 
        public bool Register(String userType, String adminMail, JObject userInformation)
        {
           if(userType.Equals(UserContract.TABLE_ADMIN) && !(adminMail==null))
            { return RegisterAdmin(userInformation); }
            return AddEntry(userType, userInformation);
}


        [HttpGet]
        [Authorize(Roles = UserContract.TABLE_ADMIN)]
        // api/user/ListAllRegisteredUser
        public Dictionary<String, object> ListAllRegisteredUser()
        {

            var store = new onlineStorePlatformEntities1();
            var normalUsersList = from NormalUser in store.NormalUsers select NormalUser.email;
            var storeOwnerList = from StoreOwner in store.StoreOwners select StoreOwner.email;
            var adminsList = from Admin in store.Admin select Admin.email;
            Dictionary<String, object> resultList = new Dictionary<string, object>();
            resultList.Add("Normal Users", normalUsersList);
            resultList.Add("Store Owners", storeOwnerList);
            resultList.Add("Admins", adminsList);
            return resultList;
        }


        [Authorize(Roles = UserContract.TABLE_ADMIN)]
        private Boolean RegisterAdmin(JObject userInfo)
        {
            using (var store = new onlineStorePlatformEntities1())
            {
                User userEntry = new User();
                userEntry.Serialize(userInfo);
                userEntry.role = UserContract.TABLE_ADMIN;
                store.Users.Add(userEntry);
                Admin adminEntry = new Admin();
                adminEntry.Serialize(userInfo);
                store.Admin.Add(adminEntry);


                try
                {


                    store.SaveChanges();
                    return true;


                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
         
        }
        private Boolean AddEntry(String userType, JObject userInformation)
        {
            Boolean save = false;
            if (userType.Equals(UserContract.TABLE_STORE_OWNER) || userType.Equals(UserContract.TABLE_NORMAL_USER))
            {
                
                using (var store = new onlineStorePlatformEntities1())
                {
                    User userEntry = new User();
                    userEntry.Serialize(userInformation);
                    userEntry.role = userType;
                    store.Users.Add(userEntry);
                    //store.SaveChanges();
                    if (userType.Equals(UserContract.TABLE_NORMAL_USER))
                    {
                        NormalUser normalUserEntry = new NormalUser();
                        normalUserEntry.Serialize(userInformation);
                        Debug.WriteLine(normalUserEntry.email);
                        store.NormalUsers.Add(normalUserEntry);
                        save = true;

                    }
                    else if (userType.Equals(UserContract.TABLE_STORE_OWNER))
                    {
                        StoreOwner storeOwnerEntry = new StoreOwner();
                        storeOwnerEntry.Serialize(userInformation);
                        store.StoreOwners.Add(storeOwnerEntry);
                        save = true;
                    }
                   
                    try
                    {
                        if (save)
                        {
                            store.SaveChanges();
                            return true;
                        }

                    }
                    catch (DbUpdateException)
                    {
                        return false;
                    }

                }


            }
            return false;
        }
    }

}
