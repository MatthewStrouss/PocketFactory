using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public Resource resource;
    public Vector3 moveToPosition;
    public Vector3 nextMoveToPosition;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField]
    private bool isMoving;
    public bool IsMoving
    {
        get => this.isMoving;
        set => this.isMoving = value;
    }

    public float moveSpeed;
    public Vector3 finalDestination = new Vector3(2f, 2f, 0f);
    public bool destroy;
    public bool insideMachine;
    //private bool insideMachine;
    //public bool InsideMachine
    //{
    //    get => this.insideMachine;
    //    set => this.insideMachine = value;
    //}

    public bool CanCollide
    {
        get
        {
            return !this.destroy && !this.insideMachine;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        destroy = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.IsMoving)
        {
            this.rigidBody.MovePosition(Vector3.MoveTowards(this.transform.position, this.moveToPosition, moveSpeed * Time.deltaTime));
        }

        if (this.transform.position == this.moveToPosition)
        {
            this.IsMoving = false;

            if (this.nextMoveToPosition != finalDestination)
            {
                this.Move(this.nextMoveToPosition);
                this.nextMoveToPosition = finalDestination;
            }
        }
        else
        {
            this.isMoving = true;
        }
    }

    public void Move(Vector3 moveToPosition)
    {
        if (!isMoving)
        {
            this.moveToPosition = moveToPosition;
            this.IsMoving = true;
        }
        else
        {
            this.nextMoveToPosition = moveToPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ResourceController rc = collision.gameObject.GetComponent<ResourceController>();
        if (rc != null)
        {
            if ((rc.resource.id == this.resource.id) && (this.CanCollide))
            {
                this.resource.Quantity += rc.resource.Quantity;
                rc.destroy = true;
                Destroy(collision.gameObject);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    ResourceController rc = collision.gameObject.GetComponent<ResourceController>();
    //    if (rc != null)
    //    {
    //        if ((rc.resource.id == this.resource.id) && (this.CanCollide))
    //        {
    //            this.resource.Quantity += rc.resource.Quantity;
    //            rc.destroy = true;
    //            Destroy(collision.gameObject);
    //        }
    //    }
    //}

    public void SetResource(Resource resource, long quantity)
    {
        this.resource = new Resource(resource.id, quantity);
    }

    public void SellResource()
    {
        Player.playerModel.AddMoney(this.resource.Value * this.resource.Quantity);

        Destroy(this.gameObject);
    }
}

public static class Extensions
{
    public static void SetValue<T>(this T property, T value)
    {
        property = value;
    }
}