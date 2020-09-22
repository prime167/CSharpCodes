using System;

namespace SocketFileServer.Common
{
    public readonly struct FileProtocol
    {
        public FileProtocol(FileRequestMode mode, int port, string fileName)
        {
            Mode = mode;
            Port = port;
            FileName = fileName;
        }

        public FileRequestMode Mode { get; }

        public int Port { get; }

        public string FileName { get; }

        public override string ToString()
        {
            return $"<protocol><file name=\"{FileName}\" mode=\"{Mode}\" port=\"{Port}\" /></protocol>";
        }
    }
}
