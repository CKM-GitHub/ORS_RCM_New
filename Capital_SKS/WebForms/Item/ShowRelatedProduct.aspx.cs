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
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    DataTable dt1 = (DataTable)Session["Related_Item_Code"];
                    DataTable dt = (DataTable)Session["Item_Code"];
                    if (dt == null)
                    {
                        ArrayList arrlst = new ArrayList(); 
                        
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                string Related_ItemCode = dt1.Rows[i]["Related_ItemCode"].ToString();
                                arrlst.Add(Related_ItemCode);
                            }
                        
                        ViewState["checkedValue"] = arrlst;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ArrayList arrlst = new ArrayList();
                       
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string Item_Code = dt.Rows[i]["Item_Code"].ToString();
                                arrlst.Add(Item_Code);
                            }
                        
                        ViewState["checkedValue"] = arrlst;
                    }
                    gvMallCategory.DataSource = SelectByItemCode();
                    gvMallCategory.DataBind();
                }
              
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        public DataTable SelectByItemCode()
        {
            try
            {
                DataTable dt = new DataTable();
                Item_Master_BL ItemMasterBL = new Item_Master_BL();
                dt = ItemMasterBL.SelectByItemCode();
                return dt;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
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
                
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    if (!chk.Checked)
                    {
                        //if one of check box is unchecked then header checkbox set to uncheck
                        if (arrlst.Contains(lbl.Text))
                        {
                            arrlst.Remove(lbl.Text);
                            ViewState["checkedValue"] = arrlst;

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
                    ArrayList arrlst = new ArrayList();
                    arrlst.Add(lbl.Text);
                    ViewState["checkedValue"] = arrlst;
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
                    //checked id list
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    CheckBox chk;
                    Label lbl;

                    //set checkbox as check when gridview row's id is contain in checked id arraylist
                    for (int i = 0; i < gvMallCategory.Rows.Count; i++)
                    {
                        chk = gvMallCategory.Rows[i].FindControl("ckItem") as CheckBox;
                        lbl = gvMallCategory.Rows[i].FindControl("lblItem_Code") as Label;

                        if (arrlst.Contains(lbl.Text))
                            chk.Checked = true;
                        else chk.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
        protected void gvMallCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt1 = (DataTable)Session["Related_Item_Code"];
            DataTable dt = (DataTable)Session["Item_Code"];
            if (dt == null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = e.Row.FindControl("lblItem_Code") as Label;
                    for (int m = 0; m < dt1.Rows.Count; m++)
                    {
                        CheckBox chkbox = (CheckBox)e.Row.FindControl("ckItem");
                        if (dt1.Rows[m]["Related_ItemCode"].ToString() == lbl.Text.ToString())
                        {
                            chkbox.Checked = true;
                        }
                    }
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = e.Row.FindControl("lblItem_Code") as Label;
                    for (int m = 0; m < dt.Rows.Count; m++)
                    {
                        CheckBox chkbox = (CheckBox)e.Row.FindControl("ckItem");
                        if (dt.Rows[m]["Item_Code"].ToString() == lbl.Text.ToString())
                        {
                            chkbox.Checked = true;
                        }
                    }
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gvMallCategory.DataSource = Search();
                gvMallCategory.DataBind();
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
                else if ((string.IsNullOrWhiteSpace(txtSearch.Text)) || (string.IsNullOrWhiteSpace(txtSearch1.Text)))
                {
                    dt = ItemMasterBL.SelectByItemCode();
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
                dt.Columns.Add("Item_Name", typeof(String));
                DataRow dr = dt.NewRow();
                if (ViewState["checkedValue"] != null)
                {
                    ArrayList arrlst = ViewState["checkedValue"] as ArrayList;
                    //foreach (GridViewRow row in gvMallCategory.Rows)
                    //{
                    //    if (row.RowType == DataControlRowType.DataRow)
                    //    {
                    //        Label lblItem_Code = (Label)row.FindControl("lblItem_Code");
                    //        Label lblItem_Name = (Label)row.FindControl("lblItem_Name");
                    //        CheckBox chk = (CheckBox)row.FindControl("ckItem") as CheckBox;
                    //        if (arrlst.Contains(lblItem_Code.Text))
                    //            chk.Checked = true;
                    //        if (((CheckBox)row.FindControl("ckItem")).Checked)
                    //        {
                    foreach (string item in arrlst)

                    {
                        dr = dt.NewRow();
                        dr["Item_Code"] = item.ToString();
                        dt.Rows.Add(dr);

                    }
                            //}
                            Session["Item_Code" + Item_Code] = dt;
                            Session["btnRelatedbtn_" + Item_Code] = "ok";
                    //}
                    //}
                }

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