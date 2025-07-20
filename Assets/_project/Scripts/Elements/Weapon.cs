using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public Transform shootStartTransform;
    public Bullet bulletPrefab;
    public float attackRateForMachineGun;
    public float attackRateForShotgun;
    public int shotgunBulletCount;
    public float spreadForShotgun;

    //private float _lastAttackTime;
    private float _attackTimer;

    //public GameObject bullets; for a more ordered hierarchy but whatever
    public ParticleSystem shootPS;
    public Light shootLight;
    //public ParticleSystem shellPS;

    public WeaponType weaponType;
    public GameObject machineGunMesh;
    public GameObject shotgunMesh;

    private void Update()
    {
        if (GameDirector.instance.gameState != GameState.GamePlay)
        {
            return;
        }
        if (Input.GetMouseButton(0) && _attackTimer > attackRateForMachineGun && 
            !EventSystem.current.IsPointerOverGameObject() && weaponType == WeaponType.MachineGun)
        {
            ShootForMachineGun();
        }

        if (Input.GetMouseButtonUp(0) && _attackTimer > attackRateForShotgun &&
            !EventSystem.current.IsPointerOverGameObject() && weaponType == WeaponType.Shotgun)
        {
            ShootForShotgun();
        }
        
        if ((weaponType == WeaponType.MachineGun && _attackTimer < attackRateForMachineGun + 1) || (weaponType == WeaponType.Shotgun && _attackTimer < attackRateForShotgun + 1))
        {
            _attackTimer += Time.deltaTime;
        }
        
    }

    private void ShootForShotgun()
    {
        for (int i = 0; i<shotgunBulletCount; i++)
        {
            var spread = new Vector3(Random.Range(-spreadForShotgun, spreadForShotgun),
            Random.Range(-spreadForShotgun, spreadForShotgun) * 0.5f,
            0); 
            var newBullet = Instantiate(bulletPrefab);
            var newBulletTransform = newBullet.transform;
            newBulletTransform.position = shootStartTransform.position;
            newBulletTransform.LookAt(newBulletTransform.position + shootStartTransform.forward + spread);
            newBullet.StartBullet(this);
        }
        _attackTimer = 0;
        GameDirector.instance.audioManager.PlayShotgunShootSFX();
        shootLight.DOKill();
        shootLight.intensity = 0;
        shootLight.DOIntensity(30, .1f).SetLoops(2, LoopType.Yoyo);
        shootPS.Play();
        GameDirector.instance.cameraHolder.ShakeCamera(.2f, .2f);
    }

    private void ShootForMachineGun()
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
        GameDirector.instance.cameraHolder.ShakeCamera(.2f,  .2f);
        //shellPS.Play();
    }

    public void WeaponButtonPressed(WeaponType wType)
    {
        weaponType = wType;
        if (weaponType == WeaponType.MachineGun)
        {
            shotgunMesh.SetActive(false);
            machineGunMesh.SetActive(true);
        }
        else if (weaponType == WeaponType.Shotgun)
        {
            machineGunMesh.SetActive(false);
            shotgunMesh.SetActive(true);
        }
    }
}

public enum WeaponType
{
    MachineGun,
    Shotgun,
}