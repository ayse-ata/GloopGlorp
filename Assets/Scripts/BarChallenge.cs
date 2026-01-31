using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BarChallenge : MonoBehaviour
{
    private static BarChallenge _instance;
    public static BarChallenge Instance { get { return _instance; } }

    private float _playerPointerPosition;
    public GameObject _playerPointer;

    public delegate void OnActionSuccess();
    public OnActionSuccess _actionSuccess;

    public delegate void OnActionFail();
    public OnActionFail _actionFail;

    private int _barLength = 100;

    private float _barWidth;
    private float _calc;

    public GameObject ChallangeBar;
    public GameObject BarClickPoint;

    private bool _barActive;

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
        //Setting this to false by default
        //Setting to true from Cus1Events.cs -Ross
        SetObjectState(false);

        SetBarDimensions();
    }

    private void Update()
    {

        if (!_barActive)
        {
            return;
        }

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
        return;
    }

    bool InBoundsOfGoal()
    {
        RectTransform rectT = BarClickPoint.GetComponent<RectTransform>();
        float x = rectT.sizeDelta.x / 2;
        float s = BarClickPoint.transform.localPosition.x - x;
        float e = BarClickPoint.transform.localPosition.x + x;
        float p = _playerPointer.transform.localPosition.x;
        if (p >= s && p <= e)
        {
            return true;
        }
        return false;
    }

    public void SetNewChallange(int size)
    {
        if (!_barActive)
        {
            return;
        }

        RectTransform rect = BarClickPoint.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size ,rect.sizeDelta.y);


        BarClickPoint.transform.localPosition = new Vector2(GetPointOnBar(RandomPositionInRangeOfBar()), BarClickPoint.transform.localPosition.y);
    }

    private float GetPointOnBar(float p)
    {
        return (p * _calc) - (_barWidth / 2);
    }

    private float RandomPositionInRangeOfBar()
    {
        return UnityEngine.Random.Range(0,100);
    }

    public void SetObjectState(bool x)
    {
        _barActive = x;
        ChallangeBar.SetActive(_barActive);
    }
}
