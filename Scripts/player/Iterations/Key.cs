using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private int keyID;

    public int GetKeyID()
    {
        return keyID;
    }
}
