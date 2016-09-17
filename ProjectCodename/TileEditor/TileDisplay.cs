using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEditor
{
    class TileDisplay : GraphicsDeviceControl
    {
        //Variables
        public event EventHandler OnInitialize;
        public event EventHandler OnDraw;


        //
        protected override void Initialize()
        {
            if (OnInitialize != null)
                OnInitialize(this, null);
        }



        protected override void Draw()
        {
            if (OnDraw != null)
                OnDraw(this, null);
        }
    }
}
