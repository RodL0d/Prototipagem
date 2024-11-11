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
[RequireComponent(typeof(Mecanicas))]
[RequireComponent(typeof(HUD))]
public class PlayerController : MonoBehaviour
{
    const float speed = 5;
    const float fallMultiplier = 2.5f;
    const float lowJumpMultiplier = 2f;
    const float jumpForce = 10;
    const float dashForce = 10;

    VerificaPulo verificaPulo;
    bool jumping, dashing, crouching;
    Vector2 direction;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    SpriteRenderer render;
    Rigidbody2D rb;
    PlayerCollider playerCollider;
    Animator animator;
    float contSuperJump;
    Inputs inputs;
    bool superJumpActive;
    int printContSuperJump;
    HUD hud;

    const float limiteSuperPulo = 2F;

    public Transform arm;
    public float stretchDistance = 3f;
    public float stretchDuration = 2f;
    private Vector3 originalPosition;
    private bool isStretching = false;
    private Transform targetBox;

    GameObject boxHeld;
    public bool puxouCaixa;
    [SerializeField] Vector2 point;
    bool holding;
    public GameObject local;
    [SerializeField] float radius;
    [SerializeField] LayerMask layerMask;
    bool isRunning = false;
    bool falling = false;

    private void Awake()
    {
        verificaPulo = GetComponentInChildren<VerificaPulo>();
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<PlayerCollider>();
        animator = GetComponent<Animator>();
        hud = GetComponent<HUD>();
        inputs = new Inputs();

        inputs.Player.Pular.performed += ctx => Jump();
        inputs.Player.Pular.canceled += ctx => jumping = false;
        inputs.Player.Andar.performed += ctx => direction = ctx.ReadValue<Vector2>();
        inputs.Player.SuperPulo.started += ctx => superJumpActive = true;
        inputs.Player.SuperPulo.canceled += ctx => superJumpActive = false;
        inputs.Player.puxar.performed += ctx => PickBox(null);
    }

    private void Update()
    {
        SetGravity();
        Movement();
        SuperJump();
        Passarfase();
        RetrocederFase();

        // Atualiza o estado de queda e aplica na animação
        falling = IsFalling();
        animator.SetBool("Falling", falling);
        // Reseta a animação de pulo quando volta ao chão
        if (playerCollider.OnGround && rb.velocity.y <= 0)
        {
            animator.SetBool("Jump", false);
        }
    }

    private void Movement()
    {
        if (!dashing)
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
            isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
        }
        animator.SetBool("Run", isRunning);
    }

    private void SetGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumping)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool IsFalling()
    {
        return rb.velocity.y < 0;
        
    }

    private void Jump()
    {
        // Verifica se o personagem está no chão e permite o pulo
        if (playerCollider.OnGround && verificaPulo.estaNoChao)
        {
            jumping = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            falling = false;

            // Ativa a animação de pulo
            animator.SetBool("Jump", true);
        }
        
    }

    private void SuperJump()
    {
        if (superJumpActive && GameManager.instance.SuperPulo)
        {
            
            contSuperJump += Time.deltaTime;
            hud.UpdateSuperPuloBar(contSuperJump, limiteSuperPulo);
            printContSuperJump++;
            Debug.Log(printContSuperJump);

            animator.SetBool("StarSuperJump", true);

            if (contSuperJump >= limiteSuperPulo)
            {
                animator.SetBool("StarSuperJump", false);
                float newJumpForce = jumpForce + 2;
                jumping = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(rb.velocity.x, newJumpForce);
                animator.SetBool("SuperJump", true);
                Debug.Log("Super pulo ativado");
                contSuperJump = 0;
                printContSuperJump = 0;
                
            }
        }
        else
        {
            contSuperJump = 0;
            printContSuperJump = 0;
            hud.UpdateSuperPuloBar(contSuperJump, limiteSuperPulo);
            Debug.Log("Super pulo resetado");
            animator.SetBool("SuperJump", false);
            animator.SetBool("StarSuperJump", false);
        }        
    }

    private IEnumerator Dash()
    {
        if (!dashing && direction.x != 0)
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
        if (holding && GameManager.instance.puxarCaixa)
        {
            holding = false;
            boxHeld.transform.parent = null;
            boxHeld.GetComponent<BoxCollider2D>().isTrigger = false;
            boxHeld.GetComponent<Rigidbody2D>().isKinematic = false;
            boxHeld = null;
            return;
        }
        if (hitColliders == null)
        {
            hitColliders = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        }
        if (hitColliders != null && hitColliders.GetComponent<Rigidbody2D>())
        {
            boxHeld = hitColliders.gameObject;
            boxHeld.GetComponent<BoxCollider2D>().isTrigger = true;
            boxHeld.GetComponent<Rigidbody2D>().isKinematic = true;
            boxHeld.transform.position = transform.position + new Vector3(-1, 0, 0);
            boxHeld.transform.parent = transform;
            holding = true;
        }
    }

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
