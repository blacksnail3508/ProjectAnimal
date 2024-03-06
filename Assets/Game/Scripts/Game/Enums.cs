
public static class Enums
{

}
public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down
}
public enum ObjectType
{
    None = 0,
    Archer = 1,
    //Enemy / trap 2-20
    EnemyUp = 2,
    EnemyDown = 3,
    EnemyLeft = 4,
    EnemyRight = 5,

    BounceWallAcute = 6,
    BounceWallGrave = 7,

    Wall = 8,
    Box = 9,

    TurretUp = 10,
    TurretDown = 11,
    TurretLeft = 12,
    TurretRight = 13,

    //Collectible 21 - 30
    Shield = 21,
    Split = 22,

    //environment 31 - 40
    Teleport = 31,
    IceSurface = 32,

    //trap
    SpikeTrap = 41,
}
public enum GameState
{
    Idle,
    Playing,
}
