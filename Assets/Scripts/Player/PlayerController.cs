using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float swayAmount = 2f;
    [SerializeField] private float swaySpeed = 2f;
    [SerializeField] private float swayStartTime = 2f;
    [SerializeField] private GameObject turretTransform;
    [SerializeField] private SwipeDetection swipeDetection;

    [HideInInspector] public bool startedGame = false;

    private float movementOffset = 0f;
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
        }
    }

    private void HandleMovement()
    {
        if (elapsedTime >= swayStartTime)
        {
            movementOffset = Mathf.Sin((elapsedTime - swayStartTime) * swaySpeed) * swayAmount;
            transform.position = new Vector3(startPosition.x + movementOffset, transform.position.y, transform.position.z);
        }
    }
    public void HandleTurretRotation()
    {
        if (swipeDetection.swiping && GameManager.Instance.isGameStarted)
        {
            float rotationDelta = swipeDetection.swipeDirection ? -1f : 1f; 
            float newZRotation = turretTransform.transform.localRotation.eulerAngles.z + rotationDelta * rotationSpeed * Time.deltaTime;

            newZRotation = (newZRotation > 180f) ? newZRotation - 360f : newZRotation;

            turretTransform.transform.localRotation = Quaternion.Euler(turretTransform.transform.localEulerAngles.x, turretTransform.transform.localEulerAngles.y, -newZRotation * rotationSpeed);
        }
    }
}