using System;
using System.Collections.Generic;


namespace softwareIIProject.Providers
{
    interface IDatabaseProvider
    {
        
        Boolean Insert(Dictionary<String,String>map);
        List<Dictionary<String, Object>> Select();
        void Update();
        void Delete();
    }
}
