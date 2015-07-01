using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using QsTech.Core.Interface;

namespace QsTech.Authentication.Sso.Models
{
    [QsTech.Framework.Feature("StringValidateCodeBuilder")]
    public class StringValidateCodeBuilder : IValidateCodeBuilder
    {
        public const int DEFAULT_LENGTH = 5;

        public StringValidateCodeBuilder()
        {
            Length = DEFAULT_LENGTH;
        }

        /// <summary>
        /// 验证码长度
        /// </summary>
        public int Length { get; set; }

        public ValidateCodeDescriptor Build()
        {
            int number;
            string RandomCode = string.Empty;
            int len = Length;
            Random r = new Random();
            for (int i = 0; i < len; i++)
            {
                number = r.Next();
                number = number % 36;
                if (number < 10)
                    number += 48;
                else
                    number += 55;

                if (number == 48 || number == 79 || number == 49 || number == 73)
                    number = number + 2;

                RandomCode += ((char)number).ToString();
            }
            return new ValidateCodeDescriptor(RandomCode);
        }
    }

    /// <summary>
    /// 算术验证码，默认为数值1-20的加/减法运算，可以通过设置 MinValue 和 MaxValue 来限定算法值的范围。
    /// </summary>
    [QsTech.Framework.Feature("ArithmeticValidateCodeBuilder")]
    public class ArithmeticValidateCodeBuilder : IValidateCodeBuilder
    {
        private const string Plus = "+";
        private const string Subtract = "-";

        private static Tuple<string, Func<int, int, OperationResult>>[] Operations;

        static ArithmeticValidateCodeBuilder()
        {
            // 定义加减法
            Operations = new Tuple<string, Func<int, int, OperationResult>>[2];
            Operations[0] = new Tuple<string, Func<int, int, OperationResult>>(Plus, (x, y) =>
            {
                var result = new OperationResult
                {
                    X = x,
                    Y = y,
                    Result = x + y
                };
                return result;
            });
            Operations[1] = new Tuple<string, Func<int, int, OperationResult>>(Subtract, (x, y) =>
            {
                // 如果 x 小于 y，则将 x 和 y 值互换，以免出现负数的情况
                if (y > x)
                {
                    int tempX = y;
                    y = x;
                    x = tempX;
                }
                var result = new OperationResult
                {
                    X = x,
                    Y = y,
                    Result = x - y
                };
                return result;
            });
        }

        public ArithmeticValidateCodeBuilder()
        {
            MinValue = 1;
            MaxValue = 10;
        }

        struct OperationResult
        {
            public int X { get; set; }

            public int Y { get; set; }

            public int Result { get; set; }
        }

        /// <summary>
        /// 获取或设置四则运算中的最小值
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// 获取或设置四则运算中的最大值
        /// </summary>
        public int MaxValue { get; set; }

        public ValidateCodeDescriptor Build()
        {
            // 随机数不能取上限，因此这里为上限 +1 以满足真实的数值范围。
            var maxValue = MaxValue + 1;
            var random = new Random();
            int x = random.Next(MinValue, maxValue);
            int y = random.Next(MinValue, maxValue);
            var operation = RandomOperation();
            var operationResult = operation.Item2(x, y);

            string text = string.Format("{0}{1}{2}=?", operationResult.X, operation.Item1, operationResult.Y);
            return new ValidateCodeDescriptor(text, operationResult.Result.ToString());
        }

        static Tuple<string, Func<int, int, OperationResult>> RandomOperation()
        {
            int randomValue = new Random().Next(1, 3);
            return Operations[--randomValue];
        }
    }
}
