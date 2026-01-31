using System;
using System.Collections;
using UnityEngine;

public class Cus1Events : MonoBehaviour
{
    

    public GameObject fadeScreenIn;
    public GameObject customer1;

    private GameObject currentCustomer;
    void Start()
    {
        StartCoroutine(EventStarter(customer1));
    }

    IEnumerator EventStarter(GameObject customer)
    {
        currentCustomer = customer;
        //Subscribe to BarChallenge events
        BarChallenge.Instance._actionSuccess += OnSuccess;
        BarChallenge.Instance._actionFail += OnFail;
        
        yield return new WaitForSeconds(1);
        fadeScreenIn.SetActive(false);
        yield return new WaitForSeconds(2);
        customer1.SetActive(true);
        
        //trigger customer drink order dialogue
        //Not sure if dialogue has been implemented yet? -Ross
        
        //wait for secs so that drink order animation will play
        yield return new WaitForSeconds(2);
        
        //trigger customer1 minigame box
        BarChallenge.Instance.SetObjectState(true);

        //Not sure how to detect when game ends at this point -Ross
        //Ending game by default after 10 seconds
        //Have something else call EndChallenge when the minigame is meant to end
        
        yield return new WaitForSeconds(10);
        EndChallenge();
    }


    private void OnFail()
    {
        SetCharacterSprite(CustomerSprite.SpriteType.ANGRY);
    }

    private void OnSuccess()
    {
        SetCharacterSprite(CustomerSprite.SpriteType.NORMAL);
    }
    private void SetCharacterSprite(CustomerSprite.SpriteType spriteType)
    {
        currentCustomer.GetComponent<CustomerSprite>().SetSprite(spriteType);
    }

    private void EndChallenge()
    {
        BarChallenge.Instance.SetObjectState(false);
        
        //Unsubscribe from BarChallenge events (to avoid errors)
        BarChallenge.Instance._actionSuccess -= OnSuccess;
        BarChallenge.Instance._actionFail -= OnFail;
    }
}
