namespace OneDay.Samples.FallingBlocks.States
{
    public static class StateConst
    {
        public const string BootState = "boot_state";
        public const string MenuState = "menu_state";
        public const string GameState = "game_state";
    }

    public static class TransitionsConst
    {
        public const string ToMenuState = "to_menu_state";
        public const string ToGameState = "to_game_state";
    }
}