using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private List<Bullet> listArcherBullet = new List<Bullet>();
    [SerializeField] private List<Bullet> listEnemyBullet = new List<Bullet>();
    [SerializeField] private Bullet archerbullet;
    [SerializeField] private Bullet enemyBullet;

    public Bullet GetArcherBullet()
    {
        for (int i = 0; i < listArcherBullet.Count; i++)
        {
            if (listArcherBullet[i].gameObject.activeSelf==false)
            {
                return listArcherBullet[i];
            }
        }

        return CreateArcherBullet();
    }
    public Bullet CreateArcherBullet()
    {
        Bullet bullet = Instantiate(archerbullet , this.transform);
        listArcherBullet.Add(bullet);
        return bullet;
    }

    public Bullet GetEnemyrBullet()
    {
        for (int i = 0; i<listEnemyBullet.Count; i++)
        {
            if (listEnemyBullet[i].gameObject.activeSelf==false)
            {
                return listEnemyBullet[i];
            }
        }

        return CreateEnemyBullet();
    }
    public Bullet CreateEnemyBullet()
    {
        Bullet bullet = Instantiate(enemyBullet , this.transform);
        listEnemyBullet.Add(bullet);
        return bullet;
    }
}
