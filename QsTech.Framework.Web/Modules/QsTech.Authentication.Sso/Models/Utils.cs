using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Framework.Environment;

namespace QsTech.Authentication.Sso.Models
{

   public static class StringExtensions
   {
       public static string AppendUrlParam(this string str,string name,string value)
       {
           if (str.Contains('?'))
           {
               return string.Format("{0}&{1}={2}",str,name,value);
           }
           else
               return string.Format("{0}?{1}={2}", str, name, value);
           
       }

       public static string AppendTokenParam(this string str, string name, string value)
       {
           if (!str.Contains('#'))
           {
               return string.Format("{0}#{1}={2}", str, name, value);
           }
           else
               return string.Format("{0}&{1}={2}", str, name, value);

       }

   }

     
  
}