using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		private Rigidbody _rb;
		private float _currentSpeed;
		private bool _isGrounded;

		private readonly float _jumpForce = 250f;


		[HideInInspector] public float forwardInput;
		[HideInInspector] public float horizontalInput;
		
		
		[HideInInspector]
		public  Animations playerAnimations;

		private Transform _heldObject;
		private bool _isHolding = false;

		private void Awake()
		{
			playerAnimations = GetComponentInChildren<Animations>();
		}
		// Start is called before the first frame update
		private void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_currentSpeed = 5.0f;
			
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
			
			Vector3 directionVector = new Vector3(horizontalInput, 0, forwardInput);
			directionVector.Normalize();

			transform.Translate(directionVector * (_currentSpeed * Time.deltaTime),Space.World);
			
			if (directionVector.magnitude != 0)
			{
				Quaternion toRotation = Quaternion.LookRotation(directionVector, Vector3.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1000f * Time.deltaTime);
			}

			
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

			if (collision.transform.CompareTag("Holdable"))
			{
				_heldObject = collision.transform;
			}
			
		}
		
		
		private void OnCollisionStay(Collision collision)
		{
			if (_isHolding && Input.GetKey(KeyCode.E))
			{
				_heldObject.GetComponent<Rigidbody>().isKinematic = false;
				//_heldObject.GetComponent<Collider>().enabled = true;
				_heldObject.transform.parent = null;
				_isHolding = false;

			}
			else if (_heldObject != null && Input.GetKey(KeyCode.E))
			{
				_heldObject.GetComponent<Rigidbody>().isKinematic = true;
				_heldObject.GetComponent<Collider>().enabled = false;
				_heldObject.transform.parent = transform;
				_heldObject.transform.localPosition = new Vector3(0, 0, 1);
				_isHolding = true;

			}
	
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
