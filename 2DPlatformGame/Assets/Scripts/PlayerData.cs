using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = nameof(PlayerData))]
public class PlayerData : ScriptableObject
{
    public float maxMovementSpeed;
    public float maxHealth;
    public float healthPotionCount;
    public float jumpForce;

    public void UpdateData(ItemType type)
    {
        switch (type)
        {
            case ItemType.JumpUpgrade:
                jumpForce += jumpForce * 0.1f;
                break;
            case ItemType.MovementSpeedUpgrade:
                maxMovementSpeed += maxMovementSpeed * 0.1f;
                break;
            case ItemType.HealthPotion:
                healthPotionCount += 1;
                break;
            case ItemType.MaxHealthUpgrade:
                maxHealth += maxHealth * 0.1f;
                break;
            default:
                break;
        }
    }
}