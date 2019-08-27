using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Resource")]
public class ResourceScriptableObject : ScriptableObject
{
    [SerializeField] private long cost;
    public long Cost
    {
        get => this.cost;
        set => this.cost = value;
    }

    [SerializeField] private long value;
    public long Value
    {
        get => this.value;
        set => this.value = value;
    }

    [SerializeField] private long quantity;
    public long Quantity
    {
        get => this.quantity;
        set => this.quantity = value;
    }

    public string Name
    {
        get => this.name;
        set => this.name = value;
    }

    [SerializeField] private Sprite sprite;
    public Sprite Sprite
    {
        get => this.sprite;
        set => this.sprite = value;
    }
}
