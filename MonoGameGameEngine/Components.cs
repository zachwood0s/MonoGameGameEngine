using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGameEngine;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Components
{
    public interface Component
    {
        Entity Parent
        {
            get;
        }
        string Name
        {
            get;
        }

        bool Load(Table table);
    }

    public class Location2 : Component
    {
        private static string _name = "Location2";
        private Entity _parent;

        private Vector2 _position;
        private double _rotation;

        public Location2(Entity parent)
        {
            _parent = parent;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public double Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
        }

        public Entity Parent
        {
            get
            {
                return _parent;
            }
        }

        public bool Load(Table table)
        {
            throw new NotImplementedException();
        }
    }

    public class Texture2 : Component
    {

        private static string _name = "Texture2";
        private Entity _parent;

        private Vector2 _dimensions;
        private Texture2D _texture;

        public Texture2(Entity parent)
        {
            _parent = parent;
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Entity Parent
        {
            get
            {
                return _parent;
            }
        }

        public Vector2 Dimensions
        {
            get
            {
                return _dimensions;
            }
            set
            {
                _dimensions = value; 
            }
        }
        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }

        public bool Load(Table table)
        {
            throw new NotImplementedException();
        }
    }

    public class Scripts : Component
    {
        private static string _name;
        private Entity _parent;

        private Dictionary<string, DynValue> _scripts;
        private Dictionary<string, string> _scriptTexts;

        private Script _script;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Entity Parent
        {
            get
            {
                return _parent;
            }
        }

        public bool Load(Table table)
        {
            _scripts = new Dictionary<string, DynValue>();
            _scriptTexts = new Dictionary<string, string>();
            _script = new Script();
            throw new NotImplementedException();
            //JObject obj;
            //obj[]
        }

        public Dictionary<string, string> ScriptText
        {
            get
            {
                return _scriptTexts;
            }
        }
        public void RunScript(string id)
        {
            if (_scripts[id] != null)
            {
                Game1.lua.Call(_scripts[id]);
            }
        }

        /*"Character" : {
            "CollisionBox":{

            }
            "Scripts":{
                
            }
        }*/
    }
}
