using UnityEngine;

public class NPCBehaviorTree : NPC
{
    private const int HEALTH_THRESHOLD = 50;
    void Update()
    {
        if (CanUseHeal())
        {
            UseHeal();
        }
        else if (CanUseGunPower())
        {
            UseGunPower();
        }
        else
        {
            Attack();
        }
    }
    bool CanUseHeal()
    {
        return FindNearestHealthPack() != null && Health <= HEALTH_THRESHOLD && Vector3.Distance(gameObject.transform.position, enemy.transform.position) >= RANGE_HEAL;
    }

    void UseHeal()
    {
        agent.stoppingDistance = RANGE_POWERUPS;
        agent.SetDestination(FindNearestHealthPack().transform.position);
    }

    bool CanUseGunPower()
    {
        return FindNearestGunPowerPack() != null && (FindNearestObject(enemy, FindNearestGunPowerPack()).tag == "GunPower");
    }

    void UseGunPower()
    {
        agent.stoppingDistance = RANGE_POWERUPS;
        agent.SetDestination(FindNearestGunPowerPack().transform.position);
    }

    void Attack()
    {
        agent.stoppingDistance = RANGE_ATTACK;
        transform.rotation = Quaternion.LookRotation(enemy.transform.position - transform.position, Vector3.up);
        ShootIfPossible();
        agent.SetDestination(enemy.transform.position);
    }
}
