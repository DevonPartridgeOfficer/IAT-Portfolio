/*  Filename: HayMachine.cs
 *   Purpose: Controls the deleting/creation of new haymachine at game start
 *            Handles player input for movement/shooting
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    //Haymachine objects
    public Transform modelParent;
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;
    //Haybale objects
    public Transform haySpawnpoint;
    public GameObject hayBalePrefab;
    //Movement Variables
    public float movementSpeed;
    public float horizontalBoundary = 22; //Keep in world boundary
    //Shooting Variables
    public float shootInterval;
    private float shootTimer;

    // Calls for new haymachine model to be created at gamestart
    void Start()
    {
        LoadModel();
    }

    // Updates haymachine depending on player input
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }

    //Deletes any existing haymachine
    //loads a new model with selected settings colour 
    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject);

        switch (GameSettings.hayMachineColor)
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }

    //Checks that the input direction doen't exceed the boundary
    //Updates movement at a constant speed over time
    private void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary)
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary)
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }

    //Checks that the player can shoot again
    //If so, shoot a haybale and reset the shoot interval timer
    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }

    //Instantiates haybale prefab and shoots from haymachine with sound
    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();
    }
}
