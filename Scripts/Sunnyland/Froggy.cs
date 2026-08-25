using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Froggy : MonoBehaviour
{
    [SerializeField] Animator _frogAnim;
    Coroutine _froggyCoroutine;
    int i = 0;
    int jumpSpeedx = 5;
    Rigidbody2D _froggy;
    [SerializeField] PlayerController playerController;


    bool _isDead=false;

    void Start()
    {
        _froggyCoroutine = StartCoroutine(FrogJump());
        _froggy = gameObject.GetComponent<Rigidbody2D>();
    }
   
    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(5);
        i = i + 1;
        if (i % 2 == 0 && !_isDead)
        {
            _frogAnim.SetBool("_isJumping", true);
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            _froggy.velocity = new Vector2(jumpSpeedx, 8);
            yield return new WaitForSeconds(2f);
            _frogAnim.SetBool("_isJumping", false);
            yield return new WaitForSeconds(5);
            _frogAnim.SetBool("_isJumping", true);
            _froggy.velocity = new Vector2(jumpSpeedx, 8);
        }
        else if (i % 2 != 0 && !_isDead)
        {
            _frogAnim.SetBool("_isJumping", true);
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            _froggy.velocity = new Vector2(jumpSpeedx * -1, 8);
            yield return new WaitForSeconds(2f);
            _frogAnim.SetBool("_isJumping", false);
            yield return new WaitForSeconds(5);
            _frogAnim.SetBool("_isJumping", true);
            _froggy.velocity = new Vector2(jumpSpeedx * -1, 8);
        }
        yield return new WaitForSeconds(2);
        _frogAnim.SetBool("_isJumping", false);
        _froggyCoroutine = StartCoroutine(FrogJump());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerController._isFalling)
        {
            _isDead = true;
        }
    }
}

