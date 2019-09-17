using Assets;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectorController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    public SelectorDirectionEnum selectorDirectionEnum;

    public GameObject selectorCanvas;

    public int DirectionCount = 2;

    private int direction;
    public int Direction
    {
        get => this.direction;
        set
        {
            this.direction = value % DirectionCount;
        }
    }

    [SerializeField] private SelectorDirection[] directions;
    public SelectorDirection[] Directions
    {
        get => this.directions;
        set
        {
            this.directions = value;
        }
    }

    void Awake()
    {
        this.Direction = 0;

        this.Directions.ToList().ForEach(x =>
        {
            if (x.Inventory == null)
            {
                x.Inventory = new List<Resource>();
            }

            x.SelectedResource = ResourceDatabase.GetResource("(None)");
        });

        this.selectorCanvas = PrefabDatabase.Instance.GetPrefab("UI", "Selector");
    }

    public void ActionToPerformOnTimer()
    {

    }

    public void OnClick()
    {
        this.selectorCanvas.GetComponent<SelectorCanvasScript>().Activate(this.gameObject);
    }

    public void CollisionEnter(Collider2D col)
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

    public int GetDirectionForResource(Resource resource)
    {
        int directionIndex = 0;

        SelectorDirection direction = this.Directions.FirstOrDefault(x => x.SelectedResource.id == resource.id);

        if (direction == null)
        {
            directionIndex = 1;
        }
        else
        {
            directionIndex = Array.IndexOf(this.Directions, direction);
        }

        return directionIndex;
    }


    public void AddToInventory(Resource resourceToAdd)
    {
        int direction = GetDirectionForResource(resourceToAdd);

        this.Directions[direction].Inventory.Add(resourceToAdd);

        //int resourceQuantity = resourceToAdd.quantity;

        //for (int i = 0; i < resourceQuantity; i++)
        //{
        //    Resource existingResource = this.Directions[this.Direction].Inventory.FirstOrDefault(x => x.id == resourceToAdd.id);

        //    if (existingResource == null)
        //    {
        //        Resource newResource = new Resource(resourceToAdd.value, resourceToAdd.cost, 1, resourceToAdd.id, resourceToAdd.name);
        //        this.Directions[this.Direction].Inventory.Add(newResource);
        //    }
        //    else
        //    {
        //        existingResource.quantity++;
        //    }
        //}
    }

    public void CreateResource(Resource resource, int direction)
    {
        GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), this.Directions[direction].ResourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));

        go.GetComponent<SpriteRenderer>().sprite = resource.Sprite;
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
        SelectorController otherController = other as SelectorController;
        this.Direction = otherController.Direction;
        this.Directions = otherController.Directions;
    }
}

[Serializable]
public class SelectorDirection
{
    public SelectorDirection()
    {

    }

    public SelectorDirection(Vector3 pushPosition, Resource selectedResource, List<Resource> inventory, Transform resourceSpawnPosition, Transform moveToPosition)
    {
        this.pushPosition = pushPosition;
        this.selectedResource = selectedResource;
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

    private Resource selectedResource;
    public Resource SelectedResource
    {
        get => this.selectedResource;
        set
        {
            this.selectedResource = value;
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
