using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelHandler : MonoBehaviour
{
    [SerializeField]
    private Customer[] _customers;
    private Customer _currentCustomer;

    public GameObject NameText;
    public GameObject CustomerImage;

    private TextMeshProUGUI _nameText;
    private SpriteRenderer _customerSprite;

    private int _customerArrayPosition;
    private int _currentRound;

    private void Start()
    {
        _customerSprite = CustomerImage.GetComponent<SpriteRenderer>();
        _nameText = NameText.GetComponent<TextMeshProUGUI>();

        BarChallenge.Instance._actionSuccess += AdvanceCustomer;
        _customerArrayPosition = 0;
        SetCustomer(_customers[_customerArrayPosition]);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            BarChallenge.Instance.Action();
        }
    }

    void NextCustomer()
    {
        _customerArrayPosition++;
        if(_customerArrayPosition >= _customers.Length)
        {
            Debug.Log("end");
            return;
        }

        SetCustomer(_customers[_customerArrayPosition]);
    }

    public void SetCustomer(Customer customer)
    {
        _currentRound = 0;
        _currentCustomer = customer;
        _nameText.text = customer.customerName;
        _customerSprite.sprite = customer.customerSprite;
        
        BarChallenge.Instance.SetNewChallange(_currentCustomer.difficulty);
    }

    public void AdvanceCustomer()
    {
        _currentRound++;
        if(_currentRound >= _currentCustomer.rounds)
        {
            NextCustomer();
            return;
        }

        BarChallenge.Instance.SetNewChallange(_currentCustomer.difficulty);
    }
}
