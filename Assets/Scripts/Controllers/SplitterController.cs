using Assets;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitterController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    public SplitterDirectionEnum splitterDirectionEnum;

    public int CurrentCount;

    public int DirectionCount = 2;

    public GameObject splitterCanvas;

    private int direction;
    public int Direction
    {
        get => this.direction;
        set
        {
            this.direction = value % DirectionCount;
        }
    }

    [SerializeField] private Direction[] directions;
    public Direction[] Directions
    {
        get => this.directions;
        set
        {
            this.directions = value;
        }
    }

    void Awake()
    {
        this.CurrentCount = 0;
        this.Direction = 0;

        this.Directions.ToList().ForEach(x => 
        {
            if (x.Count <= 0)
            {
                x.Count = 2;
            }

            if (x.Inventory == null)
            {
                x.Inventory = new List<Resource>();
            }
        });

        this.splitterCanvas = PrefabDatabase.Instance.GetPrefab("UI", "Splitter1");

        //this.Directions = new Direction[]
        //{
        //    // LEFT
        //    new Direction(new Vector3(-1.0f, 0.0f, 0.0f), 2, new List<Resource>(), resourceSpawnPointLeft.transform, moveToPointLeft.transform),
        //    // FORWARD
        //    new Direction(new Vector3(0.0f, 1.0f, 0.0f), 2, new List<Resource>(), resourceSpawnPointForward.transform, moveToPointForward.transform),
        //};
    }

    public void ActionToPerformOnTimer()
    {
        
    }

    public void OnClick()
    {
        this.splitterCanvas.GetComponent<SplitterCanvasScript1>().Activate(this.gameObject);
    }

    public void OnCollision(Collider2D col)
    {
        this.MachineController.SubtractElectricityCost();

        Resource resourceCollider = col.GetComponent<ResourceController>().resource;

        this.AddToInventory(resourceCollider);
        GameObject.Destroy(col.gameObject);
        this.CreateFromInventory();
    }

    public void CreateFromInventory()
    {
        for (int i = 0; i < DirectionCount; i++)
        {
            this.Directions[i].Inventory
                .Where(x => x.Quantity > 0)
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
        if (this.CurrentCount >= this.Directions[this.Direction].Count)
        {
            this.Direction++;

            while (this.Directions[this.Direction].Count == 0)
            {
                this.Direction++;
            }

            this.CurrentCount = 0;
        }
    }


    public void AddToInventory(Resource resourceToAdd)
    {
        long resourceQuantity = resourceToAdd.Quantity;

        for (int i = 0; i < resourceQuantity; i++)
        {
            this.CheckInventoryCount();
            Resource existingResource = this.Directions[this.Direction].Inventory.FirstOrDefault(x => x.id == resourceToAdd.id);

            if (existingResource == null)
            {
                Resource newResource = new Resource(resourceToAdd.Value, resourceToAdd.Cost, 1, resourceToAdd.id, resourceToAdd.name);
                this.Directions[this.Direction].Inventory.Add(newResource);
            }
            else
            {
                existingResource.Quantity++;
            }

            this.CurrentCount++;
        }
    }

    public void CreateResource(Resource resource, int direction)
    {
        GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), this.Directions[direction].ResourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));

        go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", resource.name);
        ResourceController rc = go.GetComponent<ResourceController>();
        rc.SetResource(resource, resource.Quantity);
        rc.Move(this.Directions[direction].MoveToPosition.position);
        rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
    }

    public void RemoveItemFromInventory(Resource resourceToRemove, int direction)
    {
        this.Directions[direction].Inventory.Remove(resourceToRemove);
    }

    public void SetControllerValues(IMachineController other)
    {
        SplitterController otherController = other as SplitterController;
        this.Direction = otherController.Direction;
        this.Directions = otherController.Directions;
    }
}

[Serializable]
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

    [JsonIgnore]
    [SerializeField]
    private Transform resourceSpawnPosition;
    public Transform ResourceSpawnPosition
    {
        get => this.resourceSpawnPosition;
        set => this.resourceSpawnPosition = value;
    }

    [JsonIgnore]
    [SerializeField]
    private Transform moveToPosition;
    public Transform MoveToPosition
    {
        get => this.moveToPosition;
        set => this.moveToPosition = value;
    }
}
