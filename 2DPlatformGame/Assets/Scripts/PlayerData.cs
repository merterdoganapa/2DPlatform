using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = nameof(PlayerData))]
public class PlayerData : ScriptableObject
{
    public float maxMovementSpeed;
    public int maxHealth;
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
            case ItemType.MaxHealthUpgrade:
                maxHealth += 10;
                break;
            default:
                break;
        }
    }
}