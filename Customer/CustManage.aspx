<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustManage.aspx.cs" Inherits="Customer_CustManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>

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
        <div class="div_All">
            <div class="Body_Title">
                销售管理 》》客户信息（显示个人录入的以及分配给本人的客户）
            </div>
            <div>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="customname">客户名称</asp:ListItem>
                    <asp:ListItem Value="responser">业务人员</asp:ListItem>
                    <asp:ListItem Value="contact">联系人</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Button ID="Button1"
                    runat="server" Text="查询" OnClick="Button1_Click" /><a style="margin-left: 20px; margin-right: 20px" href="CustomerAdd.aspx" target="button"><asp:Label
                        ID="Label2" runat="server" Font-Bold="true" ForeColor="Red" Text="新增客户"></asp:Label></a>
                <%--||<a style="margin-left: 20px" href="ImportCustomerList.aspx" target="button"><asp:Label
                    ID="Label3" runat="server" Font-Bold="true" ForeColor="Red" Text="导入客户"></asp:Label></a>--%>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>


            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="500"
                Width="100%">
                <cc1:TabPanel runat="server" HeaderText="标题" ID="TabPanel1">
                    <HeaderTemplate>
                        我名下的客户
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                我名下的客户

                  <div style="height: 400px; overflow: scroll;">
                      <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="Admin_Table" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False">
                          <Columns>
                              <asp:TemplateField HeaderText="序 号">
                                  <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("0000") %>'
                                          CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle ForeColor="Green" />
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="编号" SortExpression="kehuid">
                                  <ItemTemplate>
                                      <asp:Label ID="Label1" runat="server" Text='<%# Eval("kehuid") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="customname" HeaderText="客户名称" />
                              <asp:BoundField DataField="CustomType" HeaderText="客户类型" />
                              <asp:BoundField DataField="CustomSouce" HeaderText="客户来源" />
                              <asp:BoundField DataField="Responser" HeaderText="归属人" />

                              <asp:BoundField HeaderText="业务助理" />

                              <asp:BoundField DataField="filltime" HeaderText="录入时间" DataFormatString="{0:d}" />
                              <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustomerSee.aspx?kehuid={0}"
                                  HeaderText="" Text="查看客户" Target="button" />
                              <%--    <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Quotation/QuotationAdd.aspx?kehuid={0}"
                    HeaderText="" Text="客户报价" Target="_blank" />--%>

                              <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustTraceAdd.aspx?kehuid={0}"
                                  HeaderText="" Text="填写日志" Target="button" />



                              <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustomerAdd.aspx?kehuid={0}"
                                  HeaderText="" Text="申请客户" Target="button" Visible="False" />





                              <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CompanyAdd.aspx?kehuid={0}"
                                  HeaderText="" Text="增加协议" Target="button" Visible="false" />



                              <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Quotation/QuotationAdd1.aspx?kehuid={0}" Target="_blank" Text="客户报价" />



                              <asp:HyperLinkField Target="_blank" Text="EMC测试项目" DataNavigateUrlFields="kehuid" Visible="false" DataNavigateUrlFormatString="~/Customer/EMCAdd.aspx?kehuid={0}" />



                          </Columns>
                          <HeaderStyle CssClass="Admin_Table_Title " />
                      </asp:GridView>
                  </div>

                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Width="682px" Style="font-size: 9pt" UrlPaging="True" PageSize="12">
                                </webdiyer:AspNetPager>

                            </ContentTemplate>


                        </asp:UpdatePanel>

                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="图片" ID="TabPanel2" Visible="False">
                    <HeaderTemplate>
                        已审给我的客户
                    </HeaderTemplate>
                    <ContentTemplate>
                        已审给我的客户<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <div style="height: 400px; overflow: scroll;">

                                    <asp:GridView ID="GridView2" runat="server" Width="100%" OnRowDataBound="GridView2_RowDataBound" CssClass="Admin_Table" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="序 号">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# (Container.DisplayIndex+1).ToString("0000") %>'
                                                        CommandArgument='<%# Eval("kehuid") %>' CommandName="chakan" ForeColor="Green"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle ForeColor="Green" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="编号" SortExpression="kehuid">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("kehuid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="customname" HeaderText="客户名称" />
                                            <asp:BoundField DataField="CustomType" HeaderText="客户类型" />
                                            <asp:BoundField DataField="CustomSouce" HeaderText="客户来源" />
                                            <asp:BoundField DataField="fillname" HeaderText="填写人" />
                                            <asp:BoundField DataField="responser" HeaderText="业务员" />
                                            <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustomerSee.aspx?kehuid={0}"
                                                HeaderText="" Text="查看客户" Target="button" />
                                            <%--    <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Quotation/QuotationAdd.aspx?kehuid={0}"
                    HeaderText="" Text="客户报价" Target="_blank" />--%>


                                            <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustTraceAdd.aspx?kehuid={0}"
                                                HeaderText="" Text="填写日志" Target="button" />


                                            <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Quotation/QuotationAdd1.aspx?kehuid={0}"
                                                HeaderText="" Text="客户报价" Target="_blank" />



                                            <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/ContractAdd.aspx?kehuid={0}"
                                                HeaderText="" Text="增加协议" Target="button" Visible="false" />

                                            <asp:TemplateField HeaderText="请求记录">
                                                <ItemTemplate>
                                                    <span style="cursor: hand; color: Blue;" onclick="window.showModalDialog('CustomerRequestSee.aspx?kehuid=<%#Eval("kehuid") %>','test','dialogWidth=900px;DialogHeight=500px;status:no;help:no;resizable:yes; dialogTop:100px;edge:raised;')">
                                                        <asp:Label ID="seeLB" runat="server" Text="查看请求"></asp:Label></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <HeaderStyle CssClass="Admin_Table_Title " />
                                    </asp:GridView>

                                </div>

                            </ContentTemplate>



                        </asp:UpdatePanel>

                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>



        </div>
    </form>
</body>
</html>
