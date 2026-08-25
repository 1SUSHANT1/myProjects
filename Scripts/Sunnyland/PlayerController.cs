 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _talaAauni;
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioSource _jumpAudio;
    [SerializeField] GameObject life1;
    [SerializeField] GameObject life2;
    [SerializeField] GameObject life3;
    [SerializeField] Text _cherryText;
    [SerializeField] Text _gemText;
    [SerializeField] Text _carrotText;
    [SerializeField] ForMenuScene forMenuScene;
    [SerializeField] GameObject _death;
   /* [SerializeField] CapsuleCollider2D cap;
    [SerializeField] BoxCollider2D box;*/


    Rigidbody2D _rb;
    Animator _anim;
    Coroutine _coroutine;
    Coroutine _killPlayer;
    Coroutine _playerHurt;

    Vector2 _respawnPoint;

    public bool _isFalling;
    bool _canJump;
    bool _moveRight;
    bool _moveLeft;
    bool _jump;
    bool _grounded;
    bool _disableMovement;
    bool _canHurt=true;    

    public float health = 4;
    float _doubleJump = 0;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _jumpSpeed = 11f;
    int Cherries=0;
    int Gems;
    int Carrots;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
        Time.timeScale = 1;
    }
    void Update()
    {
        if(_moveRight && forMenuScene._gamePaused==false && !_disableMovement)
        {
            _rb.velocity = new Vector2( _moveSpeed, _rb.velocity.y);
            transform.localScale = new Vector2(1, 1f);
            _anim.SetFloat("State", 1);
        }
        else if(_moveLeft && forMenuScene._gamePaused == false && !_disableMovement)
        {
            _rb.velocity = new Vector2(-_moveSpeed, _rb.velocity.y);
            transform.localScale = new Vector2( -1, 1f);
            _anim.SetFloat("State", 1);
        }
        else
        {
            _anim.SetFloat("State", 0);
        }
        if ( _jump && _canJump && !_isFalling && !_disableMovement)
        {
            
            _doubleJump++;
             _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
            _anim.SetBool("_isJumping", true);
           _coroutine = StartCoroutine(FallDelay());
        }
        _jump = false;
        if (health <= 0)
        {
            _death.SetActive(true);
        }
        if (_isFalling && !_grounded)
        {
            _canJump = false;
        }
        if (_doubleJump==1 || _doubleJump==0 && _grounded )
        {
            _canJump = true;
        }
        else  if (_doubleJump >= 2)
        {
            _rb.gravityScale = 3.5f;
            _canJump = false;
        }
       if(_grounded)
        {
            _canJump = true;
            _isFalling = false;
            _anim.SetBool("_isFalling", false);
        }
    }
    public void PointerDownLeft()
    {
        _moveLeft = true;
    }
    public void PointerUpLeft()
    {
        _moveLeft = false;
    }
    public void PointerDownRight()
    {
        _moveRight = true;
    }
    public void PointerUpRight()
    {
        _moveRight = false;
    }
    public void PointerDownJump()
    {
        _jump = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fore")
        {
            _rb.gravityScale = 3;
            _doubleJump = 0;
            _canJump = true;
            _isFalling = false;
            _grounded = true;
            _anim.SetBool("_isFalling", false);
            _anim.SetBool("_isJumping", false);
            _anim.SetBool("_playerIdle", true);

            //  _anim.SetFloat("State", 0);
        }

        if (collision.gameObject.tag == "falling")
        {
            _talaAauni.gravityScale = 7;
        }
       
        else if (collision.gameObject.tag=="CheckPoint")
        {
            _respawnPoint = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Enemy" && _isFalling)
        {
                _rb.velocity = new Vector2(_rb.velocity.x, 10);
        }
        else if (collision.gameObject.tag == "Enemy" && !_isFalling && _canHurt==true)
        {
            /*cap.enabled = false;
            box.enabled = false;*/
            _canHurt = false;
            CollisonHurt();
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                _rb.velocity = new Vector2(-6, 9);
            }
            else if (collision.gameObject.transform.position.x < transform.position.x)
            {
                _rb.velocity = new Vector2(6, 9);
            }
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fore")
        {
            _canJump = false;
            _grounded = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
{
       /* if (collision.gameObject.tag == "Enemy" && !_isFalling && _canHurt)
        {
            cap.enabled = false;
            box.enabled = false;
            CollisonHurt();
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                _rb.velocity = new Vector2(-6, 9);
            }
            else if (collision.gameObject.transform.position.x < transform.position.x)
            {
                _rb.velocity = new Vector2(6, 9);
            }
        }*/
        if (collision.gameObject.tag == "Gem")
        {
            Gems++;
            _gemText.text = Gems.ToString();
        }
        else if (collision.gameObject.tag == "carrot")
        {
            Carrots++;
            _carrotText.text = Carrots.ToString();
        }
        else if (collision.gameObject.tag == "Cherry")
        {
            Cherries++;
            _cherryText.text = Cherries.ToString();
        }
        if (collision.gameObject.tag == "house")
        {
            Scene currentScene = SceneManager.GetActiveScene();

            // Retrieve the name of this scene.
            string sceneName = currentScene.name;

            if (sceneName == "Level1")
            {
                forMenuScene.getLevel = 10;
                forMenuScene.determineLevel();
            }
            else if (sceneName == "Level2")
            {
                forMenuScene.getLevel = 20;
                forMenuScene.determineLevel();
            }
            else if (sceneName == "Level3")
            {
                forMenuScene.getLevel = 30;
                forMenuScene.determineLevel();
            }
            else if (sceneName == "Level4")
            {
                forMenuScene.getLevel = 40;
                forMenuScene.determineLevel();
            }
            else if (sceneName == "Level5")
            {
                forMenuScene.getLevel = 50;
                forMenuScene.determineLevel();
            }
            else if (sceneName == "Level6")
            {
                forMenuScene.getLevel = 60;
                forMenuScene.determineLevel();
            }
            forMenuScene.Win();
            Time.timeScale = 0;
        }
        if (collision.gameObject.tag == "Spikes1" && _canHurt==true)
        {
            _canHurt = false;
            _anim.SetBool("_playerDead", true);
            health = health - 1f;
            _killPlayer = StartCoroutine(KillPlayer());
            _rb.velocity = new Vector2(0, 10);
            IEnumerator KillPlayer()
            {
                _rb.velocity = new Vector2(0,0);
                yield return new WaitForSeconds(0.33f);
                _anim.SetBool("_playerDead", false);
                _anim.SetBool("_playerIdle", true);
                LifeIcon();
                _anim.SetFloat("State", 0);
                _rb.transform.position = _respawnPoint;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       
    }
     IEnumerator FallDelay()
    {
        yield return new WaitForSeconds(.5f);
        _isFalling = true;
        _anim.SetBool("_isFalling", true);
    }
    IEnumerator PlayerHurt()
    {
        yield return new WaitForSeconds(.5f);
        /*cap.enabled = true;
        box.enabled = true;*/
        LifeIcon();
       _anim.SetBool("_isHurting", false);
        _disableMovement = false;
        _canHurt = true;

    }
    void CollisonHurt()
    {
        _disableMovement = true;
        health = health - 1;
        LifeIcon();
        _anim.SetBool("_isHurting", true);
        _playerHurt = StartCoroutine(PlayerHurt());
    }
    void PlayAudio()
    {
        if(_canJump )
        {
            _audio.Play();
        }
    }
    void JumpAudio()
    {
        _jumpAudio.Play();
    }
    void LifeIcon()
    {
        if (health == 3)
        {
            life3.SetActive(false);
        }
        else if (health == 2)
        {
            life2.SetActive(false);
        }
        else if (health == 1)
        {
            life1.SetActive(false);
        }
        Debug.Log(health);
        _canHurt = true;
    }
    public void ReviveAd()
    {
        health = 4;
        _rb.transform.position = _respawnPoint;
        if (health == 3)
        {
            life3.SetActive(true);
        }
        else if (health == 2)
        {
            life2.SetActive(true);
        }
        else if (health == 1)
        {
            life1.SetActive(true);
        }
    }
}
