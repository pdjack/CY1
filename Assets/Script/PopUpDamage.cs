using UnityEngine;

public class PopUpDamage : MonoBehaviour
{
    public float lifeTime = 1.5f;
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

}
