using UnityEngine;

namespace Car
{
    public class Player : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        public static float CurrentSpeed;

        public CarController motor;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            motor.enabled = false;
            UIManager.LevelStarted += EnableMotor;
        }

        void Update()
        {
            CurrentSpeed = _rigidbody.velocity.magnitude;

            if (transform.position.y <= 0)
            {
                UIManager.instance.OpenPanel("LosePanel");
            }
        }

        public void EnableMotor()
        {
            if (motor != null)
            {
                motor.enabled = true;

            }
        }
    
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Finish") && _rigidbody.velocity.magnitude <= .5f)
            {
                UIManager.instance.OpenPanel("WinPanel");
            }
        }
    }
}
