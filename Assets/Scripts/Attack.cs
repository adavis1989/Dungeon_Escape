using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canAttack = true;

    [SerializeField]
    private int _attackpoints;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit: " + other.name);

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            if (_canAttack == true)
            {
                hit.Damage(_attackpoints);
                _canAttack = false;
                StartCoroutine(AttackReset());
            }
        }
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(1f);
        _canAttack = true;
    }
}
