using UnityEngine;

public class ArmCollisionChecker : MonoBehaviour
{
    private ArmMechanic armMechanic;

    void Start()
    {
        armMechanic = GetComponentInParent<ArmMechanic>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Verifica se o braço está se estendendo e se colidiu com um objeto com a tag "Pickup"
        if (armMechanic.isExtending && other.CompareTag("Pickup"))
        {
            armMechanic.PullBox(other);
        }
    }
}