using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float health;
    private bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float value)
    {
        health = value;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetStatusGame(bool value)
    {
        isGameOver = value;
    }
}
