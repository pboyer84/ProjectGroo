using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public float Max;
    public float value;

    void Awake()
    {
        Max = value;
    }
}
