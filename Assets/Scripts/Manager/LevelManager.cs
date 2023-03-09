using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10;

    public int TotalLives { get; set; }

    private void Start()
    {
        TotalLives = lives;
    }
    private void ReduceLive(Enemy enemy)
    {
        TotalLives--;
        
        if(TotalLives <=0)
        {
            TotalLives = 0;

            //GameOver
        }

    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLive;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLive;
    }
}
