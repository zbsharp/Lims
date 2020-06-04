<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YangPin_JiechuQingTui.aspx.cs" Inherits="YangPin_YangPin_JiechuQingTui" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 
	样品封存
</title>  <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js">
<script language="javascript" src="popcalendar.js"></script>

         


    <style type="text/css">
        .style1
        {
            width: 199px;
        }
    </style>

         


</head>
<body>
    <form name="form1"  runat="server"  id="form1">
       <div class="Body_Title">
       样品管理 》》样品封存</div>


<div>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

   
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; " class="style1">
                                请扫描或者输入完整样品编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                <asp:Button ID="Button2" runat="server" Text="确认" onclick="Button2_Click" />(扫描条码前请确认光标在左侧文本框且输入法处于英文状态)</td>
                           
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; " class="style1">
                                &nbsp;</td>
                            <td style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                            </td>
                         
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: center; " colspan="4">
                                &nbsp;</td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: center; " colspan="4">
                                  <asp:GridView ID="GridView2" runat="server"  DataKeyNames="id" OnRowDeleting="GridView2_RowDeleting" AutoGenerateColumns="False" CssClass="Admin_Table"  OnRowDataBound="GridView1_RowDataBound"
                                             Width="100%"  OnRowCancelingEdit="GridView2_RowCancelingEdit"  OnRowEditing="GridView2_RowEditing"
                            OnRowUpdating="GridView2_RowUpdating">
                                           
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sid" HeaderText="样品编号"  ReadOnly ="true" />
                                           <asp:BoundField DataField="spname" HeaderText="样品名称"  ReadOnly ="true" />
                                                <asp:BoundField DataField="name1" HeaderText="领用人" />
                                                <asp:BoundField DataField="beizhu1" DataFormatString="{0:d}" HeaderText="借出时间" />
                                                <asp:BoundField DataField="state" HeaderText="样品状态" ReadOnly ="true"/>
                                                   <asp:CommandField HeaderText="取消" ShowDeleteButton="True"   ShowEditButton ="false"  />
                                                 <asp:TemplateField HeaderText="查看">
                                    <ItemTemplate>
                                       <span style="cursor: hand;
                                                color: Blue;" onclick="window.open('YangPinSee1.aspx?yangpinid=<%#Eval("sid") %>')">
                                                <asp:Label ID="Label7" runat="server" Text="查看样品"></asp:Label></span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:BoundField DataField="sq" HeaderText="申请编号" />
                                            </Columns>
                                       <HeaderStyle CssClass="Admin_Table_Title " />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                                            </EmptyDataTemplate>
                                          
                                        </asp:GridView>
                                        
                                        <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                                    Text="确认封存" OnClick="Button1_Click" />
                                <asp:TextBox ID="TextBox6" runat="server" ToolTip="状态" Visible="False" Width="3px"></asp:TextBox>
                                <asp:DropDownList ID="DropDownList1"  runat="server" 
                                    onselectedindexchanged="DropDownList1_SelectedIndexChanged"  Enabled="false"  >
                                  <asp:ListItem>封存</asp:ListItem>
                                  
                                   <asp:ListItem>清退</asp:ListItem>                               
                                    <asp:ListItem>销毁</asp:ListItem>
                                    
                                   
                                </asp:DropDownList>
                                <asp:Label ID="Label3" runat="server" Text="Label" Visible ="false" ></asp:Label>
                                备注:<asp:TextBox ID="TextBox7" runat="server" Width="17%"></asp:TextBox>


                                <asp:TextBox ID="TextBox3" runat="server"  Visible ="false" ></asp:TextBox><asp:TextBox ID="TextBox4" Visible ="false"  runat="server" 
                                    onclick="popUpCalendar(this,document.forms[0].TextBox4,'yyyy-mm-dd')"></asp:TextBox></td>
                        </tr>
        <tr bgcolor="#f4faff">
            <td colspan="4" style="text-align: left">最近封存列表
                       
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"  OnRowDataBound="GridView1_RowDataBound"
                                             Width="100%">
                                           
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sampleid" HeaderText="样品编号" />
                                                <asp:BoundField DataField="yangpinname" HeaderText="样品名称"  Visible ="false" />
                                                   <asp:BoundField DataField="model" HeaderText="样品型号" />
                                                 <asp:BoundField DataField="kh" HeaderText="制造商" />
                                               
                                                <asp:BoundField DataField="pub_field3" HeaderText="封存编号"  />
                                                <asp:BoundField DataField="filltime"  HeaderText="封存时间" />
                                                <asp:BoundField DataField="remark" HeaderText="备注" />
                                                 <asp:BoundField DataField="sq" HeaderText="申请编号" />
                                                <asp:BoundField DataField="state" HeaderText="样品状态" />
                                                   <asp:TemplateField HeaderText="查看">
                                    <ItemTemplate>
                                       <span style="cursor: hand;
                                                color: Blue;" onclick="window.open('YangPinSee1.aspx?yangpinid=<%#Eval("sampleid") %>')">
                                                <asp:Label ID="Label7" runat="server" Text="查看样品"></asp:Label></span>

                                                
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            </Columns>
                                       <HeaderStyle CssClass="Admin_Table_Title " />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                                            </EmptyDataTemplate>
                                          
                                        </asp:GridView><asp:TextBox ID="TextBox5" runat="server" Width="27%" Visible="False"></asp:TextBox>
                                   
            </td>
        </tr>
                    </table>
    


       </div> 

</form>
</body>
</html>
