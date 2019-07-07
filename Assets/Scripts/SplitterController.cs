﻿using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitterController : MonoBehaviour, IMachineController
{
    public int currentCount;

    private int direction;
    public int Direction
    {
        get => this.direction;
        set
        {
            this.direction = value % 4;
        }
    }

    private Direction[] directions;
    public Direction[] Directions
    {
        get => this.directions;
        set
        {
            this.directions = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.currentCount = 0;
        this.Direction = 0;

        this.Directions = new Direction[]
        {
            // LEFT
            new Direction(new Vector3(-1.0f, 0.0f, 0.0f), 2, new List<Resource>(), this.transform.GetChild(0), this.transform.GetChild(1)),
            // UP
            new Direction(new Vector3(0.0f, 1.0f, 0.0f), 2, new List<Resource>(), this.transform.GetChild(2), this.transform.GetChild(3)),
            // RIGHT
            new Direction(new Vector3(1.0f, 0.0f, 0.0f), 2, new List<Resource>(), this.transform.GetChild(4), this.transform.GetChild(5)),
            // DOWN
            new Direction(new Vector3(0.0f, -1.0f, 0.0f), 2, new List<Resource>(), this.transform.GetChild(6), this.transform.GetChild(7)),
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActionToPerformOnTimer()
    {
        for (int i = 0; i < 4; i++)
        {
            this.Directions[i].Inventory
                .Where(x => x.quantity > 0)
                .ToList()
                .ForEach(x =>
                {
                    this.CreateResource(x, i);
                    this.RemoveItemFromInventory(x, i);
                });
        }
    }

    public void CheckInventoryCount()
    {
        if (this.Directions[this.Direction].Count == this.currentCount)
        {
            this.Direction++;
            this.currentCount = 0;
        }
    }

    public void AddToInventory(Resource resourceToAdd)
    {
        int resourceQuantity = resourceToAdd.quantity;

        for (int i = 0; i < resourceQuantity; i++)
        {
            this.CheckInventoryCount();
            Resource existingResource = this.Directions[this.Direction].Inventory.FirstOrDefault(x => x.id == resourceToAdd.id);

            if (existingResource == null)
            {
                Resource newResource = new Resource(resourceToAdd.value, resourceToAdd.cost, 1, resourceToAdd.isUnlocked, resourceToAdd.id, resourceToAdd.name);
                this.Directions[this.Direction].Inventory.Add(newResource);
            }
            else
            {
                existingResource.quantity++;
            }

            this.currentCount++;
        }
    }

    public void RemoveItemFromInventory(Resource resourceToRemove, int direction)
    {
        this.Directions[direction].Inventory.Remove(resourceToRemove);
    }

    public void OnCollision(Collider2D col)
    {
        this.AddToInventory(col.GetComponent<ResourceController>().resource);
        Destroy(col.gameObject);
    }

    public void CreateResource(Resource resource, int direction)
    {
        GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), this.Directions[direction].ResourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));

        go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", resource.name);
        ResourceController rc = go.GetComponent<ResourceController>();
        rc.SetResource(resource, resource.quantity);
        rc.Move(this.Directions[direction].MoveToPosition.position);
        rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
    }
}

public class Direction
{
    public Direction()
    {

    }

    public Direction(Vector3 pushPosition, int count, List<Resource> inventory, Transform resourceSpawnPosition, Transform moveToPosition)
    {
        this.pushPosition = pushPosition;
        this.count = count;
        this.inventory = inventory;
        this.resourceSpawnPosition = resourceSpawnPosition;
        this.moveToPosition = moveToPosition;
    }

    private Vector3 pushPosition;
    public Vector3 PushPosition
    {
        get => this.pushPosition;
        set
        {
            this.pushPosition = value;
        }
    }

    private int count;
    public int Count
    {
        get => this.count;
        set
        {
            this.count = value;
        }
    }

    private List<Resource> inventory;
    public List<Resource> Inventory
    {
        get => this.inventory;
        set
        {
            this.inventory = value;
        }
    }

    private Transform resourceSpawnPosition;
    public Transform ResourceSpawnPosition
    {
        get => this.resourceSpawnPosition;
        set => this.resourceSpawnPosition = value;
    }

    private Transform moveToPosition;
    public Transform MoveToPosition
    {
        get => this.moveToPosition;
        set => this.moveToPosition = value;
    }
}
