﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public enum TurretBehav {pacific, slowShot, fastShot, autoDestroy};
    public TurretBehav Behavior;
    public GameObject prefabBullet;
    public Transform spawnBulletPos;
    Animator anim;


    [Header("SlowShot")]
    public float RateofFire_slow;
    public float bulletSpeed_slow;

    [Header("FastShot")]
    public float RateofFire_fast;
    public float bulletSpeed_fast;

    [Header("AutoDestroy")]
    public float autoDestroy_time;

    private float RateOfFire;
    private float bulletSpeed;
    private float timer;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch(Behavior)
        {
            case TurretBehav.pacific:
                break;
            case TurretBehav.slowShot:
                RateOfFire = RateofFire_slow;
                bulletSpeed = bulletSpeed_slow;
                Shot();
                break;
            case TurretBehav.fastShot:
                RateOfFire = RateofFire_fast;
                bulletSpeed = bulletSpeed_fast;
                Shot();
                break;
            case TurretBehav.autoDestroy:
                Invoke("AutoDestroy", autoDestroy_time);
                break;
        }
    }

    void AutoDestroy()
    {
        Destroy(gameObject);
    }

    void Shot()
    {
        //Debug.Log(timer);

        if (timer < 0f)
        {
            timer = RateOfFire;
            GameObject b = Instantiate(prefabBullet);
            b.transform.position = spawnBulletPos.position;
            
            b.transform.rotation = spawnBulletPos.rotation;
            b.transform.parent = transform;
            b.GetComponent<BulletBehavior>().speed = bulletSpeed;
            anim.SetBool("isFiring", true);
            MusicController.instance.PlayAnSFX(MusicController.instance.TurretShot);
        }
        else
        {
            timer -= Time.deltaTime;
            
        }
    }

   public void SetPacific()
    {
        Behavior = TurretBehav.pacific;
        anim.SetBool("isFiring", false);
    }
    public void SetSlowRate()
    {
        Behavior = TurretBehav.slowShot;
        
    }
    public void SetFastRate()
    {
        Behavior = TurretBehav.fastShot;
    }
    public void SetAutoDestroy()
    {
        Behavior = TurretBehav.autoDestroy;
    }

}
