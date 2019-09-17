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

    private long directionCountSum;

    public bool isWaitingToDispense;

    public float timeToWaitForDispense = 0.5f;

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

        this.UpdateDirectionSum();

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
        this.splitterCanvas.GetComponent<SplitterCanvasScript1>().Activate(this.gameObject, this.UICallback);
    }

    public void UICallback()
    {
        this.UpdateDirectionSum();
    }

    public void UpdateDirectionSum()
    {
        this.directionCountSum = this.Directions.Sum(x => x.Count);
    }

    public void CollisionEnter(Collider2D col)
    {
        ResourceController resourceCollider = col.GetComponent<ResourceController>();

        if (resourceCollider.CanCollide)
        {
            this.MachineController.SubtractElectricityCost();

            Resource newResource = new Resource(resourceCollider.resource.id, resourceCollider.resource.Quantity);
            resourceCollider.destroy = true;
            GameObject.Destroy(col.gameObject);
            this.AddToInventory(newResource);

            if (!this.isWaitingToDispense)
            {
                StartCoroutine("Dispense");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //collision.GetComponent<ResourceController>()?.insideMachine.SetValue(false);
        ResourceController rc = collision.GetComponent<ResourceController>();

        if (rc != null)
        {
            rc.insideMachine = false;
        }
    }

    IEnumerator Dispense()
    {
        this.isWaitingToDispense = true;
        yield return new WaitForSeconds(timeToWaitForDispense);
        this.isWaitingToDispense = false;
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

        for (int i = 0; i < this.Directions.Count(); i++)
        {
            long quantityToAdd = Convert.ToInt64(Mathf.Floor(((float)this.Directions[this.Direction].Count / this.directionCountSum) * resourceToAdd.Quantity));

            Resource existingResource = this.Directions[this.Direction].Inventory.FirstOrDefault(x => x.id == resourceToAdd.id);

            if (existingResource == null)
            {
                Resource newResource = new Resource(resourceToAdd.id, quantityToAdd);
                this.Directions[this.Direction].Inventory.Add(newResource);
            }
            else
            {
                existingResource.Quantity += quantityToAdd;
            }

            this.Direction++;
            resourceQuantity -= quantityToAdd;
        }


        for (int i = 0; i < resourceQuantity; i++)
        {
            this.CheckInventoryCount();
            Resource existingResource = this.Directions[this.Direction].Inventory.FirstOrDefault(x => x.id == resourceToAdd.id);

            if (existingResource == null)
            {
                Resource newResource = new Resource(resourceToAdd.id, 1);
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

        go.GetComponent<SpriteRenderer>().sprite = resource.Sprite;
        ResourceController rc = go.GetComponent<ResourceController>();
        rc.SetResource(resource, resource.Quantity);
        rc.Move(this.Directions[direction].MoveToPosition.position);
        rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
        rc.insideMachine = true;
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
        this.UpdateDirectionSum();
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
