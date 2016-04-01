using System;
using System.Collections.Generic;
using overrideSocial;

namespace fnpix
{
    public partial class login : System.Web.UI.Page
    {
        #region declarations

        private overrideSocial.users _users = new overrideSocial.users();
        private overrideSocial.permissions _permissions = new overrideSocial.permissions();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["noevents"] != null)
            {
                pnl_no_events_assigned.Visible = true;
            }
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
                Session["user_access"] = u.security_desc;

                List<Event> myevents = _permissions.select_permitted_events(Convert.ToInt32(Session["user_id"].ToString()));

                if (myevents.Count > 0)
                {
                    Session["event_id"] = myevents[0].id.ToString();

                    Response.Redirect("/dashboard");
                }
                else
                {
                    // if admin and no events exist at ALL, login and set the event_id to nothing and redirect to the Events page
                    if (u.security_desc == "system")
                    {
                        Session["event_id"] = "0";

                        Response.Redirect("/events");
                    }   
                    else
                    {
                        pnl_no_events_assigned.Visible = true;    
                    }
                }
            }
            else
            {
                pnl_error.Visible = true;
            }
        }
    }
}