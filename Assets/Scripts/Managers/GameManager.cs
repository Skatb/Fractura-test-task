using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private CameraController mainCamera;
    [HideInInspector] public bool isGameStarted;

    public GameUI gameUI;
    [SerializeField] float gameDuration;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mainCamera = Camera.main.GetComponent<CameraController>();
    }
    private void Update()
    {
        if (!isGameStarted && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        if (!isGameStarted)
        {
            mainCamera.ChangeCameraPosition();

            gameUI.timeRemaining = gameDuration;
            gameUI.StartGame();
        }
    }
}