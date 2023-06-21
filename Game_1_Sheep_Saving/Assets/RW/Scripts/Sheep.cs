/*  Filename: Sheep.cs
 *   Purpose: Used to create and manage sheep collision
 *            Updates movement and checks if hit/dropped
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    //Start Variables
    private Collider myCollider;
    private Rigidbody myRigidbody;
    //Sheep spawner
    private SheepSpawner sheepSpawner;
    //Hit by hay variables
    private bool hitByHay;
    public float gotHayDestroyDelay;
    public float runSpeed;
    public float heartOffset;
    public GameObject heartPrefab;
    //Dropped variables
    private bool dropped;
    public float dropDestroyDelay;

    // Gets the collider and rigidbody of sheep gameobject
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Moves the sheep foward at a constant runspeed over time
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    //Checks if haybale has collided with sheep or sheep has dropped off edge, calls appropriate functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep") && !dropped)
        {
            Drop();
        }
    }

    //Removes sheep from spawner list and stops movement
    //Spawns a heart after sheep hit that rotates and shrinks
    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;

        Destroy(gameObject, gotHayDestroyDelay);
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);

        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0; 
        tweenScale.timeToReachTarget = gotHayDestroyDelay;

        //Play sound and add to onscreen counter
        SoundManager.Instance.PlaySheepHitClip();
        GameStateManager.Instance.SavedSheep();
    }


    //After sheep reaches edge, add gravity, destroy object
    private void Drop()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        dropped = true;

        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);

        //Increment 'fail' counter and play sound
        GameStateManager.Instance.DroppedSheep();
        SoundManager.Instance.PlaySheepDroppedClip();
    }

    //Used by SheepSpawner.cs to spawn sheep in spawner locations
    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
}
