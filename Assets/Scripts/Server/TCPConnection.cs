using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;

[Serializable]
public class PacketLOGIN_REQUEST
{

    public PacketHeader header;
    public PACKETLoginRequest payload;

   
}
[Serializable]

public class TCPConnection : MonoBehaviour
{
    string RichardsIP = "188.40.137.215";
    private IPAddress ipAddress = IPAddress.Parse("188.40.137.215");
    bool datasent = false;

    private int port = 23323;
    private bool useNAT = false;
    private string yourIP = "";
    private string yourPort = "";
    private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private byte[] _recieveBuffer = new byte[8142];
    String responseData = String.Empty;
    // Use this for initialization
    void Start()
    {
        try
        {/*
            IPAddress ipAddress = IPAddress.Parse(RichardsIP);

            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
            Debug.Log("ipAddress : " + ipAddress);
            Debug.Log("remoteEP : " + remoteEP);
            
            string output = JsonConvert.SerializeObject(LoginData)+ "/r/n";
            Debug.Log(output);
            // Create a TCP/IP socket.
            // Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TcpClient client = new TcpClient(RichardsIP, port);
            

            //client.Connect(ipAddress, port);
            Debug.Log("1) Is connected? " + client.Connected);

           
            Byte[] data = SerializeToByteArray(output);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent: {0}");  
            String responseData = String.Empty;
            data = new Byte[256];
            //Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.Log(responseData);
            Console.WriteLine("Received: {0}", responseData);    
            */
           
            
           // SendData(data);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Login();
            PacketDataRequest DataRequest = new PacketDataRequest();
            DataRequest.header = new PacketHeader("DATA_REQUEST");
            DataRequest.payload = new PACKETDataRequest("Pokemon");
            datasent = true;
            string output = JsonConvert.SerializeObject(DataRequest);
            _clientSocket.Send(System.Text.Encoding.ASCII.GetBytes(output + " \r"));
            sw.Stop();
           Debug.Log("TimeTaken to login = " + sw.Elapsed);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
    
   private void  Login()
    {
        PacketLOGIN_REQUEST LoginData = new PacketLOGIN_REQUEST();
        LoginData.header = new PacketHeader("Login");
        LoginData.payload = new PACKETLoginRequest("Test", "Test", "");
        SetupServer();
        string output = JsonConvert.SerializeObject(LoginData);
        Debug.Log(output);
        Byte[] data = SerializeToByteArray(output + " \r");
        _clientSocket.Send(System.Text.Encoding.ASCII.GetBytes(output + " \r"));
    }
    private void SetupServer()
    {
        try
        {
            _clientSocket.Connect(new IPEndPoint(ipAddress, port));
            Debug.Log("1) Is connected? " + _clientSocket.Connected);
        }
        catch (SocketException ex)
        {
            Debug.Log(ex.Message);
        }

        _clientSocket.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);

    }
    private void ReceiveCallback(IAsyncResult AR)
    {
        //Check how much bytes are recieved and call EndRecieve to finalize handshake
        int recieved = _clientSocket.EndReceive(AR);

        if (recieved <= 0)
            return;

        //Copy the recieved data into new buffer , to avoid null bytes
        byte[] recData = new byte[recieved];
        Buffer.BlockCopy(_recieveBuffer, 0, recData, 0, recieved);

        //Process data here the way you want , all your bytes will be stored in recData

        //Start receiving again
        _clientSocket.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
    }
    private void SendData(byte[] data)
    {
        SocketAsyncEventArgs socketAsyncData = new SocketAsyncEventArgs();
        socketAsyncData.SetBuffer(data, 0, data.Length);
        _clientSocket.SendAsync(socketAsyncData);
    }
    private static byte[] SerializeToByteArray(object graph)
    {
        // Create a memory stream, and serialize.
        using (MemoryStream stream = new MemoryStream())
        {
            // Create a binary formatter.
            IFormatter formatter = new BinaryFormatter();

            // Serialize.
            formatter.Serialize(stream, graph);

            // Now return the array.
            return stream.ToArray();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (datasent)
        {
            responseData = System.Text.Encoding.ASCII.GetString(_recieveBuffer);
            Debug.Log(responseData);

        }
    }



}