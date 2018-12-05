using System;

namespace SocketFileServer.Common
{
    public struct FileProtocol
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
            return String.Format("<protocol><file name=\"{0}\" mode=\"{1}\" port=\"{2}\" /></protocol>", FileName, Mode, Port);
        }
    }
}
