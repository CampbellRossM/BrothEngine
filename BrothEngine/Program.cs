using Broth.Engine;

namespace Broth
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game;
            if (args.Length > 0)
                game = new Game(args[0]);
            else
                game = new Game();

            game.Run();
        }
    }
}
