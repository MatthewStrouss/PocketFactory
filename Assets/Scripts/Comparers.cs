using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comparers
{
    private static Comparers instance;

    private Dictionary<string, IEqualityComparer> comparers = new Dictionary<string, IEqualityComparer>(StringComparer.InvariantCultureIgnoreCase);

    public static Comparers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Comparers();
            }

            return instance;
        }
    }

    private ResourceComparer resourceComparerInstance;
    public ResourceComparer ResourceComparerInstance
    {
        get => this.resourceComparerInstance;

        set => this.resourceComparerInstance = value;
    }

    private RecipeComparer recipeComparerInstance;
    public RecipeComparer RecipeComparerInstance
    {
        get => this.recipeComparerInstance;

        set => this.recipeComparerInstance = value;
    }

    public Comparers()
    {
        this.ResourceComparerInstance = new ResourceComparer();
        this.RecipeComparerInstance = new RecipeComparer();
    }

    public class ResourceComparer : IEqualityComparer<Resource>
    {
        public bool Equals(Resource x, Resource y)
        {
            return x.name.Equals(y.name, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Resource obj)
        {
            return obj.name.GetHashCode();
        }
    }

    public class RecipeComparer : IEqualityComparer<Resource>
    {
        public bool Equals(Resource x, Resource y)
        {
            return x.name.Equals(y.name, StringComparison.OrdinalIgnoreCase) && y.Quantity >= x.Quantity;
        }

        public int GetHashCode(Resource obj)
        {
            return obj.name.GetHashCode() ^ obj.Quantity.GetHashCode();
        }
    }
}
