using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Transform startCameraPosition;
    [SerializeField] private Transform cameraCarPosition;

    [SerializeField] private float cameraTransitionSpeed;

    private PlayerController player;
    private Vector3 offset;
    private bool shouldLeadForPlayer;

    public bool isTransitioning = false;
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    void Start()
    {
        mainCamera.transform.position = startCameraPosition.position;
        mainCamera.transform.rotation = startCameraPosition.rotation;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        offset = cameraCarPosition.position - player.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isTransitioning = true;
        }

        if (isTransitioning)
        {
            ChangeCameraPosition();
        }
    }
    void LateUpdate()
    {
        if (shouldLeadForPlayer)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = player.transform.position.z + offset.z;
            transform.position = newPosition;
        }
    }
    public void ChangeCameraPosition()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraCarPosition.position, cameraTransitionSpeed * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, cameraCarPosition.rotation, cameraTransitionSpeed * Time.deltaTime);

        if (Vector3.Distance(mainCamera.transform.position, cameraCarPosition.position) < 0.01f &&
            Quaternion.Angle(mainCamera.transform.rotation, cameraCarPosition.rotation) < 0.1f)
        {
            isTransitioning = false;
            shouldLeadForPlayer = true;

            player.startedGame = true;
        }
    }
}