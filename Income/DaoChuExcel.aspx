<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DaoChuExcel.aspx.cs" Inherits="Income_DaoChuExcel" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title>生成凭证</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
     <script type="text/javascript" src="../js/calendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="Body_Title">
        财务管理 》》生成凭证</div>
  
   
    <table width="100%" class="Admin_Table" border="1">
                                <tr>
                                    <td style="width: 100%";  valign="top">
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                            ShowFooter="true" 
                                            CssClass="Admin_Table">
            
            <Columns>
             <asp:BoundField DataField="pzriqi" HeaderText="凭证日期" 
                 SortExpression="pzriqi"  DataFormatString ="{0:d}"/>
             <asp:BoundField DataField="year" HeaderText="会计年度" SortExpression="year" />
             <asp:BoundField DataField="qijian" HeaderText="会计期间" 
                 SortExpression="qijian" />
             <asp:BoundField DataField="pzzi" HeaderText="凭证字" SortExpression="pzzi" />
             <asp:BoundField DataField="pzhao" HeaderText="凭证号" SortExpression="pzhao" />
             <asp:BoundField DataField="kemudaima" HeaderText="科目代码" 
                 SortExpression="kemudaima" />
             <asp:BoundField DataField="kemumingchen" HeaderText="科目名称" 
                 SortExpression="kemumingchen" />
             <asp:BoundField DataField="bibiedaima" HeaderText="币别代码" 
                 SortExpression="bibiedaima" />
             <asp:BoundField DataField="bibiemingchen" HeaderText="币别名称" 
                 SortExpression="bibiemingchen" />
             <asp:BoundField DataField="yuanbi" HeaderText="原币" 
                 SortExpression="yuanbi" />
             <asp:BoundField DataField="jiefang" HeaderText="借方" 
                 SortExpression="jiefang" />
             <asp:BoundField DataField="daifang" HeaderText="贷方" 
                 SortExpression="daifang" />
             <asp:BoundField DataField="zhidan" HeaderText="制单" 
                 SortExpression="zhidan" />
             <asp:BoundField DataField="shenhe" HeaderText="审核" 
                 SortExpression="shenhe" />
             <asp:BoundField DataField="hezhun" HeaderText="核准" 
                 SortExpression="hezhun" />
             <asp:BoundField DataField="chuna" HeaderText="出纳" SortExpression="chuna" />
             <asp:BoundField DataField="jingban" HeaderText="经办" 
                 SortExpression="jingban" />
             <asp:BoundField DataField="jiesuanfangshi" HeaderText="结算方式" 
                 SortExpression="jiesuanfangshi" />
             <asp:BoundField DataField="jiesuanhao" HeaderText="结算号" 
                 SortExpression="jiesuanhao" />
             <asp:BoundField DataField="zhaiyao" HeaderText="摘要" 
                 SortExpression="zhaiyao" />
             <asp:BoundField DataField="shuliang" HeaderText="数量" 
                 SortExpression="shuliang" />
             <asp:BoundField DataField="shuliangdanwei" HeaderText="数量单位" 
                 SortExpression="shuliangdanwei" />
             <asp:BoundField DataField="danjia" HeaderText="单价" 
                 SortExpression="danjia" />
             <asp:BoundField DataField="cankaoxinxi" HeaderText="参考信息" 
                 SortExpression="cankaoxinxi" />
             <asp:BoundField DataField="yewuriqi" HeaderText="业务日期" 
                 SortExpression="yewuriqi" DataFormatString ="{0:d}"/>
             <asp:BoundField DataField="wanglaibianhao" HeaderText="往来编号" 
                 SortExpression="wanglaibianhao" />
             <asp:BoundField DataField="fujianshu" HeaderText="附件数" 
                 SortExpression="fujianshu" />
             <asp:BoundField DataField="xuhao" HeaderText="序号" SortExpression="xuhao" />
             <asp:BoundField DataField="xitongmokuai" HeaderText="系统模块" 
                 SortExpression="xitongmokuai" />
             <asp:BoundField DataField="yewumiaoshu" HeaderText="业务描述" 
                 SortExpression="yewumiaoshu" />
             <asp:BoundField DataField="huil" HeaderText="汇率" SortExpression="huil" />
            
             <asp:BoundField DataField="fenluxuhao" HeaderText="分录序号" SortExpression="huil" />

                


             <asp:BoundField DataField="hesuanxiangmu" HeaderText="核算项目" 
                 SortExpression="hesuanxiangmu" />
             <asp:BoundField DataField="guozhang" HeaderText="过账" 
                 SortExpression="guozhang" />
             <asp:BoundField DataField="jizhipz" HeaderText="机制凭证" 
                 SortExpression="jizhipz" />
             <asp:BoundField DataField="xianjinliuliang" HeaderText="现金流量" 
                 SortExpression="xianjinliuliang" />
         </Columns>
                                            <HeaderStyle CssClass="Admin_Table_Title " />
                                        </asp:GridView>
                                      
                                    </td>
                                </tr>
                                <tr><td align ="center" >
                                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="EXCEL" />
                                    </td></tr>
       

    </table>


     
    </form>
</body>
</html>
