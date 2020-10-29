// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class BonusMovement : MonoBehaviour
{
    public static bool TakeBonus;

    private void Update() 
    {
        transform.position -= new Vector3(0, 3f * Time.deltaTime, 0); 
        transform.Rotate(Vector3.up, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        TakeBonus = true;
        Destroy(gameObject);
    }
}
