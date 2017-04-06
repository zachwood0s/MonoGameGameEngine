using Components;
using Microsoft.Xna.Framework.Content;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameGameEngine
{
    class EntityFactory
    {
        public static Entity LoadEntityFromTable(Table table, Scene scene, ContentManager Content)
        {
            string id = table["id"] as string;

            if (id != null)
            {
                Entity newEntity = new Entity(id, scene);

                foreach(DynValue key in table.Keys)
                {
                    if(key.String != "id")
                    {
                        Type compType = Type.GetType("Components."+key.String);
                        if (compType != null)
                        {
                            Component newComp = Activator.CreateInstance(compType, newEntity) as Component;
                            if (!newComp.Load((Table)table[key], Content)) return null;
                            Debug.WriteLine(newComp);
                            newEntity.AddComponent(newComp);
                        }
                        else
                        {
                            //Try other stuff i guess?
                        }
                    }
                }
                return newEntity;
            }
            else
            {
                MessageBox.Show("All entities require an ID");
            }
            return null;
        }
    }
}
