using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    Vector3 lastPosition;
    Quaternion lastRotation;
    int framesWithoutMoving;

    Vector3 startingPosition;
    Quaternion startingRotation;
    Rigidbody pinRb;

    private void Awake()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        pinRb = this.GetComponent<Rigidbody>();
    }
    public bool DidPinFall { get; private set; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pit"))
        {
            var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.PinKnockedDown();
            Debug.Log("Pin Entered Into Pit");
            Destroy(this.gameObject);
            return;
        }
        else  if(other.gameObject.CompareTag("BowlingTrack"))
        {
            DidPinFall = true;
            Debug.Log("Pin has Fallen");
        }
    }
    public bool DidPinMove()
    {
        var didPinMove = (transform.position - lastPosition).magnitude > 0.0001f 
            || Quaternion.Angle(transform.rotation, lastRotation) > 0.001f;
        lastPosition = transform.position;
        lastRotation = transform.rotation;

        //if (didBallMove)
        //    framesWithoutMoving = 0;
        //else
        //    framesWithoutMoving += 1;

        framesWithoutMoving = didPinMove ? 0 : framesWithoutMoving + 1;

        return framesWithoutMoving <= 10;
    }

    public void ResetPosition()
    {
        pinRb.position = startingPosition;
        pinRb.rotation = startingRotation;

        pinRb.velocity = Vector3.zero;
        pinRb.angularVelocity = Vector3.zero;

        lastPosition = startingPosition;
        lastRotation = startingRotation;
    }
}
