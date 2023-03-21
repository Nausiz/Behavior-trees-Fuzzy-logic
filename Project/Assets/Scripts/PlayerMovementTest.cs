using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    private const float SPEED = 20.0f;
    private const float BULLETSPEED = 40.0f;
    private const int DEFAULTHEALTH = 100;
    private const int DEFAULTGUNPOWER = 15;

    private int health;
    private int gunpower;
    private int wins;
    private int heals;
    private int powerups;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform respawnPosition;

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


    void Start()
    {
        Health = DEFAULTHEALTH;
        GunPower = DEFAULTGUNPOWER;
        Wins = 0;
        Heals = 0;
        PowerUps = 0;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        if (Input.GetKeyDown(KeyCode.V))
        {
            Shoot();
        }

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        transform.position += movement * SPEED * Time.deltaTime;

        //IF WIN
        //if (enemy.GetComponent<PlayerMovementTest>().Health <= 0)
        //{
        //    Wins += 1;
        //    ResetNPC();
        //}


    }

    void ResetNPC()
    {
        gameObject.transform.position = respawnPosition.position;
        gameObject.transform.rotation = respawnPosition.rotation;

        Health = DEFAULTHEALTH;
        GunPower = DEFAULTGUNPOWER;
    }

    public void AddHealth(int heal)
    {
        Health += heal;

        if (Health > DEFAULTHEALTH)
            Health = DEFAULTHEALTH;
    }

    public void dealDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            ResetNPC();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().Init(GunPower, enemy);
        bullet.GetComponent<Rigidbody>().velocity = (enemy.transform.position - transform.position).normalized * BULLETSPEED;
    }
}
