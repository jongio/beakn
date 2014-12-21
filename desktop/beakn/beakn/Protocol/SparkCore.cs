using Maybe5.SharpSpark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn
{
    public class SparkCore : Protocol
    {
        private SparkClient sparkClient;

        public override void Setup()
        {
            sparkClient = new SparkClient(Config.SparkCoreAccessToken, Config.SparkCoreDeviceId);
        }

        public override void Send(string message)
        {
            try
            {
                var result = sparkClient.ExecuteFunctionReturnValue("setStatus", message);
                if (result == 1)
                {
                    OnSendSuccess(new MessageEventArgs("Message Send Success:" + message));
                }
                else
                {
                    OnSendFailure(new MessageEventArgs("Message Send Failure:" + message));

                }
            }
            catch (Exception e)
            {
                OnSendFailure(new MessageEventArgs(string.Format("Exception when sending message: {0}, Exception={1}", message, e.ToString())));
            }
        }
    }
}
