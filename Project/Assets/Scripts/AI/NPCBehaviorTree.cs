using UnityEngine;

public class NPCBehaviorTree : NPC
{
    private const int HEALTH_THRESHOLD = 50;
    private const int RANGE_HEAL = 5;

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

    bool CanUseGunPower()
    {
        return FindNearestGunPowerPack() != null && (FindNearestObject(enemy, FindNearestGunPowerPack()).tag == "GunPower");
    }
}
