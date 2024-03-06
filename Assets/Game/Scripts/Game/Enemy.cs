using LazyFramework;
using System;
using UnityEngine;

public class Enemy : IDamageable
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] MoveDirection faceDirection;
    public Enemy() { }

    public void SetDirection(MoveDirection faceDirection)
    {
        this.faceDirection=faceDirection;
        float rotation = 0;
        switch (faceDirection)
        {
            case MoveDirection.Left:
                rotation=90;
                break;
            case MoveDirection.Right:
                rotation=-90;
                break;
            case MoveDirection.Up:
                rotation=0;
                break;
            case MoveDirection.Down:
                rotation=-180;
                break;
        }
        transform.rotation=Quaternion.Euler(new Vector3(0 , 0 , rotation));
    }
    public void Fire()
    {
        if (isDead==true)
        {
            return;
        }
        //var bullet = Instantiate(bulletPrefab);
        var bullet = BulletPool.instance.GetEnemyrBullet();

        Action onBulletHit = () =>
        {
            GameServices.OnEnemyShootEnd();
        };

        bullet.Fire(faceDirection , this.gameObject , onBulletHit);
        AudioService.PlaySound(AudioName.Arrow);
        GameServices.OnEnemyShoot();
    }

    public override void OnHit()
    {
        base.OnHit();
        GameServices.OnEnemyDie();
        AudioService.PlaySound(AudioName.EnemyDie);
    }
}
