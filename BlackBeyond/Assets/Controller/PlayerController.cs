using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

// Class for Player only methods. 
public class PlayerController : ShipController
{
    public GameObject currency;
    public GameObject metal;
    public GameObject organics;
    public GameObject gas;
    public GameObject water;
    public GameObject fuel;
    public GameObject fuelMax;
    public GameObject totalSpace;

    public void SetCurrency(int number)
    {
        currency.GetComponent<Text>().text = number.ToString();
    }
    public void SetMetal(int number)
    {
        metal.GetComponent<Text>().text = number.ToString();
    }
    public void SetGas(int number)
    {
        gas.GetComponent<Text>().text = number.ToString();
    }
    public void SetWater(int number)
    {
        water.GetComponent<Text>().text = number.ToString();
    }
    public void SetOrganics(int number)
    {
        organics.GetComponent<Text>().text = number.ToString();
    }
    public void SetFuel(int number, int maxfuel)
    {
        fuel.GetComponent<Text>().text = number.ToString();
        fuelMax.GetComponent<Text>().text = maxfuel.ToString();
    }
    public void SetTotal(int number)
    {
        totalSpace.GetComponent<Text>().text = number.ToString();
    }

    private Text movementText;

    public void SetMovementTextInterface(Text movementText)
    {
        this.movementText = movementText;
    }

    public void SetCurrentMovement(int currentPlayerMovement, int maxPlayerMovement)
    {
        movementText.text = currentPlayerMovement + "/" + maxPlayerMovement;
    }

}