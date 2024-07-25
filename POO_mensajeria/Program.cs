// See https://aka.ms/new-console-template for more information
// Console.WriteLine("print1");
// int a = 5;
// int b = 7;
// int c = a + b;
// Console.WriteLine(c);
// Console.WriteLine("print2");
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
// using System.Threading;
// using System.Diagnostics;

namespace app // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            int ownPort = 0;
            if (args.Length >= 2)
            {
                if (args[0].Equals("-port"))
                {
                    try
                    {
                        int port = int.Parse(args[1]);
                        if (port >=1000 && port <=9999)
                        {
                            ownPort = port;
                        }
                    }
                    finally{}

                }
            }
            if (ownPort == 0)
            {
                Console.WriteLine("No se indico un puerto.");
                Environment.Exit(0);
            }
            
            void listen()
            {
                // Console.WriteLine("Entra a listen con port " + ownPort);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(0, ownPort));
                socket.Listen(0);   // .Listen y .Accept hacen que socket espere hasta que llegue un mensaje.

                Socket hasMessage = socket.Accept();
                byte[] message = new byte[255];
                int messageLenght = hasMessage.Receive(message, 0, message.Length, 0);
                Array.Resize(ref message, messageLenght);
                
                string textListened = Encoding.ASCII.GetString(message);
                int textListenedLength = textListened.Length;
                string textSender = textListened.Substring(textListenedLength-4, 4);
                textListened = textListened[..(textListenedLength - 4)];
                if (args.Length >= 1)
                {
                    Console.WriteLine(textSender + ": " + textListened);
                }
                else
                {
                    Console.WriteLine("NO DEBERIA DE ENTRAR AQUI PORQUE SIEMPRE SE DEBE INCIAR EL PROGRAMA CON UN PUERTO.");
                    Console.WriteLine("En listen message es " + textListened + " y args esta vacio.");
                }
                socket.Close();
                hasMessage.Close();
                listen();
            }

            void send() //El mensaje de escriba su mensaje esta antes de iniciar el thread.
            {
                // Console.WriteLine("Entra a send.");
                string senderPort = args[1];
                string? text;
                text = Console.ReadLine();
                
                int textLength = text.Length;
                int port = 0;
                bool invalidMessage = false;
                try
                {
                    port = int.Parse(text.Substring(textLength-4, 4));
                }
                catch
                {
                    invalidMessage = true;
                }
                if (invalidMessage || textLength <= 4)
                {
                    Console.WriteLine(" (Mensaje invalido).");
                    send();
                }
                
                text = text[..(textLength - 4)];
                text = string.Concat(text, senderPort);
                // Console.WriteLine("En send text = " + text + "; y port, " + port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1990));
                byte[] asciiText = Encoding.ASCII.GetBytes(text);
                try
                {
                socket.Connect("127.0.0.1", port);
                }
                catch
                {
                    Console.WriteLine(" (Destinatario invalido).");    
                    send();
                }
                socket.Send(asciiText);
                // socket.Close();
                Console.WriteLine(" (Enviado).");
                socket.Close();
                send();
            }

            {    
                Thread listenerThread = new Thread(new ThreadStart(listen));
                listenerThread.Start();

                Console.WriteLine("Escriba su mensaje.");
                Thread senderThread = new Thread(new ThreadStart(send));
                senderThread.Start();
            }
        }
    }
}