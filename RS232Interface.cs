using System;
using System.IO;
using System.IO.Ports;	// Serial communication
using System.Text;


class RS232Data{
	public Int32 nLength;
	public byte[] data;
	public Int32 nStatus;
	private Int32 nMaxBuffersize;

	public RS232Data(){
		nLength = 0;
		nMaxBuffersize = 4096;
		nStatus = 0x00;
		data = new byte[nMaxBuffersize];
	}

	public Int32 GetMaxBufferSize(){
		return nMaxBuffersize;
	}

	public void ClearBuffer()
	{
		nLength = 0;
		Array.Clear(data, 0, nMaxBuffersize);
	}
}

class RS232Interface{
	// Attribute ==========
	private string strComPort;
	private string strBaudRate;
	private SerialPort rs232;
	delegate void SetTextCallback(string opt);
	private byte[] recvData;
	private Int32 recvLength;
	private string strError;
	private string[] comlist;

	// Method ==========
	// Public
	public RS232Interface(){
		strComPort = "COM5";
		strBaudRate = "38400";
		rs232 = new SerialPort();
		recvData = new byte[256];
		recvLength = 0;
	}

	public static string[] GetPorts(){
		return SerialPort.GetPortNames();
	}

	public void SetComm(string strCom){
		strComPort = strCom;
	}

	public string GetComm(){
		return strComPort;
	}

	public void SetBaudrate(string strBaud){
		strBaudRate = strBaud;
	}

	public string GetBaudrate(){
		return strBaudRate;
	}

	public RS232Data GetData(){
		RS232Data data = new RS232Data();

		if(recvLength > 0){
			data.nLength = recvLength;
			recvLength = 0;
			Array.Copy(recvData, data.data, data.nLength);
		}

		return data;
	}

	public int ReadByte(byte[] buf, int offset, int length){
		return rs232.Read(buf, offset, length);
	}

	public string ReadEx(){
		return rs232.ReadExisting();
	}

	public bool Open(string strCom){
		rs232.PortName = strComPort;
		rs232.BaudRate = Int32.Parse(strBaudRate);
		rs232.DataBits = 8;
		rs232.StopBits = StopBits.One;
		rs232.Parity = Parity.None;

		try{
			rs232.Open();
		}catch(UnauthorizedAccessException e){
			strError = e.Message;
		}catch(ArgumentOutOfRangeException e){
			strError = e.Message;
		}catch(ArgumentException e){
			strError = e.Message;
		}catch(IOException e){
			strError = e.Message;
		}catch(InvalidOperationException e){
			strError = e.Message;
		}catch(Exception e){
			strError = e.Message;
		}

		if(rs232.IsOpen){
			return true;
		}

		return false;
	}

	public void Close(){
		if(rs232.IsOpen){
			rs232.Close();
		}
	}

	public bool Write(byte[] dat, Int32 length){
		if(rs232.IsOpen){
			rs232.Write(dat, 0, length);
		}else{
			return false;
		}

		return true;
	}

	public string GetLastError(){
		return strError;
	}

	public void RefreshComList(){
		comlist = System.IO.Ports.SerialPort.GetPortNames();
	}

	/*
	 * Function name	: GetComListCount
	 * Return			: ComPort list count
	 */
	public int GetComListCount(){
		return comlist.Length;
	}

	public string[] GetComPorts(){
		return comlist;
	}

	// Private
	private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e){
		byte[] buf = new byte[256];
		rs232.Read(buf, 0, 43);
		SetData(buf, 43);
	}

	private void SetData(byte[] buf, Int32 length){
		Array.Copy(buf, recvData, length);
		recvLength = length;
	}
}