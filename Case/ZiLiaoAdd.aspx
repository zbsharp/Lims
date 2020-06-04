<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZiLiaoAdd.aspx.cs"  Inherits="Case_ZiLiaoAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>资料清单</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />
  
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
       <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>



        <script type ="text/javascript">


            var currentRowId = 0;
            function SelectRow() {
                if (event.keyCode == 40)
                    MarkRow(currentRowId + 1);
                else if (event.keyCode == 38)
                    MarkRow(currentRowId - 1);
            }

            function MarkRow(rowId) {
                if (document.getElementById(rowId) == null)
                    return;

                if (document.getElementById(currentRowId) != null)
                    document.getElementById(currentRowId).style.backgroundColor = '#ffffff';

                currentRowId = rowId;
                document.getElementById(rowId).style.backgroundColor = '#FFE0C0';
            }
            function text() {
                document.getElementById("bnClick").click();
            }
   
    
  
    
    </script>

</head>
<body>
    <form id="form1" runat="server"> <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
            EnableScriptLocalization="false">
        </asp:ScriptManager>
                        <div id="con_one_1">
        
  





       
                    <table align="center" width="99%">
                      <thead>
        <tr class="Admin_Table_Title">
            <th colspan="4">资料清单</th>
          
        </tr>
     </thead>
                        <tr>
                            <td class="usertablerow2" colspan="4">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                                            CssClass="Admin_Table" DataKeyNames="Id" OnRowDataBound="GridView1_RowDataBound"
                                            OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating" Width="100%">
                                            <Columns>
                                                <asp:ButtonField CommandName="SingleClick" Text="SingleClick" Visible="False" />
                                                <asp:TemplateField HeaderText="Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                        <asp:TextBox ID="Id" runat="server" Text='<%# Eval("Id") %>' Visible="false" Width="30px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="任务名称">
                                                    <ItemTemplate>
                                                        <asp:Label ID="timeLabel" runat="server" Text='<%# Eval("renwuname") %>'></asp:Label>
                                                        <asp:DropDownList ID="renwuname" runat="server" Visible="false" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="工程师">
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei1Label" runat="server" Text='<%# Eval("gongchengshi") %>'></asp:Label>
                                                        <asp:DropDownList ID="gongchengshi" runat="server" Visible="false" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="计划开始">
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei2Label" runat="server" Text='<%# Eval("kaishiriqi") %>'></asp:Label>
                                                        <asp:TextBox ID="shebei2" runat="server" Text='<%# Eval("kaishiriqi") %>' Visible="false"
                                                            Width="175px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="计划结束">
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei3Label" runat="server" Text='<%# Eval("jihuajieshu") %>'></asp:Label>
                                                        <asp:TextBox ID="shebei3" runat="server" Text='<%# Eval("jihuajieshu") %>' Visible="false"
                                                            Width="175px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="实际开始">
                                                    <ItemTemplate>
                                                        <asp:Label ID="beizhu4Label" runat="server" Text='<%# Eval("beizhu4") %>'></asp:Label>
                                                        <asp:TextBox ID="beizhu4" runat="server" Text='<%# Eval("beizhu4") %>' Visible="false"
                                                            Width="175px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="实际结束">
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei4Label" runat="server" Text='<%# Eval("shijijieshu") %>'></asp:Label>
                                                        <asp:TextBox ID="shebei4" runat="server" Text='<%# Eval("shijijieshu") %>' Visible="false"
                                                            Width="175px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="任务状态">
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei5Label" runat="server" Text='<%# Eval("zhuangtai") %>'></asp:Label>
                                                        <asp:DropDownList ID="zhuangtai" runat="server" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>未开始</asp:ListItem>
                                                            <asp:ListItem>正在进行</asp:ListItem>
                                                            <asp:ListItem>已完成</asp:ListItem>
                                                            <asp:ListItem>已延期</asp:ListItem>
                                                            <asp:ListItem>等待其他人</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="具体情况">
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei6Label" runat="server" Text='<%# Eval("beizhu") %>'></asp:Label>
                                                        <asp:TextBox ID="shebei6" runat="server" Text='<%# Eval("beizhu") %>' Visible="false"
                                                            Width="175px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="步骤顺序">
                                                    <ItemTemplate>
                                                        <asp:Label ID="paixu1" runat="server" Text='<%# Eval("beizhu3") %>'></asp:Label>
                                                        <asp:DropDownList ID="paixu" runat="server" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="false" />
                                            </Columns>
                                             <HeaderStyle CssClass="Admin_Table_Title " />  
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
               
                   <tr>
                            <td class="usertablerow2" colspan="4" align="center">
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"  CssClass="BnCss"
                                    Text="增加任务" />
                                <asp:Button ID="Button2" runat="server" Text="DO" />
                            </td>
                        </tr>
                      
                    
                        <tr>
                            <td class="usertablerow2" colspan="4">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB %>"
                                    DeleteCommand="DELETE FROM [anjianxinxi] WHERE [id] = @id" InsertCommand="INSERT INTO [anjianxinxi] ([xiangmuid], [kehuname], [renwuname], [gongchengshi], [kaishiriqi], [jihuajieshu], [shijijieshu], [zhuangtai], [beizhu], [tianxieren], [tianxietime], [beizhu1], [beizhu2], [beizhu3], [beizhu4]) VALUES (@xiangmuid, @kehuname, @renwuname, @gongchengshi, @kaishiriqi, @jihuajieshu, @shijijieshu, @zhuangtai, @beizhu, @tianxieren, @tianxietime, @beizhu1, @beizhu2, @beizhu3, @beizhu4)"
                                    SelectCommand="SELECT * FROM [anjianxinxi] WHERE ([xiangmuid] = @xiangmuid) order by convert(int,beizhu3) asc "
                                    UpdateCommand="UPDATE [anjianxinxi] SET   [renwuname] = @renwuname, [gongchengshi] = @gongchengshi, [kaishiriqi] = @kaishiriqi, [jihuajieshu] = @jihuajieshu, [shijijieshu] = @shijijieshu, [zhuangtai] = @zhuangtai, [beizhu] = @beizhu,[beizhu4] = @beizhu4,[beizhu3] = @beizhu3   WHERE [id] = @id">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="xiangmuid" QueryStringField="xiangmuid" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="xiangmuid" Type="String" />
                                        <asp:Parameter Name="kehuname" Type="String" />
                                        <asp:Parameter Name="renwuname" Type="String" />
                                        <asp:Parameter Name="gongchengshi" Type="String" />
                                        <asp:Parameter Name="kaishiriqi" Type="String" />
                                        <asp:Parameter Name="jihuajieshu" Type="String" />
                                        <asp:Parameter Name="shijijieshu" Type="String" />
                                        <asp:Parameter Name="zhuangtai" Type="String" />
                                        <asp:Parameter Name="beizhu" Type="String" />
                                        <asp:Parameter Name="tianxieren" Type="String" />
                                        <asp:Parameter Name="tianxietime" Type="DateTime" />
                                        <asp:Parameter Name="beizhu1" Type="String" />
                                        <asp:Parameter Name="beizhu2" Type="String" />
                                        <asp:Parameter Name="beizhu3" Type="String" />
                                        <asp:Parameter Name="beizhu4" Type="String" />
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="xiangmuid" Type="String" />
                                        <asp:Parameter Name="kehuname" Type="String" />
                                        <asp:Parameter Name="renwuname" Type="String" />
                                        <asp:Parameter Name="gongchengshi" Type="String" />
                                        <asp:Parameter Name="kaishiriqi" Type="String" />
                                        <asp:Parameter Name="jihuajieshu" Type="String" />
                                        <asp:Parameter Name="shijijieshu" Type="String" />
                                        <asp:Parameter Name="zhuangtai" Type="String" />
                                        <asp:Parameter Name="beizhu" Type="String" />
                                        <asp:Parameter Name="tianxieren" Type="String" />
                                        <asp:Parameter Name="tianxietime" Type="DateTime" />
                                        <asp:Parameter Name="beizhu1" Type="String" />
                                        <asp:Parameter Name="beizhu2" Type="String" />
                                        <asp:Parameter Name="beizhu3" Type="String" />
                                        <asp:Parameter Name="beizhu4" Type="String" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
             
        </div> 
    </form>
</body>
</html>
