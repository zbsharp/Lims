<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoicePrint.aspx.cs" Inherits="Print_InvoicePrint" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��ӡ�շѵ�</title>





    <style type="text/css">
        .normal,.normal td,.normal th{border:dashed 1px gray;padding:3px 5px;height:30px;}
        .normal th{text-align:center;height:30px;}
        table, td,th{border-collapse: collapse;font-size:14px;}
    </style>
    <style media="print" type="text/css"> 
        .Noprint
        {
            display:none;
        } 
        .PageNext
        {
            page-break-after: always;
        } 
        
    </style>
    <style type="text/css">
       .tdp 
        { 
            border-color:#000000;
            border-bottom:1px dashed  #000000; 
            border-left:0 dashed  #000000; 
            border-right:1px dashed  #000000; 
            border-top:1px dashed  #000000;
            font-size: 14px; 
        } 
        .tabp 
        { 
            border-color: #000000; 
            border-style:1px dashed ; 
            border-top-width: 0px; 
            border-right-width: 0px; 
            border-bottom-width: 0px; 
            border-left-width: 1px; 
            
             
        }  
        .F_size12 {font-size:12px;}
        .F_size16 {font-size:21px;}
        .F_size17 {font-size:22px;}
        .F_size19 {font-size:16px; font-family :Times New Roman;}
        .F_size18 {font-size:medium;}
        .F_B{font-weight:bold;}
        .nobr{white-space:nowrap;}
    </style>
    

    <script language="javascript"  for="SignatureControl" event="EventOnSign(DocumentId,SignSn,KeySn,Extparam,EventId,Ext1)">
  //���ã���֤ǩ�µ�XMLֵ

  var mFieldXml = "<?xml version='1.0' encoding='GB2312' standalone='yes'?>";
  mFieldXml =mFieldXml+"<Signature>";
  mFieldXml =mFieldXml+"<Field>";
  mFieldXml =mFieldXml+"<Field Index='Caption'>���ܼ���</Field>";	
  mFieldXml =mFieldXml+"<Field Index='ID'>BMJH</Field>";	
  mFieldXml =mFieldXml+"<Field Index='VALUE'>"+DocForm.BMJH.value+"</Field>";	
  mFieldXml =mFieldXml+"<Field Index='ProtectItem'>TRUE</Field>";	
  mFieldXml =mFieldXml+"</Field>";	 

  mFieldXml=mFieldXml+"<Field>";                        //����"�׷�ǩ��"��
  mFieldXml=mFieldXml+"<Field Index='Caption'>�׷�ǩ��</Field>";	
  mFieldXml=mFieldXml+"<Field Index='ID'>JF</Field>";	
  mFieldXml=mFieldXml+"<Field Index='VALUE'>"+DocForm.JF.value+"</Field>";	
  mFieldXml=mFieldXml+"<Field Index='ProtectItem'>TRUE</Field>";	
  mFieldXml=mFieldXml+"</Field>";	
  mFieldXml=mFieldXml+"</Signature>";	
  DocForm.SignatureControl.FieldsXml = mFieldXml;
  DocForm.SignatureControl.EventResult = true;
</script>

<script language="javascript" type="text/javascript">
    var wnd;  //���帨������ȫ�ֱ���
    //���ã��Զ������ĵ�
    function ProtectDocument() {
        var mLength = document.getElementsByName("iHtmlSignature").length;
        var mProtect = false;
        for (var i = 0; i < mLength; i++) {
            var vItem = document.getElementsByName("iHtmlSignature")[i];
            if (vItem.DocProtect) {
                mProtect = true;
                break;
            }
        }

        if (!mProtect) {
            var vItem = document.getElementsByName("iHtmlSignature")[mLength - 1];
            vItem.LockDocument(true);
        }
    }
    //���ã��������
    function UnProtectDocument() {
        var mLength = document.getElementsByName("iHtmlSignature").length;
        var mProtect = true;
        for (var i = 0; i < mLength; i++) {
            var vItem = document.getElementsByName("iHtmlSignature")[i];
            if (vItem.DocProtect) {
                mProtect = false;
                break;
            }
        }

        if (!mProtect) {
            var vItem = document.getElementsByName("iHtmlSignature")[mLength - 1];
            vItem.LockDocument(true);
        }
    }
    //���ã����м׷�ǩ��
    function DoJFSignature() {
        DocForm.SignatureControl.FieldsList = "XYBH=Э����;BMJH=���ܼ���;JF=�׷�ǩ��;HZNR=��������;QLZR=Ȩ������;CPMC=��Ʒ����;DGSL=��������;DGRQ=��������"       //�������ֶ�
        DocForm.SignatureControl.Position(460, 260);                      //ǩ��λ�ã���Ļ����
        DocForm.SignatureControl.UserName = "lyj";                         //�ļ���ǩ���û�
        DocForm.SignatureControl.RunSignature();                         //ִ��ǩ�²���
        if (DocForm.SignatureControl.DocProtect) {
            ProtectDocument();
        }
    }

    //���ã������ҷ�ǩ��
    function DoYFSignature() {
        if (wnd != undefined) {
            var results = wnd.split(";");
            DocForm.SignatureControl.CharSetName = results[0]; 	  //�����Լ�
            DocForm.SignatureControl.WebAutoSign = results[1]; 	  //�Զ�����ǩ��
            DocForm.SignatureControl.DocPortect = results[2];
            DocForm.SignatureControl.WebCancelOrder = results[3];   //����˳��
            DocForm.SignatureControl.PassWord = results[4]; 	  //ǩ������

            var tmp = DocForm.SignatureControl.WebSetFontOther((results[5] == "1" ? true : false),
		results[6], results[7], results[8], results[9], results[10],
			(results[11] == "1" ? true : false)); 	    //����ǩ�¸������ָ�ʽ
            DocForm.SignatureControl.WebIsProtect = results[12]; 	    //���������ݣ� 0������  1���������ݣ��ɲ���  2��������ݣ������ܲ���  Ĭ��ֵ1
        } else {
            DocForm.SignatureControl.WebIsProtect = 1; 		    //���������ݣ� 0������  1���������ݣ��ɲ���  2��������ݣ������ܲ���  Ĭ��ֵ1
            DocForm.SignatureControl.WebCancelOrder = 0; 		    //ǩ�³���ԭ������, 0��˳�� 1�Ƚ����  2�Ƚ��ȳ�  Ĭ��ֵ0
        }

        DocForm.SignatureControl.FieldsList = "HTJB=��ͬ����;XYBH=Э����;BMJH=���ܼ���;JF=�׷�ǩ��;YF=�ҷ�ǩ��;HZNR=��������;QLZR=Ȩ������;CPMC=��Ʒ����;DGSL=��������;DGRQ=��������"       //�������ֶ�
        DocForm.SignatureControl.Position(0, 0);                          //ǩ��λ��
        DocForm.SignatureControl.SaveHistory = "False";                    //�Ƿ��Զ�������ʷ��¼,true����  false������  Ĭ��ֵfalse
        DocForm.SignatureControl.UserName = "wjd";                         //�ļ���ǩ���û�
        DocForm.SignatureControl.SetPositionRelativeTag("YF", 1);         //����ǩ��λ����������ĸ���ǵ�ʲôλ��
        DocForm.SignatureControl.PositionBySignType = 1;                 //����ǩ������λ�ã�1��ʾ�м�
        //DocForm.SignatureControl.ValidateCertTime = '1';                 //�������֤����Ч�ԣ���װĿǰ�±����и�֤��Root.cer�͵����б�Crl.crl
        DocForm.SignatureControl.RunSignature();                         //ִ��ǩ�²���
        if (DocForm.SignatureControl.DocPortect) {
            ProtectDocument();
        }
    }


    //���ã�������дǩ��
    function DoSXSignature() {
        if (wnd != undefined) {
            var results = wnd.split(";");
            DocForm.SignatureControl.CharSetName = results[0]; 	  //�����Լ�
            DocForm.SignatureControl.WebAutoSign = results[1]; 	  //�Զ�����ǩ��
            DocForm.SignatureControl.DocPortect = results[2];
            DocForm.SignatureControl.WebCancelOrder = results[3];   //����˳��
            DocForm.SignatureControl.PassWord = results[4]; 	  //ǩ������

            var tmp = DocForm.SignatureControl.WebSetFontOther((results[5] == "1" ? true : false),
		results[6], results[7], results[8], results[9], results[10],
			(results[11] == "1" ? true : false)); 	    //����ǩ�¸������ָ�ʽ
            DocForm.SignatureControl.WebIsProtect = results[12]; 	    //���������ݣ� 0������  1���������ݣ��ɲ���  2��������ݣ������ܲ���  Ĭ��ֵ1
        } else {
            DocForm.SignatureControl.WebIsProtect = 1; 		    //���������ݣ� 0������  1���������ݣ��ɲ���  2��������ݣ������ܲ���  Ĭ��ֵ1
            DocForm.SignatureControl.WebCancelOrder = 0; 		    //ǩ�³���ԭ������, 0��˳�� 1�Ƚ����  2�Ƚ��ȳ�  Ĭ��ֵ0
        }

        DocForm.SignatureControl.FieldsList = "XYBH=Э����;BMJH=���ܼ���;JF=�׷�ǩ��;YF=�ҷ�ǩ��;HZNR=��������;QLZR=Ȩ������;CPMC=��Ʒ����;DGSL=��������;DGRQ=��������"       //�������ֶ�
        DocForm.SignatureControl.Position(0, 0);                           //��дǩ��λ��
        //DocForm.SignatureControl.SaveHistory="false";                   //�Ƿ��Զ�������ʷ��¼,true����  false������  Ĭ��ֵfalse
        DocForm.SignatureControl.Phrase = "ͬ��;��ͬ��;���ʵ";           //����������ע���ô�
        DocForm.SignatureControl.HandPenWidth = 1;                        //���á���ȡ��дǩ���ıʿ�
        DocForm.SignatureControl.HandPenColor = 100;                      //���á���ȡ��дǩ������ɫ
        DocForm.SignatureControl.SetPositionRelativeTag("HZNR", 1);        //����ǩ��λ����������ĸ���ǵ�ʲôλ��
        DocForm.SignatureControl.PositionBySignType = 1;                  //����ǩ������λ�ã�1��ʾ�м�
        //DocForm.SignatureControl.UserName="lyj";                        //�ļ���ǩ���û�
        DocForm.SignatureControl.RunHandWrite();                          //ִ����дǩ��
        if (DocForm.SignatureControl.DocPortect) {
            ProtectDocument();
        }
    }


    //���ã�������궨λǩ��
    function DoMouseSignature() {
        var mx = event.clientX + document.body.scrollLeft - document.body.clientLeft;  //��ȡX����ֵ
        var my = event.clientY + document.body.scrollTop - document.body.clientTop;   //��ȡY����ֵ

        DocForm.SignatureControl.FieldsList = "HTJB=��ͬ����;XYBH=Э����;BMJH=���ܼ���;JF=�׷�ǩ��;YF=�ҷ�ǩ��;HZNR=��������;QLZR=Ȩ������;CPMC=��Ʒ����;DGSL=��������;DGRQ=��������"       //�������ֶ�
        DocForm.SignatureControl.Position(mx, my);                        //ǩ��λ��
        DocForm.SignatureControl.UserName = "lyj";                         //�ļ���ǩ���û�
        DocForm.SignatureControl.PositionBySignType = 1;                 //����ǩ������λ�ã�1��ʾ�м�
        DocForm.SignatureControl.RunSignature();                         //ִ��ǩ�²���  
        if (DocForm.SignatureControl.DocProtect) {
            ProtectDocument();
        }
    }



    //���ã���ȡǩ����Ϣ����XML��ʽ���أ����ҷ�����ʾ����.�����XML��ʽ����ռ�����Ƥ��
    //      ����������������δ������Լ����ʵ�����,��ʾ���������ؽ��������ʾ��
    function WebGetSignatureInfo() {
        var mSignXMl = DocForm.SignatureControl.GetSignatureInfo();   //��ȡ��ǰ�ĵ�ǩ����Ϣ����XML����
        alert(mSignXMl);                                      //������Ϣ

        var XmlObj = new ActiveXObject("Microsoft.XMLDOM");
        XmlObj.async = false;
        var LoadOk = XmlObj.loadXML(mSignXMl);
        var ErrorObj = XmlObj.parseError;

        if (ErrorObj.errorCode != 0) {
            alert("������Ϣ����...");
        } else {

            var CurNodes = XmlObj.getElementsByTagName("iSignature_HTML");
            for (var iXml = 0; iXml < CurNodes.length; iXml++) {
                var TmpNodes = CurNodes.item(iXml);
                /*
                alert(TmpNodes.selectSingleNode("SignatureOrder").text);  //ǩ�����к�
                alert(TmpNodes.selectSingleNode("SignatureName").text);   //ǩ������
                alert(TmpNodes.selectSingleNode("SignatureUnit").text);   //ǩ�µ�λ
                alert(TmpNodes.selectSingleNode("SignatureUser").text);   //ǩ���û�
                alert(TmpNodes.selectSingleNode("SignatureDate").text);   //ǩ������
                alert(TmpNodes.selectSingleNode("SignatureIP").text);     //ǩ�µ���IP
                alert(TmpNodes.selectSingleNode("KeySN").text);           //Կ�������к�
                alert(TmpNodes.selectSingleNode("SignatureSN").text);     //ǩ�����к�
                alert(TmpNodes.selectSingleNode("SignatureResult").text); //ǩ�������
                */
            }

        }
    }


    //���ã����ý�ֹ(����)ǩ�µ���Կ��   ���������Ϣ����ռ�����Ƥ��
    function WebAllowKeySN() {
        var KeySn = window.prompt("�������ֹ�ڴ�ҳ����ǩ�µ�Կ�������к�:");
        DocForm.SignatureControl.WebAllowKeySN(false, KeySn);
    }


    //���ã���ȡKEY��Կ�̵�SN���к�
    function WebGetKeySN() {
        var KeySn = DocForm.SignatureControl.WebGetKeySN();
        alert("����Կ�������к�Ϊ:" + KeySn);
    }


    //���ã�У���û��� PIN���Ƿ���ȷ
    function WebVerifyKeyPIN() {
        var KeySn = DocForm.SignatureControl.WebGetKeySN();
        var mBoolean = DocForm.SignatureControl.WebVerifyKeyPIN("123456");
        if (mBoolean) {
            alert(KeySn + ":ͨ��У��");
        } else {
            alert(KeySn + ":δͨ��У��");
        }
    }


    //���ã��޸�Կ����PIN��,����1ΪԭPIN��,����2Ϊ�޸ĺ��PIN��
    function WebEditKeyPIN() {
        var oldPIN = window.prompt("������ԭ����PIN��");
        if (oldPIN == null) {
            return;
        }
        var newPIN = window.prompt("�������޸ĺ��PIN��");
        if (newPIN == null) {
            return;
        }
        var mBoolean = DocForm.SignatureControl.WebEditKeyPIN(oldPIN, newPIN);
        if (mBoolean) {
            alert("Կ����PIN���޸ĳɹ�!");
        } else {
            alert("Կ����PIN���޸Ĳ��ɹ�!");
        }
    }


    //���ã�������֤ǩ��
    function BatchCheckSign() {
        DocForm.SignatureControl.BatchCheckSign();
    }


    //���ã���������
    function ParameterSetting() {
        var mParameter = new Array();
        mParameter[0] = DocForm.SignatureControl.CharSetName; 	//�����Լ�
        mParameter[1] = DocForm.SignatureControl.WebAutoSign; 	//�Զ�����ǩ��
        mParameter[2] = DocForm.SignatureControl.DocPortect;          //�Զ������ĵ�
        mParameter[3] = DocForm.SignatureControl.WebCancelOrder; //����˳��
        mParameter[4] = DocForm.SignatureControl.PassWord; 	//ǩ������
        if (wnd != undefined) {
            var results = wnd.split(";");
            mParameter[5] = results;
        }

        tmp =
	window.showModalDialog("ParameterSetting.aspx", mParameter, "dialogWidth:350px;dialogHeight:520px;menubar:no;toolbar:no;scrollbars:no;resizable:no;center:yes;status:no;help:no;");
        if (tmp != undefined) {
            wnd = tmp;
        }
        if (wnd != undefined) {
            var results = wnd.split(";");
            DocForm.SignatureControl.CharSetName = results[0];
            DocForm.SignatureControl.WebAutoSign = results[1];
            DocForm.SignatureControl.DocPortect = results[2];
            DocForm.SignatureControl.WebCancelOrder = results[3];
            DocForm.SignatureControl.PassWord = results[4];
            var tmp = DocForm.SignatureControl.WebSetFontOther((results[5] == "1" ? true : false),
			results[6], results[7], results[8], results[9], results[10],
			(results[11] == "1" ? true : false));
        }
    }


    //���ã���ʾ������ǩ��
    function ShowSignature(visibleValue) {
        var mLength = document.getElementsByName("iHtmlSignature").length;
        for (var i = 0; i < mLength; i++) {
            var vItem = document.getElementsByName("iHtmlSignature")[i];
            vItem.Visiabled = visibleValue;
        }
    }

    //���ã�ɾ��ǩ��
    function DeleteSignature() {
        var mLength = document.getElementsByName("iHtmlSignature").length;
        var mSigOrder = "";
        for (var i = mLength - 1; i >= 0; i--) {
            var vItem = document.getElementsByName("iHtmlSignature")[i];
            //mSigOrder := 
            if (vItem.SignatureOrder == "1") {
                vItem.DeleteSignature();
            }
        }
    }

    //���ã��ƶ�ǩ��
    function MoveSignature() {
        DocForm.SignatureControl.MovePositionByNoSave(100, 100);
        alert("λ������100");
        DocForm.SignatureControl.MovePositionByNoSave(-100, -100);
        alert("�ص�ԭ��λ��");
        DocForm.SignatureControl.MovePositionToNoSave(100, 100);
        alert("�ƶ���100��100");
    }


    //���ã�����
    function ShedCryptoDocument() {
        DocForm.SignatureControl.ShedCryptoDocument();
    }


    //���ã����ܻ�ԭ
    function ResetCryptoDocument() {
        DocForm.SignatureControl.ResetCryptoDocument();
    }


    //���ã���ӡ�ĵ�
    function PrintDocument() {
        var tagElement = document.getElementById('documentPrintID');
        tagElement.className = 'print';                                                 //��ʽ�ı�Ϊ�ɴ�ӡ
        var mCount = DocForm.SignatureControl.PrintDocument(false, 2, 5);  //��ӡ���ƴ���
        alert("ʵ�ʴ�ӡ������" + mCount);
        tagElement.className = 'Noprint';                                               //��ʽ�ı�Ϊ���ɴ�ӡ
    }
    //����:��ά�������
    function DoBarcodeSignature() {

        var mXml = "<?xml version='1.0' encoding='GB2312' standalone='yes'?>";
        mXml = mXml + "<Signature>";
        mXml = mXml + "<OtherParam>";
        //��ά��������
        mXml = mXml + "<PDF417String>�������Ƽ��ɷ����޹�˾��ά�������ǩ�¸�����ʾ</PDF417String>";
        //��ά�������� 1:PDF417String  2:QRcode
        mXml = mXml + "<BarcodeType>1</BarcodeType>";
        //�Ƿ���ʾѡ���������ʹ��� false:����ʾ true:��ʾ
        mXml = mXml + "<ShowBarcodeInfo>false</ShowBarcodeInfo>";
        mXml = mXml + "</OtherParam>";
        mXml = mXml + "</Signature>";

        DocForm.SignatureControl.XmlConfigParam = mXml;
        DocForm.SignatureControl.Position(400, 260);  //ǩ��λ��
        DocForm.SignatureControl.RunSignature();    //ִ��ǩ�²���
        if (DocForm.SignatureControl.DocProtect) {
            ProtectDocument();
        }
    }
</script>



</head>
<body style="font:20px; font-family :Times New Roman;" id="documentPrintID"  onload="DocForm.SignatureControl.ShowSignature('<%=DocumentID%>');" topmargin="0" leftmargin="0" bgcolor="c0c0c0">
   
   <form   name="DocForm" method="post" action="InvoicePrint.aspx"  >
  
   

    <input type="hidden" name="DocumentID" value="<%=DocumentID%>">
<OBJECT id="SignatureControl"  classid="clsid:D85C89BE-263C-472D-9B6B-5264CD85B36E" codebase="iSignatureHTML.cab#version=8,0,0,368" width=0 height=0 VIEWASTEXT>
<param name="ServiceUrl" value="<%=mServerUrl%>"><!--��ȥ���ݿ������Ϣ-->
<param name="WebAutoSign" value="0">             <!--�Ƿ��Զ�����ǩ��(0:�����ã�1:����)-->
<param name="PrintControlType" value=2>               <!--��ӡ���Ʒ�ʽ��0:������  1��ǩ�·���������  2�������̿��ƣ�-->
<!--param name="Weburl"  value="">        <ǩ�·�������Ӧ-->
</OBJECT>
  
    <center class="Noprint">
        <br />
        <object id="WebBrowser" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" width="0">
        </object>
        <input type="button" value="��ӡ" onclick="document.all.WebBrowser.ExecWB(6,1)" />
        <input type="button" value="ֱ�Ӵ�ӡ" onclick="document.all.WebBrowser.ExecWB(6,6)" />
        <input type="button" value="ҳ������" onclick="document.all.WebBrowser.ExecWB(8,1)" />
        <input type="button" value="��ӡԤ��" onclick="document.all.WebBrowser.ExecWB(7,1)" />

          <input type="button" value=" ǩ�� "  onclick="DoJFSignature()">&nbsp;

  <input type="button" value=" ǩ����ʾ "  onclick="DoSXSignature()">&nbsp;
  
 
    <input type="button" value=" �ƶ����� "  onclick="MoveSignature();">&nbsp;
    <input type="button" value=" ��ӡ���� "   style ="display:none;" onclick="PrintDocument();">&nbsp;
    <%--<input type="button" value=" ��ά���� "  onclick="DoBarcodeSignature();">--%>&nbsp;
    <input type="button" value=" ����ǩ�� "  onclick="ShowSignature('0')">&nbsp;
    <input type="button" value=" ��ʾǩ�� "  onclick="ShowSignature('1')">&nbsp;
  
   



        <hr align="center" width="90%" size="1" />
 
   </center>


        <asp:Label runat="server" ID="lblTable"></asp:Label>
        
      
        
        
    
        
      
        
        
    </form>
   
   
   
   
    
</body>
</html>

