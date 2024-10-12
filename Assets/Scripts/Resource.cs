using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Money,
    Crew,
    Scrap,
    Ether
}

public struct ResourceAmount
{
    public Resource Resource;
    public int Quantity;

    public ResourceAmount(Resource r, int q)
    {
        Resource = r;
        Quantity = q;
    }
}

public class ResourceManager
{
    public static string StringOfResource(Resource resource)
    {
        return resource switch
        {
            Resource.Money => "money",
            Resource.Crew => "crew",
            Resource.Scrap => "scrap",
            Resource.Ether => "ether",
            _ => "Resource not implemented"
        };
    }

    public static Sprite GetResourceIcon(Resource resource)
    {
        return resource switch
        {
            Resource.Money => Resources.Load<Sprite>("UI/Money"),
            Resource.Crew => Resources.Load<Sprite>("UI/Crew"),
            Resource.Scrap => Resources.Load<Sprite>("UI/Scrap"),
            Resource.Ether => Resources.Load<Sprite>("UI/Ether"),
            _ => throw new Exception("Resource not implemented")
        };
    }
}