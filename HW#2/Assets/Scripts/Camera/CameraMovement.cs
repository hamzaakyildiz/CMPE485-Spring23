using UnityEngine;

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private float mouseSensitivity;
        [SerializeField]
        private Vector3 followOffset = new Vector3(0,-2,-5);

        private Vector3 _offset;
        
        private Vector3 _playerPosition;

        //Note: Don't bother with vector rotation for rotatable cameras, use an r-theta polar coordinate system
        private void Start()
        {
            _offset = transform.position - playerTransform.position;
            if (playerTransform != null)
                _playerPosition = playerTransform.position;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (!playerTransform) return;
            _offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * mouseSensitivity, Vector3.up) * _offset;
            transform.position = _playerPosition + _offset - followOffset;
            transform.LookAt(_playerPosition);
        }
    }
}