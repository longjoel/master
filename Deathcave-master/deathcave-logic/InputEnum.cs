namespace deathcave_logic
{
    
    public enum InputEnum : int
    {
        NoInput = 0,
        
        Player1Up = 1,
        Player1Down = 2,
        Player1Left = 4,
        Player1Right = 8,
        Player1Fire = 16,

        Player2Up = 32,
        Player2Down = 64,
        Player2Left = 128,
        Player2Right = 256,
        Player2Fire = 512
    }
}