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
    
    //attack enemy
    public void OnTryAttack()
    {
        if (_isHitting)
        {
            slime.OnDamaged();
        }
    }

    //end weapon effect
    public void OnEndEffect()
    {
        //Debug.Log("OnEndEffect");
        this.gameObject.SetActive(false);
    }
    
    //void get weapon damage
    public int WeaponDamage()
    {
        return weaponDamage;
    }
}
