using UnityEngine;

namespace BLINK
{
    public class BearEndlessRunnerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public float horizontalSpeed = 5f; // Speed for moving left and right
        public Transform groundCheck;
        public LayerMask groundMask;
        public Transform mainCamera;
        public float cameraFollowSpeed = 5f;
        public float cameraDistance = 5f;
        public float cameraHeight = 2f;

        private Rigidbody rb;
        private bool isGrounded;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true; // To prevent the bear from rotating when hitting objects
        }

        private void Update()
        {
            // Check if the bear is grounded
            isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

            // Handle input for jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void FixedUpdate()
        {
            // Move the bear forward continuously
            Vector3 forwardMove = transform.forward * moveSpeed;

            // Handle horizontal movement input
            float horizontalInput = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                horizontalInput = -horizontalSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalInput = horizontalSpeed;
            }

            Vector3 horizontalMove = transform.right * horizontalInput;
            Vector3 movement = forwardMove + horizontalMove;

            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

            // Update camera position
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            // Calculate the desired camera position
            Vector3 targetCameraPosition = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;

            // Smoothly move the camera towards the desired position
            mainCamera.position = Vector3.Lerp(mainCamera.position, targetCameraPosition, cameraFollowSpeed * Time.deltaTime);
        }
    }
}
