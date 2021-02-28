using Broth.Util;

namespace Broth.GameStates
{
    public abstract class GameState
    {
        //TODO: Resource Context Property

        public abstract void Initialize(Game game);
        public abstract void Update(Game game, GameTime gameTime);
        public abstract void Draw(Game game, GameTime gameTime);

        /// <summary>
        /// Each game state will handle escape calls differently.
        /// It might pause the game, close a menu, or exit the game.
        /// </summary>
        public abstract void OnEscape(Game game);
    }
}