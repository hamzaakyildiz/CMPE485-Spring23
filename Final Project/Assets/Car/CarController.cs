using System.Collections;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        private float horizontalInput, verticalInput;
        private float currentSteerAngle, currentbreakForce;
        private bool isBreaking, isJumping, isDrifting, isSlowing;

        // Settings
        [SerializeField] private float motorForce, breakForce, maxSteerAngle;

        // Wheel Colliders
        [SerializeField] private WheelCollider WheelFL, WheelFR;
        [SerializeField] private WheelCollider WheelRL, WheelRR;
        [SerializeField] private Transform[] wheelMeshes;

    
    
        public LayerMask groundLayers;
        public Transform[] groundCheckPoints;
        public float groundCheckDistance = 1;
        public float jumpDelay = 1;
        public bool canJump = true;

        public Vector3 jumpForcePositionOffset = new Vector3(0f, 0f, 0f);
        public float jumpForce = 1000;
    
        [SerializeField]public float maxSpeed = 45f;
        [SerializeField]public float minTurnAngle = 6f;
        [SerializeField]public float maxTurnAngle = 30f;

        private Rigidbody rb;

        public void Start()
        {
            rb = GetComponent<Rigidbody>();
            groundCheckPoints = wheelMeshes;
        }

        private void FixedUpdate() {
            GetInput();
            ApplyTorque();
            Turn();
            RotateWheels();
            Jump();
        }

        private void GetInput() {
            // Steering Input
            horizontalInput = Input.GetAxis("Horizontal");

            // Acceleration Input
            verticalInput = Input.GetAxis("Vertical");

            // Breaking Input
            isBreaking = verticalInput == 0;
        
            // Jumping Input
            isJumping = Input.GetKey(KeyCode.Space);

        
            // Slowing Input
            isSlowing = Vector3.Dot(transform.forward, rb.velocity) * verticalInput < -0.1;

        }

        private void ApplyTorque() {
            if (rb.velocity.magnitude < 30)
            {
                WheelRL.motorTorque = verticalInput * motorForce;
                WheelRR.motorTorque = verticalInput * motorForce;
            }
            else
            {
                WheelRL.motorTorque = 0;
                WheelRR.motorTorque = 0;
            }
            
            if (isSlowing)
            {
                
                WheelRL.brakeTorque = breakForce * 200;
                WheelRR.brakeTorque = breakForce * 200;
            }else if (isBreaking)
            {
                WheelRL.brakeTorque = breakForce;
                WheelRR.brakeTorque = breakForce;
            }
            else
            {
                WheelRL.brakeTorque = 0;
                WheelRR.brakeTorque = 0 ;
            }
        }
    

        private void Turn() {
            float forwardSpeed = Vector3.Dot(rb.velocity, transform.forward);
            float speedPercent = forwardSpeed / maxSpeed;
            float turnAngle = Mathf.Lerp(maxTurnAngle, minTurnAngle, speedPercent);
            currentSteerAngle = horizontalInput * turnAngle;
        
            WheelFL.steerAngle = currentSteerAngle;
            WheelFR.steerAngle = currentSteerAngle;
        }

        private void RotateWheels() {
            RotateWheel(WheelFL, wheelMeshes[0]);
            RotateWheel(WheelFR, wheelMeshes[1]);
            RotateWheel(WheelRL, wheelMeshes[2]);
            RotateWheel(WheelRR, wheelMeshes[3]);
        }

        private void RotateWheel(WheelCollider wheelCollider, Transform wheelTransform) {
            Vector3 pos;
            Quaternion rot; 
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }

        private void Jump()
        {
            if (isJumping && canJump && IsGrounded())
            {
                Vector3 jumpForcePosition = transform.TransformPoint(jumpForcePositionOffset);
                rb.AddForceAtPosition(transform.up * jumpForce, jumpForcePosition, ForceMode.Impulse);
                rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

                canJump = false;
                StartCoroutine(JumpDelay());
            }
        }
        private bool IsGrounded()
        {
            if (groundCheckPoints != null)
            {
                foreach (var point in groundCheckPoints)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(point.position, Vector3.down, out hit, groundCheckDistance, groundLayers))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    
        private IEnumerator JumpDelay()
        {
            yield return new WaitForSeconds(jumpDelay);
            canJump = true;
            rb.constraints = RigidbodyConstraints.None;
        }
    
    
    }
}