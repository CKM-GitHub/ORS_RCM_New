using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using ORS_RCM_Common;
using System.Data;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using ORS_RCM;
using System.IO;
using System.Globalization;

namespace Capital_SKS.WebForms.Item
{
    public partial class ORS_Item_Master : System.Web.UI.Page
    {

        //Global Variables
        Item_Master_Entity ime;
        Item_Master_BL imeBL;
        Item_Category_BL itemCategoryBL;
        Category_BL cbl;
        Item_BL ibl;
        Item_BL item = new Item_BL();
        public int index = 0;
        public int extract = 0;
        public String[] ex = new String[6];
        public String[] cx = new String[100];
        public String[] ids = new String[100];
        string treepath = string.Empty;
        string catpath = string.Empty;
        UserRoleBL user;
        public int flag = 2;
        public string ItemCode
        {
            get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return Request.QueryString["Item_Code"].ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable relItem_Code
        {
            get
            {
                if (Session["relItem_Code" + ItemCode] != null)
                {
                    DataTable dt = (DataTable)Session["relItem_Code" + ItemCode];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable CopyItemCodeList
        {
            get
            {
                if (Session["Item_Code_Copy"] != null)
                {
                    DataTable dt = (DataTable)Session["Item_Code_Copy"];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }


        public DataTable CategoryList
        {
            get
            {
                if (Session["CategoryList_" + ItemCode] != null)
                {
                    DataTable dt = (DataTable)Session["CategoryList_" + ItemCode];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable ImageList
        {
            get
            {
                if (Session["ImageList_" + ItemCode] != null)
                {
                    DataTable dt = (DataTable)Session["ImageList_" + ItemCode];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable MallCategoryID
        {
            get
            {
                if (Session["Mall_Category_ID_" + ItemCode] != null)
                {
                    return (DataTable)Session["Mall_Category_ID_" + ItemCode];
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable Option
        {
            get
            {
                if (Session["Option_" + ItemCode] != null)
                {
                    DataTable dt = (DataTable)Session["Option_" + ItemCode];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public DataTable YahooSpecificValue
        {
            get
            {
                if (Session["YahooSpecificValue_" + ItemCode] != null)
                {
                    return (DataTable)Session["YahooSpecificValue_" + ItemCode];
                }
                else
                {
                    return null;
                }
            }
        }

        public int UserID
        {
            get
            {
                if (Session["User_ID"] != null)
                {
                    return Convert.ToInt32(Session["User_ID"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        string imagePath = ConfigurationManager.AppSettings["ItemImage"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string ControlID = string.Empty;
                UserRoleBL user = new UserRoleBL();
                bool resultRead = user.CanRead(UserID, "099");
                if (resultRead)
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
                if (String.IsNullOrWhiteSpace(txtSale_Price.Text))
                {
                    btnComplete.Enabled = false;
                }
                #region !IsPostBack
                if (!IsPostBack)
                {
                    BindSaleUnit();
                    BindContentUnit1();
                    BindContentUnit2();
                    BindMonotaroddl();
                    BindOption();

                    BindNormalLargeKBN();
                    BindddlPublicationType();
                    BindDDlDirectDelivery();
                    Bindddlgreenpurchasemethod();
                    BindSpecifiedprocurementitem();
                    Bindddlecomartcertifiedproduct();
                    BindddlRoHSdirective();
                    BindddlPharmaceuticalsandmedicaldevices();
                    BindddlJISConform();
                    BindddlISOConform();

                    // //// BindORSTag();//updated 3/6/2021
                    ime = new Item_Master_Entity();
                    imeBL = new Item_Master_BL();
                    Item_BL item = new Item_BL();
                    //After Save Successful or Update Successful , Back to pervious page
                    #region BackPage ViewState
                    String backpage = string.Empty;
                    if (Request.UrlReferrer != null)
                    {
                        ViewState.Clear();
                        ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
                        backpage = Request.UrlReferrer.ToString();
                    }
                    else
                    {

                        ViewState["UrlReferrer"] = backpage;
                    }
                    #endregion
                 ////  BindShop(); //Bind Shop List'
                    if (ItemCode != null)    //Come from Item View for edit
                    {
                        //Change button name
                        btnSave.Text = "更新";
                        int ItemID = imeBL.SelectItemID(ItemCode);

                        ime = imeBL.SelectByID(ItemID);  //Select From Item_Master Table
                        SetItemData(ime);
                        SetSelectedRelatedItem(ItemID);
                        SetSelectedShop(ItemID);             //Select From Item_Shop Table
                        SetSelectedCategory(ItemID);      //Select From Item_Category Table
                        SetCategoryData();
                     //   SetJishaCategoryData();
                        SelectByItemID(ItemID);                   
                        BindPhotoList();
                        BindShopName();
                        SetItemCodeURL();
                        //SetSelectedRelatedItem(ItemID);   //Select From Item_Related_Item Table

                        #region EDITED BY T.Z.A 15-03-2019

                        SKU_BIND();

 
                        #endregion

                       // BindDailyFlag(ItemCode); //for sks-587
                        SelectTemplateDetail(ItemCode);  //Select From Template_Detail Table
                        GetOptionSelectByItemID(ItemID);    //Select From Item_Option Table
                        SetYahooSpacificValue(ItemID);   //Select From Item_YahooSpecificValue Table
                        ChangeNUll_To_Space();
                    }
                  //  SetInitialRow();
                    if (ControlID.Contains("btnAddCatagories"))
                    {
                        if (Session["btnPopClick_" + ItemCode] != null && Session["btnPopClick_" + ItemCode].ToString() == "ok")
                        {
                            SetMallCategoryData();
                          //  SetJishaCategoryData();
                            Session.Remove("btnPopClick_" + ItemCode);
                        }
                        else
                        {
                            Session.Remove("btnPopClick_" + ItemCode);
                        }
                    }
                }
                #endregion

                #region IsPostBack
                else if (IsPostBack)
                {
                    DataTable dt1 = new DataTable();
                    if (Session["btnRelatedbtn_" + ItemCode] != null && Session["btnRelatedbtn_" + ItemCode].ToString() == "ok")
                    {                 
                        DisplayRelatedItem();
                        Session.Remove("btnRelatedbtn_" + ItemCode);
                    }
                    else 
                    {
                        dt1.Columns.Add("Related_ItemCode", typeof(String));
                        DataRow dr = dt1.NewRow();
                        if (!String.IsNullOrWhiteSpace(txtRelated1.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated1.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated2.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated2.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated3.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated3.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated4.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated4.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated5.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated5.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated6.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated6.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated7.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated7.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated8.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated8.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated9.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated9.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated10.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated10.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated11.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated11.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated12.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated12.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated13.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated13.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated14.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated14.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated15.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated15.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated16.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated16.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated17.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated17.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated18.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated18.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated19.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated19.Text;
                            dt1.Rows.Add(dr);
                        }
                        if (!String.IsNullOrWhiteSpace(txtRelated20.Text))
                        {
                            dr = dt1.NewRow();
                            dr["Related_ItemCode"] = txtRelated20.Text;
                            dt1.Rows.Add(dr);
                        }
                        Session["relItem_Code" + ItemCode] = dt1;
                        Session.Remove("btnRelatedbtn_" + ItemCode);
                    }
                

                if (!String.IsNullOrEmpty(CustomHiddenField.Value))
                    {
                        ControlID = CustomHiddenField.Value;
                    }
                    DataTable dt = RebindItemCodeURL(ControlID);
                    //if (dlShop.Items.Count == 0 || dt.Rows.Count == 0)
                    //{
                    //    BindShopName();
                    //}
                    //if (ControlID.Contains("lnkAddPhoto"))
                    //{
                    //    ReBindPhotoList();
                    //}
                    if (ControlID.Contains("btnAddOption"))
                    {
                        ShowOption();
                    }
                    else if (ControlID.Contains("btnAddCatagories"))
                    {
                        if (Session["btnPopClick_" + ItemCode] != null && Session["btnPopClick_" + ItemCode].ToString() == "ok")
                        {
                            SetMallCategoryData();
                           // SetJishaCategoryData();
                            Session.Remove("btnPopClick_" + ItemCode);
                        }
                        else
                        {
                            Session.Remove("btnPopClick_" + ItemCode);
                        }
                    }
                    else if (ControlID.Contains("btnCopy"))
                    {
                        ReBindNewItemCode();
                    }
                    else if (ControlID.Contains("btnRakuten_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                    }
                    else if (ControlID.Contains("btnYahoo_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                    }
                    else if (ControlID.Contains("btnWowma_CategoryID"))
                    {
                        DisplayMallCategory(); // for display Mall_Category from popup form
                    }
                    
                    else if (ControlID.Contains("imgbYahooSpecValue"))
                    {
                        if (Session["btnYPopClick_" + ItemCode] != null && Session["btnYPopClick_" + ItemCode].ToString() == "ok")
                        {
                            ShowValue();
                            Session.Remove("btnYPopClick_" + ItemCode);
                        }
                        else
                        {
                            Session.Remove("btnYPopClick_" + ItemCode);
                        }
                    }
                    else if (ControlID.Contains("btnAddSKU"))
                    {
                        SKU_BIND();
                    }

                }
                #endregion

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

        }

        //protected void Upload(object sender, EventArgs e)
        //{
        //    FileUploadtest.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUpload1.FileName)));
        //    lblMessage.Visible = true;
        //}

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList myData = new ArrayList();
                myData.Add(txtItem_Code.Text);
                myData.Add(txtItem_Name.Text);
                myData.Add(txtItem_Name.Text);
                myData.Add(txtItem_Name.Text);
                myData.Add(txtList_Price.Text);
                myData.Add(txtSale_Price.Text);
                myData.Add(txtItem_Description_PC.Text);
                myData.Add(txtSale_Description_PC.Text);
                myData.Add(txtRelated1.Text);
                myData.Add(txtRelated2.Text);
                myData.Add(txtRelated3.Text);
                myData.Add(txtRelated4.Text);
                myData.Add(txtRelated5.Text);
               
                Session["myDatatable"] = myData;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('../Item/Item_Preview_Edit.aspx?Item_Code=" + ItemCode + "','_blank');", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnToCancelExhibit_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            imeBL = new Item_Master_BL();
            itemCategoryBL = new Item_Category_BL();
            int ItemID = imeBL.SelectItemID(ItemCode);
            DataTable dtshop = CheckConditon(ItemID, ItemCode);
            DataTable dt = itemCategoryBL.SelectByItemID(ItemID);
            DataTable dtImage = ImageList as DataTable;
            DataRow[] rowRakuten = dtshop.Select("ShopID=1 OR ShopID=5 OR ShopID=8 OR ShopID=12 ");
            DataRow[] rowYahoo = dtshop.Select("ShopID=2 OR ShopID=6 OR ShopID=9 OR ShopID=13  OR ShopID=17");
            DataRow[] rowWowma = dtshop.Select("ShopID=4 ");
            DataRow[] rowTennis = dtshop.Select("ShopID=6 ");
            DataRow[] rowImage = dtImage.Select("Image_Type='0'");
            if (confirmValue == "はい")
            {
                if (dt.Rows.Count <= 0 || (rowRakuten.Count() > 0 && txtRakuten_CategoryID.Text == "") || (rowYahoo.Count() > 0 && txtYahoo_CategoryID.Text == "") ||
                    (rowWowma.Count() > 0 && txtWowma_CategoryID.Text == "") ||/* (rowTennis.Count() > 0 && txtTennis_CategoryID.Text == "") ||*/ (rowImage.Count() == 0))
                {
                    imeBL.ChangeExportStatusToPink(ItemCode, 0);
                }
            }
        }

        public void ReBindNewItemCode()
        {
            try
            {
                if (Session["Item_Code_Copy"] != null)
                {
                    DataTable dt = CopyItemCodeList;
                    if (dt.Rows.Count > 0)
                    {
                        txtItem_Code.Text = dt.Rows[0]["Item_Code"].ToString();
                        txtItem_Name.Text = dt.Rows[0]["Item_Name"].ToString();

                        DataTable dtskucolor = item.SelectSKUItemCode(dt.Rows[0]["Item_Code"].ToString()); //Select From Item Table
                        if (dtskucolor.Rows.Count > 0)
                        {
                            gvSKU.DataSource = dtskucolor;
                            gvSKU.DataBind();

                            rdb1.Checked = true;
                            rdb2.Checked = false;
                        }
                        else
                        {
                            gvSKU.DataSource = dtskucolor;
                            gvSKU.DataBind();

                            rdb1.Checked = false;
                            rdb2.Checked = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #region RelatedItem
        public void DisplayRelatedItem()
        {
            try
            {
                txtRelated1.Text = "";
                txtRelated2.Text = "";
                txtRelated3.Text = "";
                txtRelated4.Text = "";
                txtRelated5.Text = "";
                txtRelated6.Text = "";
                txtRelated7.Text = "";
                txtRelated8.Text = "";
                txtRelated9.Text = "";
                txtRelated10.Text = "";
                txtRelated11.Text = "";
                txtRelated12.Text = "";
                txtRelated13.Text = "";
                txtRelated14.Text = "";
                txtRelated15.Text = "";
                txtRelated16.Text = "";
                txtRelated17.Text = "";
                txtRelated18.Text = "";
                txtRelated19.Text = "";
                txtRelated20.Text = "";
                DataTable dt1 = relItem_Code as DataTable;
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                txtRelated1.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 1:
                                txtRelated2.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 2:
                                txtRelated3.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 3:
                                txtRelated4.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 4:
                                txtRelated5.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 5:
                                txtRelated6.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 6:
                                txtRelated7.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 7:
                                txtRelated8.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 8:
                                txtRelated9.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 9:
                                txtRelated10.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 10:
                                txtRelated11.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 11:
                                txtRelated12.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 12:
                                txtRelated13.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 13:
                                txtRelated14.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 14:
                                txtRelated15.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 15:
                                txtRelated16.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 16:
                                txtRelated17.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 17:
                                txtRelated18.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 18:
                                txtRelated19.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;
                            case 19:
                                txtRelated20.Text = dt1.Rows[i]["Item_Code"].ToString();
                                break;

                        }
                    }
                }
               // Session["relItem_Code" + ItemCode] = dt1;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }
        #endregion

        #region Related_Item
        /// <summary>
        /// Connects to Related Item 
        /// </summary>
        /// <param name="ItemID"> By selected master id</param>
        public void SetSelectedRelatedItem(int ItemID)
        {
            try
            {
                Item_Related_Item_BL ItemRelatedBL = new Item_Related_Item_BL();
                DataTable dt = ItemRelatedBL.SelectByItemID(ItemID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                txtRelated1.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 1:
                                txtRelated2.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 2:
                                txtRelated3.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 3:
                                txtRelated4.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 4:
                                txtRelated5.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 5:
                                txtRelated6.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 6:
                                txtRelated7.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 7:
                                txtRelated8.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 8:
                                txtRelated9.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 9:
                                txtRelated10.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 10:
                                txtRelated11.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 11:
                                txtRelated12.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 12:
                                txtRelated13.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 13:
                                txtRelated14.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 14:
                                txtRelated15.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 15:
                                txtRelated16.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 16:
                                txtRelated17.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 17:
                                txtRelated18.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 18:
                                txtRelated19.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                            case 19:
                                txtRelated20.Text = dt.Rows[i]["Related_ItemCode"].ToString();
                                break;
                        }
                    }
                }
                //Session["Related_Item_Code" + ItemCode] = dt;
                //Session["relItem_Code" + ItemCode] = null;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }
        #endregion

        public void BindShopName()
        {
            try
            {
                Shop_BL shopBL = new Shop_BL();
                Item_Shop_BL isbl = new Item_Shop_BL();
                DataTable dt = shopBL.SelectShop_Data();
                if (ItemCode != null)
                {
                    DataTable dtURL = isbl.SelectItemCodeURL(ItemCode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt.Columns.Add("Item_Code_URL");
                        if (dtURL.Rows.Count <= 0 && !String.IsNullOrWhiteSpace(txtItem_Code.ToString()))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            { dt.Rows[i]["Item_Code_URL"] = txtItem_Code.Text; }
                            if (!String.IsNullOrEmpty(txtItem_Code.Text))
                            {
                                btnComplete.Enabled = true;
                            }

                        }
                        else
                        {
                            foreach (DataListItem li in dlShop.Items)
                            {
                                TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                                Label shopid = li.FindControl("lblShopID") as Label;
                                CheckBox cb = li.FindControl("ckbShop") as CheckBox;
                                if (dtURL != null && dtURL.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtURL.Rows.Count; i++)
                                    {
                                        if ((dtURL.Rows[i]["Item_Code"].ToString() == txtitemcode.Text) && (dtURL.Rows[i]["Shop_ID"].ToString()) == shopid.Text)
                                        {
                                            dt.Rows[i]["Item_Code_URL"] = ItemCode;
                                        }
                                        else
                                        {
                                            if ((dtURL.Rows[i]["Shop_ID"].ToString()) == shopid.Text)
                                            {
                                                dt.Rows[i]["Item_Code_URL"] = txtitemcode.Text;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        dlShop.DataSource = dt;
                        dlShop.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {

            try
            {

                //OpenFileDialog dialog = new OpenFileDialog();
                //if (DialogResult.OK == dialog.ShowDialog())
                //{
                //    string path = dialog.FileName;
                //}

                if (FileUpload1.HasFile)
                {

                    txtimg1.Text = FileUpload1.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg1.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg1.Text = "";                     
                        Image1.ImageUrl = imagePath + "no_image.jpg";
                        hlImage1.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg1.Text.Length > 24)
                    {
                        txtimg1.Text = "";
                        Image1.ImageUrl = imagePath + "no_image.jpg";
                        hlImage1.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload1.SaveAs(Server.MapPath(imagePath) + FileUpload1.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload1.FileName.ToString()))
                        {
                            Image1.ImageUrl = imagePath + FileUpload1.FileName;
                            hlImage1.NavigateUrl = imagePath + FileUpload1.FileName;
                        }
                    }
                }
                else
                {
                    Image1.ImageUrl = imagePath + "no_image.jpg";
                    hlImage1.NavigateUrl = imagePath + "no_image.jpg";
                    txtimg1.Text = "";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton2_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload2.HasFile)
                {

                    txtimg2.Text = FileUpload2.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg2.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg2.Text = "";
                        Image2.ImageUrl = imagePath + "no_image.jpg";
                        hlImage2.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg2.Text.Length > 24)
                    {
                        txtimg2.Text = "";
                        Image2.ImageUrl = imagePath + "no_image.jpg";
                        hlImage2.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload2.SaveAs(Server.MapPath(imagePath) + FileUpload2.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload2.FileName.ToString()))
                        {
                            Image2.ImageUrl = imagePath + FileUpload2.FileName;
                            hlImage2.NavigateUrl = imagePath + FileUpload2.FileName;
                        }
                    }
                }
                else
                {
                    txtimg2.Text = "";
                    Image2.ImageUrl = imagePath + "no_image.jpg";
                    hlImage2.NavigateUrl = imagePath + "no_image.jpg";

                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton3_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload3.HasFile)
                {

                    txtimg3.Text = FileUpload3.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg3.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg3.Text = "";
                        Image3.ImageUrl = imagePath + "no_image.jpg";
                        hlImage3.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg3.Text.Length > 24)
                    {
                        txtimg3.Text = "";
                        Image3.ImageUrl = imagePath + "no_image.jpg";
                        hlImage3.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload3.SaveAs(Server.MapPath(imagePath) + FileUpload3.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload3.FileName.ToString()))
                        {
                            Image3.ImageUrl = imagePath + FileUpload3.FileName;
                            hlImage3.NavigateUrl = imagePath + FileUpload3.FileName;
                        }
                    }
                }
                else
                {
                    txtimg3.Text = "";
                    Image3.ImageUrl = imagePath + "no_image.jpg";
                    hlImage3.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton4_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload4.HasFile)
                {

                    txtimg4.Text = FileUpload4.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg4.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg4.Text = "";
                        Image4.ImageUrl = imagePath + "no_image.jpg";
                        hlImage4.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg4.Text.Length > 24)
                    {
                        txtimg4.Text = "";
                        Image4.ImageUrl = imagePath + "no_image.jpg";
                        hlImage4.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload4.SaveAs(Server.MapPath(imagePath) + FileUpload4.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload4.FileName.ToString()))
                        {
                            Image4.ImageUrl = imagePath + FileUpload4.FileName;
                            hlImage4.NavigateUrl = imagePath + FileUpload4.FileName;
                        }
                    }
                }
                else
                {
                    txtimg4.Text = "";
                    Image4.ImageUrl = imagePath + "no_image.jpg";
                    hlImage4.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton5_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload5.HasFile)
                {

                    txtimg5.Text = FileUpload5.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg5.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg5.Text = "";
                        Image5.ImageUrl = imagePath + "no_image.jpg";
                        hlImage5.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg5.Text.Length > 24)
                    {
                        txtimg5.Text = "";
                        Image5.ImageUrl = imagePath + "no_image.jpg";
                        hlImage5.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload5.SaveAs(Server.MapPath(imagePath) + FileUpload5.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload5.FileName.ToString()))
                        {
                            Image5.ImageUrl = imagePath + FileUpload5.FileName;
                            hlImage5.NavigateUrl = imagePath + FileUpload5.FileName;
                        }
                    }
                }
                else
                {
                    txtimg5.Text = "";
                    Image5.ImageUrl = imagePath + "no_image.jpg";
                    hlImage5.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton6_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload6.HasFile)
                {

                    txtimg6.Text = FileUpload6.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg6.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg6.Text = "";
                        Image6.ImageUrl = imagePath + "no_image.jpg";
                        hlImage6.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg6.Text.Length > 24)
                    {
                        txtimg6.Text = "";
                        Image6.ImageUrl = imagePath + "no_image.jpg";
                        hlImage6.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload6.SaveAs(Server.MapPath(imagePath) + FileUpload6.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload6.FileName.ToString()))
                        {
                            Image6.ImageUrl = imagePath + FileUpload6.FileName;
                            hlImage6.NavigateUrl = imagePath + FileUpload6.FileName;
                        }
                    }
                }
                else
                {
                    txtimg6.Text = "";
                    Image6.ImageUrl = imagePath + "no_image.jpg";
                    hlImage6.NavigateUrl = imagePath + "no_image.jpg";

                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton7_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload7.HasFile)
                {

                    txtimg7.Text = FileUpload7.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg7.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg7.Text = "";
                        Image7.ImageUrl = imagePath + "no_image.jpg";
                        hlImage7.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg7.Text.Length > 24)
                    {
                        txtimg7.Text = "";
                        Image7.ImageUrl = imagePath + "no_image.jpg";
                        hlImage7.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload7.SaveAs(Server.MapPath(imagePath) + FileUpload7.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload7.FileName.ToString()))
                        {
                            Image7.ImageUrl = imagePath + FileUpload7.FileName;
                            hlImage7.NavigateUrl = imagePath + FileUpload7.FileName;
                        }
                    }
                }
                else
                {
                    txtimg7.Text = "";
                    Image7.ImageUrl = imagePath + "no_image.jpg";
                    hlImage7.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton8_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload8.HasFile)
                {

                    txtimg8.Text = FileUpload8.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg8.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg8.Text = "";
                        Image8.ImageUrl = imagePath + "no_image.jpg";
                        hlImage8.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg8.Text.Length > 24)
                    {
                        txtimg8.Text = "";
                        Image8.ImageUrl = imagePath + "no_image.jpg";
                        hlImage8.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload8.SaveAs(Server.MapPath(imagePath) + FileUpload8.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload8.FileName.ToString()))
                        {
                            Image8.ImageUrl = imagePath + FileUpload8.FileName;
                            hlImage8.NavigateUrl = imagePath + FileUpload8.FileName;
                        }
                    }
                }
                else
                {
                    txtimg8.Text = "";
                    Image8.ImageUrl = imagePath + "no_image.jpg";
                    hlImage8.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton9_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload9.HasFile)
                {

                    txtimg9.Text = FileUpload9.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg9.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg9.Text = "";
                        Image9.ImageUrl = imagePath + "no_image.jpg";
                        hlImage9.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg9.Text.Length > 24)
                    {
                        txtimg9.Text = "";
                        Image9.ImageUrl = imagePath + "no_image.jpg";
                        hlImage9.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload9.SaveAs(Server.MapPath(imagePath) + FileUpload9.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload9.FileName.ToString()))
                        {
                            Image9.ImageUrl = imagePath + FileUpload9.FileName;
                            hlImage9.NavigateUrl = imagePath + FileUpload9.FileName;
                        }
                    }
                }
                else
                {
                    txtimg9.Text = "";
                    Image9.ImageUrl = imagePath + "no_image.jpg";
                    hlImage9.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton10_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload10.HasFile)
                {

                    txtimg10.Text = FileUpload10.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg10.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg10.Text = "";
                        Image10.ImageUrl = imagePath + "no_image.jpg";
                        hlImage10.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg10.Text.Length > 24)
                    {
                        txtimg10.Text = "";
                        Image10.ImageUrl = imagePath + "no_image.jpg";
                        hlImage10.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload10.SaveAs(Server.MapPath(imagePath) + FileUpload10.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload10.FileName.ToString()))
                        {
                            Image10.ImageUrl = imagePath + FileUpload10.FileName;
                            hlImage10.NavigateUrl = imagePath + FileUpload10.FileName;
                        }
                    }
                }
                else
                {
                    txtimg10.Text = "";
                    Image10.ImageUrl = imagePath + "no_image.jpg";
                    hlImage10.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton11_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload11.HasFile)
                {

                    txtimg11.Text = FileUpload11.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg11.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg11.Text = "";
                        Image11.ImageUrl = imagePath + "no_image.jpg";
                        hlImage11.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg11.Text.Length > 24)
                    {
                        txtimg11.Text = "";
                        Image11.ImageUrl = imagePath + "no_image.jpg";
                        hlImage11.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload11.SaveAs(Server.MapPath(imagePath) + FileUpload11.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload11.FileName.ToString()))
                        {
                            Image11.ImageUrl = imagePath + FileUpload11.FileName;
                            hlImage11.NavigateUrl = imagePath + FileUpload11.FileName;
                        }
                    }
                }
                else
                {
                    txtimg11.Text = "";
                    Image11.ImageUrl = imagePath + "no_image.jpg";
                    hlImage11.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton12_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload12.HasFile)
                {

                    txtimg12.Text = FileUpload12.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg12.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg12.Text = "";
                        Image12.ImageUrl = imagePath + "no_image.jpg";
                        hlImage12.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg12.Text.Length > 24)
                    {
                        txtimg12.Text = "";
                        Image12.ImageUrl = imagePath + "no_image.jpg";
                        hlImage1.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload12.SaveAs(Server.MapPath(imagePath) + FileUpload12.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload12.FileName.ToString()))
                        {
                            Image12.ImageUrl = imagePath + FileUpload12.FileName;
                            hlImage12.NavigateUrl = imagePath + FileUpload12.FileName;
                        }
                    }
                }
                else
                {
                    txtimg12.Text = "";
                    Image12.ImageUrl = imagePath + "no_image.jpg";
                    hlImage12.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton13_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload13.HasFile)
                {

                    txtimg13.Text = FileUpload13.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg13.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg13.Text = "";
                        Image13.ImageUrl = imagePath + "no_image.jpg";
                        hlImage13.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg13.Text.Length > 24)
                    {
                        txtimg13.Text = "";
                        Image13.ImageUrl = imagePath + "no_image.jpg";
                        hlImage13.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload13.SaveAs(Server.MapPath(imagePath) + FileUpload13.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload13.FileName.ToString()))
                        {
                            Image13.ImageUrl = imagePath + FileUpload13.FileName;
                            hlImage13.NavigateUrl = imagePath + FileUpload13.FileName;
                        }
                    }
                }
                else
                {
                    txtimg13.Text = "";
                    Image13.ImageUrl = imagePath + "no_image.jpg";
                    hlImage13.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton14_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload14.HasFile)
                {

                    txtimg14.Text = FileUpload14.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg14.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg14.Text = "";
                        Image14.ImageUrl = imagePath + "no_image.jpg";
                        hlImage14.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg14.Text.Length > 24)
                    {
                        txtimg14.Text = "";
                        Image14.ImageUrl = imagePath + "no_image.jpg";
                        hlImage14.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload14.SaveAs(Server.MapPath(imagePath) + FileUpload14.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload14.FileName.ToString()))
                        {
                            Image14.ImageUrl = imagePath + FileUpload14.FileName;
                            hlImage14.NavigateUrl = imagePath + FileUpload14.FileName;
                        }
                    }
                }
                else
                {
                    txtimg14.Text = "";
                    Image14.ImageUrl = imagePath + "no_image.jpg";
                    hlImage14.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton15_Click(object sender, EventArgs e)
        {

            try 
            {
                if (FileUpload15.HasFile)
                {

                    txtimg15.Text = FileUpload15.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg15.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg15.Text = "";
                        Image15.ImageUrl = imagePath + "no_image.jpg";
                        hlImage15.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg15.Text.Length > 24)
                    {
                        txtimg15.Text = "";
                        Image15.ImageUrl = imagePath + "no_image.jpg";
                        hlImage15.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload15.SaveAs(Server.MapPath(imagePath) + FileUpload15.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload15.FileName.ToString()))
                        {
                            Image15.ImageUrl = imagePath + FileUpload15.FileName;
                            hlImage15.NavigateUrl = imagePath + FileUpload15.FileName;
                        }
                    }
                }
                else
                {
                    txtimg15.Text = "";
                    Image15.ImageUrl = imagePath + "no_image.jpg";
                    hlImage15.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton16_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload16.HasFile)
                {

                    txtimg16.Text = FileUpload16.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg16.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg16.Text = "";
                        Image16.ImageUrl = imagePath + "no_image.jpg";
                        hlImage16.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg16.Text.Length > 24)
                    {
                        txtimg16.Text = "";
                        Image16.ImageUrl = imagePath + "no_image.jpg";
                        hlImage16.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload16.SaveAs(Server.MapPath(imagePath) + FileUpload16.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload16.FileName.ToString()))
                        {
                            Image16.ImageUrl = imagePath + FileUpload16.FileName;
                            hlImage16.NavigateUrl = imagePath + FileUpload16.FileName;
                        }
                    }
                }
                else
                {
                    txtimg16.Text = "";
                    Image16.ImageUrl = imagePath + "no_image.jpg";
                    hlImage16.NavigateUrl = imagePath + "no_image.jpg";

                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton17_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload17.HasFile)
                {

                    txtimg17.Text = FileUpload17.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg17.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg17.Text = "";
                        Image17.ImageUrl = imagePath + "no_image.jpg";
                        hlImage17.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg17.Text.Length > 24)
                    {
                        txtimg17.Text = "";
                        Image17.ImageUrl = imagePath + "no_image.jpg";
                        hlImage17.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload17.SaveAs(Server.MapPath(imagePath) + FileUpload17.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload17.FileName.ToString()))
                        {
                            Image17.ImageUrl = imagePath + FileUpload17.FileName;
                            hlImage17.NavigateUrl = imagePath + FileUpload17.FileName;
                        }
                    }
                }
                else
                {
                    txtimg17.Text = "";
                    Image17.ImageUrl = imagePath + "no_image.jpg";
                    hlImage17.NavigateUrl = imagePath + "no_image.jpg";

                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton18_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload18.HasFile)
                {

                    txtimg18.Text = FileUpload18.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg18.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg18.Text = "";
                        Image18.ImageUrl = imagePath + "no_image.jpg";
                        hlImage18.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg18.Text.Length > 24)
                    {
                        txtimg18.Text = "";
                        Image18.ImageUrl = imagePath + "no_image.jpg";
                        hlImage18.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload18.SaveAs(Server.MapPath(imagePath) + FileUpload18.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload18.FileName.ToString()))
                        {
                            Image18.ImageUrl = imagePath + FileUpload18.FileName;
                            hlImage18.NavigateUrl = imagePath + FileUpload18.FileName;
                        }
                    }
                }
                else
                {
                    txtimg18.Text = "";
                    Image18.ImageUrl = imagePath + "no_image.jpg";
                    hlImage18.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton19_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload19.HasFile)
                {

                    txtimg19.Text = FileUpload19.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg19.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg19.Text = "";
                        Image19.ImageUrl = imagePath + "no_image.jpg";
                        hlImage19.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg19.Text.Length > 24)
                    {
                        txtimg19.Text = "";
                        Image19.ImageUrl = imagePath + "no_image.jpg";
                        hlImage19.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload19.SaveAs(Server.MapPath(imagePath) + FileUpload19.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload19.FileName.ToString()))
                        {
                            Image19.ImageUrl = imagePath + FileUpload19.FileName;
                            hlImage19.NavigateUrl = imagePath + FileUpload19.FileName;
                        }
                    }
                }
                else
                {
                    txtimg19.Text = "";
                    Image19.ImageUrl = imagePath + "no_image.jpg";
                    hlImage19.NavigateUrl = imagePath + "no_image.jpg";

                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void UploadButton20_Click(object sender, EventArgs e)
        {

            try
            {
                if (FileUpload20.HasFile)
                {

                    txtimg20.Text = FileUpload20.FileName;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtimg20.Text, ItemCode + "-[0-1]?[0-9]|20.jpg"))
                    {
                        txtimg20.Text = "";
                        Image20.ImageUrl = imagePath + "no_image.jpg";
                        hlImage20.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("無効画像名");
                        return;
                    }

                    if (txtimg20.Text.Length > 24)
                    {
                        txtimg20.Text = "";
                        Image20.ImageUrl = imagePath + "no_image.jpg";
                        hlImage20.NavigateUrl = imagePath + "no_image.jpg";
                        GlobalUI.MessageBox("画像ファイル名は20文字までです");
                        return;
                    }
                    else
                    {

                        FileUpload20.SaveAs(Server.MapPath(imagePath) + FileUpload20.FileName);
                        if (!string.IsNullOrWhiteSpace(FileUpload20.FileName.ToString()))
                        {
                            Image20.ImageUrl = imagePath + FileUpload20.FileName;
                            hlImage20.NavigateUrl = imagePath + FileUpload20.FileName;
                        }
                    }
                }
                else
                {
                    txtimg20.Text = "";
                    Image20.ImageUrl = imagePath + "no_image.jpg";
                    hlImage20.NavigateUrl = imagePath + "no_image.jpg";
                }

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }


        #region Mall_Category
        public void DisplayMallCategory()
        {
            try
            {
                if (MallCategoryID != null)
                {
                    DataTable dt = MallCategoryID as DataTable;
                    if (dt.Rows[0]["Mall_ID"].ToString() == "1")
                    {
                        txtRakuten_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                        txtRakuten_CategoryPath.Text = dt.Rows[0]["Category_Path"].ToString();
                    }
                    else if (dt.Rows[0]["Mall_ID"].ToString() == "2")
                    {
                        txtYahoo_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                        txtYahoo_CategoryPath.Text = dt.Rows[0]["Category_Path"].ToString();
                    }
                    else if (dt.Rows[0]["Mall_ID"].ToString() == "4")
                    {
                        txtWowma_CategoryID.Text = dt.Rows[0]["Category_ID"].ToString();
                        txtWowma_CategoryPath.Text = dt.Rows[0]["Category_Path"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        #endregion

        public DataTable RebindItemCodeURL(string ctrl)
        {


            Item_Shop_BL isbl = new Item_Shop_BL();
            DataTable dt = new DataTable();
            dt.Columns.Add("Shop_ID", typeof(int));
            dt.Columns.Add("Item_Code_URL", typeof(string));
            //foreach (DataListItem li in dlShop.Items)
            //{
            //    TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
            //    Label shopid = li.FindControl("lblShopID") as Label;
            //    CheckBox cb = li.FindControl("ckbShop") as CheckBox;
            //    string icode = txtItem_Code.Text;
            //    if (icode == txtitemcode.Text)
            //    {
            //        if (cb != null)
            //        {
            //            if (cb.Checked)
            //            {
            //                DataRow dr = dt.NewRow();
            //                dr["Item_Code_URL"] = txtitemcode.Text;
            //                dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
            //                dt.Rows.Add(dr);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (cb != null)
            //        {
            //            if (cb.Checked)
            //            {
            //                if (ctrl.Contains("txtItem_Code"))
            //                {
            //                    DataRow dr = dt.NewRow();
            //                    dr["Item_Code_URL"] = txtItem_Code.Text;
            //                    dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
            //                    dt.Rows.Add(dr);
            //                }
            //                else
            //                {
            //                    DataRow dr = dt.NewRow();
            //                    dr["Item_Code_URL"] = txtitemcode.Text;
            //                    dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
            //                    dt.Rows.Add(dr);
            //                }
            //            }
            //        }
            //    }
            //}
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        foreach (DataListItem li in dlShop.Items)
            //        {
            //            TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
            //            Label shopid = li.FindControl("lblShopID") as Label;
            //            CheckBox cb = li.FindControl("ckbShop") as CheckBox;
            //            if (shopid.Text == dt.Rows[i]["Shop_ID"].ToString())
            //            {
            //                cb.Checked = true;
            //                txtitemcode.Text = dt.Rows[i]["Item_Code_URL"].ToString();
            //                break;
            //            }
            //        }
            //    }
            //}
            return dt;
        }

        public void SetMallCategoryData()
        {
            int rowIndex = 0;
            gvCatagories.DataSource = CategoryList;
            gvCatagories.DataBind();
            if (CategoryList != null && CategoryList.Rows.Count > 0)
            {
                for (int i = rowIndex; i < CategoryList.Rows.Count; i++)
                {
                    Label lblID = (Label)gvCatagories.Rows[rowIndex].Cells[1].FindControl("lblID");
                    TextBox txtValue = (TextBox)gvCatagories.Rows[rowIndex].Cells[1].FindControl("txtCTGName");
                    lblID.Text = CategoryList.Rows[i]["CID"].ToString();
                    txtValue.Text = CategoryList.Rows[i]["CName"].ToString();
                    rowIndex++;
                }
                Category_BL catbl = new Category_BL();
                DataTable dt = new DataTable();
                dt = CategoryList;

                if (dt != null && dt.Rows.Count > 0)
                {
                    String id = (dt.Rows[0]["CID"]).ToString();

                    if (dt.Rows.Count > 0)
                    {
                        DataTable driectdt = new DataTable();
                        driectdt = catbl.Get_CategoryID(id);
                        if (driectdt.Rows.Count > 0)
                        {
                            txtRakuten_CategoryID.Text = driectdt.Rows[0]["Rakutan_DirectoryID"].ToString();
                            txtRakuten_CategoryPath.Text = driectdt.Rows[0]["Rakuten_CategoryName"].ToString();
                            txtYahoo_CategoryID.Text = driectdt.Rows[0]["Yahoo_CategoryID"].ToString();
                            txtYahoo_CategoryPath.Text = driectdt.Rows[0]["Yahoo_CategoryName"].ToString();
                            txtWowma_CategoryID.Text = driectdt.Rows[0]["Wowma_CategoryID"].ToString();
                            txtWowma_CategoryPath.Text = driectdt.Rows[0]["Wowma_CategoryName"].ToString();

                            //hhw
                            //txtTennis_CategoryID.Text = driectdt.Rows[0]["Tennis_CategoryID"].ToString();
                            //txtTennis_CategoryPath.Text = driectdt.Rows[0]["Tennis_CategoryName"].ToString();
                        }
                    }
                }
                else
                {
                    txtRakuten_CategoryID.Text = string.Empty;
                    txtYahoo_CategoryID.Text = string.Empty;
                    txtWowma_CategoryID.Text = string.Empty;
                    //txtTennis_CategoryID.Text = string.Empty;
                    txtRakuten_CategoryPath.Text = "";
                    txtYahoo_CategoryPath.Text = "";
                    txtWowma_CategoryPath.Text = "";
                    //txtTennis_CategoryPath.Text = "";
                }
            }
            else
            {
                txtRakuten_CategoryID.Text = string.Empty;
                txtYahoo_CategoryID.Text = string.Empty;
                txtWowma_CategoryID.Text = string.Empty;
                //txtTennis_CategoryID.Text = string.Empty;
                txtRakuten_CategoryPath.Text = "";
                txtYahoo_CategoryPath.Text = "";
                txtWowma_CategoryPath.Text = "";
                //txtTennis_CategoryPath.Text = "";
            }
        }

        protected DataTable CreateTemplateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Template");
    
            dt.Rows.Add(txtTemplate1.Text);
            dt.Rows.Add(txtTemplate_Content1.Text);
            dt.Rows.Add(txtTemplate2.Text);
            dt.Rows.Add(txtTemplate_Content2.Text);
            dt.Rows.Add(txtTemplate3.Text);
            dt.Rows.Add(txtTemplate_Content3.Text);
            dt.Rows.Add(txtTemplate4.Text);
            dt.Rows.Add(txtTemplate_Content4.Text);
            dt.Rows.Add(txtTemplate5.Text);
            dt.Rows.Add(txtTemplate_Content5.Text);
            dt.Rows.Add(txtTemplate6.Text);
            dt.Rows.Add(txtTemplate_Content6.Text);
            dt.Rows.Add(txtTemplate7.Text);
            dt.Rows.Add(txtTemplate_Content7.Text);
            dt.Rows.Add(txtTemplate8.Text);
            dt.Rows.Add(txtTemplate_Content8.Text);
            dt.Rows.Add(txtTemplate9.Text);
            dt.Rows.Add(txtTemplate_Content9.Text);
            dt.Rows.Add(txtTemplate10.Text);
            dt.Rows.Add(txtTemplate_Content10.Text);
            dt.Rows.Add(txtTemplate11.Text);
            dt.Rows.Add(txtTemplate_Content11.Text);
            dt.Rows.Add(txtTemplate12.Text);
            dt.Rows.Add(txtTemplate_Content12.Text);
            dt.Rows.Add(txtTemplate13.Text);
            dt.Rows.Add(txtTemplate_Content13.Text);
            dt.Rows.Add(txtTemplate14.Text);
            dt.Rows.Add(txtTemplate_Content14.Text);
            dt.Rows.Add(txtTemplate15.Text);
            dt.Rows.Add(txtTemplate_Content15.Text);
            dt.Rows.Add(txtTemplate16.Text);
            dt.Rows.Add(txtTemplate_Content16.Text);
            dt.Rows.Add(txtTemplate17.Text);
            dt.Rows.Add(txtTemplate_Content17.Text);
            dt.Rows.Add(txtTemplate18.Text);
            dt.Rows.Add(txtTemplate_Content18.Text);
            dt.Rows.Add(txtTemplate19.Text);
            dt.Rows.Add(txtTemplate_Content19.Text);
            dt.Rows.Add(txtTemplate20.Text);
            dt.Rows.Add(txtTemplate_Content20.Text);
            dt.Rows.Add(txtItem_Description_PC.Text);
            dt.Rows.Add(txtSale_Description_PC.Text);
            dt.Rows.Add(txtSmart_Template.Text);
            dt.Rows.Add(txtMerchandise_Information.Text);

            return dt;
        }

        protected Boolean Check_SpecialCharacter(String[] columnName, DataTable dt)
        {
            try
            {
                DataColumnCollection col = dt.Columns;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        for (int k = 0; k < columnName.Length; k++)
                        {
                            if (dt.Columns[j].ColumnName == columnName[k])
                            {

                                string input = dt.Rows[i][j].ToString();
                                string specialChar = @"㈰㈪㈫㈬㈭㈮㈯㉀㈷㉂㉃㈹㈺㈱㈾㈴㈲㈻㈶㈳㈵㈼㈽㈿㈸㊤㊥㊦㊧㊨㊩㊖㊝㊘㊞㊙㍾㍽㍼㍻㍉㌢㌔㌖㌅㌳㍎㌃㌶㌘㌕㌧㍑㍊㌹㍗㌍㍂㌣㌦㌻㌫㍍№℡㎜㎟㎝㎠㎤㎡㎥㎞㎢㎎㎏㏄㎖㎗㎘㎳㎲㎱㎰①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩⅰⅱⅲⅳⅴⅵⅶⅷⅸⅹ";
                                string comma = ",";
                                string plusign = "[[(+)]]";
                                string minussign = "[[(-)]]";

                                foreach (var item in specialChar)
                                {
                                    if (input.Contains(item))
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Template description contains special character.');", true);
                                        return true;
                                    }

                                }
                                if (input.Contains(plusign) || input.Contains(minussign))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Template description contains special character.');", true);
                                    return true;
                                }

                            }
                        }
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        public Item_Master_Entity GetItemData()
        {
            try
            {
                ime.Ctrl_ID = hdfCtrl_ID.Value;
                if (Request.QueryString["Item_Code"] != null)
                {
                    ime.Item_Code = txtItem_Code.Text;
                }
                ime.Updated_By = UserID;
                ime.Item_Name = txtItem_Name.Text.TrimStart();
                ime.JanCode = txtJanCD.Text.TrimStart();
                ime.Memo = txtmemo.Text.TrimStart();
                ime.Siiresaki = txtsiiresaki.Text.TrimStart();
                int normallargeKBN = 0;
                if (Convert.ToInt32(ddNormalLargeKBN.SelectedIndex) == 1)
                {
                    normallargeKBN = 0;
                }
                else if (Convert.ToInt32(ddNormalLargeKBN.SelectedIndex) == 2)
                {
                    normallargeKBN = 1;
                }
                ime.NormalLargeKBN = normallargeKBN;
                ime.Product_Code = txtProduct_Code.Text.TrimStart();
                string release = Request.Form[txtRelease_Date.UniqueID];
                string post = Request.Form[txtPost_Available_Date.UniqueID];
                if (!string.IsNullOrWhiteSpace(release))
                {
                    ime.Release_Date = Convert.ToDateTime(release);
                }
                if (!string.IsNullOrWhiteSpace(post))
                {
                    ime.Post_Available_Date = Convert.ToDateTime(post);
                }
               // ime.Season = txtSeason.Text.TrimStart();
                ime.Brand_Name = txtBrand_Name.Text.TrimStart();
                ime.Brand_Code = txtBrand_Code.Text.TrimStart();
                //ime.Competition_Name = txtCompetition_Name.Text.TrimStart();
                //ime.Class_Name = txtClass_Name.Text.Trim().TrimStart();
                //ime.Catalog_Information = txtCatalog_Information.Text.TrimStart();
                ime.Merchandise_Information = txtMerchandise_Information.Text.TrimStart();
                //ime.Zett_Item_Description = txtZett_Item_Description.Text.TrimStart();
                //ime.Zett_Sale_Description = txtZett_Sale_Description.Text.TrimStart();
                ime.Item_Description_PC = txtItem_Description_PC.Text.TrimStart();
                ime.Sale_Description_PC = txtSale_Description_PC.Text.TrimStart();
                ime.Smart_Template = txtSmart_Template.Text;
                //ime.Additional_2 = txtAdditional_2.Text;
                //ime.Additional_3 = txtAdditional_3.Text;
                //ime.BlackMarket_Password = txtBlackMarket_Password.Text.TrimStart();
                //ime.DoublePrice_Ctrl_No = txtDoublePrice_Ctrl_No.Text.TrimStart();
                if (!string.IsNullOrWhiteSpace(txtExtra_Shipping.Text))
                {
                    ime.Extra_Shipping = Convert.ToInt32(txtExtra_Shipping.Text.TrimStart());
                }
                ime.Maker_Code = txtmaker_code.Text;
             //   ime.Year = txtYear.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtList_Price.Text))
                {
                    ime.List_Price = int.Parse(txtList_Price.Text.Replace(",", string.Empty));
                }
                //if (!string.IsNullOrWhiteSpace(txtJisha_Price.Text))
                //{
                //    ime.Jisha_Price = int.Parse(txtJisha_Price.Text.Replace(",", string.Empty));
                //}
                if (!string.IsNullOrWhiteSpace(txtSale_Price.Text))
                {
                    ime.Sale_Price = int.Parse(txtSale_Price.Text.Replace(",", string.Empty));
                }

                if (!string.IsNullOrWhiteSpace(txtRakutenPrice.Text))
                {
                    ime.RakutenPrice = int.Parse(txtRakutenPrice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtYahooPrice.Text))
                {
                    ime.YahooPrice = int.Parse(txtYahooPrice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtWowmaPrice.Text))
                {
                    ime.WowmaPrice = int.Parse(txtWowmaPrice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtJishaPrice.Text))
                {
                    ime.JishaPrice = int.Parse(txtJishaPrice.Text.Replace(",", string.Empty));
                }
                //if (!string.IsNullOrWhiteSpace(txtTennisPrice.Text))
                //{
                //    ime.TennisPrice = int.Parse(txtTennisPrice.Text.Replace(",", string.Empty));
                //}

                if (!string.IsNullOrWhiteSpace(txtmonoprice.Text))
                {
                    ime.Monoprice = int.Parse(txtmonoprice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtditeprice.Text))
                {
                    ime.Diteprice = int.Parse(txtditeprice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtjapanmprice.Text))
                {
                    ime.Japanmprice = int.Parse(txtjapanmprice.Text.Replace(",", string.Empty));
                }
                if (!string.IsNullOrWhiteSpace(txtkashiwagi.Text))
                {
                    ime.Kashiwagikoukiprice = int.Parse(txtkashiwagi.Text.Replace(",", string.Empty));
                }

                ime.Rakuten_CategoryID = txtRakuten_CategoryID.Text;
                ime.Yahoo_CategoryID = txtYahoo_CategoryID.Text;
                ime.Wowma_CategoryID = txtWowma_CategoryID.Text;
                //ime.Tennis_CategoryID = txtTennis_CategoryID.Text;

                if (!string.IsNullOrWhiteSpace(ddlShipping_Flag.SelectedValue))
                {
                    ime.Shipping_Flag = int.Parse(ddlShipping_Flag.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlDelivery_Charges.SelectedValue))
                {
                    ime.Delivery_Charges = int.Parse(ddlDelivery_Charges.SelectedValue);
                }
                //if (!string.IsNullOrWhiteSpace(ddlWarehouse_Specified.SelectedValue))
                //{
                //    ime.Warehouse_Specified = int.Parse(ddlWarehouse_Specified.SelectedValue);
                //}
                //if (chkActive.Checked == true)
                //{ ime.Active = 1; }
                //else
                //{ ime.Active = 0; }
                //ime.InactiveComment = txtInactive.Text;
                //ime.Yahoo_url = txtyahoourl.Text; //for sks-593
                //if (rdb1.Checked)
                //{
                //    ime.Skucheck = 1;
                //}
                //else
                //{
                //    ime.Skucheck = 0;
                //}
                ime.SalesUnit = ddlsalesunit.SelectedItem.Text;
                //ime.TagInformation=ddlTagInfo.SelectedItem.Text;
                //if (!string.IsNullOrWhiteSpace(ddlTagInfo.SelectedValue))
                //{
                //    ime.TagInformation = ddlTagInfo.SelectedItem.Text;
                //}
                ime.ContentQuantityNo1 = txtcontentquantityunitno1.Text;
                ime.ContentQuantityNo2 = txtcontentquantityunitno2.Text;
                ime.ContentUnit1 = ddlcontentunit1.Text;
                ime.ContentUnit2 = ddlcontentunit2.Text;
                ime.PC_CatchCopy = txtCatchCopy.Text;
                //ime.PC_CatchCopy_Mobile = txtCatchCopyMobile.Text;
                ime.Maker_Name = txtmakername.Text.TrimStart();
                ime.Comment = txtcomment.Text;
                if (!String.IsNullOrWhiteSpace(txtsellingprice.Text))
                {
                    ime.Selling_Price = int.Parse(txtsellingprice.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtpurchaseprice.Text))
                {
                    ime.Purchase_Price = int.Parse(txtpurchaseprice.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtsellby.Text))
                {
                    ime.SellBy = int.Parse(txtsellby.Text.TrimStart());
                }
                ime.Selling_Rank = txtsellingrank.Text.TrimStart();
                if (!String.IsNullOrWhiteSpace(txtdeliverydays.Text))
                {
                    ime.Delivery_Days = int.Parse(txtdeliverydays.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(ddlksmavaliable.SelectedValue))
                {
                    ime.KSMDelivery_Type = int.Parse(ddlksmavaliable.SelectedValue);
                }
                if (!String.IsNullOrWhiteSpace(txtksmdeliverydays.Text))
                {
                    ime.KSMDelivery_Days = int.Parse(txtksmdeliverydays.Text.TrimStart());
                }
                ime.Nation_Wide = txtnationwide.Text;
                if (!String.IsNullOrWhiteSpace(txthokkaido.Text))
                {
                    ime.Hokkaido = int.Parse(txthokkaido.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtokinawa.Text))
                {
                    ime.Okinawa = int.Parse(txtokinawa.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtremoteisland.Text))
                {
                    ime.Remote_Island = int.Parse(txtremoteisland.Text.TrimStart());
                }
                ime.Undelivered_Area = txtundeliveredarea.Text.TrimStart();
                ime.Dangerous_Goods_Contents = txtdangerousgoodscontents.Text;
                if (!string.IsNullOrWhiteSpace(ddldeliverymethod.SelectedValue))
                {
                    ime.Delivery_Method = int.Parse(ddldeliverymethod.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldeliverytype.SelectedValue))
                {
                    ime.Delivery_Type = int.Parse(ddldeliverytype.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldeliveryfees.SelectedValue))
                {
                    ime.Delivery_Fees = int.Parse(ddldeliveryfees.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlcustomerassembly.SelectedValue))
                {
                    ime.KSM_Avaliable = int.Parse(ddlcustomerassembly.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlreturnableitem.SelectedValue))
                {
                    ime.Returnable_Item = int.Parse(ddlreturnableitem.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlnoapplicablelaw.SelectedValue))
                {
                    ime.NoApplicable_Law = int.Parse(ddlnoapplicablelaw.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlsalespermission.SelectedValue))
                {
                    ime.Sales_Permission = int.Parse(ddlsalespermission.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddllaw.SelectedValue))
                {
                    ime.Law = int.Parse(ddllaw.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldanggoodsclass.SelectedValue))
                {
                    ime.Dangoods_Class = int.Parse(ddldanggoodsclass.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldanggoodsname.SelectedValue))
                {
                    ime.Dangoods_Name = int.Parse(ddldanggoodsname.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlriskrating.SelectedValue))
                {
                    ime.Risk_Rating = int.Parse(ddlriskrating.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddldanggoodsnature.SelectedValue))
                {
                    ime.Dangoods_Nature = int.Parse(ddldanggoodsnature.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(ddlfirelaw.SelectedValue))
                {
                    ime.Fire_Law = int.Parse(ddlfirelaw.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(txtcost.Text))
                {
                    ime.Cost = int.Parse(txtcost.Text.Replace(",", string.Empty));
                }
                if (!String.IsNullOrWhiteSpace(txtday_ship.Text))
                {
                    ime.Day_Ship = int.Parse(txtday_ship.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtreturn_necessary.Text))
                {
                    ime.Retrun_Necessary = int.Parse(txtreturn_necessary.Text.TrimStart());
                }
                if (!String.IsNullOrWhiteSpace(txtwarehouse_code.Text))
                {
                    ime.Warehouse_Code = int.Parse(txtwarehouse_code.Text.TrimStart());
                }

               
                ime.PublicationType = ddlPublicationType.SelectedIndex;

                if (!String.IsNullOrWhiteSpace(txtminimumorderquantity.Text.ToString()))
                {
                    ime.MinimumOrderSuu = Convert.ToInt32(txtminimumorderquantity.Text);
                }
                ime.MinimumOrderUnit = txtminimumorderunit.Text.ToString();
                
                ime.DirectDelivery = ddlDirectDelivery.SelectedIndex;
                string schedule = Request.Form[txtreleasedatemonotaro.UniqueID];
                if (!string.IsNullOrWhiteSpace(schedule))
                {
                    ime.ScheduleReleaseDate = Convert.ToDateTime(schedule);
                }
                ime.Categorymonotaro = txtmonocategory.Text.ToString();
                ime.Colormonotaro = txtcolour.Text.ToString();
                ime.ReferenceURL = txtReferenceURL.Text.ToString();
                ime.Procurement_Goods = ddlSpecifiedprocurementitem.SelectedIndex;
                ime.EcoMarkCertifiedGoods = ddlecomartcertifiedproduct.SelectedIndex;
                ime.GreenPurchasingLaw = ddlgreenpurchasemethod.SelectedIndex;

                if (!String.IsNullOrWhiteSpace(txtecomartcertifiednumber.Text.ToString()))
                {
                    ime.EcoMarkCertifiedNo = Convert.ToInt32(txtecomartcertifiednumber.Text.ToString());
                }
                ime.RoHS_Directive = ddlRoHSdirective.SelectedIndex;
                ime.JISConform = ddlJISConform.SelectedIndex;
                ime.ISOConform = ddlISOConform.SelectedIndex;

                ime.Medical_Supplies = ddlPharmaceuticalsandmedicaldevices.SelectedIndex;               


                return ime;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
                return new Item_Master_Entity();
            }
        }

        protected string CheckLength(Item_Master_Entity ime)
        {
            try
            {
                string msg = string.Empty; int byteLength = 0;
                Encoding enc = Encoding.GetEncoding(932);
                byteLength = enc.GetByteCount(ime.Ctrl_ID);
                if (byteLength > 50)
                {
                    msg += ime.Ctrl_ID + ",";
                }
                byteLength = enc.GetByteCount(ime.Product_Code);
                if (byteLength > 100)
                {
                    msg += "製品コード" + ",";
                }
                byteLength = enc.GetByteCount(ime.Item_Name);
                if (byteLength > 255)
                {
                    msg += "商品名" + ",";
                }
                byteLength = enc.GetByteCount(ime.PC_CatchCopy);
                if (byteLength > 255)
                {
                    msg += "PC用キャッチコピー" + ",";
                }
                byteLength = enc.GetByteCount(ime.PC_CatchCopy_Mobile);
                if (byteLength > 255)
                {
                    msg += "モバイル用キャッチコピー" + ",";
                }
                byteLength = enc.GetByteCount(ime.Year);
                if (byteLength > 20)
                {
                    msg += "年度" + ",";
                }
                byteLength = enc.GetByteCount(ime.Season);
                if (byteLength > 40)
                {
                    msg += "シーズン" + ",";
                }
                byteLength = enc.GetByteCount(ime.Brand_Name);
                if (byteLength > 200)
                {
                    msg += "ブランド名" + ",";
                }
                byteLength = enc.GetByteCount(ime.Brand_Code);
                if (byteLength > 4)
                {
                    msg += "ブランドコード" + ",";
                }
                byteLength = enc.GetByteCount(ime.Competition_Name);
                if (byteLength > 200)
                {
                    msg += "競技名" + ",";
                }
                byteLength = enc.GetByteCount(ime.Class_Name);
                if (byteLength > 200)
                {
                    msg += "分類名" + ",";
                }
                byteLength = enc.GetByteCount(ime.Catalog_Information);
                if (byteLength > 3000)
                {
                    msg += "カタログ情報" + ",";
                }
                byteLength = enc.GetByteCount(ime.Rakuten_CategoryID);
                if (byteLength > 50)
                {
                    msg += "楽天 カテゴリID" + ",";
                }
                byteLength = enc.GetByteCount(ime.Yahoo_CategoryID);
                if (byteLength > 50)
                {
                    msg += "ヤフー カテゴリID" + ",";
                }
                byteLength = enc.GetByteCount(ime.Wowma_CategoryID);
                if (byteLength > 50)
                {
                    msg += "ポンパレ カテゴリID" + ",";
                }
                byteLength = enc.GetByteCount(ime.BlackMarket_Password);
                if (byteLength > 50)
                {
                    msg += "闇市パスワード" + ",";
                }
                byteLength = enc.GetByteCount(ime.DoublePrice_Ctrl_No);
                if (byteLength > 50)
                {
                    msg += "二重価格文書管理番号" + ",";
                }
                return msg;
            }
            catch (Exception ex)
            {
                string str = string.Empty;
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return str;
            }
        }

        public bool ValidationUpdate()
        {
            try
            {
                #region html field
                int length = Encoding.GetEncoding(932).GetByteCount(txtItem_Description_PC.Text);
                if (length > 5120)
                {
                    MessageBox("PC用商品説明文は5120文字までです。");
                    return false;
                }
                length = Encoding.GetEncoding(932).GetByteCount(txtSale_Description_PC.Text);
                if (length > 5120)
                {
                    MessageBox("PC用販売説明文は5120文字までです。");
                    return false;
                }
                #endregion
                if (!string.IsNullOrWhiteSpace(txtJanCD.Text))
                {
                    int jan_length = txtJanCD.Text.Length;
                    if (jan_length < 13)
                    {
                        MessageBox("Please enter 13 digits in JANCD.");
                        return false;
                    }
                }
                if (string.IsNullOrWhiteSpace(txtItem_Name.Text))
                {
                    MessageBox("Please enter Item Name.");
                    return false;
                }
                length = Encoding.GetEncoding(932).GetByteCount(txtmemo.Text);
                if (length > 1000)
                {
                    MessageBox("メモは1000文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtsiiresaki.Text);
                if (length > 100)
                {
                    MessageBox("仕入先は100文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtminimumorderunit.Text);
                if (length > 20)
                {
                    MessageBox("最低発注単位は20文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtmonocategory.Text);
                if (length > 200)
                {
                    MessageBox("カテゴリは200文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtcolour.Text);
                if (length > 40)
                {
                    MessageBox("カラーは40文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtReferenceURL.Text);
                if (length > 500)
                {
                    MessageBox("参考URLは500文字までです。");
                    return false;
                }
                //if (String.IsNullOrEmpty(txtInactive.Text) && chkActive.Checked == true)
                //{
                //    MessageBox("Write a comment for inactive! ");
                //    return false;
                //}

                return true;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                user = new UserRoleBL();
                ime = new Item_Master_Entity();
                imeBL = new Item_Master_BL();
                itemCategoryBL = new Item_Category_BL();
                Item_BL item = new Item_BL();
                DataTable templatedt = new DataTable();
                templatedt = CreateTemplateTable();
                String[] colName = { "Template" };
                if (!Check_SpecialCharacter(colName, templatedt))
                {
                    if (Session["Item_Code_Copy"] != null)
                    {
                        string itemcode = txtItem_Code.Text;
                        ime.Item_Name = txtItem_Name.Text;
                        int ItemID = imeBL.SelectItemID(itemcode);
                        if (ItemID == 0)
                        {
                            ime.ID = ItemID;
                            ime.Updated_By = UserID;
                            ime = GetItemData();
                            string str = CheckLength(ime);
                            if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                            else
                            {
                                if (ValidationUpdate())
                                {
                                    string option = null;
                                    option = "Save";
                                    if (imeBL.SaveEdit(ime, option) > 0)    //btnsave
                                    {
                                        ItemID = imeBL.SelectItemID(itemcode);
                                        ime.ID = ItemID;
                                        //Insert Category List
                                        GetCategoryValueFromTextBox(ItemID, CategoryList);
                                        //Delete previous shop from Item_Shop table and then insert new shop or not
                                        InsertShopList(ItemID, itemcode);
                                        //Delete previous photo from Item_Image table and then insert new photo or not
                                        InsertPhoto(ItemID);
                                        //Insert Item table when Itemcode is not exits in ItemTable
                                        InsertItem(itemcode);
                                        //Insert into Item_Code_URL
                                        InsertItemCodeURL(ItemID);
                                        //Change Shop Status To Gray
                                        //if (chkActive.Checked == true)
                                        //{
                                        //    imeBL.ChangeExportStatusToPink(itemcode, 1);
                                        //}
                                        //else
                                        //{
                                        //    imeBL.ChangeExportStatusToPink(itemcode, 2);
                                        //}
                                        //Delete previous related item from Item_RelatedItem table and then insert new related item or not
                                        InsertRelatedItem(ItemID);
                                        //Delete previous option from Item_Option table and then insert new option or not
                                        InsertOption(ItemID);
                                        //Delete previous yahoo specific from Item_YahooSpecificValue table and then insert new yahoo specific or not
                                        if (YahooSpecificValue != null)
                                        {
                                            InsertYahooSpecificValue(ItemID);
                                        }
                                        //For sks-587
                                        if (ViewState["DailyDelivery"] != null)
                                        {
                                            imeBL.SetUnsetDailyDelivery(itemcode, Convert.ToInt32(ViewState["DailyDelivery"]));
                                        }
                                        SaveTemplateDetail(itemcode); // Insert or Update Template_Detail
                                        //MessageBox("Save Successful ! ");
                                        //if (chkActive.Checked == true)
                                        //{
                                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(true);", true);
                                        //}
                                        //else
                                        //{
                                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(false);", true);
                                        //}
                                        SetSelectedRelatedItem(ItemID);
                                        ime = new Item_Master_Entity();
                                        ime = imeBL.SelectByID(ItemID);
                                        if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                                        {
                                            txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date).Replace('-', '/').ToString();
                                        }
                                        else
                                        {
                                            txtRelease_Date.Text = "";
                                        }
                                        if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                                        {
                                            txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date).Replace('-', '/').ToString(); ;
                                        }
                                        else
                                        {
                                            txtPost_Available_Date.Text = "";
                                        }
                                        if (!string.IsNullOrWhiteSpace(ime.ScheduleReleaseDate.ToString()))
                                        {

                                            String datemono = String.Format("{0:yyyy/MM/dd}", ime.ScheduleReleaseDate);
                                            txtreleasedatemonotaro.Text = datemono.Replace('-', '/').ToString();
                                        }
                                        else
                                        {
                                            txtreleasedatemonotaro.Text = "";
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.CostRate))
                                        {
                                            txtcostrate.Text = ime.CostRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.ProfitRate))
                                        {
                                            txtprofitrate.Text = ime.ProfitRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.DiscountRate))
                                        {
                                            txtdiscountrate.Text = ime.DiscountRate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Jisha_costrate))
                                        {
                                            txtjishaCostrate.Text = ime.Jisha_costrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Jisha_profitrate))
                                        {
                                            txtjishaProfitrate.Text = ime.Jisha_profitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Jisha_discountRate))
                                        {
                                            txtjishaDiscountrate.Text = ime.Jisha_discountRate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Rakuten_costrate))
                                        {
                                            txtrakutenCostrate.Text = ime.Rakuten_costrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Rakuten_profitrate))
                                        {
                                            txtrakutenProfitrate.Text = ime.Rakuten_profitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Rakuten_discountRate))
                                        {
                                            txtrakutenDiscountrate.Text = ime.Rakuten_discountRate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Yahoo_costrate))
                                        {
                                            txtyahooCostrate.Text = ime.Yahoo_costrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Yahoo_profitrate))
                                        {
                                            txtyahooProfitrate.Text = ime.Yahoo_profitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Yahoo_discountRate))
                                        {
                                            txtyahooDiscountrate.Text = ime.Yahoo_discountRate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Wowma_costrate))
                                        {
                                            txtwowmaCostrate.Text = ime.Wowma_costrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Wowma_profitrate))
                                        {
                                            txtwowmaProfitrate.Text = ime.Wowma_profitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Wowma_discountRate))
                                        {
                                            txtwowmaDiscountrate.Text = ime.Wowma_discountRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Monocostrate))
                                        {
                                            txtmonoprice_costrate.Text = ime.Monocostrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.MonoprofitRate))
                                        {
                                            txtmonoprice_profitrate.Text = ime.MonoprofitRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Monodiscountrate))
                                        {
                                            txtmonoprice_discountrate.Text = ime.Monodiscountrate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Ditecostrate))
                                        {
                                            txtditeprice_costrate.Text = ime.Ditecostrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.DiteprofitRate))
                                        {
                                            txtditeprice_profitrate.Text = ime.DiteprofitRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Ditediscountrate))
                                        {
                                            txtditeprice_discountrate.Text = ime.Ditediscountrate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Japanmcostrate))
                                        {
                                            txtjapanmprice_costrate.Text = ime.Japanmcostrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Japanmprofitrate))
                                        {
                                            txtjapanmprice_profitrate.Text = ime.Japanmprofitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Japanmdiscountrate))
                                        {
                                            txtjapanmprice_discountrate.Text = ime.Japanmdiscountrate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Kawashigicostrate))
                                        {
                                            txtkashiwagi_costrate.Text = ime.Kawashigicostrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Kashiwagiprofitrate))
                                        {
                                            txtkashiwagi_profitrate.Text = ime.Kashiwagiprofitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Kashiwagionodiscountrate))
                                        {
                                            txtkashiwagi_discountrate.Text = ime.Kashiwagionodiscountrate;
                                        }
                                    }
                                }
                            }
                            //Response.Redirect("Item_Master.aspx?Item_Code=" + itemcode, false);
                            Session.Remove("Item_Code_Copy");
                            ViewState["UrlReferrer"] = "ORS_Item_Master.aspx?Item_Code=" + itemcode;
                            string result = "Save Successful!";
                            if (result == "Save Successful!")
                            {
                                object referrer = ViewState["UrlReferrer"];
                                string url = (string)referrer;
                                string script = "window.onload = function(){ alert('";
                                script += result;
                                script += "');";
                                script += "window.location.href = '";
                                script += url;
                                script += "'; }";
                                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                            }
                        }
                        else
                        {
                            ime.ID = ItemID;

                            ime.Updated_By = UserID;
                            ime = GetItemData();
                            string str = CheckLength(ime);
                            if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                            else
                            {
                                if (ValidationUpdate())
                                {
                                    string option = null;
                                    option = "Update";
                                    if (imeBL.SaveEdit(ime, option) > 0)    //btnsave
                                    {
                                        //Insert Category List
                                        GetCategoryValueFromTextBox(ItemID, CategoryList);
                                        //Delete previous shop from Item_Shop table and then insert new shop or not
                                        InsertShopList(ItemID, itemcode);
                                        //Delete previous photo from Item_Image table and then insert new photo or not
                                        InsertPhoto(ItemID);
                                        //Insert Item table when Itemcode is not exits in ItemTable
                                        InsertItem(itemcode);
                                        //Insert into Item_Code_URL
                                        InsertItemCodeURL(ItemID);
                                        //Change Shop Status To Gray
                                        //if (chkActive.Checked == true)
                                        //{
                                        //    imeBL.ChangeExportStatusToPink(itemcode, 1);
                                        //}
                                        //else
                                        //{
                                        //    imeBL.ChangeExportStatusToPink(itemcode, 2);
                                        //}
                                        //Delete previous related item from Item_RelatedItem table and then insert new related item or not
                                        InsertRelatedItem(ItemID);
                                        //Delete previous option from Item_Option table and then insert new option or not
                                        InsertOption(ItemID);
                                        //Delete previous yahoo specific from Item_YahooSpecificValue table and then insert new yahoo specific or not
                                        if (YahooSpecificValue != null)
                                        {
                                            InsertYahooSpecificValue(ItemID);
                                        }
                                        //For sks-587
                                        if (ViewState["DailyDelivery"] != null)
                                        {
                                            imeBL.SetUnsetDailyDelivery(itemcode, Convert.ToInt32(ViewState["DailyDelivery"]));
                                        }
                                        SaveTemplateDetail(itemcode); // Insert or Update Template_Detail
                                        MessageBox("Updating Successful ! ");
                                        //if (chkActive.Checked == true)
                                        //{
                                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(true);", true);
                                        //}
                                        //else
                                        //{
                                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(false);", true);
                                        //}
                                        ime = new Item_Master_Entity();
                                        ime = imeBL.SelectByID(ItemID);
                                        if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                                        {
                                            txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date).Replace('-', '/').ToString(); ;
                                        }
                                        else
                                        {
                                            txtRelease_Date.Text = "";
                                        }
                                        if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                                        {
                                            txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date).Replace('-', '/').ToString(); ;
                                        }
                                        else
                                        {
                                            txtPost_Available_Date.Text = "";
                                        }

                                        if (!string.IsNullOrWhiteSpace(ime.ScheduleReleaseDate.ToString()))
                                        {

                                            String datemono = String.Format("{0:yyyy/MM/dd}", ime.ScheduleReleaseDate);
                                            txtreleasedatemonotaro.Text = datemono.Replace('-', '/').ToString();
                                        }
                                        else
                                        {
                                            txtreleasedatemonotaro.Text = "";
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.CostRate))
                                        {
                                            txtcostrate.Text = ime.CostRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.ProfitRate))
                                        {
                                            txtprofitrate.Text = ime.ProfitRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.DiscountRate))
                                        {
                                            txtdiscountrate.Text = ime.DiscountRate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Jisha_costrate))
                                        {
                                            txtjishaCostrate.Text = ime.Jisha_costrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Jisha_profitrate))
                                        {
                                            txtjishaProfitrate.Text = ime.Jisha_profitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Jisha_discountRate))
                                        {
                                            txtjishaDiscountrate.Text = ime.Jisha_discountRate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Rakuten_costrate))
                                        {
                                            txtrakutenCostrate.Text = ime.Rakuten_costrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Rakuten_profitrate))
                                        {
                                            txtrakutenProfitrate.Text = ime.Rakuten_profitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Rakuten_discountRate))
                                        {
                                            txtrakutenDiscountrate.Text = ime.Rakuten_discountRate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Yahoo_costrate))
                                        {
                                            txtyahooCostrate.Text = ime.Yahoo_costrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Yahoo_profitrate))
                                        {
                                            txtyahooProfitrate.Text = ime.Yahoo_profitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Yahoo_discountRate))
                                        {
                                            txtyahooDiscountrate.Text = ime.Yahoo_discountRate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Wowma_costrate))
                                        {
                                            txtwowmaCostrate.Text = ime.Wowma_costrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Wowma_profitrate))
                                        {
                                            txtwowmaProfitrate.Text = ime.Wowma_profitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Wowma_discountRate))
                                        {
                                            txtwowmaDiscountrate.Text = ime.Wowma_discountRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Monocostrate))
                                        {
                                            txtmonoprice_costrate.Text = ime.Monocostrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.MonoprofitRate))
                                        {
                                            txtmonoprice_profitrate.Text = ime.MonoprofitRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Monodiscountrate))
                                        {
                                            txtmonoprice_discountrate.Text = ime.Monodiscountrate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Ditecostrate))
                                        {
                                            txtditeprice_costrate.Text = ime.Ditecostrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.DiteprofitRate))
                                        {
                                            txtditeprice_profitrate.Text = ime.DiteprofitRate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Ditediscountrate))
                                        {
                                            txtditeprice_discountrate.Text = ime.Ditediscountrate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Japanmcostrate))
                                        {
                                            txtjapanmprice_costrate.Text = ime.Japanmcostrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Japanmprofitrate))
                                        {
                                            txtjapanmprice_profitrate.Text = ime.Japanmprofitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Japanmdiscountrate))
                                        {
                                            txtjapanmprice_discountrate.Text = ime.Japanmdiscountrate;
                                        }

                                        if (!String.IsNullOrWhiteSpace(ime.Kawashigicostrate))
                                        {
                                            txtkashiwagi_costrate.Text = ime.Kawashigicostrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Kashiwagiprofitrate))
                                        {
                                            txtkashiwagi_profitrate.Text = ime.Kashiwagiprofitrate;
                                        }
                                        if (!String.IsNullOrWhiteSpace(ime.Kashiwagionodiscountrate))
                                        {
                                            txtkashiwagi_discountrate.Text = ime.Kashiwagionodiscountrate;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (ItemCode != null)
                    {
                        int ItemID = imeBL.SelectItemID(ItemCode);
                        ime.ID = ItemID;
                        ime.Updated_By = UserID;
                        ime = GetItemData();
                        string str = CheckLength(ime);
                        if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                        else
                        {
                            if (ValidationUpdate())
                            {
                                string option = null;
                                if (Request.QueryString["Item_Code"] != null)
                                {
                                    option = "Update";
                                }
                                if (imeBL.SaveEdit(ime, option) > 0)    //btnsave
                                {
                                    ItemID = imeBL.SelectItemID(ItemCode);
                                    //Insert Category List
                                    GetCategoryValueFromTextBox(ItemID, CategoryList);
                                    //Delete previous shop from Item_Shop table and then insert new shop or not
                                    InsertShopList(ItemID, ItemCode);
                                    //Delete previous photo from Item_Image table and then insert new photo or not
                                    InsertPhoto(ItemID);
                                    //Insert Item table when Itemcode is not exits in ItemTable
                                    InsertItem(ItemCode);
                                    //Insert into Item_Code_URL
                                    InsertItemCodeURL(ItemID);
                                    //Change Shop Status To Gray
                                    //if (chkActive.Checked == true)
                                    //{
                                    //    imeBL.ChangeExportStatusToPink(ItemCode, 1);
                                    //}
                                    //else
                                    //{
                                    //    imeBL.ChangeExportStatusToPink(ItemCode, 2);
                                    //}
                                    //Delete previous related item from Item_RelatedItem table and then insert new related item or not
                                    InsertRelatedItem(ItemID);
                                    //Delete previous option from Item_Option table and then insert new option or not
                                    InsertOption(ItemID);
                                    //Delete previous yahoo specific from Item_YahooSpecificValue table and then insert new yahoo specific or not
                                    if (YahooSpecificValue != null)
                                    {
                                        InsertYahooSpecificValue(ItemID);
                                    }
                                    //For sks-587
                                    if (ViewState["DailyDelivery"] != null)
                                    {
                                        imeBL.SetUnsetDailyDelivery(ItemCode, Convert.ToInt32(ViewState["DailyDelivery"]));
                                    }
                                    SaveTemplateDetail(ItemCode); // Insert or Update Template_Detail
                                    MessageBox("Updating Successful ! ");
                                    //if (chkActive.Checked == true)
                                    //{
                                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(true);", true);
                                    //}
                                    //else
                                    //{
                                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(false);", true);
                                    //}
                                    ime = new Item_Master_Entity();
                                    ime = imeBL.SelectByID(ItemID);
                                    if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                                    {
                                        txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date).Replace('-', '/').ToString(); ;
                                    }
                                    else
                                    {
                                        txtRelease_Date.Text = "";
                                    }
                                    if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                                    {
                                        txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date).Replace('-', '/').ToString(); ;
                                    }
                                    else
                                    {
                                        txtPost_Available_Date.Text = "";
                                    }

                                    if (!string.IsNullOrWhiteSpace(ime.ScheduleReleaseDate.ToString()))
                                    {

                                        String datemono = String.Format("{0:yyyy/MM/dd}", ime.ScheduleReleaseDate);
                                        txtreleasedatemonotaro.Text = datemono.Replace('-', '/').ToString();
                                    }
                                    else
                                    {
                                        txtreleasedatemonotaro.Text = "";
                                    }

                                    if (!String.IsNullOrWhiteSpace(ime.CostRate))
                                    {
                                        txtcostrate.Text = ime.CostRate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.ProfitRate))
                                    {
                                        txtprofitrate.Text = ime.ProfitRate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.DiscountRate))
                                    {
                                        txtdiscountrate.Text = ime.DiscountRate;
                                    }

                                    if (!String.IsNullOrWhiteSpace(ime.Jisha_costrate))
                                    {
                                        txtjishaCostrate.Text = ime.Jisha_costrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Jisha_profitrate))
                                    {
                                        txtjishaProfitrate.Text = ime.Jisha_profitrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Jisha_discountRate))
                                    {
                                        txtjishaDiscountrate.Text = ime.Jisha_discountRate;
                                    }

                                    if (!String.IsNullOrWhiteSpace(ime.Rakuten_costrate))
                                    {
                                        txtrakutenCostrate.Text = ime.Rakuten_costrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Rakuten_profitrate))
                                    {
                                        txtrakutenProfitrate.Text = ime.Rakuten_profitrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Rakuten_discountRate))
                                    {
                                        txtrakutenDiscountrate.Text = ime.Rakuten_discountRate;
                                    }

                                    if (!String.IsNullOrWhiteSpace(ime.Yahoo_costrate))
                                    {
                                        txtyahooCostrate.Text = ime.Yahoo_costrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Yahoo_profitrate))
                                    {
                                        txtyahooProfitrate.Text = ime.Yahoo_profitrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Yahoo_discountRate))
                                    {
                                        txtyahooDiscountrate.Text = ime.Yahoo_discountRate;
                                    }

                                    if (!String.IsNullOrWhiteSpace(ime.Wowma_costrate))
                                    {
                                        txtwowmaCostrate.Text = ime.Wowma_costrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Wowma_profitrate))
                                    {
                                        txtwowmaProfitrate.Text = ime.Wowma_profitrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Wowma_discountRate))
                                    {
                                        txtwowmaDiscountrate.Text = ime.Wowma_discountRate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Monocostrate))
                                    {
                                        txtmonoprice_costrate.Text = ime.Monocostrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.MonoprofitRate))
                                    {
                                        txtmonoprice_profitrate.Text = ime.MonoprofitRate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Monodiscountrate))
                                    {
                                        txtmonoprice_discountrate.Text = ime.Monodiscountrate;
                                    }

                                    if (!String.IsNullOrWhiteSpace(ime.Ditecostrate))
                                    {
                                        txtditeprice_costrate.Text = ime.Ditecostrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.DiteprofitRate))
                                    {
                                        txtditeprice_profitrate.Text = ime.DiteprofitRate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Ditediscountrate))
                                    {
                                        txtditeprice_discountrate.Text = ime.Ditediscountrate;
                                    }

                                    if (!String.IsNullOrWhiteSpace(ime.Japanmcostrate))
                                    {
                                        txtjapanmprice_costrate.Text = ime.Japanmcostrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Japanmprofitrate))
                                    {
                                        txtjapanmprice_profitrate.Text = ime.Japanmprofitrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Japanmdiscountrate))
                                    {
                                        txtjapanmprice_discountrate.Text = ime.Japanmdiscountrate;
                                    }

                                    if (!String.IsNullOrWhiteSpace(ime.Kawashigicostrate))
                                    {
                                        txtkashiwagi_costrate.Text = ime.Kawashigicostrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Kashiwagiprofitrate))
                                    {
                                        txtkashiwagi_profitrate.Text = ime.Kashiwagiprofitrate;
                                    }
                                    if (!String.IsNullOrWhiteSpace(ime.Kashiwagionodiscountrate))
                                    {
                                        txtkashiwagi_discountrate.Text = ime.Kashiwagionodiscountrate;
                                    }
                                }
                            }
                        }
                    }
                }
                SetImagenull();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

     
     

        protected void ddlOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Option_BL opBL = new Option_BL();
                string optionGroupName = ddlOption.SelectedItem.Text;
                DataTable dtOption = opBL.SelectOptionByOption_GroupName(optionGroupName);
                if (dtOption != null && dtOption.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name1", typeof(string));
                    dt.Columns.Add("Value1", typeof(string));
                    dt.Columns.Add("Name2", typeof(string));
                    dt.Columns.Add("Value2", typeof(string));
                    dt.Columns.Add("Name3", typeof(string));
                    dt.Columns.Add("Value3", typeof(string));
                    if (dtOption.Rows.Count > 2)
                    {
                        dt.Rows.Add(dtOption.Rows[0]["Option_Name"].ToString(), dtOption.Rows[0]["Option_Value"].ToString(),
                                              dtOption.Rows[1]["Option_Name"].ToString(), dtOption.Rows[1]["Option_Value"].ToString(),
                                              dtOption.Rows[2]["Option_Name"].ToString(), dtOption.Rows[2]["Option_Value"].ToString());
                    }
                    else if (dtOption.Rows.Count > 1)
                    {
                        dt.Rows.Add(dtOption.Rows[0]["Option_Name"].ToString(), dtOption.Rows[0]["Option_Value"].ToString(), dtOption.Rows[1]["Option_Name"].ToString(), dtOption.Rows[1]["Option_Value"].ToString(), "", "");
                    }
                    else if (dtOption.Rows.Count > 0)
                    {
                        dt.Rows.Add(dtOption.Rows[0]["Option_Name"].ToString(), dtOption.Rows[0]["Option_Value"].ToString(), "", "", "", "");
                    }

                    SetOption(dt);
                    Session["Option_" + ItemCode] = dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void InsertRelatedItem(int itemID)
        {
            try
            {
                DataTable dtRelated = new DataTable();
                dtRelated.Columns.Add(new DataColumn("Item_ID", typeof(int)));
                dtRelated.Columns.Add(new DataColumn("Related_ItemCode", typeof(string)));
                dtRelated.Columns.Add(new DataColumn("SN", typeof(int)));
                if (!string.IsNullOrWhiteSpace(txtRelated1.Text))
                {
                    DataRow dr1 = dtRelated.NewRow();
                    dr1["Item_ID"] = itemID;
                    dr1["Related_ItemCode"] = txtRelated1.Text.TrimStart();
                    dr1["SN"] = 1;
                    dtRelated.Rows.Add(dr1);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated2.Text))
                {
                    DataRow dr2 = dtRelated.NewRow();
                    dr2["Item_ID"] = itemID;
                    dr2["Related_ItemCode"] = txtRelated2.Text.TrimStart();
                    dr2["SN"] = 2;
                    dtRelated.Rows.Add(dr2);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated3.Text))
                {
                    DataRow dr3 = dtRelated.NewRow();
                    dr3["Item_ID"] = itemID;
                    dr3["Related_ItemCode"] = txtRelated3.Text.TrimStart();
                    dr3["SN"] = 3;
                    dtRelated.Rows.Add(dr3);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated4.Text))
                {
                    DataRow dr4 = dtRelated.NewRow();
                    dr4["Item_ID"] = itemID;
                    dr4["Related_ItemCode"] = txtRelated4.Text.TrimStart();
                    dr4["SN"] = 4;
                    dtRelated.Rows.Add(dr4);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated5.Text))
                {
                    DataRow dr5 = dtRelated.NewRow();
                    dr5["Item_ID"] = itemID;
                    dr5["Related_ItemCode"] = txtRelated5.Text.TrimStart();
                    dr5["SN"] = 5;
                    dtRelated.Rows.Add(dr5);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated6.Text))
                {
                    DataRow dr6 = dtRelated.NewRow();
                    dr6["Item_ID"] = itemID;
                    dr6["Related_ItemCode"] = txtRelated6.Text.TrimStart();
                    dr6["SN"] = 6;
                    dtRelated.Rows.Add(dr6);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated7.Text))
                {
                    DataRow dr7 = dtRelated.NewRow();
                    dr7["Item_ID"] = itemID;
                    dr7["Related_ItemCode"] = txtRelated7.Text.TrimStart();
                    dr7["SN"] = 7;
                    dtRelated.Rows.Add(dr7);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated8.Text))
                {
                    DataRow dr8 = dtRelated.NewRow();
                    dr8["Item_ID"] = itemID;
                    dr8["Related_ItemCode"] = txtRelated8.Text.TrimStart();
                    dr8["SN"] = 8;
                    dtRelated.Rows.Add(dr8);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated9.Text))
                {
                    DataRow dr9 = dtRelated.NewRow();
                    dr9["Item_ID"] = itemID;
                    dr9["Related_ItemCode"] = txtRelated9.Text.TrimStart();
                    dr9["SN"] = 9;
                    dtRelated.Rows.Add(dr9);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated10.Text))
                {
                    DataRow dr10 = dtRelated.NewRow();
                    dr10["Item_ID"] = itemID;
                    dr10["Related_ItemCode"] = txtRelated10.Text.TrimStart();
                    dr10["SN"] = 10;
                    dtRelated.Rows.Add(dr10);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated11.Text))
                {
                    DataRow dr11 = dtRelated.NewRow();
                    dr11["Item_ID"] = itemID;
                    dr11["Related_ItemCode"] = txtRelated11.Text.TrimStart();
                    dr11["SN"] = 11;
                    dtRelated.Rows.Add(dr11);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated12.Text))
                {
                    DataRow dr12 = dtRelated.NewRow();
                    dr12["Item_ID"] = itemID;
                    dr12["Related_ItemCode"] = txtRelated12.Text.TrimStart();
                    dr12["SN"] = 12;
                    dtRelated.Rows.Add(dr12);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated13.Text))
                {
                    DataRow dr13 = dtRelated.NewRow();
                    dr13["Item_ID"] = itemID;
                    dr13["Related_ItemCode"] = txtRelated13.Text.TrimStart();
                    dr13["SN"] = 13;
                    dtRelated.Rows.Add(dr13);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated14.Text))
                {
                    DataRow dr14 = dtRelated.NewRow();
                    dr14["Item_ID"] = itemID;
                    dr14["Related_ItemCode"] = txtRelated14.Text.TrimStart();
                    dr14["SN"] = 14;
                    dtRelated.Rows.Add(dr14);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated15.Text))
                {
                    DataRow dr15 = dtRelated.NewRow();
                    dr15["Item_ID"] = itemID;
                    dr15["Related_ItemCode"] = txtRelated15.Text.TrimStart();
                    dr15["SN"] = 15;
                    dtRelated.Rows.Add(dr15);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated16.Text))
                {
                    DataRow dr16 = dtRelated.NewRow();
                    dr16["Item_ID"] = itemID;
                    dr16["Related_ItemCode"] = txtRelated16.Text.TrimStart();
                    dr16["SN"] = 16;
                    dtRelated.Rows.Add(dr16);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated17.Text))
                {
                    DataRow dr17 = dtRelated.NewRow();
                    dr17["Item_ID"] = itemID;
                    dr17["Related_ItemCode"] = txtRelated17.Text.TrimStart();
                    dr17["SN"] = 17;
                    dtRelated.Rows.Add(dr17);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated18.Text))
                {
                    DataRow dr18 = dtRelated.NewRow();
                    dr18["Item_ID"] = itemID;
                    dr18["Related_ItemCode"] = txtRelated18.Text.TrimStart();
                    dr18["SN"] = 18;
                    dtRelated.Rows.Add(dr18);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated19.Text))
                {
                    DataRow dr19 = dtRelated.NewRow();
                    dr19["Item_ID"] = itemID;
                    dr19["Related_ItemCode"] = txtRelated19.Text.TrimStart();
                    dr19["SN"] = 19;
                    dtRelated.Rows.Add(dr19);
                }
                if (!string.IsNullOrWhiteSpace(txtRelated20.Text))
                {
                    DataRow dr20 = dtRelated.NewRow();
                    dr20["Item_ID"] = itemID;
                    dr20["Related_ItemCode"] = txtRelated20.Text.TrimStart();
                    dr20["SN"] = 20;
                    dtRelated.Rows.Add(dr20);
                }
                Item_Related_Item_BL ItemRelatedBL = new Item_Related_Item_BL();
                ItemRelatedBL.Insert(itemID, dtRelated);
                //Session["Related_Item_Code" + ItemCode] = dtRelated;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }



        public void InsertItemCodeURL(int ItemID)
        {
            Item_Shop_BL isbl = new Item_Shop_BL();
            DataTable dt = new DataTable();
            dt.Columns.Add("Item_ID", typeof(int));
            dt.Columns.Add("Shop_ID", typeof(int));
            dt.Columns.Add("Item_Code_URL", typeof(string));

            //foreach (DataListItem li in dlShop.Items)
            //{
            //    //TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
            //    Label shopid = li.FindControl("lblShopID") as Label;
            //    CheckBox cb = li.FindControl("ckbShop") as CheckBox;
            //    if (cb != null)
            //    {
            //        if (cb.Checked)
            //        {
            //            DataRow dr = dt.NewRow();
            //            dr["Item_ID"] = ItemID;
            //            dr["Item_Code_URL"] = txtItem_Code.Text;
            //            dr["Shop_ID"] = Convert.ToInt32(shopid.Text);
            //            dt.Rows.Add(dr);
            //        }
            //    }
            //}
            isbl.InsertItemCodeURL(dt, ItemID);
        }

        public void InsertItem(string ItemCode)
        {
            imeBL = new Item_Master_BL();
            imeBL.InsertItemInventory(ItemCode);
        }

        public void InsertShopList(int itemID, string itemcode)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemID", typeof(int));
                dt.Columns.Add("ShopID", typeof(int));
                dt.Columns.Add("ItemCode", typeof(string));
                foreach (DataListItem li in dlShop1.Items)
                {
                    Label lbl = li.FindControl("lblShopID") as Label;
                    CheckBox cb = li.FindControl("ckbShopName") as CheckBox;
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ItemID"] = itemID;
                            dr["ShopID"] = Convert.ToInt32(lbl.Text);
                            dr["ItemCode"] = itemcode;
                            dt.Rows.Add(dr);
                        }
                    }
                }
               
                Item_Shop_BL itemShopBL = new Item_Shop_BL();
                int flg = flag;
                int realflag;
                if (ViewState["flag"] == null)
                {
                    realflag = 2;
                }
                else
                {
                    realflag = Convert.ToInt32(ViewState["flag"]);
                }
                if (realflag != 2)
                {
                    itemShopBL.Check_ItemShopForAmazon(itemID, realflag, UserID);
                }
                itemShopBL.Insert(dt, itemID);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void GetCategoryValueFromTextBox(int ItemID, DataTable CategoryList)
        {
            itemCategoryBL = new Item_Category_BL();
            CreatenewDataTable();
            string cat = null;
            DataTable dtnew = (DataTable)ViewState["DataTablenew"];
            DataRow dr = null;
            //foreach (GridViewRow gvrow in gvCategory.Rows)
            //{
            //    TextBox box1 = gvrow.FindControl("txtCategory") as TextBox;
            //    TextBox box2 = gvrow.FindControl("txtSN") as TextBox;
            //    dr = dtnew.NewRow();
            //    dr["Category"] = box1.Text;
            //    dr["SN"] = box2.Text;
            //    if ((!String.IsNullOrWhiteSpace(box1.Text)) && (!String.IsNullOrWhiteSpace(box2.Text)))
            //    {
            //        dtnew.Rows.Add(dr);
            //    }
            //}
            if (dtnew != null && dtnew.Rows.Count > 0)
            {
                cat = dtnew.Rows[0]["Category"].ToString();
            }
            if (!String.IsNullOrWhiteSpace(cat))
            {
                DataTable dt = itemCategoryBL.CheckCategory(ItemID, dtnew);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<DataRow> rowsToDelete = new List<DataRow>();
                    foreach (DataRow drnew in dtnew.Rows)
                    {
                        if (drnew["Category"].ToString() == dt.Rows[0]["CName"].ToString())
                            rowsToDelete.Add(drnew);
                    }
                    foreach (var r in rowsToDelete)
                    {
                        dtnew.Rows.Remove(r);
                    }
                    dt.Merge(CategoryList);
                }
                if (dtnew != null && dtnew.Rows.Count > 0)
                {
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        string str = dtnew.Rows[i]["Category"].ToString();
                        string catsn = dtnew.Rows[i]["SN"].ToString();
                        int sn = Convert.ToInt32(catsn);
                        string path = null;
                        string[] strsplit = null;
                        if (str.Contains('\\'))
                        {
                            strsplit = str.Split('\\');
                        }
                        if (str.Contains('￥'))
                        {
                            strsplit = str.Split('￥');
                        }
                        if (strsplit != null)
                        {
                            for (int j = 0; j < strsplit.Count() - 1; j++)
                            {
                                string check = strsplit[j];
                                path += strsplit[j] + "\\";
                                if (j == 0)
                                {
                                    int catid = itemCategoryBL.CheckDescription(check, sn, 0, 0, path);
                                    hdfCatID.Value = catid.ToString();
                                }
                                else
                                {
                                    int catno = Convert.ToInt32(hdfCatID.Value);
                                    int catid = itemCategoryBL.CheckDescription(check, sn, 1, catno, path);
                                    hdfCatID.Value = catid.ToString();
                                }
                            }
                            hdfCatID.Value = "";
                        }
                        DataTable dtcopy = itemCategoryBL.CheckCategory(ItemID, dtnew);
                        dt.Merge(dtcopy);
                    }
                    itemCategoryBL.Insert(ItemID, dt);
                    Session.Remove("CategoryList_" + ItemCode);
                }
            }
            else
            {
                if (CategoryList != null)
                {
                    itemCategoryBL.Insert(ItemID, CategoryList);
                    Session.Remove("CategoryList_" + ItemCode);
                }
            }
        }

        public void CreatenewDataTable()
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtnew.Columns.Add(new DataColumn("Category", typeof(string)));
            dtnew.Columns.Add(new DataColumn("SN", typeof(string)));
            ViewState["DataTablenew"] = dtnew;
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                imeBL = new Item_Master_BL();
                user = new UserRoleBL();
                ime = new Item_Master_Entity();
                itemCategoryBL = new Item_Category_BL();
                DataTable templatedt = new DataTable();
                templatedt = CreateTemplateTable();
                String[] colName = { "Template" };
                if (!String.IsNullOrEmpty(txtSale_Price.Text))
                {
                    btnComplete.Enabled = true;
                }
                if (!Check_SpecialCharacter(colName, templatedt))
                {
                    Item_BL item = new Item_BL();
                    if (ItemCode != null)
                    {
                        int ItemID = imeBL.SelectItemID(ItemCode);
                        ime.ID = ItemID;
                        DataTable dtshop = CheckConditon(ItemID, ItemCode);
                        //DataTable dtImage = ImageList as DataTable;
                        DataTable dtImage = GetImageList(ItemID);
                        string errMsg = CheckCategoryID(dtshop, dtImage);// check sku and Mall category
                        if (!String.IsNullOrWhiteSpace(errMsg))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errMsg + "')", true);
                        }
                        else
                        {
                            ime.Updated_By = UserID;
                            ime = GetItemData();
                            string str = CheckLength(ime);
                            if (!String.IsNullOrWhiteSpace(str)) { GlobalUI.MessageBox(str + "greater than length bytes!"); }
                            else
                            {
                                if (ValidationComplete())
                                {
                                    string option = null;
                                    if (Request.QueryString["Item_Code"] != null)
                                    {
                                        option = "Edit";
                                    }
                                    if (imeBL.SaveEdit(ime, option) > 0)
                                    {
                                        ItemID = imeBL.SelectItemID(ItemCode);
                                        //1.Change Ctrl_ID=d from Item_Category table for Previous Category List
                                        //2.Insert Ctrl_ID=n from Item_Category table for New Category List
                                        //Insert Category List
                                        GetCategoryValueFromTextBox(ItemID, CategoryList);
                                        //Delete previous shop from Item_Shop table and then insert new shop or not
                                        InsertShopList(ItemID, ItemCode);
                                        //Delete previous photo from Item_Image table and then insert new photo or not
                                        InsertPhoto(ItemID);
                                        //Insert into Item_Code_URL
                                        InsertItemCodeURL(ItemID);
                                        //Change Shop Status To Gray
                                        //if (chkActive.Checked == true)
                                        //{
                                        //    imeBL.ChangeExportStatusToPink(ItemCode, 1);
                                        //}
                                        //else
                                        //{
                                        //    imeBL.ChangeExportStatusToPink(ItemCode, 2);
                                        //}
                                        //Delete previous related item from Item_RelatedItem table and then insert new related item or not
                                        InsertRelatedItem(ItemID);
                                        //Delete previous option from Item_Option table and then insert new option or not
                                        InsertOption(ItemID);
                                        //Delete previous yahoo specific from Item_YahooSpecificValue table and then insert new yahoo specific or not
                                        if (YahooSpecificValue != null)
                                        {
                                            InsertYahooSpecificValue(ItemID);
                                        }
                                        //for sks-587
                                        if (ViewState["DailyDelivery"] != null)
                                        {
                                            imeBL.SetUnsetDailyDelivery(ItemCode, Convert.ToInt32(ViewState["DailyDelivery"]));
                                        }
                                        SaveTemplateDetail(ItemCode); // Insert or Update Template_Detail
                                    }
                                }
                                MessageBox("Data Complete ! ");
                                //if (chkActive.Checked == true)
                                //{
                                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(true);", true);
                                //}
                                //else
                                //{
                                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "OnCheckedChanged(false);", true);
                                //}
                                SetSelectedRelatedItem(ItemID);
                                ime = new Item_Master_Entity();
                                ime = imeBL.SelectByID(ItemID);
                                if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                                {
                                    txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date).Replace('-', '/').ToString(); ;
                                }
                                else
                                {
                                    txtRelease_Date.Text = "";
                                }
                                if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                                {
                                    txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date).Replace('-', '/').ToString(); ;
                                }
                                else
                                {
                                    txtPost_Available_Date.Text = "";
                                }

                                if (!string.IsNullOrWhiteSpace(ime.ScheduleReleaseDate.ToString()))
                                {

                                    String datemono = String.Format("{0:yyyy/MM/dd}", ime.ScheduleReleaseDate);
                                    txtreleasedatemonotaro.Text = datemono.Replace('-', '/').ToString();
                                }
                                else
                                {
                                    txtreleasedatemonotaro.Text = "";
                                }
                            }
                        }
                    }
                }
                SetImagenull();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public DataTable CheckConditon(int itemID, string itemcode)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("ItemID", typeof(int));
                dt.Columns.Add("ShopID", typeof(int));
                dt.Columns.Add("ItemCode", typeof(string));

                foreach (DataListItem li in dlShop1.Items)
                {
                    Label lbl = li.FindControl("lblShopID") as Label;
                    CheckBox cb = li.FindControl("ckbShopName") as CheckBox;
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ItemID"] = itemID;
                            dr["ShopID"] = Convert.ToInt32(lbl.Text);
                            dr["ItemCode"] = itemcode;
                            dt.Rows.Add(dr);
                        }
                    }
                }                       
          
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String CheckCategoryID(DataTable dtshop, DataTable dtImage)
        {
            String errorMsg = string.Empty;
            DataRow[] rowRakuten = dtshop.Select("ShopID=1 OR ShopID=5 OR ShopID=8 OR ShopID=12 ");
            if (rowRakuten.Count() > 0 && txtRakuten_CategoryID.Text == "")
            {
                errorMsg += "楽天ディレクトリIDを設定してください。, ";
            }
            DataRow[] rowYahoo = dtshop.Select("ShopID=2 OR ShopID=6 OR ShopID=9 OR ShopID=13  OR ShopID=17");
            if (rowYahoo.Count() > 0 && txtYahoo_CategoryID.Text == "")
            {
                errorMsg += "YahooスペックIDを設定してください。, ";
            }
            DataRow[] rowWomma = dtshop.Select("ShopID = 4");
            if (rowWomma.Count() > 0 && txtWowma_CategoryID.Text == "")
            {
                errorMsg += "WommaカテゴリIDを設定してください。, ";
            }
            DataRow[] rowTennis = dtshop.Select("ShopID = 6");
            //if (rowTennis.Count() > 0 && txtTennis_CategoryID.Text == "")
            //{
            //    errorMsg += "ORS自社カテゴリIDを設定してください。, ";
            //}
            if (dtImage != null && dtImage.Rows.Count > 0)
            {
                DataRow[] rowImage = dtImage.Select("Image_Type='0'");
                if (rowImage.Count() == 0)
                {
                    errorMsg += "Image is Empty  ";
                    btnComplete.Visible = true;
                }
            }
            else
            {
                errorMsg += "Image is Empty  ";
                btnComplete.Visible = true;
            }
            return errorMsg;
        }

        public bool ValidationComplete()
        {
            try
            {
                #region html field
                int length = Encoding.GetEncoding(932).GetByteCount(txtItem_Description_PC.Text);
                if (length > 5120)
                {
                    MessageBox("PC用商品説明文は5120文字までです。");
                    return false;
                }
                length = Encoding.GetEncoding(932).GetByteCount(txtSale_Description_PC.Text);
                if (length > 5120)
                {
                    MessageBox("PC用販売説明文は5120文字までです。");
                    return false;
                }
                #endregion
                if (!string.IsNullOrWhiteSpace(txtJanCD.Text))
                {
                    int jan_length = txtJanCD.Text.Length;
                    if (jan_length < 13)
                    {
                        MessageBox("Please enter 13 digits in JANCD");
                        return false;
                    }
                }
                if (string.IsNullOrWhiteSpace(txtItem_Name.Text))
                {
                    MessageBox("Please enter Item Name.");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtSale_Price.Text))
                {
                    MessageBox("Please enter Sale Price.");
                    return false;
                }
                else if (txtSale_Price.Text == "0")
                {
                    MessageBox("Please enter Sale Price.");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtList_Price.Text))
                {
                    MessageBox("Please enter List Price.");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtcost.Text))
                {
                    MessageBox("Please enter Cost.");
                    return false;
                }
                length = Encoding.GetEncoding(932).GetByteCount(txtmemo.Text);
                if (length > 1000)
                {
                    MessageBox("メモは1000文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtsiiresaki.Text);
                if (length > 100)
                {
                    MessageBox("仕入先は100文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtminimumorderunit.Text);
                if (length > 20)
                {
                    MessageBox("最低発注単位は20文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtmonocategory.Text);
                if (length > 200)
                {
                    MessageBox("カテゴリは200文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtcolour.Text);
                if (length > 40)
                {
                    MessageBox("カラーは40文字までです。");
                    return false;
                }

                length = Encoding.GetEncoding(932).GetByteCount(txtReferenceURL.Text);
                if (length > 500)
                {
                    MessageBox("参考URLは500文字までです。");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return false;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "はい")
                {
                    imeBL = new Item_Master_BL();
                    imeBL.DeleteItem(ItemCode);
                    MessageBox("Delete Successful ! ");
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void MessageBox(string message)
        {
            try
            {
                if (message == "Saving Successful ! " || message == "Updating Successful ! " || message == "Data Complete ! ")
                {
                    string script = "<script type=\"text/javascript\">alert('" + message + "');</script>";
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                    }
                }
                else if (message == "Delete Successful ! ")
                {
                    object referrer = ViewState["UrlReferrer"];
                    string url = (string)referrer;
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "');";
                    script += "window.location = '";
                    script += url;
                    script += "'; }";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
                else
                {
                    string cleanMessage = message.Replace("'", "\\'");
                    string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        protected void ChangeNUll_To_Space()
        {
            //Image1.ImageUrl = imagePath + "no_image.jpg";
            //hlImage1.NavigateUrl = imagePath + "no_image.jpg";

            //Image2.ImageUrl = imagePath + "no_image.jpg";
            //hlImage2.NavigateUrl = imagePath + "no_image.jpg";

            //Image3.ImageUrl = imagePath + "no_image.jpg";
            //hlImage3.NavigateUrl = imagePath + "no_image.jpg";

            //Image4.ImageUrl = imagePath + "no_image.jpg";
            //hlImage4.NavigateUrl = imagePath + "no_image.jpg";

            //Image5.ImageUrl = imagePath + "no_image.jpg";
            //hlImage5.NavigateUrl = imagePath + "no_image.jpg";

            //Image6.ImageUrl = imagePath + "no_image.jpg";
            //hlImage6.NavigateUrl = imagePath + "no_image.jpg";

            //Image7.ImageUrl = imagePath + "no_image.jpg";
            //hlImage7.NavigateUrl = imagePath + "no_image.jpg";

            //Image8.ImageUrl = imagePath + "no_image.jpg";
            //hlImage8.NavigateUrl = imagePath + "no_image.jpg";

            //Image9.ImageUrl = imagePath + "no_image.jpg";
            //hlImage9.NavigateUrl = imagePath + "no_image.jpg";

            //Image10.ImageUrl = imagePath + "no_image.jpg";
            //hlImage10.NavigateUrl = imagePath + "no_image.jpg";

            //Image11.ImageUrl = imagePath + "no_image.jpg";
            //hlImage11.NavigateUrl = imagePath + "no_image.jpg";

            //Image12.ImageUrl = imagePath + "no_image.jpg";
            //hlImage12.NavigateUrl = imagePath + "no_image.jpg";

            //Image13.ImageUrl = imagePath + "no_image.jpg";
            //hlImage13.NavigateUrl = imagePath + "no_image.jpg";

            //Image14.ImageUrl = imagePath + "no_image.jpg";
            //hlImage14.NavigateUrl = imagePath + "no_image.jpg";

            //Image15.ImageUrl = imagePath + "no_image.jpg";
            //hlImage15.NavigateUrl = imagePath + "no_image.jpg";

            //Image17.ImageUrl = imagePath + "no_image.jpg";
            //hlImage17.NavigateUrl = imagePath + "no_image.jpg";

            //Image18.ImageUrl = imagePath + "no_image.jpg";
            //hlImage18.NavigateUrl = imagePath + "no_image.jpg";

            //Image16.ImageUrl = imagePath + "no_image.jpg";
            //hlImage16.NavigateUrl = imagePath + "no_image.jpg";

            //Image19.ImageUrl = imagePath + "no_image.jpg";
            //hlImage19.NavigateUrl = imagePath + "no_image.jpg";

            //Image20.ImageUrl = imagePath + "no_image.jpg";
            //hlImage20.NavigateUrl = imagePath + "no_image.jpg";

            if (txtRelated1.Text.ToLower().Equals("null"))
                txtRelated1.Text = String.Empty;
            if (txtRelated2.Text.ToLower().Equals("null"))
                txtRelated2.Text = String.Empty;
            if (txtRelated3.Text.ToLower().Equals("null"))
                txtRelated3.Text = String.Empty;
            if (txtRelated4.Text.ToLower().Equals("null"))
                txtRelated4.Text = String.Empty;
            if (txtRelated5.Text.ToLower().Equals("null"))
                txtRelated5.Text = String.Empty;

            if (txtRelated6.Text.ToLower().Equals("null"))
                txtRelated6.Text = String.Empty;
            if (txtRelated7.Text.ToLower().Equals("null"))
                txtRelated7.Text = String.Empty;
            if (txtRelated8.Text.ToLower().Equals("null"))
                txtRelated8.Text = String.Empty;
            if (txtRelated9.Text.ToLower().Equals("null"))
                txtRelated9.Text = String.Empty;
            if (txtRelated10.Text.ToLower().Equals("null"))
                txtRelated10.Text = String.Empty;

            if (txtRelated11.Text.ToLower().Equals("null"))
                txtRelated11.Text = String.Empty;
            if (txtRelated12.Text.ToLower().Equals("null"))
                txtRelated12.Text = String.Empty;
            if (txtRelated13.Text.ToLower().Equals("null"))
                txtRelated13.Text = String.Empty;
            if (txtRelated14.Text.ToLower().Equals("null"))
                txtRelated14.Text = String.Empty;
            if (txtRelated15.Text.ToLower().Equals("null"))
                txtRelated15.Text = String.Empty;

            if (txtRelated16.Text.ToLower().Equals("null"))
                txtRelated16.Text = String.Empty;
            if (txtRelated17.Text.ToLower().Equals("null"))
                txtRelated17.Text = String.Empty;
            if (txtRelated18.Text.ToLower().Equals("null"))
                txtRelated18.Text = String.Empty;
            if (txtRelated19.Text.ToLower().Equals("null"))
                txtRelated19.Text = String.Empty;
            if (txtRelated20.Text.ToLower().Equals("null"))
                txtRelated20.Text = String.Empty;


            if (txtTemplate1.Text.ToLower().Equals("null"))
                txtTemplate1.Text = String.Empty;
            if (txtTemplate2.Text.ToLower().Equals("null"))
                txtTemplate2.Text = String.Empty;
            if (txtTemplate3.Text.ToLower().Equals("null"))
                txtTemplate3.Text = String.Empty;
            if (txtTemplate4.Text.ToLower().Equals("null"))
                txtTemplate4.Text = String.Empty;
            if (txtTemplate5.Text.ToLower().Equals("null"))
                txtTemplate5.Text = String.Empty;
            if (txtTemplate6.Text.ToLower().Equals("null"))
                txtTemplate6.Text = String.Empty;
            if (txtTemplate7.Text.ToLower().Equals("null"))
                txtTemplate7.Text = String.Empty;
            if (txtTemplate8.Text.ToLower().Equals("null"))
                txtTemplate8.Text = String.Empty;
            if (txtTemplate9.Text.ToLower().Equals("null"))
                txtTemplate9.Text = String.Empty;
            if (txtTemplate10.Text.ToLower().Equals("null"))
                txtTemplate10.Text = String.Empty;
            if (txtTemplate11.Text.ToLower().Equals("null"))
                txtTemplate11.Text = String.Empty;
            if (txtTemplate12.Text.ToLower().Equals("null"))
                txtTemplate12.Text = String.Empty;
            if (txtTemplate13.Text.ToLower().Equals("null"))
                txtTemplate13.Text = String.Empty;
            if (txtTemplate14.Text.ToLower().Equals("null"))
                txtTemplate14.Text = String.Empty;
            if (txtTemplate15.Text.ToLower().Equals("null"))
                txtTemplate15.Text = String.Empty;
            if (txtTemplate16.Text.ToLower().Equals("null"))
                txtTemplate16.Text = String.Empty;
            if (txtTemplate17.Text.ToLower().Equals("null"))
                txtTemplate17.Text = String.Empty;
            if (txtTemplate18.Text.ToLower().Equals("null"))
                txtTemplate18.Text = String.Empty;
            if (txtTemplate19.Text.ToLower().Equals("null"))
                txtTemplate19.Text = String.Empty;
            if (txtTemplate20.Text.ToLower().Equals("null"))
                txtTemplate20.Text = String.Empty;
            if (txtTemplate_Content1.Text.ToLower().Equals("null"))
                txtTemplate_Content1.Text = String.Empty;
            if (txtTemplate_Content2.Text.ToLower().Equals("null"))
                txtTemplate_Content2.Text = String.Empty;
            if (txtTemplate_Content3.Text.ToLower().Equals("null"))
                txtTemplate_Content3.Text = String.Empty;
            if (txtTemplate_Content4.Text.ToLower().Equals("null"))
                txtTemplate_Content4.Text = String.Empty;
            if (txtTemplate_Content5.Text.ToLower().Equals("null"))
                txtTemplate_Content5.Text = String.Empty;
            if (txtTemplate_Content6.Text.ToLower().Equals("null"))
                txtTemplate_Content6.Text = String.Empty;
            if (txtTemplate_Content7.Text.ToLower().Equals("null"))
                txtTemplate_Content7.Text = String.Empty;
            if (txtTemplate_Content8.Text.ToLower().Equals("null"))
                txtTemplate_Content8.Text = String.Empty;
            if (txtTemplate_Content9.Text.ToLower().Equals("null"))
                txtTemplate_Content9.Text = String.Empty;
            if (txtTemplate_Content10.Text.ToLower().Equals("null"))
                txtTemplate_Content10.Text = String.Empty;
            if (txtTemplate_Content11.Text.ToLower().Equals("null"))
                txtTemplate_Content11.Text = String.Empty;
            if (txtTemplate_Content12.Text.ToLower().Equals("null"))
                txtTemplate_Content12.Text = String.Empty;
            if (txtTemplate_Content13.Text.ToLower().Equals("null"))
                txtTemplate_Content13.Text = String.Empty;
            if (txtTemplate_Content14.Text.ToLower().Equals("null"))
                txtTemplate_Content14.Text = String.Empty;
            if (txtTemplate_Content15.Text.ToLower().Equals("null"))
                txtTemplate_Content15.Text = String.Empty;
            if (txtTemplate_Content16.Text.ToLower().Equals("null"))
                txtTemplate_Content16.Text = String.Empty;
            if (txtTemplate_Content17.Text.ToLower().Equals("null"))
                txtTemplate_Content17.Text = String.Empty;
            if (txtTemplate_Content18.Text.ToLower().Equals("null"))
                txtTemplate_Content18.Text = String.Empty;
            if (txtTemplate_Content19.Text.ToLower().Equals("null"))
                txtTemplate_Content19.Text = String.Empty;
            if (txtTemplate_Content20.Text.ToLower().Equals("null"))
                txtTemplate_Content20.Text = String.Empty;
          
        }

        #region YahooSpecificValue

        public void ShowValue()
        {
            try
            {
                if (YahooSpecificValue != null && YahooSpecificValue.Rows.Count > 0)
                {
                    DataRow[] dr = YahooSpecificValue.Select("Type = 1");
                    if (dr.Count() > 0)
                        txtYahooValue1.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue1.Text = "";

                    dr = YahooSpecificValue.Select("Type = 2");
                    if (dr.Count() > 0)
                        txtYahooValue2.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue2.Text = "";

                    dr = YahooSpecificValue.Select("Type = 3");
                    if (dr.Count() > 0)
                        txtYahooValue3.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue3.Text = "";

                    dr = YahooSpecificValue.Select("Type = 4");
                    if (dr.Count() > 0)
                        txtYahooValue4.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue4.Text = "";

                    dr = YahooSpecificValue.Select("Type = 5");
                    if (dr.Count() > 0)
                        txtYahooValue5.Text = dr[0]["Spec_ValueName"].ToString();
                    else
                        txtYahooValue5.Text = "";
                }
                else
                {
                    //imgbYahooSpecValue.Enabled = false;
                    txtYahooValue1.Text = "";
                    txtYahooValue2.Text = "";
                    txtYahooValue3.Text = "";
                    txtYahooValue4.Text = "";
                    txtYahooValue5.Text = "";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void InsertYahooSpecificValue(int ItemID)
        {
            try
            {
                Item_YahooSpecificValue_BL YahooSpecificValueBL = new Item_YahooSpecificValue_BL();
                YahooSpecificValueBL.Insert(ItemID, YahooSpecificValue);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SetYahooSpacificValue(int ItemID)
        {
            try
            {
                Item_YahooSpecificValue_BL YahooSpecificValueBL = new Item_YahooSpecificValue_BL();
                Session["YahooSpecificValue_" + ItemCode] = YahooSpecificValueBL.SelectByItemID(ItemID);
                ShowValue();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void DisplayYahooSpecificValue(string yahoomallID)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(yahoomallID) && yahoomallID != "0")
                {
                    Yahoo_SpecName_BL YahooSpecNameBL = new Yahoo_SpecName_BL();
                    DataTable dt = YahooSpecNameBL.DisplayYahooSpecificValue(Convert.ToInt32(yahoomallID));
                    Session["YahooSpecificValue"] = dt;
                    ShowValue();
                }
                else
                {
                    Session["YahooSpecificValue"] = null;
                    ShowValue();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        #endregion

        #region Option
        public void ShowOption()
        {
            //divOption.Visible = false;
            try
            {
                if (Option != null && Option.Rows.Count > 0)
                {
                    DataTable dt = Option as DataTable;
                    SetOption(dt);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }
        /// <summary>
        /// To insert or update option and connect to Item_Option table
        /// </summary>
        /// <param name="itemID">To keep item master id</param>
        public void InsertOption(int itemID)
        {
            try
            {
                Item_Option_BL ItemOptionBL = new Item_Option_BL();
                GetOption();
                ItemOptionBL.Insert(itemID, Option);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }
        /// <summary>
        /// To display selected options
        /// </summary>
        /// <param name="itemID">By selected item master id</param>
        public void GetOptionSelectByItemID(int itemID)
        {
            try
            {
                Item_Option_BL ItemOptionBL = new Item_Option_BL();
                DataTable dttmp = ItemOptionBL.SelectByItemID(itemID);
                if (dttmp != null && dttmp.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name1", typeof(string));
                    dt.Columns.Add("Value1", typeof(string));
                    dt.Columns.Add("Name2", typeof(string));
                    dt.Columns.Add("Value2", typeof(string));
                    dt.Columns.Add("Name3", typeof(string));
                    dt.Columns.Add("Value3", typeof(string));
                    if (dttmp.Rows.Count > 2)
                    {
                        dt.Rows.Add(dttmp.Rows[0]["Option_Name"].ToString(), dttmp.Rows[0]["Option_Value"].ToString(),
                                              dttmp.Rows[1]["Option_Name"].ToString(), dttmp.Rows[1]["Option_Value"].ToString(),
                                              dttmp.Rows[2]["Option_Name"].ToString(), dttmp.Rows[2]["Option_Value"].ToString());
                    }
                    else if (dttmp.Rows.Count > 1)
                    {
                        dt.Rows.Add(dttmp.Rows[0]["Option_Name"].ToString(), dttmp.Rows[0]["Option_Value"].ToString(), dttmp.Rows[1]["Option_Name"].ToString(), dttmp.Rows[1]["Option_Value"].ToString(), "", "");
                    }
                    else if (dttmp.Rows.Count > 0)
                    {
                        dt.Rows.Add(dttmp.Rows[0]["Option_Name"].ToString(), dttmp.Rows[0]["Option_Value"].ToString(), "", "", "", "");
                    }

                    SetOption(dt);
                    Session["Option_" + ItemCode] = dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void GetOption()
        {
            try
            {
                if (Option != null)
                {
                    Option.Rows[0]["Name1"] = txtOptionName1.Text;
                    Option.Rows[0]["Name2"] = txtOptionName2.Text;
                    Option.Rows[0]["Name3"] = txtOptionName3.Text;
                    Option.Rows[0]["Value1"] = txtOptionValue1.Text;
                    Option.Rows[0]["Value2"] = txtOptionValue2.Text;
                    Option.Rows[0]["Value3"] = txtOptionValue3.Text;
                    Option.AcceptChanges();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name1", typeof(string));
                    dt.Columns.Add("Value1", typeof(string));
                    dt.Columns.Add("Name2", typeof(string));
                    dt.Columns.Add("Value2", typeof(string));
                    dt.Columns.Add("Name3", typeof(string));
                    dt.Columns.Add("Value3", typeof(string));
                    dt.Rows.Add(txtOptionName1.Text, txtOptionValue1.Text, txtOptionName2.Text, txtOptionValue2.Text, txtOptionName3.Text, txtOptionValue3.Text);
                    dt.AcceptChanges();
                    Session["Option_" + ItemCode] = dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SetOption(DataTable dt)
        {
            try
            {
                txtOptionName1.Text = dt.Rows[0]["Name1"].ToString();
                txtOptionName2.Text = dt.Rows[0]["Name2"].ToString();
                txtOptionName3.Text = dt.Rows[0]["Name3"].ToString();
                txtOptionValue1.Text = dt.Rows[0]["Value1"].ToString();
                txtOptionValue2.Text = dt.Rows[0]["Value2"].ToString();
                txtOptionValue3.Text = dt.Rows[0]["Value3"].ToString();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        #endregion

        #region Template_Detail
        public void SelectTemplateDetail(string item_code)
        {
            try
            {
                Template_Detail_BL template = new Template_Detail_BL();
                DataTable dttemplate = template.SelectByItemCode(item_code);

                if (dttemplate != null && dttemplate.Rows.Count > 0)
                {
                    txtTemplate1.Text = dttemplate.Rows[0]["Template1"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate2.Text = dttemplate.Rows[0]["Template2"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate3.Text = dttemplate.Rows[0]["Template3"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate4.Text = dttemplate.Rows[0]["Template4"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate5.Text = dttemplate.Rows[0]["Template5"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate6.Text = dttemplate.Rows[0]["Template6"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate7.Text = dttemplate.Rows[0]["Template7"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate8.Text = dttemplate.Rows[0]["Template8"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate9.Text = dttemplate.Rows[0]["Template9"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate10.Text = dttemplate.Rows[0]["Template10"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate11.Text = dttemplate.Rows[0]["Template11"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate12.Text = dttemplate.Rows[0]["Template12"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate13.Text = dttemplate.Rows[0]["Template13"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate14.Text = dttemplate.Rows[0]["Template14"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate15.Text = dttemplate.Rows[0]["Template15"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate16.Text = dttemplate.Rows[0]["Template16"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate17.Text = dttemplate.Rows[0]["Template17"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate18.Text = dttemplate.Rows[0]["Template18"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate18.Text = dttemplate.Rows[0]["Template18"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate19.Text = dttemplate.Rows[0]["Template19"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate20.Text = dttemplate.Rows[0]["Template20"].ToString().Replace("$CapitalSports$", "【・】");

                    txtTemplate_Content1.Text = dttemplate.Rows[0]["Template_Content1"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content2.Text = dttemplate.Rows[0]["Template_Content2"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content3.Text = dttemplate.Rows[0]["Template_Content3"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content4.Text = dttemplate.Rows[0]["Template_Content4"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content5.Text = dttemplate.Rows[0]["Template_Content5"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content6.Text = dttemplate.Rows[0]["Template_Content6"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content7.Text = dttemplate.Rows[0]["Template_Content7"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content8.Text = dttemplate.Rows[0]["Template_Content8"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content9.Text = dttemplate.Rows[0]["Template_Content9"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content10.Text = dttemplate.Rows[0]["Template_Content10"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content11.Text = dttemplate.Rows[0]["Template_Content11"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content12.Text = dttemplate.Rows[0]["Template_Content12"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content13.Text = dttemplate.Rows[0]["Template_Content13"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content14.Text = dttemplate.Rows[0]["Template_Content14"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content15.Text = dttemplate.Rows[0]["Template_Content15"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content16.Text = dttemplate.Rows[0]["Template_Content16"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content17.Text = dttemplate.Rows[0]["Template_Content17"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content18.Text = dttemplate.Rows[0]["Template_Content18"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content19.Text = dttemplate.Rows[0]["Template_Content19"].ToString().Replace("$CapitalSports$", "【・】");
                    txtTemplate_Content20.Text = dttemplate.Rows[0]["Template_Content20"].ToString().Replace("$CapitalSports$", "【・】");

                    //txtDetail_Template1.Text = dttemplate.Rows[0]["Detail_Template1"].ToString().Replace("$CapitalSports$", "【・】");
                    //txtDetail_Template2.Text = dttemplate.Rows[0]["Detail_Template2"].ToString().Replace("$CapitalSports$", "【・】");
                    //txtDetail_Template3.Text = dttemplate.Rows[0]["Detail_Template3"].ToString().Replace("$CapitalSports$", "【・】");
                    //txtDetail_Template4.Text = dttemplate.Rows[0]["Detail_Template4"].ToString().Replace("$CapitalSports$", "【・】");

                    //txtDetail_Template_Content1.Text = dttemplate.Rows[0]["Detail_Template_Content1"].ToString().Replace("$CapitalSports$", "【・】");
                    //txtDetail_Template_Content2.Text = dttemplate.Rows[0]["Detail_Template_Content2"].ToString().Replace("$CapitalSports$", "【・】");
                    //txtDetail_Template_Content3.Text = dttemplate.Rows[0]["Detail_Template_Content3"].ToString().Replace("$CapitalSports$", "【・】");
                    //txtDetail_Template_Content4.Text = dttemplate.Rows[0]["Detail_Template_Content4"].ToString().Replace("$CapitalSports$", "【・】");

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SaveTemplateDetail(string item_code)
        {
            Template_Detail_Entity tde = new Template_Detail_Entity();
            Template_Detail_BL tdbl = new Template_Detail_BL();

            tde.Template1 = txtTemplate1.Text;
            tde.Template2 = txtTemplate2.Text;
            tde.Template3 = txtTemplate3.Text;
            tde.Template4 = txtTemplate4.Text;
            tde.Template5 = txtTemplate5.Text;
            tde.Template6 = txtTemplate6.Text;
            tde.Template7 = txtTemplate7.Text;
            tde.Template8 = txtTemplate8.Text;
            tde.Template9 = txtTemplate9.Text;
            tde.Template10 = txtTemplate10.Text;
            tde.Template11 = txtTemplate11.Text;
            tde.Template12 = txtTemplate12.Text;
            tde.Template13 = txtTemplate13.Text;
            tde.Template14 = txtTemplate14.Text;
            tde.Template15 = txtTemplate15.Text;
            tde.Template16 = txtTemplate16.Text;
            tde.Template17 = txtTemplate17.Text;
            tde.Template18 = txtTemplate18.Text;
            tde.Template19 = txtTemplate19.Text;
            tde.Template20 = txtTemplate20.Text;

            tde.Template_Content1 = txtTemplate_Content1.Text;
            tde.Template_Content2 = txtTemplate_Content2.Text;
            tde.Template_Content3 = txtTemplate_Content3.Text;
            tde.Template_Content4 = txtTemplate_Content4.Text;
            tde.Template_Content5 = txtTemplate_Content5.Text;
            tde.Template_Content6 = txtTemplate_Content6.Text;
            tde.Template_Content7 = txtTemplate_Content7.Text;
            tde.Template_Content8 = txtTemplate_Content8.Text;
            tde.Template_Content9 = txtTemplate_Content9.Text;
            tde.Template_Content10 = txtTemplate_Content10.Text;
            tde.Template_Content11 = txtTemplate_Content11.Text;
            tde.Template_Content12 = txtTemplate_Content12.Text;
            tde.Template_Content13 = txtTemplate_Content13.Text;
            tde.Template_Content14 = txtTemplate_Content14.Text;
            tde.Template_Content15 = txtTemplate_Content15.Text;
            tde.Template_Content16 = txtTemplate_Content16.Text;
            tde.Template_Content17 = txtTemplate_Content17.Text;
            tde.Template_Content18 = txtTemplate_Content18.Text;
            tde.Template_Content19 = txtTemplate_Content19.Text;
            tde.Template_Content20 = txtTemplate_Content20.Text;

            //tde.Detail_Template1 = txtDetail_Template1.Text;
            //tde.Detail_Template2 = txtDetail_Template2.Text;
            //tde.Detail_Template3 = txtDetail_Template3.Text;
            //tde.Detail_Template4 = txtDetail_Template4.Text;

            //tde.Detail_Template_Content1 = txtDetail_Template_Content1.Text;
            //tde.Detail_Template_Content2 = txtDetail_Template_Content2.Text;
            //tde.Detail_Template_Content3 = txtDetail_Template_Content3.Text;
            //tde.Detail_Template_Content4 = txtDetail_Template_Content4.Text;

            tdbl.Update(item_code, tde);

        }

        #endregion

        public void BindDailyFlag(string ItemCode)
        {
            try
            {
                DataTable dt = imeBL.BindDailyFlag(ItemCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //if (Convert.ToBoolean(dt.Rows[0]["Flag"].ToString()) == true)
                    //    delivery_flag.Checked = true;
                    //else
                    //    delivery_flag.Checked = false;
                    //if (Convert.ToBoolean(dt.Rows[0]["Active_Status"].ToString()) == true)
                    //    chkActive.Checked = true;
                    //else
                    //    chkActive.Checked = false;
                    //if (Convert.ToBoolean(dt.Rows[0]["Inventory_Flag"].ToString()) == true)
                    //    chkInventory.Checked = true;
                    //else
                    //    chkInventory.Checked = false;
                    //txtInactive.Text = dt.Rows[0]["Comment"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SKU_BIND()
        {
            try
            {
                DataTable dtskucolor = item.SelectSKUItemCode(ItemCode);
                //gvSKUColor.DataSource = item.SelectSKUColor(ItemCode); //Select From Item Table
                //gvSKUColor.DataBind();
                DataTable dtsku = new DataTable();
                DataTable dt = new DataTable();
                if (dtskucolor.Rows.Count > 0)
                {
                    //dtsku = item.SelectSKU(ItemCode);
                    gvSKU.DataSource = dtskucolor;//item.SelectSKU(ItemCode); //Select From Item Table
                    gvSKU.DataBind();
                    //dt = item.SelectSKUSize(ItemCode); //Select From Item Table
                    //gvSKUSize.DataSource = dt;
                    //gvSKUSize.DataBind();
                    rdb1.Checked = true;
                    rdb2.Checked = false;
                }
                else
                {
                    rdb1.Checked = false;
                    rdb2.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        #region Photo
        public void BindPhotoList()
        {
            try
            {
                if (ImageList != null)
                {
                    DataTable dt = ImageList as DataTable;
                    /* 06/07/2015 by AM  SKS-26(BackLog)
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        imeBL = new Item_Master_BL();
                        int ItemID = imeBL.SelectItemID(ItemCode);
                        if (String.IsNullOrEmpty(dr2["Item_ID"].ToString()))
                        {
                            dr2["Item_ID"] = ItemID;
                            dr2["ID"] = 0;
                        }
                    }
                    */
                    #region Item Image
                    DataRow[] dr = dt.Select("Image_Type='0'");
                    if (dr.Length > 0)
                    {
                        DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();

                        for (int m = 0; m < dtImage.Rows.Count; m++)
                        {
                            switch (dtImage.Rows[m]["SN"].ToString())
                            {
                                case "1":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image1.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage1.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg1.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image1.ImageUrl = imagePath + "no_image.jpg"; ;
                                        hlImage1.NavigateUrl = imagePath + "no_image.jpg"; ;
                                        txtimg1.Text = "";
                                    }
                                    break;
                                case "2":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image2.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage2.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg2.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image2.ImageUrl = imagePath + "no_image.jpg"; ;
                                        hlImage2.NavigateUrl = imagePath + "no_image.jpg"; ;
                                        txtimg2.Text = "";
                                    }
                                    break;
                                case "3":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image3.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage3.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg3.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image3.ImageUrl = imagePath + "no_image.jpg"; ;
                                        hlImage3.NavigateUrl = imagePath + "no_image.jpg"; ;
                                        txtimg3.Text = "";
                                    }
                                    break;
                                case "4":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image4.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage4.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg4.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image4.ImageUrl = imagePath + "no_image.jpg"; ;
                                        hlImage4.NavigateUrl = imagePath + "no_image.jpg"; ;
                                        txtimg4.Text = "";
                                    }
                                    break;
                                case "5":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image5.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage5.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg5.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image5.ImageUrl = imagePath + "no_image.jpg"; ;
                                        hlImage5.NavigateUrl = imagePath + "no_image.jpg"; ;
                                        txtimg5.Text = "";
                                    }
                                    break;
                                case "6":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image6.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage6.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg6.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image6.ImageUrl = imagePath + "no_image.jpg"; ;
                                        hlImage6.NavigateUrl = imagePath + "no_image.jpg"; ;
                                        txtimg6.Text = "";
                                    }
                                    break;
                                case "7":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image7.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage7.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg7.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image7.ImageUrl = imagePath + "no_image.jpg"; 
                                        hlImage7.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg7.Text = "";
                                    }
                                    break;
                                case "8":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image8.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage8.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg8.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image8.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage8.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg8.Text = "";
                                    }
                                    break;
                                case "9":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image9.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage9.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg9.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image9.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage9.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg9.Text = "";
                                    }
                                    break;
                                case "10":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image10.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage10.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg10.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image10.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage10.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg10.Text = "";
                                    }
                                    break;
                                case "11":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image11.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage11.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg11.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image11.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage11.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg11.Text = "";
                                    }
                                    break;
                                case "12":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image12.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage12.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg12.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image12.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage12.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg12.Text = "";
                                    }
                                    break;
                                case "13":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image13.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage13.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg13.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image13.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage13.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg13.Text = "";
                                    }
                                    break;
                                case "14":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image14.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage14.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg14.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image14.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage14.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg14.Text = "";
                                    }
                                    break;
                                case "15":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image15.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage15.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg15.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image15.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage15.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg15.Text = "";
                                    }
                                    break;
                                case "16":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image16.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage16.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg16.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image16.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage16.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg16.Text = "";
                                    }
                                    break;
                                case "17":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image17.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage17.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg17.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image17.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage17.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg17.Text = "";
                                    }
                                    break;
                                case "18":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image18.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage18.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg18.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image18.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage18.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg18.Text = "";
                                    }
                                    break;
                                case "19":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image19.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage19.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg19.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image19.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage19.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg19.Text = "";
                                    }
                                    break;
                                case "20":
                                    if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                    {
                                        Image20.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                        hlImage20.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        txtimg20.Text = dtImage.Rows[m]["Image_Name"].ToString();
                                    }
                                    else
                                    {
                                        Image20.ImageUrl = imagePath + "no_image.jpg";
                                        hlImage20.NavigateUrl = imagePath + "no_image.jpg";
                                        txtimg20.Text = "";
                                    }
                                    break;

                            }
                        }
                    }
                    else
                    {
                        Image1.ImageUrl = imagePath + "no_image.jpg";
                        Image2.ImageUrl = imagePath + "no_image.jpg";
                        Image3.ImageUrl = imagePath + "no_image.jpg";
                        Image4.ImageUrl = imagePath + "no_image.jpg";
                        Image5.ImageUrl = imagePath + "no_image.jpg";
                        Image6.ImageUrl = imagePath + "no_image.jpg";
                        Image7.ImageUrl = imagePath + "no_image.jpg";
                        Image8.ImageUrl = imagePath + "no_image.jpg";
                        Image9.ImageUrl = imagePath + "no_image.jpg";
                        Image10.ImageUrl = imagePath + "no_image.jpg";
                        Image11.ImageUrl = imagePath + "no_image.jpg";
                        Image12.ImageUrl = imagePath + "no_image.jpg";
                        Image13.ImageUrl = imagePath + "no_image.jpg";
                        Image14.ImageUrl = imagePath + "no_image.jpg";
                        Image15.ImageUrl = imagePath + "no_image.jpg";
                        Image16.ImageUrl = imagePath + "no_image.jpg";
                        Image17.ImageUrl = imagePath + "no_image.jpg";
                        Image18.ImageUrl = imagePath + "no_image.jpg";
                        Image19.ImageUrl = imagePath + "no_image.jpg";
                        Image20.ImageUrl = imagePath + "no_image.jpg";

                        hlImage1.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage2.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage3.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage4.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage5.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage6.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage7.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage8.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage9.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage10.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage11.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage12.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage13.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage14.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage15.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage16.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage17.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage18.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage19.NavigateUrl = imagePath + "no_image.jpg";
                        hlImage20.NavigateUrl = imagePath + "no_image.jpg";
                    }
                    #endregion

     
                }
                else
                {
                    Image1.ImageUrl = imagePath + "no_image.jpg";
                    Image2.ImageUrl = imagePath + "no_image.jpg";
                    Image3.ImageUrl = imagePath + "no_image.jpg";
                    Image4.ImageUrl = imagePath + "no_image.jpg";
                    Image5.ImageUrl = imagePath + "no_image.jpg";
                    Image6.ImageUrl = imagePath + "no_image.jpg";
                    Image7.ImageUrl = imagePath + "no_image.jpg";
                    Image8.ImageUrl = imagePath + "no_image.jpg";
                    Image9.ImageUrl = imagePath + "no_image.jpg";
                    Image10.ImageUrl = imagePath + "no_image.jpg";
                    Image11.ImageUrl = imagePath + "no_image.jpg";
                    Image12.ImageUrl = imagePath + "no_image.jpg";
                    Image13.ImageUrl = imagePath + "no_image.jpg";
                    Image14.ImageUrl = imagePath + "no_image.jpg";
                    Image15.ImageUrl = imagePath + "no_image.jpg";
                    Image16.ImageUrl = imagePath + "no_image.jpg";
                    Image17.ImageUrl = imagePath + "no_image.jpg";
                    Image18.ImageUrl = imagePath + "no_image.jpg";
                    Image19.ImageUrl = imagePath + "no_image.jpg";
                    Image20.ImageUrl = imagePath + "no_image.jpg";

                    hlImage1.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage2.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage3.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage4.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage5.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage6.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage7.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage8.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage9.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage10.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage11.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage12.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage13.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage14.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage15.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage16.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage17.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage18.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage19.NavigateUrl = imagePath + "no_image.jpg";
                    hlImage20.NavigateUrl = imagePath + "no_image.jpg";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void ReBindPhotoList()
        {
            try
            {
                if (ImageList != null)
                {
                    DataTable dt = ImageList as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        #region Item Image
                        DataRow[] dr = dt.Select("Image_Type='0'");
                        if (dr.Length > 0)
                        {
                            DataTable dtImage = dt.Select("Image_Type='0'").CopyToDataTable();
                            #region Set null value
                            Image1.ImageUrl = "";
                            hlImage1.NavigateUrl = "";
                            Image2.ImageUrl = "";
                            hlImage2.NavigateUrl = "";
                            Image3.ImageUrl = "";
                            hlImage3.NavigateUrl = "";
                            Image4.ImageUrl = "";
                            hlImage4.NavigateUrl = "";
                            Image5.ImageUrl = "";
                            hlImage5.NavigateUrl = "";
                            Image6.ImageUrl = "";
                            Image7.ImageUrl = "";
                            Image8.ImageUrl = "";
                            Image9.ImageUrl = "";
                            Image10.ImageUrl = "";
                            Image11.ImageUrl = "";
                            Image12.ImageUrl = "";
                            Image13.ImageUrl = "";
                            Image14.ImageUrl = "";
                            Image15.ImageUrl = "";
                            Image16.ImageUrl = "";
                            Image17.ImageUrl = "";
                            Image18.ImageUrl = "";
                            Image19.ImageUrl = "";
                            Image20.ImageUrl = "";
                            hlImage6.NavigateUrl = "";
                            hlImage7.NavigateUrl = "";
                            hlImage8.NavigateUrl = "";
                            hlImage9.NavigateUrl = "";
                            hlImage10.NavigateUrl = "";
                            hlImage11.NavigateUrl = "";
                            hlImage12.NavigateUrl = "";
                            hlImage13.NavigateUrl = "";
                            hlImage14.NavigateUrl = "";
                            hlImage15.NavigateUrl = "";
                            hlImage16.NavigateUrl = "";
                            hlImage17.NavigateUrl = "";
                            hlImage18.NavigateUrl = "";
                            hlImage19.NavigateUrl = "";
                            hlImage20.NavigateUrl = "";
                            #endregion

                            #region Set value
                            for (int m = 0; m < dtImage.Rows.Count; m++)
                            {
                                switch (dtImage.Rows[m]["SN"].ToString())
                                {
                                    case "1":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image1.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage1.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image1.ImageUrl = "";
                                            hlImage1.NavigateUrl = "";
                                        }
                                        break;
                                    case "2":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image2.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage2.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image2.ImageUrl = "";
                                            hlImage2.NavigateUrl = "";
                                        }
                                        break;
                                    case "3":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image3.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage3.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image3.ImageUrl = "";
                                            hlImage3.NavigateUrl = "";
                                        }
                                        break;
                                    case "4":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image4.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage4.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image4.ImageUrl = "";
                                            hlImage4.NavigateUrl = "";
                                        }
                                        break;
                                    case "5":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image5.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage5.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image5.ImageUrl = "";
                                            hlImage5.NavigateUrl = "";
                                        }
                                        break;
                                    case "6":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image6.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage6.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image6.ImageUrl = "";
                                            hlImage6.NavigateUrl = "";
                                        }
                                        break;
                                    case "7":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image7.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage7.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image7.ImageUrl = "";
                                            hlImage7.NavigateUrl = "";
                                        }
                                        break;
                                    case "8":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image8.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage8.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image8.ImageUrl = "";
                                            hlImage8.NavigateUrl = "";
                                        }
                                        break;
                                    case "9":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image9.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage9.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image9.ImageUrl = "";
                                            hlImage9.NavigateUrl = "";
                                        }
                                        break;
                                    case "10":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image10.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage10.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image10.ImageUrl = "";
                                            hlImage10.NavigateUrl = "";
                                        }
                                        break;
                                    case "11":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image11.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage11.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image11.ImageUrl = "";
                                            hlImage11.NavigateUrl = "";
                                        }
                                        break;
                                    case "12":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image12.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage12.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image12.ImageUrl = "";
                                            hlImage12.NavigateUrl = "";
                                        }
                                        break;
                                    case "13":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image13.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage13.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image13.ImageUrl = "";
                                            hlImage13.NavigateUrl = "";
                                        }
                                        break;
                                    case "14":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image14.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage14.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image14.ImageUrl = "";
                                            hlImage14.NavigateUrl = "";
                                        }
                                        break;
                                    case "15":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image15.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage15.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image15.ImageUrl = "";
                                            hlImage15.NavigateUrl = "";
                                        }
                                        break;
                                    case "16":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image16.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage16.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image16.ImageUrl = "";
                                            hlImage16.NavigateUrl = "";
                                        }
                                        break;
                                    case "17":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image17.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage17.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image17.ImageUrl = "";
                                            hlImage17.NavigateUrl = "";
                                        }
                                        break;
                                    case "18":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image18.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage18.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image18.ImageUrl = "";
                                            hlImage18.NavigateUrl = "";
                                        }
                                        break;
                                    case "19":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image19.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage19.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image19.ImageUrl = "";
                                            hlImage19.NavigateUrl = "";
                                        }
                                        break;
                                    case "20":
                                        if (!string.IsNullOrWhiteSpace(dtImage.Rows[m]["Image_Name"].ToString()))
                                        {
                                            Image20.ImageUrl = imagePath + dtImage.Rows[m]["Image_Name"] + "";
                                            hlImage20.NavigateUrl = imagePath + dtImage.Rows[m]["Image_Name"];
                                        }
                                        else
                                        {
                                            Image20.ImageUrl = "";
                                            hlImage20.NavigateUrl = "";
                                        }
                                        break;


                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        Image1.ImageUrl = "";
                        Image2.ImageUrl = "";
                        Image3.ImageUrl = "";
                        Image4.ImageUrl = "";
                        Image5.ImageUrl = "";
                        Image6.ImageUrl = "";
                        Image7.ImageUrl = "";
                        Image8.ImageUrl = "";
                        Image9.ImageUrl = "";
                        Image10.ImageUrl = "";
                        Image11.ImageUrl = "";
                        Image12.ImageUrl = "";
                        Image13.ImageUrl = "";
                        Image14.ImageUrl = "";
                        Image15.ImageUrl = "";
                        Image16.ImageUrl = "";
                        Image17.ImageUrl = "";
                        Image18.ImageUrl = "";
                        Image19.ImageUrl = "";
                        Image20.ImageUrl = "";

                        hlImage1.NavigateUrl = "";
                        hlImage2.NavigateUrl = "";
                        hlImage3.NavigateUrl = "";
                        hlImage4.NavigateUrl = "";
                        hlImage5.NavigateUrl = "";
                        hlImage6.NavigateUrl = "";
                        hlImage7.NavigateUrl = "";
                        hlImage8.NavigateUrl = "";
                        hlImage9.NavigateUrl = "";
                        hlImage10.NavigateUrl = "";
                        hlImage11.NavigateUrl = "";
                        hlImage12.NavigateUrl = "";
                        hlImage13.NavigateUrl = "";
                        hlImage14.NavigateUrl = "";
                        hlImage15.NavigateUrl = "";
                        hlImage16.NavigateUrl = "";
                        hlImage17.NavigateUrl = "";
                        hlImage18.NavigateUrl = "";
                        hlImage19.NavigateUrl = "";
                        hlImage20.NavigateUrl = "";
                    }
                    #endregion

              
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void SetImagenull()
        {
            if (String.IsNullOrWhiteSpace(txtimg1.Text.ToString()))
            {
                Image1.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage1.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg2.Text.ToString()))
            {
                Image2.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage2.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg3.Text.ToString()))
            {
                Image3.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage3.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg4.Text.ToString()))
            {
                Image4.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage4.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg5.Text.ToString()))
            {
                Image5.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage5.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg6.Text.ToString()))
            {
                Image6.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage6.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg7.Text.ToString()))
            {
                Image7.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage7.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg8.Text.ToString()))
            {
                Image8.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage8.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg9.Text.ToString()))
            {
                Image9.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage9.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg10.Text.ToString()))
            {
                Image10.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage10.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg11.Text.ToString()))
            {
                Image11.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage11.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg12.Text.ToString()))
            {
                Image12.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage12.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg13.Text.ToString()))
            {
                Image13.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage13.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg14.Text.ToString()))
            {
                Image14.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage14.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg15.Text.ToString()))
            {
                Image15.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage15.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg16.Text.ToString()))
            {
                Image16.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage16.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg17.Text.ToString()))
            {
                Image17.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage17.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg18.Text.ToString()))
            {
                Image18.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage18.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg19.Text.ToString()))
            {
                Image19.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage19.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
            if (String.IsNullOrWhiteSpace(txtimg20.Text.ToString()))
            {
                Image20.ImageUrl = "../../Item_Image/no_image.jpg";
                hlImage20.NavigateUrl = "../../Item_Image/no_image.jpg";
            }
        }

        public void InsertPhoto(int itemID)
        {
            try
            {

                DataTable dtImage = new DataTable();
                dtImage.Columns.Add(new DataColumn("Item_ID", typeof(int)));
                dtImage.Columns.Add(new DataColumn("Image_Name", typeof(string)));
                dtImage.Columns.Add(new DataColumn("Image_Type", typeof(int)));
                dtImage.Columns.Add(new DataColumn("SN", typeof(int)));
                if (!String.IsNullOrWhiteSpace(txtimg1.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg1.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 1;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg2.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg2.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 2;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg3.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg3.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 3;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg4.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg4.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 4;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg5.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg5.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 5;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg6.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg6.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 6;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg7.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg7.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 7;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg8.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg8.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 8;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg9.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg9.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 9;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg10.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg10.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 10;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg11.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg11.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 11;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg12.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg12.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 12;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg13.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg13.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 13;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg14.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg14.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 14;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg15.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg15.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 15;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg16.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg16.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 16;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg17.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg17.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 17;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg18.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg18.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 18;
                    dtImage.Rows.Add(dr);
                }
                if (!String.IsNullOrWhiteSpace(txtimg19.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg19.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 19;
                    dtImage.Rows.Add(dr);
                }

                if (!String.IsNullOrWhiteSpace(txtimg20.Text.ToString()))
                {
                    DataRow dr = dtImage.NewRow();
                    dr["Item_ID"] = itemID;
                    dr["Image_Name"] = txtimg20.Text.ToString();
                    dr["Image_Type"] = 0;
                    dr["SN"] = 20;
                    dtImage.Rows.Add(dr);
                }



                //DataTable dtImage = ImageList as DataTable;
                Item_Image_BL itemImageBL = new Item_Image_BL();
               // dtImage = SetLibraryPhoto(dtImage);
                if (dtImage.Rows.Count > 0)
                {
                    DataRow[] dr = dtImage.Select("Image_Type='0'");
                    if (dr.Length > 0)
                    {
                        DataTable dtImage0 = dtImage.Select("Image_Type='0'").CopyToDataTable();

                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dtImage.Rows)
                        {
                            if (int.Parse(row1["Image_Type"].ToString()) == 0)
                            {
                                rows_to_remove.Add(row1);
                            }
                        }
                        foreach (DataRow row in rows_to_remove)
                        {
                            dtImage.Rows.Remove(row);
                            dtImage.AcceptChanges();
                        }

                        dtImage.Merge(RemoveDuplicateRows(dtImage0, "SN"));
                    }
                }
                itemImageBL.Insert(itemID, dtImage);
                if (String.IsNullOrWhiteSpace(txtimg1.Text.ToString()))
                {
                    Image1.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage1.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg2.Text.ToString()))
                {
                    Image2.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage2.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg3.Text.ToString()))
                {
                    Image3.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage3.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg4.Text.ToString()))
                {
                    Image4.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage4.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg5.Text.ToString()))
                {
                    Image5.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage5.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg6.Text.ToString()))
                {
                    Image6.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage6.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg7.Text.ToString()))
                {
                    Image7.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage7.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg8.Text.ToString()))
                {
                    Image8.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage8.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg9.Text.ToString()))
                {
                    Image9.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage9.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg10.Text.ToString()))
                {
                    Image10.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage10.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg11.Text.ToString()))
                {
                    Image11.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage11.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg12.Text.ToString()))
                {
                    Image12.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage12.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg13.Text.ToString()))
                {
                    Image13.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage13.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg14.Text.ToString()))
                {
                    Image14.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage14.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg15.Text.ToString()))
                {
                    Image15.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage15.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg16.Text.ToString()))
                {
                    Image16.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage16.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg17.Text.ToString()))
                {
                    Image17.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage17.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg18.Text.ToString()))
                {
                    Image18.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage18.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg19.Text.ToString()))
                {
                    Image19.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage19.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
                if (String.IsNullOrWhiteSpace(txtimg20.Text.ToString()))
                {
                    Image20.ImageUrl = "../../Item_Image/no_image.jpg";
                    hlImage20.NavigateUrl = "../../Item_Image/no_image.jpg";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }
        public DataTable GetImageList(int itemID)
        {
            DataTable dtImage = new DataTable();
            dtImage.Columns.Add(new DataColumn("Item_ID", typeof(int)));
            dtImage.Columns.Add(new DataColumn("Image_Name", typeof(string)));
            dtImage.Columns.Add(new DataColumn("Image_Type", typeof(int)));
            dtImage.Columns.Add(new DataColumn("SN", typeof(int)));
            if (!String.IsNullOrWhiteSpace(txtimg1.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg1.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 1;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg2.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg2.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 2;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg3.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg3.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 3;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg4.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg4.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 4;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg5.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg5.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 5;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg6.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg6.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 6;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg7.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg7.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 7;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg8.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg8.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 8;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg9.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg9.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 9;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg10.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg10.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 10;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg11.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg11.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 11;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg12.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg12.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 12;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg13.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg13.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 13;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg14.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg14.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 14;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg15.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg15.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 15;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg16.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg16.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 16;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg17.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg17.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 17;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg18.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg18.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 18;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg19.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg19.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 19;
                dtImage.Rows.Add(dr);
            }
            if (!String.IsNullOrWhiteSpace(txtimg20.Text.ToString()))
            {
                DataRow dr = dtImage.NewRow();
                dr["Item_ID"] = itemID;
                dr["Image_Name"] = txtimg20.Text.ToString();
                dr["Image_Type"] = 0;
                dr["SN"] = 20;
                dtImage.Rows.Add(dr);
            }

            return dtImage;
        }
        //for new case if Item_Image exist or not
        public DataTable SetLibraryPhoto(DataTable dt)
        {
            try
            {
                if (dt == null) // not exist ImageList
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Item_ID", typeof(int)));
                    dt.Columns.Add(new DataColumn("Image_Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("Image_Type", typeof(int)));
                    dt.Columns.Add(new DataColumn("SN", typeof(int)));
     
                    return dt;
                }
                else   //exist ImageList
                {

                    #region delete row
                    DataRow[] dr = dt.Select("Image_Type='1' OR Image_Type='2'");
                    if (dr.Length > 0)
                    {
                        DataTable dtLibrary = dt.Select("Image_Type='1' OR Image_Type='2'").CopyToDataTable();
                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dt.Rows)
                        {
                            if (int.Parse(row1["Image_Type"].ToString()) == 1 || int.Parse(row1["Image_Type"].ToString()) == 2)
                            {
                                rows_to_remove.Add(row1);
                            }
                        }
                        foreach (DataRow row in rows_to_remove)
                        {
                            dt.Rows.Remove(row);
                            dt.AcceptChanges();
                        }
                    }
                    #endregion

                
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
                return new DataTable();
            }
        }

        //for new case if Item_Image exist or not


        /// <summary>
        /// To display image when
        /// </summary>
        /// <param name="itemID">By the selected item master id</param>
        public void SelectByItemID(int itemID)
        {
            try
            {
                Item_Image_BL itemImageBL = new Item_Image_BL();
                DataTable dtImage = itemImageBL.SelectByItemID(itemID);
                //added by aam (20/10/2015)
                if (dtImage.Rows.Count > 0)
                {
                    DataRow[] dr = dtImage.Select("Image_Type='0'");
                    if (dr.Length > 0)
                    {
                        DataTable dtImage0 = dtImage.Select("Image_Type='0'").CopyToDataTable();

                        List<DataRow> rows_to_remove = new List<DataRow>();
                        foreach (DataRow row1 in dtImage.Rows)
                        {
                            if (int.Parse(row1["Image_Type"].ToString()) == 0)
                            {
                                rows_to_remove.Add(row1);
                            }
                        }
                        foreach (DataRow row in rows_to_remove)
                        {
                            dtImage.Rows.Remove(row);
                            dtImage.AcceptChanges();
                        }
                        dtImage.Merge(RemoveDuplicateRows(dtImage0, "SN"));
                    }
                }
                Session["ImageList_" + ItemCode] = dtImage;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string col_SN)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[col_SN])))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[col_SN], string.Empty);
            }
            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);
            //Datatable which contains unique records will be return as output.
            return dTable;
        }
        #endregion

  
 

        public void SetCategoryData()
        {
            try
            {
                int rowIndex = 0;
                gvCatagories.DataSource = CategoryList;
                gvCatagories.DataBind();
                if (CategoryList != null && CategoryList.Rows.Count > 0)
                {
                    for (int i = rowIndex; i < CategoryList.Rows.Count; i++)
                    {
                        Label lblID = (Label)gvCatagories.Rows[rowIndex].Cells[1].FindControl("lblID");
                        TextBox txtValue = (TextBox)gvCatagories.Rows[rowIndex].Cells[1].FindControl("txtCTGName");
                        lblID.Text = CategoryList.Rows[i]["CID"].ToString();
                        txtValue.Text = CategoryList.Rows[i]["CName"].ToString();
                        rowIndex++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SetSelectedCategory(int itemID)
        {
            try
            {
                itemCategoryBL = new Item_Category_BL();
                DataTable dt = itemCategoryBL.SelectByItemID(itemID);
                Session["CategoryList_" + ItemCode] = dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void SetSelectedShop(int itemID)
        {
            try
            {
                Shop_BL shopBL = new Shop_BL();
                DataTable dt1 = shopBL.SelectShop_Data();
                dlShop1.DataSource = dt1;
                dlShop1.DataBind();
                Item_Shop_BL itemShopBL = new Item_Shop_BL();
                DataTable dt = itemShopBL.SelectByItemID(itemID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        foreach (DataListItem li in dlShop1.Items)
                        {
                            Label lbl = li.FindControl("lblShopID") as Label;
                            CheckBox cb = li.FindControl("ckbShopName") as CheckBox;
                            if (lbl.Text == dt.Rows[i]["Shop_ID"].ToString())
                            {
                                cb.Checked = true;
                                break;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
        }

        public void SetItemCodeURL()
        {

            Item_Shop_BL isbl = new Item_Shop_BL();
            DataTable dt = isbl.SelectItemCodeURL(ItemCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (DataListItem li in dlShop.Items)
                    {
                        TextBox txtitemcode = li.FindControl("txtItem_CodeList") as TextBox;
                        Label shopid = li.FindControl("lblShopID") as Label;
                        CheckBox cb = li.FindControl("ckbShop") as CheckBox;
                        if (shopid.Text == dt.Rows[i]["Shop_ID"].ToString())
                        {
                            cb.Checked = true;
                            txtitemcode.Text = dt.Rows[i]["Item_Code_URL"].ToString();
                            break;
                        }
                    }
                }
            }
        }

        public void BindOption()
        {
            Option_BL opBL = new Option_BL();
            DataTable dt = opBL.BindOption();
          
            ddlOption.DataSource = dt;
            ddlOption.DataTextField = "Option_GroupName";
            ddlOption.DataValueField = "Option_GroupName";
            ddlOption.DataBind();
            ddlOption.Items.Insert(0, "");
        }

        public void BindSaleUnit()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindSaleUnit();
            ddlsalesunit.DataSource = dt;
            ddlsalesunit.DataTextField = "Sales_unit";
            ddlsalesunit.DataValueField = "Sales_unit";
            ddlsalesunit.DataBind();
            ddlsalesunit.Items.Insert(0, "");
        }

        public void SetItemData(Item_Master_Entity ime)
        {
            try
            {
                hdfCtrl_ID.Value = ime.Ctrl_ID;
                txtItem_Code.Text = ime.Item_Code;
                txtItem_Name.Text = ime.Item_Name;
                txtJanCD.Text = ime.JanCode;
                txtmemo.Text = ime.Memo;
                if (!string.IsNullOrWhiteSpace(ime.NormalLargeKBN.ToString()))
                {
                    int normallargeKBN = 0;
                    if (Convert.ToInt32(ime.NormalLargeKBN) == 0)
                    {
                        normallargeKBN = 1;
                    }
                    else if (Convert.ToInt32(ime.NormalLargeKBN) == 1)
                    {
                        normallargeKBN = 2;
                    }
                    ddNormalLargeKBN.SelectedIndex = normallargeKBN;
                }
                txtsiiresaki.Text = ime.Siiresaki;
                txtProduct_Code.Text = ime.Product_Code;
                if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                {
                    txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date).Replace('-', '/').ToString(); ;
                }
                else
                {
                    txtRelease_Date.Text = "";
                }
                if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                {
                    txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date).Replace('-', '/').ToString(); ;
                }
                else
                {
                    txtPost_Available_Date.Text = "";
                }
              //  txtSeason.Text = ime.Season;
                txtBrand_Code.Text = ime.Brand_Code;
                txtBrand_Name.Text = ime.Brand_Name;
                //txtCompetition_Name.Text = ime.Competition_Name;
                //txtClass_Name.Text = ime.Class_Name;
                //txtCatalog_Information.Text = ime.Catalog_Information;
                txtMerchandise_Information.Text = ime.Merchandise_Information;
            
                //txtZett_Item_Description.Text = ime.Zett_Item_Description;
                //txtZett_Sale_Description.Text = ime.Zett_Sale_Description;
                txtItem_Description_PC.Text = ime.Item_Description_PC;
                txtSale_Description_PC.Text = ime.Sale_Description_PC;
                txtSmart_Template.Text = ime.Smart_Template;
                //txtAdditional_2.Text = ime.Additional_2;
                //txtAdditional_3.Text = ime.Additional_3;
                txtList_Price.Text = string.Format("{0:#,#}", ime.List_Price);
                txtSale_Price.Text = string.Format("{0:#,#}", ime.Sale_Price);
                //txtJisha_Price.Text = string.Format("{0:#,#}", ime.Jisha_Price);

                if (ime.RakutenPrice != 0 || ime.YahooPrice != 0 || ime.WowmaPrice != 0 || ime.JishaPrice != 0 || ime.TennisPrice != 0)
                {
                    //priceDiv.Visible = true;

                    txtRakutenPrice.Text = string.Format("{0:#,#}", ime.RakutenPrice);
                    txtYahooPrice.Text = string.Format("{0:#,#}", ime.YahooPrice);
                    txtWowmaPrice.Text = string.Format("{0:#,#}", ime.WowmaPrice);
                    txtJishaPrice.Text = string.Format("{0:#,#}", ime.JishaPrice);
                    //txtTennisPrice.Text = string.Format("{0:#,#}", ime.TennisPrice); hhw
                }


                else
                {
                    //priceDiv.Visible = false;
                }

                if (ime.Monoprice != 0 )
                {
                    txtmonoprice.Text = string.Format("{0:#,#}", ime.Monoprice);
                }

                if (ime.Diteprice != 0)
                {
                    txtditeprice.Text = string.Format("{0:#,#}", ime.Diteprice);
                }

                if (ime.Japanmprice != 0)
                {
                    txtjapanmprice.Text = string.Format("{0:#,#}", ime.Japanmprice);
                }

                if (ime.Kashiwagikoukiprice != 0)
                {
                    txtkashiwagi.Text = string.Format("{0:#,#}", ime.Kashiwagikoukiprice);
                }


                if (!String.IsNullOrWhiteSpace(txtSale_Price.Text))
                    btnComplete.Enabled = true;

                //txtYear.Text = Convert.ToString(ime.Year);
                ddlShipping_Flag.SelectedValue = Convert.ToString(ime.Shipping_Flag);
                ddlDelivery_Charges.SelectedValue = Convert.ToString(ime.Delivery_Charges);
                //ddlWarehouse_Specified.SelectedValue = Convert.ToString(ime.Warehouse_Specified);
                //txtBlackMarket_Password.Text = ime.BlackMarket_Password;
                //txtDoublePrice_Ctrl_No.Text = ime.DoublePrice_Ctrl_No;
                if (ime.Extra_Shipping != 0)
                    txtExtra_Shipping.Text = ime.Extra_Shipping.ToString();
                else txtExtra_Shipping.Text = "";
                txtmaker_code.Text = ime.Maker_Code;
                if (ime.Rakuten_CategoryID == "0")
                {
                    txtRakuten_CategoryID.Text = "";
                    txtRakuten_CategoryPath.Text = string.Empty;
                }
                else
                {
                    txtRakuten_CategoryID.Text = ime.Rakuten_CategoryID.ToString();
                    txtRakuten_CategoryPath.Text = ime.Rakuten_CategoryPath.ToString();
                }
                if (ime.Yahoo_CategoryID == "0")
                {
                    txtYahoo_CategoryID.Text = "";
                    txtYahoo_CategoryPath.Text = string.Empty;
                }
                else
                {
                    txtYahoo_CategoryID.Text = ime.Yahoo_CategoryID.ToString();
                    txtYahoo_CategoryPath.Text = ime.Yahoo_CategoryPath.ToString();
                }
                if (!String.IsNullOrWhiteSpace(ime.Wowma_CategoryID))
                {
                    if (ime.Wowma_CategoryID == "0")
                    {
                        txtWowma_CategoryID.Text = "";
                        txtWowma_CategoryPath.Text = string.Empty;
                    }
                    else
                    {
                        txtWowma_CategoryID.Text = ime.Wowma_CategoryID.ToString();
                        txtWowma_CategoryPath.Text = ime.Wowma_CategoryPath.ToString();
                    }
                }

                //hhw
                //if (!String.IsNullOrWhiteSpace(ime.Tennis_CategoryID))
                //{
                //    if (ime.Tennis_CategoryID == "0")
                //    {
                //        txtTennis_CategoryID.Text = "";
                //        txtTennis_CategoryPath.Text = string.Empty;
                //    }
                //    else
                //    {
                //        txtTennis_CategoryID.Text = ime.Tennis_CategoryID.ToString();
                //        txtTennis_CategoryPath.Text = ime.Tennis_CategoryPath.ToString();
                //    }
                //}
                //txtyahoourl.Text = ime.Yahoo_url; //for sks-593
                if (ime.SalesUnit != "")
                {
                    ddlsalesunit.Text = Convert.ToString(ime.SalesUnit);
                }
                else
                {
                    ddlsalesunit.Text = Convert.ToString(ime.SalesUnit);
                }
                if (ime.TagInformation != "") //update 3/6/2021
                {
                    //ddlTagInfo.Text = Convert.ToString(ime.TagInformation);
                }
                else
                {
                    //ddlTagInfo.Text = Convert.ToString(ime.TagInformation);
                }
                txtcontentquantityunitno1.Text = Convert.ToString(ime.ContentQuantityNo1);
                txtcontentquantityunitno2.Text = Convert.ToString(ime.ContentQuantityNo2);
                ddlcontentunit1.Text = Convert.ToString(ime.ContentUnit1);
                ddlcontentunit2.Text = Convert.ToString(ime.ContentUnit2);
                txtCatchCopy.Text = ime.PC_CatchCopy;
                //txtCatchCopyMobile.Text = ime.PC_CatchCopy_Mobile;
                txtmakername.Text = ime.Maker_Name.ToString();
                txtcomment.Text = ime.Comment;

                if (ime.Selling_Price == 0)
                    txtsellingprice.Text = string.Empty;
                else
                    txtsellingprice.Text = ime.Selling_Price.ToString();

                if (ime.Cost == 0)
                    txtcost.Text = string.Empty;
                else
                    txtcost.Text = string.Format("{0:#,#}", ime.Cost); ;

                if (ime.Purchase_Price == 0)
                    txtpurchaseprice.Text = string.Empty;
                else
                    txtpurchaseprice.Text = ime.Purchase_Price.ToString();

                if (ime.SellBy == 0)
                    txtsellby.Text = string.Empty;
                else
                    txtsellby.Text = ime.SellBy.ToString();
                if (!String.IsNullOrWhiteSpace(ime.PublicationType.ToString()))
                {
                    ddlPublicationType.SelectedIndex = Convert.ToInt32(ime.PublicationType);
                }
                else
                {
                    ddlPublicationType.SelectedIndex = 0;
                }

                txtminimumorderquantity.Text = ime.MinimumOrderSuu.ToString() ;
                txtminimumorderunit.Text = ime.MinimumOrderUnit.ToString();

                if (!String.IsNullOrWhiteSpace(ime.DirectDelivery.ToString()))
                {
                    ddlDirectDelivery.SelectedIndex = Convert.ToInt32(ime.DirectDelivery);
                }
                else
                {
                    ddlDirectDelivery.SelectedIndex = 0;
                }

                if (!string.IsNullOrWhiteSpace(ime.ScheduleReleaseDate.ToString()))
                {                  
                  
                     String datemono= String.Format("{0:yyyy/MM/dd}", ime.ScheduleReleaseDate);
                    txtreleasedatemonotaro.Text = datemono.Replace('-','/').ToString();
                }
                else
                {
                    txtreleasedatemonotaro.Text = "";
                }

                txtmonocategory.Text = ime.Categorymonotaro.ToString();
                txtcolour.Text = ime.Colormonotaro.ToString();
                txtReferenceURL.Text = ime.ReferenceURL.ToString();

                if (!String.IsNullOrWhiteSpace(ime.Procurement_Goods.ToString()))
                {
                    ddlSpecifiedprocurementitem.SelectedIndex = Convert.ToInt32(ime.Procurement_Goods);
                }
                else
                {
                    ddlSpecifiedprocurementitem.SelectedIndex = 0;
                }

                if (!String.IsNullOrWhiteSpace(ime.EcoMarkCertifiedGoods.ToString()))
                {
                    ddlecomartcertifiedproduct.SelectedIndex = Convert.ToInt32(ime.EcoMarkCertifiedGoods);
                }
                else
                {
                    ddlecomartcertifiedproduct.SelectedIndex = 0;
                }

                if (!String.IsNullOrWhiteSpace(ime.GreenPurchasingLaw.ToString()))
                {
                    ddlgreenpurchasemethod.SelectedIndex = Convert.ToInt32(ime.GreenPurchasingLaw);
                }
                else
                {
                    ddlgreenpurchasemethod.SelectedIndex = 0;
                }

                if (!String.IsNullOrWhiteSpace(ime.EcoMarkCertifiedNo.ToString()))
                {
                    txtecomartcertifiednumber.Text = ime.EcoMarkCertifiedNo.ToString() ;
                }
                else
                {
                    txtecomartcertifiednumber.Text = "";
                }

                if (!String.IsNullOrWhiteSpace(ime.RoHS_Directive.ToString()))
                {
                    ddlRoHSdirective.SelectedIndex = Convert.ToInt32(ime.RoHS_Directive);
                }
                else
                {
                    ddlRoHSdirective.SelectedIndex = 0;
                }

                if (!String.IsNullOrWhiteSpace(ime.Medical_Supplies.ToString()))
                {
                    ddlPharmaceuticalsandmedicaldevices.SelectedIndex = Convert.ToInt32(ime.Medical_Supplies);
                }
                else
                {
                    ddlPharmaceuticalsandmedicaldevices.SelectedIndex = 0;
                }
                if (!String.IsNullOrWhiteSpace(ime.JISConform.ToString()))
                {
                    ddlJISConform.SelectedIndex = Convert.ToInt32(ime.JISConform);
                }
                else
                {
                    ddlJISConform.SelectedIndex = 0;
                }
                if (!String.IsNullOrWhiteSpace(ime.ISOConform.ToString()))
                {
                    ddlISOConform.SelectedIndex = Convert.ToInt32(ime.ISOConform);
                }
                else
                {
                    ddlISOConform.SelectedIndex = 0;
                }

                txtsellingrank.Text = ime.Selling_Rank;
                ddldeliverymethod.SelectedValue = Convert.ToString(ime.Delivery_Method);
                ddldeliverytype.SelectedValue = Convert.ToString(ime.Delivery_Type);

                if (ime.Delivery_Days == 0)
                    txtdeliverydays.Text = string.Empty;
                else
                    txtdeliverydays.Text = Convert.ToString(ime.Delivery_Days);

                ddldeliveryfees.SelectedValue = Convert.ToString(ime.Delivery_Fees);
                ddlksmavaliable.SelectedValue = Convert.ToString(ime.KSMDelivery_Type);

                if (ime.KSMDelivery_Days == 0)
                    txtksmdeliverydays.Text = string.Empty;
                else
                    txtksmdeliverydays.Text = ime.KSMDelivery_Days.ToString();

                ddlreturnableitem.SelectedValue = Convert.ToString(ime.Returnable_Item);
                ddlnoapplicablelaw.SelectedValue = Convert.ToString(ime.NoApplicable_Law);
                ddlsalespermission.SelectedValue = Convert.ToString(ime.Sales_Permission);
                ddllaw.SelectedValue = Convert.ToString(ime.Law);

                if (ime.Nation_Wide == "0")
                    txtnationwide.Text = string.Empty;
                else
                    txtnationwide.Text = Convert.ToString(ime.Nation_Wide);

                if (ime.Hokkaido == 0)
                    txthokkaido.Text = string.Empty;
                else
                    txthokkaido.Text = Convert.ToString(ime.Hokkaido);

                if (ime.Okinawa == 0)
                    txtokinawa.Text = string.Empty;
                else
                    txtokinawa.Text = Convert.ToString(ime.Okinawa);

                if (ime.Remote_Island == 0)
                    txtremoteisland.Text = string.Empty;
                else
                    txtremoteisland.Text = Convert.ToString(ime.Remote_Island);

                txtundeliveredarea.Text = Convert.ToString(ime.Undelivered_Area);
                txtdangerousgoodscontents.Text = ime.Undelivered_Area.ToString();
                ddldanggoodsclass.SelectedValue = Convert.ToString(ime.Dangoods_Class);
                ddldanggoodsname.SelectedValue = Convert.ToString(ime.Dangoods_Name);
                ddlriskrating.SelectedValue = Convert.ToString(ime.Risk_Rating);
                ddldanggoodsnature.SelectedValue = Convert.ToString(ime.Dangoods_Nature);
                ddlfirelaw.SelectedValue = Convert.ToString(ime.Fire_Law);
                if (!String.IsNullOrWhiteSpace(ime.CostRate))
                {
                    txtcostrate.Text = ime.CostRate ;
                }
                if (!String.IsNullOrWhiteSpace(ime.ProfitRate))
                {
                    txtprofitrate.Text = ime.ProfitRate ;
                }
                if (!String.IsNullOrWhiteSpace(ime.DiscountRate))
                {
                    txtdiscountrate.Text = ime.DiscountRate ;
                }

                if (!String.IsNullOrWhiteSpace(ime.Jisha_costrate))
                {
                    txtjishaCostrate.Text = ime.Jisha_costrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Jisha_profitrate))
                {
                    txtjishaProfitrate.Text = ime.Jisha_profitrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Jisha_discountRate))
                {
                    txtjishaDiscountrate.Text = ime.Jisha_discountRate;
                }

                if (!String.IsNullOrWhiteSpace(ime.Rakuten_costrate))
                {
                    txtrakutenCostrate.Text = ime.Rakuten_costrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Rakuten_profitrate))
                {
                    txtrakutenProfitrate.Text = ime.Rakuten_profitrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Rakuten_discountRate))
                {
                    txtrakutenDiscountrate.Text = ime.Rakuten_discountRate;
                }

                if (!String.IsNullOrWhiteSpace(ime.Yahoo_costrate))
                {
                    txtyahooCostrate.Text = ime.Yahoo_costrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Yahoo_profitrate))
                {
                    txtyahooProfitrate.Text = ime.Yahoo_profitrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Yahoo_discountRate))
                {
                    txtyahooDiscountrate.Text = ime.Yahoo_discountRate;
                }

                if (!String.IsNullOrWhiteSpace(ime.Wowma_costrate))
                {
                    txtwowmaCostrate.Text = ime.Wowma_costrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Wowma_profitrate))
                {
                    txtwowmaProfitrate.Text = ime.Wowma_profitrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Wowma_discountRate))
                {
                    txtwowmaDiscountrate.Text = ime.Wowma_discountRate;
                }

                //monotaro

                if (!String.IsNullOrWhiteSpace(ime.Monocostrate))
                {
                    txtmonoprice_costrate.Text = ime.Monocostrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.MonoprofitRate))
                {
                    txtmonoprice_profitrate.Text = ime.MonoprofitRate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Monodiscountrate))
                {
                    txtmonoprice_discountrate.Text = ime.Monodiscountrate;
                }

                if (!String.IsNullOrWhiteSpace(ime.Ditecostrate))
                {
                    txtditeprice_costrate.Text = ime.Ditecostrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.DiteprofitRate))
                {
                    txtditeprice_profitrate.Text = ime.DiteprofitRate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Ditediscountrate))
                {
                    txtditeprice_discountrate.Text = ime.Ditediscountrate;
                }

                if (!String.IsNullOrWhiteSpace(ime.Japanmcostrate))
                {
                    txtjapanmprice_costrate.Text = ime.Japanmcostrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Japanmprofitrate))
                {
                    txtjapanmprice_profitrate.Text = ime.Japanmprofitrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Japanmdiscountrate))
                {
                    txtjapanmprice_discountrate.Text = ime.Japanmdiscountrate;
                }

                if (!String.IsNullOrWhiteSpace(ime.Kawashigicostrate))
                {
                    txtkashiwagi_costrate.Text = ime.Kawashigicostrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Kashiwagiprofitrate))
                {
                    txtkashiwagi_profitrate.Text = ime.Kashiwagiprofitrate;
                }
                if (!String.IsNullOrWhiteSpace(ime.Kashiwagionodiscountrate))
                {
                    txtkashiwagi_discountrate.Text = ime.Kashiwagionodiscountrate;
                }
                if (ime.Day_Ship == 0)
                    txtday_ship.Text = string.Empty;
                else
                    txtday_ship.Text = Convert.ToString(ime.Day_Ship);
                if (ime.Retrun_Necessary == 0)
                    txtreturn_necessary.Text = string.Empty;
                else
                    txtreturn_necessary.Text = Convert.ToString(ime.Retrun_Necessary);
                if (ime.Warehouse_Code == 0)
                    txtwarehouse_code.Text = string.Empty;
                else
                    txtwarehouse_code.Text = Convert.ToString(ime.Warehouse_Code);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public void BindContentUnit2()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindContentunit2();
            ddlcontentunit2.DataSource = dt;
            ddlcontentunit2.DataTextField = "Contents_unit_2";
            ddlcontentunit2.DataValueField = "Contents_unit_2";
            ddlcontentunit2.DataBind();
            ddlcontentunit2.Items.Insert(0, "");
        }

        //public void BindORSTag()
        //{
        //    imeBL = new Item_Master_BL();
        //    DataTable dt = imeBL.BindORSTag();
        //    ddlTagInfo.DataSource = dt;
        //    ddlTagInfo.DataTextField = "Name";
        //    ddlTagInfo.DataValueField = "Name";
        //    ddlTagInfo.DataBind();
        //    ddlTagInfo.Items.Insert(0, "");
        //}

        public void BindContentUnit1()
        {
            imeBL = new Item_Master_BL();
            DataTable dt = imeBL.BindContentunit1();
            ddlcontentunit1.DataSource = dt;
            ddlcontentunit1.DataTextField = "Contents_unit_1";
            ddlcontentunit1.DataValueField = "Contents_unit_1";
            ddlcontentunit1.DataBind();
            ddlcontentunit1.Items.Insert(0, "");
        }

        public void BindNormalLargeKBN()
        {
            //imeBL = new Item_Master_BL();
            //DataTable dt = imeBL.BindContentunit1();
            //ddlcontentunit1.DataSource = dt;
            //ddlcontentunit1.DataTextField = "Contents_unit_1";
            //ddlcontentunit1.DataValueField = "Contents_unit_1";
            //ddlcontentunit1.DataBind();
            ddNormalLargeKBN.Items.Insert(0, "");
            ddNormalLargeKBN.Items.Insert(1, "通常品");
            ddNormalLargeKBN.Items.Insert(2, "大型品");
        }

        public void BindddlPublicationType()
        {         
            ddlPublicationType.Items.Insert(0, "公開");
            ddlPublicationType.Items.Insert(1, "非公開");
        }

        public void BindDDlDirectDelivery()
        {
            ddlDirectDelivery.Items.Insert(0, "可");
            ddlDirectDelivery.Items.Insert(1, "不可");
        }

        public void Bindddlgreenpurchasemethod()
        {
            ddlgreenpurchasemethod.Items.Insert(0, "適合");
            ddlgreenpurchasemethod.Items.Insert(1, "非適合");
        }

        public void BindSpecifiedprocurementitem()
        {
            imeBL = new Item_Master_BL();
            DataTable dtProcurement_Goods = imeBL.BindProcurement_Goods();
            ddlSpecifiedprocurementitem.DataSource = dtProcurement_Goods;
            ddlSpecifiedprocurementitem.DataTextField = "Procurement_Goods_Name";
            ddlSpecifiedprocurementitem.DataValueField = "Procurement_Goods_ID";
            ddlSpecifiedprocurementitem.DataBind();

            //ddlSpecifiedprocurementitem.Items.Insert(0, "コピー用紙");
            //ddlSpecifiedprocurementitem.Items.Insert(1, "フォーム用紙");
            //ddlSpecifiedprocurementitem.Items.Insert(2, "インクジェットカラープリンター用塗工紙");
            //ddlSpecifiedprocurementitem.Items.Insert(3, "塗工されていない印刷用紙");
            //ddlSpecifiedprocurementitem.Items.Insert(4, "塗工されている印刷用紙");
            //ddlSpecifiedprocurementitem.Items.Insert(5, "トイレットペーパー");
            //ddlSpecifiedprocurementitem.Items.Insert(6, "トイレットペーパー");
            //ddlSpecifiedprocurementitem.Items.Insert(7, "ティッシュペーパー");         
        }

        public void Bindddlecomartcertifiedproduct()
        {
            ddlecomartcertifiedproduct.Items.Insert(0, "認定");
            ddlecomartcertifiedproduct.Items.Insert(1, "非認定");
        }
        public void BindddlRoHSdirective()
        {
            imeBL = new Item_Master_BL();
            DataTable dtRoHS_Directive = imeBL.BindRoHS_Directive();
            ddlRoHSdirective.DataSource = dtRoHS_Directive;
            ddlRoHSdirective.DataTextField = "RoHS_Directive_Name";
            ddlRoHSdirective.DataValueField = "RoHS_Directive_ID";
            ddlRoHSdirective.DataBind();

            //ddlRoHSdirective.Items.Insert(0, "10物質対応");
            //ddlRoHSdirective.Items.Insert(1, "6物質対応");
            //ddlRoHSdirective.Items.Insert(2, "対象外");
        }

        public void BindddlPharmaceuticalsandmedicaldevices()
        {
            imeBL = new Item_Master_BL();
            DataTable dtMedical_Supplies = imeBL.BindMedical_Supplies();
            ddlPharmaceuticalsandmedicaldevices.DataSource = dtMedical_Supplies;
            ddlPharmaceuticalsandmedicaldevices.DataTextField = "Medical_Supplies_Name";
            ddlPharmaceuticalsandmedicaldevices.DataValueField = "Medical_Supplies_ID";
            ddlPharmaceuticalsandmedicaldevices.DataBind();
            ddlPharmaceuticalsandmedicaldevices.Items.Insert(0, "");

            //ddlPharmaceuticalsandmedicaldevices.Items.Insert(0, " ");
            //ddlPharmaceuticalsandmedicaldevices.Items.Insert(1, "医薬品");
            //ddlPharmaceuticalsandmedicaldevices.Items.Insert(2, "医療機器");
        }

        public void BindddlJISConform()
        {
            ddlJISConform.Items.Insert(0, "非適合");
            ddlJISConform.Items.Insert(1, "適合");
        }
        public void BindddlISOConform()
        {
            ddlISOConform.Items.Insert(0, "非適合");
            ddlISOConform.Items.Insert(1, "適合");
        }

        public void BindMonotaroddl()
        {
            imeBL = new Item_Master_BL();
            DataTable dtdeliverymethod = imeBL.BindMonotaro("Delivery_Method");
            ddldeliverymethod.DataSource = dtdeliverymethod;
            ddldeliverymethod.DataTextField = "Delivery_Method";
            ddldeliverymethod.DataValueField = "Delivery_Method_ID";
            ddldeliverymethod.DataBind();

            DataTable dtdeliverytype = imeBL.BindMonotaro("Delivery_Type");
            ddldeliverytype.DataSource = dtdeliverytype;
            ddldeliverytype.DataTextField = "Delivery_Type_Name";
            ddldeliverytype.DataValueField = "Delivery_Type_ID";
            ddldeliverytype.DataBind();

            DataTable dtdeliveryfees = imeBL.BindMonotaro("COD");
            ddldeliveryfees.DataSource = dtdeliveryfees;
            ddldeliveryfees.DataTextField = "COD_Name";
            ddldeliveryfees.DataValueField = "COD_Value";
            ddldeliveryfees.DataBind();

            DataTable dtksmavaliable = imeBL.BindMonotaro("Customer_Delivery_Type");
            ddlksmavaliable.DataSource = dtksmavaliable;
            ddlksmavaliable.DataTextField = "Customer_Delivery_Type";
            ddlksmavaliable.DataValueField = "Customer_Delivery_Type_ID";
            ddlksmavaliable.DataBind();

            DataTable dtreturnableitem = imeBL.BindMonotaro("Return_Type");
            ddlreturnableitem.DataSource = dtreturnableitem;
            ddlreturnableitem.DataTextField = "Return_Type";
            ddlreturnableitem.DataValueField = "Return_Type_ID";
            ddlreturnableitem.DataBind();

            DataTable dtapplicablelaw = imeBL.BindMonotaro("Applicable_Law");
            ddlnoapplicablelaw.DataSource = dtapplicablelaw;
            ddlnoapplicablelaw.DataTextField = "Applicable_Law_Type";
            ddlnoapplicablelaw.DataValueField = "Applicable_Law_Type_ID";
            ddlnoapplicablelaw.DataBind();

            DataTable dtsalespermission = imeBL.BindMonotaro("Sales_Permission");
            ddlsalespermission.DataSource = dtsalespermission;
            ddlsalespermission.DataTextField = "Sale_Permission_Type";
            ddlsalespermission.DataValueField = "Sale_Permission_Type_ID";
            ddlsalespermission.DataBind();

            DataTable dtlaw = imeBL.BindMonotaro("Law");
            ddllaw.DataSource = dtlaw;
            ddllaw.DataTextField = "Law_And_Regulations_Type";
            ddllaw.DataValueField = "Law_And_Regulations_ID";
            ddllaw.DataBind();

            DataTable dtcustomerassembly = imeBL.BindMonotaro("Customer_Assembly");
            ddlcustomerassembly.DataSource = dtcustomerassembly;
            ddlcustomerassembly.DataTextField = "Customer_Assembly";
            ddlcustomerassembly.DataValueField = "Customer_Assembly_ID";
            ddlcustomerassembly.DataBind();

            DataTable dtfirelaw = imeBL.BindMonotaro("Fire_Service_Law");
            ddlfirelaw.DataSource = dtfirelaw;
            ddlfirelaw.DataTextField = "Fire_Service_Law";
            ddlfirelaw.DataValueField = "Fire_Service_Law_ID";
            ddlfirelaw.DataBind();

            DataTable dtdanggoodsclass = imeBL.BindMonotaro("Dangarous_Goods");
            ddldanggoodsclass.DataSource = dtdanggoodsclass;
            ddldanggoodsclass.DataTextField = "Dangarous_Goods";
            ddldanggoodsclass.DataValueField = "Dangarous_Goods_ID";
            ddldanggoodsclass.DataBind();

            DataTable dtdanggoodsname = imeBL.BindMonotaro("Dangerous_Goods_Name");
            ddldanggoodsname.DataSource = dtdanggoodsname;
            ddldanggoodsname.DataTextField = "Dangerous_Goods_Name";
            ddldanggoodsname.DataValueField = "Dangarous_Goods_Name_ID";
            ddldanggoodsname.DataBind();

            DataTable dtriskrating = imeBL.BindMonotaro("Risk_Rating");
            ddlriskrating.DataSource = dtriskrating;
            ddlriskrating.DataTextField = "Risk_Rating";
            ddlriskrating.DataValueField = "Risk_Rating_ID";
            ddlriskrating.DataBind();

            DataTable dtdanggoodsnature = imeBL.BindMonotaro("Dangerous_Goods_Nature");
            ddldanggoodsnature.DataSource = dtdanggoodsnature;
            ddldanggoodsnature.DataTextField = "Dangerous_Goods_Nature";
            ddldanggoodsnature.DataValueField = "Dangerous_Goods_Nature_ID";
            ddldanggoodsnature.DataBind();
        }







    }
}