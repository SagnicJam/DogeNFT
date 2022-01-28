using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
public class EnemyDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    IObjectPool<EnemyDisplay> enemyPool;

    public void SpawnNewPrefab(Enemy enemy,RectTransform parentRect)
    {
        transform.SetParent(parentRect);
        textMeshProUGUI.text = "Enemy id : " + enemy.id + " ,Cost : " + enemy.enemyCost;
    }

    public void SetPool(IObjectPool<EnemyDisplay> pool)
    {
        enemyPool = pool;
    }

    public void DestroyToPool()
    {
        if (enemyPool != null)
        {
            //on return object code
            enemyPool.Release(this);
        }
    }
}
