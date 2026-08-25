using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    [SerializeField] GameObject _closedChest;
    [SerializeField] GameObject _openedChest;

    // Start is called before the first frame update
   
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _closedChest.SetActive(false);
            _openedChest.SetActive(true);
        }
    }
}
