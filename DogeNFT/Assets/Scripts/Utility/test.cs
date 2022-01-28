using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public string ss;

    public string newString;

    private void Start()
    {
        newString = ss.Substring(1, ss.Length-1);
    }
}
