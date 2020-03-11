using MvcBreadCrumbs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;

namespace TransCanada.Controllers
{
    [BreadCrumb]
    public class TempController : Controller
    {
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
        // GET: Temp
        [BreadCrumb(Clear = true, Label = "Title List")]
        public ActionResult ListTitle()
        {
            List<LookUp> TitleList = new List<LookUp>();
            SqlConnection con = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd = new SqlCommand("tbl_titleList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LookUp Title = new LookUp();
                Title.Id = Convert.ToInt32(dt.Rows[j]["Id"].ToString());

                if (!string.IsNullOrEmpty(dt.Rows[j]["Title"].ToString()))
                {
                    Title.Description = dt.Rows[j]["Title"].ToString();
                }
                else
                {
                    Title.Description = string.Empty;
                }
                TitleList.Add(Title);
            }
                return View(TitleList);
        }

        [BreadCrumb(Label = "New Title")]

        // GET: Temp/Create
        public ActionResult CreateTitle()
        {
            return View();
        }

        // POST: Temp/Create
        [HttpPost]
        public ActionResult CreateTitle(LookUp title)
        {
            if (!ModelState.IsValid)
                return View(title);
            try
            {
                // TODO: Add insert logic here
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_TitleInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(title.Description))
                {
                    cmd.Parameters.AddWithValue("@Title", title.Description);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@Title", string.Empty);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("ListTitle");
            }
            catch(Exception ex)
            {
                return View(title);
            }
        }

        // GET: Temp/Edit/5
        [BreadCrumb(Label = "Update Title")]
        public ActionResult EditTitle(int id)
        {
            LookUp Title = new LookUp();
            SqlConnection conn = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd1 = new SqlCommand("tbl_titleEdit", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();

            da1.Fill(dt1);
               for (int i = 0; i < dt1.Rows.Count; i++)
            {
                
                Title.Id = Convert.ToInt32(dt1.Rows[i]["Id"].ToString());
                Title.Description = dt1.Rows[i]["Title"].ToString();

                
            }
        
            return View(Title);
        }

        // POST: Temp/Edit/5
        [HttpPost]
        public ActionResult EditTitle(int id, LookUp Title)
        {
            if (!ModelState.IsValid)
                return View(Title);
            try
            {
                // TODO: Add update logic here

                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_titleUpdate", con);
                cmd.Parameters.AddWithValue("@id",id);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(Title.Description))
                {
                    cmd.Parameters.AddWithValue("@Title", Title.Description);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@Title", string.Empty);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("ListTitle");
            }
            catch
            {
                return View(Title);
            }
        }

        // GET: Temp/Delete/5
        public ActionResult DeleteTitle(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(TransCanadaConnection);
                SqlCommand sqlCommand = new SqlCommand("tbl_titleDelete", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("ListTitle");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        [BreadCrumb(Clear = true, Label = "Function List")]

        public ActionResult ListFunction()
        {
            List<LookUp> functionList = new List<LookUp>();
            SqlConnection con = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd = new SqlCommand("tbl_functionList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LookUp function = new LookUp();
                function.Id = Convert.ToInt32(dt.Rows[j]["Id"].ToString());

                if (!string.IsNullOrEmpty(dt.Rows[j]["functionname"].ToString()))
                {
                    function.Description = dt.Rows[j]["functionname"].ToString();
                }
                else
                {
                    function.Description = string.Empty;
                }
                functionList.Add(function);
            }
            return View(functionList);
        }
        // GET: Temp/Create
        [BreadCrumb(Label = "New Function")]
        public ActionResult Createfunction()
        {
            return View();
        }

        // POST: Temp/Create
        [HttpPost]
        public ActionResult Createfunction(LookUp function)
        {
            if (!ModelState.IsValid)
                return View(function);
            try
            {
                // TODO: Add insert logic here
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_functionInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(function.Description))
                {
                    cmd.Parameters.AddWithValue("@functionname", function.Description);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@functionname", string.Empty);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("ListFunction");
            }
            catch (Exception ex)
            {
                return View(function);
            }
        }
        // GET: Temp/Edit/5
        [BreadCrumb(Label = "Update Function")]
        public ActionResult EditFunction(int id)
        {
            LookUp function = new LookUp();
            SqlConnection conn = new SqlConnection(TransCanadaConnection);

            SqlCommand cmd1 = new SqlCommand("tbl_functionEdit", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();

            da1.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                function.Id = Convert.ToInt32(dt1.Rows[i]["Id"].ToString());
                function.Description = dt1.Rows[i]["functionname"].ToString();


            }

            return View(function);
        }

        // POST: Temp/Edit/5
        [HttpPost]
        public ActionResult EditFunction(int id, LookUp function)
        {
            if (!ModelState.IsValid)
                return View(function);
            try
            {
                // TODO: Add update logic here

                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_functionUpdate", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(function.Description))
                {
                    cmd.Parameters.AddWithValue("@functionname", function.Description);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@functionname", string.Empty);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("ListFunction");
            }
            catch(Exception ex)
            {
                return View(function);
            }
        }
        public ActionResult Deletefunction(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(TransCanadaConnection);
                SqlCommand sqlCommand = new SqlCommand("tbl_functionDelete", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("ListFunction");
            }
            catch
            {
                return View();
            }
        }
        [BreadCrumb(Clear=true,Label = "Speciman Type List")]
        public ActionResult ListSpecimantype()
        {
            List<LookUp> SpecimanList = new List<LookUp>();
            SqlConnection con = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd = new SqlCommand("tbl_specimanTypeList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LookUp ST = new LookUp();
                ST.Id = Convert.ToInt32(dt.Rows[j]["Id"].ToString());

                if (!string.IsNullOrEmpty(dt.Rows[j]["specimanType"].ToString()))
                {
                    ST.Description = dt.Rows[j]["specimanType"].ToString();
                }
                else
                {
                    ST.Description = string.Empty;
                }
                SpecimanList.Add(ST);
            }
            return View(SpecimanList);
        }
        [BreadCrumb( Label = "New Speciman Type")]
        public ActionResult CreateSpeciman()
        {
            return View();
        }

        // POST: Temp/Create
        [HttpPost]
        public ActionResult CreateSpeciman(LookUp speciman)
        {
            if (!ModelState.IsValid)
                return View(speciman);
            try
            {
                // TODO: Add insert logic here
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_specimanType_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(speciman.Description))
                {
                    cmd.Parameters.AddWithValue("@specimanType", speciman.Description);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@specimanType", string.Empty);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("ListSpecimantype");
            }
            catch
            {
                return View(speciman);
            }
        }
        [BreadCrumb(Label = "Update Speciman Type")]
        public ActionResult EditSpeciman(int id)
        {
            LookUp speciman = new LookUp();
            SqlConnection conn = new SqlConnection(TransCanadaConnection);

            SqlCommand cmd1 = new SqlCommand("tbl_specimanType_Edit", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();

            da1.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                speciman.Id = Convert.ToInt32(dt1.Rows[i]["Id"].ToString());
                speciman.Description = dt1.Rows[i]["specimanType"].ToString();


            }

            return View(speciman);
        }

        // POST: Temp/Edit/5
        [HttpPost]
        public ActionResult EditSpeciman(int id, LookUp speciman)
        {
            if (!ModelState.IsValid)
                return View(speciman);
            try
            {
                // TODO: Add update logic here

                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_specimanType_Update", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(speciman.Description))
                {
                    cmd.Parameters.AddWithValue("@specimanType", speciman.Description);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@specimanType", string.Empty);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("ListSpecimantype");
            }
            catch
            {
                return View(speciman);
            }
        }
        public ActionResult DeleteSpeciman(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(TransCanadaConnection);
                SqlCommand sqlCommand = new SqlCommand("tbl_specimanType_Delete", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("ListSpecimantype");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public static List<SelectListItem> Title() 
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand selectCommand = new SqlCommand("tbl_titleList", con);
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<SelectListItem> TitleList = new List<SelectListItem>();
            for (int index = 0; index < dataTable.Rows.Count; index++)
                TitleList.Add(new SelectListItem
                {
                    Value = dataTable.Rows[index]["Id"].ToString().Trim(),
                    Text = string.IsNullOrEmpty(dataTable.Rows[index]["Title"].ToString().Trim()) ? string.Empty : dataTable.Rows[index]["Title"].ToString().Trim()
                });
            return TitleList;
        }
        public static List<SelectListItem> Function()
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand selectCommand = new SqlCommand("tbl_functionList", con);
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<SelectListItem> FunctionList = new List<SelectListItem>();
            for (int index = 0; index < dataTable.Rows.Count; index++)
                FunctionList.Add(new SelectListItem
                {
                    Value = dataTable.Rows[index]["Id"].ToString().Trim(),
                    Text = string.IsNullOrEmpty(dataTable.Rows[index]["functionname"].ToString().Trim()) ? string.Empty : dataTable.Rows[index]["functionname"].ToString().Trim()
                });
            return FunctionList;
        }
        public static List<SelectListItem> SpecimanType()
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand selectCommand = new SqlCommand("tbl_specimanTypeList", con);
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<SelectListItem> specimanTypeList = new List<SelectListItem>();
            for (int index = 0; index < dataTable.Rows.Count; index++)
                specimanTypeList.Add(new SelectListItem
                {
                    Value = dataTable.Rows[index]["Id"].ToString().Trim(),
                    Text = string.IsNullOrEmpty(dataTable.Rows[index]["specimanType"].ToString().Trim()) ? string.Empty : dataTable.Rows[index]["specimanType"].ToString().Trim()
                });
            return specimanTypeList;
        }
    }
}
