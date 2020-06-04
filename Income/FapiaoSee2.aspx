<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FapiaoSee2.aspx.cs" Inherits="Income_FapiaoSee2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title> 发票信息
	
</title><link href="../css.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>

    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>

    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>

       
       <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
         
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

<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false" EnableScriptLocalization="false">
                    </asp:ScriptManager>

      <div class="Body_Title">
            业务管理 》》发票信息</div>
    
    <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
        <tr bgcolor="#f4faff">
            <td colspan="4" style="text-align: left">
                申请编号：<asp:Label ID="Label3" runat="server"></asp:Label></td>
        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                发票编号：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 90px;">
                                发票金额：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>
                        
        <tr bgcolor="#f4faff">
            <td style="width: 90px; text-align: left">
                状态：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
            <td style="width: 90px; text-align: left">
                业务员：</td>
            <td style="text-align: left">
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                抬头：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox7" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                备注：</td>
                            <td colspan="3" style="text-align: left">
                                <asp:TextBox ID="TextBox8" runat="server" Width="90%"></asp:TextBox></td>
                        </tr>
                        <tr bgcolor="#f4faff">
                            <td colspan="4" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CausesValidation="False"  Visible ="false" CssClass="BnCss"
                                    Text="修 改" OnClick="Button1_Click" />
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


                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                领票人：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                            <td style="text-align: left; width: 90px;">
                                领票时间：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="TextBox4" runat="server" onclick="popUpCalendar(this,document.forms[0].TextBox4,'yyyy-mm-dd')"></asp:TextBox></td>
                        </tr>


                        <tr bgcolor="#f4faff">
                            <td style="text-align: left; width: 90px;">
                                领票备注：</td>
                            <td style="text-align: left" >
                                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                            </td>

                             <td style="text-align: left; width: 90px;">
                                任务号：</td>
                            <td style="text-align: left" >
                                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                            </td>


                        </tr>


                        <tr bgcolor="#f4faff">
                            <td style="text-align:center;" colspan="4">
                                <asp:Button ID="Button2" runat="server" Text="领票" Visible ="false"  onclick="Button2_Click" /></td>
                        </tr>


                    </table>
    
     <fieldset>
        <legend style="color: Red">报告样品信息</legend> 
        
         <table align="center" border="0" cellpadding="3" cellspacing="1" style="background-color: #b9d8f3"
                        width="100%">
           <tr bgcolor="#f4faff" style ="display :none;">
                            <td style="text-align: left; width: 90px;">
                                流水号或付款人：</td>
                            <td style="text-align: left" >
                               
                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                               
                            </td>

                             <td style="text-align: left; " >
                                 <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="查询" />
                            </td>
                            <td style="text-align: left; " >
                                <asp:Button ID="Button4" runat="server" Text="确定" onclick="Button4_Click" />

                                <asp:Button ID="Button5" runat="server" Text="取消" onclick="Button5_Click" />

                            </td>
                        </tr>

                           <tr bgcolor="#f4faff">
                           <td style="text-align:center; " colspan="4">报告信息
                                 <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" 
                      >
                        <Columns>
                            <asp:BoundField DataField="tjid" HeaderText="任务号"   Visible ="false" />
                            <asp:BoundField DataField="baogaoid" HeaderText="报告号" />
                            <asp:BoundField DataField="leibie" HeaderText="类别" Visible="false" />
                            <asp:BoundField DataField="dayintime" HeaderText="安排打印" />
                            <asp:BoundField DataField="wanchengtime" HeaderText="实际完成" />
                            <asp:BoundField DataField="state" DataFormatString="{0:d}" HeaderText="状态" />                     
                            <asp:BoundField DataField="statebumen1" DataFormatString="{0:d}" HeaderText="签字" />
                            <asp:BoundField DataField="statebumen2" DataFormatString="{0:d}" HeaderText="审核" />
                            <asp:BoundField DataField="pizhunby" DataFormatString="{0:d}" HeaderText="批准" />
                            <asp:BoundField DataField="pizhundate" DataFormatString="{0:d}" HeaderText="批准日期" />
                            <asp:BoundField DataField="fafangdate" DataFormatString="{0:d}" HeaderText="发放日期" />
                            <asp:BoundField DataField="danganid" DataFormatString="{0:d}" HeaderText="归档编号" />
                            <asp:BoundField DataField="dangandate" DataFormatString="{0:d}" HeaderText="归档日期" />
                            <asp:BoundField DataField="dengjiby" HeaderText="申请编号"     />
                            <asp:BoundField DataField="kuaidihao" HeaderText="快递编号"     />
                             <asp:HyperLinkField HeaderText="明细"  DataTextField ="tjid" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=0"
                    DataNavigateUrlFields="tjid" />
                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>                   
                    </td>
                        </tr>

                          <tr bgcolor="#f4faff">
                           <td style="text-align:center; " colspan="4">样品信息
                                 <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="id" CssClass="Admin_Table" 
                      >
                        <Columns>
                               <asp:BoundField DataField="sampleid" HeaderText="样品编号" />

                                  <asp:BoundField DataField="anjianid" HeaderText="任务编号"  />
                                  <asp:BoundField DataField="name" HeaderText="样品名称" />
                                <asp:BoundField DataField="model" HeaderText="型号" />
                                 <asp:BoundField DataField="position" HeaderText="制造厂商" />
                                  
                                <asp:BoundField DataField="receivetime" DataFormatString="{0:d}" HeaderText="收到日期" />
                                 <asp:BoundField DataField="kf" HeaderText="项目经理"  />
                              <asp:BoundField DataField="gc" HeaderText="工程师"  />
                                
                               
                               
                                <asp:BoundField DataField="state" HeaderText="状态" />
                                 <asp:BoundField DataField="dd" HeaderText="借出人"  />

                          
                               
                                <asp:HyperLinkField HeaderText="附件" Text="附件" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?baojiaid={0}&amp;&amp;id={1}"
                                    DataNavigateUrlFields="baojiaid,kehuid" />


                                     <asp:HyperLinkField HeaderText="配件" Text="配件" Target="_blank" DataNavigateUrlFormatString="~/YangPin/YanPinManage.aspx?baojiaid={0}&&kehuid={1}&&sampleid={2}&&bianhao={3}"
                                    DataNavigateUrlFields="baojiaid,kehuid,sampleid,bianhao" />


                                     <asp:HyperLinkField HeaderText="流转" Text="流转" Target="_blank" DataNavigateUrlFormatString="~/Print/YangPinLiuZhuan.aspx?baojiaid={0}&&kehuid={1}&&sampleid={2}&&bianhao={3}"
                                    DataNavigateUrlFields="baojiaid,kehuid,sampleid,bianhao" />

                                     <asp:HyperLinkField HeaderText="任务号"  DataTextField ="anjianid" Target="_blank" DataNavigateUrlFormatString="~/Case/Tasksee.aspx?tijiaobianhao={0}&&chakan=1"
                    DataNavigateUrlFields="bianhao" />

                        </Columns>
                        <HeaderStyle CssClass="Admin_Table_Title " />
                    </asp:GridView>                   
                    </td>
                        </tr>



        
        
        </fieldset> 

       </div> 

</form>
       
        
        
        
        
        
        </body>
</html>

