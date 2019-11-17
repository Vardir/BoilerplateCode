﻿using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Vardirsoft.Shared.Helpers
{
    public static class SecureStringHelpers
    {
        public static bool EqualsTo(this SecureString s1, SecureString s2)
        {
            if (s1 == null)
                throw new ArgumentNullException(nameof(s1));

            if (s2 == null)
                throw new ArgumentNullException(nameof(s2));

            if (s1.Length != s2.Length)
                return false;

            var bstr1 = IntPtr.Zero;
            var bstr2 = IntPtr.Zero;

            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(s1);
                bstr2 = Marshal.SecureStringToBSTR(s2);

                unsafe
                {
                    for (char* ptr1 = (char*)bstr1.ToPointer(), ptr2 = (char*)bstr2.ToPointer(); *ptr1 != 0 && *ptr2 != 0; ++ptr1, ++ptr2)
                    {
                        if (*ptr1 != *ptr2)
                            return false;
                    }
                }
                
                return true;
            }
            finally
            {
                if (bstr1 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstr1);

                if (bstr2 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstr2);
            }
        }

        public static string Unsecure(this SecureString self)
        {
            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(self);

                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString Secure(this string self)
        {
            var result = new SecureString();

            foreach (var c in self)
            {
                result.AppendChar(c);
            }
            
            return result;
        }
    }
}