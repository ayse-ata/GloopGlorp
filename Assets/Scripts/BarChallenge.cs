using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BarChallenge : MonoBehaviour
{
    private static BarChallenge _instance;
    public static BarChallenge Instance { get { return _instance; } }

    private float playerPointerPosition;
    public GameObject playerPointer;

    UnityEvent OnSuccess;
    UnityEvent OnFail;

    private int barLength = 100;

    private float barWidth;
    private float calc;
    public GameObject challangeBar;

    public GameObject barClickPoint;

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

        OnSuccess = new UnityEvent();
        //OnSuccess.AddListener(LevelManager.Instance.AdvanceRound);

        OnFail = new UnityEvent();
    }

    private void Update()
    {

        MovePointer();
    }

    private void MovePointer()
    {

        float time = Mathf.PingPong(Time.time * 1f, 1);
        playerPointerPosition = Mathf.Lerp(0, barLength, time);

        playerPointer.transform.localPosition = new Vector2(GetPointOnBar(playerPointerPosition), playerPointer.transform.localPosition.y);
    }

    private void SetBarDimensions()
    {
        barWidth = challangeBar.GetComponent<RectTransform>().rect.width;
        calc = barWidth / barLength;
    }

    public void Action()
    {
        if (InBoundsOfGoal())
        {
            OnSuccess.Invoke();
            Debug.Log("win");
        }
        else
        {
            Debug.Log("fail");
        }
        
    }

    bool InBoundsOfGoal()
    {
        RectTransform rectT = barClickPoint.GetComponent<RectTransform>();
        float x = rectT.sizeDelta.x / 2;
        float s = barClickPoint.transform.localPosition.x - x;
        float e = barClickPoint.transform.localPosition.x + x;
        float p = playerPointer.transform.localPosition.x;
        if (p >= s && p <= e)
        {
            return true;
        }
        return false;
    }

    public void SetNewChallange(int size)
    {
        RectTransform rect = barClickPoint.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size ,rect.sizeDelta.y);


        barClickPoint.transform.localPosition = new Vector2(GetPointOnBar(RandomPositionInRangeOfBar()), barClickPoint.transform.localPosition.y);
    }

    private float GetPointOnBar(float p)
    {
        return (p * calc) - (barWidth / 2);
    }

    private float RandomPositionInRangeOfBar()
    {
        return UnityEngine.Random.Range(0,100);
    }
}
