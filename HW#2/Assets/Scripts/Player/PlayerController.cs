using UnityEngine;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		private Rigidbody _rb;
		private float _currentSpeed;
		private bool _isGrounded;

		private readonly float _jumpForce = 250f;
		
		
		[HideInInspector]
		public float forwardInput = 0;
		[HideInInspector]
		public float horizontalInput = 0;



		// Start is called before the first frame update
		private void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_currentSpeed = 300.0f;
		}

		// Update is called once per frame
		private void Update()
		{
			if (!Input.GetKeyDown(KeyCode.R)) return;
			_rb.velocity = Vector3.zero;
			_rb.angularVelocity = Vector3.zero;
		}
		

		public void Move()
		{
			forwardInput = Input.GetAxis("Vertical");
			horizontalInput = Input.GetAxis("Horizontal");
			
			Vector3 directionVector = new Vector3(forwardInput, 0, horizontalInput);
			directionVector.Normalize();
			
			
			Vector3 vel = ((transform.forward * forwardInput) + (transform.right * horizontalInput)) * _currentSpeed / 100f;
			_rb.velocity = new Vector3(vel.x, _rb.velocity.y, vel.z);

		}
		
		public void Jump()
		{
			Vector3 rbVelocity = _rb.velocity;
			_rb.velocity = new Vector3(rbVelocity.x, 0, rbVelocity.z);
			_rb.AddForce(new Vector3(0, _jumpForce, 0));

		}

		private void OnCollisionEnter(Collision collision)
		{
			_isGrounded = collision.gameObject.layer.Equals(6);
		}

		public bool IsJumping()
		{
			_isGrounded = !Input.GetKeyDown(KeyCode.Space);
			return !_isGrounded;
		}
		
		public bool IsGrounded()
		{
			return _isGrounded;
		}
		public bool IsMoving()
		{
			Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			return directionVector.magnitude > 0;
		}
		public void AddGravity(float force)
		{
			_rb.AddForce(new Vector3(0, -force, 0));
		}
		public float GetVerticalVelocity()
		{
			return _rb.velocity.y;
		}
		

		

	}
}
