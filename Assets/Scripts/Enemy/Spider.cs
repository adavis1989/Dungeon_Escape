using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    public GameObject _acidEffectPrefab;

    //Use for Initialize
    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Update()
    {
        
    }

    public void Damage(int damage)
    {
        if (isDead == true)
        {
            return;
        }

        Health = Health - damage;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Movement()
    {
        //sit still
    }

    public void Attack()
    {
        //instantiate acid ball
        Instantiate(_acidEffectPrefab, transform.position, Quaternion.identity);

    }
}
