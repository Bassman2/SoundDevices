using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaDevices.IO.Internal.ASIO.COMInterface
{
	/*
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("a91eaba1-cf4c-11d3-b96a-00a0c9c7b61a")]
	public interface IASIO
	{
	[return: MarshalAs(UnmanagedType.Bool)]
	bool init([In,Out] ref IntPtr sysHandle);

	void getDriverName([Out, MarshalAs(UnmanagedType.LPStr)] out
	string name);
	....
	} */ 
	  
	[ComImport]
	[Guid("232685c6-6548-49d8-846d-4141a3ef7560")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	internal interface IAsioDriver
    {
		[return: MarshalAs(UnmanagedType.Bool)]
		bool init(ref IntPtr sysHandle);
		void getDriverName([Out, MarshalAs(UnmanagedType.LPStr)] out string name);	
		long getDriverVersion();
		void getErrorMessage([Out, MarshalAs(UnmanagedType.LPStr)] out string errorMsg);	
		int start();
		int stop();
		int getChannels(out int numInputChannels, out int numOutputChannels);
		int getLatencies(out int inputLatency, out int outputLatency);
		int getBufferSize(out int minSize, out int maxSize, out int preferredSize, out int granularity);
		int canSampleRate(double sampleRate);
		int getSampleRate(out double sampleRate);
		int setSampleRate(double sampleRate);
		int getClockSources(out ASIOClockSource clocks, out long numSources);
		int setClockSource(long reference);
		int getSamplePosition(out long sPos, out long tStamp);
		int getChannelInfo(out ASIOChannelInfo info);
		int createBuffers(out ASIOBufferInfo bufferInfos, long numChannels, long bufferSize, IntPtr callbacks);
		int disposeBuffers();
		int controlPanel();
		int future(int selector, IntPtr opt);
		int outputReady();    
    }

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct ASIOClockSource
	{
		int index;                 // as used for ASIOSetClockSource()
		int associatedChannel;     // for instance, S/PDIF or AES/EBU
		int associatedGroup;       // see channel groups (ASIOGetChannelInfo())
		int isCurrentSource;   // ASIOTrue if this is the current clock source
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		string name;              // for user selection
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct ASIOChannelInfo
	{
		int channel;           // on input, channel index
		int isInput;       // on input
		int isActive;      // on exit
		int channelGroup;      // dto
		int type;    // dto
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		string name;          // dto
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct ASIOBufferInfo
	{
		int isInput;           // on input:  ASIOTrue: input, else output
		int channelNum;            // on input:  channel index
		IntPtr[] buffers;           // on output: double buffer addresses
	}
		
}
