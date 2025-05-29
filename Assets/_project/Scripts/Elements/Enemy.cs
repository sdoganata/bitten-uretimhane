using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private Collider _aliveCollider;
    public Collider deadCollider;
    public EnemyState enemyState;
    public int startHealth;
    private int _currentHealth;
    private NavMeshAgent _navAgent;
    private Player _player;
    private Transform _transform;
    public float attackRate;
    public float attackRange;
    public float attackAngleThreshold;
    private float _attackTimer;
    private HealthBar _healthBar;
    private Animator _animator;
    private bool _isAttackInProgress;
    public List<Light> eyeLights;
    public float flashDuration;
    public Material flashMaterial;
    
    public Material originalMaterial1;
    public Material originalMaterial2;
    public List<Renderer> renderers1;
    public List<Renderer> renderers2;
    /*private Rigidbody _rb;
    public float speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }*/

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _transform = transform;
        _healthBar = GetComponentInChildren<HealthBar>();
        _animator = GetComponentInChildren<Animator>();
        _aliveCollider = GetComponent<Collider>();
    }

    public void Start()
    {
        _player  = GameDirector.instance.player;
        _currentHealth = startHealth;
        _navAgent.isStopped = true;
        _healthBar.UpdateHealthBar(1);
        //enemyState = EnemyState.Walking;
    }

    /*private void Update()
    {
        if (enemyState == EnemyState.Walking)
        {
            var player = GameDirector.instance.player;
            var dir = player.transform.position - transform.position;
            _rb.linearVelocity = dir.normalized * speed;
        }
    }*/

    private void Update()
    {
        if (enemyState == EnemyState.Dead)
        {
            return;
        }


        var distanceToPlayer = Vector3.Distance(_player.transform.position, _transform.position);
        if (distanceToPlayer < attackRange)
        {
            enemyState = EnemyState.Attacking;
            _navAgent.isStopped = true;
            if (_attackTimer < attackRate + 1)
            {
                _attackTimer += Time.deltaTime;
            }
            if (_attackTimer > attackRate)
            {
                Attack();
            }

        }
        else if (distanceToPlayer < 20 && enemyState != EnemyState.Walking && !_isAttackInProgress)
        {
            StartWalking();
            _attackTimer = attackRate;

        }


        if (enemyState == EnemyState.Walking)
        {
            _navAgent.SetDestination(_player.transform.position);
        }

    }

    private void Attack()
    {
        //print("Attack");
        _isAttackInProgress = true;
        _animator.SetTrigger("Attack");
        _attackTimer = 0;
    }
    internal void AttackCompleted()
    {
        _isAttackInProgress = false;
        var angle = Vector3.Angle(_transform.position - _player.transform.position, transform.forward);
        var distance = Vector3.Distance(_transform.position, _player.transform.position);
        if (angle > attackAngleThreshold && distance < attackRange)
        {
            _player.GetHit();
        }
    }

    private void StartWalking()
    {
        enemyState = EnemyState.Walking;
        _animator.SetTrigger("Walk");
        _navAgent.isStopped = false;

        GameDirector.instance.audioManager.PlayZombieGrowlSFX();
    }

    public void GetHit(int damage)
    {
        GameDirector.instance.audioManager.PlayGetHitSFX();
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar((float)_currentHealth / startHealth);
        if (_currentHealth <= 0 && enemyState != EnemyState.Dead)
        {
            Die();
        }
        StartCoroutine(FlashEnemyCoRoutine());
    }

    private void Die()
    {
        var rb = GetComponent<Rigidbody>();
        
        _navAgent.isStopped = true;
        _navAgent.enabled = false;
        rb.useGravity = false;
        Destroy(gameObject, 5);
        if (Random.value < .5f)
        {
            _animator.SetTrigger("Die");
        } else
        {
            _animator.SetTrigger("Die 2");
        }
        transform.LookAt(_player.transform.position);
        enemyState = EnemyState.Dead;
        //Invoke(nameof(DisableCollider), 3);
        Invoke(nameof(ExpireEnemy), 3);
        DisableAliveCollider();
        foreach (var l in eyeLights)
        {
            //l.enabled = false;
            l.DOIntensity(0, 1);
        }
    }

    public void ExpireEnemy ()
    {
        var rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation |
            RigidbodyConstraints.FreezePositionX |
            RigidbodyConstraints.FreezePositionZ;
        deadCollider.enabled = false;
    }

    public void DisableAliveCollider()
    {
        _aliveCollider.enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
        deadCollider.enabled = true;
    }

    IEnumerator FlashEnemyCoRoutine()
    {
        foreach (var r in renderers1)
        {
            r.material = flashMaterial;
        }
        foreach (var r in renderers2)
        {
            r.material = flashMaterial;
        }
        yield return new WaitForSeconds(flashDuration);
        foreach (var r in renderers1)
        {
            r.material = originalMaterial1;
        }
        foreach (var r in renderers2)
        {
            r.material = originalMaterial2;
        }
    }
    
}

/*public enum EnemyType
{
    Basic,
    Boss,
    Ranged,
}*/

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead,
}
