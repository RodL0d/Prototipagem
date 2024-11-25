using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMechanic : MonoBehaviour
{
        public Transform arm;           // Referência ao objeto braço
        public float armSpeed = 5f;     // Velocidade do braço ao se estender
        public float pullForce = 7f;
        public float range = 3f;        // Alcance máximo do braço
        public bool isFacingRight = true;

        public bool isExtending = false;
        public bool isPulling = false;
        private GameObject grabbedObject = null;
        private Vector3 originalArmPosition;
        private Vector3 armDirection;
       public  bool OnBraco;
      [SerializeField]public SpriteRenderer BRACO;

    PlayerController playerController;
    GameManager gameManager;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
            gameManager = GetComponent<GameManager>();
            originalArmPosition = arm.localPosition;
        ResetArm();
    }

        void Update()
        {
        isFacingRight = transform.localScale.x > 0;

            if (isExtending)
            {
                ExtendArm();
            }
         }

        public void SetArmDirection()
        {
            if (!isExtending && GameManager.instance.esticarBraço)
            {
            armDirection = isFacingRight ? Vector3.right : Vector3.left;
            arm.localPosition = originalArmPosition; // Reseta a posição do braço antes de estender
            isExtending = true;
            BRACO.enabled = true;
            }
        }

         void ExtendArm()
        {
            arm.Translate(Vector3.right * armSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, arm.position) >= range)
            {
                ResetArm();
            }
        }   

        public void ResetArm()
        {
            arm.localPosition = originalArmPosition;
            isExtending = false;
            BRACO.enabled = false;
        }
}
