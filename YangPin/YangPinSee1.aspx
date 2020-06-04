<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPinSee1.aspx.cs" Inherits="YangPin_YangPinSee1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	样品查看
</title>
  <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
<script language="javascript" src="popcalendar.js"></script>
	<script language="javascript" src="../../Js/calendar.js" type="text/javascript">
	    function bnQuery_onclick() {

	    }

        </script>
         <script type="text/javascript" src="../Celend/popcalendar.js"></script>
         
<script type ="text/javascript" >
    function add() {
        var a = parseFloat(document.getElementById("TextBox8").value) * parseFloat(document.getElementById("TextBox9").value);

        if (a != null) {
            document.getElementById("TextBox10").value = a;
        }
    }
</script>

</head>
<body>
    <form name="form1"  runat="server"  id="form1">
<div>
<div class="Body_Title">
       业务受理 》》样品查看</div>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

      
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff" style =" display :none ;">
                            <td style="text-align: left; width: 90px;">
                                厂商名称：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                案件编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 110px;">
                                服务编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                样品编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox12" runat="server" ReadOnly="True"></asp:TextBox></td>
                            <td style="text-align: left; width: 110px;">
                                样品名称：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                型号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 110px;">
                                测试项目报告种类：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                数量：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 110px;">
                                单位：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                        </tr>
        <tr bgcolor="#f4faff">
            <td style="width: 90px; text-align: left">
                                制造厂商：</td>
            <td style="text-align: left">
                                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
            <td style="width: 110px; text-align: left">
                收到日期：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox11" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox11,'yyyy-mm-dd')"></asp:TextBox></td>
        </tr>
         <tr bgcolor="#f4faff">
           <%-- <td style="width: 110px; text-align: left">
                                保管要求：</td>
            <td style="text-align: left">
                               <asp:CheckBoxList ID="CheckBoxList2" runat="server" 
              RepeatDirection="Horizontal" Width="100%" RepeatColumns="5" RepeatLayout="Flow">
                                    <asp:ListItem>外观无损</asp:ListItem>
                                    <asp:ListItem>配件齐全</asp:ListItem>
                                    <asp:ListItem>装配还原</asp:ListItem>
                                    <asp:ListItem>包装完好</asp:ListItem>
                                </asp:CheckBoxList> 
            </td>--%>
            <td style="width: 90px; text-align: left">
                最后处置：</td>
            <td style="text-align: left" colspan="3">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
              RepeatDirection="Horizontal" Width="100%" RepeatColumns="5" RepeatLayout="Flow">
                    <asp:ListItem>退回客户</asp:ListItem>
                    <asp:ListItem>实验室封存</asp:ListItem>
                    <asp:ListItem>实验室销毁</asp:ListItem>
                </asp:CheckBoxList> 
            </td>
        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                备注：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox10" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" Visible ="false" CssClass="BnCss"
                                    Text="修改" OnClick="Button1_Click" />
                                <asp:Button ID="Button2" runat="server" Text="打印标签" Visible ="false"  onclick="Button2_Click" />
                                <asp:Button ID="Button3" runat="server" Text="生成新样品" Visible ="false"  onclick="Button3_Click" />
                                <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="流转单" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
    

      <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left">配件信息
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                 
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"  DataKeyNames="id" 
                                            OnRowDeleting="GridView1_RowDeleting" Style="font-size: 9pt" Width="98%">
                                       
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="点击明细"> 
                                                            <ItemTemplate>    
                                    <span  style ="cursor :hand;color :Blue ;" onclick ="window.showModalDialog('YanPinSee.aspx?id=<%#Eval("id") %>','test','dialogWidth=800px;DialogHeight=350px;status:no;help:no;resizable:yes; edge:raised;')"><asp:Label id="seeLB" runat="server" Text='<%# Eval("picihaobianhao") %>'></asp:Label></span>                     
                                                            </ItemTemplate>
                                                  </asp:TemplateField>
                                                
                                                <asp:BoundField DataField="yaopinname" HeaderText="样品名称" />
                                                <asp:BoundField DataField="guige" HeaderText="型号" />
                                                <asp:BoundField DataField="jiliang" HeaderText="数量" />
                                                <asp:BoundField DataField="danwei" HeaderText="单位" />
                                                <asp:BoundField DataField="shengchanchangjia" HeaderText="生产厂家" />
                                                <asp:BoundField DataField="goumaidate" DataFormatString="{0:d}" HeaderText="接收日期" />
                                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                                <asp:CommandField HeaderText="删除" ShowDeleteButton="false" Visible ="false" >
                                                    <ItemStyle ForeColor="Blue" />
                                                </asp:CommandField>
                                            </Columns>
                                            <HeaderStyle CssClass="Admin_Table_Title " />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                            </EmptyDataTemplate>
                                          
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>


                         <tr bgcolor="#f4faff">
                            <td style="text-align: left">流转信息
                               
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"  DataKeyNames="id" 
                                           Style="font-size: 9pt" Width="98%"   OnRowCancelingEdit="GridView2_RowCancelingEdit"  OnRowEditing="GridView2_RowEditing"
                            OnRowUpdating="GridView2_RowUpdating">
                                       
                                         <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sampleid" HeaderText="样品编号"  ReadOnly ="true" />
                                                <asp:BoundField DataField="yangpinname" HeaderText="样品名称"  ReadOnly ="true"/>
                                                <asp:BoundField DataField="name" HeaderText="领用人"  ReadOnly ="true"/>
                                                <asp:BoundField DataField="time" DataFormatString="{0:d}" HeaderText="时间"  ReadOnly ="true"/>
                                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                                <asp:BoundField DataField="state" HeaderText="类别"  ReadOnly ="true"/>
                                                 <asp:BoundField DataField="pub_field2" HeaderText="库房" ReadOnly ="true" />
                                                  <asp:BoundField DataField="pub_field3" HeaderText="封存编号" ReadOnly ="true" />
                                                  <asp:CommandField HeaderText="编辑备注"   ShowEditButton ="true"  />
                                            </Columns>
                                            <HeaderStyle CssClass="Admin_Table_Title " />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                            </EmptyDataTemplate>
                                          
                                        </asp:GridView>
                                  
                            </td>
                        </tr>

                         <tr bgcolor="#f4faff">
                            <td style="text-align: left">快递信息
                               
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"  DataKeyNames="id" 
                                           Style="font-size: 9pt" Width="98%">
                                       
                                         <Columns>
                                              
                                                <asp:BoundField DataField="sampleid" HeaderText="样品编号" />
                                                <asp:BoundField DataField="kuaidiid" HeaderText="快递编号" />
                                                <asp:BoundField DataField="fillname" HeaderText="填写人" />
                                                <asp:BoundField DataField="filltime" DataFormatString="{0:d}" HeaderText="填写日期" />
                                                   <asp:HyperLinkField HeaderText="查看" Text="查看" Target="_blank" DataNavigateUrlFormatString="~/case/KuaiDiSee.aspx?id=={0}"
                                        DataNavigateUrlFields="bianhao" />
                                            </Columns>
                                            <HeaderStyle CssClass="Admin_Table_Title " />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                                            </EmptyDataTemplate>
                                          
                                        </asp:GridView>
                                  
                            </td>
                        </tr>

                    </table>

       </div> 

</form>
</body>
</html>

