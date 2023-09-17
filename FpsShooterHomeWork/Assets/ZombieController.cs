using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieController : MonoBehaviour
{
    public Transform player;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool playerSeen = false;
    private bool playerDead = false;
    private bool damaged = false;
    private bool isWalking = false;
    private bool isRandomWalking = false;
    private float idleTimer = 0f;
    private float AudioDelayTimer = 0f;
    private float AttackTimer = 0f;
    private float idleDuration = 3f;
    private float AudioDelayDuration = 1f;
    private float AttackDuration = 0.5f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float RunSpeed = 5f;
    private float rotationSpeed = 2f;
    AudioSource audioSource;
    AudioClip audioClip;
    [SerializeField] AudioClip[] idleSounds;
    [SerializeField] AudioClip ScreamSound;
    [SerializeField] AudioClip AttackSound;

    bool call = true;

    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StopAllAnimation();
        animator.SetBool("Idle1", true);
        AudioClip randomClip = idleSounds[Random.Range(0, idleSounds.Length)];
        audioClip = randomClip;
        PlayRandomSound();        
        // NavMeshAgent bile?enini al
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // Hareket h?z?n? ayarla
        navMeshAgent.speed = RunSpeed;
    }

    void StopAllAnimation()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle1", false);
        animator.SetBool("Idle2", false);
        animator.SetBool("Biting", false);
        animator.SetBool("Run", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);

    }

    private void Update()
    {
        if (playerDead)
        {
            StopAllAnimation();
            animator.SetBool("Biting", true);
            return;
        }

        if (IsPlayerinArea())
        {
            StopAllAnimation();
            MoveToPlayer();
            animator.SetBool("Run", true);
            navMeshAgent.speed = RunSpeed;
            isWalking = false;
            isRandomWalking = false;
        }

        if (!playerSeen)
        {
            if (damaged)
            {
                StopAllAnimation();
                animator.SetBool("Run", true);
                navMeshAgent.speed = RunSpeed;
                damaged = false;
            }

            CheckPlayerVisibility();
            return;
        }

        if (IsPlayerClose())
        {
            AttackTimer += Time.deltaTime;
            if (AttackTimer >= AttackDuration)
            {
                AttackTimer = 0f;
                AttackDuration = 1f;
                StopAllAnimation();
                int randomAttack = Random.Range(1, 3);
                animator.SetBool("Attack1", randomAttack == 1);
                animator.SetBool("Attack2", randomAttack == 2);
                AttackPlayer();
            }
        }
        else
        {
            if (IsPlayerInSight())
            {
                StopAllAnimation();
                MoveToPlayer();
                animator.SetBool("Run", true);
                navMeshAgent.speed = RunSpeed;
                isWalking = false;
                isRandomWalking = false;
            }            
            else
            {
                if (!isWalking && !isRandomWalking)
                {
                    StopAllAnimation();
                    animator.SetBool("Walk", true);
                    navMeshAgent.speed = walkSpeed;

                    idleTimer += Time.deltaTime;
                    if (idleTimer >= idleDuration)
                    {
                        isRandomWalking = true;
                        idleTimer = 0f;

                        StopAllAnimation();
                        int randomIdle = Random.Range(1, 3);
                        AudioClip randomClip = idleSounds[Random.Range(0, idleSounds.Length)];
                        audioClip = randomClip;
                        PlayRandomSound();

                        animator.SetBool("Idle1", randomIdle == 1);
                        animator.SetBool("Idle2", randomIdle == 2);
                    }
                }
                else if (isRandomWalking)
                {
                    Invoke("StopRandomWalk", 10f);
                    StopAllAnimation();
                    RandomWalk();
                    //if (call)
                    //{
                    //    call = false;
                    //    var value = Random.Range(0, 7f);
                    //    StartCoroutine(CallScreamAnim(value));
                    //}
                    
                    animator.SetBool("Walk", true);
                    navMeshAgent.speed = walkSpeed;
                }
            }
        }
    }
    
    IEnumerator CallScreamAnim(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Scream();
    }

    private void CheckPlayerVisibility()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle < 60f && 2.5f < angle)
        {
            StopAllAnimation();
            playerSeen = true;
            animator.SetBool("Run", true);
            navMeshAgent.speed = RunSpeed;
        }
    }

    private bool IsPlayerClose()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < 2.5f;
    }

    private bool IsPlayerinArea()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < 20f && distance > 2.5f;
    }

    private bool IsPlayerInSight()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    private void MoveToPlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    private void RandomWalk()
    {
        if (!navMeshAgent.hasPath)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 2f;
            randomDirection.y = 0f;
            randomDirection = transform.TransformDirection(randomDirection);
            Vector3 targetPosition = transform.position + randomDirection;
            Quaternion targetRotation = Quaternion.LookRotation(randomDirection);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            // Hedefe do?ru ilerle
            navMeshAgent.SetDestination(targetPosition);
        }
    }

    private void StopRandomWalk()
    {
        isRandomWalking = false;
        StopAllAnimation();
        animator.SetBool("Idle2", true);

        AudioClip randomClip = idleSounds[Random.Range(0, idleSounds.Length)];
        audioClip = randomClip;
        PlayRandomSound();

        // Zombi'nin hareketini durdur
        navMeshAgent.ResetPath();
    }

    void PlayRandomSound()
    {
        AudioDelayTimer += Time.deltaTime;
        if (!audioClip.name.Contains( AttackSound.name))
        {
            if (AudioDelayTimer >= 2f)
            {
                AudioDelayTimer = 0f;

                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                if (audioClip != null) audioSource.clip = audioClip;
                audioSource.Play();

                CancelInvoke("PlayRandomSound");
                // Ses ?al?nd?ktan sonra rastgele bir s?re bekleyerek tekrar ?al
                float randomTime = Random.Range(5f, 10f); // ?rne?in 5 ile 10 saniye aras?nda bir s?re se?
                Invoke("PlayRandomSound", randomTime);

            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            if (audioClip != null) audioSource.clip = audioClip;
            audioSource.Play();

            CancelInvoke("PlayRandomSound");
            // Ses ?al?nd?ktan sonra rastgele bir s?re bekleyerek tekrar ?al
            float randomTime = Random.Range(5f, 10f); // ?rne?in 5 ile 10 saniye aras?nda bir s?re se?
            Invoke("PlayRandomSound", randomTime);

        }
        
       
    }

    private void AttackPlayer()
    {
        audioClip = AttackSound;
        PlayRandomSound();

        player.GetComponent<PlayerHealth>().TakeDamage(10);
        // Player'a hasar verme i?lemleri burada ger?ekle?tirilir.
    }

    private void Scream()
    {
        StopAllCoroutines();
        StopAllAnimation();
        animator.SetBool("Scream", true);
        
        audioClip = ScreamSound;
        PlayRandomSound();
        
        call = true;
    }

    public void SetDamaged()
    {
        damaged = true;
    }

    public void SetPlayerDead()
    {
        playerDead = true;
    }
    
}
