<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default5.aspx.cs" Inherits="Quotation_Default5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script>
        function RtValue(rtstr) {

            window.returnValue = rtstr;
            window.close()
        }

 
</script>
<script language =javascript type="text/javascript">
    function OnTreeNodeChecked() {
        var Obj = event.srcElement;
        if (Obj.tagName == "A" || Obj.tagName == "a") {
            alert(Obj.id); //访问被点击结点的值，同样可以通过Obj.innerText设置它的值   
            window.returnValue = Obj.id;
            window.close()
            return;
        }
    }  
                               </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        当前产品：<asp:Label ID="Label1" runat="server" Text="" ForeColor ="Red" ></asp:Label>


         <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="删除该目录" />


         <div style ="text-align:center ">
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="保存项目" Visible ="false"  /></div>
     <asp:GridView ID="GridView1"  runat="server" DataKeyNames ="id" AutoGenerateColumns="False" CssClass="Admin_Table"  Width="100%">
                     <Columns>

                      <asp:TemplateField Visible ="false" >
                            <HeaderTemplate>
                                全选<asp:CheckBox ID="CheckBox3" Enabled="false" runat="server" 
                                    AutoPostBack="True" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="neirongid" HeaderText="项目编号"  />
                         <asp:BoundField DataField="neirong" HeaderText="测试项目" DataFormatString="&lt;a style=quot;cursor:pointer quot;  onclick=&quot;RtValue('{0}')&quot;&gt;{0}&lt;/a&gt;" HtmlEncode="False">
                             <HeaderStyle Width="200px" />
                             <ItemStyle HorizontalAlign="Center" />
                         </asp:BoundField>
                         
                         <asp:BoundField DataField="biaozhun" HeaderText="依据标准"  />
                          <asp:BoundField DataField="zhouqi" HeaderText="周期"  />
                           <asp:BoundField DataField="yp" HeaderText="样品数量"  />
                            <asp:BoundField DataField="shoufei" HeaderText="费用"  />

                            <asp:BoundField DataField="danwei" HeaderText="单位"  />
                  <asp:BoundField DataField="beizhu" HeaderText="说明"  />

                         <asp:BoundField DataField="epiboly" HeaderText="项目类型" />

                    <asp:TemplateField HeaderText="修改"> 
                                <ItemTemplate>
                                  
                                    <span  style ="cursor :hand;" onclick ="window.open('../SysManage/ChanPingUpdate.aspx?id=<%#Eval("id") %>','test','dialogWidth=800px;DialogHeight=400px;status:no;help:no;resizable:yes; dialogTop:100px;edge:raised;')">修改</span> 
                                 
                                    
                                </ItemTemplate>
                            </asp:TemplateField>    


                     </Columns>
              <HeaderStyle CssClass="Admin_Table_Title " />
                 </asp:GridView>
    </div>
   



    </form>
</body>
</html>

