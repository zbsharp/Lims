<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Body.aspx.cs" Inherits="Body" %>

<%@ Register Assembly="EeekSoft.Web.PopupWin" Namespace="EeekSoft.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_All">
        <table cellpadding="3" cellspacing="1" class="Admin_Table">
            <thead>
                <tr class="Admin_Table_Title">
                    <th colspan="8">
                        <%--<script charset="utf-8" type="text/javascript" id="TOOL_115_COM_JS" src="../JavaScript/Weather.js"></script>--%>
                    </th>
                </tr>
                <tr>
                <td align ="center" style ="color :Red ;" >相关内部系统通知</td>
                </tr>
                <tr><td>
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table" DataKeyNames="id" 
                       Style="font-size: 9pt" Width="100%" >
                     
                       <Columns>
                           <asp:TemplateField HeaderText="序号">
                               <ItemTemplate>
                                   <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                       CommandName="BussinessNeeds" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                               </ItemTemplate>
                               <ItemStyle ForeColor="Green" />
                           </asp:TemplateField>
                           <asp:BoundField DataField="name" HeaderText="录入人" />
                           <asp:BoundField DataField="time" HeaderText="录入时间" DataFormatString="{0:d}" />
                           <asp:BoundField DataField="notice" HeaderText="信息" >
                               <ItemStyle Width="55%" />
                           </asp:BoundField>
                          
                           
                       </Columns>
                     
                       <EmptyDataTemplate>
                           <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="暂时没有数据"></asp:Label>
                       </EmptyDataTemplate>
                      <HeaderStyle CssClass="Admin_Table_Title " />
                   </asp:GridView>
                
                
                </td></tr>
            </thead>
            
        </table>
    </div>

      <cc1:PopupWin ID="PopupWin1"  ShowAfter ="0"  DragDrop ="true"  Width ="200px" Height ="120px" TextColor="Red" WindowScroll="true"  ActionType ="OpenLink"     runat="server"  ColorStyle="Custom" 
            DockMode="BottomRight"     HideAfter ="500000"   Message ="前往查看将要超期任务列表"  Link="../Case/YiFenYiShou4(WillChaoQi).aspx"  LinkTarget="main" 
             BackColor="Red"     Title="请关注您将超期的任务"  />

    </form>
</body>
</html>





