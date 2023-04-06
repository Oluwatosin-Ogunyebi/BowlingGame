using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    Animator _anim;

    private void Awake()
    {
        _anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            _anim.SetTrigger("Wave");
        }
    }
}
