using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using QsTech.Framework;

namespace QsTech.Authentication.Sso.Models.Authorization
{
    public interface IRandomStringManager : ISingletonDependency
    {
        /// <summary>
        /// 根据传入的长度，获取随机数字和字母组合
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GetRandomNumberAndCharacters(int length);

 
    }

    public class RandomStringManager : IRandomStringManager
    {
        private  readonly Random _seedRandom;
        private const string RandomString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public RandomStringManager()
        {
            _seedRandom = new Random();
        }


        public string GetRandomNumberAndCharacters(int length)
        {
            var seeds = new int[length];
            for (var i = 0; i < length; i++)
            {
                var tick = (i+1)*1999999;
                seeds[i] = _seedRandom.Next(tick);
            }
            var code = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var randomIndex = seeds[i] % length;
                var vseed = seeds[randomIndex];
                var vrd = new Random(vseed);
                var startIndex = vrd.Next(0, RandomString.Length);
                code.Append(RandomString.Substring(startIndex, 1));
            }
            return code.ToString();
        }
    }
}