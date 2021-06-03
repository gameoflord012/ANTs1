using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Combat;
using Game.Global;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform planetModel;
    [SerializeField] SpriteRenderer healthBar;

    int maxHealth;

    [SerializeField] Color32 green;
    [SerializeField] Color32 red;
    

    private void Start()
    {
        Init();
        
    }

    void Init()
    {
        Vector3 offset = new Vector3(0, planetModel.transform.localScale.x * 0.5f + 0.5f, 0);
        transform.localPosition = offset;
    }

    public void UpdateHealth(Health health)
    {
        healthBar.size = new Vector2(((float)health.health / maxHealth) * 1.25f, healthBar.size.y);
    }

    public void UpdateMaxHealth(Health health)
    {
        this.maxHealth = health.maxHealth;
    }
}
