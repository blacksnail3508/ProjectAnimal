using LazyFramework;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform sprite;
    [Header("Config")]
    [SerializeField] GameConfig gameConfig;
    [SerializeField] bool isArcherBullet;
    float bulletSpeed;
    [HideInInspector] public MoveDirection MoveDirection;
    public Action callback;
    Vector2 boardPosition;
    Vector2 lastPosition;

    private void Awake()
    {
        if (gameConfig!=null)
        {
            bulletSpeed=gameConfig.bulletConfig.bulletSpeed;
        }
    }
    public void Fire(MoveDirection direction , GameObject firePoint , Action OnReturnPool)
    {
        this.gameObject.SetActive(true);

        this.MoveDirection = direction;
        this.transform.position = firePoint.transform.position;
        ChangeRotation(direction);
        //cache callback
        this.callback = OnReturnPool;
    }
    private void LateUpdate()
    {
        transform.position += DirectionToVector(MoveDirection)*bulletSpeed*Time.deltaTime;

        //check for slot
        boardPosition = GameServices.TransformPositionToBoardPosition(transform.position);

        if (GameServices.IsBulletOnBoard((int)boardPosition.x, (int)boardPosition.y) == false)
        {
            ReturnPool();
            return;
        }

        if (lastPosition != boardPosition)
        {
            lastPosition = boardPosition;
            OnBulletHit();
        }
    }

    public void OnBulletHit()
    {
        var list = GameServices.GetObjectsAtPosition((int)boardPosition.x , (int)boardPosition.y);

        if (list == null) return;
        for(int i = 0; i< list.Count; i++)
        {
            var obj = list[i];
            //archer bullet hit enemy/turret
            if (isArcherBullet==true)
            {
                if (obj.objectType==ObjectType.EnemyUp||
                    obj.objectType==ObjectType.EnemyDown||
                    obj.objectType==ObjectType.EnemyLeft||
                    obj.objectType==ObjectType.EnemyRight||

                    obj.objectType==ObjectType.TurretUp||
                    obj.objectType==ObjectType.TurretDown||
                    obj.objectType==ObjectType.TurretLeft||
                    obj.objectType==ObjectType.TurretRight)
                {
                    obj.GetComponent<IDamageable>().OnHit();
                    ReturnPool();
                    return;
                }
            }
            //enemy bullet
            else
            {
                if (obj.objectType==ObjectType.Archer)
                {
                    obj.GetComponent<IDamageable>().OnHit();
                    ReturnPool();
                    return;
                }
            }

            //hit wall
            if (obj.objectType==ObjectType.BounceWallGrave)
            {
                obj.GetComponent<BounceWall>().GraceAccentBounce(this);
                return;
            }
            if (obj.objectType==ObjectType.BounceWallAcute)
            {
                obj.GetComponent<BounceWall>().AcuteBounce(this);
                return;
            }
            if (obj.objectType==ObjectType.Wall)
            {
                ReturnPool();
            }
            //hit Boxes
            if (obj.objectType==ObjectType.Box)
            {
                obj.GetComponent<IDamageable>().OnHit();
                ReturnPool();
                return;
            }
        }
    }
    public int ChangeRotation(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , 90));
                return 90;
            case MoveDirection.Right:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , -90));
                return -90;
            case MoveDirection.Up:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , 0));
                return 0;
            case MoveDirection.Down:
                sprite.rotation=Quaternion.Euler(new Vector3(0 , 0 , -180));
                return -180;
        }
        return 0;
    }

    private Vector3 DirectionToVector(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Up:
                return Vector2.up;
            case MoveDirection.Down:
                return Vector2.down;
            case MoveDirection.Left:
                return Vector2.left;
            case MoveDirection.Right:
                return Vector2.right;
            default: return Vector2.zero;
        }
    }

    private void ReturnPool()
    {
        callback?.Invoke();
        gameObject.SetActive(false);
    }
}
