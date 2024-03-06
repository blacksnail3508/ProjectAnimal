using DG.Tweening;
using LazyFramework;
using System.Threading.Tasks;
using UnityEngine;

public class Archer : IDamageable
{
    [Header("Master config")]
    [SerializeField] GameConfig gameConfig;

    [Header("Archer references")]
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] MoveDirection faceDirection;
    [SerializeField] GameObject shield;
    [Header("Sprite")]
    [SerializeField] Sprite normalArcher;
    [SerializeField] Sprite eliteArcher;
    private int shieldStrength = 0;
    private bool isElite = false;
    private SpriteRenderer spriteRenderer { get => GetComponent<SpriteRenderer>(); }

    public MoveDirection FaceDirection { get => faceDirection; set => faceDirection=value; }
    public void Move(MoveDirection direction)
    {
        faceDirection=direction;
        if (isDead==true)
        {
            return;
        }
        float rotation = ChangeRotation(direction);

        if (IsDirectionMoveable(direction))
        {
            Vector2 moveVector = Vector2.zero;
            switch (direction)
            {
                case MoveDirection.Left:
                    moveVector=Vector2.left;
                    positionX--;
                    break;

                case MoveDirection.Right:
                    moveVector=Vector2.right;
                    positionX++;
                    break;
                case MoveDirection.Up:
                    moveVector=Vector2.up;
                    positionY++;
                    break;
                case MoveDirection.Down:
                    moveVector=Vector2.down;
                    positionY--;
                    break;
            }

            Vector2 targetPosition = (Vector2)transform.position+moveVector;
            ChangeRotation(direction);

            AudioService.PlaySound(AudioName.Gliding);
            transform.DOMove(targetPosition , gameConfig.archerConfig.ArcherMoveTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                Shoot(direction);
                AudioService.PlaySound(AudioName.Arrow);
            });

        }
        else
        {
            transform.rotation=Quaternion.Euler(new Vector3(0 , 0 , ChangeRotation(direction)));
            Shoot(direction);
        }

        //check if new position have buff or environment
        OnArcherHit();
    }
    public int ChangeRotation(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                transform.rotation=Quaternion.Euler(new Vector3(0 , 0 , 90));
                return 90;
            case MoveDirection.Right:
                transform.rotation=Quaternion.Euler(new Vector3(0 , 0 , -90));
                return -90;
            case MoveDirection.Up:
                transform.rotation=Quaternion.Euler(new Vector3(0 , 0 , 0));
                return 0;
            case MoveDirection.Down:
                transform.rotation=Quaternion.Euler(new Vector3(0 , 0 , -180));
                return -180;
        }
        return 0;
    }
    private void Shoot(MoveDirection direction)
    {
        //var bullet = Instantiate(bulletPrefab);
        var bullet = BulletPool.instance.GetArcherBullet();

        GameServices.ArcherShoot();

        bullet.Fire(direction , this.gameObject , GameServices.ArcherShootEnd);

        //fire 2 additional arrows
        if (isElite)
        {
            GameServices.ArcherShoot();
            GameServices.ArcherShoot();
            var bullet1 = Instantiate(bulletPrefab);
            var bullet2 = Instantiate(bulletPrefab);
            switch (faceDirection)
            {
                case MoveDirection.Up:
                    bullet1.Fire(MoveDirection.Left , this.gameObject , GameServices.ArcherShootEnd);
                    bullet2.Fire(MoveDirection.Right , this.gameObject , GameServices.ArcherShootEnd);
                    break;
                case MoveDirection.Down:
                    bullet1.Fire(MoveDirection.Left , this.gameObject , GameServices.ArcherShootEnd);
                    bullet2.Fire(MoveDirection.Right , this.gameObject , GameServices.ArcherShootEnd);
                    break;
                case MoveDirection.Left:
                    bullet1.Fire(MoveDirection.Down , this.gameObject , GameServices.ArcherShootEnd);
                    bullet2.Fire(MoveDirection.Up , this.gameObject , GameServices.ArcherShootEnd);
                    break;
                case MoveDirection.Right:
                    bullet1.Fire(MoveDirection.Down , this.gameObject , GameServices.ArcherShootEnd);
                    bullet2.Fire(MoveDirection.Up , this.gameObject , GameServices.ArcherShootEnd);
                    break;
            }
        }
    }
    private bool IsDirectionMoveable(MoveDirection direction)
    {
        bool isMoveable = false;
        switch (direction)
        {
            case MoveDirection.Left:
                isMoveable=GameServices.IsPositionMoveable(positionX-1 , positionY);
                break;
            case MoveDirection.Right:
                isMoveable=GameServices.IsPositionMoveable(positionX+1 , positionY);
                break;
            case MoveDirection.Up:
                isMoveable=GameServices.IsPositionMoveable(positionX , positionY+1);
                break;
            case MoveDirection.Down:
                isMoveable=GameServices.IsPositionMoveable(positionX , positionY-1);
                break;
        }

        return isMoveable;
    }
    public override void OnHit()
    {
        if (shieldStrength>0)
        {
            shieldStrength-=1;
            if (shieldStrength<=0) { shield.SetActive(false); }
        }
        else
        {
            transform.DOKill();
            gameObject.SetActive(false);
            isDead=true;
            GameServices.OnArcherDie();
            AudioService.PlaySound(AudioName.ArcherDie);


        }
    }

    private async void OnArcherHit()
    {
        await Task.Delay((int)(gameConfig.archerConfig.ArcherMoveTime*1000-50));
        var list = GameServices.GetObjectsAtPosition(positionX , positionY);

        for (int i = 0; i<list.Count; i++)
        {
            var obj = list[i];
            if (obj==null) { return; }

            //buff
            if (obj.objectType==ObjectType.Shield)
            {
                shieldStrength+=gameConfig.buffConfig.ShieldStrength;
                if (shieldStrength>0) { shield.SetActive(true); }

                obj.GetComponent<Collectible>().OnCollected();
                return;
            }

            if (obj.objectType==ObjectType.Split)
            {
                spriteRenderer.sprite=eliteArcher;
                isElite=true;
                obj.GetComponent<Collectible>().OnCollected();
                return;
            }

            //environment
            if (obj.objectType==ObjectType.Teleport)
            {
                transform.DOKill();
                obj.GetComponent<Teleport>().DoTeleport(this);
                Shoot(faceDirection);
                return;
            }

            if (obj.objectType==ObjectType.IceSurface)
            {
                transform.DOKill();
                obj.GetComponent<IceSurface>().MoveAhead(this);
            }
            //trap
            if (obj.objectType==ObjectType.SpikeTrap)
            {
                obj.GetComponent<SpikeTrap>().OnArcherStep(this);
            }
        }
    }
}
