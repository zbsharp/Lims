<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoOther.aspx.cs" Inherits="Quotation_QuoOther" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="div_All">
    <div class="Body_Title">报价管理 》》录入附加信息</div>
        <hr />
     <table cellpadding="2" cellspacing="1" class="Admin_Table" style="width:99%; margin:5px auto;">
     <thead>
        <tr class="Admin_Table_Title">
            <th colspan="4">基本信息</th>
          
        </tr>
     </thead>
        <tr>
            <td>委托方：</td>
            <td>
                <asp:TextBox ID="weituot1" runat="server" CssClass="txtHInput" 
                     ></asp:TextBox>&nbsp;<img src="../Images/warning.png"/>必填</td>
            <td>委托方联系：</td>
            <td>
                <asp:TextBox ID="weituolianxi1" runat="server" CssClass="txtHInput" 
                   ></asp:TextBox></td>
        </tr>
        <tr>
            <td>付款方：</td>
            <td>
                <asp:TextBox ID="fukuan1" runat="server" CssClass="txtHInput" 
                    ></asp:TextBox>&nbsp;<img src="../Images/warning.png"/>必填</td>
            <td>付款方联系：</td>
            <td>
                <asp:TextBox ID="fukuanlianxi1" runat="server" CssClass="txtHInput" 
                    ></asp:TextBox></td>
        </tr>
        <tr>
            <td>代理方：</td>
            <td>
            <asp:TextBox ID="daili1" runat="server" CssClass="txtHInput" ></asp:TextBox>&nbsp;<img src="../Images/warning.png"/>必填</td>
            <td>代理方联系：</td>
            <td>
                <asp:TextBox ID="daililianxi1" runat="server" CssClass="txtHInput" 
                   ></asp:TextBox></td>
        </tr>
        <tr>
            <td>制造方：</td>
            <td>
            <asp:TextBox ID="zhizao1" runat="server" CssClass="txtHInput" ></asp:TextBox></td>
            <td>制造方联系：</td>
            <td>
            <asp:TextBox ID="zhizaolianxi1" runat="server" CssClass="txtHInput" ></asp:TextBox></td>
        </tr>
        <tr>
            <td>生产方：</td>
            <td>
            <asp:TextBox ID="shenchan1" runat="server" CssClass="txtHInput" ></asp:TextBox></td>
            <td>生产方联系：</td>
            <td>
            <asp:TextBox ID="shenchanlianxi1" runat="server" CssClass="txtHInput" ></asp:TextBox></td>
        </tr>

          <tr>
            <td>生产方地址：</td>
            <td>
            <asp:TextBox ID="shenchandidian1" runat="server" CssClass="txtHInput" ></asp:TextBox></td>
            <td>备注：</td>
            <td>
            <asp:TextBox ID="beizhu" runat="server" CssClass="txtHInput" ></asp:TextBox></td>
        </tr>
        <tr>
           
            <td colspan="4" align="center" >
                <asp:Button ID="Button2" runat="server" Text="保存信息" onclick="Button2_Click" 
                    />&nbsp;
                <input id="txtRe" type="reset" value=" 重填 " />
                &nbsp;</td>
         
           
        </tr>

        <tr>
        <td colspan ="4">
        
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" AutoGenerateColumns="false">
                           
                            <Columns>
                                 <asp:BoundField DataField="weituofang" HeaderText="委托方"></asp:BoundField>
                                                        <asp:BoundField DataField="weituolianxi" HeaderText="委托方联系" Visible="false"></asp:BoundField>
                                                        <asp:BoundField DataField="fukuan" HeaderText="付款方"></asp:BoundField>
                                                        <asp:BoundField DataField="fukuanlianxi" HeaderText="付款联系"></asp:BoundField>
                                                        <asp:BoundField DataField="dailifang" HeaderText="代理"></asp:BoundField>
                                                        <asp:BoundField DataField="daililianxi" HeaderText="代理方联系"></asp:BoundField>
                                                        <asp:BoundField DataField="zhizao" HeaderText="制造"></asp:BoundField>
                                                      
                                                        <asp:BoundField DataField="zhizaolianxi" HeaderText="制造联系"></asp:BoundField>
                                                        <asp:BoundField DataField="shenchan" HeaderText="生产方"></asp:BoundField>
                                                      
                                                        <asp:BoundField DataField="shenchanlianxi" HeaderText="生产联系"></asp:BoundField>
                                                        <asp:BoundField DataField="shenchandizhi" HeaderText="生产地址"></asp:BoundField>
                                                       <asp:BoundField DataField="beizhu" HeaderText="备注"></asp:BoundField>
                                                     
                                                      
                                                      
                                                        <asp:CommandField ShowEditButton="false" ShowDeleteButton="true" CausesValidation="False" />
                                                       
                            </Columns>
                            <HeaderStyle CssClass="Admin_Table_Title " />
                        </asp:GridView>
                         
                    </ContentTemplate>

                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                    </Triggers>

                </asp:UpdatePanel>
        
        
        </td>
        
        </tr>
     </table>

    </form>
</body>
</html>
