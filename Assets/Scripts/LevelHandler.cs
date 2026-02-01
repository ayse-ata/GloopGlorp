using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    [SerializeField]
    private Customer[] _customers; 
    private Customer _currentCustomer;

    public GameObject NameText;
    public GameObject CustomerImage;
    private TextMeshProUGUI _nameText;
    private Image _customerSprite;

    [SerializeField] int max_rounds = 5;
    private int _customerArrayPosition;
    private int _currentRound;
    private bool _challengeRunning;
    public static LevelHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _nameText = NameText.GetComponent<TextMeshProUGUI>();
        _customerSprite = CustomerImage.GetComponent<Image>();

        // Subscribe bar success event
        BarChallenge.Instance._actionSuccess += AdvanceCustomer;
        _customerArrayPosition = 0;
        _currentRound = 0;
        SetCustomer(_customers[_customerArrayPosition]);
    }

    
    public void StartChallenge()
    {
        if (_challengeRunning) return;
        if (_currentCustomer == null || BarChallenge.Instance == null) return;

        _challengeRunning = true;
        SetCustomer(_currentCustomer);

        // BarChallenge handles its own UI
        BarChallenge.Instance.StartChallenge();
        BarChallenge.Instance.SetNewChallange((int)_currentCustomer.difficulty);
        Debug.Log("LevelHandler: Challenge difficulty " + (int)_currentCustomer.difficulty);

        Debug.Log("LevelHandler: Challenge started for " + _currentCustomer.customerName);
    }
    public void StopChallenge()
    {
        if (!_challengeRunning) return;
        _challengeRunning = false;
        BarChallenge.Instance.StopChallenge();

        // BarChallenge.Instance.gameObject.SetActive(false);
        Debug.Log("BarSet: " + false);

    }

    void PrepareNextCustomer()
    {
        _customerArrayPosition++;
        if (_customerArrayPosition >= _customers.Length)
        {
            Debug.Log("All customers finished"); return;
        }
        _currentRound = 0; 
        SetCustomer(_customers[_customerArrayPosition]);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            BarChallenge.Instance.Action();
        }

    }
    public void NextCustomer()
    {
        _customerArrayPosition++;
        if (_customerArrayPosition >= _customers.Length)
        {
            Debug.Log("end"); 
            return;

        }
        _currentRound = 0;
        SetCustomer(_customers[_customerArrayPosition]);

    }

    public void SetCustomer()
    {
        SetCustomer(_customers[_customerArrayPosition]);
    }
    public void SetCustomer(Customer customer)
    {
        //NextCustomer();
        _currentCustomer = customer;
        _nameText.text = customer.customerName;
        _customerSprite.sprite = customer.customerSprite;

        Dialogue.Instance.PushNewDialogue(_currentCustomer.drinkOrder, 3f);

    }

    public void AdvanceCustomer()
    {
        if (!_challengeRunning) return;

        _currentRound++;

        if (_currentRound >= max_rounds)
        {
            StopChallenge();
            PrepareNextCustomer();
            return;
        }
        
            // Next round for the same customer
            BarChallenge.Instance.SetNewChallange((int)_currentCustomer.difficulty);
        
    }
    public void ForceNextCustomer()
    {
        Debug.Log("LevelHandler: ForceNextCustomer called");
        StopChallenge();
        PrepareNextCustomer();

    }
       
    public bool IsChallengeRunning()
    {
        return _challengeRunning;

    } 

}
  
    