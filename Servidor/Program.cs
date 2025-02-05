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
    private List<string> clientNames = new List<string>(); // Lista para guardar los nombres
    private bool martxan = true;
    private const int MaxClients = 2; // Límite de conexiones

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
                TcpClient client = server.AcceptTcpClient(); // Aceptar conexión

                // Crear hilo para manejar la comunicación
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
            // Leer el nombre del cliente
            string clientName = sr.ReadLine();

            // Verificar si el nombre ya está en uso
            if (clientNames.Contains(clientName))
            {
                sw.WriteLine("Error: El nombre ya está en uso.");
                client.Close();
                Console.WriteLine($"Conexión rechazada. El nombre '{clientName}' ya está en uso.");
                return;
            }

            // Si la lista de clientes está llena, rechazar la conexión
            if (clients.Count >= MaxClients)
            {
                sw.WriteLine("Error: Máximo de conexiones alcanzado. Inténtelo más tarde.");
                client.Close();
                Console.WriteLine($"Conexión rechazada. Límite de {MaxClients} clientes alcanzado.");
                return;
            }

            // Si no está en uso y hay espacio, agregarlo a la lista
            clients.Add(client);
            clientNames.Add(clientName);
            Console.WriteLine($"Cliente {clientName} se ha unido. Conexiones actuales: {clients.Count}");

            // Enviar un mensaje de bienvenida
            sw.WriteLine("Conectado");

            string data;
            while ((data = sr.ReadLine()) != null)
            {
                Console.WriteLine("Bezeroa: " + data);
                Bidali($"{clientName} esan du: {data}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea bezeroarekin: {0}", e.Message);
        }
        finally
        {
            // Desconectar cliente y eliminar su nombre
            clients.Remove(client);
            clientNames.Remove(clientNames.Find(name => name == clientNames.Find(c => c == name)));
            client.Close();
            Console.WriteLine($"Cliente desconectado. Conexiones actuales: {clients.Count}");
        }
    }

    private void Bidali(string mensaje)
    {
        if (string.IsNullOrEmpty(mensaje)) return;

        foreach (var client in clients)
        {
            try
            {
                StreamWriter sw = new StreamWriter(client.GetStream()) { AutoFlush = true };
                sw.WriteLine(mensaje);
            }
            catch
            {
                Console.WriteLine("Errorea mezua bidaltzean bezero bati.");
            }
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
