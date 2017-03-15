using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameGameEngine
{
    public class Scene
    {
        //private delegate void System(SpriteBatch spriteBatch, GameTime gameTime, Entity entity);

        private List<Entity> _entities;
        private SystemManager _systemManager;

        public Scene()
        {
            _entities = new List<Entity>();
            _systemManager = new SystemManager();
            _systemManager.LoadSystems();
        }

        public SystemManager SystemManager
        {
            get
            {
                return _systemManager;
            }
        }
        //private List<System> _systems;

        public void Update(SpriteBatch spriteBatch, GameTime time)
        {
            _systemManager.UpdateSystems(spriteBatch, time);
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }
    }
}
