using Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameGameEngine
{
    public class Entity
    {
        private Scene _parent;
        private string _id;
        private List<Component> _components;
        private List<Entity> _children;

        public Entity(string id, Scene parent)
        {
            _id = id;
            _components = new List<Component>();
            _parent = parent;
        }

        public string Id
        {
            get
            {
                return _id;
            }
        }
        public List<Component> Components
        {
            get
            {
                return _components;
            }
        }

        public Component GetComponent(string name)
        {
            foreach(Component comp in _components)
            {
                if (comp.Name == name) return comp;
            }

            return null;
        }

        public T GetComponent<T>()
        {
            foreach(Component comp in _components)
            {
                if (comp.GetType() == typeof(T))
                    return (T) comp;
            }
            return default(T);
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);

            _parent.SystemManager.RegisterComponent(this);
        }
        public void RemoveComponent(Component component)
        {
            _parent.SystemManager.RemoveComponent(component);
            _components.Remove(component);
        }

        public void AddComponent(string name)
        {
            object obj = Activator.CreateInstance(Type.GetType("Components."+name), this);
            Component comp = (Component)obj;
            _components.Add(comp);

            _parent.SystemManager.RegisterComponent(this);
        }
        public void RemoveComponent(string name)
        {
            Component remove = null;
            foreach(Component component in _components)
            {
                if (component.Name == name) remove = component;
            }
            if (remove != null)
            {
                _components.Remove(remove);
                _parent.SystemManager.RemoveComponent(remove);
            }
        }
    }
}
