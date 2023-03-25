using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    protected const float SPEED = 5f;
    protected const float ACCELERATION = 8f;
    protected const float SHOOT_DELAY = 0.5f;
    protected const int DEFAULT_HEALTH = 100;
    private const float BULLET_SPEED = 35.0f;
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


    void Start()
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

    void ResetNPC()
    {
        gameObject.transform.position = respawnPosition.position;
        gameObject.transform.rotation = respawnPosition.rotation;

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
}
