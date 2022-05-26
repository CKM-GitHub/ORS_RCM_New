using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;
using System.Collections;

namespace Capital_SKS.WebForms.Item
{
    public partial class ShowRelatedProduct : System.Web.UI.Page
    {
        public string Item_Code
        {
            get
            {
                if (Request.QueryString["Item_Code"] != null)
                {
                    return Request.QueryString["Item_Code"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public DataTable Related_Item_Code
        {
            get
            {
                if (Session["Related_Item_Code" + Item_Code] != null)
                {
                    DataTable dt = (DataTable)Session["Related_Item_Code" + Item_Code];
                    return dt;
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
                if (Session["relItem_Code" + Item_Code] != null)
                {
                    DataTable dt = (DataTable)Session["relItem_Code" + Item_Code];
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    gvMallCategory.DataBind();
                }
                else
                {
                    gvMallCategory.DataSource = Search();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void gvMallCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMallCategory.DataSource = Search();
                gvMallCategory.PageIndex = e.NewPageIndex;
                gvMallCategory.DataBind();
                ItemCheck_Change();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void chkItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;
                GridViewRow row = chk.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;
                Label lbl = gvMallCategory.Rows[rowIndex].FindControl("lblItem_Code") as Label;
                ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                if (ViewState["checkedValue"] != null && arrlst != null)
                {
                    int c = 0;
                    if (!unCheck.Checked)
                    {
                        if (relItem_Code == null)
                            c = 20 - Related_Item_Code.Rows.Count;
                        else
                            c = 20 - relItem_Code.Rows.Count;
                    }
                    else 
                        c = 20 ;

                    if (arrlst.Count < c)
                        {
                            if (!chk.Checked)
                            {
                                //if one of check box is unchecked then header checkbox set to uncheck
                                if (arrlst.Contains(lbl.Text))
                                {
                                    arrlst.Remove(lbl.Text);
                                    ViewState["checkedValue"] = arrlst;
                                    text.Text = "";
                                }
                            }
                            else
                            {
                                arrlst.Add(lbl.Text);
                                ViewState["checkedValue"] = arrlst;
                            }
                        }
                        else
                        {
                            if (!chk.Checked)
                            {
                                //if one of check box is unchecked then header checkbox set to uncheck
                                if (arrlst.Contains(lbl.Text))
                                {
                                    arrlst.Remove(lbl.Text);
                                    ViewState["checkedValue"] = arrlst;
                                    text.Text = "";
                                }
                            }
                            else
                            {
                                chk.Checked = false;
                                text.Text = "関連商品の数が報大値を超えています。";
                            }
                        }
                }
                else
                {
                    ArrayList arrlst1 = new ArrayList();
                    arrlst1.Add(lbl.Text);
                    ViewState["checkedValue"] = arrlst1;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void ItemCheck_Change()
        {
            try
            {
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    CheckBox chk;
                    Label lbl;
                    for (int i = 0; i < gvMallCategory.Rows.Count; i++)
                    {
                        chk = gvMallCategory.Rows[i].FindControl("ckItem") as CheckBox;
                        lbl = gvMallCategory.Rows[i].FindControl("lblItem_Code") as Label;

                        if (arrlst.Contains(lbl.Text))
                            chk.Checked = true;
                        else
                            chk.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gvMallCategory.DataSource = Search();
                gvMallCategory.DataBind();
                ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                arrlst = null;
                ViewState["checkedValue"] = arrlst;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        public DataTable Search()
        {
            try
            {
                DataTable dt = new DataTable();
                Item_Master_BL ItemMasterBL = new Item_Master_BL();
                ItemMasterBL = new Item_Master_BL();
                if ((!string.IsNullOrWhiteSpace(txtSearch.Text)) || (!string.IsNullOrWhiteSpace(txtSearch1.Text)))
                {
                    dt = ItemMasterBL.SearchRelatedItem(txtSearch.Text.Trim(), txtSearch1.Text.Trim());
                    return dt;
                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }
        protected void btn_Close(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Item_Code", typeof(String));
                DataRow dr = dt.NewRow();
                ArrayList arrlst = ViewState["checkedValue"] as ArrayList;                                
                if (ViewState["checkedValue"] != null)
                {
                    if (!unCheck.Checked)
                    {
                        if (relItem_Code == null && Related_Item_Code != null)
                        {
                            for (int i = 0; i < Related_Item_Code.Rows.Count; i++)
                            {
                                string Related_ItemCode = Related_Item_Code.Rows[i]["Related_ItemCode"].ToString();
                                if (!arrlst.Contains(Related_ItemCode))
                                    arrlst.Add(Related_ItemCode);
                            }
                            ViewState["checkedValue"] = arrlst;
                        }
                        if (relItem_Code != null && relItem_Code.Rows.Count > 0)
                        {
                            for (int i = 0; i < relItem_Code.Rows.Count; i++)
                            {
                                string Item_Code = relItem_Code.Rows[i]["Item_Code"].ToString();
                                if (!arrlst.Contains(Item_Code))
                                    arrlst.Add(Item_Code);
                                
                            }
                            ViewState["checkedValue"] = arrlst;
                        }
                    }
                    foreach (string item in arrlst)
                    {
                        dr = dt.NewRow();
                        dr["Item_Code"] = item.ToString();
                        dt.Rows.Add(dr);
                    }
                }                   
                Session["relItem_Code" + Item_Code] = dt;
                Session["btnRelatedbtn_" + Item_Code] = "ok";

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack();window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void btn_Cancel(object sender, EventArgs e)
        {
            try
            {
                Session["btnRelatedbtn_" + Item_Code] = "cancel";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}