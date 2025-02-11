using UnityEngine;
using System.Collections; // Required for IEnumerator

// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 200.0f; // Set player's rotation speed.
    public float jumpForce = 5.0f; // Set player's jump force.
    public float jumpCooldown = 1.0f; // Set the cooldown time between jumps.

    private Rigidbody rb; // Reference to player's Rigidbody.
    private Animator animator; // Reference to player's Animator.
    private bool canJump = true; // Flag to control jump cooldown.

    public Camera mainCamera; // Reference to the main camera
    public Camera frontCamera; // Reference to the front camera

    private KeyCode runKey;
    private KeyCode jumpKey;
    private KeyCode lookBackKey;

    public GameObject mainMenu;
    public GameObject optionsPanel;

    public Vector3 respawnPosition = new Vector3(-10.527f, 0.037f, -10.934f);

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
        animator = GetComponent<Animator>(); // Access player's Animator.
    }

    // Update is called once per frame
    private void Update()
    {
        // Open and Close Main Menu
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsPanel.activeSelf)
        {
            mainMenu.gameObject.SetActive(!mainMenu.gameObject.activeSelf);
        }

        // Stop player actions if main menu or options panel is active
        if (mainMenu.activeSelf || optionsPanel.activeSelf)
        {
            return;
        }

        runKey = GetKeyCodeFromPlayerPrefs("run");
        jumpKey = GetKeyCodeFromPlayerPrefs("jump");
        lookBackKey = GetKeyCodeFromPlayerPrefs("lookback");

        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            StartCoroutine(JumpCooldown());
        }

        if (Input.GetKeyDown(lookBackKey))
        {
            SwitchToFrontCamera(false, true);
        }
        else if (Input.GetKeyUp(lookBackKey))
        {
            SwitchToFrontCamera(true, false);
        }

        // Update the isWalking parameter based on player movement input
        float moveVertical = Input.GetAxis("Vertical");
        bool isWalking = Mathf.Abs(moveVertical) > 0.1f; // Adjust threshold as needed

        // Animations
        if (isWalking == false)
        {
            // Idle
            animator.SetFloat("Speed", 0);
        }
        else if (!Input.GetKey(runKey) || Input.GetKeyUp(runKey))
        {
            // Walk
            animator.SetFloat("Speed", 0.3f);
            speed = 1f;
        }
        else if (Input.GetKey(runKey)) // Input.GetKey(KeyCode.LeftShift)
        {
            // Run
            animator.SetFloat("Speed", 1);
            speed = 2f;
        }
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Stop player actions if main menu or options panel is active
        if (mainMenu.activeSelf || optionsPanel.activeSelf)
        {
            return;
        }

        // Move player based on vertical input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void SwitchToFrontCamera(bool mainCameraOn, bool frontCameraOn)
    {
        if (frontCamera != null && mainCamera != null)
        {
            mainCamera.gameObject.SetActive(mainCameraOn); // Disable the main camera
            frontCamera.gameObject.SetActive(frontCameraOn); // Enable the front camera
        }
        else
        {
            Debug.LogError("Main camera or front camera reference is not assigned!");
        }
    }

    KeyCode GetKeyCodeFromPlayerPrefs(string keyName)
    {
        string keyString = PlayerPrefs.GetString(keyName);

        if (keyString.EndsWith("-Key"))
        {
            keyString = keyString.Substring(0, keyString.Length - 4); // Remove the last 4 characters ("-Key")
        }

        return (KeyCode)System.Enum.Parse(typeof(KeyCode), keyString);
    }

    public void RespawnPlayer()
    {
        //Respawn at start coordinates
        transform.position = respawnPosition;
    }
}
