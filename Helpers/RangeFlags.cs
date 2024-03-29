using System;

namespace Vardirsoft.Shared.Helpers
{
    [Flags]
    public enum RangeFlags
    {
        None           = 0,
        LeftInclusive  = 1,
        RightInclusive = 2,
        BothInclusive  = LeftInclusive | RightInclusive
    }
}