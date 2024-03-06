using LazyFramework;
using System.Threading.Tasks;
using UnityEngine;

public class BounceWall : BoardObject
{
    private void OnValidate()
    {
        ChangeRotation();
    }
    public void SetData(ObjectType wallType)
    {
        this.objectType=wallType;

        ChangeRotation();
    }
    private void ChangeRotation()
    {
        if (objectType==ObjectType.BounceWallGrave)
        {
            this.transform.rotation=Quaternion.Euler(new Vector3(0 , 0 , 45));
        }
        else
        {
            this.transform.rotation=Quaternion.Euler(new Vector3(0 , 0 , -45));
        }
    }
    public async void GraceAccentBounce(Bullet bullet)
    {
        AudioService.PlaySound(AudioName.Bounce);
        //smooth arow bounce
        await Task.Delay(30);
        bullet.transform.position=this.transform.position;
        switch (bullet.MoveDirection)
        {
            case MoveDirection.Left:
                bullet.MoveDirection=MoveDirection.Up;
                break;
            case MoveDirection.Right:
                bullet.MoveDirection=MoveDirection.Down;
                break;
            case MoveDirection.Down:
                bullet.MoveDirection=MoveDirection.Right;
                break;
            case MoveDirection.Up:
                bullet.MoveDirection=MoveDirection.Left;
                break;
        }
        bullet.ChangeRotation(bullet.MoveDirection);
    }
    public async void AcuteBounce(Bullet bullet)
    {
        AudioService.PlaySound(AudioName.Bounce);
        //smooth arow bounce
        await Task.Delay(30);
        bullet.transform.position=this.transform.position;
        switch (bullet.MoveDirection)
        {
            case MoveDirection.Left:
                bullet.MoveDirection=MoveDirection.Down;
                break;
            case MoveDirection.Right:
                bullet.MoveDirection=MoveDirection.Up;
                break;
            case MoveDirection.Down:
                bullet.MoveDirection=MoveDirection.Left;
                break;
            case MoveDirection.Up:
                bullet.MoveDirection=MoveDirection.Right;
                break;
        }
        bullet.ChangeRotation(bullet.MoveDirection);
    }
}
