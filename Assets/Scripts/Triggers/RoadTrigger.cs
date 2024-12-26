using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    public GameObject roadSection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoadTrigger"))
        {
            Instantiate(roadSection, new Vector3(0, 0, 84f), roadSection.transform.rotation);
        }
    }
}
