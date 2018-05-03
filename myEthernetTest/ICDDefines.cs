using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace myEthernetTest
{
    namespace ICD
    {
        enum COMMAND
        {
            START,
            STOP,
            PARAM
        }
        enum TYPE
        {
            REQ,
            ACK,
            REP
        }
        enum MAGIC
        {
            SOF = 0xaa,
            EOF = 0xbb
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        class HEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public uint sof;
            [MarshalAs(UnmanagedType.U4)]
            public uint id;
            [MarshalAs(UnmanagedType.U4)]
            public uint size;
            [MarshalAs(UnmanagedType.U4)]
            public uint type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string time;

            static public int HeaderSize()
            {
                return Marshal.SizeOf(typeof(HEADER));
            }
            static public HEADER GetHeaderInfo(byte[] buf)
            {
                HEADER obj = new HEADER();
                int headSize = HeaderSize();
                byte[] headBuf = new byte[headSize];
                Array.Copy(buf, headBuf, headSize);
                Deserialize(obj, ref headBuf);
                return obj;
            }

            static public byte[] Serialize(Object obj)
            {
                // allocate a byte array for the struct data
                var buffer = new byte[Marshal.SizeOf(obj)];

                // Allocate a GCHandle and get the array pointer
                var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                var pBuffer = gch.AddrOfPinnedObject();

                // copy data from struct to array and unpin the gc pointer
                Marshal.StructureToPtr(obj, pBuffer, false);
                gch.Free();

                return buffer;
            }

            static public void Deserialize(Object obj, ref byte[] data)
            {
                var gch = GCHandle.Alloc(data, GCHandleType.Pinned);
                Marshal.PtrToStructure(gch.AddrOfPinnedObject(), obj);
                gch.Free();
            }

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        class ACK : HEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public uint result;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string log;
            [MarshalAs(UnmanagedType.U4)]
            public uint eof;

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        class ACK_TEST_A : HEADER
        {
            [MarshalAs(UnmanagedType.I4)]
            public int val1;
            [MarshalAs(UnmanagedType.U4)]
            public uint val2;
            [MarshalAs(UnmanagedType.R8)]
            public double val3;
            [MarshalAs(UnmanagedType.R4)]
            public float val4;

        }

    }
}
