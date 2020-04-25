﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : WarriorInteractable
{
    public bool trapped;

    public GameObject _sprtrappedChest;
    public GameObject _sprChest;

    public Animator anim;
    public ParticleSystem coin;
    public ParticleSystem death;

    public void Awake()
    {
        interestType = E_WarriorInterests.Chest;

        _sprChest.SetActive(trapped ? false : true);
        _sprtrappedChest.SetActive(trapped ? true : false);
    }

    public override void Interact()
    {
        Debug.Log("Opening loot chest !");
        
        Warrior.instance.animator.SetBool("Attacking", true);
        anim.SetTrigger("Explode");
        interestType = E_WarriorInterests.EmptyChest;
        if (Healer.instance.Tg_grab == GetComponent<GrabableItem>())
            Healer.instance.ReleaseObj(Healer.instance.Tg_grab);
        if (trapped)
        {
            StartCoroutine(Explode());
            death.Play();
        }
        else
        {
            MusicController.instance.PlayAnSFX(MusicController.instance.Chest);
            coin.Play();
        }
    }   

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1f);
        MusicController.instance.PlayAnSFX(MusicController.instance.ChestExplosion);
        Warrior.instance.TakeDamage(200, null);
    }
}