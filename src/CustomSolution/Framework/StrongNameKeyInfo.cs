namespace Microsoft.VisualStudio.Framework
{
    public struct StrongNameKeyInfo
    {
        private readonly byte[] _rawBytes;
        private readonly byte[] _rawPublicKey;
        private readonly byte[] _rawPublicKeyToken;

        public static StrongNameKeyInfo Create(byte[] rawBytes, byte[] publicKey, byte[] publicKeyToken)
        {
            return new StrongNameKeyInfo(rawBytes, publicKey, publicKeyToken);
        }

        private StrongNameKeyInfo(byte[] rawBytes, byte[] rawPublicKey, byte[] publicKeyToken) : this()
        {
            _rawBytes = rawBytes;
            _rawPublicKey = rawPublicKey;
            _rawPublicKeyToken = publicKeyToken;
        }

        public string PublicKey
        {
            get
            {
                return HexUtilities.EncodeHexString(_rawPublicKey);
            }
        }

        public string PublicKeyToken
        {
            get
            {
                return HexUtilities.EncodeHexString(_rawPublicKeyToken).ToLower();
            }
        }

        public byte[] RawBytes
        {
            get
            {
                return _rawBytes;
            }
        }
    }
}
