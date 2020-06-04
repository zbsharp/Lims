<%@ WebHandler Language="C#" Class="Handler2" %>

using System;
using System.Web;
using System.Drawing;
using Common;

public class Handler2 : IHttpHandler {

    protected int _height = 30;
    protected string _code = "0002bfft6280824";
    protected string code1 = "";


    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.QueryString["code"] != null)
        {
            code1 = context.Request.QueryString["code"].ToString();
        }

        Code128 _Code = new Code128(); 
        _Code.ValueFont = new Font("宋体", 10);
        //System.Drawing.Bitmap imgTemp = _Code.GetCodeImage("T26200-1900-123-1-0900",Code128.Encode.Code128A); 
        //imgTemp.Save(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + "BarCode.gif", System.Drawing.Imaging.ImageFormat.Gif);
        
           
     
        context.Response.ContentType = "image/gif";
        System.Drawing.Bitmap myBitmap = _Code.GetCodeImage(code1, Code128.Encode.Code128A); 

      
        myBitmap.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        context.Response.End();

    }

    public bool IsReusable
    {
        get
        {
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