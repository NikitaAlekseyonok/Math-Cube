// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class CoinBonus : MonoBehaviour
{
    public float DestroyTime = 7f;

    public void Start()
    {
        Invoke("destroyBullet", DestroyTime);
    }

    public void Update()
    {
        if(transform.position.y > 4.8)
            transform.position -= new Vector3(0, 0.1f, 0);

        transform.Rotate(Vector3.up, 1f);
    }

    public void destroyBullet()
    {
         Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        GameManager.Coins++;
        destroyBullet();
    }
}
