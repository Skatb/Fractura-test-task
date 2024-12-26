using UnityEngine;

public class RoadMove : MonoBehaviour
{
    [SerializeField] private float roadMovingSpeed = 4f;
    void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            transform.position += new Vector3(0, 0, -roadMovingSpeed) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
