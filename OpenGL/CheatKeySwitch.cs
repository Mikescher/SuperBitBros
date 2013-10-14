using OpenTK.Input;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SuperBitBros.OpenGL
{
    public class CheatKeySwitch : BooleanKeySwitch
    {
        int pos = 0;
        private string code;
        bool activated;

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
                    if (pos < code.Length && code[pos] == c)
                    {
                        pos++;
                        return;
                    }
                    else if (pos > 0 && code[pos - 1] == c)
                    {
                        // all OK
                    }
                    else
                    {
                        pos = 0;
                        return;
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
                default: { return Key.Unknown; }
            }
        }

        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Crypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }
}