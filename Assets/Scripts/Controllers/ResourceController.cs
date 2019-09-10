using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public Resource resource;
    public Vector3 moveToPosition;
    public Vector3 nextMoveToPosition;

    [SerializeField]
    private bool isMoving;
    public bool IsMoving
    {
        get => this.isMoving;
        set => this.isMoving = value;
    }

    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public float moveSpeed;
    public Vector3 finalDestination = new Vector3(2f, 2f, 0f);
    public bool destroy;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        rigidBody = base.gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = base.gameObject.GetComponent<SpriteRenderer>();
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
            if ((rc.resource.id == this.resource.id) && (!this.destroy))
            {
                this.resource.quantity += rc.resource.quantity;
                rc.destroy = true;
                Destroy(collision.gameObject);
            }
        }
    }

    public void SetResource(Resource resource, long quantity)
    {
        this.resource = new Resource(resource);
        this.resource.quantity = quantity;
    }

    public void SellResource()
    {
        Player.playerModel.AddMoney(this.resource.value * this.resource.quantity);

        Destroy(this.gameObject);
    }
}
