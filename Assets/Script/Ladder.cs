using UnityEngine;

public class Ladder : MonoBehaviour
{
    Rigidbody2D _rigid;

    float vertical;
    public float climbSpeed;
    bool _isLadder;
    bool _isClimbing;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ladder - _isClimbing
        vertical = Input.GetAxis("Vertical");
        if (_isLadder && Mathf.Abs(vertical) > 0f)
        {
            OnLadder();
            _isClimbing = true;
        }
        //off ladder state
        else if (_isClimbing == false)
        {
            OffLadder();
        }
    }
    
    //OnLadder state condition - _isLadder = true
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = true;
        }
        
    }
    // OffLadder state condition - _isLadder = false
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;
        }
    }

    void OnLadder()
    {
        _rigid.gravityScale = 0f;
        _rigid.linearVelocityY = climbSpeed * Mathf.Sign(vertical);
        if (vertical == 0f)
        {
            _rigid.linearVelocityY = 0.3f * Mathf.Sign(vertical);
        }
        
    }

    void OffLadder()
    {
        _rigid.gravityScale = 2f;
    }
}
