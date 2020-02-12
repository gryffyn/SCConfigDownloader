using System;

namespace sccdownloader
{
    public class SCCException : Exception
    {
        public SCCException(string message)
        {
        }
    }

    [Serializable]
    public class ConnectionException : Exception
    {
        public ConnectionException(string message)
        {

        }
    }
}