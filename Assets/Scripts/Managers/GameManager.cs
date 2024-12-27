using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private CameraController mainCamera;
    [HideInInspector] public bool isGameStarted;

    public GameUI gameUI;
    [SerializeField] float gameDuration;

    private bool touchToStart = false;
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
        if (!isGameStarted && !touchToStart && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            touchToStart = true;
            StartGame();
        }
    }
    public void StartGame()
    {
        if (!isGameStarted)
        {
            mainCamera.ChangeCameraPosition();
        }
    }
    public IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(1f);
        mainCamera.shouldLeadForPlayer = true;
        isGameStarted = true;
        gameUI.StartCounter(gameDuration);
    }
}