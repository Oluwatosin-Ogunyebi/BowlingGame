using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    PlayerController playerController;

    private Pin[] _currentPins = new Pin[0];
    private Ball _currentBall;

    [SerializeField] private Transform _pinSetSpawnPosition;
    [SerializeField] private GameObject _pinSetPrefab;

    private bool _throwStarted;
    private int _throwNumber;

    [Header("UI Text Fields")]
    [SerializeField] private TMP_Text _frameNumber;
    [SerializeField] private TMP_Text _firstThrowScore;
    [SerializeField] private TMP_Text _secondThrowScore;
    [SerializeField] private TMP_Text _frametotalScore;

    private int _currentFrameScore;
    private int _currentThrowScore;
    private int _currentFrame;
    private int _totalScore;

    private float remainingTimeout;
    private float throwTimeout = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        Invoke(nameof(SetupFrame), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_throwStarted || !playerController.wasBallThrown)
            return;
        remainingTimeout -= Time.deltaTime;
        if (remainingTimeout <= 0 || CheckIfPiecesAreStatic())
            FinishThrow();
    }

    public void UpdateScoreUI()
    {
        if (_throwNumber == 0)
        {
            if (_currentFrameScore == 10)
                _secondThrowScore.text = "X";
            else
                _firstThrowScore.text = _currentFrameScore.ToString();
        }
        else
        {
            _secondThrowScore.text = _currentFrameScore == 10 ? "/" : _currentThrowScore.ToString();
        }

    }

    private void ResetScoreUI()
    {
        _frameNumber.text = _currentFrame.ToString();
        _firstThrowScore.text = "";
        _secondThrowScore.text = "";
        _frametotalScore.text = _totalScore.ToString();
    }

    public void PinKnockedDown()
    {
        _currentFrameScore++;
        _currentThrowScore++;
        Debug.Log("Current Score is: "+_currentFrameScore);
        //Called when each pin is hit and pin head comes in contact with pit
    }

    public void BallKnockedDown()
    {
        _currentBall = null;
    }

    public void BallThrown(Ball ball)
    {
        _currentBall = ball;
    }

    private  bool CheckIfPiecesAreStatic()
    {
        foreach (var pin in _currentPins)
        {
            if (pin != null && pin.DidPinMove())
            {
                return false;
            }
        }
        var ballStatus = _currentBall == null || !_currentBall.DidBallMove();
        return ballStatus; //Checks for Pin Movement
    }

    private void SetupFrame()
    {
        _throwNumber = _currentFrameScore = 0;
        DisposeLastFrame();
        Instantiate(_pinSetPrefab, _pinSetSpawnPosition.position, _pinSetSpawnPosition.rotation);
        _currentPins = FindObjectsOfType<Pin>();

        _currentFrame++;
        ResetScoreUI();
        SetupThrow();
    }

    private void FinishThrow()
    {
        _throwStarted = false;

        foreach (var pin in _currentPins)
        {
            if (pin != null && pin.DidPinFall)
            {
                _currentFrameScore++;
                _currentThrowScore++;
                Destroy(pin.gameObject);
            }
        }

        _totalScore += _currentThrowScore;
        UpdateScoreUI();

        if (_throwNumber == 0 && _currentFrameScore < 10)
        {
            Invoke(nameof(SetupThrow), 1);
            _throwNumber++;
            return;
        }
        if (_currentFrame >= 10)
        {
            Debug.Log("Last Frame");
            Invoke(nameof(FinishGame), 5);
            return;
        }
        Invoke(nameof(SetupFrame), 1);
    }
    public void FinishGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SetupThrow()
    {
        _currentThrowScore = 0;
        foreach (var pin in _currentPins)
        {
            if (pin != null)
                pin.ResetPosition();
        }

        if (_currentBall != null) Destroy(_currentBall.gameObject);

        playerController.StartAiming();
        _throwStarted = true;
        remainingTimeout = throwTimeout;
    }

    public void DisposeLastFrame()
    {
        foreach (var pin in _currentPins)
        {
            if (pin != null)
            {
                Destroy(pin.gameObject);
            }
        }
    }

}
