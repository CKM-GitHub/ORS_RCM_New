<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ORS_Item_Master.aspx.cs" Inherits="Capital_SKS.WebForms.Item.ORS_Item_Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha3/dist/css/bootstrap.min.css" />
      <link href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" rel="stylesheet">
   <%--   <link href="assets/css/all.min.css" rel="stylesheet">
      <link href="assets/css/style.css?20220412" rel="stylesheet">--%>
      <link href="css/lightbox.css" rel="stylesheet" />
      <link rel="stylesheet" href="../../Styles/ors_item_master_style.css"  />
    <link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />

     <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>--%>
        <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
        <script src="../../Scripts/jquery.droppy.js" type="text/javascript"></script> 
        <script src="https://unpkg.com/vue@next"></script>
        <script src="https://kit.fontawesome.com/7c0c75e583.js" ></script>  
       <%-- <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>--%>
        <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js"></script>
        <script src="js/lightboxfororsitemmaster.js" type="text/javascript"></script>

     <style type="text/css">
      
.mycheckBig input {width:25px; height:25px;}
.mycheckSmall input {width:10px; height:10px;} 
table {
    width: 100%;
    border-collapse: collapse;
}
.table th {
	height: 0px;
}
table td {
	padding: 2px 5px;
	height:	0px;
}
#ui-datepicker-div{
        font-size: 0.8em !important;
}
.h{
    margin-top:10px !important;
}

.lblstyle dl, dt{
    font-weight: bold;
    display: block;
    text-align: left;
    padding: 1px 4px;
    border-left: 3px solid #3D3A35;
    border-bottom: 1px solid #73c5ff;
    background: #73C5FF;
    box-sizing: border-box;
    font-size: 10px;
}
    .rdostyleclass input[type=radio]:checked + label
    {
        -webkit-border-before-style: none;
        border-right: 0px solid #ddd;
        border-left: 0px solid #ddd;
    }

    .tag{
        position:relative;
        left:20px;
    } 
</style>
<style type="text/css">
         a:hover{
             cursor:default !important;
             color:black !important;
         }
     </style>

        <script type="text/javascript">
       
    function pageLoad(sender, args) {
        //$(function () {

            $("[id$=txtRelease_Date]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '../../images/calendar.gif',
                dateFormat: 'yy/mm/dd',
                yearRange: "2013:2030"
            });
        //});

        //$(function () {
            $("[id$=txtPost_Available_Date]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '../../images/calendar.gif',
                dateFormat: 'yy/mm/dd',
                yearRange: "2013:2030"
            });
        //});

         $("#<%=txtreleasedatemonotaro.ClientID %>").datepicker(
            { 
            showOn: 'button',
            dateFormat: 'yy/mm/dd',
            buttonImageOnly: true,
            buttonImage:'../../images/calendar.gif',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:2030" ,
            }
            );
            $(".ui-datepicker-trigger").mouseover(function () {
            $(this).css('cursor', 'pointer');
            });
        
    }
	</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <asp:HiddenField ID="CustomHiddenField" runat="server" />
        <asp:HiddenField ID="hdfPostDate" runat="server" />
        <asp:HiddenField ID="hdfReleaseDate" runat="server"/>
        <asp:HiddenField ID="hdfScheduleDatemono" runat="server"/>
        <asp:HiddenField  ID="hdfCtrl_ID" runat="server"/>
        <asp:HiddenField  ID="hdfCatID" runat="server"/>
        <asp:HiddenField  ID="hdfTab" runat="server"/>
    <div style="height:55px;"></div>
    <div class="container">
        <div id="headdiv" class="row g-0">
         
            <div class="col-2" id="navleft">

               <%-- <div class="list-group text-center text-lg-left">--%>
                
                <a id="amaster" class="list-group-item aitem act"   onclick="changeStyle(this);">
                <i class="fa-solid fa-brush " style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">商品情報</span>
                </a>
                 <a id="asku" class="list-group-item aitem"  onclick="changeStyle(this);">
                <i class="fa-solid fa-list-check" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">SKU情報</span>
                </a>
                 <a id="aprice" class="list-group-item aitem" onclick="changeStyle(this);">
                <i class="fa-solid fa-circle-dollar-to-slot" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">価格関連</span>
                </a>
                 <a id="aimage" class="list-group-item aitem" onclick="changeStyle(this);">
                <i class="fa-solid fa-images" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">画像関連</span>
                </a>
                 <a id="aoption" class="list-group-item aitem"  onclick="changeStyle(this);">
                <i class="fa-solid fa-filter" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">オプション<br/>カテゴリ</span>
                </a>
                 <a id="amalldata" class="list-group-item aitem"  onclick="changeStyle(this);">
                <i class="fa-solid fa-shop" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">モール設定</span>
                </a>
                <a id="asetting" class="list-group-item aitem"  onclick="changeStyle(this);">
                <i class="fa-solid fa-building" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">取引先設定</span>
                </a>
                 <asp:Label CssClass="list-group-item atxt" style="color:yellow; background-color: var(--left-column-bgcolor); height:400px; text-align:center;" runat="server"> 
                <span class="d-none d-lg-inline">Ctrl + 上下で<br />メニューを<br />切り替えられます</span>
                </asp:Label>
                   <%-- </div>--%>
            </div>
                        
            <div id="maincontent5" class="col-10" style="background-color:#f1f4f6;">
                <div class="row">
                     <div class="col-md-7" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                            <div class="rowfirst">
                              <div class="columnItemcode" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl greenlable lblrequired">商品番号</span>
                           </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="txtItem_Code"  runat="server" ReadOnly="true"></asp:TextBox>
                              </div>
                              <div class="columnJanCD" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl greenlable">JANCD</span>
                            </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="txtJanCD" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  MaxLength="13"  runat="server"></asp:TextBox>                           
                              </div>
                              <div class="columnmaker" >
                                      
                            <asp:Label  ID="lblmakername" runat="server" Text="">
                                 <span class="label label-md lbl greenlable">メーカー名</span>
                            </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="txtmakername" MaxLength="200" runat="server"></asp:TextBox>
                              </div>
                            </div>
                             <div class="rowfirst">
                              <div class="" style="padding:5px;">      
                            <asp:Label  ID="lblitemname" runat="server" Text="">
                                 <span class="label label-md lbl greenlable lblrequired">商品名</span>
                            </asp:Label>   <br />                       
                           <%-- <textarea class="txtarea textamemo" ID="txtItem_Name" cols="20" rows="2"></textarea>--%>
                                  <asp:TextBox ID="txtItem_Name" CssClass="txtarea textamemo" runat="server" TextMode="MultiLine" style="width: 65%;" MaxLength="255"></asp:TextBox>
                                   </div> 
                                  </div> 
                          </div>             
                      </div>
                        <div class=" col-md-5">
                            <div class=" col-md-12 containerboxmemo" style="padding-left: 10px;padding-right: 10px;">
                            <asp:Label  ID="Label1" runat="server" Text="">
                                 <span class="label label-md lbl orangelable">メモ</span>
                            </asp:Label>
                                 <asp:TextBox ID="txtmemo" CssClass="txtarea textamemo" runat="server" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                            <%--<textarea class="txtarea textamemo" ID="txtmemo" cols="20" rows="2"></textarea>--%>
                                </div>
                            </div>
                        
                </div>
                   
    
                <section id="master">
                   <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <div class="row" style="width:100%;margin-left: 3px;margin-right: 50px;">
                                    <div class="floaddiv" style="width:100.81px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">販売単位</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlsalesunit" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="floaddiv" style="width:80.23px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">内容量</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtcontentquantityunitno1" MaxLength="50"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                 <div class="floaddiv" style="width:100.81px;">
                                        <asp:Label CssClass=""  runat="server">
                                        <span class="label label-md lbl greenlable">内容量単位</span>
                                        </asp:Label>
                                         <asp:DropDownList ID="ddlcontentunit1" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                 <div class="floaddiv" style="width:94.23px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">まとめ販売数</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtcontentquantityunitno2" MaxLength="50"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                 <div class="floaddiv" style="width:100.81px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">まとめ単位</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlcontentunit2" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                <div class="floaddiv" style="width:151.23px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">通常・大型</span>
                                        </asp:Label>
                                         <asp:DropDownList ID="ddNormalLargeKBN" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                <div class="floaddiv" style="width:201.64px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">仕入先</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtsiiresaki" MaxLength="100"  runat="server" style="width: 100%;"></asp:TextBox> 
                                </div>
                               <div class="floaddiv" style="width:150.64px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">発注コード</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txthachu" MaxLength="50"  runat="server" style="width: 100%;"></asp:TextBox> 
                                </div> 
                          </div>
                     </div>
                    </div>
                     <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
<%--<asp:UpdatePanel runat="server" id="UpdatePanelrelative" updatemode="Conditional">
                                  <ContentTemplate>--%>
                           <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl orangelable" style="padding-right: 0px;margin-right: 0px;">関連商品</span>
                           </asp:Label>

                           <%--  <asp:Button ID="DisplayRelativeItem" CssClass ="btnplus" runat="server" Text="" OnClientClick="allIteminfo_kanren()" />--%>
                       <%--     <asp:LinkButton runat="server" ID="dis"  CssClass="btnplus" ">
                                      <%--<button id="DisplayRelativeItem" class="btnplus" onclick="allIteminfo_kanren()">--%>
                                  <%--<i id="iplus" class="fa-solid fa-square-plus"></i>
                                  <i id="iminus" class="fa-solid fa-square-minus nonactive"></i>--%>
                             <%-- </button>--%>
                            <%--    </asp:LinkButton>--%>

                             <asp:LinkButton ID="dis"  CssClass="btnplus" runat="server">
                                  <i id="iplus" class="fa-solid fa-square-plus"></i>
                                  <i id="iminus" class="fa-solid fa-square-minus nonactive"></i>
                             </asp:LinkButton>
                         
                             <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                 <ContentTemplate>
                                     <asp:LinkButton ID="LinkButton1" CssClass="fr_btn" OnClientClick ="ShowRelatedProduct(this)" runat="server">
                                         <i class="fa-solid fa-magnifying-glass"></i>
                                         関連商品を検索する</asp:LinkButton>
                              </ContentTemplate>
                             </asp:UpdatePanel>
                          <%--   <a class="fr_btn" onclick="ShowRelatedProduct(this)">
                                 <i class="fa-solid fa-magnifying-glass"></i>
                                 関連商品を検索する
                             </a>--%>
                       <asp:UpdatePanel runat="server" id="UpdatePanelrelative" updatemode="Conditional">
                                  <ContentTemplate>
                            <div class="row" style="width:100%;margin-left: 3px;margin-right: 5px;">
                                <div class="floaddiv" style="width:20%;">
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated1" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <div ID ="tohide" class="tohide">
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated6" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated11" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated16" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                    
                                   
                                </div>
                                <div class="floaddiv" style="width:20%;">
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated2" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                     <section ID="tohide1">
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated7" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated12" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated17" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </section>
                                    </div>
                                <div class="floaddiv" style="width:20%;">
                                     <asp:TextBox CssClass="txtbox" ID="txtRelated3" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <section ID="tohide2">
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated8" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated13" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated18" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                </section>
                                    </div>
                                <div class="floaddiv" style="width:20%;">
                                     <asp:TextBox CssClass="txtbox" ID="txtRelated4" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                   <section ID="tohide3">
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated9" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated14" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated19" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                </section>
                                   </div>
                                <div class="floaddiv" style="width:20%;">
                                     <asp:TextBox CssClass="txtbox" ID="txtRelated5" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                  <section ID="tohide4">
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated10" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated15" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRelated20" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                                  </section>  
                                </div>
                            </div>
                        </ContentTemplate>
                         <Triggers>
                               <%-- <asp:Asyncpostbacktrigger  ControlID="dis" />--%>
                             <asp:Asyncpostbacktrigger ControlID="dis" />
                            </Triggers>
                          </asp:UpdatePanel>
                          </div>
                     </div>

                     <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl greenlable" style="padding-right: 0px;margin-right: 0px;">商品基本情報</span>
                           </asp:Label>

                              <asp:LinkButton ID="template"  CssClass="btnplus" runat="server">
                                  <i id="iplustemplate" class="fa-solid fa-square-plus"></i>
                                  <i id="iminustemplate" class="fa-solid fa-square-minus nonactive"></i>
                             </asp:LinkButton>
                              <asp:UpdatePanel runat="server" id="UpdatePanel2" updatemode="Conditional">
                                  <ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 3px;margin-right: 5px;">
                             <div class="floaddiv" style="width:20%;">

                                 <div style="margin-bottom: 10px;">
                                     <%--<asp:TextBox CssClass="txtbox" ID="TextBox61"  runat="server" style="width: 100%;"></asp:TextBox>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate1" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content1" TextMode="MultiLine"/></p>
                                    <%--  <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                 </div >
                                    <div ID="tohidet">
                                    <div style="margin-bottom: 10px;">
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate6" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content6" TextMode="MultiLine"/></p>
                                     <%--<asp:TextBox CssClass="txtbox" ID="TextBox66"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                 </div>
                                   <div style="margin-bottom: 10px;">
                                    <%-- <asp:TextBox CssClass="txtbox" ID="TextBox71"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate11" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content11" TextMode="MultiLine"/></p>
                                 </div>
                                 <div style="margin-bottom: 10px;">
                                    <%-- <asp:TextBox CssClass="txtbox" ID="txtTemplate16"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="txtTemplate_Content16" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate16" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content16" TextMode="MultiLine"/></p>
                                 </div>
                                   </div>
                                </div>
                                <div class="floaddiv" style="width:20%;">
                                       <div style="margin-bottom: 10px;">
                                     <%--<asp:TextBox CssClass="txtbox" ID="TextBox81"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="txttemplate" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate2" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content2" TextMode="MultiLine"/></p>
                                 </div>
                                      <div ID="tohidet1">
                                    <div style="margin-bottom: 10px;">
                                 <%--    <asp:TextBox CssClass="txtbox" ID="TextBox82"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate7" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content7" TextMode="MultiLine"/></p>
                                        </div>
                                   <div style="margin-bottom: 10px;">
                                     <%--<asp:TextBox CssClass="txtbox" ID="TextBox83"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate12" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content12" TextMode="MultiLine"/></p>
                                 </div>
                                 <div style="margin-bottom: 10px;">
                              <%--       <asp:TextBox CssClass="txtbox" ID="TextBox84"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                       <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate17" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content17" TextMode="MultiLine"/></p>
                                 </div>
                                          </div>
                                    </div>
                                <div class="floaddiv" style="width:20%;">
                                        <div style="margin-bottom: 10px;">
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate3" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content3" TextMode="MultiLine"/></p>
                                 
                            <%--         <asp:TextBox CssClass="txtbox" ID="TextBox85"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                 </div>
                                      <div ID="tohidet2">
                                    <div style="margin-bottom: 10px;">
                                    <%-- <asp:TextBox CssClass="txtbox" ID="TextBox86"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate8" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content8" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                   <div style="margin-bottom: 10px;">
                                  <%--   <asp:TextBox CssClass="txtbox" ID="TextBox87"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate13" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content13" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                 <div style="margin-bottom: 10px;">
                                   <%--  <asp:TextBox CssClass="txtbox" ID="TextBox88"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate18" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content18" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                </div>
                                </div>
                                <div class="floaddiv" style="width:20%;">
                                       <div style="margin-bottom: 10px;">
                                 <%--    <asp:TextBox CssClass="txtbox" ID="TextBox89"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate4" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content4" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                      <div ID="tohidet3">
                                    <div style="margin-bottom: 10px;">
                                  <%--   <asp:TextBox CssClass="txtbox" ID="TextBox90"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                    <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate9" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content9" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                   <div style="margin-bottom: 10px;">
                                   <%--  <asp:TextBox CssClass="txtbox" ID="TextBox91"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                 <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate14" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content14" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                 <div style="margin-bottom: 10px;">
                                 <%--    <asp:TextBox CssClass="txtbox" ID="TextBox92"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                  <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate19" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content19" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                          </div>
                                </div>
                                <div class="floaddiv" style="width:20%;">
                                        <div style="margin-bottom: 10px;">
                                     <%--<asp:TextBox CssClass="txtbox" ID="TextBox93"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate5" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content5" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                      <div ID="tohidet4">
                                    <div style="margin-bottom: 10px;">
                                    <%-- <asp:TextBox CssClass="txtbox" ID="TextBox94"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate10" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content10" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                   <div style="margin-bottom: 10px;">
                                 <%--    <asp:TextBox CssClass="txtbox" ID="TextBox95"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                  <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate15" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content15" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                 <div style="margin-bottom: 10px;">
                                     <%--<asp:TextBox CssClass="txtbox" ID="TextBox96"  runat="server" style="width: 100%;"></asp:TextBox>
                                      <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>--%>
                                     <p><asp:TextBox CssClass="txtbox" style="width: 100%;" runat="server" ID="txtTemplate20" onkeypress="return isNumberKeys(event)"/>
                                     <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtTemplate_Content20" TextMode="MultiLine"/></p>
                                 
                                 </div>
                                    </div>
                                </div>
                                  </div>
                    </ContentTemplate>
                         <Triggers>
                            
                             <asp:Asyncpostbacktrigger ControlID="template" />
                            </Triggers>
                          </asp:UpdatePanel>
                          </div>
                     </div>

                     <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                          
                             <div class="floaddiv" style="width:33.33%;">
                                <div>
                                     <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">PC用販売説明文</span>
                                    </asp:Label>
                                   <%--  <textarea class="txtarea textamemo" ID="txtSale_Description_PC" cols="20" rows="2"></textarea>--%>
                                    <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtSale_Description_PC" TextMode="MultiLine"/>
                                </div>

                                       <div>
                                     <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">商品情報</span>
                                    </asp:Label>
                                    <%-- <textarea class="txtarea textamemo" ID="txtMerchandise_Information" cols="20" rows="2"></textarea>--%>
                                           <asp:TextBox CssClass="txtarea textamemo" runat="server" ID="txtMerchandise_Information" TextMode="MultiLine"/>
                                </div>

                                </div>
                                <div class="floaddiv" style="width:33.33%;">
                                     <div>
                                     <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">PC用商品説明文</span>
                                    </asp:Label>
                                    <%-- <textarea class="txtarea textamemo" ID="txtItem_Description_PC" cols="20" rows="2"></textarea>--%>
                                         <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtItem_Description_PC"  TextMode="MultiLine"/>
                                </div>
                                     <div>
                                     <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">スマートフォン用商品説明文</span>
                                    </asp:Label>
                                   <%--  <textarea class="txtarea textamemo" ID="txtSmart_Template" cols="20" rows="2"></textarea>--%>
                                         <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtSmart_Template" TextMode="MultiLine"/>
                                </div>
                                    
                                </div>
                              <div class="floaddiv" style="width:33.33%;">
                                   <div>
                                     <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">PC用キャッチコピー</span>
                                    </asp:Label>
                                    <%-- <textarea class="txtarea textamemo" ID="txtCatchCopy" cols="20" rows="2"></textarea>--%>
                                       <asp:TextBox runat="server" CssClass="txtarea textamemo" ID="txtCatchCopy" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                    
                                </div>
                          </div>
                     </div>
                    <div class="row">
                        
                         <div class="floaddiv containerbox" style="width:285.19px; margin-right:10px;"> 
                             <div class="row" style="width:100%; margin-left:2px;">
                             <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">販売日</span>
                                        
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox"  ID="txtRelease_Date" ReadOnly="true" style="width: 88px;" runat="server" ></asp:TextBox>
                                 <asp:Image ID="ImageButton1" runat="server" Width="15px" Height="15px" ImageUrl="~/Styles/clear.png"  ImageAlign="AbsBottom"  Onclick="clrCtrl()"/>
                             </div>
                               <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">掲載可能日</span>
                                     
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtPost_Available_Date" ReadOnly="true" runat="server" style="width: 88px;"></asp:TextBox>
                                   <asp:Image ID="ImageButton2" runat="server" Width="15px" Height="15px" ImageUrl="~/Styles/clear.png"  ImageAlign="AbsBottom"  Onclick="clrCtrl1()"/>
                             </div>            
                             </div>
                             </div>
                     
                         <div class="floaddiv containerbox" style="width:239.01px; margin-right:10px;"> 
                              <div class="row" style="width:100%; margin-left:2px;">
                             <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">製品コード</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtProduct_Code" MaxLength="100" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                             </div>
                              <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">ブランドコード</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtBrand_Code" MaxLength="4"  runat="server" style="width: 100%;"></asp:TextBox>
                             </div>
                             </div>
                             </div>

                         <div class="floaddiv containerbox" style="width:218.23px; margin-right:10px;"> 
                              <div class="row" style="width:100%; margin-left:2px;">
                             <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">送料フラグ</span>
                                        </asp:Label>
                                       <%-- <asp:DropDownList ID="ddlShipping_Flag" style="width:100%;" runat="server"></asp:DropDownList>--%>
                                 <asp:DropDownList runat="server" ID="ddlShipping_Flag" style="width:100%;" >
                                 <asp:ListItem Value="0">送料別</asp:ListItem>
                                 <asp:ListItem Value="1">送料込</asp:ListItem>
                                 </asp:DropDownList>
                             </div>
                              <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">代引料フラグ</span>
                                        </asp:Label>
                                       <%-- <asp:DropDownList ID="ddlDelivery_Charges" style="width:100%;" runat="server"></asp:DropDownList>--%>
                              <asp:DropDownList runat="server" ID="ddlDelivery_Charges" style="width:100%;">
                                <asp:ListItem Value="0">代引料別</asp:ListItem>
                                <asp:ListItem Value="1">代引料込</asp:ListItem>
                                </asp:DropDownList>
                              </div>
                             </div>

                             </div>

                         <div class="floaddiv containerbox" style="width:255.19px;"> 
                             <div class="row" style="width:100%; margin-left:2px;">
                             <div class="floaddiv" style="width:45%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">個別送料</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtExtra_Shipping" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');" MaxLength="8"  runat="server" style="width: 80%;"></asp:TextBox>
                                 <span> 円</span>
                             </div>
                              <div class="floaddiv" style="width:55%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">Maker_Code</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtmaker_code"  runat="server" style="width: 100%;"></asp:TextBox>
                             </div>
                             </div>
                             </div>
                            
                    </div>

                   
                </section>
               <%--  <section id="sku" hidden>
                     <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl greenlable">SKU在庫</span>
                           </asp:Label>
                             <asp:GridView ID="gvSKU" runat="server"></asp:GridView>
                          </div>
                     </div>
                      
                 </section>--%>
                <section id="sku" hidden>
                     <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl greenlable">SKU在庫</span>
                           </asp:Label>
                             <div class="float-container">
                                  <div class="float-first-child">
                                      <br/>
                                      <input type="button" id = "btnAddSKU" onclick ="AddSKU(this)" value="AddSKU" runat="server"/>
                                      <br/>
                                      <dl style="margin-top:10px;">
                                          <dt style="padding-top: 4px;width: 80px;">SKU</dt>
                                          <dd style="margin-top:10px;">
                                              <asp:RadioButton runat="server" ID="rdb1" Text="あり" GroupName="groupsku" Enabled="false" CssClass="rdostyleclass"/>
                                              <asp:RadioButton runat="server" ID="rdb2" Text="なし" GroupName="groupsku" Enabled="false" CssClass="rdostyleclass"/>
                                          </dd>
                                      </dl>
                                  </div>
                                 <div class="float-second-child">
                                     <asp:GridView ID="gvSKU" CssClass="gvSKUStyle" runat="server"></asp:GridView>
                                 </div>
                             </div>
                          </div>
                     </div>                     
                 </section>
                 <section id="price" hidden> 
                       <div class="row rowprice">
                         <div class="col-md-4" style="padding-left: 1px;padding-right: 1px;"> 
                             <div class="col-md-12 containerbox"> 
                                 <p class="pprice">基本価格</p>
                                  <asp:UpdatePanel runat="server" id="UpdatePanel37" updatemode="Conditional"><ContentTemplate>
                                <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="listpricediv" style="width:146.88px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable lblrequired">定価（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtList_Price" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');" AutoPostBack="true" onchange="list_price_change(this)" runat="server" style="width: 80%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="salepricediv" style="width:146.88px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable lblrequired">原価（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtcost" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');" AutoPostBack="true" onchange="cost_change(this)" runat="server" style="width: 80%;"></asp:TextBox> <span> 円</span>
                                    </div>
                                </div> 
                                  </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtList_Price"/>
                                     <asp:Asyncpostbacktrigger controlid="txtcost"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                              </div>             
                          </div>
                        
                </div>
                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                             <p class="pprice">WEB</p>
                             <asp:UpdatePanel runat="server" id="UpdatePanel26" updatemode="Conditional"><ContentTemplate>
                            <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:174.85px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable lblrequired">販売価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtSale_Price" onchange="Web_calcuteTax(this)" AutoPostBack="true" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtprofitrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtdiscountrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtcostrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>   
                                 </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtSale_Price"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>             
                      </div>
                      <div class=" col-md-6">
                            <div class="col-md-12 containerbox"> 
                             <p class="pprice">自社</p>
                            <asp:UpdatePanel runat="server" id="UpdatePanel27" updatemode="Conditional"><ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:167.15px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtJishaPrice" onchange="Jisha_calcuteTax(this)" AutoPostBack="true" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:95.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtjishaProfitrate"  Enabled="false" runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:95.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtjishaDiscountrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:95.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtjishaCostrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>   
                                </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtJishaPrice"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>
                       </div>            
                </div>

                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                              <p class="pprice">楽天</p>
                              <asp:UpdatePanel runat="server" id="UpdatePanel28" updatemode="Conditional"><ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:174.85px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtRakutenPrice" onchange="Rakuten_calcuteTax(this)" AutoPostBack="true" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');" runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtrakutenProfitrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtrakutenDiscountrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtrakutenCostrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>     
                                  </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtRakutenPrice"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>             
                      </div>
                      <div class=" col-md-6">
                            <div class="col-md-12 containerbox"> 
                              <p class="pprice">Yahoo</p>
                                <asp:UpdatePanel runat="server" id="UpdatePanel29" updatemode="Conditional"><ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:167.15px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtYahooPrice" onchange="Yahoo_calcuteTax(this)" AutoPostBack="true"  oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtyahooProfitrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtyahooDiscountrate" Enabled="false" runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtyahooCostrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div> 
                                    </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtYahooPrice"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>   
                       </div>            
                </div>

                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                                <p class="pprice">Wowma</p>
                             <asp:UpdatePanel runat="server" id="UpdatePanel30" updatemode="Conditional"><ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:174.85px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtWowmaPrice" onchange="Wowma_calcuteTax(this)" AutoPostBack="true" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtwowmaProfitrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtwowmaDiscountrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtwowmaCostrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>  
                                 </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtWowmaPrice"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>             
                      </div>
                         
                </div>

                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                               <p class="pprice">モノタロウ</p>
                             <asp:UpdatePanel runat="server" id="UpdatePanel31" updatemode="Conditional"><ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:174.85px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtmonoprice" onchange="Monotarou_calcuteTax(this)" AutoPostBack="true"  oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtmonoprice_profitrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtmonoprice_discountrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtmonoprice_costrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div> 
                                 </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtmonoprice"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>             
                      </div>
                      <div class=" col-md-6">
                          <div class="col-md-12 containerbox"> 
                               <p class="pprice">ダイト</p>
                              <asp:UpdatePanel runat="server" id="UpdatePanel32" updatemode="Conditional"><ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:167.15px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtditeprice" onchange="Daito_calcuteTax(this)" AutoPostBack="true"  oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtditeprice_profitrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtditeprice_discountrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtditeprice_costrate"  Enabled="false" runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>    
                                  </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtditeprice"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>     
                       </div>            
                </div>

                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                              <p class="pprice">日本モーターパーツ</p>
                              <asp:UpdatePanel runat="server" id="UpdatePanel33" updatemode="Conditional"><ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:174.8px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtjapanmprice" onchange="Japanmotorpart_calcuteTax(this)" AutoPostBack="true"  oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtjapanmprice_profitrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtjapanmprice_discountrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:99.91px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtjapanmprice_costrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>  
                                  </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtjapanmprice"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>             
                      </div>

                          <div class="col-md-6" > 
                         <div class="col-md-12 containerbox"> 
                              <p class="pprice">柏木工機</p>
                                <asp:UpdatePanel runat="server" id="UpdatePanel34" updatemode="Conditional"><ContentTemplate>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1" style="width:167.15px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtkashiwagi" onchange="KashiwagiPrice_calcuteTax(this)" AutoPostBack="true"  oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtkashiwagi_profitrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtkashiwagi_discountrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2" style="width:95.51px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtkashiwagi_costrate" Enabled="false"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>
                                    </ContentTemplate> 
                                 <Triggers>
                                     <asp:Asyncpostbacktrigger controlid="txtkashiwagi"/>
                                 </Triggers>
                             </asp:UpdatePanel>
                          </div>             
                      </div>
                          
                </div>
                 </section>
                <section id="image" hidden> 
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                              <asp:Label CssClass=""  style="padding: 5px;" runat="server" >
                               <span class="label label-md lbl greenlable">商品画像</span>
                           </asp:Label>
                             <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                            <ContentTemplate>
                           <ul class="image_list">
                               <li>

                                 
                                   <asp:UpdatePanel runat="server" id="UpdatePaneimg1" updatemode="Conditional">
                                       <ContentTemplate>
                                       <asp:HyperLink rel="lightbox"  style="height: 180px;" runat="server" NavigateUrl ="~/Item_Image/no_image.jpg" ID="hlImage1">
                                           <asp:Image runat="server" ID="Image1" ImageUrl="~/Item_Image/no_image.jpg" />
                                       </asp:HyperLink>
                                           
                                           <%--<asp:FileUpload ID="FileUploadtest" runat="server" />
                                           <asp:Label ID="lblMessage" runat="server" Text="File uploaded successfully." ForeColor="Green" Visible="false" />
                                            <asp:Button ID="Button2" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>     
                                    <asp:TextBox ID="txtimg1" CssClass="txtbox" runat="server"></asp:TextBox> 
                                <asp:FileUpload ID="FileUpload1" Style="display: none" runat="server" accept=".jpg" onchange="upload()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput" value="ファイル選択"  onclick="showBrowseDialog()"/>
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete" value="削除"  onclick="Delete()" hidden/>
                                <asp:Button runat="server" ID="hideButton" Text="" Style="display: none;"  OnClick="UploadButton_Click" />
                            
                                </ContentTemplate> 
                                <Triggers>         
                                   
                                   <asp:PostBackTrigger controlid="hideButton"/>                 
                                </Triggers>
                                </asp:UpdatePanel>
                                   
                             
                               </li>
                               <li>
                                 <asp:UpdatePanel runat="server" id="UpdatePane2" updatemode="Conditional">
                                 <ContentTemplate>
                                   <asp:HyperLink rel="lightbox" style="height: 180px;" NavigateUrl ="~/Item_Image/no_image.jpg" runat="server" ID="hlImage2">
                                   <asp:Image runat="server" ID="Image2" ImageUrl ="~/Item_Image/no_image.jpg" /></asp:HyperLink>
                                   <asp:TextBox ID="txtimg2" CssClass="txtbox"  runat="server"></asp:TextBox>                                                           
                                  <%-- <asp:FileUpload ID="FileUpload2" Style="display: none" runat="server" onchange="upload2()" />
                                   <asp:Button runat="server" CssClass="btn btn-default imgchoosebtn" ID="imgbtn2" Text ="ファイル選択" OnClientClick="showBrowseDialog2()" />--%>
                                <asp:FileUpload ID="FileUpload2" Style="display: none" runat="server" accept=".jpg" onchange="upload2()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput2" value="ファイル選択"  onclick="showBrowseDialog2()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete2" value="削除"  onclick="Delete2()" hidden/>
                                <asp:Button runat="server" ID="imgbtn2_1" Text="" Style="display: none;" OnClick="UploadButton2_Click" />
                               </ContentTemplate> 
                                <Triggers>              
                                   <asp:PostBackTrigger controlid="imgbtn2_1"/>
                                    <%--<asp:PostBackTrigger controlid="imgbtn2_1"/>--%>                           
                               </Triggers>
                                </asp:UpdatePanel>
                               
                               
                               </li>
                               <li>
                                   
                                 <asp:UpdatePanel runat="server" id="UpdatePanel3" updatemode="Conditional">
                                 <ContentTemplate>    

                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage3"><asp:Image runat="server" ID="Image3" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>                
                                   <asp:TextBox ID="txtimg3" CssClass="txtbox" runat="server"></asp:TextBox>                                                                                             
                                <asp:FileUpload ID="FileUpload3" Style="display: none" accept=".jpg" runat="server" onchange="upload3()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput3" value="ファイル選択"  onclick="showBrowseDialog3()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete3" value="削除"  onclick="Delete3()" hidden/>
                                 <asp:Button runat="server" ID="imgbtn3_1" Text="" Style="display: none;" OnClick="UploadButton3_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn3_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                                
                               </li>
                               <li>
                               <asp:UpdatePanel runat="server" id="UpdatePanel4" updatemode="Conditional">
                                 <ContentTemplate>  
                                    <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage4"><asp:Image runat="server" ID="Image4" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                 
                                    <asp:TextBox ID="txtimg4" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                <asp:FileUpload ID="FileUpload4" Style="display: none" runat="server" accept=".jpg" onchange="upload4()" />                   
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput4" value="ファイル選択"  onclick="showBrowseDialog4()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete4" value="削除"  onclick="Delete4()" hidden/>
                                <asp:Button runat="server" ID="imgbtn4_1" Text="" Style="display: none;" OnClick="UploadButton4_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn4_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                               <li>
                                 
                                <asp:UpdatePanel runat="server" id="UpdatePanel5" updatemode="Conditional">
                                 <ContentTemplate> 
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage5"><asp:Image runat="server" ID="Image5" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                  
                                   <asp:TextBox ID="txtimg5" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                    <asp:FileUpload ID="FileUpload5" Style="display: none" accept=".jpg" runat="server" onchange="upload5()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput5" value="ファイル選択"  onclick="showBrowseDialog5()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete5" value="削除"  onclick="Delete5()" hidden/>
                                <asp:Button runat="server" ID="imgbtn5_1" Text="" Style="display: none;" OnClick="UploadButton5_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn5_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                 <asp:UpdatePanel runat="server" id="UpdatePanel6" updatemode="Conditional">
                                 <ContentTemplate>  
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage6"><asp:Image runat="server" ID="Image6" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>                             
                                   <asp:TextBox ID="txtimg6" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                  <asp:FileUpload ID="FileUpload6" Style="display: none" accept=".jpg" runat="server" onchange="upload6()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput6" value="ファイル選択"  onclick="showBrowseDialog6()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete6" value="削除"  onclick="Delete6()" hidden/>
                                <asp:Button runat="server" ID="imgbtn6_1" Text="" Style="display: none;" OnClick="UploadButton6_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn6_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                 <asp:UpdatePanel runat="server" id="UpdatePanel7" updatemode="Conditional">
                                 <ContentTemplate>  
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage7"><asp:Image runat="server" ID="Image7" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>                               
                                   <asp:TextBox ID="txtimg7" CssClass="txtbox"  runat="server"></asp:TextBox>                                                                
                                   <asp:FileUpload ID="FileUpload7" Style="display: none" accept=".jpg" runat="server" onchange="upload7()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput7" value="ファイル選択"  onclick="showBrowseDialog7()"/>
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete7" value="削除"  onclick="Delete7()" hidden/>
                                <asp:Button runat="server" ID="imgbtn7_1" Text="" Style="display: none;" OnClick="UploadButton7_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn7_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                 <asp:UpdatePanel runat="server" id="UpdatePanel8" updatemode="Conditional">
                                 <ContentTemplate>    
                                    <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage8"><asp:Image runat="server" ID="Image8" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                    <asp:TextBox ID="txtimg8" CssClass="txtbox"  runat="server"></asp:TextBox> 
                                   <asp:FileUpload ID="FileUpload8" Style="display: none" accept=".jpg" runat="server" onchange="upload8()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput8" value="ファイル選択"  onclick="showBrowseDialog8()"/>
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete8" value="削除"  onclick="Delete8()" hidden/>
                                <asp:Button runat="server" ID="imgbtn8_1" Text="" Style="display: none;" OnClick="UploadButton8_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn8_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                 <asp:UpdatePanel runat="server" id="UpdatePanel9" updatemode="Conditional">
                                 <ContentTemplate>   
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage9"><asp:Image runat="server" ID="Image9" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   <asp:TextBox ID="txtimg9" CssClass="txtbox"  runat="server"></asp:TextBox> 
                                   <asp:FileUpload ID="FileUpload9" Style="display: none" runat="server" accept=".jpg" onchange="upload9()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput9" value="ファイル選択"  onclick="showBrowseDialog9()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete9" value="削除"  onclick="Delete9()" hidden/>
                                <asp:Button runat="server" ID="imgbtn9_1" Text="" Style="display: none;" OnClick="UploadButton9_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn9_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>

                                 <asp:UpdatePanel runat="server" id="UpdatePanel10"  updatemode="Conditional">
                                 <ContentTemplate>  
                                    <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage10"><asp:Image runat="server" ID="Image10" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                    <asp:TextBox ID="txtimg10" CssClass="txtbox"  runat="server"></asp:TextBox> 
                                   <asp:FileUpload ID="FileUpload10" Style="display: none" accept=".jpg" runat="server" onchange="upload10()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput10" value="ファイル選択"  onclick="showBrowseDialog10()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete10" value="削除"  onclick="Delete10()" hidden/>
                                <asp:Button runat="server" ID="imgbtn10_1" Text="" Style="display: none;" OnClick="UploadButton10_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn10_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                               <asp:UpdatePanel runat="server" id="UpdatePanel11" updatemode="Conditional">
                               <ContentTemplate> 
                                 <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage11"><asp:Image runat="server" ID="Image11" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                 <asp:TextBox ID="txtimg11" CssClass="txtbox" runat="server"></asp:TextBox>                           
                                 <asp:FileUpload ID="FileUpload11" Style="display: none" accept=".jpg" runat="server" onchange="upload11()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput11" value="ファイル選択"  onclick="showBrowseDialog11()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete11" value="削除"  onclick="Delete11()" hidden/>
                                <asp:Button runat="server" ID="imgbtn11_1" Text="" Style="display: none;" OnClick="UploadButton11_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn11_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                               <asp:UpdatePanel runat="server" id="UpdatePanel12" updatemode="Conditional">
                               <ContentTemplate> 
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage12"><asp:Image runat="server" ID="Image12" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>                                 
                                   <asp:TextBox ID="txtimg12" CssClass="txtbox" runat="server"></asp:TextBox> 
                                  <asp:FileUpload ID="FileUpload12" Style="display: none" runat="server" accept=".jpg" onchange="upload12()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput12" value="ファイル選択"  onclick="showBrowseDialog12()"/>
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete12" value="削除"  onclick="Delete12()" hidden/>
                                <asp:Button runat="server" ID="imgbtn12_1" Text="" Style="display: none;" OnClick="UploadButton12_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn12_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                <asp:UpdatePanel runat="server" id="UpdatePanel13" updatemode="Conditional">
                                <ContentTemplate>
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage13"><asp:Image runat="server" ID="Image13" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   <asp:TextBox ID="txtimg13" CssClass="txtbox" runat="server"></asp:TextBox> 
                                   <asp:FileUpload ID="FileUpload13" Style="display: none" runat="server" accept=".jpg" onchange="upload13()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput13" value="ファイル選択"  onclick="showBrowseDialog13()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete13" value="削除"  onclick="Delete13()" hidden/>
                                <asp:Button runat="server" ID="imgbtn13_1" Text="" Style="display: none;" OnClick="UploadButton13_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn13_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                <asp:UpdatePanel runat="server" id="UpdatePanel14" updatemode="Conditional">
                                <ContentTemplate>
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage14"><asp:Image runat="server" ID="Image14" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>                   
                                   <asp:TextBox ID="txtimg14"  CssClass="txtbox" runat="server"></asp:TextBox> 
                                   <asp:FileUpload ID="FileUpload14" Style="display: none" accept=".jpg" runat="server" onchange="upload14()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput14" value="ファイル選択"  onclick="showBrowseDialog14()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete14" value="削除"  onclick="Delete14()" hidden/>
                                 <asp:Button runat="server" ID="imgbtn14_1" Text="" Style="display: none;" OnClick="UploadButton14_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn14_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                <asp:UpdatePanel runat="server" id="UpdatePanel15" updatemode="Conditional">
                                <ContentTemplate>
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage15"><asp:Image runat="server" ID="Image15" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   <asp:TextBox ID="txtimg15" CssClass="txtbox"  runat="server"></asp:TextBox> 
                                  <asp:FileUpload ID="FileUpload15" Style="display: none" runat="server" accept=".jpg" onchange="upload15()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput15" value="ファイル選択"  onclick="showBrowseDialog15()"/>
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete15" value="削除"  onclick="Delete15()" hidden/>
                                <asp:Button runat="server" ID="imgbtn15_1" Text="" Style="display: none;" OnClick="UploadButton15_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn15_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                <asp:UpdatePanel runat="server" id="UpdatePanel16" updatemode="Conditional">
                                <ContentTemplate>
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage16"><asp:Image runat="server" ID="Image16" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>             
                                   <asp:TextBox ID="txtimg16"  CssClass="txtbox" runat="server"></asp:TextBox> 
                                  <asp:FileUpload ID="FileUpload16" Style="display: none" runat="server" accept=".jpg" onchange="upload16()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput16" value="ファイル選択"  onclick="showBrowseDialog16()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete16" value="削除"  onclick="Delete16()" hidden/>
                                <asp:Button runat="server" ID="imgbtn16_1" Text="" Style="display: none;" OnClick="UploadButton16_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn16_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                <asp:UpdatePanel runat="server" id="UpdatePanel17" updatemode="Conditional">
                                <ContentTemplate>
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage17"><asp:Image runat="server" ID="Image17" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>              
                                   <asp:TextBox ID="txtimg17" CssClass="txtbox" runat="server"></asp:TextBox> 
                                   <asp:FileUpload ID="FileUpload17" Style="display: none" runat="server" accept=".jpg" onchange="upload17()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput17" value="ファイル選択"  onclick="showBrowseDialog17()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete17" value="削除"  onclick="Delete17()" hidden/>
                                <asp:Button runat="server" ID="imgbtn17_1" Text="" Style="display: none;" OnClick="UploadButton17_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn17_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                <asp:UpdatePanel runat="server" id="UpdatePanel18" updatemode="Conditional">
                                <ContentTemplate>
                                    <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage18"><asp:Image runat="server" ID="Image18" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                    <asp:TextBox ID="txtimg18" CssClass="txtbox" runat="server"></asp:TextBox> 
                                    <asp:FileUpload ID="FileUpload18" Style="display: none" runat="server" accept=".jpg" onchange="upload18()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput18" value="ファイル選択"  onclick="showBrowseDialog18()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete18" value="削除"  onclick="Delete18()" hidden/>
                                <asp:Button runat="server" ID="imgbtn18_1" Text="" Style="display: none;" OnClick="UploadButton18_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn18_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                               <asp:UpdatePanel runat="server" id="UpdatePanel19" updatemode="Conditional">
                                <ContentTemplate>
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage19"><asp:Image runat="server" ID="Image19" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   <asp:TextBox ID="txtimg19" CssClass="txtbox" runat="server"></asp:TextBox> 
                                  <asp:FileUpload ID="FileUpload19" Style="display: none" runat="server" accept=".jpg" onchange="upload19()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput19" value="ファイル選択"  onclick="showBrowseDialog19()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete19" value="削除"  onclick="Delete19()" hidden/>
                                <asp:Button runat="server" ID="imgbtn19_1" Text="" Style="display: none;" OnClick="UploadButton19_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn19_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                               </li>
                                <li>
                                <asp:UpdatePanel runat="server" id="UpdatePanel20" updatemode="Conditional">
                                <ContentTemplate>
                                   <asp:HyperLink rel="lightbox" runat="server" style="height: 180px;" ID="hlImage20"><asp:Image runat="server" ID="Image20" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>                
                                   <asp:TextBox ID="txtimg20" CssClass="txtbox" runat="server"  ></asp:TextBox> 
                                  <asp:FileUpload ID="FileUpload20" Style="display: none" runat="server" accept=".jpg" onchange="upload20()" />
                                <input type="button" class="btn btn-default imgchoosebtn" id ="imginput20" value="ファイル選択"  onclick="showBrowseDialog20()"/>
                                 <input type="button" class="btn btn-default imgchoosebtn" id ="imginputdelete20" value="削除"  onclick="Delete20()" hidden/>
                                <asp:Button runat="server" ID="imgbtn20_1" Text="" Style="display: none;" OnClick="UploadButton20_Click" />
                                </ContentTemplate> 
                                <Triggers>                                  
                                   <asp:PostBackTrigger controlid="imgbtn20_1"/>                           
                                </Triggers>
                                </asp:UpdatePanel>
                              </li>
                           </ul>
                                </ContentTemplate>
                    </asp:UpdatePanel>
                          </div>
                     </div>

                </section>
                <section id="option" hidden> 
                    <%--<div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                          
                            Option
                          </div>
                     </div>--%>
                     <div class="row" style="margin-top:5px;">
                     <div class="col-md-7" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                          <%--  <div class="rowfirst">--%>
                                <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style="margin-left:3px;font-weight:600;">オプション</span>
                           </asp:Label>
                             <br />
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                             <asp:Label CssClass="" style="margin-left:3px;" runat="server" >
                               <span class="label label-md lbl orangelable">テンプレート選択</span>
                           </asp:Label>
                             <br />
                             <asp:DropDownList ID="ddlOption" style="width:300px;margin-left:3px;" runat="server" OnSelectedIndexChanged="ddlOption_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                             <br />

                            
                               <div class="row" style="width:100%;margin-left: 3px;margin-right: 5px; margin-top:10px;">
                                <div class="floaddiv" style="width:30%;">
                                     <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl orangelable">項目名</span>
                           </asp:Label>
                                    <asp:TextBox CssClass="txtbox" ID="txtOptionName1" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtOptionName2" onkeypress="return isNumberKeys(event)" runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtOptionName3" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                   
                                </div>
                                <div class="floaddiv" style="width:70%;">
                                    <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl orangelable">選択肢</span>
                           </asp:Label>
                                    <asp:TextBox CssClass="txtbox" ID="txtOptionValue1" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtOptionValue2" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtOptionValue3" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                    </div>
                                   </div>
                       </ContentTemplate>
                    </asp:UpdatePanel>
                          </div>             
                      </div>
                        <div class=" col-md-5">
                            <div class=" col-md-12 containerboxmemo" style="padding-left: 10px;padding-right: 10px;">
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">ショップカテゴリ</span>
                           </asp:Label>   
                                <br />
                             <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl orangelable">カテゴリ</span>
                           </asp:Label>
                                <asp:UpdatePanel ID="UpdatePanel21" updatemode="Conditional" runat="server">
                                <ContentTemplate>
                                 <asp:GridView ID="gvCatagories" runat="server" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
                                <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                <asp:Label runat="server" ID="lblID" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox CssClass="txtbox" style="width: 100%; margin-bottom:10px;" runat="server" ID="txtCTGName" onkeypress="return isNumberKeys(event)"/>
                                <asp:Label runat="server" ID="lblCTGName" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                </asp:GridView>
                                 </ContentTemplate>
                                <Triggers>
                                    <asp:postbacktrigger ControlID="btnAddCatagories"/>
                                </Triggers>
                                </asp:UpdatePanel>
                                <input type="button" id = "btnAddCatagories" onclick ="ShowCatagoryList(this)" value="選択" runat="server" style="width:100px;margin-left:6px;border-radius: 8px;"/>
                            
                                    </div>
                            </div>
                        
                </div>
                </section>
                <section id="malldata" hidden> 
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">カテゴリID</span>
                           </asp:Label>   
                             <div class="row">
                                 <div class="floaddiv" style="width:49%; margin-left:10px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">楽天</span>
                                        </asp:Label>
                                     <br />
                                <asp:UpdatePanel ID="UpdatePanel22" updatemode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox CssClass="txtbox" ID="txtRakuten_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRakuten_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 300px; margin-bottom:10px;"></asp:TextBox>                                   
                                 
                                     <input type="button" id ="btnRakuten_CategoryID" onclick ="ShowMallCategory(1, this)" runat="server" value="選択" style="width:50px;border-radius: 8px;" />
                                    </ContentTemplate>
                                <Triggers>
                                    <asp:postbacktrigger ControlID="btnRakuten_CategoryID"/>
                                </Triggers>
                                </asp:UpdatePanel>  
                                    <br />
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">Wowma</span>
                                        </asp:Label>
                                   <br />
                                <asp:UpdatePanel ID="UpdatePanel23" updatemode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox CssClass="txtbox" ID="txtWowma_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtWowma_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 300px; margin-bottom:10px;"></asp:TextBox>                                   
                                 
                                    <input type="button" id ="btnWowma_CategoryID" onclick ="ShowMallCategory(4,this)" runat="server" value="選択" style="width:50px;border-radius: 8px;" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:postbacktrigger ControlID="btnWowma_CategoryID"/>
                                </Triggers>
                                </asp:UpdatePanel>  
                                    </div>
                                 <div class="floaddiv" style="width:50%;">
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">Yahoo</span>
                                        </asp:Label>
                                     <br />
                                 <asp:UpdatePanel ID="UpdatePanel24" updatemode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox CssClass="txtbox" ID="txtYahoo_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtYahoo_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 304px; margin-bottom:10px;"></asp:TextBox>                                   
                                     
                                    <input type="button" id ="btnYahoo_CategoryID" onclick ="ShowMallCategory(2,this)" runat="server" value="選択" style="width:50px;border-radius: 8px;" />
                                     </ContentTemplate>
                                <Triggers>
                                    <asp:postbacktrigger ControlID="btnYahoo_CategoryID"/>
                                </Triggers>
                                </asp:UpdatePanel> 
                                    <br />
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">ヤフースペック値</span>
                                        </asp:Label>
                                   <br />
                                <asp:UpdatePanel ID="UpdatePanel25" updatemode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox CssClass="txtbox" ID="txtYahooValue1" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtYahooValue2" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>  
                                     <asp:TextBox CssClass="txtbox" ID="txtYahooValue3"  ReadOnly="true" onkeypress="return isNumberKeys(event)" runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtYahooValue4" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>     
                                     <asp:TextBox CssClass="txtbox" ID="txtYahooValue5" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>
                                      
                                    <input type="button" id = "imgbYahooSpecValue" onclick ="ShowYahooSpecValue(this)" runat="server" value="選択" style="width:50px;border-radius: 8px;" />
                                      </ContentTemplate>
                                <Triggers>
                                    <asp:postbacktrigger ControlID="imgbYahooSpecValue"/>
                                </Triggers>
                                </asp:UpdatePanel>  
                                 </div>
                             </div>
                          </div>
                     </div>

                     <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">出店対象ショップ</span>
                           </asp:Label>   <br />
                              <asp:DataList ID="dlShop1" runat="server" RepeatDirection="Vertical" RepeatColumns="4">
                                <ItemTemplate>
                                    <p>
                                        <asp:Label runat="server" ID="lblShopID1" Text='<%# Bind("ID")%>' Visible="false"/>
                                        <asp:CheckBox runat="server" CssClass="mycheckbox" ID="ckbShopName" Text='<%# Bind("ORS_Shop_Name")%>'/>
                                    </p>
                                  </ItemTemplate>
                             </asp:DataList>
                             </div>
                         </div>
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">アイテムコード（旧商品用）</span>
                           </asp:Label>  
                             <br />
                             <asp:DataList ID="dlShop" runat="server" RepeatDirection="Vertical" RepeatColumns="4" >
                                 <ItemTemplate >
                                     <asp:CheckBox runat="server" ID="ckbShop" EnableViewState="true" Visible="false" />
                                     <asp:Label runat="server" ID="lblShopID" Text='<%# Bind("ID")%>' Visible="false"/>
                                     <asp:Label class="label label-md lbl orangelable" runat="server" ID="lblShopName" Text='<%# Bind("ORS_Shop_Name")%>'/>
                                     <asp:TextBox ID="txtItem_CodeList" runat="server" Text='<%# Bind("Item_Code_URL")%>' style="width: 100%; margin-bottom:10px;"/>
                                 </ItemTemplate>
                             </asp:DataList>
                           
                             </div>
                         </div>
                </section>
                <section id="setting" hidden> 
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox" style="padding-left:16px;"> 
                           <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">商品情報</span>
                           </asp:Label>  
                             <br />

                            <div class="rowfirst">
                                 <div class="floaddiv" style="width:60%;" >
                                     <div class="row">
                                          <div class="floaddiv" style="width: 307.05px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">ブランド名</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtBrand_Name" MaxLength="40"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                            <div class="floaddiv" style="width:141.24px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">市場売価</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtsellingprice" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 80%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="floaddiv" style="width:141.24px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">仕入価格</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtpurchaseprice" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 80%;"></asp:TextBox> <span> 円</span>
                                    </div>

                                     </div>
                                     <div class="row">
                                         <div class="floaddiv" style="width:307.06px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">賞味期限</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtsellby" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                            <div class="floaddiv" style="width:294.77px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">年間出荷数もしくは売れ筋A～Dランク</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtsellingrank" MaxLength="20"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                     </div>
                              
                              </div>
                              <div class="floaddiv" style="min-width:398px;max-width:400px;" >
                             <%--  <div class="" style="padding:5px;">    --%>  
                            <asp:Label  ID="Label7" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable ">商品詳細登録コメント</span>
                            </asp:Label>   <br />                       
                           <%-- <textarea class="txtarea textamemo" style="height: 95px;" ID="txtcomment" cols="20" rows="2"></textarea>--%>
                           <asp:TextBox ID="txtcomment" class="txtarea textamemo" style="height: 95px;" runat="server" Width="388px" MaxLength="40" TextMode="MultiLine"></asp:TextBox>
                                  <%-- </div> --%>
                                </div>
                          </div>
                             <div class="row" style="padding-left: 5px;padding-right: 5px;">
                                 <div class="floaddiv" style="width:101.41px;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">公開種別</span>
                                        </asp:Label>
                                      <asp:DropDownList ID="ddlPublicationType" style="width:100%;" runat="server"></asp:DropDownList>
                                        
                                 </div>
                                   <div class="floaddiv" style="width:101.41px;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">最低発注数量</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtminimumorderquantity" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:101.41px;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">最低発注単位</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtminimumorderunit" MaxLength="20"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:101.41px;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">直送可否</span>
                                        </asp:Label>
                                         <asp:DropDownList ID="ddlDirectDelivery" style="width:100%;" runat="server"></asp:DropDownList>
                                        
                                 </div>
                                   <div class="floaddiv" style="width:131.84px;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">公開予定日</span>
                                        </asp:Label>
                                       
                                        <asp:TextBox CssClass="txtbox" ReadOnly="true" ID="txtreleasedatemonotaro"  runat="server" style="width: 67%; margin-right: 3px;"></asp:TextBox>
                                        <asp:Image ID="Image21" runat="server" Width="15px" Height="15px" ImageUrl="~/Styles/clear.png"  ImageAlign="AbsBottom"  Onclick="clrCtrlmono()"/>
                                 </div>
                                   <div class="floaddiv" style="width:234.4px;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">カテゴリ</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtmonocategory" MaxLength="200"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:233.26px;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">カラー</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtcolour" MaxLength="40"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                             </div>
                             <div class="row" style="padding-left: 5px;padding-right: 5px;">
                                  <div class="floaddiv" style="width:100%;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable" style="width:54.09px">参考URL</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtReferenceURL" MaxLength="500"  runat="server" style="width: 990px;"></asp:TextBox>
                                 </div>
                             </div>
                     </div>
                        </div>
                    <div class="row">
                     <div class="col-md-7" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                                <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">配送</span>
                           </asp:Label>  
                             <br />
                            <div class="rowfirst">
                              <div class="floaddiv" style="width:117.24px;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">引取方法</span>
                           </asp:Label>
                          <asp:DropDownList ID="ddldeliverymethod" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                              <div class="floaddiv" style="width:87.93px;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">配送種別</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddldeliverytype" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>
                              <div class="floaddiv" style="width:87.93px;">
                                      
                            <asp:Label  ID="Label2" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">入荷日数</span>
                            </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="txtdeliverydays" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');" runat="server"></asp:TextBox>
                              </div>

                                       <div class="floaddiv" style="width:117.24px;">
                                      
                            <asp:Label  ID="Label3" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">代引可否</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddldeliveryfees" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>

                                       <div class="floaddiv" style="width:117.24px;">
                                      
                            <asp:Label  ID="Label4" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">返品可否</span>
                            </asp:Label>
                         <asp:DropDownList ID="ddlreturnableitem" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                            </div>
                         
                          </div>             
                      </div>
                        <div class=" col-md-5">
                            <div class=" col-md-12 containerboxmemo" style="padding-left: 10px;padding-right: 10px;">
                            <p class="pprice">笠間納品</p>
                                <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="listpricediv" style="width:174.15px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">可否</span>
                                        </asp:Label>
                                         <asp:DropDownList ID="ddlksmavaliable" style="width:100%;" runat="server"></asp:DropDownList>

                                    </div>
                                    <div class="salepricediv" style="width:174.15px">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">入荷日数</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtksmdeliverydays" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 80%;"></asp:TextBox>
                                    </div>
                                </div>                           
                              </div>             
                                </div>
                            </div>
                        
               <%-- </div>--%>
                   
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">法令</span>
                           </asp:Label>  
                             <br />
                            <div class="rowfirst">
                              <div class="floaddiv" style="width:302.46px;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">該当法令</span>
                           </asp:Label>
                          <asp:DropDownList ID="ddlnoapplicablelaw" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                              <div class="floaddiv" style="width:302.46px;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">販売許可・認可・届出</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddlsalespermission" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>
                              <div class="floaddiv" style="width:302.46px;">
                                      
                            <asp:Label  ID="Label5" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">法令・規格</span>
                            </asp:Label>
                             <asp:DropDownList ID="ddllaw" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                                     
                            </div>
                         
                          </div>
                     </div>

                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                            <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">地域設定</span>
                           </asp:Label>  
                             <br />
                             <div class="rowfirst">
                              <div class="floaddiv" style="width:171.39px;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">全国</span>
                           </asp:Label>
                          <asp:TextBox CssClass="txtbox" ID="txtnationwide" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                              </div>
                              <div class="floaddiv" style="width:171.39px;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">北海道</span>
                            </asp:Label>
                          <asp:TextBox CssClass="txtbox" ID="txthokkaido" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                            
                              </div>

                                  <div class="floaddiv" style="width:171.39px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">沖縄</span>
                                        </asp:Label>
                                       <asp:TextBox CssClass="txtbox" ID="txtokinawa" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>

                                  <div class="floaddiv" style="width:171.39px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">離島</span>
                                        </asp:Label>
                                    <asp:TextBox CssClass="txtbox" ID="txtremoteisland" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                <div class="floaddiv" style="width:312.54px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">直送時配送不可地域</span>
                                        </asp:Label>
                                       <asp:TextBox CssClass="txtbox" ID="txtundeliveredarea" MaxLength="40"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>               
                            </div>
                          </div>
                     </div>
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                             <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">危険物</span>
                           </asp:Label>  
                             <br />
                             <div class="rowfirst">
                              <div class="floaddiv" style="width:191.55px;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">危険物の含有量</span>
                           </asp:Label>
                          <asp:TextBox CssClass="txtbox" ID="txtdangerousgoodscontents" MaxLength="40" runat="server" style="width: 100%;"></asp:TextBox>
                              </div>
                              <div class="floaddiv" style="width:191.55px;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">危険物の種別</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddldanggoodsclass" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>

                                  <div class="floaddiv" style="width:201.64px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">危険物の品名</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddldanggoodsname" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>

                                  <div class="floaddiv" style="width:201.64px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">危険等級</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlriskrating" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                <div class="floaddiv" style="width:201.64px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">危険物の性質</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddldanggoodsnature" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>               
                            </div>
                          </div>
                     </div>
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">SDGs関連</span>
                           </asp:Label>  
                             <br />
                             <div class="rowfirst">
                              <div class="floaddiv" style="width:110.9px;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">グリーン購入法</span>
                           </asp:Label>
                          <asp:DropDownList ID="ddlgreenpurchasemethod" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                              <div class="floaddiv" style="max-width: 227.89px;min-width: 220px;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">特定調達品目</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddlSpecifiedprocurementitem" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>

                                  <div class="floaddiv" style="width:131.06px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">エコマーク認定品</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlecomartcertifiedproduct" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>

                                  <div class="floaddiv" style="width:141.14px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">エコマーク認定番号</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtecomartcertifiednumber" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                <div class="floaddiv" style="width:131.06px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">RoHS指令</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlRoHSdirective" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>    
                                  <div class="floaddiv" style="width:131.06px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">JIS適合</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlJISConform" style="width:100%;" runat="server">
                                        </asp:DropDownList>
                                    </div>  
                                  <div class="floaddiv" style="width:131.06px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">ISO適合</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlISOConform" style="width:100%;" runat="server">
                                        </asp:DropDownList>
                                    </div>  
                            </div>
                          </div>
                     </div>
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                                                 
                            <div class="rowfirst">
                              <div class="floaddiv" style="width:201.64px;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">お客様組立て</span>
                           </asp:Label>
                          <asp:DropDownList ID="ddlcustomerassembly" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                              <div class="floaddiv" style="width:252.05px;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">消防法上、届出を必要とする物質</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddlfirelaw" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>

                                  <div class="floaddiv" style="width:100.81px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">倉庫コード</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtwarehouse_code" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>

                                  <div class="floaddiv" style="width:100.81px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">出荷日数</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtday_ship" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                <div class="floaddiv" style="width:100.81px;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">返品承認要否</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtreturn_necessary" MaxLength="40" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>

                              <div class="floaddiv" style="width:151.23px;">
                                      
                            <asp:Label  ID="Label6" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">医薬品・医療機器</span>
                            </asp:Label>
                             <asp:DropDownList ID="ddlPharmaceuticalsandmedicaldevices" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                                     
                            </div>
                          </div>
                     </div>
<%--</div>--%>
                </section>
                <div class="row rowbtn" style="margin-bottom: 40px;">
                    <div class="col-md-2 btncolum">      
                        <input type="button" class="mainbtnbox btndecoraction" id="btnCopy" onclick="ShowCopy(this)" value="複製コピー" runat="server"  />

                    </div>
                    <div class="col-md-2 btncolum">                             
                        <asp:Button CssClass="mainbtnbox btndecoraction" runat="server" ID="btnPreview" Text="プレビュー" onclick="btnPreview_Click"  />                                 
                    </div>
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" OnClientClick="SaveClick()" OnClick="btnSave_Click" runat="server" ID="btnSave" Text="登 録" />               
                    </div>
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" OnClick="btnComplete_Click"  runat="server" ID="btnComplete" Text="出品待ち"  />               
                    </div>
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" runat="server" ID="btnToCancelExhibit"  OnClientClick ="Confirm()" onclick="btnToCancelExhibit_Click" Text="出品待ち取消し" />              
                    </div>
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" OnClientClick ="Confirm()" OnClick="btnDelete_Click" runat="server" ID="btnDelete" Text="削除" />                
                    </div>
                 </div>
            </div>
        </div>
        </div>
  
 <%--<script src="js/lightbox.min.js" type="text/javascript"></script>--%>
 <script>
    
     $(document).ready(function () {
         //$('#master').show();
         //$('#sku').hide();
         //$('#price').hide();
         //$('#image').hide();
         //$('#option').hide();
         //$('#malldata').hide();
         //$('#setting').hide(); 
         $('#tohide').hide();
         $('#tohide1').hide();
         $('#tohide2').hide();
         $('#tohide3').hide();
         $('#tohide4').hide();
         $('#tohidet').hide();
         $('#tohidet1').hide();
         $('#tohidet2').hide();
         $('#tohidet3').hide();
         $('#tohidet4').hide();


         var tab = document.getElementById('<%= hdfTab.ClientID%>').value;
         
         if (tab != '' && tab != null) {
             //alert(tab);
             if (tab == 'amaster' ||
                 tab == 'asku' ||
                 tab == 'aprice' ||
                 tab == 'aimage' ||
                 tab == 'aoption' ||
                 tab == 'amalldata' ||
                 tab == 'asetting') {
                 refreshTab(tab);
               
             }
         }

         var img1 = document.getElementById('<%= txtimg1.ClientID %>')
        
         if (img1.value != '' && img1.value != null) {

             document.getElementById("imginputdelete").hidden = false;
             document.getElementById("imginput").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete").hidden = true;
             document.getElementById("imginput").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage1.ClientID %>');
             var image1 = document.getElementById('<%= Image1.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img2 = document.getElementById('<%= txtimg2.ClientID %>')
        
         if (img2.value != '' && img2.value != null) {
            
             document.getElementById("imginputdelete2").hidden = false;
             document.getElementById("imginput2").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete2").hidden = true;
             document.getElementById("imginput2").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage2.ClientID %>');
             var image1 = document.getElementById('<%= Image2.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img3 = document.getElementById('<%= txtimg3.ClientID %>')
        
         if (img3.value != '' && img3.value != null) {
           
             document.getElementById("imginputdelete3").hidden = false;
             document.getElementById("imginput3").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete3").hidden = true;
             document.getElementById("imginput3").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage3.ClientID %>');
             var image1 = document.getElementById('<%= Image3.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img4 = document.getElementById('<%= txtimg4.ClientID %>')
        
         if (img4.value != '' && img4.value != null) {
          
            document.getElementById("imginputdelete4").hidden = false;
             document.getElementById("imginput4").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete4").hidden = true;
             document.getElementById("imginput4").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage4.ClientID %>');
             var image1 = document.getElementById('<%= Image4.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }
         var img5 = document.getElementById('<%= txtimg5.ClientID %>')
        
         if (img5.value != '' && img5.value != null) {
         
             document.getElementById("imginputdelete5").hidden = false;
             document.getElementById("imginput5").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete5").hidden = true;
             document.getElementById("imginput5").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage5.ClientID %>');
             var image1 = document.getElementById('<%= Image5.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img6 = document.getElementById('<%= txtimg6.ClientID %>')
        
         if (img6.value != '' && img6.value != null) {
           
             document.getElementById("imginputdelete6").hidden = false;
             document.getElementById("imginput6").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete6").hidden = true;
             document.getElementById("imginput6").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage6.ClientID %>');
             var image1 = document.getElementById('<%= Image6.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }  

         var img7 = document.getElementById('<%= txtimg7.ClientID %>')
        
         if (img7.value != '' && img7.value != null) {
           
            document.getElementById("imginputdelete7").hidden = false;
             document.getElementById("imginput7").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete7").hidden = true;
             document.getElementById("imginput7").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage7.ClientID %>');
             var image1 = document.getElementById('<%= Image7.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img8 = document.getElementById('<%= txtimg8.ClientID %>')
        
         if (img8.value != '' && img8.value != null) {
         
            document.getElementById("imginputdelete8").hidden = false;
             document.getElementById("imginput8").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete8").hidden = true;
             document.getElementById("imginput8").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage8.ClientID %>');
             var image1 = document.getElementById('<%= Image8.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img9 = document.getElementById('<%= txtimg9.ClientID %>')
        
         if (img9.value != '' && img9.value != null) {
         
             document.getElementById("imginputdelete9").hidden = false;
             document.getElementById("imginput9").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete9").hidden = true;
             document.getElementById("imginput9").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage9.ClientID %>');
             var image1 = document.getElementById('<%= Image9.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img10 = document.getElementById('<%= txtimg10.ClientID %>')
        
         if (img10.value != '' && img10.value != null) {
         
             document.getElementById("imginputdelete10").hidden = false;
             document.getElementById("imginput10").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete10").hidden = true;
             document.getElementById("imginput10").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage10.ClientID %>');
             var image1 = document.getElementById('<%= Image10.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img11 = document.getElementById('<%= txtimg11.ClientID %>')
        
         if (img11.value != '' && img11.value != null) {
         
             document.getElementById("imginputdelete11").hidden = false;
             document.getElementById("imginput11").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete11").hidden = true;
             document.getElementById("imginput11").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage11.ClientID %>');
             var image1 = document.getElementById('<%= Image11.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img12 = document.getElementById('<%= txtimg12.ClientID %>')
        
         if (img12.value != '' && img12.value != null) {
         
            document.getElementById("imginputdelete12").hidden = false;
             document.getElementById("imginput12").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete12").hidden = true;
             document.getElementById("imginput12").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage12.ClientID %>');
             var image1 = document.getElementById('<%= Image12.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img13 = document.getElementById('<%= txtimg13.ClientID %>')
        
         if (img13.value != '' && img13.value != null) {
         
            document.getElementById("imginputdelete13").hidden = false;
             document.getElementById("imginput13").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete13").hidden = true;
             document.getElementById("imginput13").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage13.ClientID %>');
             var image1 = document.getElementById('<%= Image13.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img14 = document.getElementById('<%= txtimg14.ClientID %>')
        
         if (img14.value != '' && img14.value != null) {
         
             document.getElementById("imginputdelete14").hidden = false;
             document.getElementById("imginput14").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete14").hidden = true;
             document.getElementById("imginput14").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage14.ClientID %>');
             var image1 = document.getElementById('<%= Image14.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img15 = document.getElementById('<%= txtimg15.ClientID %>')
        
         if (img15.value != '' && img15.value != null) {
         
            document.getElementById("imginputdelete15").hidden = false;
             document.getElementById("imginput15").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete15").hidden = true;
             document.getElementById("imginput15").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage15.ClientID %>');
             var image1 = document.getElementById('<%= Image15.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img16 = document.getElementById('<%= txtimg16.ClientID %>')
        
         if (img16.value != '' && img16.value != null) {
         
            document.getElementById("imginputdelete16").hidden = false;
             document.getElementById("imginput16").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete16").hidden = true;
             document.getElementById("imginput16").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage16.ClientID %>');
             var image1 = document.getElementById('<%= Image16.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img17 = document.getElementById('<%= txtimg17.ClientID %>')
        
         if (img17.value != '' && img17.value != null) {
         
             document.getElementById("imginputdelete17").hidden = false;
             document.getElementById("imginput17").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete17").hidden = true;
             document.getElementById("imginput17").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage17.ClientID %>');
             var image1 = document.getElementById('<%= Image17.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img18 = document.getElementById('<%= txtimg18.ClientID %>')
        
         if (img18.value != '' && img18.value != null) {
         
             document.getElementById("imginputdelete18").hidden = false;
             document.getElementById("imginput18").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete18").hidden = true;
             document.getElementById("imginput18").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage18.ClientID %>');
             var image1 = document.getElementById('<%= Image18.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img19 = document.getElementById('<%= txtimg19.ClientID %>')
        
         if (img19.value != '' && img19.value != null) {
         
             document.getElementById("imginputdelete19").hidden = false;
             document.getElementById("imginput19").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete19").hidden = true;
             document.getElementById("imginput19").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage19.ClientID %>');
             var image1 = document.getElementById('<%= Image19.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

         var img20 = document.getElementById('<%= txtimg20.ClientID %>')
        
         if (img20.value != '' && img20.value != null) {
         
             document.getElementById("imginputdelete20").hidden = false;
             document.getElementById("imginput20").hidden = true;
            
         }
         else {
             document.getElementById("imginputdelete20").hidden = true;
             document.getElementById("imginput20").hidden = false;
             var hlImage1 = document.getElementById('<%= hlImage20.ClientID %>');
             var image1 = document.getElementById('<%= Image20.ClientID %>');
             hlImage1.href = '../../Item_Image/no_image.jpg';
             image1.src = '../../Item_Image/no_image.jpg';
         }

          $("#<%=txtreleasedatemonotaro.ClientID %>").datepicker(
            { 
            showOn: 'button',
            dateFormat: 'yy/mm/dd ',
            buttonImageOnly: true,
            buttonImage:'../../images/calendar.gif',
            changeMonth: true,
            changeYear: true,
            yearRange: "1900:2030" ,
            }
            );
            $(".ui-datepicker-trigger").mouseover(function () {
            $(this).css('cursor', 'pointer');
            });
    

     });

 function refreshTab(id)
 {
         if (id == 'amaster') {
             var anchorsArray = document.querySelectorAll('.aitem');
             for (var i = 0; i < anchorsArray.length; i++) {
                 anchorsArray[i].classList.remove("act");

             }
             var ele = document.getElementById("amaster");
             ele.classList.add("act")
             $('#master').show();
             $('#sku').hide();
             $('#price').hide();
             $('#image').hide();
             $('#option').hide();
             $('#malldata').hide();
             $('#setting').hide();
         }
         if (id == 'asku') {
             var anchorsArray = document.querySelectorAll('.aitem');
             for (var i = 0; i < anchorsArray.length; i++) {
                 anchorsArray[i].classList.remove("act");

             }
             var ele = document.getElementById("asku");
             ele.classList.add("act")
             $('#master').hide();
             $('#sku').show();
             $('#price').hide();
             $('#image').hide();
             $('#option').hide();
             $('#malldata').hide();
             $('#setting').hide();
             var sec = document.getElementById('sku');
             sec.removeAttribute("hidden");
         }
         if (id == 'aprice') {
             var anchorsArray = document.querySelectorAll('.aitem');
             for (var i = 0; i < anchorsArray.length; i++) {
                 anchorsArray[i].classList.remove("act");

             }
             var ele = document.getElementById("aprice");
             ele.classList.add("act")
             $('#master').hide();
             $('#sku').hide();
             $('#price').show();
             $('#image').hide();
             $('#option').hide();
             $('#malldata').hide();
             $('#setting').hide();
             var sec = document.getElementById('price');
             sec.removeAttribute("hidden");
         }
         if (id == 'aimage') {
             var anchorsArray = document.querySelectorAll('.aitem');
             for (var i = 0; i < anchorsArray.length; i++) {
                 anchorsArray[i].classList.remove("act");

             }
             var ele = document.getElementById("aimage");
             ele.classList.add("act")
             $('#master').hide();
             $('#sku').hide();
             $('#price').hide();
             $('#image').show();
             $('#option').hide();
             $('#malldata').hide();
             $('#setting').hide();
             var sec = document.getElementById('image');
             sec.removeAttribute("hidden");

         }
         if (id == 'aoption') {
             var anchorsArray = document.querySelectorAll('.aitem');
             for (var i = 0; i < anchorsArray.length; i++) {
                 anchorsArray[i].classList.remove("act");

             }
             var ele = document.getElementById("aoption");
             ele.classList.add("act")
             $('#master').hide();
             $('#sku').hide();
             $('#price').hide();
             $('#image').hide();
             $('#option').show();
             $('#malldata').hide();
             $('#setting').hide();
             var sec = document.getElementById('option');
             sec.removeAttribute("hidden");
         }
         if (id == 'amalldata') {
             var anchorsArray = document.querySelectorAll('.aitem');
             for (var i = 0; i < anchorsArray.length; i++) {
                 anchorsArray[i].classList.remove("act");

             }
             var ele = document.getElementById("amalldata");
             ele.classList.add("act")
             $('#master').hide();
             $('#sku').hide();
             $('#price').hide();
             $('#image').hide();
             $('#option').hide();
             $('#malldata').show();
             $('#setting').hide();
             var sec = document.getElementById('malldata');
             sec.removeAttribute("hidden");
         }
         if (id == 'asetting') {
             var anchorsArray = document.querySelectorAll('.aitem');
             for (var i = 0; i < anchorsArray.length; i++) {
                 anchorsArray[i].classList.remove("act");

             }
             var ele = document.getElementById("asetting");
             ele.classList.add("act")
             $('#master').hide();
             $('#sku').hide();
             $('#price').hide();
             $('#image').hide();
             $('#option').hide();
             $('#malldata').hide();
             $('#setting').show();
             var sec = document.getElementById('setting');
             sec.removeAttribute("hidden");
         }

       //  eventRef.preventDefault();
      //   return true;
     }

  $(document).keydown(function (event) {
         var charCode = (window.event) ? event.keyCode : event.which;
         if (charCode == 38 && event.ctrlKey) {
             var id = $('.act').attr('id');
             handleKeyDownEvent(id, event);
         }
         if (charCode == 40 && event.ctrlKey) {
             var id = $('.act').attr('id');
             handleKeyDownEvent(id, event);
         }
     });

    function changeStyle(sender) {
    var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        sender.classList.add("act")
  
     }

     $('#amaster').click(function() {
        $('#master').show();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "amaster";
          //var sec = document.getElementById('malldata');
          //sec.removeAttribute("hidden"); 
     });

      $('#asku').click(function() {
        $('#master').hide();
         $('#sku').show();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
          $('#setting').hide();  
           var sec = document.getElementById('sku');
          sec.removeAttribute("hidden");
          document.getElementById('<%=hdfTab.ClientID %>').value = "asku";
     });

      $('#aprice').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').show();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
          $('#setting').hide(); 
           var sec = document.getElementById('price');
          sec.removeAttribute("hidden"); 
          document.getElementById('<%=hdfTab.ClientID %>').value = "aprice";
     });

      $('#aimage').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').show();
         $('#option').hide();
         $('#malldata').hide();
          $('#setting').hide(); 
           var sec = document.getElementById('image');
          sec.removeAttribute("hidden"); 
          document.getElementById('<%=hdfTab.ClientID %>').value = "aimage";
     });

      $('#aoption').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').show();
         $('#malldata').hide();
          $('#setting').hide(); 
           var sec = document.getElementById('option');
          sec.removeAttribute("hidden"); 
          document.getElementById('<%=hdfTab.ClientID %>').value = "aoption";
     });

      $('#amalldata').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').show();
          $('#setting').hide(); 
          var sec = document.getElementById('malldata');
          sec.removeAttribute("hidden"); 
          document.getElementById('<%=hdfTab.ClientID %>').value = "amalldata";
     });

      $('#asetting').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
          $('#malldata').hide();
          $('#setting').show(); 
          var sec = document.getElementById('setting');
          sec.removeAttribute("hidden"); 
          $('#setting').show(); 
          document.getElementById('<%=hdfTab.ClientID %>').value = "asetting";

     });

     $('#<%= dis.ClientID %>').click(function (event) {
         event.preventDefault();

         if ((document.getElementById('iplus').classList.contains('nonactive')) == true) {
             $('#tohide').hide();
             $('#tohide1').hide();
             $('#tohide2').hide();
             $('#tohide3').hide();
             $('#tohide4').hide();
             document.getElementById('iplus').classList.remove("nonactive");
             iminus.classList.add('nonactive');
        
         }
         else {
           
             document.getElementById('iminus').classList.remove("nonactive");
             iplus.classList.add('nonactive');
           
             $('#tohide').show();
             $('#tohide1').show();
             $('#tohide2').show();
             $('#tohide3').show();
             $('#tohide4').show();
             
         }

     });


      $('#<%= template.ClientID %>').click(function (event) {
         event.preventDefault();

         if ((document.getElementById('iplustemplate').classList.contains('nonactive')) == true) {
                 $('#tohidet').hide();
                 $('#tohidet1').hide();
                 $('#tohidet2').hide();
                 $('#tohidet3').hide();
                 $('#tohidet4').hide();
             document.getElementById('iplustemplate').classList.remove("nonactive");
             iminustemplate.classList.add('nonactive');
        
         }
         else {
           
             document.getElementById('iminustemplate').classList.remove("nonactive");
             iplustemplate.classList.add('nonactive');
           
                 $('#tohidet').show();
                 $('#tohidet1').show();
                 $('#tohidet2').show();
                 $('#tohidet3').show();
                 $('#tohidet4').show();
             
         }

     });


 function handleKeyDownEvent(id, eventRef)
 {
     
     var charCode = (window.event) ? eventRef.keyCode : eventRef.which;
     var ele = '';
     var id = $('.act').attr('id');
    
 // Up arrow...
 if ( charCode == 38 && eventRef.ctrlKey )
 {
     if (id == 'amaster') {
         var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
         }
         var ele = document.getElementById("asetting");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').show(); 
           var sec = document.getElementById('setting');
          sec.removeAttribute("hidden"); 
     document.getElementById('<%=hdfTab.ClientID %>').value = "asetting";
     }
     if (id == 'asku')
     {
    
          var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
         }
            var ele = document.getElementById("amaster");
         ele.classList.add("act")
         $('#master').show();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "amaster";
       
     }
     if (id == 'aprice')
     {
          var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
          var ele = document.getElementById("asku");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').show();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
           var sec = document.getElementById('sku');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "asku";
     }
     if (id == 'aimage')
     {
        
          var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        var ele = document.getElementById("aprice");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').show();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
           var sec = document.getElementById('price');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "aprice";

     }
     if (id == 'aoption')
     {
        var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        var ele = document.getElementById("aimage");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').show();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
           var sec = document.getElementById('image');
         sec.removeAttribute("hidden");
         document.getElementById('<%=hdfTab.ClientID %>').value = "aimage";
     }
     if (id == 'amalldata')
     {
         var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        var ele = document.getElementById("aoption");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').show();
         $('#malldata').hide();
         $('#setting').hide(); 
           var sec = document.getElementById('option');
         sec.removeAttribute("hidden");
         document.getElementById('<%=hdfTab.ClientID %>').value = "aoption";
     }
     if(id == 'asetting')
     {
         var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        var ele = document.getElementById("amalldata");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').show();
         $('#setting').hide(); 
           var sec = document.getElementById('malldata');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "amalldata";

     }
   
  return false;
 }
 // down arrow...
 if ( charCode == 40 && eventRef.ctrlKey  )
 {
     if (id == 'amaster') {
         var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
         }
         var ele = document.getElementById("asku");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').show();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
           var sec = document.getElementById('sku');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "asku";
     
     }
     if (id == 'asku')
     {
          var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
         }
            var ele = document.getElementById("aprice");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').show();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
            var sec = document.getElementById('price');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "aprice";
     }
     if (id == 'aprice')
     {
          var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
          var ele = document.getElementById("aimage");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').show();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
            var sec = document.getElementById('image');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "aimage";
     }
     if (id == 'aimage')
     {
          var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        var ele = document.getElementById("aoption");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').show();
         $('#malldata').hide();
         $('#setting').hide(); 
            var sec = document.getElementById('option');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "aoption";

     }
     if (id == 'aoption')
     {
        var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        var ele = document.getElementById("amalldata");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').show();
         $('#setting').hide(); 
            var sec = document.getElementById('malldata');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "amalldata";
     }
     if (id == 'amalldata')
     {
         var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        var ele = document.getElementById("asetting");
         ele.classList.add("act")
         $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').show(); 
            var sec = document.getElementById('setting');
         sec.removeAttribute("hidden"); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "asetting";
     }
     if(id == 'asetting')
     {
         var anchorsArray = document.querySelectorAll('.aitem');
        for (var i = 0; i < anchorsArray.length; i++) {
            anchorsArray[i].classList.remove("act");
       
        }
        var ele = document.getElementById("amaster");
         ele.classList.add("act")
         $('#master').show();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
         document.getElementById('<%=hdfTab.ClientID %>').value = "amaster";

     }
    
  return false;
     }
    
     eventRef.preventDefault();
 return true;
}

 </script>

<script type="text/javascript">
    function ShowMallCategory(mallID, ctrl) {
        var width = 800;
        var height = 500;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        var retval = window.open('../Item/Mall_Category_Choice.aspx?Mall_ID=' + mallID + '&Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
         var scheduledate = document.getElementById("<%=txtreleasedatemonotaro.ClientID%>").value;
        document.getElementById('<%=hdfScheduleDatemono.ClientID %>').value = scheduledate;
        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
</script>

    <script type="text/javascript" >
       
        function showBrowseDialog2() {
        var fileuploadctrl = document.getElementById('<%= FileUpload2.ClientID %>');
            fileuploadctrl.click();
       
        }
       

    function upload2() {
        var btn = document.getElementById('<%= imgbtn2_1.ClientID %>');
        btn.click();
        }

        function showBrowseDialog3() {
        var fileuploadctrl = document.getElementById('<%= FileUpload3.ClientID %>');
            fileuploadctrl.click();
            
        }
       

    function upload3() {
        var btn = document.getElementById('<%= imgbtn3_1.ClientID %>');
        btn.click();
        }

        function showBrowseDialog4() {
        var fileuploadctrl = document.getElementById('<%= FileUpload4.ClientID %>');
            fileuploadctrl.click();
        
        }
       

    function upload4() {
        var btn = document.getElementById('<%= imgbtn4_1.ClientID %>');
        btn.click();
        }

       function showBrowseDialog5() {
        var fileuploadctrl = document.getElementById('<%= FileUpload5.ClientID %>');
           fileuploadctrl.click();
            
        }
       

    function upload5() {
        var btn = document.getElementById('<%= imgbtn5_1.ClientID %>');
        btn.click();
        }

      function showBrowseDialog6() {
        var fileuploadctrl = document.getElementById('<%= FileUpload6.ClientID %>');
          fileuploadctrl.click();
           
        }
       

    function upload6() {
        var btn = document.getElementById('<%= imgbtn6_1.ClientID %>');
        btn.click();
        }

    function showBrowseDialog7() {
        var fileuploadctrl = document.getElementById('<%= FileUpload7.ClientID %>');
        fileuploadctrl.click();
        
        }
       

    function upload7() {
        var btn = document.getElementById('<%= imgbtn7_1.ClientID %>');
        btn.click();
        }

        function showBrowseDialog8() {
        var fileuploadctrl = document.getElementById('<%= FileUpload8.ClientID %>');
            fileuploadctrl.click();
            
        }
       

    function upload8() {
        var btn = document.getElementById('<%= imgbtn8_1.ClientID %>');
        btn.click();
        }

      function showBrowseDialog9() {
        var fileuploadctrl = document.getElementById('<%= FileUpload9.ClientID %>');
          fileuploadctrl.click();
         
        }
       

    function upload9() {
        var btn = document.getElementById('<%= imgbtn9_1.ClientID %>');
        btn.click();
        }

       function showBrowseDialog10() {
        var fileuploadctrl = document.getElementById('<%= FileUpload10.ClientID %>');
           fileuploadctrl.click();
        
        }
       

    function upload10() {
        var btn = document.getElementById('<%= imgbtn10_1.ClientID %>');
        btn.click();
        }

     function showBrowseDialog11() {
        var fileuploadctrl = document.getElementById('<%= FileUpload11.ClientID %>');
         fileuploadctrl.click();
       
        }
       

    function upload11() {
        var btn = document.getElementById('<%= imgbtn11_1.ClientID %>');
        btn.click();
        }

        function showBrowseDialog12() {
        var fileuploadctrl = document.getElementById('<%= FileUpload12.ClientID %>');
            fileuploadctrl.click();
           
        }
       

    function upload12() {
        var btn = document.getElementById('<%= imgbtn12_1.ClientID %>');
        btn.click();
        }

        function showBrowseDialog13() {
        var fileuploadctrl = document.getElementById('<%= FileUpload13.ClientID %>');
            fileuploadctrl.click();
             
        }
       

    function upload13() {
        var btn = document.getElementById('<%= imgbtn13_1.ClientID %>');
        btn.click();
        }

        function showBrowseDialog14() {
        var fileuploadctrl = document.getElementById('<%= FileUpload14.ClientID %>');
            fileuploadctrl.click();
           
        }
       

    function upload14() {
        var btn = document.getElementById('<%= imgbtn14_1.ClientID %>');
        btn.click();
        }

       function showBrowseDialog15() {
        var fileuploadctrl = document.getElementById('<%= FileUpload15.ClientID %>');
           fileuploadctrl.click();
           
        }
       

    function upload15() {
        var btn = document.getElementById('<%= imgbtn15_1.ClientID %>');
        btn.click();
        }

      function showBrowseDialog16() {
        var fileuploadctrl = document.getElementById('<%= FileUpload16.ClientID %>');
          fileuploadctrl.click();
         
        }
       

    function upload16() {
        var btn = document.getElementById('<%= imgbtn16_1.ClientID %>');
        btn.click();
        }

    function showBrowseDialog17() {
        var fileuploadctrl = document.getElementById('<%= FileUpload17.ClientID %>');
        fileuploadctrl.click();
        
        }
       

    function upload17() {
        var btn = document.getElementById('<%= imgbtn17_1.ClientID %>');
        btn.click();
        }

    function showBrowseDialog18() {
        var fileuploadctrl = document.getElementById('<%= FileUpload18.ClientID %>');
        fileuploadctrl.click();
        
        }
       

    function upload18() {
        var btn = document.getElementById('<%= imgbtn18_1.ClientID %>');
        btn.click();
        }

      function showBrowseDialog19() {
        var fileuploadctrl = document.getElementById('<%= FileUpload19.ClientID %>');
          fileuploadctrl.click();
         
        }
       

    function upload19() {
        var btn = document.getElementById('<%= imgbtn19_1.ClientID %>');
        btn.click();
        }

       function showBrowseDialog20() {
        var fileuploadctrl = document.getElementById('<%= FileUpload20.ClientID %>');
           fileuploadctrl.click();
            
        }
       

    function upload20() {
        var btn = document.getElementById('<%= imgbtn20_1.ClientID %>');
        btn.click();
    }
</script>

<script type="text/javascript">
    function ShowYahooSpecValue(ctrl) {
        var width = 600;
        var height = 500;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        var hidderValue = document.getElementById("<%= txtYahoo_CategoryID.ClientID %>").value;
        var retval = window.open('../Item/Item_YahooSpecificValue.aspx?YahooMallCategoryID=' + hidderValue + '&Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
         var scheduledate = document.getElementById("<%=txtreleasedatemonotaro.ClientID%>").value;
        document.getElementById('<%=hdfScheduleDatemono.ClientID %>').value = scheduledate;
        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
</script>
<script type="text/javascript">
    function ShowCatagoryList(ctrl) {
        var width = 600;
        var height = 500;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        var retval = window.open('../Item/PopupCatagoryList.aspx?Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
         var scheduledate = document.getElementById("<%=txtreleasedatemonotaro.ClientID%>").value;
        document.getElementById('<%=hdfScheduleDatemono.ClientID %>').value = scheduledate;
            if (window.focus) {
                newwin.focus()
            }
            return false;
        }
    </script>
 
<script type = "text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
         
        if (confirm("削除しますか？")) {
            confirm_value.value = "はい";
        } 
        else {
            confirm_value.value = "いいえ";
        }
        var val = document.forms[0].appendChild(confirm_value);
        if (val.value.toString() == "はい") {
            //document.forms[0].target = "_blank";
            location.reload();
        }
    }

   
</script>

  <script type="text/javascript">
    function AddSKU(ctrl) {
        var width = 1075;
        var height = 550;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        if (itemcode.indexOf("itemcode") >= 0) {
            var txtitemcode = document.getElementById("txtItem_Code").value;
            var retval = window.open('../Item/AddSKU.aspx?Item_Code=' + txtitemcode, window, params);
        }
        else {
            var retval = window.open('../Item/AddSKU.aspx?Item_Code=' + itemcode, window, params);
        }
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
         var scheduledate = document.getElementById("<%=txtreleasedatemonotaro.ClientID%>").value;
        document.getElementById('<%=hdfScheduleDatemono.ClientID %>').value = scheduledate;
            if (window.focus) {
                newwin.focus()
            }
            return false;
        }
</script>
  <script type="text/javascript">
    function ShowRelatedProduct(ctrl) {
        var width = 1230;
        var height = 660;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height + 100) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';       
        params += ', status=no';
        params += ', location=no';
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        var retval = window.open('../Item/ShowRelatedProduct.aspx?Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
    document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
        var scheduledate = document.getElementById("<%=txtreleasedatemonotaro.ClientID%>").value;
    document.getElementById('<%=hdfScheduleDatemono.ClientID %>').value = scheduledate;
        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
  </script>


    <script type="text/javascript" >
    function showBrowseDialog() {
        var fileuploadctrl = document.getElementById('<%= FileUpload1.ClientID %>');
        fileuploadctrl.click();
    }

    function upload() {
        var btn = document.getElementById('<%= hideButton.ClientID %>');
        btn.click();
        }

        function Delete() {
            //alert("delete")
            var hlImage1 = document.getElementById('<%= hlImage1.ClientID %>');
            var image1 = document.getElementById('<%= Image1.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg1.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete").hidden = true;
            document.getElementById("imginput").hidden = false;   
        }

        function Delete2() {
            var hlImage1 = document.getElementById('<%= hlImage2.ClientID %>');
            var image1 = document.getElementById('<%= Image2.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg2.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete2").hidden = true;
            document.getElementById("imginput2").hidden = false;   
        }

        function Delete3() {
            var hlImage1 = document.getElementById('<%= hlImage3.ClientID %>');
            var image1 = document.getElementById('<%= Image3.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg3.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete3").hidden = true;
            document.getElementById("imginput3").hidden = false;
        }

        function Delete4() {
            var hlImage1 = document.getElementById('<%= hlImage4.ClientID %>');
            var image1 = document.getElementById('<%= Image4.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg4.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete4").hidden = true;
            document.getElementById("imginput4").hidden = false;   
        }

        function Delete5() {
            var hlImage1 = document.getElementById('<%= hlImage5.ClientID %>');
            var image1 = document.getElementById('<%= Image5.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg5.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete5").hidden = true;
            document.getElementById("imginput5").hidden = false;   
        }

        function Delete6() {
            var hlImage1 = document.getElementById('<%= hlImage6.ClientID %>');
            var image1 = document.getElementById('<%= Image6.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg6.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete6").hidden = true;
            document.getElementById("imginput6").hidden = false;   
        }

        function Delete7() {
            var hlImage1 = document.getElementById('<%= hlImage7.ClientID %>');
            var image1 = document.getElementById('<%= Image7.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg7.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete7").hidden = true;
            document.getElementById("imginput7").hidden = false;   
        }

       function Delete8() {
            var hlImage1 = document.getElementById('<%= hlImage8.ClientID %>');
            var image1 = document.getElementById('<%= Image8.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg8.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete8").hidden = true;
            document.getElementById("imginput8").hidden = false;   
        }

       function Delete9() {
            var hlImage1 = document.getElementById('<%= hlImage9.ClientID %>');
            var image1 = document.getElementById('<%= Image9.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg9.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete9").hidden = true;
            document.getElementById("imginput9").hidden = false;   
        }

       function Delete10() {
            var hlImage1 = document.getElementById('<%= hlImage10.ClientID %>');
            var image1 = document.getElementById('<%= Image10.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg10.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete10").hidden = true;
            document.getElementById("imginput10").hidden = false;   
        }

        function Delete11() {
            var hlImage1 = document.getElementById('<%= hlImage11.ClientID %>');
            var image1 = document.getElementById('<%= Image11.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg11.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete11").hidden = true;
            document.getElementById("imginput11").hidden = false;   
        }

        function Delete12() {
            var hlImage1 = document.getElementById('<%= hlImage12.ClientID %>');
            var image1 = document.getElementById('<%= Image12.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg12.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete12").hidden = true;
            document.getElementById("imginput12").hidden = false;   
        }

        function Delete13() {
            var hlImage1 = document.getElementById('<%= hlImage13.ClientID %>');
            var image1 = document.getElementById('<%= Image13.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg13.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete13").hidden = true;
            document.getElementById("imginput13").hidden = false;   
        }

        function Delete14() {
            var hlImage1 = document.getElementById('<%= hlImage14.ClientID %>');
            var image1 = document.getElementById('<%= Image14.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg14.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete14").hidden = true;
            document.getElementById("imginput14").hidden = false;   
        }

        function Delete15() {
            var hlImage1 = document.getElementById('<%= hlImage15.ClientID %>');
            var image1 = document.getElementById('<%= Image15.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg15.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete15").hidden = true;
            document.getElementById("imginput15").hidden = false;   
        }

        function Delete16() {
            var hlImage1 = document.getElementById('<%= hlImage16.ClientID %>');
            var image1 = document.getElementById('<%= Image16.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg16.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete16").hidden = true;
            document.getElementById("imginput16").hidden = false;   
        }

        function Delete17() {
            var hlImage1 = document.getElementById('<%= hlImage17.ClientID %>');
            var image1 = document.getElementById('<%= Image17.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg17.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete17").hidden = true;
            document.getElementById("imginput17").hidden = false;   
        }

        function Delete18() {
            var hlImage1 = document.getElementById('<%= hlImage18.ClientID %>');
            var image1 = document.getElementById('<%= Image18.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg18.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete18").hidden = true;
            document.getElementById("imginput18").hidden = false;   
        }

        function Delete19() {
            var hlImage1 = document.getElementById('<%= hlImage19.ClientID %>');
            var image1 = document.getElementById('<%= Image19.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg19.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete19").hidden = true;
            document.getElementById("imginput19").hidden = false;   
        }

       function Delete20() {
            var hlImage1 = document.getElementById('<%= hlImage20.ClientID %>');
            var image1 = document.getElementById('<%= Image20.ClientID %>');
            var txtimg1 = document.getElementById('<%= txtimg20.ClientID %>');
            hlImage1.href = '../../Item_Image/no_image.jpg';
            image1.src = '../../Item_Image/no_image.jpg';
            txtimg1.value = "";
            document.getElementById("imginputdelete20").hidden = true;
            document.getElementById("imginput20").hidden = false;   
        }

        function clrCtrlmono() {
        document.getElementById('<%=txtreleasedatemonotaro.ClientID %>').value = "";
    }

    </script>
    <script type="text/javascript">
    function clrCtrl() {
        document.getElementById('<%=txtRelease_Date.ClientID %>').value = "";
    }
    function clrCtrl1() {
        document.getElementById('<%=txtPost_Available_Date.ClientID %>').value = "";
    }
</script>

    <script type="text/javascript">
    function SaveClick() {
        document.getElementById("<%=CustomHiddenField.ClientID%>").value = "";
        document.getElementById("<%=hdfPostDate.ClientID%>").value = "";
        document.getElementById("<%=hdfReleaseDate.ClientID%>").value = "";
        document.getElementById("<%=hdfScheduleDatemono.ClientID%>").value = "";
    }
</script>

    <script type="text/javascript">	
         function ShowCopy(ctrl) {
             var width = 600;
             var height = 300;
             var left = (screen.width - width) / 2;
             var top = (screen.height - height) / 2;
             var params = 'width=' + width + ', height=' + height;
             params += ', top=' + top + ', left=' + left;
             params += ', toolbar=no';
             params += ', menubar=no';
             params += ', resizable=yes';
             params += ', directories=no';
             params += ', scrollbars=yes';
             params += ', status=no';
             params += ', location=no';
             var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
             var itemname = document.getElementById("<%=txtItem_Name.ClientID %>").value;
            var retval = window.open('../Item/Item_Master_Copy_Data.aspx?Item_Code=' + itemcode + "&Item_Name=" + itemname, window, params);	
            var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
             hidSourceID.value = ctrl.id;
             if (window.focus) {
                 newwin.focus()
             }
             return false;
         }
     </script>
    <script type="text/javascript">
        function isNumberKeys(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode == 13 && charCode == 46)
                return false;
            else return true;
        }
        </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || charCode == 8)
                return true;
            else return false;
        }
    </script>
   
    <script type="text/javascript">
        function numberWithCommas(x) {
            return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
          function Web_calcuteTax(txt) {
              var sale_Price = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (sale_Price.trim().length == 0) {
                  sale_Price = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }
              var discount_rate = ((parseFloat(list_Price - sale_Price).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var profit_rate = ((parseFloat(sale_Price - cost).toFixed(1) / parseFloat(sale_Price).toFixed(1)) * 100).toFixed(1);
              var cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(sale_Price).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(discount_rate)) {
                  discount_rate = 0;
              }
              if (!isFinite(profit_rate)) {
                  profit_rate = 0;
              }
              if (!isFinite(cost_rate)) {
                  cost_rate = 0;
              }
              document.getElementById("<%=txtprofitrate.ClientID %>").value = profit_rate;
              document.getElementById("<%=txtdiscountrate.ClientID %>").value = discount_rate;
              document.getElementById("<%=txtcostrate.ClientID %>").value = cost_rate;
              document.getElementById("<%=txtSale_Price.ClientID %>").value = numberWithCommas(sale_Price);
          }
          function Rakuten_calcuteTax(txt) {
              var RakutenPrice = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (RakutenPrice.trim().length == 0) {
                  RakutenPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var rakuten_discount_rate = ((parseFloat(list_Price - RakutenPrice).toFixed(1)/ parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var rakuten_profit_rate = ((parseFloat(RakutenPrice - cost).toFixed(1) / parseFloat(RakutenPrice).toFixed(1)) * 100).toFixed(1);
              var rakuten_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(RakutenPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(rakuten_discount_rate)) {
                  rakuten_discount_rate = 0;
              }
              if (!isFinite(rakuten_profit_rate)) {
                  rakuten_profit_rate = 0;
              }
              if (!isFinite(rakuten_cost_rate)) {
                  rakuten_cost_rate = 0;
              }
              document.getElementById("<%=txtrakutenProfitrate.ClientID %>").value = rakuten_profit_rate;
              document.getElementById("<%=txtrakutenDiscountrate.ClientID %>").value = rakuten_discount_rate;
              document.getElementById("<%=txtrakutenCostrate.ClientID %>").value = rakuten_cost_rate;
              document.getElementById("<%=txtRakutenPrice.ClientID %>").value = numberWithCommas(RakutenPrice);
          }
          function Yahoo_calcuteTax(txt) {
              var YahooPrice = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (YahooPrice.trim().length == 0) {
                  YahooPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var yahoo_discount_rate = ((parseFloat(list_Price - YahooPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var yahoo_profit_rate = ((parseFloat(YahooPrice - cost).toFixed(1) / parseFloat(YahooPrice).toFixed(1)) * 100).toFixed(1);
              var yahoo_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(YahooPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(yahoo_discount_rate)) {
                  yahoo_discount_rate = 0;
              }
              if (!isFinite(yahoo_profit_rate)) {
                  yahoo_profit_rate = 0;
              }
              if (!isFinite(yahoo_cost_rate)) {
                  yahoo_cost_rate = 0;
              }

              document.getElementById("<%=txtyahooProfitrate.ClientID %>").value = yahoo_profit_rate;
              document.getElementById("<%=txtyahooDiscountrate.ClientID %>").value = yahoo_discount_rate;
              document.getElementById("<%=txtyahooCostrate.ClientID %>").value = yahoo_cost_rate;
              document.getElementById("<%=txtYahooPrice.ClientID %>").value = numberWithCommas(YahooPrice);
          }
          function Wowma_calcuteTax(txt) {
              var WowmaPrice = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (WowmaPrice.trim().length == 0) {
                  WowmaPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var wowma_discount_rate = ((parseFloat(list_Price - WowmaPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var wowma_profit_rate = ((parseFloat(WowmaPrice - cost).toFixed(1) / parseFloat(WowmaPrice).toFixed(1)) * 100).toFixed(1);
              var wowma_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(WowmaPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(wowma_discount_rate)) {
                  wowma_discount_rate = 0;
              }
              if (!isFinite(wowma_profit_rate)) {
                  wowma_profit_rate = 0;
              }
              if (!isFinite(wowma_cost_rate)) {
                  wowma_cost_rate = 0;
              }

              document.getElementById("<%=txtwowmaProfitrate.ClientID %>").value = wowma_profit_rate;
              document.getElementById("<%=txtwowmaDiscountrate.ClientID %>").value = wowma_discount_rate;
              document.getElementById("<%=txtwowmaCostrate.ClientID %>").value = wowma_cost_rate;
              document.getElementById("<%=txtWowmaPrice.ClientID %>").value = numberWithCommas(WowmaPrice);
          }
          function Jisha_calcuteTax(txt) {
              var JishaPrice = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (JishaPrice.trim().length == 0) {
                  JishaPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var jisha_discount_rate = ((parseFloat(list_Price - JishaPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var jisha_profit_rate = ((parseFloat(JishaPrice - cost).toFixed(1) / parseFloat(JishaPrice).toFixed(1)) * 100).toFixed(1);
              var jisha_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(JishaPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(jisha_discount_rate)) {
                  jisha_discount_rate = 0;
              }
              if (!isFinite(jisha_profit_rate)) {
                  jisha_profit_rate = 0;

              }
              if (!isFinite(jisha_cost_rate)) {
                  jisha_cost_rate = 0;
              }

              document.getElementById("<%=txtjishaProfitrate.ClientID %>").value = jisha_profit_rate;
              document.getElementById("<%=txtjishaDiscountrate.ClientID %>").value = jisha_discount_rate;
              document.getElementById("<%=txtjishaCostrate.ClientID %>").value = jisha_cost_rate;
              document.getElementById("<%=txtJishaPrice.ClientID %>").value = numberWithCommas(JishaPrice);
          }
          function Monotarou_calcuteTax(txt) {
              var MonotaroPrice = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (MonotaroPrice.trim().length == 0) {
                  MonotaroPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var monotaro_discount_rate = ((parseFloat(list_Price - MonotaroPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var monotaro_profit_rate = ((parseFloat(MonotaroPrice - cost).toFixed(1) / parseFloat(MonotaroPrice).toFixed(1)) * 100).toFixed(1);
              var monotaro_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(MonotaroPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(monotaro_discount_rate)) {
                  monotaro_discount_rate = 0;
              }
              if (!isFinite(monotaro_profit_rate)) {
                  monotaro_profit_rate = 0;
              }
              if (!isFinite(monotaro_cost_rate)) {
                  monotaro_cost_rate = 0;
              }

              document.getElementById("<%=txtmonoprice_profitrate.ClientID %>").value = monotaro_profit_rate;
              document.getElementById("<%=txtmonoprice_discountrate.ClientID %>").value = monotaro_discount_rate;
              document.getElementById("<%=txtmonoprice_costrate.ClientID %>").value = monotaro_cost_rate;
              document.getElementById("<%=txtmonoprice.ClientID %>").value = numberWithCommas(MonotaroPrice);
          }
          function Daito_calcuteTax(txt) {
              var DaitoPrice = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (DaitoPrice.trim().length == 0) {
                  DaitoPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var dite_discount_rate = ((parseFloat(list_Price - DaitoPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var dite_profit_rate = ((parseFloat(DaitoPrice - cost).toFixed(1) / parseFloat(DaitoPrice).toFixed(1)) * 100).toFixed(1);
              var dite_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(DaitoPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(dite_discount_rate)) {
                  dite_discount_rate = 0;

              }
              if (!isFinite(dite_profit_rate)) {
                  dite_profit_rate =0;

              }
              if (!isFinite(dite_cost_rate)) {
                  dite_cost_rate = 0;
              }

              document.getElementById("<%=txtditeprice_profitrate.ClientID %>").value = dite_profit_rate;
              document.getElementById("<%=txtditeprice_discountrate.ClientID %>").value = dite_discount_rate;
              document.getElementById("<%=txtditeprice_costrate.ClientID %>").value = dite_cost_rate;
              document.getElementById("<%=txtditeprice.ClientID %>").value = numberWithCommas(DaitoPrice);
          }
          function Japanmotorpart_calcuteTax(txt) {
              var JapanMotorPrice = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (JapanMotorPrice.trim().length == 0) {
                  JapanMotorPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var japanm_discount_rate = ((parseFloat(list_Price - JapanMotorPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var japanm_profit_rate = ((parseFloat(JapanMotorPrice - cost).toFixed(1) / parseFloat(JapanMotorPrice).toFixed(1)) * 100).toFixed(1);
              var japanm_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(JapanMotorPrice)) * 100).toFixed(1);
              if (!isFinite(japanm_discount_rate)) {
                  japanm_discount_rate = 0;
              }
              if (!isFinite(japanm_profit_rate)) {
                  japanm_profit_rate = 0;
              }
              if (!isFinite(japanm_cost_rate)) {
                  japanm_cost_rate = 0;
              }

              document.getElementById("<%=txtjapanmprice_profitrate.ClientID %>").value = japanm_profit_rate;
              document.getElementById("<%=txtjapanmprice_discountrate.ClientID %>").value = japanm_discount_rate;
              document.getElementById("<%=txtjapanmprice_costrate.ClientID %>").value = japanm_cost_rate;
              document.getElementById("<%=txtjapanmprice.ClientID %>").value = numberWithCommas(JapanMotorPrice);
          }
          function KashiwagiPrice_calcuteTax(txt) {
              var KashiwagiPrice = txt.value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (KashiwagiPrice.trim().length == 0) {
                  KashiwagiPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var kashiwagi_discount_rate = ((parseFloat(list_Price - KashiwagiPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var kashiwagi_profit_rate = ((parseFloat(KashiwagiPrice - cost).toFixed(1) / parseFloat(KashiwagiPrice).toFixed(1)) * 100).toFixed(1);
              var kashiwagi_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(KashiwagiPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(kashiwagi_discount_rate)) {
                  kashiwagi_discount_rate = 0;
              }
              if (!isFinite(kashiwagi_profit_rate)) {
                  kashiwagi_profit_rate = 0;
              }
              if (!isFinite(kashiwagi_cost_rate)) {
                  kashiwagi_cost_rate = 0;
              }

              document.getElementById("<%=txtkashiwagi_profitrate.ClientID %>").value = kashiwagi_profit_rate;
              document.getElementById("<%=txtkashiwagi_discountrate.ClientID %>").value = kashiwagi_discount_rate;
              document.getElementById("<%=txtkashiwagi_costrate.ClientID %>").value = kashiwagi_cost_rate;
              document.getElementById("<%=txtkashiwagi.ClientID %>").value = numberWithCommas(KashiwagiPrice);
          }

    </script>
      <script type="text/javascript">
          function list_price_change(txt) {
              var list_Price = txt.value.replace(/,/g, '');
              document.getElementById("<%=txtList_Price.ClientID %>").value = numberWithCommas(list_Price);
              
              //new
              var sale_Price = document.getElementById("<%=txtSale_Price.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
            //  var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (sale_Price.trim().length == 0) {
                  sale_Price = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }
              var discount_rate = ((parseFloat(list_Price - sale_Price).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var profit_rate = ((parseFloat(sale_Price - cost).toFixed(1) / parseFloat(sale_Price).toFixed(1)) * 100).toFixed(1);
              var cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(sale_Price).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(discount_rate)) {
                  discount_rate = 0;
              }
              if (!isFinite(profit_rate)) {
                  profit_rate = 0;
              }
              if (!isFinite(cost_rate)) {
                  cost_rate = 0;
              }
              document.getElementById("<%=txtprofitrate.ClientID %>").value = profit_rate;
              document.getElementById("<%=txtdiscountrate.ClientID %>").value = discount_rate;
              document.getElementById("<%=txtcostrate.ClientID %>").value = cost_rate;
              document.getElementById("<%=txtSale_Price.ClientID %>").value = numberWithCommas(sale_Price);


              var JishaPrice = document.getElementById("<%=txtJishaPrice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
             // var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (JishaPrice.trim().length == 0) {
                  JishaPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var jisha_discount_rate = ((parseFloat(list_Price - JishaPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var jisha_profit_rate = ((parseFloat(JishaPrice - cost).toFixed(1) / parseFloat(JishaPrice).toFixed(1)) * 100).toFixed(1);
              var jisha_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(JishaPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(jisha_discount_rate)) {
                  jisha_discount_rate = 0;
              }
              if (!isFinite(jisha_profit_rate)) {
                  jisha_profit_rate = 0;

              }
              if (!isFinite(jisha_cost_rate)) {
                  jisha_cost_rate = 0;
              }

              document.getElementById("<%=txtjishaProfitrate.ClientID %>").value = jisha_profit_rate;
              document.getElementById("<%=txtjishaDiscountrate.ClientID %>").value = jisha_discount_rate;
              document.getElementById("<%=txtjishaCostrate.ClientID %>").value = jisha_cost_rate;
              document.getElementById("<%=txtJishaPrice.ClientID %>").value = numberWithCommas(JishaPrice);

              var RakutenPrice = document.getElementById("<%=txtRakutenPrice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
           //   var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (RakutenPrice.trim().length == 0) {
                  RakutenPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var rakuten_discount_rate = ((parseFloat(list_Price - RakutenPrice).toFixed(1)/ parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var rakuten_profit_rate = ((parseFloat(RakutenPrice - cost).toFixed(1) / parseFloat(RakutenPrice).toFixed(1)) * 100).toFixed(1);
              var rakuten_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(RakutenPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(rakuten_discount_rate)) {
                  rakuten_discount_rate = 0;
              }
              if (!isFinite(rakuten_profit_rate)) {
                  rakuten_profit_rate = 0;
              }
              if (!isFinite(rakuten_cost_rate)) {
                  rakuten_cost_rate = 0;
              }
              document.getElementById("<%=txtrakutenProfitrate.ClientID %>").value = rakuten_profit_rate;
              document.getElementById("<%=txtrakutenDiscountrate.ClientID %>").value = rakuten_discount_rate;
              document.getElementById("<%=txtrakutenCostrate.ClientID %>").value = rakuten_cost_rate;
              document.getElementById("<%=txtRakutenPrice.ClientID %>").value = numberWithCommas(RakutenPrice);

              var YahooPrice = document.getElementById("<%=txtYahooPrice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              //var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (YahooPrice.trim().length == 0) {
                  YahooPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var yahoo_discount_rate = ((parseFloat(list_Price - YahooPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var yahoo_profit_rate = ((parseFloat(YahooPrice - cost).toFixed(1) / parseFloat(YahooPrice).toFixed(1)) * 100).toFixed(1);
              var yahoo_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(YahooPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(yahoo_discount_rate)) {
                  yahoo_discount_rate = 0;
              }
              if (!isFinite(yahoo_profit_rate)) {
                  yahoo_profit_rate = 0;
              }
              if (!isFinite(yahoo_cost_rate)) {
                  yahoo_cost_rate = 0;
              }

              document.getElementById("<%=txtyahooProfitrate.ClientID %>").value = yahoo_profit_rate;
              document.getElementById("<%=txtyahooDiscountrate.ClientID %>").value = yahoo_discount_rate;
              document.getElementById("<%=txtyahooCostrate.ClientID %>").value = yahoo_cost_rate;
              document.getElementById("<%=txtYahooPrice.ClientID %>").value = numberWithCommas(YahooPrice);

              var WowmaPrice = document.getElementById("<%=txtWowmaPrice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              //var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (WowmaPrice.trim().length == 0) {
                  WowmaPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var wowma_discount_rate = ((parseFloat(list_Price - WowmaPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var wowma_profit_rate = ((parseFloat(WowmaPrice - cost).toFixed(1) / parseFloat(WowmaPrice).toFixed(1)) * 100).toFixed(1);
              var wowma_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(WowmaPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(wowma_discount_rate)) {
                  wowma_discount_rate = 0;
              }
              if (!isFinite(wowma_profit_rate)) {
                  wowma_profit_rate = 0;
              }
              if (!isFinite(wowma_cost_rate)) {
                  wowma_cost_rate = 0;
              }

              document.getElementById("<%=txtwowmaProfitrate.ClientID %>").value = wowma_profit_rate;
              document.getElementById("<%=txtwowmaDiscountrate.ClientID %>").value = wowma_discount_rate;
              document.getElementById("<%=txtwowmaCostrate.ClientID %>").value = wowma_cost_rate;
              document.getElementById("<%=txtWowmaPrice.ClientID %>").value = numberWithCommas(WowmaPrice);


              var MonotaroPrice = document.getElementById("<%=txtmonoprice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
             // var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (MonotaroPrice.trim().length == 0) {
                  MonotaroPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var monotaro_discount_rate = ((parseFloat(list_Price - MonotaroPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var monotaro_profit_rate = ((parseFloat(MonotaroPrice - cost).toFixed(1) / parseFloat(MonotaroPrice).toFixed(1)) * 100).toFixed(1);
              var monotaro_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(MonotaroPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(monotaro_discount_rate)) {
                  monotaro_discount_rate = 0;
              }
              if (!isFinite(monotaro_profit_rate)) {
                  monotaro_profit_rate = 0;
              }
              if (!isFinite(monotaro_cost_rate)) {
                  monotaro_cost_rate = 0;
              }

              document.getElementById("<%=txtmonoprice_profitrate.ClientID %>").value = monotaro_profit_rate;
              document.getElementById("<%=txtmonoprice_discountrate.ClientID %>").value = monotaro_discount_rate;
              document.getElementById("<%=txtmonoprice_costrate.ClientID %>").value = monotaro_cost_rate;
              document.getElementById("<%=txtmonoprice.ClientID %>").value = numberWithCommas(MonotaroPrice);

              var DaitoPrice = document.getElementById("<%=txtditeprice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              //var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (DaitoPrice.trim().length == 0) {
                  DaitoPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var dite_discount_rate = ((parseFloat(list_Price - DaitoPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var dite_profit_rate = ((parseFloat(DaitoPrice - cost).toFixed(1) / parseFloat(DaitoPrice).toFixed(1)) * 100).toFixed(1);
              var dite_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(DaitoPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(dite_discount_rate)) {
                  dite_discount_rate = 0;

              }
              if (!isFinite(dite_profit_rate)) {
                  dite_profit_rate =0;

              }
              if (!isFinite(dite_cost_rate)) {
                  dite_cost_rate = 0;
              }

              document.getElementById("<%=txtditeprice_profitrate.ClientID %>").value = dite_profit_rate;
              document.getElementById("<%=txtditeprice_discountrate.ClientID %>").value = dite_discount_rate;
              document.getElementById("<%=txtditeprice_costrate.ClientID %>").value = dite_cost_rate;
              document.getElementById("<%=txtditeprice.ClientID %>").value = numberWithCommas(DaitoPrice);


              var JapanMotorPrice = document.getElementById("<%=txtjapanmprice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
             // var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (JapanMotorPrice.trim().length == 0) {
                  JapanMotorPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var japanm_discount_rate = ((parseFloat(list_Price - JapanMotorPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var japanm_profit_rate = ((parseFloat(JapanMotorPrice - cost).toFixed(1) / parseFloat(JapanMotorPrice).toFixed(1)) * 100).toFixed(1);
              var japanm_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(JapanMotorPrice)) * 100).toFixed(1);
              if (!isFinite(japanm_discount_rate)) {
                  japanm_discount_rate = 0;
              }
              if (!isFinite(japanm_profit_rate)) {
                  japanm_profit_rate = 0;
              }
              if (!isFinite(japanm_cost_rate)) {
                  japanm_cost_rate = 0;
              }

              document.getElementById("<%=txtjapanmprice_profitrate.ClientID %>").value = japanm_profit_rate;
              document.getElementById("<%=txtjapanmprice_discountrate.ClientID %>").value = japanm_discount_rate;
              document.getElementById("<%=txtjapanmprice_costrate.ClientID %>").value = japanm_cost_rate;
              document.getElementById("<%=txtjapanmprice.ClientID %>").value = numberWithCommas(JapanMotorPrice);


              var KashiwagiPrice = document.getElementById("<%=txtkashiwagi.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
             // var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (KashiwagiPrice.trim().length == 0) {
                  KashiwagiPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var kashiwagi_discount_rate = ((parseFloat(list_Price - KashiwagiPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var kashiwagi_profit_rate = ((parseFloat(KashiwagiPrice - cost).toFixed(1) / parseFloat(KashiwagiPrice).toFixed(1)) * 100).toFixed(1);
              var kashiwagi_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(KashiwagiPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(kashiwagi_discount_rate)) {
                  kashiwagi_discount_rate = 0;
              }
              if (!isFinite(kashiwagi_profit_rate)) {
                  kashiwagi_profit_rate = 0;
              }
              if (!isFinite(kashiwagi_cost_rate)) {
                  kashiwagi_cost_rate = 0;
              }

              document.getElementById("<%=txtkashiwagi_profitrate.ClientID %>").value = kashiwagi_profit_rate;
              document.getElementById("<%=txtkashiwagi_discountrate.ClientID %>").value = kashiwagi_discount_rate;
              document.getElementById("<%=txtkashiwagi_costrate.ClientID %>").value = kashiwagi_cost_rate;
              document.getElementById("<%=txtkashiwagi.ClientID %>").value = numberWithCommas(KashiwagiPrice);
              //finish
          }
          function cost_change(txt) {
              var cost = txt.value.replace(/,/g, '');
              document.getElementById("<%=txtcost.ClientID %>").value = numberWithCommas(cost);

              //new
              var sale_Price = document.getElementById("<%=txtSale_Price.ClientID %>").value.replace(/,/g, '');
             // var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (sale_Price.trim().length == 0) {
                  sale_Price = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }
              var discount_rate = ((parseFloat(list_Price - sale_Price).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var profit_rate = ((parseFloat(sale_Price - cost).toFixed(1) / parseFloat(sale_Price).toFixed(1)) * 100).toFixed(1);
              var cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(sale_Price).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(discount_rate)) {
                  discount_rate = 0;
              }
              if (!isFinite(profit_rate)) {
                  profit_rate = 0;
              }
              if (!isFinite(cost_rate)) {
                  cost_rate = 0;
              }
              document.getElementById("<%=txtprofitrate.ClientID %>").value = profit_rate;
              document.getElementById("<%=txtdiscountrate.ClientID %>").value = discount_rate;
              document.getElementById("<%=txtcostrate.ClientID %>").value = cost_rate;
              document.getElementById("<%=txtSale_Price.ClientID %>").value = numberWithCommas(sale_Price);


              var JishaPrice = document.getElementById("<%=txtJishaPrice.ClientID %>").value.replace(/,/g, '');
             // var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (JishaPrice.trim().length == 0) {
                  JishaPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var jisha_discount_rate = ((parseFloat(list_Price - JishaPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var jisha_profit_rate = ((parseFloat(JishaPrice - cost).toFixed(1) / parseFloat(JishaPrice).toFixed(1)) * 100).toFixed(1);
              var jisha_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(JishaPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(jisha_discount_rate)) {
                  jisha_discount_rate = 0;
              }
              if (!isFinite(jisha_profit_rate)) {
                  jisha_profit_rate = 0;

              }
              if (!isFinite(jisha_cost_rate)) {
                  jisha_cost_rate = 0;
              }

              document.getElementById("<%=txtjishaProfitrate.ClientID %>").value = jisha_profit_rate;
              document.getElementById("<%=txtjishaDiscountrate.ClientID %>").value = jisha_discount_rate;
              document.getElementById("<%=txtjishaCostrate.ClientID %>").value = jisha_cost_rate;
              document.getElementById("<%=txtJishaPrice.ClientID %>").value = numberWithCommas(JishaPrice);

              var RakutenPrice = document.getElementById("<%=txtRakutenPrice.ClientID %>").value.replace(/,/g, '');
             // var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (RakutenPrice.trim().length == 0) {
                  RakutenPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var rakuten_discount_rate = ((parseFloat(list_Price - RakutenPrice).toFixed(1)/ parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var rakuten_profit_rate = ((parseFloat(RakutenPrice - cost).toFixed(1) / parseFloat(RakutenPrice).toFixed(1)) * 100).toFixed(1);
              var rakuten_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(RakutenPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(rakuten_discount_rate)) {
                  rakuten_discount_rate = 0;
              }
              if (!isFinite(rakuten_profit_rate)) {
                  rakuten_profit_rate = 0;
              }
              if (!isFinite(rakuten_cost_rate)) {
                  rakuten_cost_rate = 0;
              }
              document.getElementById("<%=txtrakutenProfitrate.ClientID %>").value = rakuten_profit_rate;
              document.getElementById("<%=txtrakutenDiscountrate.ClientID %>").value = rakuten_discount_rate;
              document.getElementById("<%=txtrakutenCostrate.ClientID %>").value = rakuten_cost_rate;
              document.getElementById("<%=txtRakutenPrice.ClientID %>").value = numberWithCommas(RakutenPrice);

              var YahooPrice = document.getElementById("<%=txtYahooPrice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              //var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (YahooPrice.trim().length == 0) {
                  YahooPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var yahoo_discount_rate = ((parseFloat(list_Price - YahooPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var yahoo_profit_rate = ((parseFloat(YahooPrice - cost).toFixed(1) / parseFloat(YahooPrice).toFixed(1)) * 100).toFixed(1);
              var yahoo_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(YahooPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(yahoo_discount_rate)) {
                  yahoo_discount_rate = 0;
              }
              if (!isFinite(yahoo_profit_rate)) {
                  yahoo_profit_rate = 0;
              }
              if (!isFinite(yahoo_cost_rate)) {
                  yahoo_cost_rate = 0;
              }

              document.getElementById("<%=txtyahooProfitrate.ClientID %>").value = yahoo_profit_rate;
              document.getElementById("<%=txtyahooDiscountrate.ClientID %>").value = yahoo_discount_rate;
              document.getElementById("<%=txtyahooCostrate.ClientID %>").value = yahoo_cost_rate;
              document.getElementById("<%=txtYahooPrice.ClientID %>").value = numberWithCommas(YahooPrice);

              var WowmaPrice = document.getElementById("<%=txtWowmaPrice.ClientID %>").value.replace(/,/g, '');
              var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              //var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (WowmaPrice.trim().length == 0) {
                  WowmaPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var wowma_discount_rate = ((parseFloat(list_Price - WowmaPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var wowma_profit_rate = ((parseFloat(WowmaPrice - cost).toFixed(1) / parseFloat(WowmaPrice).toFixed(1)) * 100).toFixed(1);
              var wowma_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(WowmaPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(wowma_discount_rate)) {
                  wowma_discount_rate = 0;
              }
              if (!isFinite(wowma_profit_rate)) {
                  wowma_profit_rate = 0;
              }
              if (!isFinite(wowma_cost_rate)) {
                  wowma_cost_rate = 0;
              }

              document.getElementById("<%=txtwowmaProfitrate.ClientID %>").value = wowma_profit_rate;
              document.getElementById("<%=txtwowmaDiscountrate.ClientID %>").value = wowma_discount_rate;
              document.getElementById("<%=txtwowmaCostrate.ClientID %>").value = wowma_cost_rate;
              document.getElementById("<%=txtWowmaPrice.ClientID %>").value = numberWithCommas(WowmaPrice);


              var MonotaroPrice = document.getElementById("<%=txtmonoprice.ClientID %>").value.replace(/,/g, '');
              //var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (MonotaroPrice.trim().length == 0) {
                  MonotaroPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var monotaro_discount_rate = ((parseFloat(list_Price - MonotaroPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var monotaro_profit_rate = ((parseFloat(MonotaroPrice - cost).toFixed(1) / parseFloat(MonotaroPrice).toFixed(1)) * 100).toFixed(1);
              var monotaro_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(MonotaroPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(monotaro_discount_rate)) {
                  monotaro_discount_rate = 0;
              }
              if (!isFinite(monotaro_profit_rate)) {
                  monotaro_profit_rate = 0;
              }
              if (!isFinite(monotaro_cost_rate)) {
                  monotaro_cost_rate = 0;
              }

              document.getElementById("<%=txtmonoprice_profitrate.ClientID %>").value = monotaro_profit_rate;
              document.getElementById("<%=txtmonoprice_discountrate.ClientID %>").value = monotaro_discount_rate;
              document.getElementById("<%=txtmonoprice_costrate.ClientID %>").value = monotaro_cost_rate;
              document.getElementById("<%=txtmonoprice.ClientID %>").value = numberWithCommas(MonotaroPrice);

              var DaitoPrice = document.getElementById("<%=txtditeprice.ClientID %>").value.replace(/,/g, '');
              //var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (DaitoPrice.trim().length == 0) {
                  DaitoPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var dite_discount_rate = ((parseFloat(list_Price - DaitoPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var dite_profit_rate = ((parseFloat(DaitoPrice - cost).toFixed(1) / parseFloat(DaitoPrice).toFixed(1)) * 100).toFixed(1);
              var dite_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(DaitoPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(dite_discount_rate)) {
                  dite_discount_rate = 0;

              }
              if (!isFinite(dite_profit_rate)) {
                  dite_profit_rate =0;

              }
              if (!isFinite(dite_cost_rate)) {
                  dite_cost_rate = 0;
              }

              document.getElementById("<%=txtditeprice_profitrate.ClientID %>").value = dite_profit_rate;
              document.getElementById("<%=txtditeprice_discountrate.ClientID %>").value = dite_discount_rate;
              document.getElementById("<%=txtditeprice_costrate.ClientID %>").value = dite_cost_rate;
              document.getElementById("<%=txtditeprice.ClientID %>").value = numberWithCommas(DaitoPrice);


              var JapanMotorPrice = document.getElementById("<%=txtjapanmprice.ClientID %>").value.replace(/,/g, '');
              //var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (JapanMotorPrice.trim().length == 0) {
                  JapanMotorPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var japanm_discount_rate = ((parseFloat(list_Price - JapanMotorPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var japanm_profit_rate = ((parseFloat(JapanMotorPrice - cost).toFixed(1) / parseFloat(JapanMotorPrice).toFixed(1)) * 100).toFixed(1);
              var japanm_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(JapanMotorPrice)) * 100).toFixed(1);
              if (!isFinite(japanm_discount_rate)) {
                  japanm_discount_rate = 0;
              }
              if (!isFinite(japanm_profit_rate)) {
                  japanm_profit_rate = 0;
              }
              if (!isFinite(japanm_cost_rate)) {
                  japanm_cost_rate = 0;
              }

              document.getElementById("<%=txtjapanmprice_profitrate.ClientID %>").value = japanm_profit_rate;
              document.getElementById("<%=txtjapanmprice_discountrate.ClientID %>").value = japanm_discount_rate;
              document.getElementById("<%=txtjapanmprice_costrate.ClientID %>").value = japanm_cost_rate;
              document.getElementById("<%=txtjapanmprice.ClientID %>").value = numberWithCommas(JapanMotorPrice);


              var KashiwagiPrice = document.getElementById("<%=txtkashiwagi.ClientID %>").value.replace(/,/g, '');
             // var cost = document.getElementById("<%=txtcost.ClientID %>").value.replace(/,/g, '');
              var list_Price = document.getElementById("<%=txtList_Price.ClientID %>").value.replace(/,/g, '');
              if (KashiwagiPrice.trim().length == 0) {
                  KashiwagiPrice = 0;
              }
              if (cost.trim().length == 0) {
                  cost = 0;
              }
              if (list_Price.trim().length == 0) {
                  list_Price = 0;
              }

              var kashiwagi_discount_rate = ((parseFloat(list_Price - KashiwagiPrice).toFixed(1) / parseFloat(list_Price).toFixed(1)) * 100).toFixed(1);
              var kashiwagi_profit_rate = ((parseFloat(KashiwagiPrice - cost).toFixed(1) / parseFloat(KashiwagiPrice).toFixed(1)) * 100).toFixed(1);
              var kashiwagi_cost_rate = ((parseFloat(cost).toFixed(1) / parseFloat(KashiwagiPrice).toFixed(1)) * 100).toFixed(1);
              if (!isFinite(kashiwagi_discount_rate)) {
                  kashiwagi_discount_rate = 0;
              }
              if (!isFinite(kashiwagi_profit_rate)) {
                  kashiwagi_profit_rate = 0;
              }
              if (!isFinite(kashiwagi_cost_rate)) {
                  kashiwagi_cost_rate = 0;
              }

              document.getElementById("<%=txtkashiwagi_profitrate.ClientID %>").value = kashiwagi_profit_rate;
              document.getElementById("<%=txtkashiwagi_discountrate.ClientID %>").value = kashiwagi_discount_rate;
              document.getElementById("<%=txtkashiwagi_costrate.ClientID %>").value = kashiwagi_cost_rate;
              document.getElementById("<%=txtkashiwagi.ClientID %>").value = numberWithCommas(KashiwagiPrice);
              //finish
          }
      </script>

</asp:Content>
