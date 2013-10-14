using OpenTK.Input;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SuperBitBros.OpenGL
{
    public class CheatKeySwitch : BooleanKeySwitch
    {
        private int pos = 0;
        private string code;
        private bool targetKeyState = false;
        private bool activated;

        public CheatKeySwitch(bool initial, Key actkey, KeyTriggerMode tmode, string cheat)
            : base(initial, actkey, tmode)
        {
            activated = initial;
            code = cheat.ToLower();
        }

        protected override void Update(KeyboardDevice keyboard)
        {
            if (activated)
            {
                base.Update(keyboard);
                return;
            }

            for (int i = 0; i < code.Length; i++)
            {
                char c = code[i];
                Key k = getKeyForChar(c);
                if (keyboard[k])
                {
                    if (pos < code.Length && code[pos] == c && targetKeyState)
                    {
                        //Console.Out.WriteLine("CHEAT INC");
                        pos++;
                        targetKeyState = false;
                        return;
                    }
                    else if (pos > 0 && code[pos - 1] == c)
                    {
                        //Console.Out.WriteLine("CHEAT STAY");
                        // all OK
                    }
                    else if (targetKeyState)
                    {
                        //Console.Out.WriteLine("CHEAT RESET");
                        pos = 0;
                        return;
                    }
                }
                else
                {
                    if (pos < code.Length && code[pos] == c && !targetKeyState)
                    {
                        targetKeyState = true;
                    }
                }
            }

            if (pos == code.Length)
            {
                Console.Out.WriteLine("CHEAT SWITCH ACTIVATED");
                Turn(true);
                activated = true;
            }
        }

        private Key getKeyForChar(char c)
        {
            switch (c)
            {
                case '1': { return Key.Number1; }
                case '2': { return Key.Number2; }
                case '3': { return Key.Number3; }
                case '4': { return Key.Number4; }
                case '5': { return Key.Number5; }
                case '6': { return Key.Number6; }
                case '7': { return Key.Number7; }
                case '8': { return Key.Number8; }
                case '9': { return Key.Number9; }
                case '0': { return Key.Number0; }
                case 'a': { return Key.A; }
                case 'b': { return Key.B; }
                case 'c': { return Key.C; }
                case 'd': { return Key.D; }
                case 'e': { return Key.E; }
                case 'f': { return Key.F; }
                case 'g': { return Key.G; }
                case 'h': { return Key.H; }
                case 'i': { return Key.I; }
                case 'j': { return Key.J; }
                case 'k': { return Key.K; }
                case 'l': { return Key.L; }
                case 'm': { return Key.M; }
                case 'n': { return Key.N; }
                case 'o': { return Key.O; }
                case 'p': { return Key.P; }
                case 'q': { return Key.Q; }
                case 'r': { return Key.R; }
                case 's': { return Key.S; }
                case 't': { return Key.T; }
                case 'u': { return Key.U; }
                case 'v': { return Key.V; }
                case 'w': { return Key.W; }
                case 'x': { return Key.X; }
                case 'y': { return Key.Y; }
                case 'z': { return Key.Z; }

                case '!': { return Key.Up; }
                case '=': { return Key.Right; }
                case '§': { return Key.Down; }
                case '$': { return Key.Left; }
                case '%': { return Key.Enter; }
                case '&': { return Key.BackSpace; }
                case '/': { return Key.ShiftRight; }
                case '(': { return Key.ScrollLock; }
                case ')': { return Key.Slash; }
                case '?': { return Key.Tab; }
                case '[': { return Key.WinLeft; }
                case ']': { return Key.RAlt; }
                case '{': { return Key.Tilde; }
                case '}': { return Key.Minus; }
                case '*': { return Key.Menu; }
                case '+': { return Key.Home; }
                case '#': { return Key.Insert; }

                default: { return Key.Unknown; }
            }
        }

        public static string Crypt(string text)
        {
            return Convert.ToBase64String(DES.Create().CreateEncryptor(new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 }, new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 }).TransformFinalBlock(Encoding.Unicode.GetBytes(text), 0, Encoding.Unicode.GetBytes(text).Length));
        }

        public static string Decrypt(string text)
        {
            return Encoding.Unicode.GetString(DES.Create().CreateDecryptor(new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 }, new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 }).TransformFinalBlock(Convert.FromBase64String(text), 0, Convert.FromBase64String(text).Length));
        }
    }
}