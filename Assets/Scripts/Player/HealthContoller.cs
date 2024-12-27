using UnityEngine;

public class HealthController : MonoBehaviour, IDamagable, IHealth
{
    [SerializeField] private PlayerHealthBarUI healthBarUI;
    private int _health { get; set; }
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            
            healthBarUI.UpdateHealthBar(_health, maxHealth);
        }
    }
    [SerializeField] private int maxHealth;

    private void Start()
    {
        Health = maxHealth;
    }
    public void OnTakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnDie();
        }
    }
    public void OnDie()
    {
        GameManager.Instance.gameUI.TriggerDefeat();
        Debug.Log("Player is dead");
    }
}
