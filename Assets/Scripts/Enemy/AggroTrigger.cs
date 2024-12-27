using UnityEngine;

public class AggroTrigger : MonoBehaviour
{
    [SerializeField] BoxCollider aggroCollider;

    private void Start()
    {
        aggroCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aggroCollider.enabled = false;
            GetComponentInParent<Enemy>().SetAggro(other.transform);
        }
    }
}
