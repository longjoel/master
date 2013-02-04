namespace deathcave_logic
{
    
    public enum GameObjectEnum : int
    {
        NoGameObject = 0,
        
        PlayerShip = 1,
        //Player2Ship = 2,
        
        PowerUpSheild = 4,
        PowerUpExtraLife = 8,
        
        PlayerProjectile = 16,
        EnemyProjectile = 32,

        EnemyDropper = 64,
        EnemySwooper = 128,
        EnemyShooter = 256,

        ObstacleWall = 512,

        EffectExplosion = 1024
    }
}