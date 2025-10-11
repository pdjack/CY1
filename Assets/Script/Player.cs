using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxSpeed;
    public int jumpPower;
    
    public float climbSpeed;
    private float _vertical;
    private bool _isLadder;
    private bool _isClimbing;
    
    Rigidbody2D _rigid;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        //ladder
        _vertical = Input.GetAxis("Vertical");
        if (_isLadder && Mathf.Abs(_vertical) > 0f)
        {
            _isClimbing = true;
        }
        
        //jump, jump animation(true)
        if (Input.GetKeyDown(KeyCode.Space) && !_animator.GetBool("isJumping"))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, Vector3.down, 100000, LayerMask.GetMask("Platform"));
            if (rayHit.distance != 0)//rayHit.distance 바닥 비여있으면 0됨
            {
                _rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                _animator.SetBool("isJumping", true);
            }
        }
        
        //stop move
        if (Input.GetButtonUp("Horizontal"))
        {
            _rigid.linearVelocity = new Vector2(_rigid.linearVelocity.normalized.x * 0.5f , _rigid.linearVelocity.y);
        }

        //move - flipX
        if (Input.GetButton("Horizontal"))
        {
            _spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") < 0;
        }

        //move animation
        if (Mathf.Abs(_rigid.linearVelocity.x) <= 0.3f)
        {
            _animator.SetBool("isWalking", false);
        }
        else
        {
            _animator.SetBool("isWalking", true);
        }
    }
    
    void FixedUpdate()
    {
        //ladder
        if (_isClimbing)
        {
            _rigid.gravityScale = 0f;
            _rigid.linearVelocity = new Vector2(_rigid.linearVelocityX, _rigid.linearVelocityY * climbSpeed);
        }
        else
        {
            _rigid.gravityScale = 1f;
        }
        
        //player move
        float h = Input.GetAxis("Horizontal");
        _rigid.AddForce(Vector2.right * (h * 30.0f), ForceMode2D.Impulse);
        //right max speed
        if (_rigid.linearVelocity.x > maxSpeed)
        {
            _rigid.linearVelocity = new Vector2(maxSpeed, _rigid.linearVelocity.y);
        }
        //left max speed
        else if (_rigid.linearVelocity.x < maxSpeed * (-1))
        {
            _rigid.linearVelocity = new Vector2(maxSpeed * (-1), _rigid.linearVelocity.y);
        }
        
        //jump animation(false)
        if (_rigid.linearVelocityY < 0)
        {
            Debug.DrawRay(_rigid.position, Vector3.down, new Color(0,1,0));
            RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, Vector3.down, 10000000, LayerMask.GetMask("Platform"));
            //Debug.Log("position:" + _rigid.position);
            if (rayHit.distance < 1.0f)
            {
                //Debug.Log("rayHit.distance:" + rayHit.distance);
                _animator.SetBool("isJumping", false);
                //Debug.Log("isJump:" + _animator.GetBool("isJump") );
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = true;
            Debug.Log("_isLadder = true");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;
        }
    }
    
    
}
