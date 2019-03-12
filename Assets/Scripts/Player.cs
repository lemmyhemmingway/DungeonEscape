using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Use this for initialization
    private Rigidbody2D _rb;

    [SerializeField]
    private float _jumpforce = 5.0f;

    [SerializeField]
    private float _playerSpeed = 2.5f;

    private bool _resetJump = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * _speed;
        //_rb.MovePosition(_rb.position + Vector2.right * horizontal);
        Movement();
    }

    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(move * _playerSpeed, _rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce);
            StartCoroutine(ResetJumpRoutine());
        }
    }

    private bool IsGrounded()
    {
        // bitshifting to get the ground layer
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, 1 << 8);
        if (hitInfo.collider != null && _resetJump == false)
        {
            return true;
        }
        return false;
    }

    private IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
}