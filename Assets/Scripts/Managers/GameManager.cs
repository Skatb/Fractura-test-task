using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private CameraController mainCamera;
    private bool isGameStarted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            isGameStarted = true;
            mainCamera.isTransitioning = true;
            Debug.Log("Game Started!");
        }
    }
}
