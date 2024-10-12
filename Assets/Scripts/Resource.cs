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
        switch (resource)
        {
            case Resource.Money:
                return "Money";
            case Resource.Ether:
                return "Ether";
            default:
                return "Resource not implemented";
        }
    }
}