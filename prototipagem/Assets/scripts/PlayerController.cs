using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerCollider))]
[RequireComponent(typeof(Tempodevida))]
[RequireComponent(typeof(ArmMechanic))]
[RequireComponent(typeof(HUD))]
public class PlayerController : MonoBehaviour
{
    const float speed = 5;
    const float fallMultiplier = 2.5f;
    const float lowJumpMultiplier = 2f;
    const float jumpForce = 10;
    const float dashForce = 10;

    bool jumping, dashing, agachar;
    Vector2 direction;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    SpriteRenderer render;
    Rigidbody2D rb;
    PlayerCollider playerCollider;
    Animator animator;
    float contSuperJump;
    Inputs inputs;
    bool superJumpAcert;
    int printContSuperJump;
    HUD hud;

    const float limiteSuperPulo = 2F;
 

    public Transform arm;
    public float stretchDistance = 3f;
    public float stretchDuration = 2f;
    private Vector3 originalPosition;
    private bool isStretching = false;
    private Transform targetBox;

   
    GameObject boxHolded;
    public bool puxouCaixa;
    [SerializeField] Vector2 point;
    bool holding;
    public GameObject local;
    [SerializeField] float radius;
    [SerializeField] LayerMask layerMask;
    bool isRunning = false;



    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<PlayerCollider>();
        animator = GetComponent<Animator>();
        hud = GetComponent<HUD>();
        inputs = new Inputs();

        inputs.Player.Pular.performed += ctx => Jump();
        inputs.Player.Pular.canceled += ctx => jumping = false;
        inputs.Player.Andar.performed += ctx => direction = ctx.ReadValue<Vector2>();
        inputs.Player.SuperPulo.started += ctx => superJumpAcert = true;
        inputs.Player.SuperPulo.canceled += ctx => superJumpAcert = false;
        inputs.Player.puxar.performed += ctx => PickBox(null);

        
    }
    private void Update()
    {
        SetGravity();
        Movement();
        SuperJump();
        Passarfase();
        RetrocederFase();
    }

    private void Movement()
    {
        
        if (!dashing)
        {
            // Atualize a velocidade do rigidbody com base na direção e na velocidade
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            // Verifique se o personagem está correndo (movimento horizontal diferente de zero)
            isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
        }

       
        animator.SetBool("Run", isRunning);
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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            print("Pulo normal");
        }
             
    }

    
      private void SuperJump()
    {
        if (superJumpAcert && GameManager.instance.SuperPulo)
        {
            contSuperJump += Time.deltaTime;
            hud.UpdateSuperPuloBar(contSuperJump, limiteSuperPulo);
            printContSuperJump++;
            print(printContSuperJump);
            if (contSuperJump >= limiteSuperPulo)
            {
                float newJumpForce;
                newJumpForce = jumpForce + 2;

                jumping = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.velocity = new Vector2(rb.velocity.x, newJumpForce);
                print("Super pulo");
                contSuperJump = 0;
                printContSuperJump = 0;
            }
        }
        
    }

    private IEnumerator Dash()
    {
        if (!dashing)
        {
            animator.SetTrigger("Dashing");
            dashing = true;
            rb.velocity = new Vector2(dashForce * direction.x, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;

            yield return new WaitForSeconds(0.8f);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            dashing = false;
        }
    }


    private void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }

    private void OnEnable()
    {
        inputs.Enable();
        
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
   public void PickBox(Collider2D hitColliders)
    {
        if (holding == true && GameManager.instance.puxarCaixa)
        {
            holding = false;
            boxHolded.transform.parent = null;
            boxHolded.GetComponent<BoxCollider2D>().isTrigger = false;
            boxHolded.GetComponent<Rigidbody2D>().isKinematic = false;
            boxHolded = null;
            return;
        }
        if (hitColliders == null)
        {
            hitColliders = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        }
        if (hitColliders != null)
        {

            if (hitColliders.GetComponent<Rigidbody2D>())
            {
                boxHolded = hitColliders.gameObject;
                boxHolded.GetComponent<BoxCollider2D>().isTrigger = true;
                boxHolded.GetComponent<Rigidbody2D>().isKinematic = true;
                boxHolded.transform.position = transform.position + new Vector3(-1, 0, 0);
                boxHolded.transform.parent = transform;
                holding = true;
            }
        }
    }

    //temporario
    private void Passarfase()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SceneManager.LoadScene(index + 1);

        }
    } 
    private void RetrocederFase()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SceneManager.LoadScene(index - 1);

        }
    }
}
