<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportXiaZai.aspx.cs" Inherits="Report_ReportXiaZai" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <fieldset>
            <legend style="color: Red">报告信息(先发放后下载)</legend>
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="Admin_Table"
                DataKeyNames="id" Style="font-size: 9pt" Width="98%" OnRowCommand="GridView4_RowCommand" OnRowDataBound="GridView4_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>'
                                CommandName="chakan" ForeColor="Green" Text='<%# (Container.DisplayIndex+1).ToString("00") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle ForeColor="Green" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="baogaoid" HeaderText="报告编号" ReadOnly="True" />
                    <asp:BoundField DataField="leibie" HeaderText="类别" />
                    <asp:BoundField DataField="dayintime" HeaderText="安排打印" />
                    <asp:BoundField DataField="wanchengtime" HeaderText="实际完成" />
                    <asp:BoundField DataField="statebumen1" DataFormatString="{0:d}" HeaderText="签字" />
                    <asp:BoundField DataField="statebumen2" DataFormatString="{0:d}" HeaderText="审核" />
                    <asp:BoundField DataField="pizhunby" DataFormatString="{0:d}" HeaderText="批准" />
                    <asp:BoundField DataField="pizhundate" DataFormatString="{0:d}" HeaderText="批准日期" />
                    <asp:BoundField DataField="fafangdate" DataFormatString="{0:d}" HeaderText="发放日期" />
                    <asp:BoundField DataField="danganid" DataFormatString="{0:d}" HeaderText="归档编号" />
                    <asp:BoundField DataField="dangandate" DataFormatString="{0:d}" HeaderText="归档日期" />
                          <asp:TemplateField HeaderText="下载"  >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" Text="下载" CommandArgument='<%# Eval("baogaoid") %>'
                            CommandName="xiada"></asp:LinkButton>
                     
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>
                        
                             <asp:HyperLinkField HeaderText="下载" Text="下载" Visible ="false"  Target="_blank" DataNavigateUrlFormatString="~/Report/BaoGaoShenPi.aspx?baogaoid={0}&&pp=1"
                                DataNavigateUrlFields="baogaoid" />
                             
                                 <asp:TemplateField HeaderText="发放">
                                <ItemTemplate>
                                    <span style="cursor: hand; color: Blue;" onclick="window.open('../report/BaoGaoFaFang.aspx?id=<%#Eval("id") %>','test','dialogWidth=750px;DialogHeight=300px;status:no;help:no;resizable:yes;edge:raised;')">
                                        发放</span>
                                </ItemTemplate>
                            </asp:TemplateField>
                  
                </Columns>
                <HeaderStyle CssClass="Admin_Table_Title " />
            </asp:GridView>
        </fieldset>

    </div>
    </form>
</body>
</html>
