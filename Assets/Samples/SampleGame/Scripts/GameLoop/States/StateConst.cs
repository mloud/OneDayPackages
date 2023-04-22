namespace OneDay.Samples.FallingBlocks.States
{
    public static class StateNames
    {
        public static string PlayState = "play_state";
        public static string LostState = "lost_state";
        public static string WinState = "win_state";
    }

    public static class TransitionsNames
    {
        public static string ToLostState = "to_lost_state";
        public static string ToWinState = "to_win_state";
    }
}
