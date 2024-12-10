using UnityEngine;

namespace Unity.FPS.Game
{
    public class FirstPersonGun : MonoBehaviour
    {
        [Header("Gun Settings")]
        [SerializeField] Transform gunBarrel;        // Reference to the gun barrel (or where the bullet comes from)
        [SerializeField] float rotationSpeed = 5f;   // Speed of gun rotation (mouse sensitivity)

        [Header("Shooting Settings")]
        [SerializeField] GameObject bulletPrefab;    // Bullet prefab to instantiate
        [SerializeField] Transform bulletSpawnPoint; // Where bullets spawn from (usually at the gun barrel)
        [SerializeField] float bulletSpeed = 10f;    // Speed of the bullet

        private float currentRotationX = 0f; // Current horizontal rotation (left-right) of the gun

        private Transform m_PlayerTransform; // Reference to the player or camera

        // Start is called before the first frame update
        void Start()
        {
            // Directly reference the player's camera as the main transform
            m_PlayerTransform = Camera.main?.transform;

            if (m_PlayerTransform == null)
            {
                Debug.LogError("Player transform not found! Please make sure the player object is assigned.");
                enabled = false;  // Disable the script if no player transform is available
                return;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Rotate the gun based on horizontal mouse movement (Mouse X)
            RotateGun();

            // Handle shooting input (e.g., left mouse button or another configured button)
            if (Input.GetButtonDown("Fire1")) // Fire1 is typically mapped to the left mouse button
            {
                ShootBullet();
            }
        }

        // LateUpdate to ensure the gun follows the player after all other updates
        void LateUpdate()
        {
            // Keep the gun's position relative to the player, maintaining the initial offset
            if (m_PlayerTransform != null)
            {
                transform.position = m_PlayerTransform.position;
            }
        }

        void RotateGun()
        {
            // Get horizontal mouse movement (Mouse X)
            float mouseX = Input.GetAxis("Mouse X");

            // Apply a smoothing factor to the mouse input to reduce sensitivity
            mouseX *= 0.3f; // Adjust this for your desired sensitivity

            // Update the current rotation (left-right) based on mouseX input
            currentRotationX += mouseX * rotationSpeed;

            // Apply the horizontal rotation to the gun on the Y-axis (left and right)
            // We're rotating the gun around the Y-axis to create horizontal movement.
            transform.localRotation = Quaternion.Euler(0, currentRotationX, 0);
        }

        void ShootBullet()
        {
            // Instantiate a bullet at the spawn point with the rotation of the gun
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Get the Rigidbody component of the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // Apply velocity to the bullet in the forward direction of the spawn point
            rb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;

            // Optionally destroy the bullet after a set time (to prevent too many bullets from accumulating)
            Destroy(bullet, 5f);
        }
    }
}
