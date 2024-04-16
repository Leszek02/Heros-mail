using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ClientApplication.emailClient
{
    class ServerHandler
    {
        
        [StructLayout(LayoutKind.Sequential)]
        public struct sockaddr_in
        {
            public short sin_family;
            public ushort sin_port;
            public in_addr sin_addr;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sin_zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct in_addr
        {
            public uint s_addr;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Email
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string from;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string to;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string title;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string message;
        }

        [DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int WSAStartup(ushort wVersionRequested, ref WSAData wsaData);

       
        [DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int WSACleanup();


        [StructLayout(LayoutKind.Sequential)]
        public struct WSAData
        {
            public ushort wVersion;
            public ushort wHighVersion;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
            public string szDescription;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
            public string szSystemStatus;

            public ushort iMaxSockets;
            public ushort iMaxUdpDg;
            public IntPtr lpVendorInfo;
        }

        public class EmailClient
        {
            public IntPtr fd;
            public WSAData wd;
            public sockaddr_in sa;


            [DllImport("clientServer.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int Connect(ref IntPtr fd, ref sockaddr_in sa, String ipAddress, String username);

            [DllImport("clientServer.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void sendMessage(String title, String from, String content, String to, IntPtr fd);

            [DllImport("clientServer.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr getSendTitles(IntPtr fd);

            [DllImport("clientServer.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr getReceivedTitles(IntPtr fd);

            [DllImport("clientServer.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr disconnect(IntPtr fd);

            [DllImport("clientServer.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr getSenderEmailContent(IntPtr fd, ref Email email, String title);

            [DllImport("clientServer.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr getReceiverEmailContent(IntPtr fd, ref Email email, String title);


            public EmailClient()
            {
                WSAStartup(0x202, ref wd);
                sockaddr_in sa = new sockaddr_in();
            }


            ~EmailClient()
            {
                disconnect(fd);
                WSACleanup();
            }

            public int ConnectToServer(string ipAddress, string username)
            {
                return Connect(ref fd, ref sa, ipAddress, username);
            }

            public void SendMail(string title, string from, string content, string to)
            {
                sendMessage(title, from, content, to, fd);
            }

            public string ReceiveSendMail()
            {
                IntPtr resultPtr = getSendTitles(fd);
                string result = Marshal.PtrToStringAnsi(resultPtr);
                Marshal.FreeHGlobal(resultPtr);
                Debug.WriteLine("Received titles and freed resources");
                Debug.WriteLine(result);
                return result;
            }

            public string ReceiveReceivedMail()
            {
                IntPtr resultPtr = getReceivedTitles(fd);
                string result = Marshal.PtrToStringAnsi(resultPtr);
                Marshal.FreeHGlobal(resultPtr);
                Debug.WriteLine("Received titles and freed resources");
                Debug.WriteLine(result);
                return result;
            }

            public List<string> GetSenderMail(string title)
            {
                Email email = new Email();
                getSenderEmailContent(fd, ref email, title);
                List<string> result = new List<string>
                {
                    email.from,
                    email.to,
                    email.title,
                    email.message
                };
                Debug.WriteLine("Received email content");
                Debug.WriteLine(email.title);
                return result;
            }

            public List<string> GetReceiverMail(string title)
            {
                Email email = new Email();
                getReceiverEmailContent(fd, ref email, title);
                List<string> result = new List<string>
                {
                    email.from,
                    email.to,
                    email.title,
                    email.message
                };
                Debug.WriteLine("Received email content");
                Debug.WriteLine(email.title);
                return result;
            }
        }
    }
}
