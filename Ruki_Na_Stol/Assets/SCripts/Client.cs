using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
    class Client : MonoBehaviour
    {
    [SerializeField] private InputField _input_field;
    [SerializeField] private Text _text;
        // адрес и порт сервера, к которому будем подключаться
    static int port = 8080; // порт сервера
    static string address = "127.0.0.1"; // адрес сервера

    private Socket socket=null;
    private IPEndPoint ipPoint;
    private byte[] data;
    private void Start()
    {
        ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // подключаемся к удаленному хосту
        socket.Connect(ipPoint);
    }
    private void Update()
    {
        Debug.Log("Let's go!");
        // получаем ответ
        data = new byte[1024]; // буфер для ответа

        StringBuilder builder = new StringBuilder();
        int bytes = 0; // количество полученных байт

        //StartCoroutine(Connect_To_Server());
        do
        {
            bytes = socket.Receive(data, data.Length, 0);
            builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
        }
        while (socket.Available > 0);
        Debug.Log(builder.ToString());
        _text.text = "ответ сервера: " + builder.ToString();
        data = Encoding.UTF8.GetBytes("OK");
        socket.Send(data);
        Debug.Log("Sended");
        // закрываем сокет
        //socket.Shutdown(SocketShutdown.Both);
        //socket.Close();
    }
    private IEnumerator Connect_To_Server()
    {
        Debug.Log("Let's go!");
        if (socket == null)
        {
            Debug.Log("Created");
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);
            yield return new WaitForEndOfFrame();
        }
    }
}
