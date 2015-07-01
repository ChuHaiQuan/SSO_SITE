using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QsTech.Core.Interface
{
    public interface IListSort
    {
         Dictionary<string,string> SortNames { get; }

         string Sorted { get; }
    }

    public class DefaultListSort : IListSort
    {
        public Dictionary<string, string> SortNames{ get; set;}

        public string Sorted { get; set; }
    }
}
