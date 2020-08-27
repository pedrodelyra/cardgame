namespace Gameplay.Core
{
    public enum Team
    {
        None = 0,
        Home = 1,
        Visitor = 2,
    }

    public static class Teams
    {
        public static string GetTag(this Team team) => team.ToString();

        public static Team Opposite(this Team team)
        {
            switch (team)
            {
                case Team.Home:
                    return Team.Visitor;
                case Team.Visitor:
                    return Team.Home;
                default:
                    return Team.None;
            }
        }
    }
}
