using System;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public int direction;
    public float maxSpeed;

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

    //get SlimeEnemy move-direction
    int MoveDirection()
    {
        direction = 0;
        if (transform.position.x - player.transform.position.x > 0)
        {
            direction = -1;
        }
        else if (transform.position.x - player.transform.position.x < 0)
        {
            direction = 1;
        }
        
        return direction;
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
