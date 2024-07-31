using com2eth.serialport;

namespace com2eth {
    internal class Program {
        static void Main(string[] args) {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("config/log4net.config"));
            NetSerialPort netComd = new NetSerialPort();
            Console.WriteLine("Hello, World!");
        }
    }
}
