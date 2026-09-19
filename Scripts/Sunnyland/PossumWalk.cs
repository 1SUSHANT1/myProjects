using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossumWalk : MonoBehaviour
{
    Rigidbody2D _ps;
    float _poSpeed=-3;


    void Start()
    {
        _ps = gameObject.GetComponent<Rigidbody2D>();
        _ps.velocity = new Vector2(_poSpeed, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (_ps.velocity.x!= _poSpeed && _ps.velocity.x >=0 && _ps.velocity.x!=0 )
        {
            _ps.velocity = new Vector2(-_poSpeed,0f) ;
          
        }
        if (_ps.velocity.x != _poSpeed && _ps.velocity.x <= 0 && _ps.velocity.x != 0)
        {
            _ps.velocity = new Vector2(_poSpeed, 0f);           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PosBar" )
        {
            MoveSpeed();
        }
    }
    void MoveSpeed()
    {
        _ps.velocity = new Vector2(_ps.velocity.x * -1, 0f);
        transform.localScale = new Vector2(transform.localScale.x * -1, 1);
    }
}
