﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace overrideSocial
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="fntech_media")]
	public partial class FNTech_MediaDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertuser(user instance);
    partial void Updateuser(user instance);
    partial void Deleteuser(user instance);
    partial void Insertsetting(setting instance);
    partial void Updatesetting(setting instance);
    partial void Deletesetting(setting instance);
    partial void Insertmedia(media instance);
    partial void Updatemedia(media instance);
    partial void Deletemedia(media instance);
    partial void Insertplatform(platform instance);
    partial void Updateplatform(platform instance);
    partial void Deleteplatform(platform instance);
    partial void Inserttag(tag instance);
    partial void Updatetag(tag instance);
    partial void Deletetag(tag instance);
    #endregion
		
		public FNTech_MediaDataContext() : 
				base(global::overrideSocial.Properties.Settings.Default.fntech_mediaConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FNTech_MediaDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FNTech_MediaDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FNTech_MediaDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FNTech_MediaDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<user> users
		{
			get
			{
				return this.GetTable<user>();
			}
		}
		
		public System.Data.Linq.Table<setting> settings
		{
			get
			{
				return this.GetTable<setting>();
			}
		}
		
		public System.Data.Linq.Table<media> medias
		{
			get
			{
				return this.GetTable<media>();
			}
		}
		
		public System.Data.Linq.Table<platform> platforms
		{
			get
			{
				return this.GetTable<platform>();
			}
		}
		
		public System.Data.Linq.Table<tag> tags
		{
			get
			{
				return this.GetTable<tag>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.users")]
	public partial class user : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _first_name;
		
		private string _last_name;
		
		private string _email;
		
		private string _company;
		
		private System.DateTime _created;
		
		private bool _active;
		
		private int _notify_every_minutes;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void Onfirst_nameChanging(string value);
    partial void Onfirst_nameChanged();
    partial void Onlast_nameChanging(string value);
    partial void Onlast_nameChanged();
    partial void OnemailChanging(string value);
    partial void OnemailChanged();
    partial void OncompanyChanging(string value);
    partial void OncompanyChanged();
    partial void OncreatedChanging(System.DateTime value);
    partial void OncreatedChanged();
    partial void OnactiveChanging(bool value);
    partial void OnactiveChanged();
    partial void Onnotify_every_minutesChanging(int value);
    partial void Onnotify_every_minutesChanged();
    #endregion
		
		public user()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_first_name", DbType="VarChar(50)")]
		public string first_name
		{
			get
			{
				return this._first_name;
			}
			set
			{
				if ((this._first_name != value))
				{
					this.Onfirst_nameChanging(value);
					this.SendPropertyChanging();
					this._first_name = value;
					this.SendPropertyChanged("first_name");
					this.Onfirst_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_last_name", DbType="VarChar(50)")]
		public string last_name
		{
			get
			{
				return this._last_name;
			}
			set
			{
				if ((this._last_name != value))
				{
					this.Onlast_nameChanging(value);
					this.SendPropertyChanging();
					this._last_name = value;
					this.SendPropertyChanged("last_name");
					this.Onlast_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email", DbType="VarChar(MAX)")]
		public string email
		{
			get
			{
				return this._email;
			}
			set
			{
				if ((this._email != value))
				{
					this.OnemailChanging(value);
					this.SendPropertyChanging();
					this._email = value;
					this.SendPropertyChanged("email");
					this.OnemailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_company", DbType="VarChar(50)")]
		public string company
		{
			get
			{
				return this._company;
			}
			set
			{
				if ((this._company != value))
				{
					this.OncompanyChanging(value);
					this.SendPropertyChanging();
					this._company = value;
					this.SendPropertyChanged("company");
					this.OncompanyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_created", DbType="DateTime NOT NULL")]
		public System.DateTime created
		{
			get
			{
				return this._created;
			}
			set
			{
				if ((this._created != value))
				{
					this.OncreatedChanging(value);
					this.SendPropertyChanging();
					this._created = value;
					this.SendPropertyChanged("created");
					this.OncreatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_active", DbType="Bit NOT NULL")]
		public bool active
		{
			get
			{
				return this._active;
			}
			set
			{
				if ((this._active != value))
				{
					this.OnactiveChanging(value);
					this.SendPropertyChanging();
					this._active = value;
					this.SendPropertyChanged("active");
					this.OnactiveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_notify_every_minutes", DbType="Int NOT NULL")]
		public int notify_every_minutes
		{
			get
			{
				return this._notify_every_minutes;
			}
			set
			{
				if ((this._notify_every_minutes != value))
				{
					this.Onnotify_every_minutesChanging(value);
					this.SendPropertyChanging();
					this._notify_every_minutes = value;
					this.SendPropertyChanged("notify_every_minutes");
					this.Onnotify_every_minutesChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.settings")]
	public partial class setting : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _name;
		
		private string _value;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnvalueChanging(string value);
    partial void OnvalueChanged();
    #endregion
		
		public setting()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_value", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((this._value != value))
				{
					this.OnvalueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("value");
					this.OnvalueChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.media")]
	public partial class media : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _service;
		
		private string _username;
		
		private string _full_name;
		
		private string _description;
		
		private System.Nullable<System.DateTime> _createdate;
		
		private string _profilepic;
		
		private string _link;
		
		private int _likes;
		
		private string _latitude;
		
		private string _longitude;
		
		private bool _approved;
		
		private System.Nullable<int> _approved_by;
		
		private System.Nullable<System.DateTime> _approved_date;
		
		private System.DateTime _added_to_db_date;
		
		private string _location_name;
		
		private string _source_id;
		
		private System.Nullable<int> _width;
		
		private System.Nullable<int> _height;
		
		private bool _is_video;
		
		private string _source;
		
		private string _tags;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnserviceChanging(string value);
    partial void OnserviceChanged();
    partial void OnusernameChanging(string value);
    partial void OnusernameChanged();
    partial void Onfull_nameChanging(string value);
    partial void Onfull_nameChanged();
    partial void OndescriptionChanging(string value);
    partial void OndescriptionChanged();
    partial void OncreatedateChanging(System.Nullable<System.DateTime> value);
    partial void OncreatedateChanged();
    partial void OnprofilepicChanging(string value);
    partial void OnprofilepicChanged();
    partial void OnlinkChanging(string value);
    partial void OnlinkChanged();
    partial void OnlikesChanging(int value);
    partial void OnlikesChanged();
    partial void OnlatitudeChanging(string value);
    partial void OnlatitudeChanged();
    partial void OnlongitudeChanging(string value);
    partial void OnlongitudeChanged();
    partial void OnapprovedChanging(bool value);
    partial void OnapprovedChanged();
    partial void Onapproved_byChanging(System.Nullable<int> value);
    partial void Onapproved_byChanged();
    partial void Onapproved_dateChanging(System.Nullable<System.DateTime> value);
    partial void Onapproved_dateChanged();
    partial void Onadded_to_db_dateChanging(System.DateTime value);
    partial void Onadded_to_db_dateChanged();
    partial void Onlocation_nameChanging(string value);
    partial void Onlocation_nameChanged();
    partial void Onsource_idChanging(string value);
    partial void Onsource_idChanged();
    partial void OnwidthChanging(System.Nullable<int> value);
    partial void OnwidthChanged();
    partial void OnheightChanging(System.Nullable<int> value);
    partial void OnheightChanged();
    partial void Onis_videoChanging(bool value);
    partial void Onis_videoChanged();
    partial void OnsourceChanging(string value);
    partial void OnsourceChanged();
    partial void OntagsChanging(string value);
    partial void OntagsChanged();
    #endregion
		
		public media()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_service", DbType="VarChar(50)")]
		public string service
		{
			get
			{
				return this._service;
			}
			set
			{
				if ((this._service != value))
				{
					this.OnserviceChanging(value);
					this.SendPropertyChanging();
					this._service = value;
					this.SendPropertyChanged("service");
					this.OnserviceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_username", DbType="VarChar(100)")]
		public string username
		{
			get
			{
				return this._username;
			}
			set
			{
				if ((this._username != value))
				{
					this.OnusernameChanging(value);
					this.SendPropertyChanging();
					this._username = value;
					this.SendPropertyChanged("username");
					this.OnusernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_full_name", DbType="VarChar(MAX)")]
		public string full_name
		{
			get
			{
				return this._full_name;
			}
			set
			{
				if ((this._full_name != value))
				{
					this.Onfull_nameChanging(value);
					this.SendPropertyChanging();
					this._full_name = value;
					this.SendPropertyChanged("full_name");
					this.Onfull_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_description", DbType="VarChar(MAX)")]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				if ((this._description != value))
				{
					this.OndescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("description");
					this.OndescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_createdate", DbType="DateTime")]
		public System.Nullable<System.DateTime> createdate
		{
			get
			{
				return this._createdate;
			}
			set
			{
				if ((this._createdate != value))
				{
					this.OncreatedateChanging(value);
					this.SendPropertyChanging();
					this._createdate = value;
					this.SendPropertyChanged("createdate");
					this.OncreatedateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_profilepic", DbType="VarChar(MAX)")]
		public string profilepic
		{
			get
			{
				return this._profilepic;
			}
			set
			{
				if ((this._profilepic != value))
				{
					this.OnprofilepicChanging(value);
					this.SendPropertyChanging();
					this._profilepic = value;
					this.SendPropertyChanged("profilepic");
					this.OnprofilepicChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_link", DbType="VarChar(MAX)")]
		public string link
		{
			get
			{
				return this._link;
			}
			set
			{
				if ((this._link != value))
				{
					this.OnlinkChanging(value);
					this.SendPropertyChanging();
					this._link = value;
					this.SendPropertyChanged("link");
					this.OnlinkChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_likes", DbType="Int NOT NULL")]
		public int likes
		{
			get
			{
				return this._likes;
			}
			set
			{
				if ((this._likes != value))
				{
					this.OnlikesChanging(value);
					this.SendPropertyChanging();
					this._likes = value;
					this.SendPropertyChanged("likes");
					this.OnlikesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_latitude", DbType="VarChar(50)")]
		public string latitude
		{
			get
			{
				return this._latitude;
			}
			set
			{
				if ((this._latitude != value))
				{
					this.OnlatitudeChanging(value);
					this.SendPropertyChanging();
					this._latitude = value;
					this.SendPropertyChanged("latitude");
					this.OnlatitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_longitude", DbType="VarChar(50)")]
		public string longitude
		{
			get
			{
				return this._longitude;
			}
			set
			{
				if ((this._longitude != value))
				{
					this.OnlongitudeChanging(value);
					this.SendPropertyChanging();
					this._longitude = value;
					this.SendPropertyChanged("longitude");
					this.OnlongitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_approved", DbType="Bit NOT NULL")]
		public bool approved
		{
			get
			{
				return this._approved;
			}
			set
			{
				if ((this._approved != value))
				{
					this.OnapprovedChanging(value);
					this.SendPropertyChanging();
					this._approved = value;
					this.SendPropertyChanged("approved");
					this.OnapprovedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_approved_by", DbType="Int")]
		public System.Nullable<int> approved_by
		{
			get
			{
				return this._approved_by;
			}
			set
			{
				if ((this._approved_by != value))
				{
					this.Onapproved_byChanging(value);
					this.SendPropertyChanging();
					this._approved_by = value;
					this.SendPropertyChanged("approved_by");
					this.Onapproved_byChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_approved_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> approved_date
		{
			get
			{
				return this._approved_date;
			}
			set
			{
				if ((this._approved_date != value))
				{
					this.Onapproved_dateChanging(value);
					this.SendPropertyChanging();
					this._approved_date = value;
					this.SendPropertyChanged("approved_date");
					this.Onapproved_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_added_to_db_date", DbType="DateTime NOT NULL")]
		public System.DateTime added_to_db_date
		{
			get
			{
				return this._added_to_db_date;
			}
			set
			{
				if ((this._added_to_db_date != value))
				{
					this.Onadded_to_db_dateChanging(value);
					this.SendPropertyChanging();
					this._added_to_db_date = value;
					this.SendPropertyChanged("added_to_db_date");
					this.Onadded_to_db_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_location_name", DbType="VarChar(MAX)")]
		public string location_name
		{
			get
			{
				return this._location_name;
			}
			set
			{
				if ((this._location_name != value))
				{
					this.Onlocation_nameChanging(value);
					this.SendPropertyChanging();
					this._location_name = value;
					this.SendPropertyChanged("location_name");
					this.Onlocation_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_source_id", DbType="VarChar(100)")]
		public string source_id
		{
			get
			{
				return this._source_id;
			}
			set
			{
				if ((this._source_id != value))
				{
					this.Onsource_idChanging(value);
					this.SendPropertyChanging();
					this._source_id = value;
					this.SendPropertyChanged("source_id");
					this.Onsource_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_width", DbType="Int")]
		public System.Nullable<int> width
		{
			get
			{
				return this._width;
			}
			set
			{
				if ((this._width != value))
				{
					this.OnwidthChanging(value);
					this.SendPropertyChanging();
					this._width = value;
					this.SendPropertyChanged("width");
					this.OnwidthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_height", DbType="Int")]
		public System.Nullable<int> height
		{
			get
			{
				return this._height;
			}
			set
			{
				if ((this._height != value))
				{
					this.OnheightChanging(value);
					this.SendPropertyChanging();
					this._height = value;
					this.SendPropertyChanged("height");
					this.OnheightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_is_video", DbType="Bit NOT NULL")]
		public bool is_video
		{
			get
			{
				return this._is_video;
			}
			set
			{
				if ((this._is_video != value))
				{
					this.Onis_videoChanging(value);
					this.SendPropertyChanging();
					this._is_video = value;
					this.SendPropertyChanged("is_video");
					this.Onis_videoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_source", DbType="VarChar(MAX)")]
		public string source
		{
			get
			{
				return this._source;
			}
			set
			{
				if ((this._source != value))
				{
					this.OnsourceChanging(value);
					this.SendPropertyChanging();
					this._source = value;
					this.SendPropertyChanged("source");
					this.OnsourceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tags", DbType="VarChar(MAX)")]
		public string tags
		{
			get
			{
				return this._tags;
			}
			set
			{
				if ((this._tags != value))
				{
					this.OntagsChanging(value);
					this.SendPropertyChanging();
					this._tags = value;
					this.SendPropertyChanged("tags");
					this.OntagsChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.platforms")]
	public partial class platform : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _value;
		
		private bool _active;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnvalueChanging(string value);
    partial void OnvalueChanged();
    partial void OnactiveChanging(bool value);
    partial void OnactiveChanged();
    #endregion
		
		public platform()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_value", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((this._value != value))
				{
					this.OnvalueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("value");
					this.OnvalueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_active", DbType="Bit NOT NULL")]
		public bool active
		{
			get
			{
				return this._active;
			}
			set
			{
				if ((this._active != value))
				{
					this.OnactiveChanging(value);
					this.SendPropertyChanging();
					this._active = value;
					this.SendPropertyChanged("active");
					this.OnactiveChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tags")]
	public partial class tag : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _value;
		
		private bool _is_tag;
		
		private bool _entire_event;
		
		private System.Nullable<System.DateTime> _start_time;
		
		private System.Nullable<System.DateTime> _end_time;
		
		private bool _facebook;
		
		private bool _instagram;
		
		private bool _twitter;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnvalueChanging(string value);
    partial void OnvalueChanged();
    partial void Onis_tagChanging(bool value);
    partial void Onis_tagChanged();
    partial void Onentire_eventChanging(bool value);
    partial void Onentire_eventChanged();
    partial void Onstart_timeChanging(System.Nullable<System.DateTime> value);
    partial void Onstart_timeChanged();
    partial void Onend_timeChanging(System.Nullable<System.DateTime> value);
    partial void Onend_timeChanged();
    partial void OnfacebookChanging(bool value);
    partial void OnfacebookChanged();
    partial void OninstagramChanging(bool value);
    partial void OninstagramChanged();
    partial void OntwitterChanging(bool value);
    partial void OntwitterChanged();
    #endregion
		
		public tag()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_value", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((this._value != value))
				{
					this.OnvalueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("value");
					this.OnvalueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_is_tag", DbType="Bit NOT NULL")]
		public bool is_tag
		{
			get
			{
				return this._is_tag;
			}
			set
			{
				if ((this._is_tag != value))
				{
					this.Onis_tagChanging(value);
					this.SendPropertyChanging();
					this._is_tag = value;
					this.SendPropertyChanged("is_tag");
					this.Onis_tagChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_entire_event", DbType="Bit NOT NULL")]
		public bool entire_event
		{
			get
			{
				return this._entire_event;
			}
			set
			{
				if ((this._entire_event != value))
				{
					this.Onentire_eventChanging(value);
					this.SendPropertyChanging();
					this._entire_event = value;
					this.SendPropertyChanged("entire_event");
					this.Onentire_eventChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_start_time", DbType="DateTime")]
		public System.Nullable<System.DateTime> start_time
		{
			get
			{
				return this._start_time;
			}
			set
			{
				if ((this._start_time != value))
				{
					this.Onstart_timeChanging(value);
					this.SendPropertyChanging();
					this._start_time = value;
					this.SendPropertyChanged("start_time");
					this.Onstart_timeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_end_time", DbType="DateTime")]
		public System.Nullable<System.DateTime> end_time
		{
			get
			{
				return this._end_time;
			}
			set
			{
				if ((this._end_time != value))
				{
					this.Onend_timeChanging(value);
					this.SendPropertyChanging();
					this._end_time = value;
					this.SendPropertyChanged("end_time");
					this.Onend_timeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_facebook", DbType="Bit NOT NULL")]
		public bool facebook
		{
			get
			{
				return this._facebook;
			}
			set
			{
				if ((this._facebook != value))
				{
					this.OnfacebookChanging(value);
					this.SendPropertyChanging();
					this._facebook = value;
					this.SendPropertyChanged("facebook");
					this.OnfacebookChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_instagram", DbType="Bit NOT NULL")]
		public bool instagram
		{
			get
			{
				return this._instagram;
			}
			set
			{
				if ((this._instagram != value))
				{
					this.OninstagramChanging(value);
					this.SendPropertyChanging();
					this._instagram = value;
					this.SendPropertyChanged("instagram");
					this.OninstagramChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_twitter", DbType="Bit NOT NULL")]
		public bool twitter
		{
			get
			{
				return this._twitter;
			}
			set
			{
				if ((this._twitter != value))
				{
					this.OntwitterChanging(value);
					this.SendPropertyChanging();
					this._twitter = value;
					this.SendPropertyChanged("twitter");
					this.OntwitterChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
