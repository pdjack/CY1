using UnityEngine;

public class Ladder : MonoBehaviour
{
    Rigidbody2D _rigid;

    public float vertical;
    public float climbSpeed;
    public bool _isLadder;
    public bool _isClimbing;
    
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
        Debug.Log("부호: " + Mathf.Sign(_rigid.linearVelocity.y));
        _rigid.gravityScale = 0f;
        _rigid.linearVelocityY = climbSpeed * Mathf.Sign(vertical);
        if (vertical == 0f)
        {
            _rigid.linearVelocityY = 0.3f * Mathf.Sign(vertical);
            Debug.Log("곱한 값: " + _rigid.linearVelocity.y);
        }
        
    }

    void OffLadder()
    {
        _rigid.gravityScale = 1f;
    }
}
