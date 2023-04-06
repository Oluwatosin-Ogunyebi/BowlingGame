using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    Vector3 lastPosition;
    Quaternion lastRotation;
    int framesWithoutMoving;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BowlingTrack"))
        {
            Debug.Log("Ball Hit the bowling track");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pit"))
        {
            Debug.Log("Ball entered pit");
            var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.BallKnockedDown();
            Destroy(this.gameObject,1f);
        }
    }

    public bool DidBallMove()
    {
        var didBallMove = (transform.position - lastPosition).magnitude > 0.0001f || Quaternion.Angle(transform.rotation, lastRotation) > 0.001f;
        lastPosition = transform.position;
        lastRotation = transform.rotation;

        //if (didBallMove)
        //    framesWithoutMoving = 0;
        //else
        //    framesWithoutMoving += 1;

        framesWithoutMoving = didBallMove ? 0 : framesWithoutMoving + 1;

        return framesWithoutMoving <= 10;
    }

}
