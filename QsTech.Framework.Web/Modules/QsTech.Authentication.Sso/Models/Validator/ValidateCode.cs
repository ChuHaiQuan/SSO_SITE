using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using QsTech.Core.Interface;

namespace QsTech.Authentication.Sso.Models
{
    public class ValidateCodeGraph
    {
        public ValidateCodeGraph(string code)
        {
            CodeDescriptor = new ValidateCodeDescriptor(code);
        }

        public ValidateCodeGraph(IValidateCodeBuilder builder)
        {
            CodeDescriptor = builder.Build();
        }

        public ValidateCodeDescriptor CodeDescriptor { get; private set; }

        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="containsPage"></param>
        /// <param name="validateNum"></param>
        public byte[] CreatePicture()
        {
            var text = this.CodeDescriptor.Text;
            int randAngle = 35; //随机转动角度
            int mapwidth = (int)(text.Length * 19);
            Bitmap image = new Bitmap(mapwidth, 25);
            Graphics graph = Graphics.FromImage(image);
            graph.Clear(Color.White);
            graph.DrawRectangle(new Pen(Color.Silver, 0), 0, 0, image.Width - 1, image.Height - 1);
            Random rand = new Random();
            char[] chars = text.ToCharArray();
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            for (int i = 0; i < 1; i++)
            {
                int x1 = rand.Next(10);
                int x2 = rand.Next(image.Width - 10, image.Width);
                int y1 = rand.Next(image.Height);
                int y2 = rand.Next(image.Height);
                //graph.DrawLine(new Pen(c[rand.Next(7)]), x1, y1, x2, y2);
            }
            for (int i = 0; i < chars.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);
                Font f = new System.Drawing.Font("宋体", 20, System.Drawing.FontStyle.Bold);//字体样式(参数2为字体大小)
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                Point dot = new Point(13, 14);
                float angle = rand.Next(-randAngle, randAngle);
                graph.TranslateTransform(dot.X, dot.Y);
                //graph.RotateTransform(angle);
                graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
                //graph.RotateTransform(-angle);
                graph.TranslateTransform(2, -dot.Y);
            }
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }
    }
}
