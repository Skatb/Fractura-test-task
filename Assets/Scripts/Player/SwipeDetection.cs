using UnityEngine;
//using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    private Touch oneTouch, touchToMove;
    // [SerializeField] InputActionAsset swipeInputActions;
    [SerializeField] float maxSwipeTime = 0.5f;
    [SerializeField] float minSwipeDistance = 100f;

    public bool swiping = false;
    public float swipeAngle = 0f;

    Vector2 swipeStartPosition;
    Vector2 swipeEndPosition;

    float swipeStartTime;
    float swipeEndTime;

    public bool swipeDirection;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                oneTouch = Input.GetTouch(0);

                if (oneTouch.position.x < Screen.width / 2)
                {
                    if (oneTouch.phase == TouchPhase.Moved)
                    {
                        transform.Rotate(0f, 0f, -oneTouch.deltaPosition.x * Time.deltaTime * 0.1f);
                    }
                }
            }
        }
    }
    /*   private void OnEnable()
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
           swiping = true;
           swipeStartPosition = swipeInputActions.FindAction("TouchPosition").ReadValue<Vector2>();
           swipeStartTime = Time.time;
       }
       void SwipeEnd(InputAction.CallbackContext context)
       {
           if (swiping)
           {
               swipeEndPosition = swipeInputActions.FindAction("TouchPosition").ReadValue<Vector2>();
               Vector2 swipeDelta = swipeEndPosition - swipeStartPosition;

               if (Mathf.Abs(swipeDelta.x) >= minSwipeDistance)
               {
                   if (swipeDelta.x > 0)
                   {
                       swipeDirection = true;
                       Debug.Log("Swipe Right");
                   }
                   else
                   {
                       swipeDirection = false;
                       Debug.Log("Swipe Left");
                   }
               }
           }

           swiping = false;
           swipeEndTime = Time.time;
       }*/
}
