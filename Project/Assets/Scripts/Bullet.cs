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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject == Target)
        {
            Target.GetComponent<NPC>().TakeDamage(Damage);
        }
    }
}
