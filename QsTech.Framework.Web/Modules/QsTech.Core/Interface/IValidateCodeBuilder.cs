using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface
{
    public class ValidateCodeDescriptor
    {
        public ValidateCodeDescriptor(string text)
            : this(text, text)
        {

        }

        public ValidateCodeDescriptor(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            if (Text != null && Value != null)
            {
                return Text + ":" + Value;
            }
            return string.Empty;
        }
    }

    public interface IValidateCodeBuilder : QsTech.Framework.ISingletonDependency
    {
        ValidateCodeDescriptor Build();
    }
}
