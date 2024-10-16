using com2eth.serialport;
using com2eth.serialport.Codec;
using log4net;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthSerialPort.Codec
{
    /// <summary>
    /// 以 \n (0x0A) 或者 \r\n(0x0D 0x0A) 为结束符的数据帧解析类
    /// </summary>
    public class LineBasedFrame : IDataFrameHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LineBasedFrame));
        /// <summary>
        /// 默认以换行符 \n (0x0D)为结束符
        /// </summary>
        private byte[] endBytes = new byte[1] {0x0D};
        public LineBasedFrame() {  }
        public LineBasedFrame(string endFlag) {
            if (!string.IsNullOrEmpty(endFlag)) {
                endBytes = System.Text.Encoding.ASCII.GetBytes(endFlag);
            }
        }
        public LineBasedFrame(byte[] endFlag) { }
        
        private List<byte> _cacheBytes = new List<byte>();

        List<IFrame>? IDataFrameHandler.Decode(byte[] data) {
            return DecodeWithByte(data);
        }
        public List<IFrame>? DecodeWithByte(byte[] data) {
            List<IFrame>? frames = new List<IFrame>();            
            _cacheBytes.AddRange(data);
            int count = _cacheBytes.Count;
            int tailLen = endBytes.Length;
            byte[] last = _cacheBytes.TakeLast(tailLen).ToArray();

            int begin = 0, end = -1, remove = 0;
            for (int i = 0; i< count; i++) {
                // 情况1: 没有结束符,或者不全
                // 情况2: 刚好是一个完整的包
                bool tailStart = false;
                for(int t = 0; t < tailLen;t++) {
                    int idx = i + t;
                    if (t==0 && _cacheBytes[idx] == endBytes[t]) {
                        // 匹配到第1个尾字符
                        tailStart = true;
                        //log.DebugFormat("结束符首个匹配:i={0},byte={1}", idx, _cacheBytes[idx]);
                    } else if (t > 0 && tailStart && idx < count && _cacheBytes[idx] == endBytes[t]) {
                        // 后续也匹配上了
                        tailStart = true;
                        end= idx;
                        //log.DebugFormat("结束符匹配:i={0},byte={1}", idx, _cacheBytes[idx]);
                    }else {
                        tailStart = false;
                        end = -1;
                    }                    
                }
                // log.DebugFormat("结束符号匹配完毕:是否存在结束符={0},end={1}", tailStart, end);
                if (end != -1) {
                    List<byte> frameBytes = _cacheBytes.GetRange(begin, (end - begin + 1));
                    remove += frameBytes.Count();
                    IFrame frame = new IFrame();
                    byte[] fbyte = frameBytes.ToArray();
                    frame.RawBytes = fbyte;
                    frame.String = Encoding.UTF8.GetString(fbyte);
                    frames.Add(frame);
                    begin = (end +1); end=-1;
                }
            }
            if (remove > 0) {
                _cacheBytes.RemoveRange(0, remove);
            }
            log.DebugFormat("完整数据帧:{0},缓存数据剩余:{1}", frames.Count, _cacheBytes.Count);
            return frames;
        }
       
        string _StringCache = "";
        private List<string> ReadLine(string data) {
            List<string> lines = new List<string>();
            if (String.IsNullOrEmpty(data)) {
                return lines;
            }

            // 先将缓存中内容拼接当前数据
            string tmp = _StringCache + data;
            int firstEndIdx = tmp.IndexOf("\r\n");
            if (firstEndIdx < 0) {
                // 没有结束符,表示为半包
                _StringCache = _StringCache + data;
                return lines;
            } else {
                int dl = tmp.Length;
                int start = 0;
                int len = 0;
                // 存在结束符,开始遍历查找
                for (int i = 0; i < dl; i++) {
                    char c = tmp[i];
                    int next = i + 1;
                    if (c == '\r' && next < dl && tmp[next] == '\n') {
                        // 找到结束符
                        len = next - start + 1;
                        string pack = tmp.Substring(start, len);
                        lines.Add(pack);
                        start = next + 1;
                    }
                }
                if (start < dl) {
                    // 如果结束符小于数据长度,表示还有剩余
                    _StringCache = tmp.Substring(start, (dl - start));
                } else if (start == dl) {
                    _StringCache = string.Empty;
                }
                return lines;
            }

        }
        private List<byte> cache = new List<byte>();
        private byte[] header = new byte[3] { 0x24, 0x36, 0x30 };
        protected  void OnReceived(byte[] buffer, long offset, long size) {
            // ScrewEventBus seb = ScrewEventBus.Inst();
            try {
                byte[] temp = buffer.Skip((int)offset).Take((int)size).ToArray();
                cache.AddRange(temp);
                string bufferStr = BitConverter.ToString(temp, 0);
                log.InfoFormat("received screw buffer: {0}", bufferStr);
                int startIndex = -1, endIndex = -1;
                if (cache.Count > 3) {
                    for (int i = 0; i < cache.Count; i++) {
                        int nextIdx = i + 1;
                        int nextNextIdx = i + 2;
                        if (cache[i] == 0x24 && nextIdx < cache.Count && cache[nextIdx] == 0x36 && nextNextIdx < cache.Count && cache[nextNextIdx] == 0x30) {
                            startIndex = i;
                        }
                        // 如果,刚好最后一个字符时 0d.
                        if (cache[i] == 0x0D && nextIdx < cache.Count && cache[nextIdx] == 0x0A) {
                            endIndex = i;
                        }
                    }
                }
                // 24 36 30 46 46 46 46 46 46 46 46 46 46 30 30 30 30 30 30 2C 34 30 36 2C 50 52 45 53 45 54 31 5F 54 52 51 3A 30 2E 37 38 2C 4F 4B 2C 6E 6F 6E 2C 6E 6F 6E 2C 4C 48 43 32 2C 37 34 32 2C 31 32 36 32 2C 31 33 30 2C 2D 2D 2C 32 31 33 34 2C 31 35 32 32 2E 39 2C 33 32 33 2E 33 2C 31 32 2E 34 2C 2D 2D 2C 2D 2D 2C 2D 2D 2C 31 38 35 38 2E 36 2C 30 2E 31 31 38 2C 30 2E 37 37 39 2C 2D 2D 2C 30 2E 30 31 37 2C 32 2E 33 2C 20 20 4E 2E 6D 20 2C 2A 35 30 2C 2A 34 30 30 2C 2A 35 30 2C 31 30 31 38 31 39 2C 46 4C 38 34 38 31 2C 30 2C 30 30 3A 30 30 3A 30 30 2E 30 30 2C 2D 2D 2C 2D 2D 2C 2D 2D 2C 2D 2D 2C 2D 2D 2C 2D 2D 2C 2D 2D 2C 33 35 38 2E 30 0D 0A
                if (startIndex > -1 && endIndex > -1 && (endIndex > startIndex)) {
                    List<byte> msgPacke = cache.GetRange(startIndex, (endIndex - startIndex + 1));
                    string msgPackeStr = Encoding.UTF8.GetString(msgPacke.ToArray(), 0, msgPacke.Count);
                    string msg = string.Format("螺丝机完整数据: {0}", msgPackeStr);
                    
                    // 解析到一个正常包后,处理剩余数据
                    if (startIndex == 0 && endIndex == (cache.Count - 1)) {
                        log.InfoFormat("缓存中为完整数据包...");
                        cache.Clear();
                    } else if (endIndex < (cache.Count - 1)) {
                        log.InfoFormat("缓存中数据多于一个完整包,需要容错处理");
                        if (startIndex > 0) {
                            log.InfoFormat("存在前序缓存数据,忽略前序异常数据...");
                            startIndex = 0;
                        }
                        cache.RemoveRange(startIndex, (endIndex - startIndex + 2));
                        log.InfoFormat("缓存剩余数据: {0}", BitConverter.ToString(cache.ToArray(), 0));
                    }
                }
            } catch (Exception e) {
                cache.Clear();
                string msge = string.Format("解析螺丝机数据异常: {0}", e);
                log.Info(msge);
                // seb.ShowMsg(msge);
            }
            // $60FFFFFFFF00000000,15,PRESET2_TRQ:0.325,OK,non,non,non,1086,154,100,--,1340,5455.7,42.6,0.4,--,--,--,5498.7,0.019,0.324,--,0.003,0.9,kgf.cm,20,895,352,100255,GL2218,0,00:00:00.00

        }
    }
}
