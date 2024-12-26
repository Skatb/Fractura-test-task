using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float swayAmount = 2f;
    [SerializeField] private float swaySpeed = 2f;
    [SerializeField] private float swayStartTime = 2f;
    [SerializeField] private GameObject turretTransform;
    [SerializeField] private SwipeDetection swipeDetection;

    [HideInInspector] public bool startedGame = false;

    private bool shouldMove = false;
    private float movementOffset = 0f;
    private bool isSwiping = false;

    private float targetYRotation = 0f;
    [SerializeField] private float rotationSpeed;
    private Quaternion initialRotation;
    private void Start()
    {
        transform.position = startPosition;
        initialRotation = turretTransform.transform.localRotation;
    }
    private float elapsedTime = 0f;

    void Update()
    {
        if (startedGame)
        {
            elapsedTime += Time.deltaTime;
            HandleMovement();
            HandleTurretRotation();
        }
    }

    private void HandleMovement()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        if (elapsedTime >= swayStartTime)
        {
            movementOffset = Mathf.Sin((elapsedTime - swayStartTime) * swaySpeed) * swayAmount;
            transform.position = new Vector3(startPosition.x + movementOffset, transform.position.y, transform.position.z);
        }
    }
    private void HandleTurretRotation()
    {
        if (swipeDetection.swiping)
        {
            float rotationDelta = 0f;

            if (!swipeDetection.swipeDirection)
            {
                rotationDelta = 1f; // Поворот вправо
            }
            else if (swipeDetection.swipeDirection)
            {
                rotationDelta = -1f; // Поворот влево
            }

            targetYRotation = Mathf.Clamp(targetYRotation + rotationDelta * rotationSpeed, -30f, 30f);
        }

        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, targetYRotation);

        turretTransform.transform.localRotation = targetRotation;
    }
}