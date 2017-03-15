using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameGameEngine
{
    public abstract class System
    {
        protected static List<string> _types;
        protected List<Entity> _entities = new List<Entity>();   

        public List<Entity> Entities
        {
            get
            {
                return _entities;
            }
        }

        public List<string> Types
        {
            get
            {
                return _types;
            }
        }

        public void AddEntity(Entity entity)
        {
            List<string> types = new List<string>(_types);

            foreach (Component comp in entity.Components)
            {
                if (types.Contains(comp.Name)) types.Remove(comp.Name);
            }
            if (types.Count == 0 && !_entities.Contains(entity)) _entities.Add(entity);
        }

        public void RemoveEntity(Component component)
        {
            Entity parent = component.Parent;

            if(_entities.Contains(parent) && _types.Contains(component.Name))
            {
                _entities.Remove(parent);
            }
        }

        public abstract void Update(SpriteBatch spriteBatch, GameTime gameTime);
    }
    public class SystemManager
    {
        private List<System> _systems;
        public SystemManager()
        {
            _systems = new List<System>();
        }
        public void LoadSystems()
        {
            _systems.Add(new Texture2DSystem());
        }
        
        public void UpdateSystems(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(System system in _systems)
            {
                system.Update(spriteBatch, gameTime);
            }
        }

        public void RegisterComponent(Entity entity)
        {
            foreach(System system in _systems)
            {
                system.AddEntity(entity);
            }
        }

        public void RemoveComponent(Component component)
        {
            foreach (System system in _systems)
            {
                system.RemoveEntity(component);
            }
        }
    }

    public class Texture2DSystem : System
    {
        public Texture2DSystem()
        {
            _types = new List<string>() { "Texture2", "Location2" };
        }
        public override void Update(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(Entity entity in _entities)
            {
                Location2 location = entity.GetComponent<Location2>();
                Texture2 texture = entity.GetComponent<Texture2>();

                float x = location.Position.X - texture.Dimensions.X / 2;
                float y = location.Position.Y - texture.Dimensions.Y / 2;
                Rectangle destRect = new Rectangle((int) x, (int) y, (int) texture.Dimensions.X, (int) texture.Dimensions.Y);

                if (texture.Texture != null)
                {
                    spriteBatch.Draw(texture.Texture, destRect, Color.White);
                }
                //Debug.WriteLine(location.Name);
            }   
        }
    }

}
