using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectCodename
{
    class GameObject : Sprite, ICollidable
    {
        public Rectangle collisionRect
        {
            get
            {
                return collisionRect;
            }

            set
            {
                collisionRect = value;
            }
        }

        public GameObject()
        {

        }

        public GameObject(string spriteAsset)
        {
            AssetName = spriteAsset;
        }

        public GameObject(string spriteAsset, Vector2 position)
        {
            AssetName = spriteAsset;
            Position = position;
        }

        public bool isColliding(GameObject obj)
        {
            if (collisionRect.Intersects(obj.collisionRect)) return true;
            return false;
        }

        protected virtual void onCollision()
        {

        }

        public void onDestroy()
        {

        }
    }
}
