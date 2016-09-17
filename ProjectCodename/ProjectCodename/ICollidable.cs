using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCodename
{
    interface ICollidable
    {
        bool isColliding(GameObject obj);
        Rectangle collisionRect { get; set; }
    }
}
