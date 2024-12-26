using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("Aggro Triggered");
            aggroCollider.enabled = false;
            GetComponentInParent<Enemy>().SetAggro(other.transform);
        }
    }
}
