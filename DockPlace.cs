using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class DockPlace
    {
        public Boat[] dockPlace { get; set; } = new Boat[2];

        public int InnerBertNumber { get; set; }
        public bool Available = true;
    }
}
