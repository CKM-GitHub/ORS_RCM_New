﻿/* 
Created By              :Aye Aye Mon
Created Date          : 19/06/2014
Updated By             :Kay Thi Aung
Updated Date         :31/07/2014

 Tables using: Category
    -
    -
    -
*/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ORS_RCM_BL;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace ORS_RCM.WebForms.Item
{
    public partial class PopupCatagoryList_Test : System.Web.UI.Page
    {
        Category_BL cbl;
        public int index = 0;
        public String[] ids = new String[100];
        public int extract = 0;
        public String[] ex = new String[6];
        string treepath = string.Empty;
        DataTable dt = new DataTable();
        public String[] cx = new String[100];
        public int parid;
        string catpath = string.Empty;
        String[] stpid = new String[100];
        String[] searchcatdesc = new String[5];
        string fdesc = string.Empty; string sdesc = string.Empty; string tdesc = string.Empty;
        string fourdesc = string.Empty; string fivedesc = string.Empty;
        DataTable Maindt = new DataTable();
        // Session Variable

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

        public DataTable CategoryList
        {
            get
            {
                if (Session["CategoryList_" + Item_Code] != null)
                {
                    DataTable dt = (DataTable)Session["CategoryList_" + Item_Code];
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
                    //ParentNode();
                    parendnote();
                    cbl = new Category_BL();
                    ////gvSelectedCatagory.DataSource = cbl.ForTreeviewSearch(0);
                    ////gvSelectedCatagory.DataBind();

                    //if (dt != null && dt.Rows.Count > 0)
                    if (Request.QueryString["ID"] != null)
                    {
                        if (CategoryList != null)
                        {
                            ViewState["CurrentTable"] = CategoryList;
                            gvSelectedCatagory.DataSource = CategoryList;
                            gvSelectedCatagory.DataBind();
                        }

                        //else
                        //{
                        //    String itemID = Request.QueryString["ID"].ToString();
                        //    Item_Category_BL icbl = new Item_Category_BL();
                        //    dt = icbl.SelectByItemID(Convert.ToInt32(itemID));

                        //    DataTable dtCategory = new DataTable();
                        //    dtCategory.Columns.Add("Category_SN", typeof(String));
                        //    dtCategory.Columns.Add("CID", typeof(String));
                        //    dtCategory.Columns.Add("CName", typeof(String));

                        //    int j = 0;
                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        dtCategory.Rows.Add();

                        //        dtCategory.Rows[j]["CName"] = getCategory(dt.Rows[i]["CID"].ToString(), String.Empty);
                        //        dtCategory.Rows[j]["CID"] = dt.Rows[i]["CID"].ToString();
                        //        dtCategory.Rows[j]["Category_SN"] = dt.Rows[i]["Category_SN"].ToString();
                        //        j++;
                        //    }


                        //    gvSelectedCatagory.DataSource = dtCategory;
                        //    gvSelectedCatagory.DataBind();

                        //    ViewState["CurrentTable"] = dtCategory;
                        //}
                    }
                    else if (CategoryList != null)
                    {
                        ViewState["CurrentTable"] = CategoryList;
                        gvSelectedCatagory.DataSource = CategoryList;
                        gvSelectedCatagory.DataBind();
                    }
                    else
                    {
                        DataTable dt = cbl.SearchShopTree(txtcatid.Text.Trim(), txtdesc.Text.Trim());
                        gvSelectedCatagory.DataSource = dt;
                        gvSelectedCatagory.DataBind();
                    }
                    SetPreviousData();

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected String getCategory(String id, String category)
        {
            try
            {
                Category_BL cbl = new Category_BL();
                DataTable dt = cbl.SelectForCatalogID(Convert.ToInt32(id));
                if (dt.Rows.Count > 0)
                {
                    if (String.IsNullOrWhiteSpace(category))
                        category = dt.Rows[0]["Description"].ToString();
                    else if (!String.IsNullOrWhiteSpace(category) && !dt.Rows[0]["ParentID"].ToString().Equals("0"))
                        category = dt.Rows[0]["Description"].ToString() + "\\" + category;

                    if (!dt.Rows[0]["ParentID"].ToString().Equals("0"))
                    {
                        category = getCategory(dt.Rows[0]["ParentID"].ToString(), category);
                    }
                }
                return category;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return string.Empty;
            }
        }

        protected String getCategory(String id, String category, String serial, String result)
        {
            try
            {
                Category_BL cbl = new Category_BL();
                DataTable dt = cbl.SelectForCatalogID(Convert.ToInt32(id));
                if (dt.Rows.Count > 0)
                {
                    if (String.IsNullOrWhiteSpace(category))
                    {
                        category = dt.Rows[0]["CName"].ToString();
                        serial = dt.Rows[0]["Category_SN"].ToString();
                    }
                    else
                    {
                        category = dt.Rows[0]["CName"].ToString() + "\\" + category;
                        serial = dt.Rows[0]["Category_SN"].ToString() + "\\" + serial;
                    }

                    if (!dt.Rows[0]["ParentID"].ToString().Equals("0"))
                    {
                        result = getCategory(dt.Rows[0]["ParentID"].ToString(), category, serial, result);
                        String[] strarr = new String[2];
                        strarr = result.Split(',');
                        category = strarr[0];
                        serial = strarr[1];
                    }
                }
                return category + "," + serial;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

        /// <summary>
        /// Connect to Category Table to display Parent Node
        /// </summary>
        protected void ParentNode()
        {
            try
            {
                tvCategory.Nodes.Clear();
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForTreeview(0);
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        TreeNode parentNode = new TreeNode();
                        parentNode.Text = dt.Rows[i]["Description"].ToString();
                        parentNode.Value = dt.Rows[i]["ID"].ToString();
                        parentNode.SelectAction = TreeNodeSelectAction.Select;

                        tvCategory.Nodes.Add(parentNode);
                        AddChildNode(parentNode);
                        i++;
                    }
                }
                tvCategory.CollapseAll();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// Connect to Category Table to display Child Nodes from Parent nodes
        /// </summary>
        /// <param name="parentNode"></param>
        protected void AddChildNode(TreeNode parentNode)
        {
            try
            {
                DataTable dt = cbl.ForTreeviewSearch(Convert.ToInt32(parentNode.Value));
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Text = dt.Rows[i]["CName"].ToString();
                        childNode.Value = dt.Rows[i]["ID"].ToString();
                        childNode.SelectAction = TreeNodeSelectAction.Select;
                        parentNode.ChildNodes.Add(childNode);
                        AddChildNode(childNode);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btntreecontrol_Click(object sender, EventArgs e)
        {
            try
            {
                if (btntreecontrol.Text == "Expand")
                {
                    tvCategory.ExpandAll();
                    btntreecontrol.Text = "Collapse";
                }

                else
                {
                    tvCategory.CollapseAll();
                    btntreecontrol.Text = "Expand";
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void tvCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                treepath = string.Empty;
                SetRow(tvCategory.SelectedNode.Value);
                SetPreviousData();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// To display Category with their path in gridview
        /// </summary>
        /// <param name="id">To keep category id</param>
        private void SetRow(string id)
        {
            try
            {
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("CID", typeof(int)));
                dt.Columns.Add(new DataColumn("CName", typeof(string)));
                dt.Columns.Add(new DataColumn("Category_SN", typeof(int)));
                dr = dt.NewRow();
                dr["CID"] = (int.Parse(id));
                dr["CName"] = ShowHierarchy(int.Parse(id));
                //dr["Serial_No"] = ShowSLHierarchy(int.Parse(id));
                dr["Category_SN"] = 1;

                dt.Rows.Add(dr);
                //Store the DataTable in ViewState
                if (ViewState["CurrentTable"] == null)
                {
                    ViewState["CurrentTable"] = dt;
                    gvSelectedCatagory.DataSource = dt;
                    gvSelectedCatagory.DataBind();
                }
                else
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    dtCurrentTable.Merge(dt, true, MissingSchemaAction.Ignore);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    gvSelectedCatagory.DataSource = dtCurrentTable;
                    gvSelectedCatagory.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// To display after changing in gridview 
        /// </summary>
        private void SetPreviousData()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = rowIndex; i < dt.Rows.Count; i++)
                        {
                            Label lblID = (Label)gvSelectedCatagory.Rows[rowIndex].Cells[1].FindControl("lblID");
                            Label lblValue = (Label)gvSelectedCatagory.Rows[rowIndex].Cells[1].FindControl("lbldesc");
                            TextBox txtsno = (TextBox)gvSelectedCatagory.Rows[rowIndex].Cells[1].FindControl("txtserial");
                            lblID.Text = dt.Rows[i]["CID"].ToString();
                            lblValue.Text = dt.Rows[i]["CName"].ToString();
                            txtsno.Text = dt.Rows[i]["Category_SN"].ToString();
                            rowIndex++;
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

        protected void gvSelectedCatagory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //int ID = int.Parse(gvSelectedCatagory.DataKeys[e.RowIndex].Value.ToString());
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                dt.Rows.RemoveAt(e.RowIndex);
                dt.AcceptChanges();
                gvSelectedCatagory.DataSource = dt;
                gvSelectedCatagory.DataBind();
                ViewState["CurrentTable"] = dt;
                SetPreviousData();
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                for (int i = 0; i < gvSelectedCatagory.Rows.Count; i++)
                {
                    TextBox txtserial = (TextBox)gvSelectedCatagory.Rows[i].FindControl("txtSerial");
                    if (!String.IsNullOrEmpty(txtserial.Text))
                    {
                        dt.Rows[i]["Category_SN"] = txtserial.Text;
                    }
                }
                dt.DefaultView.Sort = "Category_SN ASC";
                dt = dt.DefaultView.ToTable();

                if (Request.QueryString["row"] != null)
                {
                    String row = Request.QueryString["row"].ToString();
                    DataColumn col = new DataColumn();
                    col.ColumnName = "Index";
                    col.DefaultValue = row;
                    if (dt.Rows.Count == 0)
                        dt.Rows.Add();
                    dt.Columns.Add(col);
                }

                Session["CategoryList_" + Item_Code] = dt;
                Session["btnPopClick_" + Item_Code] = "ok";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.opener.__doPostBack('ShopCategory','');window.close()", true);
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        /// <summary>
        /// To display Category path 
        /// </summary>
        /// <param name="CID">select by Category id</param>
        /// <returns>string of path</returns>
        public string ShowHierarchy(int CID)
        {
            try
            {
                cbl = new Category_BL();
                ids[index++] = CID.ToString();
                GetCategory(CID);

                int i = 0;
                while (!String.IsNullOrWhiteSpace(ids[i]) && ids[i].ToString() != "1")
                {
                    DataTable dts = cbl.SelectForCatalogID(Convert.ToInt32(ids[i++]));
                    if (dts != null && dts.Rows.Count > 0)
                    {
                        if (dts.Rows.Count > 0)
                        {
                            ex[extract++] = dts.Rows[0]["Description"].ToString();
                            // lblparnode.Text += dts.Rows[0]["Description"].ToString() + ",";
                        }
                    }
                }

                for (int x = extract - 1; x >= 0; x--)
                {
                    if (x > 0)
                    {
                        treepath += ex[x] + "\\";
                    }
                    else if (x == 0)
                    {
                        treepath += ex[x];
                    }
                }

                return treepath;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return String.Empty;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Session["btnPopClick_" + Item_Code] = "cancel";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
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
                tvCategory.Nodes.Clear();
                if (!String.IsNullOrWhiteSpace(txtcatid.Text) || !String.IsNullOrWhiteSpace(txtdesc.Text))
                {
                    ViewState["IDdt"] = null;
                    cbl = new Category_BL();
                    Item_Category_BL icbl = new Item_Category_BL();
                    string description = txtdesc.Text.Trim();
                    string replaceWith = " ";
                    string line2 = Regex.Replace(description, @"　", replaceWith);
                    searchcatdesc = null;
                    searchcatdesc = line2.Split(' ');
                    for (int y = 0; y < searchcatdesc.Count(); y++)
                    {
                        if (y == 0)
                            fdesc = searchcatdesc[y];
                        else if (y == 1)
                            sdesc = searchcatdesc[y];
                        else if (y == 2)
                            tdesc = searchcatdesc[y];
                        else if (y == 3)
                            fourdesc = searchcatdesc[y];
                        else if (y == 4)
                            fivedesc = searchcatdesc[y];
                        else
                            break;
                    }
                    DataTable dt = cbl.Search(txtcatid.Text.Trim(), fdesc, sdesc, tdesc, fourdesc, fivedesc);

                    Maindt.Columns.Add("ID", typeof(int));
                    Maindt.Columns.Add("ParentID", typeof(int));
                    Maindt.Columns.Add("Description", typeof(String));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            //Maindt.Merge(icbl.getAllParentsbyName(txtdesc.Text.Trim()));
                            Maindt.Merge(icbl.getAllParentsbyName(Convert.ToInt32(dt.Rows[j]["ParentID"].ToString()), fdesc, sdesc, tdesc, fourdesc, fivedesc));
                        }
                    }

                    Maindt = RemoveDuplicateRows(Maindt, "ID");

                    DataRow[] dr = Maindt.Select("ParentID=1");
                    if (dr.Count() > 0)
                    {
                        DataTable dtParent = Maindt.Select("ParentID=1").CopyToDataTable();
                        for (int i = 0; i < dtParent.Rows.Count; i++)
                        {
                            TreeNode parentNode = new TreeNode();

                            parentNode.Text = dtParent.Rows[i]["Description"].ToString();
                            parentNode.Value = dtParent.Rows[i]["ID"].ToString();
                            parentNode.SelectAction = TreeNodeSelectAction.Select;

                            tvCategory.Nodes.Add(parentNode);
                            AddChildNoteSearch(parentNode, Maindt);
                        }
                    }
                    #region
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    #region For multiple similar data search
                    //    if (dt.Rows.Count > 1)
                    //    {


                    //        for (int t = 0; t < dt.Rows.Count; t++)
                    //        {
                    //            //parid = (int)dt.Rows[t]["ParentID"];

                    //                searchparentid += dt.Rows[t]["ParentID"].ToString() + ',';

                    //        }

                    //        TreeNode parentNode = new TreeNode();

                    //        parentNode.Text = dt.Rows[0]["Description"].ToString();
                    //        parentNode.Value = dt.Rows[0]["ID"].ToString();

                    //        parentNode.SelectAction = TreeNodeSelectAction.Select;
                    //        int parentids = (int)dt.Rows[0]["ParentID"];
                    //            if (parentids == 1)
                    //            {
                    //                tvCategory.Nodes.Clear();

                    //                tvCategory.Nodes.Add(parentNode);
                    //                AddChildNode(parentNode);

                    //            }
                    //       // stpid= searchparentid.Split(',');
                    //        string[] arr = searchparentid.Split(',');
                    //        arr = arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    //   //for (int k = 0; k < arr.Length; k++)
                    //  for (int k = arr.Length-1; k >=0; k--)
                    //        {
                    //            parid = Convert.ToInt32(arr[k]);

                    //      if (parid == 1)
                    //        {
                    //            tvCategory.Nodes.Clear();

                    //            tvCategory.Nodes.Add(parentNode);
                    //            AddChildNode(parentNode);
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            index = 0;
                    //            ids = new String[100];
                    //            tvCategory.Nodes.Clear();
                    //            int ID = parid;
                    //            ids[index++] = ID.ToString();
                    //            GetCategory(ID);
                    //            int x = 0;
                    //            while (!String.IsNullOrWhiteSpace(ids[x]))
                    //            {
                    //                DataTable dtc = cbl.SelectForTreeview(Convert.ToInt32(ids[x++]));

                    //                DataTable dts = cbl.SelectForCatalogID((int)dtc.Rows[0]["ParentID"]);
                    //                if (dts != null && dts.Rows.Count > 0)
                    //                {

                    //                        if ((int)dts.Rows[0]["ParentID"] == 1)
                    //                        {


                    //                           parentNode.Text = dts.Rows[0]["Description"].ToString();
                    //                            parentNode.Value = dts.Rows[0]["ID"].ToString();
                    //                           parentNode.SelectAction = TreeNodeSelectAction.Select;

                    //                            tvCategory.Nodes.Add(parentNode);
                    //                            AddChildNode(parentNode);
                    //                        }



                    //                }
                    //            }
                    //        }

                    //        }//for k

                    //     //  }//for dt condition
                    //    #endregion
                    //    }//if condition
                    //    #region
                    //    else
                    //    {
                    //        tvCategory.Nodes.Clear();
                    //        int i = 0;
                    //        while (i < dt.Rows.Count)
                    //        {
                    //            TreeNode parentNode = new TreeNode();

                    //            parentNode.Text = dt.Rows[i]["Description"].ToString();
                    //            parentNode.Value = dt.Rows[i]["ID"].ToString();
                    //            parid = (int)dt.Rows[i]["ParentID"];
                    //            parentNode.SelectAction = TreeNodeSelectAction.Select;

                    //            if (parid == 1)
                    //            {
                    //                tvCategory.Nodes.Add(parentNode);
                    //                AddChildNode(parentNode);
                    //            }
                    //            else if (parid == 0)
                    //            {
                    //                tvCategory.Nodes.Add(parentNode);
                    //                AddChildNode(parentNode);
                    //            }
                    //            else
                    //            {
                    //                #region
                    //                #endregion

                    //                tvCategory.Nodes.Clear();
                    //                int ID = parid;
                    //                ids[index++] = ID.ToString();
                    //                GetCategory(ID);
                    //                int x = 0;
                    //                while (!String.IsNullOrWhiteSpace(ids[x]))
                    //                {
                    //                    DataTable dtc = cbl.ForTreeviewSearch(Convert.ToInt32(ids[x++]));

                    //                    DataTable dts = cbl.SelectForCatalogID((int)dtc.Rows[0]["ParentID"]);
                    //                    if (dts != null && dts.Rows.Count > 0)
                    //                    {
                    //                        if ((int)dts.Rows[0]["ParentID"] == 0)
                    //                        {
                    //                            parentNode.Text = dts.Rows[0]["Description"].ToString();//change
                    //                            parentNode.Value = dts.Rows[0]["ID"].ToString();
                    //                            parentNode.SelectAction = TreeNodeSelectAction.Select;
                    //                            tvCategory.Nodes.Add(parentNode);
                    //                            AddChildNode(parentNode);
                    //                        }

                    //                    }
                    //                }

                    //            }
                    //            i++;
                    //        }

                    //    }
                    //    #endregion
                    //    tvCategory.ExpandAll();
                    //    //gvSelectedCatagory.DataSource = cbl.ForTreeviewSearch(parid);
                    //    //gvSelectedCatagory.DataBind();


                    //}
                    //else
                    //{
                    //    tvCategory.Nodes.Clear();
                    //    //gvSelectedCatagory.DataSource = null;
                    //    //gvSelectedCatagory.DataBind();
                    //}
                    #endregion
                    ViewState["IDdt"] = Maindt;
                }//first if condition
                else
                {
                    ParentNode();
                    //gvSelectedCatagory.DataSource = null;
                    //gvSelectedCatagory.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }


        public void AddChildNoteSearch(TreeNode parentNode, DataTable Maindt)
        {
            try
            {
                DataRow[] dr = Maindt.Select("ParentID=" + parentNode.Value);
                if (dr.Count() > 0)
                {
                    int i = 0;
                    DataTable dt = Maindt.Select("ParentID=" + parentNode.Value).CopyToDataTable();
                    while (i < dt.Rows.Count)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Text = dt.Rows[i]["Description"].ToString();
                        childNode.Value = dt.Rows[i]["ID"].ToString();
                        childNode.SelectAction = TreeNodeSelectAction.Select;
                        parentNode.ChildNodes.Add(childNode);
                        AddChildNoteSearch(childNode, Maindt);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            try
            {
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();

                //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                //And add duplicate item value in arraylist.
                foreach (DataRow drow in dTable.Rows)
                {
                    if (hTable.Contains(drow[colName]))
                        duplicateList.Add(drow);
                    else
                        hTable.Add(drow[colName], string.Empty);
                }

                //Removing a list of duplicate items from datatable.
                foreach (DataRow dRow in duplicateList)
                    dTable.Rows.Remove(dRow);

                //Datatable which contains unique records will be return as output.
                return dTable;
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
                return new DataTable();
            }
        }

        protected void GetCategory(int id)
        {
            try
            {
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForCatalogID(id);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ParentID"].ToString() != "0")
                    {
                        ids[index++] = dt.Rows[i]["ParentID"].ToString();
                        GetCategory(Convert.ToInt32(dt.Rows[i]["ParentID"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        //protected void btnupdatenode_Click(object sender, EventArgs e)
        //{


        //}

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{

        //}


        //protected TreeNode SearchNode(string SearchText, TreeNode StartNode)
        //{
        //    TreeNode node = null;
        //    while (StartNode != null)
        //    {
        //        if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
        //        {
        //            node = StartNode;
        //            break;
        //        };
        //        if (StartNode.Nodes.Count != 0)
        //        {
        //            node = SearchNode(SearchText, StartNode.Nodes[0]);//Recursive Search
        //            if (node != null)
        //            {
        //                break;
        //            };
        //        };
        //        StartNode = StartNode.NextNode;
        //    };
        //    return node;
        //}

        //protected void button1_Click(object sender, EventArgs e)
        //{
        //    string SearchText = this.txtdesc.Text;
        //    if (SearchText == "")
        //    {
        //        return;
        //    };
        //    TreeNode SelectedNode = SearchNode(SearchText, tvCategory.Nodes[0]);
        //    if (SelectedNode != null)
        //    {
        //        this.tvCategory.SelectedNode = SelectedNode;
        //        this.tvCategory.SelectedNode.Expand();
        //        this.tvCategory.Select();
        //    };
        //}

        protected void parendnote()
        {
            try
            {
                tvCategory.Nodes.Clear();
                cbl = new Category_BL();
                DataTable dt = cbl.SelectForTreeview(0);
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    while (i < dt.Rows.Count)
                    {
                        TreeNode parentNode = new TreeNode();

                        parentNode.Text = dt.Rows[i]["Description"].ToString();
                        parentNode.Value = dt.Rows[i]["ID"].ToString();
                        parentNode.SelectAction = TreeNodeSelectAction.Select;

                        tvCategory.Nodes.Add(parentNode);
                        Childnode(parentNode);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
            //tvCategory.CollapseAll();
            // tvCategory.ExpandAll();
        }

        protected void Childnode(TreeNode parentNode)
        {
            try
            {
                DataTable dt = new DataTable();

                if (parentNode.Value == "1")
                {
                    dt = cbl.SelectForTreeview(Convert.ToInt32(parentNode.Value));
                }
                else
                {
                    dt = cbl.SelectForTreeview1(Convert.ToInt32(parentNode.Value));
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0; string cid = dt.Rows[i]["ParentID"].ToString();
                    while (i < dt.Rows.Count)
                    {
                        TreeNode childNode = new TreeNode();

                        if (cid == "1")
                        {
                            childNode.Text = dt.Rows[i]["Description"].ToString();
                            childNode.Value = dt.Rows[i]["ID"].ToString();
                            childNode.SelectAction = TreeNodeSelectAction.Select;
                            parentNode.ChildNodes.Add(childNode);
                            childNode.Collapse();
                            parentNode.Expand();
                        }
                        else
                        {
                            childNode.Text = "";
                            childNode.Value = null;
                            childNode.SelectAction = TreeNodeSelectAction.Select;
                            parentNode.ChildNodes.Add(childNode);
                        }

                        cid = dt.Rows[i]["ParentID"].ToString();

                        if (!String.IsNullOrWhiteSpace(childNode.Value))
                            Childnode(childNode);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void tvCategory_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            try
            {
                if (e.Node.Parent != null && !String.IsNullOrWhiteSpace(e.Node.Value.ToString()))
                {

                    int str = Int32.Parse(e.Node.Value);
                    e.Node.ChildNodes.Clear();
                    cbl = new Category_BL();


                    if (!String.IsNullOrWhiteSpace(txtdesc.Text.Trim()))
                    {
                        string Desc = null;
                        int PID = 0;
                        if (ViewState["IDdt"] != null)
                        {
                            Maindt = ViewState["IDdt"] as DataTable;
                            if (Maindt != null && Maindt.Rows.Count > 0)
                            {
                                for (int y = 0; y < Maindt.Rows.Count; y++)
                                {
                                    if (Convert.ToInt32(Maindt.Rows[y]["ParentID"].ToString()) == str)
                                    {
                                        PID = Convert.ToInt32(Maindt.Rows[y]["ID"].ToString());
                                        Desc = Maindt.Rows[y]["Description"].ToString();
                                        TreeNode childNode = new TreeNode();
                                        childNode.Text = Desc;
                                        childNode.Value = Convert.ToString(PID);
                                        childNode.SelectAction = TreeNodeSelectAction.Select;
                                        e.Node.ChildNodes.Add(childNode);
                                        SelectChildnode(childNode);
                                    }
                                }
                            }





                        }//viewstate
                        else
                        {
                            DataTable dts = cbl.SelectForTreeview(Convert.ToInt32(str));
                            if (dts.Rows.Count > 0)
                            {
                                int i = 0;
                                while (i < dts.Rows.Count)
                                {
                                    TreeNode childNode = new TreeNode();
                                    childNode.Text = dts.Rows[i]["Description"].ToString();
                                    childNode.Value = dts.Rows[i]["ID"].ToString();
                                    childNode.SelectAction = TreeNodeSelectAction.Select;
                                    e.Node.ChildNodes.Add(childNode);
                                    SelectChildnode(childNode);

                                    i++;
                                }
                            }
                        }

                    }
                    else
                    {
                        DataTable dt = cbl.SelectForTreeview(Convert.ToInt32(str));
                        if (dt.Rows.Count > 0)
                        {
                            int i = 0;
                            while (i < dt.Rows.Count)
                            {

                                TreeNode childNode = new TreeNode();
                                childNode.Text = dt.Rows[i]["Description"].ToString();
                                childNode.Value = dt.Rows[i]["ID"].ToString();
                                childNode.SelectAction = TreeNodeSelectAction.Select;
                                e.Node.ChildNodes.Add(childNode);
                                SelectChildnode(childNode);

                                i++;

                            }
                        }
                    }//else

                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }

        protected void SelectChildnode(TreeNode parentNode)
        {
            try
            {
                DataTable dt = new DataTable();
                string pid = parentNode.Value.ToString();

                dt = cbl.SelectForTreeview1(Convert.ToInt32(parentNode.Value));
                if (dt.Rows.Count > 0)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = "";
                    childNode.Value = null;
                    childNode.SelectAction = TreeNodeSelectAction.Select;
                    parentNode.ChildNodes.Add(childNode);
                }
            }
            catch (Exception ex)
            {
                Session["Exception"] = ex.ToString();
                Response.Redirect("~/CustomErrorPage.aspx?");
            }
        }
    }
}