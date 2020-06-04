<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerAssign1.aspx.cs" Inherits="Customer_CustomerAssign1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
    <style type="text/css">
<!--
html,body {}
.mydiv2 {
background-color: #FFFFFF;
border: 1px solid #f00;
text-align: center;
line-height: 16px;
overflow :scroll;
font-size: 13px;
font-weight: bold;
z-index:999;
width: 700px;
height: 350px;
left:50%;
top:8%;
/*margin-left:-150px!important;FF IE7 该值为本身宽的一半 */
margin-top:-40px!important;/*FF IE7 该值为本身高的一半*/
margin-top:0px;
position:fixed!important;/* FF IE7*/
position:absolute;/*IE6*/
_top:       expression(eval(document.compatMode &&
            document.compatMode=='CSS1Compat') ?
            documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
            document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/

}

.bg2,.popIframe2 {
background-color: #666; display:none;
width: 100%;
height: 100%;
left:0px;
top:0;/*FF IE7*/
filter:alpha(opacity=15);/*IE*/
opacity:0.5;/*FF*/
z-index:1;
position:fixed!important;/*FF IE7*/
position:absolute;/*IE6*/
_top:       expression(eval(document.compatMode &&
            document.compatMode=='CSS1Compat') ?
            documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/
            document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/* www.codefans.net IE5 IE5.5*/
}
.popIframe2 
{
filter:alpha(opacity=0);/*IE*/
opacity:0;/*FF*/
}
-->
</style>
    <script language="javascript" type="text/javascript">
        function showDiv2() {
            document.getElementById('popDiv2').style.display = 'block';
            document.getElementById('popIframe2').style.display = 'block';
            document.getElementById('bg2').style.display = 'block';



        }
        function closeDiv2() {
            document.getElementById('popDiv2').style.display = 'none';
            document.getElementById('bg2').style.display = 'none';
            document.getElementById('popIframe2').style.display = 'none';


            Text1



        }
        function bnSave_onclick() {
            showDiv2();
        }

    </script>
    <script language="javascript" type="text/javascript">
        function showDiv2() {
            document.getElementById('popDiv2').style.display = 'block';
            document.getElementById('popIframe2').style.display = 'block';
            document.getElementById('bg2').style.display = 'block';



        }
        function closeDiv2() {
            document.getElementById('popDiv2').style.display = 'none';
            document.getElementById('bg2').style.display = 'none';
            document.getElementById('popIframe2').style.display = 'none';
            CheckValue7();






        }
        function bnSave_onclick() {
            showDiv2();
        }

    </script>
    <script type="text/javascript">
        var xmlhttp2;
        function jizhuangxiang2() {
            document.getElementById('popDiv2').style.display = 'block';
            document.getElementById('popIframe2').style.display = 'block';
            document.getElementById('bg2').style.display = 'block';



            /*var id2=document.getElementById("Text31").value;*/


        }

    </script>
    <script language="javascript">

        function CheckValue7() {
            //在JS端调用CheckBoxList
            var chkObject = document.getElementById('<%=CheckBoxList7.ClientID%>');
            var chkInput = chkObject.getElementsByTagName("INPUT");
            var arrListValue = chkObject.ListValue.split(',');
            var count = arrListValue.length;
            var strCheckChecked = "";
            var arrCheckChecked;
            var chkValue = "";

            //每次点击CheckBoxList的一个Item，都循环把所有Item的选中状态按0、1标志，存入一个变量，最后再根据这个标志来决定checkboxlist中要取的值

            for (var i = 0; i < chkInput.length; i++) {
                if (chkInput[i].checked) {
                    strCheckChecked = strCheckChecked + "1" + ",";
                }
                else {
                    strCheckChecked = strCheckChecked + "0" + ",";
                }
            }
            arrCheckChecked = RTrim(strCheckChecked).split(',');
            for (var j = 0; j < count; j++) {
                if (arrCheckChecked[j] == "1") {
                    chkValue += arrListValue[j] + ",";
                }
            }
            chkValue = RTrim(chkValue);









            document.getElementById("Text1").value = chkValue;

        }

        //如果有则移除末尾的逗号
        function RTrim(str) {
            if (str.charAt(str.length - 1) == ",")
                return str.substring(0, str.length - 1);
            else
                return str;
        }
    
    </script>
    <script language="javascript">

        function xiaochu7() {
            //在JS端调用CheckBoxList
            var chkObject = document.getElementById('<%=CheckBoxList7.ClientID%>');
            var chkInput = chkObject.getElementsByTagName("INPUT");
            var arrListValue = chkObject.ListValue.split(',');
            var count = arrListValue.length;
            var strCheckChecked = "";
            var arrCheckChecked;
            var chkValue = "";

            //每次点击CheckBoxList的一个Item，都循环把所有Item的选中状态按0、1标志，存入一个变量，最后再根据这个标志来决定checkboxlist中要取的值

            for (var i = 0; i < chkInput.length; i++) {
                if (chkInput[i].checked) {
                    chkInput[i].checked = false;
                }

            }


        }

        //如果有则移除末尾的逗号
   
    
    </script>
    <script language="javascript">
        function xiaochu() {
            //alert("nihao");
            xiaochu7();

        }
    </script>
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/WebCss.css" rel="stylesheet" type="text/css" />
    <link href="../Web_CSS/ymPrompt/vista/ymPrompt.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../JavaScript/ManageWeb.js"></script>
    <script type="text/javascript" src="../JavaScript/PCASClass.js"></script>
    <script type="text/javascript" src="../JavaScript/ymPrompt.js"></script>
    <script type="text/javascript" src="../JavaScript/popcalendar.js"></script>
</head>
<body>
    <form name="form1" runat="server" id="form1">
    <div class="Body_Title">
        销售管理 》》业务员请求分派的客户</div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="false"
        EnableScriptLocalization="false">
    </asp:ScriptManager>
    <div id="popDiv2" class="mydiv2" style="display: none; left: 5%; top: 20%; background-color: #ffffff;
        border-right: red 2px solid; border-top: red 2px solid; border-left: red 2px solid;
        border-bottom: red 2px solid; margin-top: 5px;">
        <div>
            <br />
            业务员成员</div>
        <div style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
            border-bottom: 1px solid;">
            <asp:CheckBoxList ID="CheckBoxList7" runat="server" RepeatColumns="5" Width="100%"
                TextAlign="Left" RepeatDirection="Horizontal">
            </asp:CheckBoxList>
            &nbsp;</div>
        <br />
        <div>
        </div>
        <div style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
            border-bottom: 1px solid; display: none;">
            <asp:CheckBoxList ID="CheckBoxList8" runat="server" Visible="false" RepeatColumns="8"
                RepeatDirection="Horizontal">
            </asp:CheckBoxList>
        </div>
        <br />
        <div>
        </div>
        <div style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
            border-bottom: 1px solid; display: none;">
            <asp:CheckBoxList ID="CheckBoxList9" runat="server" Visible="false" RepeatDirection="Horizontal">
            </asp:CheckBoxList>
        </div>
        <input id="Button4" type="button" value="关闭" class="BnCss" onclick="closeDiv2()" />
        <input id="Button3" type="button" value="重置" class="BnCss" onclick="xiaochu()" />
    </div>
    <div id="bg2" class="bg2" style="display: none;">
    </div>
    <iframe id='popIframe2' class='popIframe2' frameborder='0' language="javascript"
        onblur="return popIframe2_onblur()"></iframe>
    <input id="txFDate" name="txFDate" visible ="false"  class="TxCss" type="text" value="" onclick="popUpCalendar(this,document.forms[0].txFDate,'yyyy-mm-dd')"
        readonly runat="server" style="width: 68px" />备注信息：<asp:TextBox 
        ID="TextBox2" runat="server" Width="391px"></asp:TextBox>
&nbsp;<div style ="display:none;">
    <input id="txTDate" name="txTDate" class="TxCss" visible ="false"  type="text" value="" onclick="popUpCalendar(this,document.forms[0].txTDate,'yyyy-mm-dd')"
        readonly runat="server" style="width: 69px" />客户名称或业务人员<asp:DropDownList ID="DropDownList1"
            runat="server" Width="77px" Visible ="false" >
           <asp:ListItem  Value ="customname">客户名称</asp:ListItem>
           <asp:ListItem  Value ="responser">业务人员</asp:ListItem>
        </asp:DropDownList>
   <asp:TextBox ID="TextBox1" class="TxCss"   runat="server" Width="74px"></asp:TextBox>&nbsp;<asp:Button
        ID="Button2" runat="server" OnClick="Button2_Click" class="BnCss" Text="查询" />
   
    <input id="Button7" onclick="jizhuangxiang2()" type="button" class="BnCss" value="业务员"  style ="display :none "/>
    <asp:Button ID="Button1" runat="server" class="BnCss" Text="确定分派给" OnClick="Button1_Click1" /> <asp:DropDownList ID="DropDownList2" runat="server"  AutoPostBack ="true" 
        onselectedindexchanged="DropDownList2_SelectedIndexChanged">
    </asp:DropDownList>

    <asp:DropDownList ID="DropDownList3" runat="server" >
    </asp:DropDownList></td>
    <input id="Text1" type="text" runat="server" Visible ="false" ondblclick="jizhuangxiang2()" style="width: 100%;
        color: Red;" /></div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:CheckBox ID="CheckBox2" runat="server" Text="全选" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:GridView ID="GridView1" runat="server" Width="100%" DataKeyNames="kehuid" CssClass="Admin_Table"
                    AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                全选<asp:CheckBox ID="CheckBox3" Enabled="false" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                                    AutoPostBack="True" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="customname" HeaderText="客户名称" />
                        <asp:BoundField DataField="CustomType" HeaderText="客户类型" />
                        <asp:BoundField DataField="CustomSouce" HeaderText="客户来源" />
                        <asp:BoundField DataField="fillname" HeaderText="填写人" />
                        <asp:BoundField DataField="pubtime1" HeaderText="录入时间" DataFormatString="{0:d}" HtmlEncode="false" />
                        <asp:BoundField DataField="customlevel" HeaderText="是否请求分派" />
                        <asp:HyperLinkField DataNavigateUrlFields="kehuid" DataNavigateUrlFormatString="~/Customer/CustomerSee.aspx?kehuid={0}"
                    HeaderText="" Text="查看客户" />
                      <asp:TemplateField HeaderText="查看请求"> 
                                <ItemTemplate>    
                                    <span  style ="cursor :hand;color :Blue ;" onclick ="window.showModalDialog('CustomerRequestSee.aspx?kehuid=<%#Eval("kehuid") %>','test','dialogWidth=900px;DialogHeight=500px;status:no;help:no;resizable:yes; dialogTop:100px;edge:raised;')"><asp:Label id="seeLB" runat="server" Text="查看请求"></asp:Label></span>                     
                                </ItemTemplate>
                            </asp:TemplateField> 
                         <asp:TemplateField HeaderText="分派"  >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" Text="同意" CommandArgument='<%# Eval("kehuid") %>'
                            CommandName="xiada"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>


                 <asp:TemplateField HeaderText="分派"  >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton51" runat="server" Text="拒绝" CommandArgument='<%# Eval("kehuid") %>'
                            CommandName="xiada1"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle ForeColor="Green" />
                </asp:TemplateField>


                    </Columns>
                    <HeaderStyle CssClass="Admin_Table_Title " />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CheckBox2" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>

         <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="第<font color='red'><b>%CurrentPageIndex%</b></font>页  共:%PageCount%页;第 %StartRecordIndex%-%EndRecordIndex%"
                                    CustomInfoTextAlign="Center" FirstPageText="【首页】" Height="25px" HorizontalAlign="Center"
                                    InputBoxStyle="width:19px" LastPageText="【尾页】" NextPageText="【下页】 " OnPageChanged="AspNetPager1_PageChanged"
                                    PrevPageText="【前页】 " ShowCustomInfoSection="Left" ShowInputBox="Never" ShowNavigationToolTip="True"
                                    Style="font-size: 9pt" UrlPaging="True" PageSize="15">
                                </webdiyer:AspNetPager> 

    </div>
    <asp:Literal ID="ld" runat="server"></asp:Literal>
    </form>
</body>
</html>

