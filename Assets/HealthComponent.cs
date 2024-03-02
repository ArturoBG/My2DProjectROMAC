using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private int characterHealth; //HP character, static

    [SerializeField]
    private int currentHealth; // dynamic, HP restante

    private int maxHP;
    private float goodHp;
    private float halfHP;
    private float lowHP;
    private float healthPercentage;

    public void SetTotalHealth(int value)
    {
        currentHealth = value;
    }

    private void Start()
    {
        InitHealth();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        CheckColor();
    }

    public void HealUp(int value)
    {
        currentHealth += value;
        if (currentHealth > characterHealth)
        {
            currentHealth = maxHP;
        }
        CheckColor();
    }

    public void InitHealth()
    {
        currentHealth = characterHealth;
        maxHP = characterHealth;
        goodHp = maxHP * 0.75f;
        halfHP = maxHP * 0.5f;
        lowHP = maxHP * 0.25f;
    }

    private void CheckColor()
    {
        if (currentHealth >= goodHp && currentHealth <= maxHP && currentHealth > halfHP)
        {
            //green
            Debug.Log("green bar " + (float)currentHealth / maxHP);
        }
        else if (currentHealth < goodHp && currentHealth > lowHP)
        {
            //yellow
            Debug.Log("yellow bar " + (float)currentHealth / maxHP);
        }
        else if (currentHealth < halfHP && currentHealth > 0)
        {
            Debug.Log("red bar " + (float)currentHealth / maxHP);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HealUp(10);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            TakeDamage(15);
        }
    }
}