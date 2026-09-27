using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CinemachineAutoBob : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float walkBobSpeed = 12f;
    public float walkBobAmount = 0.04f;

    [Header("Idle Breathing Settings")]
    public float idleBobSpeed = 1.5f;
    public float idleBobAmount = 0.006f;

    [Header("Run Bobbing Settings")]
    public float runBobSpeed = 16f;
    public float runBobAmount = 0.08f;

    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private CharacterController characterController;
    private Vector3 originalLocalPos;
    private float timer = 0f;

    private float currentBobSpeed;
    private float currentBobAmount;

    void Start()
    {
        
        originalLocalPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (fpsController == null || characterController == null) return;

        // Use characterController for physics data
        bool isMoving = characterController.velocity.magnitude > 0.1f && characterController.isGrounded;

        // Set default target values to Idle
        float targetSpeed = idleBobSpeed;
        float targetAmount = idleBobAmount;

        // Change targets if we are moving
        if (isMoving)
        {
            bool isRunning = characterController.velocity.magnitude > (fpsController.m_WalkSpeed + 0.5f);

            if (isRunning)
            {
                targetSpeed = runBobSpeed;
                targetAmount = runBobAmount;
            }
            else
            {
                targetSpeed = walkBobSpeed;
                targetAmount = walkBobAmount;
            }
        }

        // Changed Time.deltaTime to Time.fixedDeltaTime to match physics
        currentBobSpeed = Mathf.Lerp(currentBobSpeed, targetSpeed, Time.fixedDeltaTime * 5f);
        currentBobAmount = Mathf.Lerp(currentBobAmount, targetAmount, Time.fixedDeltaTime * 5f);

        // Progress timer and apply position strictly on the physics clock
        timer += Time.fixedDeltaTime * currentBobSpeed;
        float newY = originalLocalPos.y + Mathf.Sin(timer) * currentBobAmount;

        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}