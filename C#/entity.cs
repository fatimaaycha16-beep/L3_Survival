using Microsoft.Xna.Framework;

namespace L3_Survival {
    
    public abstract class Entity
    {
        public Vector2 Position; // vecteur qui a x et y et cest celui la que on va utiliser pour tous les autre class
        public int Width;
        public int Height;

        public Entity(float x, float y, int width, int height)
        {
            Position = new Vector2(x, y);
            Width = width;
            Height = height;
        }
        public abstract void Update(GameTime gameTime);
    }
}