using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGameEngine;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        bool Load(Table table, ContentManager Content);
    }
    [MoonSharpUserData]
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

        public bool Load(Table table, ContentManager Content)
        {
            double x = 0;
            double y = 0;
            try
            {
                if (table["x"] != null) x = (double)table["x"];
                if (table["y"] != null) y = (double)table["y"];
                if (table["rotation"] != null) _rotation = (double)table["rotation"];
                _position = new Vector2((float)x, (float)y);
                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid format for 'dimensions' on Texture2 \n" + exc.ToString());
                return false;
            }
        }
    }
    [MoonSharpUserData]
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

        public bool Load(Table table, ContentManager Content)
        {
            if (table["dimensions"] == null)
            {
                MessageBox.Show("'dimensions' property is required for Texture2");
                return false;
            }
            if (table["src"] == null)
            {
                MessageBox.Show("'src' property is required for Texture2");
                return false;
            }

            try
            {
                Table dimensions = table["dimensions"] as Table;
                double width = (double)dimensions["width"];
                double height = (double)dimensions["height"];

                _dimensions = new Vector2((float)width, (float)height);

                _texture = Content.Load<Texture2D>("Graphics\\" + table["src"]);


                return true;
            }
            catch(ContentLoadException eCont)
            {
                MessageBox.Show("Problem loading texture from src '"+table["src"]+"' in Texture2 \n" + eCont.ToString());
                return false;
            }
            catch(Exception exc)
            {
                MessageBox.Show("Invalid format for 'dimensions' on Texture2 \n" + exc.ToString());
                return false;
            }
        }
        public override string ToString()
        {
            return "Texture2: Dimensions[" + _dimensions.X + ", " + _dimensions.Y + "]";
        }
    }
    [MoonSharpUserData]
    public class Scripts : Component
    {
        private static string _name = "Scripts";
        private Entity _parent;

        private Dictionary<string, DynValue> _scripts;
       // private Table _properties;

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

        public Dictionary<string, DynValue> ScriptFunctions
        {
            get
            {
                return _scripts;
            }
        }
        public Script Script
        {
            get
            {
                return _script;
            }
        }
        
        public Scripts(Entity parent)
        {
            _parent = parent;
        }
        public bool Load(Table table, ContentManager Content)
        {
            _scripts = new Dictionary<string, DynValue>();
            _script = new Script();
            //_properties = new Table(_script);

            _script.Globals["this"] = _parent;
            _script.Globals["Vector2"] = typeof(Vector2);
            _script.Globals["InputManager"] = typeof(InputManager);

            foreach (DynValue scriptKey in table.Keys)
            {
                Table script = table[scriptKey] as Table;
                string scriptSrc = script["src"] as string;
                if (scriptSrc != null)
                {
                    try
                    {
                        DynValue newScript = _script.LoadFile(Game1.BASE_PATH + scriptSrc);
                        _scripts.Add(scriptKey.String, newScript);
                        // _script.Call(newScript);

                        //Table props = _script.Globals["Properties"] as Table;
                        Table props = new Table(_script);
                        foreach(DynValue prop in script.Keys)
                        {
                            //props[prop]
                            if(script[prop] != null && prop.String != "src")
                            {
                                props[prop] = script[prop];
                            }
                        }
                        _script.Globals["Properties"] = props;
                        //Debug.WriteLine((_script.Globals["Properties"] as Table).Keys + "--------");



                    }
                    catch (IOException exc)
                    {
                        MessageBox.Show("Trouble loading script at location '" + scriptSrc + "'. File not found\n" + exc.ToString());
                        return false;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("Invalid form for script '" + scriptKey.String + "', must be a string pointing to the src of the script");
                    return false;
                }
            }
            return true;
        }
    }
}
