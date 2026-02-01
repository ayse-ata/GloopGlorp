using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using static CustomerSprite;

public class Cus1Events : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject customer1;
    //public GameObject cus1Order;
    public GameObject customer2;
   // public GameObject cus2Order;
    public GameObject customer3;
    //public GameObject cus3Order;

    private GameObject currentCustomer;
    private GameObject[] customers;

    void Start()
    {
        customers = new GameObject[] { customer1, customer2, customer3 };

        // Make sure all customers start inactive
        foreach (GameObject c in customers)
        {
            c.SetActive(false);
        }
        //cus1Order.SetActive(false);
        //cus2Order.SetActive(false);
        //cus3Order.SetActive(false);
        // Start the first customer sequence
        StartCoroutine(EventStarter(0));
    }

    IEnumerator EventStarter(int customerIndex)
    {
        if (customerIndex >= customers.Length)
            yield break;

        currentCustomer = customers[customerIndex];

        Debug.Log("Cus1Events: EventStarter running");
        //Debug.Log("Starting sequence for: " + currentCustomer.name);
        currentCustomer.SetActive(true);
        Debug.Log("Starting sequence for: " + currentCustomer.name);

        yield return new WaitForSeconds(1);
        fadeScreenIn.SetActive(false);
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(Dialogue.Instance.RunDialogue(currentCustomer.GetComponent<CustomerSprite>().customerData.drinkOrder, 1f));
        Debug.Log("cus1events: Dialogue finished");
        //yield return new WaitForSeconds(2);
        Debug.Log("Cus1Events: Starting challenge");

        BarChallenge.Instance._actionSuccess += OnSuccess;
        BarChallenge.Instance._actionFail += OnFail;

        LevelHandler.Instance.StartChallenge();
        //wait until challenge finishes

        while (LevelHandler.Instance.IsChallengeRunning())
        {
            yield return null;
        }

        //chalenge finixhes - walk customer out
        yield return StartCoroutine(CustomerExit(currentCustomer));

        BarChallenge.Instance._actionSuccess -= OnSuccess;
        BarChallenge.Instance._actionFail -= OnFail;

        //move onto next customer
        StartCoroutine(EventStarter(customerIndex + 1));

        yield return new WaitForSeconds(10);
        if (!BarChallenge.Instance.IsChallengeRunning())
        {

           // yield return StartCoroutine(CustomerExit(currentCustomer));

            LevelHandler.Instance.ForceNextCustomer();
            LevelHandler.Instance.SetCustomer();
            LevelHandler.Instance.NextCustomer();
        }
    }

    IEnumerator CustomerExit(GameObject customer)
    {
        Animator anim = customer.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("walkOut", true);
        }

        Vector3 startPos = customer.transform.position; // current position
        Vector3 endPos = startPos + new Vector3(5f, 0f, 0f);

        float duration = 2f; // seconds
        float distance = Vector3.Distance(startPos, endPos);
        float speed = distance / duration; // units per second
        float moved = 0f;

        while (moved < distance)
        {
            float step = speed * Time.deltaTime;
            customer.transform.position = Vector3.MoveTowards(customer.transform.position, endPos, step);
            moved += step;
            yield return null;
        }

        customer.SetActive(false);

        if (anim != null) 
            anim.SetBool("walkOut", false);
    }

    private void OnFail()
    {
        currentCustomer.GetComponent<CustomerSprite>().SetSprite(SpriteType.ANGRY);
    }

    private void OnSuccess()
    {
        currentCustomer.GetComponent<CustomerSprite>().SetSprite(SpriteType.NORMAL);
    }
    private void SetCharacterSprite(CustomerSprite.SpriteType spriteType)
    {
        currentCustomer.GetComponent<CustomerSprite>().SetSprite(spriteType);
    }

    private void OnDestroy()
    {
        if (BarChallenge.Instance != null) return;

        BarChallenge.Instance._actionSuccess -= OnSuccess;
        BarChallenge.Instance._actionFail -= OnFail;
    }
}
