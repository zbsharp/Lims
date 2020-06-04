<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xiangmulist.aspx.cs" Inherits="Case_xiangmulist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/Jquery.js"></script>
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
</head>
<body>
        <form id="form2" runat="server">
       
    <div class="Body_Title">
        案件管理 》》查看任务单中的项目</div>
   
        
        
                        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table"
                               AutoGenerateColumns="False" DataKeyNames="id"   >
                                                      
                                           <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'
                                            CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Green" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ceshiname" HeaderText="测试项目" />
                                <asp:BoundField DataField="biaozhun" HeaderText="标准" />
                                <asp:BoundField DataField="neirong" HeaderText="内容" />
                                <asp:BoundField DataField="yp" HeaderText="样品" />
                                 <asp:BoundField DataField="zhouqi" HeaderText="周期" />
                                <asp:BoundField DataField="feiyong" HeaderText="费用" />
                                <asp:BoundField DataField="zhekou" HeaderText="折扣" />
                                <asp:BoundField DataField="shuliang" HeaderText="数量" />
                                <asp:BoundField DataField="beizhu" HeaderText="备注" />
                                <asp:BoundField DataField="total" HeaderText="小计" ReadOnly ="true"   DataFormatString ="{0:f2}" />
                                 <asp:HyperLinkField DataTextField="id" HeaderText="附件" Target="_blank" DataNavigateUrlFormatString="~/Case/UploadFile.aspx?id={0}"
                DataNavigateUrlFields="id" />
                            </Columns>
                               <EmptyDataTemplate>
                                   <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时未有数据"></asp:Label>
                               </EmptyDataTemplate>
                                                      <HeaderStyle CssClass="Admin_Table_Title " />    
                                                    </asp:GridView>
             
               
              
                     
 
    </form>
</body>
</html>


