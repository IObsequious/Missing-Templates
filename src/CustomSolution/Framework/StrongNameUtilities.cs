// -----------------------------------------------------------------------
// <copyright file="StrongNameUtilities.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.VisualStudio.Framework
{
    public static class StrongNameUtilities
    {
        [ThreadStatic]
        private static int _ts_LastStrongNameHR;
        [SecurityCritical]
        [ThreadStatic]
        private static IClrStrongName _s_StrongName;
        private static IClrStrongName StrongName
        {
            [SecurityCritical]
            get
            {
                if (_s_StrongName == null)
                {
                    _s_StrongName = (IClrStrongName)RuntimeEnvironment.GetRuntimeInterfaceAsObject(
                        new Guid("B79B0ACD-F5CD-409b-B5A5-A16244610B92"),
                        new Guid("9FD93CCF-3280-4391-B3A9-96E1CDE77C8D"));
                }
                return _s_StrongName;
            }
        }

        private static IClrStrongNameUsingIntPtr StrongNameUsingIntPtr
        {
            [SecurityCritical]
            get { return (IClrStrongNameUsingIntPtr)StrongName; }
        }

        [SecurityCritical]
        public static StrongNameKeyInfo GenerateStrongNameKeyInfo()
        {
            IntPtr ppbKeyBlob = IntPtr.Zero;
            int pcbKeyBlob = 0;
            StrongNameKeyGen(null, 0, out ppbKeyBlob, out pcbKeyBlob);
            byte[] strongNameKey = new byte[pcbKeyBlob];
            Marshal.Copy(ppbKeyBlob, strongNameKey, 0, pcbKeyBlob);
            IntPtr ppbPublicKeyBlob = IntPtr.Zero;
            int pcbPublicKeyBlob = 0;
            StrongNameGetPublicKey(null, ppbKeyBlob, pcbKeyBlob, out ppbPublicKeyBlob, out pcbPublicKeyBlob);
            byte[] publicKey = new byte[pcbPublicKeyBlob];
            Marshal.Copy(ppbPublicKeyBlob, publicKey, 0, pcbPublicKeyBlob);
            IntPtr ppbStrongNameToken = IntPtr.Zero;
            int pcbStrongNameToken = 0;
            StrongNameTokenFromPublicKey(ppbPublicKeyBlob, pcbPublicKeyBlob, out ppbStrongNameToken, out pcbStrongNameToken);
            byte[] publicKeyToken = new byte[pcbStrongNameToken];
            Marshal.Copy(ppbStrongNameToken, publicKeyToken, 0, pcbStrongNameToken);
            return StrongNameKeyInfo.Create(strongNameKey, publicKey, publicKeyToken);
        }



        [SecurityCritical]
        public static int StrongNameErrorInfo()
        {
            return _ts_LastStrongNameHR;
        }

        [SecurityCritical]
        public static void StrongNameFreeBuffer(IntPtr pbMemory)
        {
            StrongNameUsingIntPtr.StrongNameFreeBuffer(pbMemory);
        }

        [SecurityCritical]
        public static bool StrongNameGetPublicKey(string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, out IntPtr ppbPublicKeyBlob, out int pcbPublicKeyBlob)
        {
            int hr = StrongNameUsingIntPtr.StrongNameGetPublicKey(pwzKeyContainer, pbKeyBlob, cbKeyBlob, out ppbPublicKeyBlob, out pcbPublicKeyBlob);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                ppbPublicKeyBlob = IntPtr.Zero;
                pcbPublicKeyBlob = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameKeyDelete(string pwzKeyContainer)
        {
            int hr = StrongName.StrongNameKeyDelete(pwzKeyContainer);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameKeyGen(string pwzKeyContainer, int dwFlags, out IntPtr ppbKeyBlob, out int pcbKeyBlob)
        {
            int hr = StrongName.StrongNameKeyGen(pwzKeyContainer, dwFlags, out ppbKeyBlob, out pcbKeyBlob);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                ppbKeyBlob = IntPtr.Zero;
                pcbKeyBlob = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameKeyInstall(string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob)
        {
            int hr = StrongNameUsingIntPtr.StrongNameKeyInstall(pwzKeyContainer, pbKeyBlob, cbKeyBlob);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob)
        {
            IntPtr ppbSignatureBlob = IntPtr.Zero;
            int cbSignatureBlob = 0;
            return StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, pbKeyBlob, cbKeyBlob, ref ppbSignatureBlob, out cbSignatureBlob);
        }

        [SecurityCritical]
        public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, ref IntPtr ppbSignatureBlob, out int pcbSignatureBlob)
        {
            int hr = StrongNameUsingIntPtr.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, pbKeyBlob, cbKeyBlob, ppbSignatureBlob, out pcbSignatureBlob);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                pcbSignatureBlob = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameSignatureSize(IntPtr pbPublicKeyBlob, int cbPublicKeyBlob, out int pcbSize)
        {
            int hr = StrongNameUsingIntPtr.StrongNameSignatureSize(pbPublicKeyBlob, cbPublicKeyBlob, out pcbSize);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                pcbSize = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameSignatureVerification(string pwzFilePath, int dwInFlags, out int pdwOutFlags)
        {
            int hr = StrongName.StrongNameSignatureVerification(pwzFilePath, dwInFlags, out pdwOutFlags);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                pdwOutFlags = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameSignatureVerificationEx(string pwzFilePath, bool fForceVerification, out bool pfWasVerified)
        {
            int hr = StrongName.StrongNameSignatureVerificationEx(pwzFilePath, fForceVerification, out pfWasVerified);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                pfWasVerified = false;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameTokenFromPublicKey(IntPtr pbPublicKeyBlob, int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken)
        {
            int hr = StrongNameUsingIntPtr.StrongNameTokenFromPublicKey(pbPublicKeyBlob, cbPublicKeyBlob, out ppbStrongNameToken, out pcbStrongNameToken);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                ppbStrongNameToken = IntPtr.Zero;
                pcbStrongNameToken = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameSignatureSize(byte[] bPublicKeyBlob, int cbPublicKeyBlob, out int pcbSize)
        {
            int hr = StrongName.StrongNameSignatureSize(bPublicKeyBlob, cbPublicKeyBlob, out pcbSize);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                pcbSize = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameTokenFromPublicKey(byte[] bPublicKeyBlob, int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken)
        {
            int hr = StrongName.StrongNameTokenFromPublicKey(bPublicKeyBlob, cbPublicKeyBlob, out ppbStrongNameToken, out pcbStrongNameToken);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                ppbStrongNameToken = IntPtr.Zero;
                pcbStrongNameToken = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameGetPublicKey(string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob, out IntPtr ppbPublicKeyBlob, out int pcbPublicKeyBlob)
        {
            int hr = StrongName.StrongNameGetPublicKey(pwzKeyContainer, bKeyBlob, cbKeyBlob, out ppbPublicKeyBlob, out pcbPublicKeyBlob);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                ppbPublicKeyBlob = IntPtr.Zero;
                pcbPublicKeyBlob = 0;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameKeyInstall(string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob)
        {
            int hr = StrongName.StrongNameKeyInstall(pwzKeyContainer, bKeyBlob, cbKeyBlob);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                return false;
            }
            return true;
        }

        [SecurityCritical]
        public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob)
        {
            IntPtr ppbSignatureBlob = IntPtr.Zero;
            int cbSignatureBlob = 0;
            return StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, bKeyBlob, cbKeyBlob, ref ppbSignatureBlob, out cbSignatureBlob);
        }

        [SecurityCritical]
        public static bool StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, byte[] bKeyBlob, int cbKeyBlob, ref IntPtr ppbSignatureBlob, out int pcbSignatureBlob)
        {
            int hr = StrongName.StrongNameSignatureGeneration(pwzFilePath, pwzKeyContainer, bKeyBlob, cbKeyBlob, ppbSignatureBlob, out pcbSignatureBlob);
            if (hr < 0)
            {
                _ts_LastStrongNameHR = hr;
                pcbSignatureBlob = 0;
                return false;
            }
            return true;
        }
    }
}
