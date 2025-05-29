using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shootStartTransform;
    public Bullet bulletPrefab;
    public float attackRate;
    //private float _lastAttackTime;
    private float _attackTimer;

    //public GameObject bullets; for a more ordered hierarchy but whatever
    public ParticleSystem shootPS;
    public Light shootLight;
    //public ParticleSystem shellPS;

    private void Update()
    {
        if (Input.GetMouseButton(0) && _attackTimer > attackRate)
        {
            Shoot();
        }
        
        if (_attackTimer < attackRate + 1)
        {
            _attackTimer += Time.deltaTime;
        }
        
    }

    private void Shoot()
    {
        var newBullet = Instantiate(bulletPrefab);
        var newBulletTransform = newBullet.transform;
        newBulletTransform.position = shootStartTransform.position;
        newBulletTransform.LookAt(newBulletTransform.position + shootStartTransform.forward);
        newBullet.StartBullet(this);
        //_lastAttackTime = Time.time;
        _attackTimer = 0;

        GameDirector.instance.audioManager.PlayMachineGunShootSFX();
        shootLight.DOKill();
        shootLight.intensity = 0;
        shootLight.DOIntensity(30, .1f).SetLoops(2, LoopType.Yoyo);
        shootPS.Play();
        GameDirector.instance.cameraHolder.ShakeCamera(.2f, .2f);
        //shellPS.Play();
    }
}
