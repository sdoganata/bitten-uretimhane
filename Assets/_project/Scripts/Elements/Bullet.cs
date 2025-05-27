using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float range;
    private Transform _tr;
    private Weapon _weapon;
    public int damage;

    public Color enemyImpactParticleColor;
    public Color groundImpactParticleColor;

    private void Awake()
    {
        _tr = transform;
    }

    public void StartBullet(Weapon weapon)
    {
        _weapon = weapon;
    }

    private void Update()
    {
        _tr.position += _tr.forward * Time.deltaTime * speed;
        if ((_tr.position - _weapon.transform.position).magnitude > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            other.GetComponent<Enemy>().GetHit(damage);
            //var particleColor = new Color(1, .2f, .2f, 1);
            GameDirector.instance.fxManager.PlayBulletImpactFX(_tr.position, _tr.forward, enemyImpactParticleColor);
            Destroy(gameObject);
        }
        if (other.CompareTag("Ground"))
        {
            GameDirector.instance.fxManager.PlayBulletImpactFX(_tr.position, _tr.forward, groundImpactParticleColor);
            Destroy(gameObject);
        }
    }
}
