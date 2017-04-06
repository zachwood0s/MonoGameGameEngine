using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MonoGameGameEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static MoonSharp.Interpreter.Script lua = new MoonSharp.Interpreter.Script();
        public static string BASE_PATH = "../../../../Content/GameData/";

        private Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
        private Scene _currentScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Scene test = new Scene();
            //Entity fake = new Entity("test", test);
            // fake.AddComponent(new Location2(fake));
            //fake.AddComponent(new Texture2(fake));
            //fake.RemoveComponent("Texture2");
            //fake.AddComponent("Texture2");
            //test.AddEntity(fake);

            //JObject obj = JObject.Parse("{test:'wow'}");
            StreamReader sr = new StreamReader(BASE_PATH+"Scenes/TestScenes.lua");

            string scene = sr.ReadToEnd();
            Debug.WriteLine(scene);
            sr.Close();

            DynValue val = Script.RunString("return {"+scene+"}");

            Table scenes = val.Table;
            
            foreach(DynValue key in scenes.Keys)
            {
                Debug.WriteLine(key.String + ": ");
                Scene newScene = new Scene();

                Table entities = (Table) scenes[key];
                foreach(DynValue key2 in entities.Keys)
                {
                    Entity newEntity = EntityFactory.LoadEntityFromTable((Table) entities[key2], newScene, Content);
                    if (newEntity != null) newScene.AddEntity(newEntity);
                    //Debug.WriteLine("\t" + key2.String + ": " + entities[key2].ToString());
                }

                _scenes.Add(key.String, newScene);
            }

            _currentScene = _scenes["Room1"];
            //EntityFactory.LoadEntityFromTable(val.Table, test);

            //Debug.WriteLine(obj["test"]);
            
            //_scenes.Add(test);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //foreach (Scene scene in _scenes)
            //{
            //    scene.Update(spriteBatch, gameTime);
            //}
            _currentScene.Update(spriteBatch, gameTime);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
