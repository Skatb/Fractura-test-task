using System.Collections;
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
    void LateUpdate()
    {
        if (shouldLeadForPlayer)
        {
            LeadForPlayer();
        }
    }
    private void LeadForPlayer()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = player.transform.position.z + offset.z;
        transform.position = newPosition;
    }
    public void ChangeCameraPosition()
    {
        StartCoroutine(ChangeCameraPositionCoroutine());
    }
    private IEnumerator ChangeCameraPositionCoroutine()
    {
        while (Vector3.Distance(mainCamera.transform.position, cameraCarPosition.position) >= 0.01f ||
               Quaternion.Angle(mainCamera.transform.rotation, cameraCarPosition.rotation) >= 0.1f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraCarPosition.position, cameraTransitionSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, cameraCarPosition.rotation, cameraTransitionSpeed * Time.deltaTime);
            yield return null;
        }
        yield return StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        shouldLeadForPlayer = true;
        GameManager.Instance.isGameStarted = true;
    }
}
