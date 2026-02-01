using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BarChallenge : MonoBehaviour
{
    private static BarChallenge _instance;
    public static BarChallenge Instance { get { return _instance; } }

    public GameObject _playerPointer;
    public GameObject ChallangeBar;
    public GameObject BarClickPoint;

    private bool _barActive = false;
    private float _playerPointerPosition;
    private float _barWidth;
    private float _calc;
    private int _barLength = 100;

    private bool _challengeRunning = false;
    public bool IsChallengeRunning() 
    { 
        return _challengeRunning; 
    }

    public delegate void OnActionSuccess();
    public OnActionSuccess _actionSuccess;

    public delegate void OnActionFail();
    public OnActionFail _actionFail;

    public delegate void OnChallangeStart();
    public OnChallangeStart _challangeStart;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        SetBarDimensions();

        SetObjectState(false);

    }

    private void Update()
    {

       // Debug.Log($"BarChallenge Update — barActive = {_barActive}");

        if (!_barActive)
            return;

        MovePointer();
    }

    
    private void MovePointer()
    {

        float time = Mathf.PingPong(Time.time * 1f, 1);
        _playerPointerPosition = Mathf.Lerp(0, _barLength, time);

        _playerPointer.transform.localPosition = new Vector2(GetPointOnBar(_playerPointerPosition), _playerPointer.transform.localPosition.y);
    }

    private void SetBarDimensions()
    {
        _barWidth = ChallangeBar.GetComponent<RectTransform>().rect.width;
        _calc = _barWidth / _barLength;
    }
    private float GetPointOnBar(float p)
    {
        return (p * _calc) - (_barWidth / 2);
    }


    public void StartChallenge()
    {
        _challengeRunning = true;
        Debug.Log("Bar Challenge STARTED");
        Debug.Log("ChallangeBar: " + (ChallangeBar != null));
        Debug.Log("_playerPointer: " + (_playerPointer != null));
        Debug.Log("BarClickPoint: " + (BarClickPoint != null));
        SetObjectState(true);
        _challangeStart?.Invoke();
    }

    public void StopChallenge()
    {

        _challengeRunning = false;
        SetObjectState(false); // hide the Bar UI
        Debug.Log("BarSet: " + _barActive);

    }

    public void SetObjectState(bool state)
    {
        _barActive = state;

       

        if (ChallangeBar != null)
        {
            ChallangeBar.SetActive(state);
            Debug.Log("BarSet: " + _barActive);

        }
        if (BarClickPoint != null)
            BarClickPoint.SetActive(state);
        if (_playerPointer != null )
            _playerPointer.SetActive(state);
    }

    public void Action()
    {
        if (!_barActive)
        {
            return;
        }

        if (InBoundsOfGoal())
        {
            _actionSuccess?.Invoke();
            Debug.Log("win");
            return;
        }

        _actionFail?.Invoke();
        Debug.Log("fail");
    }

    bool InBoundsOfGoal()
    {
        RectTransform rectT = BarClickPoint.GetComponent<RectTransform>();
        float x = rectT.sizeDelta.x / 2;
        float s = BarClickPoint.transform.localPosition.x - x;
        float e = BarClickPoint.transform.localPosition.x + x;
        float p = _playerPointer.transform.localPosition.x;
        return (p >= s && p <= e);
    }

    public void SetNewChallange(int size)
    {
        if (!_barActive)
        {
            return;
        }

        RectTransform rect = BarClickPoint.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size, rect.sizeDelta.y);


        BarClickPoint.transform.localPosition = new Vector2(GetPointOnBar(RandomPositionInRangeOfBar()), BarClickPoint.transform.localPosition.y);
    }

    

    private float RandomPositionInRangeOfBar()
    {
        return UnityEngine.Random.Range(0, 100);
    }
}