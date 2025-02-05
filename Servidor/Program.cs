using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

class MyTcpListener
{
    // Zerbitzariaren socket-a
    private TcpListener server;
    // Bezeroen zerrenda gordetzeko
    private List<TcpClient> clients = new List<TcpClient>();
    // Zerbitzaria martxan dagoen ala ez jakiteko
    private bool martxan = true;

    public MyTcpListener(IPAddress ip, int port)
    {
        // TcpListener objektua sortu
        server = new TcpListener(ip, port);
    }

    /**
     * Zerbitzaria martxan jarri eta bezeroen konexioak itxaron
     */
    public void Entzun()
    {
        try
        {
            server.Start();
            Console.WriteLine("Bezeroen konexioa itxaroten...");

            while (martxan)
            {
                // Bezeroa onartu era asinkronoan
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                Console.WriteLine("Bezero berria konektatu da.");

                // Bezeroaren konexioa kudeatzeko haria sortu
                Thread clientThread = new Thread(() => Komunikazioa(client));
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea zerbitzarian: {0}", e.Message);
        }
    }

    /**
     * Bezero bakoitzaren komunikazioa kudeatu
     */
    private void Komunikazioa(TcpClient client)
    {
        NetworkStream str = client.GetStream();
        StreamReader sr = new StreamReader(str);
        StreamWriter sw = new StreamWriter(str) { AutoFlush = true };

        try
        {
            string data;
            while ((data = sr.ReadLine()) != null)
            {
                Console.WriteLine("Bezeroa: " + data);

                // Bezeroari jasotako mezua letra larriz bidali
                sw.WriteLine("Zerbitzaria: " + data.ToUpper());

                // Mezua bezero guztiei bidali
                Bidali($"Bezeroak esan du: {data}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Errorea bezeroarekin: {0}", e.Message);
        }
        finally
        {
            // Bezeroa deskonektatu eta zerrendatik kendu
            client.Close();
            clients.Remove(client);
            Console.WriteLine("Bezeroa deskonektatu da.");
        }
    }

    /**
     * Mezua bezero guztiei bidali
     */
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

    /**
     * Zerbitzaria gelditu eta bezero guztiak deskonektatu
     */
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

    /**
     * Main metodoa, hemen hasten da programa.
     */
    public static int Main(string[] args)
    {
        // Zerbitzariaren portu-zenbakia eta IP helbidea.
        int port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        // Zerbitzariaren objektua sortu
        MyTcpListener server = new MyTcpListener(localAddr, port);
        server.Entzun();
        server.Itxi();

        Console.WriteLine("\nSakatu <ENTER> irteteko...");
        Console.Read();
        return 0;
    }
}
