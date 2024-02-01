using DymoSDK.Implementations;
using PrintHook.Services;
using System;
using System.Linq;
using System.ServiceProcess;

namespace PrintHook
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        internal void OnDebug()
        {
        }
    }
}
