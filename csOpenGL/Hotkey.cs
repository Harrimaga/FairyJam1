using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class Hotkey
    {

        private bool repeat;
        private List<Key> keys;

        public Hotkey(bool repeat)
        {
            this.repeat = repeat;
            keys = new List<Key>();
        }

        public Hotkey AddKey(Key k)
        {
            keys.Add(k);
            return this;
        }

        public bool IsDown()
        {
            foreach(Key k in keys)
            {
                if(repeat)
                {
                    if(now.IsKeyDown(k))
                    {
                        return true;
                    }
                }
                else
                {
                    if(now.IsKeyDown(k) && !prev.IsKeyDown(k))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void Type()
        {
            if (now.IsKeyDown(Key.BackSpace) && !prev.IsKeyDown(Key.BackSpace))
            {
                Globals.typing.Text = Globals.typing.Text.Length > 0 ? Globals.typing.Text.Substring(0, Globals.typing.Text.Length - 1) : Globals.typing.Text;
            }
            if (Globals.typing.Text.Length < 10)
            {
                if(!Globals.num) 
                {
                    for (int i = (int)Key.A; i <= (int)Key.Z; i++)
                    {
                        if (now.IsKeyDown((Key)i) && !prev.IsKeyDown((Key)i))
                        {
                            int offset = 0;
                            if (now.IsKeyDown(Key.ShiftLeft) || now.IsKeyDown(Key.ShiftRight))
                            {
                                offset = -32;
                            }
                            Globals.typing.Text += (char)(i + 14 + offset);
                        }
                    }
                }
                for (int i = (int)Key.Number0; i <= (int)Key.Number9; i++)
                {
                    if (now.IsKeyDown((Key)i) && !prev.IsKeyDown((Key)i))
                    {
                        Globals.typing.Text += i - 109;
                    }
                }
                for (int i = (int)Key.Keypad0; i <= (int)Key.Keypad9; i++)
                {
                    if (now.IsKeyDown((Key)i) && !prev.IsKeyDown((Key)i))
                    {
                        Globals.typing.Text += i - 67;
                    }
                }
            }
        }

        //Static update shit
        private static KeyboardState prev, now;

        public static void Update(KeyboardState kstate)
        {
            prev = now;
            now = kstate;
        } 

    }
}
