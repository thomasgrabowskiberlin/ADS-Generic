using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;
using System.Data;
using System.IO;


namespace IStack3_HMI.ADS
{
    public class TcADSVar<T>
    {
        public string VarName = new string(new char[] { });

        public TcAdsClient ADSclient;

        public int Handle;
        public T value;

        public TcADSVar(ref TcAdsClient refadsclient, string strName)
        {
            this.VarName = strName;
            ADSclient = refadsclient;

            try
            {
                Handle = ADSclient.CreateVariableHandle(VarName);
            }
            catch
            {
                Handle = 0;
            }
        }

        public int VarHandle
        {
            get { return Handle; }
            set { Handle = ADSclient.CreateVariableHandle(this.VarName); }
        }

        public T Value
        {

            set { ADSclient.WriteAny(Handle, value); }

            get { return (T)ADSclient.ReadAny(Handle, typeof(T)); }

        }

        public void SetValue(T value)
        {
            ADSclient.WriteAny(Handle, value);
        }

        public void DeleteVarHandle()
        {
            if (VarHandle != 0)
                ADSclient.DeleteVariableHandle(VarHandle);
        }

        public void ReadADSArray( ref double[] Array)
        {
            Type itemType = typeof(T);
            if (itemType == typeof(double[]))
            {
                // AdsStream which gets the data
                AdsStream dataStream = new AdsStream(Array.Length * 8);
                BinaryReader binRead = new BinaryReader(dataStream);

                //read comlpete Array 
                ADSclient.Read(VarHandle, dataStream);

                for (int cnt = 0; cnt < Array.Length; cnt++)
                    Array[cnt] = binRead.ReadDouble();
            }

        }

        public void WriteADSArray( ref double[] Array)
        {
            Type itemType = typeof(T);
            if (itemType == typeof(double[]))
            {

                // AdsStream for sending data
                AdsStream dataStream = new AdsStream(Array.Length * 8);
                BinaryWriter binWriter = new BinaryWriter(dataStream);


                for (int cnt = 0; cnt < Array.Length; cnt++)
                    binWriter.Write(Array[cnt]);

                // Write complete Array
                ADSclient.Write(VarHandle, dataStream);
            }




        }

        // int
        public void ReadADSArray( ref int[] Array)
        {
            Type itemType = typeof(T);
            if (itemType == typeof(int[]))
            {
                // AdsStream which gets the data
                AdsStream dataStream = new AdsStream(Array.Length * 4);
                BinaryReader binRead = new BinaryReader(dataStream);

                //read comlpete Array 
                ADSclient.Read(VarHandle, dataStream);

                for (int cnt = 0; cnt < Array.Length; cnt++)
                    Array[cnt] = binRead.ReadInt32();
            }

        }

        public void WriteADSArray( ref int[] Array)
        {
            Type itemType = typeof(T);
            if (itemType == typeof(int[]))
            {

                // AdsStream for sending data
                AdsStream dataStream = new AdsStream(Array.Length * 4);
                BinaryWriter binWriter = new BinaryWriter(dataStream);


                for (int cnt = 0; cnt < Array.Length; cnt++)
                    binWriter.Write(Array[cnt]);

                // Write complete Array
                ADSclient.Write(VarHandle, dataStream);
            }

        }
    }
}
