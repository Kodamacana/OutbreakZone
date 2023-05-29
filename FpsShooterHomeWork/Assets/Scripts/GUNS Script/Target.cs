using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health ;
    [SerializeField] Slider enemyHealthBar;
    [SerializeField] GameObject Blood;
    [SerializeField] GameObject Ceset;
    private void Awake()
    {
        enemyHealthBar.maxValue = health;
        enemyHealthBar.value =health;
    }
    public void TakeDamage (float amount)
    {
        Instantiate(Blood, transform.position, Quaternion.identity);
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
        var pos = transform.position;
        var ceset = Instantiate(Ceset, new Vector3(pos.x,pos.y+1.6f,pos.z), Quaternion.identity);
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
