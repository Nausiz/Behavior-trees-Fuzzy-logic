using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    protected const float SPEED = 12f;
    protected const float ACCELERATION = 55f;
    protected const float SHOOT_DELAY = 0.75f;
    protected const int DEFAULT_HEALTH = 100;
    protected const int RANGE_ATTACK = 10;
    protected const int RANGE_POWERUPS = 2;
    private const float BULLET_SPEED = 30.0f;
    private const int DEFAULT_GUNPOWER = 15;

    private int health;
    private int gunpower;
    private int wins;
    private int heals;
    private int powerups;
    private float shootTimer = 0.0f;

    protected NavMeshAgent agent;

    private GameObject[] healthPacks;
    private GameObject[] gunPowerPacks;

    [SerializeField] protected GameObject enemy;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform respawnPosition;
    [SerializeField] private Transform gun;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public int GunPower
    {
        get => gunpower;
        set => gunpower = value;
    }

    public int Wins
    {
        get => wins;
        set => wins = value;
    }

    public int Heals
    {
        get => heals;
        set => heals = value;
    }

    public int PowerUps
    {
        get => powerups;
        set => powerups = value;
    }


    public float ShootTimer
    {
        get => shootTimer;
        set => shootTimer = value;
    }

    void Awake()
    {
        Health = DEFAULT_HEALTH;
        GunPower = DEFAULT_GUNPOWER;
        Wins = 0;
        Heals = 0;
        PowerUps = 0;
        healthPacks = GameObject.FindGameObjectsWithTag("Heal");
        gunPowerPacks = GameObject.FindGameObjectsWithTag("GunPower");
        
        agent = GetComponent<NavMeshAgent>();
        agent.speed = SPEED;
        agent.acceleration = ACCELERATION;
    }

    public void ResetNPC()
    {
        agent.gameObject.transform.position = respawnPosition.transform.position;
        agent.gameObject.transform.rotation = respawnPosition.transform.rotation;

        Health = DEFAULT_HEALTH;
        GunPower = DEFAULT_GUNPOWER;
    }

    public void AddHealth(int heal)
    {
        Health += heal;

        if (Health > DEFAULT_HEALTH)
            Health = DEFAULT_HEALTH;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            ResetNPC();
            enemy.GetComponent<NPC>().ResetNPC();
            enemy.GetComponent<NPC>().Wins+=1;
        }
    }

    protected void ShootIfPossible()
    {
        if (CanSeeTarget())
        {
            if (Time.time > shootTimer)
            {
                Shoot();
                shootTimer = Time.time + SHOOT_DELAY;
            }
        }
    }

    private bool CanSeeTarget()
    {
        Vector3 direction = enemy.transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.gameObject.transform.position == enemy.transform.position)
            {
                return true;
            }
        }
        return false;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);
        bullet.GetComponent<Bullet>().Init(GunPower, enemy);
        bullet.GetComponent<Rigidbody>().velocity = (enemy.transform.position - transform.position).normalized * BULLET_SPEED;
    }

    protected GameObject FindNearestHealthPack()
    {
        float distance = Mathf.Infinity;
        GameObject nearestHealthPack = null;

        foreach (GameObject heal in healthPacks)
        {
            if (heal.activeSelf)
            {
                float currDistance = Vector3.Distance(transform.position, heal.transform.position);
                if (currDistance < distance)
                {
                    distance = currDistance;
                    nearestHealthPack = heal;
                }
            }
        }

        return nearestHealthPack;
    }

    protected GameObject FindNearestGunPowerPack()
    {
        float distance = Mathf.Infinity;
        GameObject nearestGunPowerPack = null;

        foreach (GameObject gunPower in gunPowerPacks)
        {
            if (gunPower.activeSelf)
            {
                float currDistance = Vector3.Distance(transform.position, gunPower.transform.position);
                if (currDistance < distance)
                {
                    distance = currDistance;
                    nearestGunPowerPack = gunPower;
                }
            }
        }

        return nearestGunPowerPack;
    }

    protected GameObject FindNearestObject(GameObject object1, GameObject object2)
    {
        if (object1 == null)
            return object2;
        else if (object2 == null)
            return object1;


        if (Vector3.Distance(transform.position, object1.transform.position) <=
            Vector3.Distance(transform.position, object2.transform.position))
            return object1;
        else
            return object2;
    }

    private void DestroyAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        for(int i = bullets.Length-1; i >= 0; i--)
        {
            Destroy(bullets[i]);
        }
    }

    protected void UseHeal()
    {
        agent.stoppingDistance = RANGE_POWERUPS;
        agent.SetDestination(FindNearestHealthPack().transform.position);
    }

    protected void UseGunPower()
    {
        agent.stoppingDistance = RANGE_POWERUPS;
        agent.SetDestination(FindNearestGunPowerPack().transform.position);
    }

    protected void Attack()
    {
        agent.stoppingDistance = RANGE_ATTACK;
        transform.rotation = Quaternion.LookRotation(enemy.transform.position - transform.position, Vector3.up);
        ShootIfPossible();
        agent.SetDestination(enemy.transform.position);
    }
}
