using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyStats : MonoBehaviour
{
    public Enemy[] enemies;

    private void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].enemyCost = (i +1);
        }
    }
}
[Serializable]
public struct Enemy
{
    public int id;
    
    public int enemyCost;
}