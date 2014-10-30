using System;
using System.Web.Routing;

namespace fnpix
{
    public class global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // this is the routing engine
            RegisterRoutes(RouteTable.Routes);
        }

        void RegisterRoutes(RouteCollection routes)
        {
            // ######################## LOGIN SECTION ################################### //
            routes.MapPageRoute("login-route", "login", "~/login.aspx");
            routes.MapPageRoute("logout-route", "logout", "~/logout.aspx");

            // ######################## DASHBOARD SECTION ################################### //
            routes.MapPageRoute("dashboard-route", "dashboard", "~/dashboard.aspx");

            // ######################## MEDIA MANAGER SECTION ################################### //
            routes.MapPageRoute("media-all-route", "media", "~/media.aspx");
            routes.MapPageRoute("media-approve-route", "media/approve/{id}", "~/approve.aspx");
            routes.MapPageRoute("media-unapproved-route", "media/{unapproved}", "~/media.aspx");

            // ######################## SYSTEM PREFERENCES SECTION ################################### //
            routes.MapPageRoute("preferences-route", "preferences", "~/preferences.aspx");
            routes.MapPageRoute("preferences-add-route", "preferences/add", "~/add_preference.aspx");
            routes.MapPageRoute("preferences-edit-route", "preferences/edit/{id}", "~/add_preference.aspx");
            routes.MapPageRoute("preferences-delete-route", "preferences/delete/{id}", "~/delete_tag.aspx");

            // ######################## USERS SECTION ################################### //
            routes.MapPageRoute("users-route", "users", "~/users.aspx");
            routes.MapPageRoute("users-add-route", "users/add", "~/add_user.aspx");
            routes.MapPageRoute("users-edit-route", "users/edit/{id}", "~/add_user.aspx");
            routes.MapPageRoute("users-delete-route", "users/delete/{id}", "~/delete_user.aspx");

            // ######################## EVENTS SECTION ################################### //
            routes.MapPageRoute("events-manage-route", "events", "~/events.aspx");
            routes.MapPageRoute("events-add-route", "events/add", "~/add_event.aspx");
            routes.MapPageRoute("events-edit-rotue", "events/edit/{id}", "~/add_event.aspx");
            routes.MapPageRoute("events-delete-route", "events/delete/{id}", "~/delete_event.aspx");

            // ######################## PERMISSIONS SECTION ################################### //
            routes.MapPageRoute("permissions-by-user-route", "permissions/{id}", "~/permissions.aspx");
            routes.MapPageRoute("permissions-add-route", "permissions/add/{id}", "~/add_permission.aspx");
            routes.MapPageRoute("permissions-edit-route", "permissions/edit/{id}/{permission_id}", "~/add_permission.aspx");
            routes.MapPageRoute("permissions-delete-route", "permissions/delete/{user_id}/{id}", "~/delete_permission.aspx");

            // ######################## DROPBOX SECTION ################################### //
            routes.MapPageRoute("dropbox-setup-route", "dropbox-setup", "~/dropbox.aspx");
            routes.MapPageRoute("dropbox-media-main-route", "dropbox", "~/dropbox_media.aspx");
            routes.MapPageRoute("dropbox-media-unapproved-route", "dropbox/{unapproved}", "~/dropbox_media.aspx");
            routes.MapPageRoute("dropbox-visual-theme-route", "displays/dropbox/{id}", "~/visual/dropbox.aspx");

            // ######################## DISPLAYS SECTION ################################### //
            routes.MapPageRoute("visual-display-instagram-route", "displays/instagram/{id}", "~/visual/effect2.aspx");

            // this has to be last!!!!
            //routes.MapPageRoute("page-by-url-route", "{url}", "~/page.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}