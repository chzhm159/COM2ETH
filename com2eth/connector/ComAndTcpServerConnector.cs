﻿using com2eth.serialport;
using com2eth.server;
using EthSerialPort.Codec;
using EthSerialPort;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using com2eth.serialport.Codec;

namespace com2eth.connector
{
    /// <summary>
    /// 
    /// </summary>
    internal class ComAndTcpServerConnector
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ComAndTcpServerConnector));
        private TcpServerChannel? tcpServerChannel;
        private NetSerialPort? comChannel;
        EndpointEntity? endpoint_a;
        EndpointEntity? endpoint_b;
        public readonly static EndpointType[] Endpoints = new EndpointType[] { EndpointType.TCP_SERVER, EndpointType.SERIALPORT };

        public ComAndTcpServerConnector() {
            Config();
        }

        private void Config() {
            // TODO 可以从配置文件加载某些配置
        }

        public void SetEndpoint(EndpointEntity epA, EndpointEntity epB) {
            log.InfoFormat("连接端配置信息已设定, A:{0},B:{1}",epA.type, epB.type);
            endpoint_a = epA;
            endpoint_b = epB;
            
        }
        public void Start() {
            log.InfoFormat("转换器启动中...");
            string tcpServer = StringEnum.GetStringValue(EndpointType.TCP_SERVER);
            string serialPort = StringEnum.GetStringValue(EndpointType.SERIALPORT);
            
            bool edpAIsTcpSvr = StringEnum.FromStringValue(endpoint_a.type)== EndpointType.TCP_SERVER;
            bool edpAIsCom = StringEnum.FromStringValue(endpoint_a.type) == EndpointType.SERIALPORT;

            bool edpBIsTcpSvr = StringEnum.FromStringValue(endpoint_b.type) == EndpointType.TCP_SERVER;
            bool edpBIsCom = StringEnum.FromStringValue(endpoint_b.type) == EndpointType.SERIALPORT;

            if (edpAIsTcpSvr && edpBIsCom) {
                bool tcpOk = StartTcpServer(endpoint_a);
                bool comOk = StartCom(endpoint_b);
            } else if(edpAIsCom && edpBIsTcpSvr) {
                bool tcpOk = StartTcpServer(endpoint_b);
                bool comOk = StartCom(endpoint_a);

            } else {
                log.WarnFormat("Endpoint A:{0} 与 Endpoint B:{1} 配置错误!", endpoint_a.type, endpoint_b.type);
            }

        }

        private bool StartTcpServer(EndpointEntity cfg) {
            bool started = false;
            string ip = "127.0.0.1";
            IPAddress addr = IPAddress.Parse(ip);
            tcpServerChannel = new TcpServerChannel(addr, cfg.port);
            tcpServerChannel.DataReceived += TcpDataHandler;
            started = tcpServerChannel.Start();
            log.InfoFormat("TCP_Server <-> COM 口适配器: Tcp Server 已启动. IP:{0},Port:{1},suc:{2}", ip, cfg.port, started);            
            return started;
        }

        private bool StartCom(EndpointEntity cfg) {
            bool started = false;
            LineBasedFrame decoder = new LineBasedFrame("\r\n");
            comChannel = new NetSerialPort("com", decoder);
            NetSerialPortOptions opt = new NetSerialPortOptions();
            opt.PortName = cfg.com;
            opt.BaudRate = cfg.baudrate;
            opt.Parity = cfg.parity;
            opt.DataBits = cfg.databits;
            opt.StopBits = cfg.stopbits;
            comChannel.Config(opt);
            comChannel.DataReceived += ComDataHandler;
            bool suc = comChannel.Open();
            log.InfoFormat("TCP_Server <-> COM 口适配器: COM启动. COM:{0},suc:{1}", opt.PortName,suc);
            return started;
        }

        private void TcpDataHandler(object? sender,TcpMsg msg) {
            C2TSession? client = sender as C2TSession;
            
            if (msg.buffer != null) {
                byte[] data = msg.buffer.Where(b => {
                    return (b != 0x0);
                }).ToArray();
                if (data.Length > 0) {
                    log.DebugFormat("tcp -> com data:{0}",data.Length);
                    comChannel?.Write(data);
                }
            }
        }

        /// <summary>
        /// 当收到来自Com口数据的时候,需要转发给已连接到TCP服务的客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComDataHandler(object? sender, IFrame e) {
            byte[]? data = e.RawBytes;
            tcpServerChannel?.Multicast(data);
        }
    }
    internal class RequestWapper {

    }
    internal class ResponseWapper
    {

    }
}
