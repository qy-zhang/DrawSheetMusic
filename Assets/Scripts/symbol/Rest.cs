using System;

namespace symbol
{
    public class Rest : Symbol
    {
        public override void SetType(string type1)
        {
            int temp = 1;
            if (base.Dot > 0)
            {
                temp = (int) Math.Pow(2, Dot - 1);
                temp = 1 + (temp - 1) / temp;
            }
            switch (base.Duration / temp)
            {
                case 256: base.Type = 1; break;
                case 128: base.Type = 2; break;
                case 64:  base.Type = 4; break;
                case 32:  base.Type = 8; break;
                case 16:  base.Type = 16; break;
                default: break;
            }
        }

        public override int GetRate() { return 1; }
    }
}