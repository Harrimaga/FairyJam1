using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class Timer
    {
        protected TimeSpan timer = new TimeSpan();
        public bool ready;
        protected int mSecs;

        public Timer(int m)
        {
            ready = false;
            mSecs = m;
        }

        public Timer(int m, bool ready)
        {
            this.ready = ready;
            mSecs = m;
        }

        public void UpdateTimer()
        {
            timer += TimeSpan.FromSeconds(Globals.DeltaTime * 60);
        }

        public virtual void AddToTimer(int mSecs)
        {
            timer += TimeSpan.FromMilliseconds(mSecs);
        }

        public bool Expired()
        {
            if (timer.TotalMilliseconds >= mSecs || ready)
            {
                ready = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            timer = timer.Subtract(new TimeSpan(0, 0, mSecs / 60000, mSecs / 1000, mSecs % 1000));
            if (timer.TotalMilliseconds < 0)
            {
                timer = TimeSpan.Zero;
            }
            ready = false;
        }

        public void Reset(int newTimer)
        {
            timer = TimeSpan.Zero;
            mSecs = newTimer;
            ready = false;
        }

        public void ResetToZero()
        {
            timer = TimeSpan.Zero;
            ready = false;
        }

        public void SetTimer(TimeSpan time)
        {
            timer = time;
        }

        public virtual void SetTimer(int mSecs)
        {
            timer = TimeSpan.FromMilliseconds((long)mSecs);
        }
    }
}
