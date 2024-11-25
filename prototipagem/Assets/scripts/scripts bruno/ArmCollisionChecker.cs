using UnityEngine;

public class ArmCollisionChecker : MonoBehaviour
{
    private ArmMechanic armMechanic;
    PlayerController playerController;
    [SerializeField] float radius;
    [SerializeField]LayerMask layerMask;

    void Start()
    {
        armMechanic = GetComponentInParent<ArmMechanic>();
        playerController = GetComponentInParent<PlayerController>();
        layerMask = LayerMask.GetMask("Caixa");
    }

    private void Update()
    {
        if (armMechanic.isExtending)
        {
            Collider2D hitColliders = Physics2D.OverlapCircle(transform.position, radius, layerMask);
            if (hitColliders != null)
            {
                playerController.PickBox(hitColliders);
                armMechanic.ResetArm();
            }
        }
    }

    
}