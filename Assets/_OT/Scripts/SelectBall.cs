using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBall : MonoBehaviour
{
    public int ballIndex;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SelectBallIndex(int ballIndex)
    {
        this.ballIndex = ballIndex;
    }
}
