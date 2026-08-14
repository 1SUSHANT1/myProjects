using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField] Animator _enemyAnim;
    [SerializeField] PlayerController _playerController;
    Coroutine _enemyCoroutine;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _playerController._isFalling)
        {
            Interaction();
        }
    }
    void Interaction()
    {
        _enemyAnim.SetBool("_enemyDead", true);
        _enemyCoroutine = StartCoroutine(Dead());
        gameObject.GetComponent<Collider2D>().enabled = false;

        IEnumerator Dead()
        {
            _rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(0.3f);
            gameObject.SetActive(false);
        }
    }
}
