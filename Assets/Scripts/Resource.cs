using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Resource
{
    Money,
    Ether
}

class ResourceManager
{
    public static string StringOfResource(Resource resource)
    {
        return resource switch
        {
            Resource.Money => "money",
            Resource.Ether => "ether",
            _ => "Resource not implemented"
        };
    }
}