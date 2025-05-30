using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyToggler : MonoBehaviour
{
    void Start()
    {
        // Get all Rigidbody components in the scene
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        // Set all Rigidbody components to be kinematic (i.e. not affected by physics)
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        // Call the EnableRigidbodies function after 1 second
        Invoke("EnableRigidbodies", 1f);
    }

    void EnableRigidbodies()
    {
        // Get all Rigidbody components in the scene
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        // Set all Rigidbody components to be affected by physics
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
    }
}
