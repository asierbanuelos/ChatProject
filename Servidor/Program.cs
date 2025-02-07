using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

class MyTcpListener
{
    private TcpListener server;
    private List<TcpClient> clients = new List<TcpClient>();
    private List<string> clientNames = new List<string>();
    private bool martxan = true;
    private const int MaxClients = 2;

    public MyTcpListener(IPAddress ip, int port)
    {
        server = new TcpListener(ip, port);
    }

    public void Entzun()
    {
        try
        {
            server.Start();
            Console.WriteLine("Bezeroen konexioa itxaroten...");

            while (martxan)
            {
                TcpClient client = server.AcceptTcpClient();
                Thread clientThread = new Thread(() => Komunikazioa(client));
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea zerbitzarian: {0}", e.Message);
        }
    }

    private void Komunikazioa(TcpClient client)
    {
        NetworkStream str = client.GetStream();
        StreamReader sr = new StreamReader(str);
        StreamWriter sw = new StreamWriter(str) { AutoFlush = true };

        try
        {
            string clientName = sr.ReadLine();
            if (clientNames.Contains(clientName))
            {
                sw.WriteLine("Error: El nombre ya está en uso.");
                client.Close();
                return;
            }
            if (clients.Count >= MaxClients)
            {
                sw.WriteLine("Error: Máximo de conexiones alcanzado. Inténtelo más tarde.");
                client.Close();
                return;
            }

            clients.Add(client);
            clientNames.Add(clientName);
            Console.WriteLine($"Cliente {clientName} se ha unido. Conexiones actuales: {clients.Count}");
            sw.WriteLine($"Conectado-{clientName}");
            EnviarListaDeUsuarios();

            string data;
            while ((data = sr.ReadLine()) != null)
            {
                if (data.StartsWith("DISC:"))
                {
                    string disconnectedUser = data.Split(':')[1];
                    Console.WriteLine($"Cliente {disconnectedUser} se desconectó.");
                    break;
                }
                else if (data.StartsWith("MSG:"))
                {
                    Console.WriteLine(data);
                    EnviarMensajeATodos(data);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea bezeroarekin: {0}", e.Message);
        }
        finally
        {
            lock (clients)
            {
                int index = clientNames.IndexOf(clientNames.Find(name => name == clientNames.Find(n => n == clientNames[clients.IndexOf(client)])));
                if (index != -1)
                {
                    clients.RemoveAt(index);
                    clientNames.RemoveAt(index);
                    EnviarListaDeUsuarios();
                }
            }
            client.Close();
            Console.WriteLine($"Conexiones actuales: {clients.Count}");
        }
    }

    public void EnviarListaDeUsuarios()
    {
        string usuarios = "USUARIOS_ACTIVOS:" + string.Join(",", clientNames);
        foreach (var cliente in clients)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                writer.WriteLine(usuarios);
            }
            catch { }
        }
    }

    public void EnviarMensajeATodos(string mensaje)
    {
        foreach (var cliente in clients)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                writer.WriteLine(mensaje);
            }
            catch { }
        }
    }

    public void Itxi()
    {
        martxan = false;
        foreach (var client in clients)
        {
            client.Close();
        }
        server.Stop();
        Console.WriteLine("Zerbitzaria itxi da.");
    }

    public static void Main(string[] args)
    {
        int port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        MyTcpListener server = new MyTcpListener(localAddr, port);
        server.Entzun();
        server.Itxi();
        Console.WriteLine("\nSakatu <ENTER> irteteko...");
        Console.Read();
    }
}
