using EventSource4Net;
using Maybe5.SharpSpark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace beakn
{
    public class SparkCore : Protocol
    {
        private SparkClient sparkClient;
        private EventSource eventSource;

        public override void Setup()
        {
            if (!IsConfigValid())
            {
                throw new ArgumentNullException("", "Please enter your Spark Core Device Id and Access Token in Settings dialog.");
            }
            else
            {
                sparkClient = new SparkClient(Properties.Settings.Default.AccessToken, Properties.Settings.Default.DeviceId);

                eventSource = sparkClient.GetEventStream();
                eventSource.EventReceived += eventSource_EventReceived;
                eventSource.Start(new CancellationToken());
            }
        }

        private bool IsConfigValid()
        {
            return !string.IsNullOrEmpty(Properties.Settings.Default.AccessToken) || !string.IsNullOrEmpty(Properties.Settings.Default.DeviceId);
        }

        void eventSource_EventReceived(object sender, ServerSentEventReceivedEventArgs e)
        {
            if (string.Compare(e.Message.EventType, "spark/status", true) == 0)
                OnEventReceived(e);
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
