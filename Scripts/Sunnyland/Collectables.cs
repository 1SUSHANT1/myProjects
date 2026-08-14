using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    Animator _collectableAnim;
    Coroutine _collectable;

    // Start is called before the first frame update
    void Start()
    {
        _collectableAnim = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _collectableAnim.SetBool("_eaten", true);
            _collectable = StartCoroutine(DestroyCollectable());
        }
    }
    IEnumerator DestroyCollectable()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

}
