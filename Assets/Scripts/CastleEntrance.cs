using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntrance : MonoBehaviour
{
    public GameObject gameVictory;
    public GameObject needAKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (GameManager.Instance.HasKeyToCastle == true)
            {
                Debug.Log("You Win!");
                gameVictory.SetActive(true);
            }
            else
            {
                needAKey.SetActive(true);
                Debug.Log("You Need A Key!");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            needAKey.SetActive(false);
        }
    }
}
