using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{

    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag(Tags.gameManager).GetComponent<GameManager>();
    }

    public Dictionary<int, Food> GetListFood(List list)
    {
        Dictionary<int, Food> result = new Dictionary<int, Food>();
        foreach (var food in list.FoodItems)
        {
                result.Add(food.id, food);
        }
        return result;
    }
    public float ConvertDistance(Enums.Distance distance)
    {
        if (distance == Enums.Distance.small)
        {
            return 10;
        }
        else if(distance == Enums.Distance.medium)
        {
            return 20;
        }
        else if (distance == Enums.Distance.big)
        {
            return 30;
        }
        else
        {
            return 0;
        }
    }

    public float ConvertDame(Food food,Enums.Dame dame)
    {
        float speedReturn;
        if (dame == Enums.Dame.small)
        {
            speedReturn =  10;
        }
        else if (dame == Enums.Dame.medium)
        {
            speedReturn =  20;

        }
        else if (dame == Enums.Dame.big)
        {
            speedReturn =  30;
        }
        else
        {
            return 0;
        }

        if (food.TypeFood == Enums.TypeFood.upDame)
        {
            speedReturn *= 2;
        }

        return speedReturn;
    }

    public void HealingProcess()
    {
        gameManager.SetHealth(gameManager.GetHealth() + 15);
        if (gameManager.GetHealth() > 100)
        {
            gameManager.SetHealth(100);
        }
    }

    public void Defence(float takeDame)
    {
        gameManager.SetHealth(gameManager.GetHealth() - takeDame* 20 / 100);
        if (gameManager.GetHealth() < 0)
        {
            gameManager.SetStatusGame(true);
        }
    }
}
