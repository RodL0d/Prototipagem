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
    }

    private void Update()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (armMechanic.isExtending && hitColliders != null)
        {
            playerController.PickBox(hitColliders);
        }
    }
}