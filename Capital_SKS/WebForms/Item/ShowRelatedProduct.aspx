<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowRelatedProduct.aspx.cs" Inherits="Capital_SKS.WebForms.Item.ShowRelatedProduct" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <meta charset="UTF-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <!--[if lt IE 9]>
        <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->
        <link rel="stylesheet" href="../../Styles/base.css" />
        <link rel="stylesheet" href="../../Styles/common.css" />
        <link rel="stylesheet" href="../../Styles/manager-style.css" />
        <link rel="stylesheet" href="../../Styles/item.css" />
        <style type="text/css">
            #btnSearch {
                margin-left:30px !important;
                width: 80px !important;
                height:32px !important;
                border-radius:10px;
            }
            #txtSearch{
                margin-right:30px !important;
            }
            #btnClose,#btnCancel{
                height: 40px !important;
                border-radius:10px;float:left;
            }
            #btnClose{
                margin-left:450px !important;
                margin-right:50px !important;
            }   
            .clNon{
                overflow:hidden!important;
                height:550px !important;
            }
        </style>
        <script type="text/javascript">
            function ckItem_Check(latestcheck) {
                var dl = document.getElementById("<%=gvMallCategory.ClientID%>");
                var inputs = dl.getElementsByTagName("input");
                var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');
                if (checkboxes.length > 5) {
                    alert('The number of related products exceeds the maximun values.');
                    latestcheck.checked = false;
                }
            }
        </script>
    </head>
    <body class="clNon">
        <div id="PopWrapper" style="margin-left:120px;">
            <section runat="server">
	            <div id="PopContents" class="pop4_Mcate">
	                <form runat="server">

	                    <p class="popSearch" style="margin-left:150px;margin-bottom:35px; width:800px">
                            商品番号
				            <asp:TextBox ID="txtSearch" runat="server" Height="32px" Font-Bold="True" Width="210px" MaxLength="50"  onkeypress="return isNumberKeys(event)" ></asp:TextBox>
                            商品名
				            <asp:TextBox ID="txtSearch1" runat="server" Height="32px" Font-Bold="True" Width="210px" MaxLength="50" onkeypress="return isNumberKeys(event)"></asp:TextBox>
                            <asp:Button runat="server" ID="btnSearch" Text="検 索" Font-Bold="True" onclick="btnSearch_Click" />
	                    </p>

	                    <div style="height:405px;width:998px;">
		                    <table>
			                    <asp:GridView ID="gvMallCategory" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvMallCategory_RowDataBound" 
                                AllowPaging="True" PageSize="15" CellPadding="4" ForeColor="#333333" GridLines="None" onpageindexchanging="gvMallCategory_PageIndexChanging" >
                                     <AlternatingRowStyle BackColor="White"  />
                                        <HeaderStyle ForeColor="White" Font-Bold="True"  Font-Size="18px" BackColor="#555555"></HeaderStyle>
                                    
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="ckItem" onclick ="ckItem_Check(this);" OnCheckedChanged="chkItem_CheckedChanged" AutoPostBack="true"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="商品番号">
                                             <ItemTemplate>
                                             <asp:Label runat="server" ID="lblItem_Code" Text='<%#Eval("Item_Code") %>' />
                                             </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="商品名">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem_Name" runat="server" Text='<%#Eval("Item_Name") %>'  />
                                            </ItemTemplate> 
                                      </asp:TemplateField>
                                  </Columns>

                                </asp:GridView>
		                    </table>
	                    
                            <div class ="btn">
                                <asp:Button runat="server" ID="btnClose" Text="決定" Width="150px" OnClick="btn_Close" />
                                <asp:Button ID="btnCancel" runat="server" Text="キャンセル"  Width="150px" OnClick="btn_Cancel" />
                            </div>

                       </div>
	                </form>
	            </div>
            </section>
        </div>
    </body>
</html>