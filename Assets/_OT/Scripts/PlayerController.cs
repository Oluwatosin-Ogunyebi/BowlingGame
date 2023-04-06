using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _aimDirection;
    [SerializeField] private Animator _animDirection;
    [SerializeField] private float _throwSpeed;
    [SerializeField] private List<Rigidbody> _bowlingBallPrefabs;
    [SerializeField] private FollowTarget _followTarget;
    [SerializeField] private GameManager _gameManager;

    public bool wasBallThrown;

    private Rigidbody _bowlingBallPrefabInstance;

    private int ballIndex;
    // Start is called before the first frame update
    void Start()
    {
        //ballIndex = FindObjectOfType<SelectBall>().ballIndex;
    }

    public void StartAiming()
    {
        _animDirection.SetBool("Aiming", true);
        wasBallThrown = false;
        _followTarget.playerPosition = transform;
    }
    // Update is called once per frame
    void Update()
    {
        TryThrowBall();

    }

    void TryThrowBall()
    {
        if (wasBallThrown || !Input.GetButtonDown("Fire1")) return;

        wasBallThrown = true;
        _bowlingBallPrefabInstance = Instantiate(_bowlingBallPrefabs[ballIndex], transform.position, transform.rotation);
        _bowlingBallPrefabInstance.AddForce((_aimDirection.forward * -1) * _throwSpeed, ForceMode.Impulse);
        _followTarget.playerPosition = _bowlingBallPrefabInstance.transform;
        _gameManager.BallThrown(_bowlingBallPrefabInstance.GetComponent<Ball>());
        _animDirection.SetBool("Aiming", false);
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // _ballRb.AddForce((_aimDirection.forward * -1) * _throwSpeed, ForceMode.Impulse);
        }
    }

    private int RandomNumber()
    {
        int randomNum = Random.Range(0, _bowlingBallPrefabs.Count);
        return randomNum;
    }

}
