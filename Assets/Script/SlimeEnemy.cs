using System;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public int jumpPower;
    public float maxSpeed;
    int _direction;

    public GameObject player;
    
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    Animator _anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move, move animation(true)
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= 5.0f && distance >= 1.4f)
        {
            _rb.linearVelocity = new Vector2(maxSpeed * MoveDirection(), _rb.linearVelocity.y);
            _anim.SetBool("isWalk", true);
            
        }
        //move animation(false)
        else
        {
            _anim.SetBool("isWalk", false);
        }
        
    }

    void EnemyJump()
    {
        Debug.DrawRay(_rb.position, Vector3.down, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.down, 20, LayerMask.GetMask("Platform"));
        if (rayHit.distance < 1.0f)
        {
            _rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    
    //get SlimeEnemy move-direction
    int MoveDirection()
    {
        //left direction, jump
        _direction = 0;
        if (transform.position.x - player.transform.position.x > 0)
        {
            _direction = -1;
            Debug.DrawRay(_rb.position, Vector3.left, new Color(0,1,0));
            RaycastHit2D rayHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.2f), Vector3.left, 10000000, LayerMask.GetMask("Platform"));
            if (rayHit.distance < 1.0f && rayHit.distance > 0.0f)
            {
                EnemyJump();
            }
        }
        //right direction, jump
        else if (transform.position.x - player.transform.position.x < 0)
        {
            _direction = 1;
            Debug.DrawRay(_rb.position, Vector3.right, new Color(0,1,0));
            RaycastHit2D rayHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.2f), Vector3.right, 10000000, LayerMask.GetMask("Platform"));
            if (rayHit.distance < 1.0f && rayHit.distance > 0.0f)
            {
                EnemyJump();
            }
        }
        
        return _direction;
    }

    public void OnDamaged()
    {
        _sr.color = new Color(1,0.3537736f,0.3537736f);
        _rb.AddForce(new Vector2(MoveDirection() * -20, _rb.linearVelocity.y), ForceMode2D.Impulse);
        Invoke("OffDamaged", 0.5f);
    }

    void OffDamaged()
    {
        _sr.color = new Color(1,1,1,1f);
    }
}
