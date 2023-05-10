using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float damage = 10f; // B��a��n verdi�i hasar de�eri
    public float attackSpeed = 1f; // B��a��n sald�r� h�z�
    public float attackRange = 2f; // B��a��n sald�r� menzili

    private float nextAttackTime = 0f; // Sonraki sald�r� zaman�

    // B��ak objesi tetiklendi�i zaman �al��acak fonksiyon
    private void OnTriggerEnter(Collider other)
    {
        // E�er b��ak bir d��mana �arparsa
        if (other.CompareTag("Enemy"))
        {
            // D��mana hasar ver
            Target enemy = other.GetComponent<Target>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    // B��ak sald�r�s� fonksiyonu
    public void Attack()
    {
        // E�er sald�r� h�z� zaman� gelmi�se
        if (Time.time >= nextAttackTime)
        {
            // B��a�� animasyonla oynat
            // (Animasyon k�sm� size ve b��a��n�z�n modeline ba�l�d�r.)

            // B��a��n menzili i�indeki d��manlar� tespit et
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

            // Her tespit edilen d��man� d�ng�ye al
            foreach (Collider enemy in hitEnemies)
            {
                // E�er d��man tag'i "Enemy" ise
                if (enemy.CompareTag("Enemy"))
                {
                    // D��mana hasar ver
                    Target enemyScript = enemy.GetComponent<Target>();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage(damage);
                    }
                }
            }

            // Sonraki sald�r� zaman�n� hesapla
            nextAttackTime = Time.time + 1f / attackSpeed;
        }
    }
}
