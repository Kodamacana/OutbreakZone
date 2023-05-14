using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health ;
    [SerializeField] Slider enemyHealthBar;
    private void Awake()
    {
        enemyHealthBar.maxValue = health;
        enemyHealthBar.value =health;
    }
    public void TakeDamage (float amount)
    {
        health -= amount;

        StopCoroutine("HealthBarAnim");
        StartCoroutine(HealthBarAnim());

        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator HealthBarAnim()
    {
        enemyHealthBar.value = health;
        enemyHealthBar.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        enemyHealthBar.gameObject.SetActive(false);
    }
}
