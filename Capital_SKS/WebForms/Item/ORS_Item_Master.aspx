<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ORS_Item_Master.aspx.cs" Inherits="Capital_SKS.WebForms.Item.ORS_Item_Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-alpha3/dist/css/bootstrap.min.css" />
      <link href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" rel="stylesheet">
      <link href="assets/css/all.min.css" rel="stylesheet">
      <link href="assets/css/style.css?20220412" rel="stylesheet">
      <link href="css/lightbox.css" rel="stylesheet" />
      <link rel="stylesheet" href="../../Styles/ors_item_master_style.css"  />

        <script src="https://unpkg.com/vue@next"></script>
        <script src="https://kit.fontawesome.com/7c0c75e583.js" ></script>  
        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js"></script>
        <script src="js/lightbox.min.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <asp:HiddenField ID="CustomHiddenField" runat="server" />
        <asp:HiddenField ID="hdfPostDate" runat="server" />
        <asp:HiddenField ID="hdfReleaseDate" runat="server"/>
        <asp:HiddenField  ID="hdfCtrl_ID" runat="server"/>
        <asp:HiddenField  ID="hdfCatID" runat="server"/>

    <div style="height:55px;"></div>
    <div class="container">
        <div id="headdiv" class="row g-0">
         
            <div class="col-2" id="navleft">

               <%-- <div class="list-group text-center text-lg-left">--%>
                
                <a href="#" id="amaster" class="list-group-item aitem act"  onkeydown="handleKeyDownEvent(this;id, event);" onclick="changeStyle(this);">
                <i class="fa-solid fa-brush " style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">商品情報</span>
                </a>
                 <a href="#" id="asku" class="list-group-item aitem"  onkeydown="handleKeyDownEvent(this.id, event);" onclick="changeStyle(this);">
                <i class="fa-solid fa-list-check" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">SKU情報</span>
                </a>
                 <a href="#" id="aprice" class="list-group-item aitem" onkeydown="handleKeyDownEvent(this.id, event);" onclick="changeStyle(this);">
                <i class="fa-solid fa-circle-dollar-to-slot" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">価格関連</span>
                </a>
                 <a href="#" id="aimage" class="list-group-item aitem" onkeydown="handleKeyDownEvent(this.id, event);" onclick="changeStyle(this);">
                <i class="fa-solid fa-images" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">画像関連</span>
                </a>
                 <a href="#" id="aoption" class="list-group-item aitem" onkeydown="handleKeyDownEvent(this.id, event);" onclick="changeStyle(this);">
                <i class="fa-solid fa-filter" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">オプションカテゴリ</span>
                </a>
                 <a href="#" id="amalldata" class="list-group-item aitem" onkeydown="handleKeyDownEvent(this.id, event);" onclick="changeStyle(this);">
                <i class="fa-solid fa-shop" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">モール設定</span>
                </a>
                <a href="#" id="asetting" class="list-group-item aitem" onkeydown="handleKeyDownEvent(this.id, event);"  onclick="changeStyle(this);">
                <i class="fa-solid fa-building" style="margin-right: 5px;"></i>
                <span class="d-none d-lg-inline">取引先設定</span>
                </a>
                <a href="#" class="list-group-item atxt" style="color:yellow;">
               
                <span class="d-none d-lg-inline">Ctrl + 上下で<br />メニューを<br />切り替えられます</span>
                </a>
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
                            <asp:TextBox CssClass="txtbox" ID="txtItem_Code"  runat="server"></asp:TextBox>
                              </div>
                              <div class="columnJanCD" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl greenlable">JANCD</span>
                            </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="txtJanCD"  runat="server"></asp:TextBox>
                            
                              </div>
                              <div class="columnmaker" >
                                      
                            <asp:Label  ID="lblmakername" runat="server" Text="">
                                 <span class="label label-md lbl greenlable">メーカー名</span>
                            </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="txtmakername" runat="server"></asp:TextBox>
                              </div>
                            </div>
                             <div class="rowfirst">
                              <div class="" style="padding:5px;">      
                            <asp:Label  ID="lblitemname" runat="server" Text="">
                                 <span class="label label-md lbl greenlable lblrequired">商品名</span>
                            </asp:Label>   <br />                       
                           <%-- <textarea class="txtarea textamemo" ID="txtItem_Name" cols="20" rows="2"></textarea>--%>
                                  <asp:TextBox ID="txtItem_Name" CssClass="txtarea textamemo" runat="server" TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                                   </div> 
                                  </div> 
                          </div>             
                      </div>
                        <div class=" col-md-5">
                            <div class=" col-md-12 containerboxmemo" style="padding-left: 10px;padding-right: 10px;">
                            <asp:Label  ID="Label1" runat="server" Text="">
                                 <span class="label label-md lbl orangelable">メモ</span>
                            </asp:Label>                          
                            <textarea class="txtarea textamemo" ID="txtmemo" cols="20" rows="2"></textarea>
                                </div>
                            </div>
                        
                </div>
                   
    
                <section id="master">
                   <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <div class="row" style="width:100%;margin-left: 3px;margin-right: 50px;">
                                    <div class="floaddiv" style="width:10%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">販売単位</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlsalesunit" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="floaddiv" style="width:15%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">内容量</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtcontentquantityunitno1"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                 <div class="floaddiv" style="width:10%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">内容量単位</span>
                                        </asp:Label>
                                         <asp:DropDownList ID="ddlcontentunit1" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                 <div class="floaddiv" style="width:15%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">まとめ販売数</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtcontentquantityunitno2"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                 <div class="floaddiv" style="width:10%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">まとめ単位</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlcontentunit2" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                <div class="floaddiv" style="width:15%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">通常・大型</span>
                                        </asp:Label>
                                         <asp:DropDownList ID="DropDownList3" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                <div class="floaddiv" style="width:20%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable">仕入先</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox60"  runat="server" style="width: 100%;"></asp:TextBox> 
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

                             <a class="fr_btn">
                                 <i class="fa-solid fa-magnifying-glass"></i>
                                 関連商品を検索する
                             </a>
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
                    <div class="row" style=" width:100%; margin-left: 0px !important;">
                        
                         <div class="floaddiv containerbox" style="width:18%; margin-right:10px;"> 
                             <div class="row" style="width:100%; margin-left:2px;">
                             <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">販売日</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtRelease_Date"  runat="server" style="width: 100%;"></asp:TextBox>
                             </div>
                              <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">掲載可能日</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtPost_Available_Date"  runat="server" style="width: 100%;"></asp:TextBox>
                             </div>
                             </div>
                             </div>

                         <div class="floaddiv containerbox" style="width:24%; margin-right:10px;"> 
                              <div class="row" style="width:100%; margin-left:2px;">
                             <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">製品コード</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtProduct_Code" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100%;"></asp:TextBox>
                             </div>
                              <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">ブランドコード</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtBrand_Code"  runat="server" style="width: 100%;"></asp:TextBox>
                             </div>
                             </div>
                             </div>

                         <div class="floaddiv containerbox" style="width:24%; margin-right:10px;"> 
                              <div class="row" style="width:100%; margin-left:2px;">
                             <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">送料フラグ</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlShipping_Flag" style="width:100%;" runat="server"></asp:DropDownList>
                             </div>
                              <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">代引料フラグ</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlDelivery_Charges" style="width:100%;" runat="server"></asp:DropDownList>
                             </div>
                             </div>

                             </div>

                         <div class="floaddiv containerbox" style="width:31%;"> 
                             <div class="row" style="width:100%; margin-left:2px;">
                             <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">個別送料</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtExtra_Shipping"  runat="server" style="width: 80%;"></asp:TextBox>
                                 <span> 円</span>
                             </div>
                              <div class="floaddiv" style="width:50%;" >
                                   <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">Maker_Code</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtmaker_code"  runat="server" style="width: 100%;"></asp:TextBox>
                             </div>
                             </div>
                             </div>
                            
                    </div>
                </section>
                 <section id="sku">
                     <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl greenlable">SKU在庫</span>
                           </asp:Label>
                             <asp:GridView ID="gvSKU" runat="server"></asp:GridView>
                          </div>
                     </div>
                      
                 </section>
                 <section id="price"> 
                       <div class="row rowprice">
                         <div class="col-md-4" style="padding-left: 1px;padding-right: 1px;"> 
                             <div class="col-md-12 containerbox"> 
                                 <p class="pprice">基本価格</p>
                                <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="listpricediv">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable lblrequired">定価（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtList_Price"  runat="server" style="width: 80%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="salepricediv">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl greenlable lblrequired">原価（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtcost"  runat="server" style="width: 80%;"></asp:TextBox> <span> 円</span>
                                    </div>
                                </div>                           
                              </div>             
                          </div>
                        
                </div>
                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                             <p class="pprice">WEB</p>
                            <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable lblrequired">販売価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtSale_Price"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox3"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox4"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox5"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>                             
                          </div>             
                      </div>
                      <div class=" col-md-6">
                            <div class="col-md-12 containerbox"> 
                             <p class="pprice">自社</p>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtJishaPrice"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox7"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox8"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox9"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>                             
                          </div>
                       </div>            
                </div>

                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                              <p class="pprice">楽天</p>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtRakutenPrice"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox11"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox12"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox13"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>                         
                          </div>             
                      </div>
                      <div class=" col-md-6">
                            <div class="col-md-12 containerbox"> 
                              <p class="pprice">Yahoo</p>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtYahooPrice"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox15"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox16"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox17"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>                               
                          </div>   
                       </div>            
                </div>

                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                                <p class="pprice">Wowma</p>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtWowmaPrice"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox19"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox20"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox21"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>                               
                          </div>             
                      </div>
                         
                </div>

                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                               <p class="pprice">モノタロウ</p>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox22"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox23"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox24"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox25"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>                               
                          </div>             
                      </div>
                      <div class=" col-md-6">
                          <div class="col-md-12 containerbox"> 
                               <p class="pprice">ダイト</p>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox26"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox27"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox28"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox29"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>                               
                          </div>     
                       </div>            
                </div>

                     <div class="row rowprice">
                     <div class="col-md-6" style="padding-left: 1px;padding-right: 1px;"> 
                         <div class="col-md-12 containerbox"> 
                              <p class="pprice">日本モーターパーツ</p>
                              <div class="row" style="width:100%;margin-left: 30px;margin-right: 50px;">
                                    <div class="web1">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">価格（税抜）</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox30"  runat="server" style="width:70%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">利益率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox31"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">割引率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox32"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                <div class="web2">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable ">原価率</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox33"  runat="server" style="width:60%;"></asp:TextBox> <span> %</span>
                                    </div>
                                </div>                             
                          </div>             
                      </div>
                          
                </div>
                 </section>
                <section id="image"> 
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                              <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl greenlable">商品画像</span>
                           </asp:Label>
                           <ul class="image_list">
                               <li>
                                   <a class="ImgClass">
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage1"><asp:Image runat="server" ID="Image1" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                    <%--   <asp:Image src="http://163.43.113.92/RCM_Capital/Item_Images/cps-test-2020-1.jpg" ID="Image1" runat="server" />--%>
                                   </a>
                                   <asp:TextBox ID="txtimg1" CssClass="txtbox" runat="server"></asp:TextBox> 
                                
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload1" type="file" hidden>
                                    </label>

                               </li>
                               <li>
                                   <a class="ImgClass">
                                    <%--   <asp:Image src="http://163.43.113.92/RCM_Capital/Item_Images/cps-test-2020-4.jpg" ID="Image2" runat="server" />--%>
                                         <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage2"><asp:Image runat="server" ID="Image2" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                       </a>
                                   <asp:TextBox ID="txtimg2" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload2" type="file" hidden>
                                    </label>

                               </li>
                               <li>
                                   <a class="ImgClass">
                                       <%--<asp:Image ID="Image3"  runat="server" />--%>
                                              <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage3"><asp:Image runat="server" ID="Image3" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg3" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload3" type="file" hidden>
                                    </label>

                               </li>
                                   <li>
                                   <a class="ImgClass">
                                       <%--<asp:Image ID="Image4" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage4"><asp:Image runat="server" ID="Image4" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg4" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload4" type="file" hidden>
                                    </label>

                               </li>
                               <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image5" runat="server" />--%>
                                   <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage5"><asp:Image runat="server" ID="Image5" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg5" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload5" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image6" runat="server" />--%>
                                      <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage6"><asp:Image runat="server" ID="Image6" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg6" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload6" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                   <%--    <asp:Image ID="Image7" runat="server" />--%>
                                           <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage7"><asp:Image runat="server" ID="Image7" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg7" CssClass="txtbox" runat="server"></asp:TextBox>                                                                
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload7" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image8" runat="server" />--%>
                                              <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage8"><asp:Image runat="server" ID="Image8" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg8" CssClass="txtbox" runat="server"></asp:TextBox> 
                                   
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload8" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image9" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage9"><asp:Image runat="server" ID="Image9" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg9" CssClass="txtbox" runat="server"></asp:TextBox> 
                                 
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload9" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image10" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage10"><asp:Image runat="server" ID="Image10" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg10" CssClass="txtbox" runat="server"></asp:TextBox> 
                                   
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload10" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image11"  runat="server" />--%>
                                           <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage11"><asp:Image runat="server" ID="Image11" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>

                                   </a>
                                   <asp:TextBox ID="txtimg11" CssClass="txtbox" runat="server"></asp:TextBox> 
                                  
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload11" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                       <%--<asp:Image ID="Image12"  runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage12"><asp:Image runat="server" ID="Image12" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg12" CssClass="txtbox" runat="server"></asp:TextBox> 
                                  
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload12" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image13" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage13"><asp:Image runat="server" ID="Image13" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg13" CssClass="txtbox" runat="server"></asp:TextBox> 
                                   
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload13" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image14" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage14"><asp:Image runat="server" ID="Image14" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg14" CssClass="txtbox" runat="server"></asp:TextBox> 
                                  
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload14" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                       <%--<asp:Image ID="Image15" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage15"><asp:Image runat="server" ID="Image15" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg15" CssClass="txtbox" runat="server"></asp:TextBox> 
                                  
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload15" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image16" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage16"><asp:Image runat="server" ID="Image16" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg16" CssClass="txtbox" runat="server"></asp:TextBox> 
                                 
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload16" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                   <%--    <asp:Image ID="Image17" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage17"><asp:Image runat="server" ID="Image17" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg17" CssClass="txtbox" runat="server"></asp:TextBox> 
                                   
                                   <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload17" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image18" runat="server" />--%>
                                         <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage18"><asp:Image runat="server" ID="Image18" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg18" CssClass="txtbox" runat="server"></asp:TextBox> 
                                   
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload18" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                      <%-- <asp:Image ID="Image19" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage19"><asp:Image runat="server" ID="Image19" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="TextBox52" CssClass="txtbox" runat="server"></asp:TextBox> 
                                  
                                  <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload19" type="file" hidden>
                                    </label>

                               </li>
                                <li>
                                   <a class="ImgClass">
                                       <%--<asp:Image ID="Image20" runat="server" />--%>
                                       <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage20"><asp:Image runat="server" ID="Image20" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink>
                                   </a>
                                   <asp:TextBox ID="txtimg20" CssClass="txtbox" runat="server"></asp:TextBox> 
                                  
                                    <%--<input type="file" id="FileUpload20" hidden/>
                                    <label class="imgchoosebtn" for="FileUpload20n">ファイル選択</label>--%>
                                    <label class="btn btn-default imgchoosebtn">
                                        ファイル選択 <input id="FileUpload20" type="file" hidden>
                                    </label>
                               </li>
                           </ul>
                          </div>
                     </div>

                </section>
                <section id="option"> 
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
                               <span class="label label-md lbl" style=" font-weight:600;">オプション</span>
                           </asp:Label>
                             <br />
                             <asp:Label CssClass=""  runat="server" >
                               <span class="label label-md lbl orangelable">テンプレート選択</span>
                           </asp:Label>
                             <br />
                             <asp:DropDownList ID="DropDownList6" style="width:300px;" runat="server"></asp:DropDownList>
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

                             <%-- <div class="columnItemcode" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl greenlable lblrequired">商品番号</span>
                           </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="TextBox101"  runat="server"></asp:TextBox>
                              </div>
                              <div class="columnJanCD" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl greenlable">JANCD</span>
                            </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="TextBox102"  runat="server"></asp:TextBox>
                            
                              </div>
                              <div class="columnmaker" >
                                      
                            <asp:Label  ID="Label2" runat="server" Text="">
                                 <span class="label label-md lbl greenlable">メーカー名</span>
                            </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="TextBox105" runat="server"></asp:TextBox>
                              </div>--%>
                          <%--  </div>--%>
                           <%--  <div class="rowfirst">
                              <div class="" style="padding:5px;">      
                            <asp:Label  ID="Label3" runat="server" Text="">
                                 <span class="label label-md lbl greenlable lblrequired">商品名</span>
                            </asp:Label>   <br />                       
                            <textarea class="txtarea textamemo" ID="" cols="20" rows="2"></textarea>
                                   </div> 
                                  </div>--%> 
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
                                  <%--  <asp:TextBox CssClass="txtbox" ID="TextBox101"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="TextBox107"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="TextBox111"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="TextBox112"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>    --%>
                                 <asp:GridView ID="gvCatagories" runat="server" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
                                <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                <asp:Label runat="server" ID="lblID" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                <ItemTemplate>
                                <p><asp:TextBox CssClass="txtbox" style="width: 100%; margin-bottom:10px;" runat="server" ID="txtCTGName" onkeypress="return isNumberKeys(event)"/></p>
                                <asp:Label runat="server" ID="lblCTGName" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                </asp:GridView>
                                <asp:Button ID="Button2" runat="server" Text="選択" Width="100px" />
                            </div>
                            </div>
                        
                </div>
                </section>
                <section id="malldata"> 
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
                                    <asp:TextBox CssClass="txtbox" ID="txtRakuten_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtRakuten_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 300px; margin-bottom:10px;"></asp:TextBox>                                   
                                    <asp:Button ID="btnRakuten_CategoryID" OnClientClick ="ShowMallCategory(1,this)" runat="server" Text="選択" Width="50px" />
                                     <br />
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">Wowma</span>
                                        </asp:Label>
                                   <br />
                                    <asp:TextBox CssClass="txtbox" ID="txtWowma_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtWowma_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 300px; margin-bottom:10px;"></asp:TextBox>                                   
                                    <asp:Button ID="btnWowma_CategoryID" OnClientClick ="ShowMallCategory(4,this)" runat="server" Text="選択" Width="50px" />
                                 </div>
                                 <div class="floaddiv" style="width:50%;">
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">Yahoo</span>
                                        </asp:Label>
                                     <br />
                                    <asp:TextBox CssClass="txtbox" ID="txtYahoo_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 100px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtYahoo_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 304px; margin-bottom:10px;"></asp:TextBox>                                   
                                    <asp:Button ID="btnYahoo_CategoryID" OnClientClick ="ShowMallCategory(2,this)" runat="server" Text="選択" Width="50px" />
                                     <br />
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">ヤフースペック値</span>
                                        </asp:Label>
                                   <br />
                                    <asp:TextBox CssClass="txtbox" ID="txtYahooValue1" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtYahooValue2" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>  
                                     <asp:TextBox CssClass="txtbox" ID="txtYahooValue3"  ReadOnly="true" onkeypress="return isNumberKeys(event)" runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>
                                    <asp:TextBox CssClass="txtbox" ID="txtYahooValue4" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>     
                                     <asp:TextBox CssClass="txtbox" ID="txtYahooValue5" ReadOnly="true" onkeypress="return isNumberKeys(event)"  runat="server" style="width: 79px; margin-bottom:10px;"></asp:TextBox>
                                       
                                    <asp:Button ID="Button10" runat="server" Text="選択" Width="50px" />

                                 </div>
                             </div>
                          </div>
                     </div>

                     <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">出店対象ショップ</span>
                           </asp:Label>   <br />
                              <asp:DataList ID="dlShop1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <ItemTemplate>
                                    <p>
                                  <asp:CheckBox runat="server" ID="ckbMall1Shop" EnableViewState="true" Text='<%# Bind("Mall_Name")%>' />
                                        <asp:Label runat="server" ID="lblMall1ShopID" Text='<%# Bind("ID")%>' Visible="false" /></p>
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
                              <div class="floaddiv" style="width:23%;">
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">楽天 ペイントアンドツール</span>
                                        </asp:Label><br />
                                        <asp:TextBox CssClass="txtbox" ID="TextBox124"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                     <br />
                                  </div>
                              <div class="floaddiv" style="width:23%;">
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">Yahoo ペイントアンドツール</span>
                                        </asp:Label><br />
                                        <asp:TextBox CssClass="txtbox" ID="TextBox125"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                     <br />
                                  </div>
                              <div class="floaddiv" style="width:23%;">
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable">Wowma ペイントアンドツール</span>
                                        </asp:Label><br />
                                        <asp:TextBox CssClass="txtbox" ID="TextBox126"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                     <br />
                                  </div>
                              <div class="floaddiv" style="width:23%;">
                                      <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl orangelable"> 自社 ペイントアンドツール</span>
                                        </asp:Label><br />
                                        <asp:TextBox CssClass="txtbox" ID="TextBox127"  runat="server" style="width: 100%; margin-bottom:10px;"></asp:TextBox>
                                     <br />
                                  </div>
                             </div>
                         </div>
                </section>
                <section id="setting"> 
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                           <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl" style=" font-weight:600;">商品情報</span>
                           </asp:Label>  
                             <br />

                            <div class="rowfirst">
                                 <div class="floaddiv" style="width:60%;" >
                                     <div class="row">
                                          <div class="floaddiv" style="width:50%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">ブランド名</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtBrand_Name"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                            <div class="floaddiv" style="width:23%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">市場売価</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtsellingprice"  runat="server" style="width: 80%;"></asp:TextBox><span> 円</span>
                                    </div>
                                    <div class="floaddiv" style="width:23%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">仕入価格</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtpurchaseprice"  runat="server" style="width: 80%;"></asp:TextBox> <span> 円</span>
                                    </div>

                                     </div>
                                     <div class="row">
                                         <div class="floaddiv" style="width:50%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">賞味期限</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtsellby"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                            <div class="floaddiv" style="width:48%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">年間出荷数もしくは売れ筋A～Dランク</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtsellingrank"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                     </div>
                              
                              </div>
                              <div class="floaddiv" style="width:40%;" >
                             <%--  <div class="" style="padding:5px;">    --%>  
                            <asp:Label  ID="Label7" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable ">商品詳細登録コメント</span>
                            </asp:Label>   <br />                       
                           <%-- <textarea class="txtarea textamemo" style="height: 95px;" ID="txtcomment" cols="20" rows="2"></textarea>--%>
                           <asp:TextBox ID="txtcomment" class="txtarea textamemo" style="height: 95px;" runat="server" Width="290px" MaxLength="40" TextMode="MultiLine"></asp:TextBox>
                                  <%-- </div> --%>
                                </div>
                          </div>
                             <div class="row">
                                 <div class="floaddiv" style="width:10%;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">公開種別</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox145"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:10%;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">最低発注数量</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox146"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:10%;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">最低発注単位</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox147"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:10%;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">直送可否</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox148"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:12%;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">公開予定日</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox149"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:24%;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">カテゴリ</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox150"  runat="server" style="width: 100%;"></asp:TextBox>
                                 </div>
                                   <div class="floaddiv" style="width:24%;">
                                    <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable">カラー</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox151"  runat="server" style="width: 100%;"></asp:TextBox>
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
                              <div class="floaddiv" style="width:20%;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">引取方法</span>
                           </asp:Label>
                          <asp:DropDownList ID="ddldeliverymethod" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                              <div class="floaddiv" style="width:15%;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">配送種別</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddldeliverytype" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>
                              <div class="floaddiv" style="width:15%;">
                                      
                            <asp:Label  ID="Label2" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">入荷日数</span>
                            </asp:Label>
                            <asp:TextBox CssClass="txtbox" ID="txtdeliverydays" runat="server"></asp:TextBox>
                              </div>

                                       <div class="floaddiv" style="width:20%;">
                                      
                            <asp:Label  ID="Label3" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">代引可否</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddldeliveryfees" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>

                                       <div class="floaddiv" style="width:20%;">
                                      
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
                                    <div class="listpricediv">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">可否</span>
                                        </asp:Label>
                                         <asp:DropDownList ID="ddlksmavaliable" style="width:100%;" runat="server"></asp:DropDownList>

                                    </div>
                                    <div class="salepricediv">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">入荷日数</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtksmdeliverydays"  runat="server" style="width: 80%;"></asp:TextBox>
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
                              <div class="floaddiv" style="width:30%;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">該当法令</span>
                           </asp:Label>
                          <asp:DropDownList ID="ddlnoapplicablelaw" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                              <div class="floaddiv" style="width:30%;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">販売許可・認可・届出</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddlsalespermission" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>
                              <div class="floaddiv" style="width:30%;">
                                      
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
                              <div class="floaddiv" style="width:17%;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">全国</span>
                           </asp:Label>
                          <asp:TextBox CssClass="txtbox" ID="txtnationwide"  runat="server" style="width: 100%;"></asp:TextBox>
                              </div>
                              <div class="floaddiv" style="width:17%;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">北海道</span>
                            </asp:Label>
                          <asp:TextBox CssClass="txtbox" ID="txthokkaido"  runat="server" style="width: 100%;"></asp:TextBox>
                            
                              </div>

                                  <div class="floaddiv" style="width:17%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">沖縄</span>
                                        </asp:Label>
                                       <asp:TextBox CssClass="txtbox" ID="txtokinawa"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>

                                  <div class="floaddiv" style="width:17%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">離島</span>
                                        </asp:Label>
                                    <asp:TextBox CssClass="txtbox" ID="txtremoteisland"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                <div class="floaddiv" style="width:31%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">直送時配送不可地域</span>
                                        </asp:Label>
                                       <asp:TextBox CssClass="txtbox" ID="txtundeliveredarea"  runat="server" style="width: 100%;"></asp:TextBox>
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
                              <div class="floaddiv" style="width:19%;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">危険物の含有量</span>
                           </asp:Label>
                          <asp:TextBox CssClass="txtbox" ID="txtdangerousgoodscontents"  runat="server" style="width: 100%;"></asp:TextBox>
                              </div>
                              <div class="floaddiv" style="width:19%;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">危険物の種別</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddldanggoodsclass" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>

                                  <div class="floaddiv" style="width:20%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">危険物の品名</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddldanggoodsname" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>

                                  <div class="floaddiv" style="width:20%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">危険等級</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlriskrating" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>
                                <div class="floaddiv" style="width:20%;">
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
                              <div class="floaddiv" style="width:15%;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">グリーン購入法</span>
                           </asp:Label>
                          <asp:DropDownList ID="DropDownList18" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                              <div class="floaddiv" style="width:25%;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">特定調達品目</span>
                            </asp:Label>
                          <asp:DropDownList ID="DropDownList19" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>

                                  <div class="floaddiv" style="width:15%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">エコマーク認定品</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="DropDownList20" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>

                                  <div class="floaddiv" style="width:15%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">エコマーク認定番号</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="TextBox134"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                <div class="floaddiv" style="width:`15%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">RoHS指令</span>
                                        </asp:Label>
                                        <asp:DropDownList ID="DropDownList21" style="width:100%;" runat="server"></asp:DropDownList>
                                    </div>               
                            </div>
                          </div>
                     </div>
                    <div class="row" style="margin-top: 10px;margin-right: 1px; margin-bottom:10px">
                         <div class="col-md-12 containerbox"> 
                                                 
                            <div class="rowfirst">
                              <div class="floaddiv" style="width:20%;" >
                              <asp:Label CssClass="lblitemcode"  runat="server" >
                               <span class="label label-md lbl yellowlable ">お客様組立て</span>
                           </asp:Label>
                          <asp:DropDownList ID="ddlcustomerassembly" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                              <div class="floaddiv" style="width:25%;" >
                              <asp:Label  runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">消防法上、届出を必要とする物質</span>
                            </asp:Label>
                          <asp:DropDownList ID="ddlfirelaw" style="width:100%;" runat="server"></asp:DropDownList>
                            
                              </div>

                                  <div class="floaddiv" style="width:10%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">倉庫コード</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtwarehouse_code"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>

                                  <div class="floaddiv" style="width:10%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">出荷日数</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtday_ship"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>
                                <div class="floaddiv" style="width:10%;">
                                        <asp:Label CssClass=""  runat="server" >
                                        <span class="label label-md lbl yellowlable ">返品承認要否</span>
                                        </asp:Label>
                                        <asp:TextBox CssClass="txtbox" ID="txtreturn_necessary"  runat="server" style="width: 100%;"></asp:TextBox>
                                    </div>

                              <div class="floaddiv" style="width:15%;">
                                      
                            <asp:Label  ID="Label6" runat="server" Text="">
                                 <span class="label label-md lbl yellowlable">医薬品・医療機器</span>
                            </asp:Label>
                             <asp:DropDownList ID="DropDownList17" style="width:100%;" runat="server"></asp:DropDownList>
                              </div>
                                     
                            </div>
                          </div>
                     </div>
<%--</div>--%>
                </section>
                <div class="row rowbtn">
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" runat="server" ID="Button1" Text="複製コピー"  />               
                    </div>
                    <div class="col-md-2 btncolum">                             
                        <asp:Button CssClass="mainbtnbox btndecoraction" runat="server" ID="Button3" Text="プレビュー"  />                                 
                    </div>
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" runat="server" ID="btnSave" Text="登 録" />               
                    </div>
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" runat="server" ID="btnComplete" Text="出品待ち"  />               
                    </div>
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" runat="server" ID="Button9" Text="出品待ち取消し" />              
                    </div>
                    <div class="col-md-2 btncolum">      
                        <asp:Button CssClass="mainbtnbox btndecoraction" runat="server" ID="btnDelete" Text="削除" />                
                    </div>
                 </div>
            </div>
        </div>
        </div>
  

 <script>

     $(document).ready(function () {
         $('#master').show();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
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
       document.getElementById("amaster").focus();
    

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
     });

      $('#asku').click(function() {
        $('#master').hide();
         $('#sku').show();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide();  
     });

      $('#aprice').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').show();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
     });

      $('#aimage').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').show();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').hide(); 
     });

      $('#aoption').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').show();
         $('#malldata').hide();
         $('#setting').hide(); 
     });

      $('#amalldata').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').show();
         $('#setting').hide(); 
     });

      $('#asetting').click(function() {
          $('#master').hide();
         $('#sku').hide();
         $('#price').hide();
         $('#image').hide();
         $('#option').hide();
         $('#malldata').hide();
         $('#setting').show(); 
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

//function handleKeyDownEvent(eventRef)
//{
// var charCode = (window.event) ? eventRef.keyCode : eventRef.which;

// alert(charCode);

// // Arrow keys (37:left, 38:up, 39:right, 40:down)...
// if ( (charCode >= 37) && (charCode <= 40) )
// {
//  if ( window.event )
//   window.event.keyCode = 9;
//  else
//   event.which = 9;

//  return true;
// }

// return true;
//     }  

 function handleKeyDownEvent(id, eventRef)
 {
     
     var charCode = (window.event) ? eventRef.keyCode : eventRef.which;
     var ele = '';
     
    
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
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").innerHTML;
        var retval = window.open('../Item/Mall_Category_Choice.aspx?Mall_ID=' + mallID + '&Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
        if (window.focus) {
            newwin.focus()
        }
        return false;
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
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").innerHTML;
        var hidderValue = document.getElementById("<%= txtYahoo_CategoryID.ClientID %>").value;
        var retval = window.open('../Item/Item_YahooSpecificValue.aspx?YahooMallCategoryID=' + hidderValue + '&Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
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

</asp:Content>
