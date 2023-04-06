using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform playerPosition;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        if (playerPosition == null)
            return;

        transform.position = playerPosition.position + offset;
        transform.LookAt(playerPosition);
    }
}
