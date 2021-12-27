using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (GameManager.Instance.HasKeyToCastle == true)
            {
                Debug.Log("You Win!");
            }
            else
            {
                Debug.Log("You Need A Key!");
            }
        }
    }
}
