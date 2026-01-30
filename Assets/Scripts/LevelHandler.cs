using UnityEngine;
using UnityEngine.InputSystem;

public class LevelHandler : MonoBehaviour
{
    [SerializeField]
    private Customer[] customers;
    private Customer currentCustomer;

    private int customerArrayPosition;
    private int currentRound;

    private void Start()
    {
        BarChallenge.Instance.OnSuccess.AddListener(AdvanceCustomer);
        customerArrayPosition = 0;
        SetCustomer(customers[customerArrayPosition]);
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
        customerArrayPosition++;
        if(customerArrayPosition >= customers.Length)
        {
            return;
        }

        SetCustomer(customers[customerArrayPosition]);
    }

    void SetCustomer(Customer customer)
    {
        currentRound = 0;
        currentCustomer = customer;
    }

    void AdvanceCustomer()
    {
        currentRound++;
        if(currentRound >= currentCustomer.rounds)
        {
            NextCustomer();
            return;
        }

        BarChallenge.Instance.SetNewChallange(currentCustomer.difficulty);
    }
}
