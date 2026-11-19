using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace L3_Survival;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _playerTexture;
    private Texture2D _offreTexture;
    private Texture2D _obstacleTexture;
    private Texture2D _coeurTexture;
    private Texture2D _fondTexture;

    private Vector2 _playerPosition;
    private List<Vector2> _offres = new List<Vector2>();
    private List<Vector2> _obstacles = new List<Vector2>();
    
    private int _vies = 3;
    private float _vitesse = 3f;
    private Random _random = new Random();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Chargement de TES images
        _playerTexture = Content.Load<Texture2D>("Textures/fille");
        _offreTexture = Content.Load<Texture2D>("Textures/offre");
        _obstacleTexture = Content.Load<Texture2D>("Textures/obstacle");
        _coeurTexture = Content.Load<Texture2D>("Textures/coeur");
        _fondTexture = Content.Load<Texture2D>("Textures/fond");

        _playerPosition = new Vector2(400, 450);

        for (int i = 0; i < 9; i++) {
            _offres.Add(new Vector2(_random.Next(50, 750), _random.Next(-600, 0)));
        }
        for (int i = 0; i < 5; i++) {
            _obstacles.Add(new Vector2(_random.Next(50, 750), _random.Next(-800, -200)));
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

        MouseState mouse = Mouse.GetState();
        _playerPosition.X = mouse.X - 50; // Centré sur la souris (taille 100)
        _playerPosition.Y = 450;

        if (_playerPosition.X < 0) _playerPosition.X = 0;
        if (_playerPosition.X > 700) _playerPosition.X = 700;

        // Offres
        for (int i = 0; i < _offres.Count; i++) {
            _offres[i] = new Vector2(_offres[i].X, _offres[i].Y + _vitesse);
            if (_offres[i].Y > 600) _offres[i] = new Vector2(_random.Next(50, 750), -100);

            Rectangle pRect = new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, 100, 100);
            Rectangle oRect = new Rectangle((int)_offres[i].X, (int)_offres[i].Y, 40, 40);
            
            if (pRect.Intersects(oRect)) {
                _offres[i] = new Vector2(_random.Next(50, 750), -100);
            }
        }

        // Obstacles
        for (int i = 0; i < _obstacles.Count; i++) {
            _obstacles[i] = new Vector2(_obstacles[i].X, _obstacles[i].Y + _vitesse);
            if (_obstacles[i].Y > 600) _obstacles[i] = new Vector2(_random.Next(50, 750), -100);

            Rectangle pRect = new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, 100, 100);
            Rectangle obRect = new Rectangle((int)_obstacles[i].X, (int)_obstacles[i].Y, 40, 40);
            
            if (pRect.Intersects(obRect)) {
                _vies--;
                _obstacles[i] = new Vector2(_random.Next(50, 750), -100);
                if (_vies <= 0) Exit();
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        
        // 1. Le fond (étiré sur tout l'écran)
        _spriteBatch.Draw(_fondTexture, new Rectangle(0, 0, 800, 600), Color.White);

        // 2. Les offres (forcées à 50x50 pixels)
        foreach (var o in _offres) 
            _spriteBatch.Draw(_offreTexture, new Rectangle((int)o.X, (int)o.Y, 50, 50), Color.White);
        
        // 3. Les obstacles (forcés à 60x60 pixels)
        foreach (var o in _obstacles) 
            _spriteBatch.Draw(_obstacleTexture, new Rectangle((int)o.X, (int)o.Y, 60, 60), Color.White);
        
        // 4. Le joueur (forcé à 100x100 pixels)
        _spriteBatch.Draw(_playerTexture, new Rectangle((int)_playerPosition.X, (int)_playerPosition.Y, 100, 100), Color.White);

        // 5. L'interface (les cœurs forcés à 40x40 pixels)
        for (int i = 0; i < _vies; i++) {
            _spriteBatch.Draw(_coeurTexture, new Rectangle(20 + (i * 50), 20, 40, 40), Color.White);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}