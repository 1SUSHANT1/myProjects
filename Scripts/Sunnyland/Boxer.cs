using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer : MonoBehaviour
{
    [SerializeField] Rigidbody2D _player;
    Animator _boxerAnim;

    Vector2 _offset;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        _boxerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _offset = (_player.transform.position - transform.position);
        distance = _offset.sqrMagnitude;

        if (_player.gameObject.transform.position.x> transform.position.x && distance<6)
        {
            transform.localScale = new Vector2(-1, 1f);
            _boxerAnim.SetBool("_boxerPunch", true);
        }
        if (_player.gameObject.transform.position.x < transform.position.x && distance < 6)
        {
            _boxerAnim.SetBool("_boxerPunch", true);
            transform.localScale = new Vector2(1, 1f);
        }
        else if(distance > 6)
        {
            _boxerAnim.SetBool("_boxerPunch", false);

        }
    }
}
