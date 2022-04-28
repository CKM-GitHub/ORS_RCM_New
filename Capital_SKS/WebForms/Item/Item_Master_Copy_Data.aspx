<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_Master_Copy_Data.aspx.cs" Inherits="Capital_SKS.WebForms.Item.Item_Master_Copy_Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>複製コピー</title>
    <link rel="stylesheet" href="../../Styles/base.css" />
    <link rel="stylesheet" href="../../Styles/common.css" />
   <%-- <link rel="stylesheet" href="../../Styles/manager-style.css" />--%>
   <link rel="stylesheet" href="../../Styles/item.css" />
</head>
<body>
    <div id="PopWrapper">

<section runat="server">
	<h1><asp:Label runat="server" ID="lblheader" /></h1>
	
	<div id="PopContents" class="pop4_Mcate">
    <form id="form1" runat="server">

        <div>
            <h3>複製コピー</h3>
            <br/>
         <dl class="itemNo">
			<dt style="font-weight:unset">商品番号</dt>
            <dd><asp:TextBox ID="txtItemCode" runat="server" Width="150px"></asp:TextBox></dd>
		</dl>
            <br/>
		<dl class="itemName">
			<dt style="font-weight:unset">商品名</dt>
			<dd><asp:TextBox ID="txtItem_Name" runat="server" TextMode="MultiLine" Width ="550px" MaxLength="255"></asp:TextBox></dd>
		</dl>
        </div>
        <div class="btn">
            <asp:Button runat="server" ID="btnCopy" Text="複製" Width="150px" style="display:inline;margin-left:73px;" OnClick="btnCopy_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="キャンセル" Width="150px" style="display:inline;margin-left:80px;" OnClick="btnCancel_Click" />
        </div>
    </form>
        </div>
</section>
</div>
</body>
</html>
