using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement(){
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput < 0)
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0){
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }
}
