using CK_WEB.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CK_WEB.Controllers
{
    public class CK_WEBController : Controller
    {
        // GET: CK_WEB
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CK_WEBConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [MACP],[GIA_M_BA],[KL_M_BA],[GIA_M_HAI],[KL_M_HAI],[GIA_M_MOT],[KL_M_MOT],[GIA_KHOP_MOI],[KL_KHOP_MOI],
                                                            [GIA_B_MOT],[KL_B_MOT],[GIA_B_HAI],[KL_B_HAI],[GIA_B_BA],[KL_B_BA],[TONG_KL_KHOP] From [dbo].[BANGGIATT]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    var listCus = reader.Cast<IDataRecord>()
                            .Select(x => new
                            {
                                MACP = (string)x["MACP"],
                                GIA_M_BA = (int)x["GIA_M_BA"],
                                KL_M_BA = (int)x["KL_M_BA"],
                                GIA_M_HAI = (int)x["GIA_M_HAI"],
                                KL_M_HAI = (int)x["KL_M_HAI"],
                                GIA_M_MOT = (int)x["GIA_M_MOT"],
                                KL_M_MOT = (int)x["KL_M_MOT"],
                                GIA_KHOP_MOI = (int)x["GIA_KHOP_MOI"],
                                KL_KHOP_MOI = (int)x["KL_KHOP_MOI"],
                                GIA_B_MOT = (int)x["GIA_B_MOT"],
                                KL_B_MOT = (int)x["KL_B_MOT"],
                                GIA_B_HAI = (int)x["GIA_B_HAI"],
                                KL_B_HAI = (int)x["KL_B_HAI"],
                                GIA_B_BA = (int)x["GIA_B_BA"],
                                KL_B_BA = (int)x["KL_B_BA"],
                                TONG_KL_KHOP = (int)x["TONG_KL_KHOP"],
                            }).ToList();

                    return Json(new { listCus = listCus }, JsonRequestBehavior.AllowGet);

                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            CusHub.Show();
        }
    }
}