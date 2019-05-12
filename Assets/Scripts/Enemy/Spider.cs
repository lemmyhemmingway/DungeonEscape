using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageble
{
    public int health { get; set; }

    public GameObject acidEffectPrefab;

    public override void Init()
    {
        base.Init();

        health = base.health;
    }

    public override void Update()
    {
    }

    public void Damage()
    {
        if (isDead == true) return;
        health -= 1;
        if (health < 1)
        {
            isDead = true;
            anim.SetTrigger("death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Movement()
    {
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
