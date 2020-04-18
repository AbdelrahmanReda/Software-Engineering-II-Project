<<<<<<< HEAD
﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace onlineStore.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        // api/user/Register/storeOwner or normalUser 
        public bool Register(String userType, String adminMail, JObject userInformation)
        {

            if (userType.Equals(UserContract.TABLE_ADMIN) && !(adminMail == null))
            { return RegisterAdmin(userInformation); }
            return AddEntry(userType, userInformation);



        }

        [Authorize(Roles = "Admin")]
        private Boolean RegisterAdmin(JObject userInfo)
        {
            using (var store = new onlineStorePlatformEntities())
            {
                User userEntry = new User();
                userEntry.Serialize(userInfo);
                userEntry.role = UserContract.TABLE_ADMIN;
                store.Users.Add(userEntry);
                Admin adminEntry = new Admin();
                adminEntry.Serialize(userInfo);
                store.Admins.Add(adminEntry);
                store.SaveChanges();

                return true;
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        // api/user/ListAllRegisteredUser
        public Dictionary<String, object> ListAllRegisteredUser()
        {

            var store = new onlineStorePlatformEntities();
            var normalUsersList = from NormalUser in store.NormalUsers select NormalUser.email;
            var storeOwnerList = from StoreOwner in store.StoreOwners select StoreOwner.email;
            var adminsList = from Admin in store.Admins select Admin.email;
            Dictionary<String, object> resultList = new Dictionary<string, object>();
            resultList.Add("Normal Users", normalUsersList);
            resultList.Add("Store Owners", storeOwnerList);
            resultList.Add("Admins", adminsList);
            return resultList;
        }



        private bool AddEntry(String userType, JObject userInformation)
        {
            Boolean save = false;
            if (userType.Equals(UserContract.TABLE_NORMAL_USER) || userType.Equals(UserContract.TABLE_STORE_OWNER))
            {
                using (var store = new onlineStorePlatformEntities())
                {
                    User userEntry = new User();
                    userEntry.Serialize(userInformation);
                    userEntry.role = userType;
                    store.Users.Add(userEntry);

                    if (userType.Equals(UserContract.TABLE_NORMAL_USER))
                    {
                        NormalUser normalUserEntry = new NormalUser();
                        normalUserEntry.Serialize(userInformation);

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


||||||| 6248ce8
=======
﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace onlineStore.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        // api/user/Register/storeOwner or normalUser 
        public bool Register(String userType,JObject userInformation)
        {
            return AddEntry(userType, userInformation);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        // api/user/ListAllRegisteredUser
        public Dictionary<String, object> ListAllRegisteredUser()
        {
            using (var store = new onlineStorePlatformEntities())
            {
                var normalUsersList = from NormalUser in store.NormalUsers select NormalUser.email;
                var storeOwnerList = from StoreOwner in store.StoreOwners select StoreOwner.email;
                var adminsList = from Admin in store.Admins select Admin.email;
                Dictionary<String, object> resultList = new Dictionary<string, object>();
                resultList.Add("Normal Users", normalUsersList);
                resultList.Add("Store Owners", storeOwnerList);
                resultList.Add("Admins", adminsList);
                return resultList;
            }
        }



        private bool AddEntry(String userType, JObject userInformation)
        {
            Boolean save = false;
            if (userType.Equals(UserContract.TABLE_ADMIN)||userType.Equals(UserContract.TABLE_NORMAL_USER) || userType.Equals(UserContract.TABLE_STORE_OWNER))
            {
                using (var store = new onlineStorePlatformEntities())
                {
                    User userEntry = new User();
                    userEntry.Serialize(userInformation);
                    userEntry.role = userType;
                    store.Users.Add(userEntry);

                    if (userType.Equals(UserContract.TABLE_NORMAL_USER))
                    {
                        NormalUser normalUserEntry = new NormalUser();
                        normalUserEntry.Serialize(userInformation);

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
                    else if (userType.Equals(UserContract.TABLE_ADMIN) && User.IsInRole(UserContract.TABLE_ADMIN))
                    {
                        Admin adminEntry = new Admin();
                        adminEntry.Serialize(userInformation);
                        store.Admins.Add(adminEntry);
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


>>>>>>> 31a1531486d640f2857d52622b3ad357ec368e77
