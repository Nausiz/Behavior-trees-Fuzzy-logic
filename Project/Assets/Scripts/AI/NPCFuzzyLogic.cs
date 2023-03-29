using UnityEngine;

public class NPCFuzzyLogic : NPC
{
    private const int LOW_HEALTH_THRESHOLD = 25;
    private const int MID_HEALTH_THRESHOLD = 50;
    private const int CLOSE_CAN_USE_HEAL_DISTANCE_THRESHOLD = 5;
    private const int FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD = 10;
    private const int CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD = 7;
    private const int FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD = 12;
    private const float ATTACK_THRESHOLD = 0.2f;

    void Update()
    {
        //DO POPRAWY
        if (CalculateCanUseGunPowerValue() >= ATTACK_THRESHOLD || CalculateCanUseHealValue() >= ATTACK_THRESHOLD)
        {
            Attack();
        }
        else if (CalculateCanUseGunPowerValue() >= CalculateCanUseHealValue() && CalculateCanUseGunPowerValue() > 0)
        {
            UseGunPower();
        }
        else
        {
            UseHeal();
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
        else if (Health > LOW_HEALTH_THRESHOLD && Health < MID_HEALTH_THRESHOLD)
        {
            lowHealthValue = (MID_HEALTH_THRESHOLD - Health) / (MID_HEALTH_THRESHOLD - LOW_HEALTH_THRESHOLD);
        }

        float midHealthValue = 0.0f;
        if (Health >= LOW_HEALTH_THRESHOLD && Health <= MID_HEALTH_THRESHOLD)
        {
            midHealthValue = (Health - LOW_HEALTH_THRESHOLD) / (MID_HEALTH_THRESHOLD - LOW_HEALTH_THRESHOLD);
        }
        else if (Health > MID_HEALTH_THRESHOLD && Health < DEFAULT_HEALTH)
        {
            midHealthValue = (DEFAULT_HEALTH - Health) / (DEFAULT_HEALTH - MID_HEALTH_THRESHOLD);
        }


        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, enemy.transform.position);

        float closeDistanceValue = 0.0f;
        if (distanceFromPlayer <= CLOSE_CAN_USE_HEAL_DISTANCE_THRESHOLD)
        {
            closeDistanceValue = 1.0f;
        }
        else if (distanceFromPlayer > CLOSE_CAN_USE_HEAL_DISTANCE_THRESHOLD && distanceFromPlayer < FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD)
        {
            closeDistanceValue = (FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD - distanceFromPlayer) / (FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD - CLOSE_CAN_USE_HEAL_DISTANCE_THRESHOLD);
        }

        float canUseHealValue = Mathf.Min(lowHealthValue, midHealthValue, closeDistanceValue);

        return canUseHealValue;
    }

    private float CalculateCanUseGunPowerValue()
    {
        if (FindNearestGunPowerPack() == null)
            return 0.0f;

        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, enemy.transform.position);

        float closeEnemyValue = 0.0f;
        if (distanceFromPlayer <= CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD)
        {
            closeEnemyValue = 1.0f;
        }
        else if (distanceFromPlayer > CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD && distanceFromPlayer < FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD)
        {
            closeEnemyValue = (FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD - distanceFromPlayer) / (FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD - CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD);
        }

        float closeGunPowerValue = 0.0f;
        if (distanceFromPlayer <= CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD)
        {
            closeGunPowerValue = 1.0f;
        }
        else if (distanceFromPlayer > CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD && distanceFromPlayer < FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD)
        {
            closeGunPowerValue = (FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD - distanceFromPlayer) / (FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD - CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD);
        }

        float canUseGunPowerValue = Mathf.Min(closeEnemyValue, closeGunPowerValue);

        return canUseGunPowerValue;
    }
}
