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

    PlayerController playerController;

    void Start()
    {
           
            originalArmPosition = arm.localPosition;
       
    }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1); // Vira o player para a esquerda
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1); // Vira o player para a direita
            }
            if (Input.GetKeyDown(KeyCode.J) && !isExtending && !isPulling )
            {
                SetArmDirection();
                isExtending = true;
            }

            if (isExtending)
            {
                ExtendArm();
            }

            if (isPulling && grabbedObject != null)
            {
                PullObject();
            }
         }

        void SetArmDirection()
        {
            armDirection = isFacingRight ? Vector3.right : Vector3.left;
            arm.localPosition = originalArmPosition; // Reseta a posição do braço antes de estender
        }

         void ExtendArm()
        {
            arm.Translate(Vector3.right * armSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, arm.position) >= range)
            {
                ResetArm();
            }
        }

        public void PullObject()
        {
            Vector3 directionToPlayer = (transform.position - grabbedObject.transform.position).normalized;
            grabbedObject.transform.position = Vector3.MoveTowards(grabbedObject.transform.position, transform.position, pullForce * Time.deltaTime);

            if (Vector3.Distance(grabbedObject.transform.position, transform.position) < 0.1f)
            {
                ReleaseObject();
            }
        }
        
        

        public void PullBox(Collider2D other)
        {
            grabbedObject = other.gameObject;
            isExtending = false;
            isPulling = true;
        }

        void ResetArm()
        {
            arm.localPosition = originalArmPosition;
            isExtending = false;
            isPulling = false;
            if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
        }
            grabbedObject = null;
        }

        void ReleaseObject()
        {
            if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject = null;
        }
            ResetArm();
        }

}
