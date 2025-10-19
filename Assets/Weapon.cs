using UnityEngine;

public class Weapon : MonoBehaviour
{
    bool _isHitting = false;
    
    public SlimeEnemy slime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            _isHitting = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            _isHitting = false;
        }
    }
    
    public void OnTryAttack()
    {
        if (_isHitting)
        {
            slime.OnDamaged();
        }
    }

    public void OnEndEffect()
    {
        //Debug.Log("OnEndEffect");
        this.gameObject.SetActive(false);
    }
}
