using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;
using IStack3_HMI.ADS;


namespace ADSVar_Generic
{


    class TcADSTest
    {


        
        static void ADSTest()
        {
            string stAddr = "1.1.1.1.1.1";
            int nPort = 851;
            TcAdsClient tcADS = new TcAdsClient();

            tcADS.Connect(stAddr, nPort);

            TcADSVar<bool> bEnable = new TcADSVar<bool>(ref tcADS, "GVL.bEnable");
            TcADSVar<double> AxisPosition = new TcADSVar<double>(ref tcADS, "GVL.Position");

            AxisPosition.Value = 3.14;
            double Pos = AxisPosition.Value;

            bEnable.Value = true;
            bool En = bEnable.Value;

            tcADS.Dispose();






        }
    }
}
