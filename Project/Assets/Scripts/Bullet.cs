using UnityEngine;

public class Bullet : MonoBehaviour
{

    private int damage;
    private GameObject target;

    public int Damage
    {
        get => damage;
        set => damage = value;
    }
    public GameObject Target
    {
        get => target;
        set => target = value;
    }

    public void Init(int damage, GameObject target)
    {
        this.damage = damage;
        this.target = target;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject == Target)
        {
            Destroy(gameObject);
            Target.GetComponent<NPC>().TakeDamage(Damage);
        }
        
    }
}
