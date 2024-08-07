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

    public Transform arm;
    public float stretchDistance = 3f;
    public float stretchDuration = 2f;
    private Vector3 originalPosition;
    private bool isStretching = false;
    private Transform targetBox;

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
        inputs.Player.EsticarBraço.performed += ctx => StretchingArm();


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

    void StretchingArm()
    {
        originalPosition = arm.position;
        if (Input.GetKeyDown(KeyCode.J) && !isStretching)
        {
            StartCoroutine(StretchArm());
        }
    }

    IEnumerator StretchArm()
    {
        isStretching = true;
        Vector3 targetPosition = arm.position + arm.right * stretchDistance;
        float time = 0f;

        // Esticar o braço
        while (time < 1f)
        {
            time += Time.deltaTime * stretchDuration;
            arm.position = Vector3.Lerp(originalPosition, targetPosition, time);
            yield return null;
        }

        // Detectar se há uma caixa próxima
        Collider2D[] colliders = Physics2D.OverlapCircleAll(arm.position, 0.5f, layerMask);
        if (colliders.Length > 0)
        {
            targetBox = colliders[0].transform;
            // Mover a caixa para o braço
            targetBox.position = arm.position;
        }

        // Voltar o braço para a posição original
        time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * stretchDuration;
            arm.position = Vector3.Lerp(targetPosition, originalPosition, time);
            if (targetBox)
            {
                targetBox.position = arm.position;
            }
            yield return null;
        }

        isStretching = false;
        targetBox = null;
    }
}