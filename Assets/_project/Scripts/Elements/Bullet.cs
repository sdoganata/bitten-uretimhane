using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float range;
    private Transform _tr;
    private Weapon _weapon;
    public int damage;

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
            Destroy(gameObject);
        }
    }
}
