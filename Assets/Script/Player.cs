using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject weapon;
    public int maxSpeed;
    public int jumpPower;
    
    Rigidbody2D _rigid;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    SpriteRenderer _weaponSr;
    
    BoxCollider2D _boxCollider_Weapon;
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weaponSr = weapon.GetComponent<SpriteRenderer>();
        _boxCollider_Weapon = weapon.GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
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

        //start weapon affect
        if (Input.GetKeyDown(KeyCode.F))
        {
            weapon.SetActive(true);
        }
        
        //stop move
        if (Input.GetButtonUp("Horizontal"))
        {
            _rigid.linearVelocity = new Vector2(_rigid.linearVelocity.normalized.x * 0.5f , _rigid.linearVelocity.y);
        }

        //flipX (move, weapon)
        if (Input.GetButton("Horizontal"))
        {
            bool isFlipX = Input.GetAxisRaw("Horizontal") < 0;
            _spriteRenderer.flipX = isFlipX;
            _weaponSr.flipX = isFlipX;

            
            if (isFlipX)
            {
                weapon.transform.localPosition = new Vector3(-0.5f, weapon.transform.localPosition.y, weapon.transform.localPosition.z);
                
                if (_boxCollider_Weapon)
                {
                    _boxCollider_Weapon.offset = new Vector2(-0.56f, 0.0f);
                }
            }
            else
            {
                weapon.transform.localPosition = new Vector3(0.5f, weapon.transform.localPosition.y, weapon.transform.localPosition.z);
                if (_boxCollider_Weapon)
                {
                    _boxCollider_Weapon.offset = new Vector2(0.56f, 0.0f);
                }
            }
            
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
            if (rayHit.distance < 1.0f)
            {
                _animator.SetBool("isJumping", false);
            }
        }
    }
    
    
}
