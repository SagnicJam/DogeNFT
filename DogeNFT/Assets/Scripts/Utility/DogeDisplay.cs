using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class DogeDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    IObjectPool<DogeDisplay> dogePool;

    public void SpawnNewPrefab(string dogeName)
    {
        textMeshProUGUI.text = dogeName.ToUpper();
    }

    public void SetPool(IObjectPool<DogeDisplay> pool)
    {
        dogePool = pool;
    }

    public void DestroyToPool()
    {
        if (dogePool != null)
        {
            //on return object code
            dogePool.Release(this);
        }
    }
}
