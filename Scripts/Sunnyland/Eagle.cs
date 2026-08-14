using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField] Rigidbody2D _Player;
    [SerializeField] PlayerController playerController;
    Vector2 _offset;
    float distance;

    bool _isDead;

   [SerializeField] AudioSource _eagleCry;
   

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
     //  _eagleCry = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        _offset = (_Player.transform.position - transform.position);
        distance = _offset.sqrMagnitude;
        if (distance <= 400 && distance>=390 )
        {
            _eagleCry.Play();
        }

        if (distance <400 && !_isDead)
        {
            _rb.velocity = new Vector2(_offset.x*0.2f, _offset.y*0.2f);
            if(distance<200)
            {
                _rb.velocity = new Vector2(_offset.x*.5f , _offset.y*.5f );

                if(distance<50)
                {
                _rb.velocity = new Vector2(_offset.x*1.5f , _offset.y*1.5f);

                }
            }
        }
        else if(distance>400)
        {
            _rb.velocity = new Vector2(0f, 0f);
        }
        if (transform.position.x > _Player.transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
         }
        else if (transform.position.x < _Player.transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player" && playerController._isFalling)
        {
            _isDead = true;
        }
    }
}
