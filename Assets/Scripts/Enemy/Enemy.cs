using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IHealth, IDamage
{
    private int _health { get; set; }
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                OnDie();
            }
        }
    }
    public int Damage { get; set; }

    [SerializeField] private int maxHealth;

    private void Start()
    {
        Health = maxHealth;
    }

    public void OnTakeDamage(int damage)
    {
        Health -= damage;
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
