using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yarhl.IO;

namespace Hato.Steam
{
    public class PackageInfo
    {
        public List<int> AppID = new List<int>();
        public DataReader _reader;
        public PackageInfo(string path)
        {
            if (File.Exists(path))
                _reader = new DataReader(DataStreamFactory.FromFile(path, FileOpenMode.Read));
            else
                throw new Exception("");

            while (true)
            {
                var p = Search(_reader.Stream, new byte[] { 0x61, 0x70, 0x70, 0x69, 0x64, 0x73, 0x00, 0x02, 0x30, 0x00 });
                if (p != -1)
                    AppID.Add(_reader.ReadInt32());
                else
                {
                    break;
                }
            }
        }

        public bool GameOwned(int appid)
        {
            if (AppID.Contains(appid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static long Search(Stream stream, byte[] pattern)
        {
            long start = -1;

            while (stream.Position < stream.Length)
            {
                if (stream.ReadByte() != pattern[0])
                    continue;

                start = stream.Position - 1;

                for (int idx = 1; idx < pattern.Length; idx++)
                {
                    if (stream.ReadByte() != pattern[idx])
                    {
                        start = -1;
                        break;
                    }
                }

                if (start > -1)
                {
                    return start;
                }
            }

            return start;
        }
    }
}
