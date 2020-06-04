<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CeShiFeiGc.aspx.cs" Inherits="Case_CeShiFeiGc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工程师上报费用</title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
    <script type="text/javascript" src="../js/calendar.js"></script>

</head>
<body>
    <form id="form1" runat="server"> <asp:ScriptManager ID="ScriptManager1" runat="server"  EnableScriptGlobalization="false" EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div>
     <div class="Body_Title">
        业务管理 》》录入上报费用</div>


     <table align="center"  class="Admin_Table"
                        width="100%">
      <tr  >
         <td align="left" style="width: 90px" >
             检测类别：</td>
          <td align="left">
              <asp:DropDownList ID="DropDownList1"    runat="server" Width="151px">
                <asp:ListItem>安全检测费</asp:ListItem>
                <asp:ListItem>电磁兼容检测费</asp:ListItem>
                <asp:ListItem>电磁兼容转报告核查</asp:ListItem>
                <asp:ListItem>随机元器件检测</asp:ListItem>
                <asp:ListItem>性能检测费用</asp:ListItem>
                <asp:ListItem>其他费用</asp:ListItem>
              </asp:DropDownList></td>
          <td align="left" style="width: 90px">
              检测标准：</td>
          <td align="left">

          <asp:DropDownList ID="DropDownList4" onselectedindexchanged="DropDownList4_SelectedIndexChanged" AutoPostBack ="true" runat="server">
                </asp:DropDownList>



              
          </td>
    </tr>
        <tr  >
            <td align="left" style="width: 90px">
                检测项目：</td>
            <td align="left">
                <asp:DropDownList ID="DropDownList3" runat="server">
              </asp:DropDownList>
            </td>
            <td align="left" style="width: 90px">
                报告编号：</td>
            <td align="left">
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr  >
            <td align="left" style="width: 90px">
                每项检测次数：</td>
            <td align="left">
                <asp:TextBox ID="TextBox3" runat="server" Text ="1"></asp:TextBox></td>
            <td align="left" style="width: 90px">
                每项检测收费：</td>
            <td align="left">
                <asp:TextBox ID="TextBox4" runat="server" Text ="10"></asp:TextBox></td>
        </tr>
        <tr  >
            <td align="left" style="width: 90px">
                日期：</td>
            <td align="left">
                <asp:TextBox ID="TextBox5" runat="server" onclick="new Calendar().show(this.form.TextBox5);" ></asp:TextBox></td>
            <td align="left" style="width: 90px">
                填写人：</td>
            <td align="left">
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
        </tr>
        <tr  >
            <td align="left" style="width: 90px">
                备注：</td>
            <td align="left">
                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
            </td>
            <td align="left" style="width: 90px">
                要求收费：</td>
            <td align="left">
                <asp:TextBox ID="TextBox10" Text ="0" runat="server"></asp:TextBox>
                <asp:Button ID="Button3"
                    runat="server" Text="修改发改委要求费用" onclick="Button3_Click" />
            </td>
        </tr>
        <tr  >
            <td align="left" style="width: 90px">
                打印是否显示发改委要求费用</td>
            <td align="left">
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem>否</asp:ListItem>
                    <asp:ListItem>是</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" style="width: 90px">
                是否为上报项目：</td>
            <td align="left">
                <asp:DropDownList ID="DropDownList5" runat="server">
                    <asp:ListItem>是</asp:ListItem>
                    <asp:ListItem>否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr  >
            <td align="left" colspan="4" style="text-align: center">
                <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="BnCss"
                    OnClick="Button1_Click" Text="保 存" />
              
                <asp:Button ID="Button2" runat="server" Text="打印" onclick="Button2_Click" />
               
              
            </td>
        </tr>

           <tr  >
            <td  colspan="4" style="text-align: center">
            
                 
            
                <asp:GridView ID="GridView1" Width ="100%" ShowFooter ="true"  OnRowDataBound="GridView1_RowDataBound"  
                 runat="server" CssClass="Admin_Table" AutoGenerateColumns ="false" DataKeyNames ="id"
                  OnRowDeleting="GridView1_RowDeleting" 
                  OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand"  OnRowEditing="GridView1_RowEditing"
                            OnRowUpdating="GridView1_RowUpdating"
                  >
                      <Columns>
                                                          
                                                          <asp:TemplateField HeaderText="序号" >
                                                                <ItemTemplate  >
                                                                     <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("00") %>' CommandArgument='<%# Eval("id") %>' CommandName="chakan"  ForeColor="Green"></asp:LinkButton>
                                                                    
                                                                </ItemTemplate>
                                                                 <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="type" HeaderText="类型" />
                                                            <asp:BoundField DataField="xiangmu" HeaderText="检测项目" />
                                                             <asp:BoundField DataField="beizhu4" HeaderText="检测标准" />
                                                              <asp:BoundField DataField="beizhu5" HeaderText="报告编号" />
                                                            <asp:BoundField DataField="shuliang" HeaderText="每项检测次数" />
                                                            <asp:BoundField DataField="feiyong" HeaderText="每项检测收费" />
                                                             <asp:BoundField DataField="xiaoji" ReadOnly ="true"  HeaderText="检测收费小计" />
                                                              <asp:BoundField DataField="beizhu3" HeaderText="备注" />
                                                            <asp:BoundField DataField="filltime" HeaderText="填写时间"  ReadOnly ="true" />
                                                            <asp:BoundField DataField="fillname" HeaderText="填写人" ReadOnly ="true"/>
                                                            <asp:BoundField DataField="beizhu2" ReadOnly ="true"  HeaderText="是否核算" />
                                                            <asp:BoundField DataField="fwz" ReadOnly ="true"  HeaderText="是否作废" />
                                                        <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" CausesValidation="False" />
                                                                <asp:TemplateField HeaderText="作废"  >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" Text="作废" CommandArgument='<%# Eval("id") %>'
                            CommandName="xiada"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                                                          
                                                          <asp:BoundField DataField="sb" ReadOnly ="true"  HeaderText="是否上报" />


                                                        </Columns>

                                                        <HeaderStyle CssClass="Admin_Table_Title " />    

                </asp:GridView>
            
             </td>
        </tr>
        <tr>
        
        <td colspan="4" style="text-align: left">
        
        说明：<br />1，请工程师按照收费标准和相关要求，正确填报拟上报给认证机构的检测费。

                            其他服务费，应当在报价单中约定或者把相关凭证提供给前台，不可在此填报。<br />

                     2，填报费用应当在签发报告之前完成，以便随检测结果提供给认证机构和客户。

                            之后的任何修改，应当知会客服部采取补救措施。<br />
                            3，请到工程管理的上报类别处增加相应的检测标准。<br />
                            4，请到工程管理的上报类别处增加相应的检测项目。<br />
                            5，要求收费这一项可手工填写或者修改按照发改委的要求针对整个清单的总共收费。<br />
                             6，费用被核算后将不能删改，可按作废处理或取消核算费用或者通过系统管理人员授权处理。

        
        </td>
        </tr>

    </table>
    </div>
    </form>
</body>
</html>
