using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    // Rigidbody component of the cube object
    Rigidbody rb;
    
    // Transform component of the object used to control the cube
    public Transform controller;
    
    // Amount of rotation to apply to the cube
    [Range(0.0f, 360.0f)]
    public float rotateBy = 50f;

    // Array of game objects with the "deko" tag
    GameObject[] objs;
    
    // Direction to move the cube in
    public Vector3 direction;

    // Save the previous position of the controller
    Vector3 previousControllerPosition;

    void Start()
    {
        // Get the Rigidbody component of the cube object
        rb = GetComponent<Rigidbody>();
        
        // Get all game objects with the "deko" tag
        objs = GameObject.FindGameObjectsWithTag("deko");

        // Set the initial previous position of the controller
        previousControllerPosition = controller.position;
    }

    void FixedUpdate()
    {
        // Set the position of the cube to the position of the controller
        rb.MovePosition(controller.position + controller.TransformDirection(new Vector3(0f, 0f, -0.075f)));

        // Set the rotation of the cube to the rotation of the controller
        rb.MoveRotation(controller.rotation * Quaternion.Euler(rotateBy, 0, 0));

        // Calculate the difference between the previous and current positions of the controller
        Vector3 movement = controller.position * 100 - previousControllerPosition * 100;

        // Calculate the direction to move the cube in
        direction = ((controller.rotation * Quaternion.Euler(rotateBy, 0, 0)) * Vector3.up) * 2;

        // Save the current position of the controller as the previous position
        previousControllerPosition = controller.position;

        foreach (GameObject deko in objs)
        {
            // Get the Rigidbody component of the deko object
            Rigidbody dekoRb = deko.GetComponent<Rigidbody>();

            // If the Rigidbody component exists, add force to it in the direction of the controller's movement
            // and the object's default direction
            if (dekoRb != null)
            {
                dekoRb.AddForce(movement * dekoRb.mass * 5 + direction * dekoRb.mass);
            }
        }
    }
}
