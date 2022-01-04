using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;

    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _jumpHeight = 6;
    private bool _resetJump;
    [SerializeField]
    private float _speed = 3.0f;
    private bool _grounded = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    [SerializeField]
    private Vector2 _spawnPos;
    
    [SerializeField]
    private bool _doubleJumpActive = false;
    [SerializeField]
    private bool _canDoubleJump = false;

    public bool flameAttack = false;
    
    public int Health { get; set; }


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    void Update()
    {
        Movement();

        if (Health < 1)
        {
            if (CrossPlatformInputManager.GetButtonDown("A_Button"))
            {
                SceneManager.LoadScene("Game");
            }
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded())
        {
            if (flameAttack == true)
            {
                _playerAnim.FlameAttack();
            }

                _playerAnim.Attack();
        }
    }
    void Movement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        //float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Health < 1)
        {
            horizontalInput = 0;
        }

        _grounded = IsGrounded();

        if (horizontalInput > 0)
        {
            Flip(true);
        }
        else if (horizontalInput < 0)
        {
            Flip(false);
        }

        if (CrossPlatformInputManager.GetButtonDown("B_Button") && IsGrounded())
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpHeight);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jumping(true);
        }
        if (CrossPlatformInputManager.GetButtonDown("B_Button") && _canDoubleJump == true)
        {
            _canDoubleJump = false;
            _playerAnim.DoubleJump();
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpHeight);
        }


        _rigidBody.velocity = new Vector2(horizontalInput * _speed, _rigidBody.velocity.y);

        _playerAnim.Move(horizontalInput);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitInfo.collider != null)
        {
            if (_doubleJumpActive == true)
            {
                _canDoubleJump = true;
            }

            if (_resetJump == false)
            {
                _playerAnim.Jumping(false);
                return true;
            }
        }

        return false;
    
        
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            //_swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            //_swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage(int damage)
    {
        if (Health < 1)
        {
            return;
        }
        Debug.Log("Player::Damage()");
        Health = Health - damage;
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            _playerAnim.Death();
        }
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

    public void DoubleJumpActive()
    {
        _doubleJumpActive = true;
        if (_doubleJumpActive == true)
        {
            _canDoubleJump = true;
        }
    }
    public void FlameAttackActive()
    {
        flameAttack = true;
    }

    public void Respawn()
    {
        transform.position = _spawnPos;
        Damage(1);
    }
}
