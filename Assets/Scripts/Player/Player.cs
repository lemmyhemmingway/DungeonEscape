using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageble
{
    public int diamonds;

    // Use this for initialization
    private Rigidbody2D _rb;

    [SerializeField]
    private readonly float _jumpforce = 7.0f;

    [SerializeField]
    private readonly float _playerSpeed = 2.5f;

    private bool _grounded = false;

    private bool _resetJump = false;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int health { get; set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        health = 4;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * _speed;
        //_rb.MovePosition(_rb.position + Vector2.right * horizontal);
        Movement();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() == true)
        {
            _playerAnim.Attack();
        }
    }

    private void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();
        if (move < 0)
        {
            Flip(true);
        }
        else if (move > 0)
        {
            Flip(false);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded() == true)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
        _rb.velocity = new Vector2(move * _playerSpeed, _rb.velocity.y);

        _playerAnim.Move(move);
    }

    private bool IsGrounded()
    {
        // bitshifting to get the ground layer
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    private void Flip(bool flip)
    {
        Vector3 newPos = _swordArcSprite.transform.localPosition;
        if (flip == true)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (flip == false)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    private IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if (health < 1)
            return;
        health -= 1;
        UIManager.Instance.UpdateLives(health);
        if (health < 1)
        {
            _playerAnim.Death();
        }
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}
