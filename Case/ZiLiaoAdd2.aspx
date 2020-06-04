<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZiLiaoAdd2.aspx.cs" Inherits="Case_ZiLiaoAdd2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>资料清单</title>
   <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript">


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
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
   
       
                  <div style ="text-align :center ;">    资料清单<asp:Label ID="Label2" runat="server" Text=""  ForeColor ="Red" ></asp:Label></div>  
                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                                 DataKeyNames="Id" Width ="100%" OnRowDataBound="GridView1_RowDataBound"
                                OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating" >
                                <Columns>

                                <asp:BoundField DataField="beizhu" HeaderText="顺序"   />
                                    <asp:BoundField DataField="name" HeaderText="资料类型"   />

                                    <asp:BoundField DataField="leibie1" HeaderText="检测类别" Visible ="false"/>

                                     <asp:BoundField DataField="ybyq" HeaderText="一般要求" />
                                      <asp:BoundField DataField="syqk" HeaderText="适应情况" />
                                       
                                        <asp:BoundField DataField="jingji" HeaderText="紧急"   />

                                   

                                   
                                    <asp:TemplateField HeaderText="上传"  >
                                        <ItemTemplate>
                                            <span style="cursor: hand; color: Blue;" onclick="window.open('../Case/ZiLiao.aspx?id=<%#Eval("id") %>&&baojiaid=')">
                                                <asp:Label ID="Label1" runat="server" Text="上传"></asp:Label></span> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="SingleClick" Text="SingleClick" Visible="False" />
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            <asp:TextBox ID="Id" runat="server" Text='<%# Eval("Id") %>' Visible="false" Width="30px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="备注" >
                                        <ItemTemplate>
                                            <asp:Label ID="timeLabel" runat="server" Text='<%# Eval("renwuname") %>'></asp:Label>
                                             <asp:TextBox ID="timeLabel3" runat="server" Text='<%# Eval("renwuname") %>' Visible="false"
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                   
                                 <%--   <asp:TemplateField HeaderText="首次提供">
                                        <ItemTemplate>
                                            <asp:Label ID="shebei1Label" runat="server" Text='<%# Eval("gongchengshi") %>'></asp:Label>
                                             <asp:TextBox ID="shebei1Label3" runat="server" Text='<%# Eval("gongchengshi") %>' Visible="false"
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="首次提供" ItemStyle-BackColor ="#FFFFCC" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei1Label" runat="server" Text='<%# Eval("gongchengshi") %>'></asp:Label>
                                                        <asp:DropDownList ID="shebei1Label3" runat="server" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem></asp:ListItem>
                                                              <asp:ListItem>否</asp:ListItem>

                                                            <asp:ListItem>是</asp:ListItem>
                                                        
                                                            
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                   <%-- <asp:TemplateField HeaderText="需要修正">
                                        <ItemTemplate>
                                            <asp:Label ID="shebei2Label" runat="server" Text='<%# Eval("kaishiriqi") %>'></asp:Label>
                                            <asp:TextBox ID="shebei2" runat="server" Text='<%# Eval("kaishiriqi") %>' Visible="false"
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                     <asp:TemplateField HeaderText="需要补正"  ItemStyle-BackColor ="AliceBlue" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei2Label" runat="server" Text='<%# Eval("kaishiriqi") %>'></asp:Label>
                                                        <asp:DropDownList ID="shebei2" runat="server" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>是</asp:ListItem>
                                                        
                                                            
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                    <asp:TemplateField HeaderText="补正原因"  ItemStyle-BackColor ="AliceBlue" >
                                        <ItemTemplate>
                                            <asp:Label ID="shebei3Label" runat="server" Text='<%# Eval("jihuajieshu") %>'></asp:Label>
                                            <asp:TextBox ID="shebei3" runat="server" Text='<%# Eval("jihuajieshu") %>' Visible="false"
                                               Width="100px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="首提日期"  ItemStyle-BackColor ="AliceBlue" >
                                        <ItemTemplate>
                                            <asp:Label ID="beizhu4Label" runat="server" Text='<%# Eval("beizhu4") %>'></asp:Label>
                                            <asp:TextBox ID="beizhu4" runat="server" Text='<%# Eval("beizhu4") %>' Visible="false"
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修正情况" ItemStyle-BackColor ="AntiqueWhite"  >
                                        <ItemTemplate>
                                            <asp:Label ID="shebei4Label" runat="server" Text='<%# Eval("shijijieshu") %>'></asp:Label>
                                            <asp:TextBox ID="shebei4" runat="server" Text='<%# Eval("shijijieshu") %>' Visible="false"
                                             Width="100px"   ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%--  <asp:TemplateField HeaderText="修正情况">
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei4Label" runat="server" Text='<%# Eval("shijijieshu") %>'></asp:Label>
                                                        <asp:DropDownList ID="shebei4" runat="server" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>是</asp:ListItem>
                                                        
                                                            
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="修正日期" ItemStyle-BackColor ="AntiqueWhite" >
                                        <ItemTemplate>
                                            <asp:Label ID="shebei5Label" runat="server" Text='<%# Eval("zhuangtai") %>'></asp:Label>
                                            <asp:TextBox ID="shebei5Label5" runat="server" Text='<%# Eval("zhuangtai") %>' Visible="false"
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="工程确认">
                                        <ItemTemplate>
                                            <asp:Label ID="shebei6Label" runat="server" Text='<%# Eval("beizhu") %>'></asp:Label>
                                            <asp:TextBox ID="shebei6" runat="server" Text='<%# Eval("beizhu") %>' Visible="false"
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                      <asp:TemplateField HeaderText="工程确认" ItemStyle-BackColor ="#99CCFF"  >  
                                                    <ItemTemplate>
                                                        <asp:Label ID="shebei6Label" runat="server" Text='<%# Eval("beizhu") %>'></asp:Label>
                                                        <asp:DropDownList ID="shebei6" runat="server" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem></asp:ListItem>

                                                              <asp:ListItem>否</asp:ListItem>
                                                            <asp:ListItem>是</asp:ListItem>
                                                        
                                                            
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                   <asp:TemplateField HeaderText="确认情况" ItemStyle-BackColor ="#99CCFF"  >
                                        <ItemTemplate>
                                            <asp:Label ID="timeLabelbb" runat="server" Text='<%# Eval("beizhu1") %>'></asp:Label>
                                             <asp:TextBox ID="timeLabel3bb" runat="server" Text='<%# Eval("beizhu1") %>' Visible="false"
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="确认日期"  ItemStyle-BackColor ="#99CCFF" >
                                        <ItemTemplate>
                                            <asp:Label ID="paixu1" runat="server" Text='<%# Eval("beizhu3") %>'></asp:Label>
                                           <asp:TextBox ID="paixutext" runat="server" Text='<%# Eval("beizhu3") %>' Visible="false"
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="步骤顺序" Visible ="false" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="paixuz" runat="server" Text='<%# Eval("beizhu2") %>'></asp:Label>
                                                        <asp:DropDownList ID="paixuzz" runat="server" Visible="false" AutoPostBack="true">
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
             
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="BnCss"
                        Text="增加资料" />
                    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="打印补充资料" />
                    <asp:Button ID="Button2" runat="server" Text="DO" />
                    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="初审资料"  Visible ="false" />
                    <asp:Button ID="Button5" runat="server" Text="确定资料" onclick="Button5_Click" Visible ="false" />
             
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB %>"
                        DeleteCommand="DELETE FROM [anjianxinxi] WHERE [id] = @id" InsertCommand="INSERT INTO [anjianxinxi] ([xiangmuid], [kehuname], [renwuname], [gongchengshi], [kaishiriqi], [jihuajieshu], [shijijieshu], [zhuangtai], [beizhu], [tianxieren], [tianxietime], [beizhu1], [beizhu2], [beizhu3], [beizhu4]) VALUES (@xiangmuid, @kehuname, @renwuname, @gongchengshi, @kaishiriqi, @jihuajieshu, @shijijieshu, @zhuangtai, @beizhu, @tianxieren, @tianxietime, @beizhu1, @beizhu2, @beizhu3, @beizhu4)"
                        SelectCommand="SELECT ZiLiaoType.beizhu, ZiLiaoType.name,ZiLiaoType.leibie1,ZiLiaoType.ybyq,ZiLiaoType.syqk,ZiLiaoType.bumen,ZiLiaoType.jingji, AnJianXinXi.id, AnJianXinXi.xiangmuid, AnJianXinXi.kehuname, AnJianXinXi.renwuname, AnJianXinXi.gongchengshi, AnJianXinXi.kaishiriqi, AnJianXinXi.jihuajieshu, AnJianXinXi.shijijieshu, AnJianXinXi.zhuangtai, AnJianXinXi.beizhu, AnJianXinXi.tianxieren, AnJianXinXi.tianxietime, AnJianXinXi.beizhu1, AnJianXinXi.beizhu2, AnJianXinXi.beizhu3, AnJianXinXi.beizhu4 FROM ZiLiaoType LEFT OUTER JOIN AnJianXinXi ON ZiLiaoType.id = anjianxinxi.leibieid WHERE (AnJianXinXi.xiangmuid = @xiangmuid) and anjianxinxi.leibie='资料' order by convert(int,beizhu2) asc"
                        UpdateCommand="UPDATE [anjianxinxi] SET   [renwuname] = @renwuname, [gongchengshi] = @gongchengshi, [kaishiriqi] = @kaishiriqi, [jihuajieshu] = @jihuajieshu, [shijijieshu] = @shijijieshu, [zhuangtai] = @zhuangtai, [beizhu] = @beizhu,[beizhu4] = @beizhu4,[beizhu3] = @beizhu3 ,[beizhu2] = @beizhu2,[beizhu1] = @beizhu1    WHERE [id] = @id">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="xiangmuid" QueryStringField="xiangmuid" Type="String" />
                        </SelectParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="renwuname" Type="String" />
                            <asp:Parameter Name="gongchengshi" Type="String" />
                            <asp:Parameter Name="kaishiriqi" Type="String" />
                            <asp:Parameter Name="jihuajieshu" Type="String" />
                            <asp:Parameter Name="shijijieshu" Type="String" />
                            <asp:Parameter Name="zhuangtai" Type="String" />
                            <asp:Parameter Name="beizhu" Type="String" />
                             <asp:Parameter Name="beizhu2" Type="String" />
                                  <asp:Parameter Name="beizhu1" Type="String" />
                            <asp:Parameter Name="beizhu4" Type="String" />
                            <asp:Parameter Name="beizhu3" Type="String" />
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
              
   
    </form>
</body>
</html>
