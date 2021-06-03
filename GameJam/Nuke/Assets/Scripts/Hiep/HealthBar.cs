using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Combat;
using Game.Global;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform planetModel;
    public SpriteRenderer healthBar;

    int maxHealth;    
    

    private void Start()
    {
        Init();
        
    }

    void Init()
    {
        Vector3 offset = new Vector3(0, planetModel.transform.localScale.x * 0.5f + 0.5f, 0);
        transform.localPosition = offset;

        // change size of bar
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.size = new Vector2(planetModel.transform.localScale.x * 1f, sprite.size.y);

        // change size of health inside bar
        healthBar.transform.localPosition = new Vector3(-(sprite.size.x / 2), 0, 0);
        healthBar.size = new Vector2(planetModel.transform.localScale.x * 1f, sprite.size.y);        
    }

    public void UpdateHealth(Health health)
    {
        healthBar.size = new Vector2(((float)health.health / maxHealth) * planetModel.transform.localScale.x, healthBar.size.y);
    }

    public void UpdateMaxHealth(Health health)
    {
        this.maxHealth = health.maxHealth;
    }
}
