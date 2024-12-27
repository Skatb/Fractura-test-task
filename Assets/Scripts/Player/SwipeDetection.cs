using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    public PlayerController playerController;
    private Touch oneTouch, touchToMove;
    [SerializeField] InputActionAsset swipeInputActions;
    [SerializeField] float minSwipeDistance = 100f;

    public bool swiping = false;
    public float swipeAngle = 0f;

    Vector2 swipeStartPosition;
    Vector2 swipeEndPosition;

    float swipeStartTime;
    float swipeEndTime;

    public bool swipeDirection;

    private void OnEnable()
    {
        swipeInputActions.Enable();
    }

    private void OnDisable()
    {
        swipeInputActions.Disable();
    }
    void Start()
    {
        swipeInputActions.FindAction("Touch").performed += SwipeStart;
        swipeInputActions.FindAction("Touch").canceled += SwipeEnd;
    }

    void SwipeStart(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.isGameStarted)
        {
            swiping = true;
            swipeStartPosition = swipeInputActions.FindAction("TouchPosition").ReadValue<Vector2>();
            swipeStartTime = Time.time;
        }
    }
    void SwipeEnd(InputAction.CallbackContext context)
    {
        if (swiping && GameManager.Instance.isGameStarted)
        {
            swipeEndPosition = swipeInputActions.FindAction("TouchPosition").ReadValue<Vector2>();

            Vector2 swipeDelta = swipeEndPosition - swipeStartPosition;
            
            if (Mathf.Abs(swipeDelta.x) >= minSwipeDistance)
            {
                if (swipeDelta.x > 0)
                {
                    swipeDirection = true;
                }
                else
                {
                    swipeDirection = false;
                }
                playerController.HandleTurretRotation();
            }
        }

        swiping = false;
        swipeEndTime = Time.time;
    }
}