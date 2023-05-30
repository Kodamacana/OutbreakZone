using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float damage = 10f; // Býçaðýn verdiði hasar deðeri
    public float attackSpeed = 1f; // Býçaðýn saldýrý hýzý
    public float attackRange = 2f; // Býçaðýn saldýrý menzili

    private float nextAttackTime = 0f; // Sonraki saldýrý zamaný

    // Býçak objesi tetiklendiði zaman çalýþacak fonksiyon
    private void OnTriggerEnter(Collider other)
    {
        // Eðer býçak bir düþmana çarparsa
        if (other.CompareTag("Enemy"))
        {
            // Düþmana hasar ver
            Target enemy = other.GetComponent<Target>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    // Býçak saldýrýsý fonksiyonu
    public void Attack()
    {
        // Eðer saldýrý hýzý zamaný gelmiþse
        if (Time.time >= nextAttackTime)
        {
            // Býçaðý animasyonla oynat
            // (Animasyon kýsmý size ve býçaðýnýzýn modeline baðlýdýr.)

            // Býçaðýn menzili içindeki düþmanlarý tespit et
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

            // Her tespit edilen düþmaný döngüye al
            foreach (Collider enemy in hitEnemies)
            {
                // Eðer düþman tag'i "Enemy" ise
                if (enemy.CompareTag("Enemy"))
                {
                    // Düþmana hasar ver
                    Target enemyScript = enemy.GetComponent<Target>();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage(damage);
                    }
                }
            }

            // Sonraki saldýrý zamanýný hesapla
            nextAttackTime = Time.time + 1f / attackSpeed;
        }
    }
}
