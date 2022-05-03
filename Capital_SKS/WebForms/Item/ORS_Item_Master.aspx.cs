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
                   ////// BindShop(); //Bind Shop List'
                    if (ItemCode != null)    //Come from Item View for edit
                    {
                        //Change button name
                        btnSave.Text = "更新";
                        int ItemID = imeBL.SelectItemID(ItemCode);
                        ime = imeBL.SelectByID(ItemID);  //Select From Item_Master Table
                        SetItemData(ime);
                        SetSelectedShop(ItemID);             //Select From Item_Shop Table
                        SetSelectedCategory(ItemID);      //Select From Item_Category Table
                        SetCategoryData();
                     //   SetJishaCategoryData();
                        SelectByItemID(ItemID);                   
                        BindPhotoList();
                       // BindShopName();
                        //SetItemCodeURL();
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

            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }

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

        protected void ChangeNUll_To_Space()
        {
            
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
                DataTable dtskucolor = item.SelectSKUColor(ItemCode);
                //gvSKUColor.DataSource = item.SelectSKUColor(ItemCode); //Select From Item Table
                //gvSKUColor.DataBind();
                DataTable dtsku = new DataTable();
                DataTable dt = new DataTable();
                if (dtskucolor.Rows.Count > 0)
                {
                    dtsku = item.SelectSKU(ItemCode);
                    gvSKU.DataSource = item.SelectSKU(ItemCode); //Select From Item Table
                    gvSKU.DataBind();
                    //dt = item.SelectSKUSize(ItemCode); //Select From Item Table
                    //gvSKUSize.DataSource = dt;
                    //gvSKUSize.DataBind();
                    //rdb1.Checked = true;
                }
                else
                {
                    //rdb2.Checked = true;
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

        public void InsertPhoto(int itemID)
        {
            try
            {
                DataTable dtImage = ImageList as DataTable;
                Item_Image_BL itemImageBL = new Item_Image_BL();
                dtImage = SetLibraryPhoto(dtImage);
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
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?", false);
            }
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
                Item_Shop_BL itemShopBL = new Item_Shop_BL();
                DataTable dt = itemShopBL.SelectByItemID(itemID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        foreach (DataListItem li in dlShop1.Items)
                        {
                            Label lbl = li.FindControl("lblMall1ShopID") as Label;
                            CheckBox cb = li.FindControl("ckbMall1Shop") as CheckBox;
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
                txtProduct_Code.Text = ime.Product_Code;
                if (!string.IsNullOrWhiteSpace(ime.Release_Date.ToString()))
                {
                    txtRelease_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Release_Date);
                }
                else
                {
                    txtRelease_Date.Text = "";
                }
                if (!string.IsNullOrWhiteSpace(ime.Post_Available_Date.ToString()))   //updated by nandar 05/01/2016
                {
                    txtPost_Available_Date.Text = String.Format("{0:yyyy/MM/dd}", ime.Post_Available_Date);
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
                   // lblcostrate.Text = ime.CostRate + "%";
                }
                if (!String.IsNullOrWhiteSpace(ime.ProfitRate))
                {
                   // lblprofitrate.Text = ime.ProfitRate + "%";
                }
                if (!String.IsNullOrWhiteSpace(ime.DiscountRate))
                {
                    //lbldiscountrate.Text = ime.DiscountRate + "%";
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