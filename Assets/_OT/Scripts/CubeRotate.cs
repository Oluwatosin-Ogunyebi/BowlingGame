using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    private float _cubeSpeed;
    private float _normalSpeed = 2f;
    private float _sprintSpeed = 20f;
    
    void Update()
    {
        PlayerMovement();
    }
    private void FixedUpdate()
    {
        
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _cubeSpeed = _sprintSpeed;
        }
        else
        {
            _cubeSpeed = _normalSpeed;
        }
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * _cubeSpeed * Time.deltaTime);

        Debug.Log("The speed is currently: " + _cubeSpeed);
    }

    //private void OnEnable()
    //{
    //    _cubeSpeed++;
    //    Debug.Log("The speed is currently: " + _cubeSpeed);
    //}

}
