using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface.Common
{
    public interface ICallBackFunction
    {
        string Confirm { get;}
        bool CanConfirm{ get;}
        bool CanCancel { get;}
        string Cancel  { get;}
        string Context { get;}

    }


    public class CallBackFunctionBase : ICallBackFunction
    {
        public CallBackFunctionBase()
        {
            Configure();
        }

        public string Confirm { get; set; }

        public bool CanConfirm { get; set; }

        public bool CanCancel { get; set; }

        public string Cancel { get; set; }

        public string Context { get; set; }

        public bool MultiSelect { get; set; }

        private void Configure()
        {
            if (this.Confirm != null)
                this.CanConfirm = true;
            if (this.Cancel != null)
                this.CanCancel = true;
        }
    }




}
