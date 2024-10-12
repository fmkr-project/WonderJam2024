using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Money,
    Crew,
    Scraps,
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
            Resource.Scraps => "scraps",
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
            Resource.Scraps => Resources.Load<Sprite>("UI/Scraps"),
            Resource.Ether => Resources.Load<Sprite>("UI/Ether")
        };
    }
}