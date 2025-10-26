using UnityEngine;

public class Weapon : MonoBehaviour
{
    bool _isHitting = false;
    
    public SlimeEnemy slime;
    public int weaponDamage = 10;
    
    //check on enemy hitting
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            _isHitting = true;
        }
    }

    //check off enemy hitting
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            _isHitting = false;
        }
    }
    
    //at weapon animation on attack enemy
    public void OnTryAttack()
    {
        if (_isHitting)
        {
            slime.OnDamaged(weaponDamage);
        }
    }

    //at the weapon animation end off weapon effect
    public void OnEndEffect()
    {
        //Debug.Log("OnEndEffect");
        this.gameObject.SetActive(false);
    }
}
