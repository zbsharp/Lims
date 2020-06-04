<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Drawing;

public class Handler : IHttpHandler {



    protected int _height = 30;
    protected string _code = "0002bfft6280824";
    protected string code = "";

    
    public void ProcessRequest (HttpContext context)    
    {
        if (context.Request.QueryString["height"] != null)
        {
            _height = Convert.ToInt32(context.Request.QueryString["height"].ToString());
        }
        if (context.Request.QueryString["code"] != null)
        {
            _code = context.Request.QueryString["code"].ToString();
        }
        code = getCodeText(_code);
        int p_w = code.Length;
        int p_h = _height + 20;
        context.Response.ContentType = "image/gif";
        System.Drawing.Bitmap myBitmap = new Bitmap(p_w, p_h);

        System.Drawing.Graphics myGrap = Graphics.FromImage(myBitmap);
        myGrap.Clear(Color.White);

        for (int i = 0; i < p_w; i++)
        {
            Pen myPen = new Pen(Color.White, 1);
            if (code.Substring(i, 1) == "|")
            {
                myPen.Color = Color.Black;
            }
            // myGrap.DrawString(_code.Substring(i, 1), new Font("宋体", 12), new SolidBrush(Color.Black), i*13, 20);
            myGrap.DrawLine(myPen, i, 0, i, _height);
        }

        myGrap.DrawString(_code, new Font("Courier New", 6), new SolidBrush(Color.Black), -4, _height);
        myBitmap.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        context.Response.End();

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string getCodeText(string n)
    {
        string zf = n.ToLower();
        zf = zf.Replace("0", "_|_|__||_||_|");
        zf = zf.Replace("1", "_||_|__|_|_||");
        zf = zf.Replace("2", "_|_||__|_|_||");
        zf = zf.Replace("3", "_||_||__|_|_|");
        zf = zf.Replace("4", "_|_|__||_|_||");
        zf = zf.Replace("5", "_||_|__||_|_|");
        zf = zf.Replace("7", "_|_|__|_||_||");
        zf = zf.Replace("6", "_|_||__||_|_|");
        zf = zf.Replace("8", "_||_|__|_||_|");
        zf = zf.Replace("9", "_|_||__|_||_|");
        zf = zf.Replace("a", "_||_|_|__|_||");
        zf = zf.Replace("b", "_|_||_|__|_||");
        zf = zf.Replace("c", "_||_||_|__|_|");
        zf = zf.Replace("d", "_|_|_||__|_||");
        zf = zf.Replace("e", "_||_|_||__|_|");
        zf = zf.Replace("f", "_|_||_||__|_|");
        zf = zf.Replace("g", "_|_|_|__||_||");
        zf = zf.Replace("h", "_||_|_|__||_|");
        zf = zf.Replace("i", "_|_||_|__||_|");
        zf = zf.Replace("j", "_|_|_||__||_|");
        zf = zf.Replace("k", "_||_|_|_|__||");
        zf = zf.Replace("l", "_|_||_|_|__||");
        zf = zf.Replace("m", "_||_||_|_|__|");
        zf = zf.Replace("n", "_|_|_||_|__||");
        zf = zf.Replace("o", "_||_|_||_|__|");
        zf = zf.Replace("p", "_|_||_||_|__|");
        zf = zf.Replace("r", "_||_|_|_||__|");
        zf = zf.Replace("q", "_|_|_|_||__||");
        zf = zf.Replace("s", "_|_||_|_||__|");
        zf = zf.Replace("t", "_|_|_||_||__|");
        zf = zf.Replace("u", "_||__|_|_|_||");
        zf = zf.Replace("v", "_|__||_|_|_||");
        zf = zf.Replace("w", "_||__||_|_|_|");
        zf = zf.Replace("x", "_|__|_||_|_||");
        zf = zf.Replace("y", "_||__|_||_|_|");
        zf = zf.Replace("z", "_|__||_||_|_|");
        zf = zf.Replace("-", "_|__|_|_||_||");
        zf = zf.Replace("*", "_|__|_||_||_|");
        zf = zf.Replace("/", "_|__|__|_|__|");
        zf = zf.Replace("%", "_|_|__|__|__|");
        zf = zf.Replace("+", "_|__|_|__|__|");
        zf = zf.Replace(".", "_||__|_|_||_|");
        return zf;
    }

    

}