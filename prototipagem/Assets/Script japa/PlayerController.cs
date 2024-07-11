using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerCollider))]
public class PlayerController : MonoBehaviour
{
    const float speed = 5;
    const float fallMultiplier = 2.5f;
    const float lowJumpMultiplier = 2f;


    bool jumping;
    Vector2 direction;

    public GameObject armPrefab;
    public float armReach = 3.0f;
    public float armDuration = 2.0f;

    private GameObject armInstance;
    private bool isStretching = false;
    private Vector3 armDirection;
    private float armTimer = 0f;

    SpriteRenderer render;
    Rigidbody2D rb;
    PlayerCollider playerCollider;
    Animator animator;
    GameObject boxHolded;
    public bool puxouCaixa;
    [SerializeField] Vector2 point;
    bool holding;
    public GameObject local;
    [SerializeField]float radius;
    [SerializeField] LayerMask layerMask;

    Inputs inputs;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<PlayerCollider>();
        animator = GetComponent<Animator>();

        inputs = new Inputs();

        inputs.Player.Pular.performed += ctx => Jump();
        inputs.Player.Pular.canceled += ctx => jumping = false;
        inputs.Player.Andar.performed += ctx => direction = ctx.ReadValue<Vector2>();
        inputs.Player.Puxar.performed += ctx => PickBox();


    }
    private void Update()
    {
        SetGravity();
        Movement();
    }

    private void Movement()
    {
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }

        render.flipX = rb.velocity.x < 0;
    }

    private void SetGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * fallMultiplier * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumping)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * lowJumpMultiplier * Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (playerCollider.OnGround)
        {
            jumping = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

     void PickBox()
    {
        if (holding == true)
        {
            holding = false;
            boxHolded.transform.parent = null;
            boxHolded.GetComponent<Rigidbody2D>().isKinematic = false;
            boxHolded = null;
            return;
        }
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (hitColliders != null)
        {
            
                if (hitColliders.GetComponent<Rigidbody2D>())
                {
                boxHolded = hitColliders.gameObject;
                boxHolded.GetComponent<Rigidbody2D>().isKinematic = true;
                boxHolded.transform.position = transform.position + new Vector3(-1,0,0);
                boxHolded.transform.parent = local.transform;
                holding = true;

                }
                
        }
    }
   
    void StretcArm()
    {
        armDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left;

        armInstance = Instantiate(armPrefab, transform.position, Quaternion.identity);
        armInstance.transform.parent = transform;
        armInstance.transform.localScale = new Vector3(armReach, 1, 1);

        armInstance.transform.localPosition = armDirection * armReach / 2;

        isStretching = true;
        armTimer = 0f;
    }

    void RetractArm()
    {
        Destroy(armInstance);
        isStretching = false;
    }


}