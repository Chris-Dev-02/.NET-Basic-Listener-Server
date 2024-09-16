using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = null;

try
{
    var port = 13000;
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

    server = new TcpListener(localAddr, port);
    server.Start();

    string data = null;

    while (true)
    {
        Console.WriteLine("Waiting for requests...");
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected");

        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytes = stream.Read(buffer, 0, buffer.Length);
        string httpRequest = Encoding.UTF8.GetString(buffer, 0, bytes);
        Console.WriteLine("Message received: " + httpRequest);

        string httprequest = "HTTP/1.1 200 OK\r\nContent-Type: text/html; charset=UTF-8\r\n\r\n<html><body><h1>Hello, I'm a server!</h1></body></html>";
        byte[] responseBytes = Encoding.UTF8.GetBytes(httprequest);
        stream.Write(responseBytes, 0, responseBytes.Length);

        client.Close();
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error {0}", ex);
}
finally
{
    server.Stop();
}