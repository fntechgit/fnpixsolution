using System;
using overrideSocial;

namespace fnpix
{
    public partial class login : System.Web.UI.Page
    {
        #region declarations

        private overrideSocial.users _users = new overrideSocial.users();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void signin(object sender, EventArgs e)
        {
            User u = _users.login(email.Text.ToString(), pwd.Text.ToString());

            if (u.id > 0)
            {
                // you got it right
                Session["user_id"] = u.id.ToString();
                Session["user_name"] = u.first_name + " " + u.last_name;
                Session["company_name"] = u.company;
                Session["user_email"] = u.email;
                Session["user_pic"] = u.picture;

                Response.Redirect("/dashboard");
            }
            else
            {
                pnl_error.Visible = true;
            }
        }
    }
}