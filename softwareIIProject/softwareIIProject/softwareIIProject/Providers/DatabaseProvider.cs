using System;
using System.Collections.Generic;


namespace softwareIIProject.Providers
{
    interface IDatabaseProvider
    {
        
        Boolean Insert(Dictionary<String,String>map);
        string Select();
        void Update();
        void Delete();
    }
}
