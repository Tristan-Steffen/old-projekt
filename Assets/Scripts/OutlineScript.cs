using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OutlineScript : MonoBehaviour
{
    // The material to use for the semi-transparent copies
    public Material copyMaterial;

    float timer;
    
    GameObject[] dekoObjects;

    void Start()
    {
        // Find all objects with the "deko" tag
        dekoObjects = GameObject.FindGameObjectsWithTag("deko");

        // Create a semi-transparent copy of each object
        foreach (GameObject deko in dekoObjects)
        {
            GameObject copy = Instantiate(deko, deko.transform.position, deko.transform.rotation);
            copy.gameObject.tag = "copy";

            Rigidbody rb = copy.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Destroy(rb);
            }
            MeshCollider collider = copy.GetComponent<MeshCollider>();
            if (collider != null)
            {
                Destroy(collider);
            }

            // Recursively apply the material to all child objects with mesh rendererss
            ApplyMaterialRecursive(copy, copyMaterial);
        }
    }

    // Recursive function to apply the given material to all child objects with mesh renderers
    void ApplyMaterialRecursive(GameObject obj, Material material)
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = material;
            renderer.material.color = new Color(1, 1, 1, 0.5f);
        }

        // Destroy the rigidbody component if it exists
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Destroy(rb);
        }

        // Destroy the collider component if it exists
        MeshCollider collider = obj.GetComponent<MeshCollider>();
        if (collider != null)
        {
            Destroy(collider);
        }

        // Recursively call this function for all child objects
        foreach (Transform child in obj.transform)
        {
            ApplyMaterialRecursive(child.gameObject, material);
        }
    }

    void SnapToCopy(GameObject dekoObject, float snapDistance)
    {
        // Get the first 4 letters of the deko object's name
        string dekoName = dekoObject.name.Substring(0, 4);
    
        // Find all objects in the scene with a name starting with the deko name
        GameObject[] allCopies = GameObject.FindGameObjectsWithTag("copy");
        GameObject[] copyCandidates = allCopies.Where(obj => obj.name.StartsWith(dekoName)).ToArray();
    
        // Iterate over the candidates and find the one that is within snap distance
        foreach (GameObject candidate in copyCandidates)
        {
            float distance = Vector3.Distance(dekoObject.transform.position, candidate.transform.position);
            if (distance <= snapDistance)
            {
                // Snap the deko object to the copy's position and rotation
                dekoObject.transform.position = candidate.transform.position;
                dekoObject.transform.rotation = candidate.transform.rotation;
    
                // Remove the rigidbody component from the deko object
                Rigidbody rb = dekoObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                }
    
                // Destroy the copy object
                Destroy(candidate);
                dekoObject.gameObject.tag = "Untagged";
                break;
            }
        }
    }

    void Update()
    {
        // Increment the timer by the time elapsed since the last frame
        timer += Time.deltaTime;

        // If the timer has reached 5 seconds, call the SnapToCopy function for each element in the dekoObjects array
        if (timer >= 5f)
        {
            foreach (GameObject dekoObject in dekoObjects)
            {
                SnapToCopy(dekoObject, 0.5f);
            }
        }
    }
}
