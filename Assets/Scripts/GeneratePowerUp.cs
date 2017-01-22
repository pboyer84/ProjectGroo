using UnityEngine;
using System.Collections;
using System;

public class GeneratePowerUp : MonoBehaviour {


    internal void MakePowerUp(GameObject myTankPrefab)
    {
        Instantiate(myTankPrefab, Vector3.zero, Quaternion.identity);
    }
}
