using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class CircleButton
    {
        public delegate void EventAction();

        public float X { get; set; }
        public float Y { get; set; }
        private float Radius { get; set; }
        private EventAction OnClickAction { get; set; }

        public CircleButton(float x, float y, float radius, EventAction onClickAction)
        {
            X = x;
            Y = y;
            Radius = radius;
            OnClickAction = onClickAction;
        }

        public void Update(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public bool IsInButton(float x, float y)
        {
            return Math.Sqrt((x-X)*(x-X) + (y-Y)*(y-Y)) < Radius;
        }

        public void OnClick()
        {
            OnClickAction();
        }
    }
}
