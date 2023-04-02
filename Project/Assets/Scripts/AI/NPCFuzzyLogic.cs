using UnityEngine;

public class NPCFuzzyLogic : NPC
{
    private const int LOW_HEALTH_THRESHOLD = 25;
    private const int MID_HEALTH_THRESHOLD = 75;
    private const int CLOSE_CAN_USE_HEAL_DISTANCE_THRESHOLD = 5;
    private const int FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD = 10;
    private const int CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD = 15;
    private const int FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD = 20;
    private const float ATTACK_THRESHOLD = 0.2f;

    void Update()
    {
        if (CalculateCanUseGunPowerValue() >= CalculateCanUseHealValue() && CalculateCanUseGunPowerValue() > ATTACK_THRESHOLD)
        {
            UseGunPower();
        }
        else if (CalculateCanUseHealValue() >= CalculateCanUseGunPowerValue() && CalculateCanUseHealValue() > ATTACK_THRESHOLD)
        {
            UseHeal();
        }
        else
        {
            Attack();
        }
    }


    private float CalculateCanUseHealValue()
    {
        if (FindNearestHealthPack() == null)
            return 0.0f;

        float lowHealthValue = 0.0f;
        if (Health <= LOW_HEALTH_THRESHOLD)
        {
            lowHealthValue = 1.0f;
        }
        else if (Health > LOW_HEALTH_THRESHOLD && Health <= MID_HEALTH_THRESHOLD)
        {
            lowHealthValue = (MID_HEALTH_THRESHOLD - Health) / (MID_HEALTH_THRESHOLD - LOW_HEALTH_THRESHOLD);
        }

        float distanceToEnemy = Vector3.Distance(gameObject.transform.position, enemy.transform.position);

        float closeDistanceValue = 0.0f;
        if (distanceToEnemy >= CLOSE_CAN_USE_HEAL_DISTANCE_THRESHOLD)
        {
            closeDistanceValue = 1.0f;
        }
        else if (distanceToEnemy > FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD)
        {
            closeDistanceValue = (FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD - distanceToEnemy) / (FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD - CLOSE_CAN_USE_HEAL_DISTANCE_THRESHOLD);
        }

        float canUseHealValue = Mathf.Min(lowHealthValue, closeDistanceValue);

        return canUseHealValue;
    }

    private float CalculateCanUseGunPowerValue()
    {
        if (FindNearestGunPowerPack() == null)
            return 0.0f;

        float distanceToEnemy = Vector3.Distance(gameObject.transform.position, enemy.transform.position);

        float closeEnemyValue = 0.0f;
        if (distanceToEnemy <= CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD)
        {
            closeEnemyValue = 1.0f;
        }
        else if (distanceToEnemy > CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD && distanceToEnemy < FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD)
        {
            closeEnemyValue = (FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD - distanceToEnemy) / (FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD - CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD);
        }

        float distanceToGunPower = Vector3.Distance(gameObject.transform.position, FindNearestGunPowerPack().transform.position);

        float closeGunPowerValue = 0.0f;
        if (distanceToGunPower <= CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD)
        {
            closeGunPowerValue = 1.0f;
        }
        else if (distanceToGunPower > CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD && distanceToGunPower < FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD)
        {
            closeGunPowerValue = (FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD - distanceToGunPower) / (FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD - CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD);
        }

        float canUseGunPowerValue = Mathf.Min(closeEnemyValue, closeGunPowerValue);

        return canUseGunPowerValue;
    }
}
