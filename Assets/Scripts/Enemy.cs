using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int nivel = 1;
    public int maxHP = 100;
    private int currentHP;
    public float damage = 10f;
    public Transform objetivo;

    void Start()
    {
        currentHP = maxHP + nivel * 20;
        transform.localScale *= 1f + 0.2f * (nivel - 1);
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            GameManager.Instance.AddScore(10);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (objetivo != null)
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, 2f * Time.deltaTime);
    }
}