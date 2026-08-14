using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D _mp;
   [SerializeField] float _mpSpeedx = -3;
    [SerializeField] float _mpSpeedy=0;

    // Start is called before the first frame update
    void Start()
    {
        _mp = gameObject.GetComponent<Rigidbody2D>();
        _mp.velocity = new Vector2(_mpSpeedx,_mpSpeedy);

    }
    // Update is called once per frame
    void Update()
    {
        if (_mp.velocity.x != _mpSpeedx && _mp.velocity.x >= 0)
        {
            _mp.velocity = new Vector2(_mpSpeedx, _mpSpeedy);

        }
        else if(_mp.velocity.y != _mpSpeedy && _mp.velocity.y >= 0)
        {
            _mp.velocity = new Vector2(-_mpSpeedx, _mpSpeedy);

        }
        else if (_mp.velocity.x != _mpSpeedx && _mp.velocity.x <= 0)
        {
            _mp.velocity = new Vector2(-_mpSpeedx, 0f);
        }
        else if (_mp.velocity.y != _mpSpeedy && _mp.velocity.y <= 0)
        {
            _mp.velocity = new Vector2(_mpSpeedx, -_mpSpeedy);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PosBar")
        {
            _mp.velocity = new Vector2(_mp.velocity.x * -1, _mp.velocity.y*-1);
        }
    }
}
