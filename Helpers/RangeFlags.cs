using System;

namespace BPCode.Helpers
{
    [Flags]
    public enum RangeFlags
    {
        None,
        LeftInclusive = 1,
        RightInclusive = 2,
        BothInclusive = LeftInclusive | RightInclusive
    }
}