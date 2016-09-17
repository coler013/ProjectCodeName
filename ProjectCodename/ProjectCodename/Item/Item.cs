using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCodename.Item
{
    class Item : GameObject
    {
        public Item()
        {    
        }

        protected override void onCollision()
        {
            onPickup();
        }

        protected virtual void onPickup()
        {
            onDestroy();
        }
    }
}
